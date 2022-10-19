import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OnlineOfflineService } from '@app/@core/onlineoffline.service';
import { BaseResponse } from '@app/@shared/models/base-response';
import { environment } from '@env/environment';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Truck } from '../model/truck';
import { TruckModel } from '../model/truck.model';

import Dexie, { Table } from 'dexie';
import { CredentialsService } from '@app/auth';

@Injectable({
  providedIn: 'root',
})
export class TruckService {
  apiPath: string = `${environment.serverUrl}/trucks`;
  private db: any;
  private token: string;

  constructor(
    private http: HttpClient,
    private readonly onlineOfflineService: OnlineOfflineService,
    public credentialsService: CredentialsService
  ) {
    this.token = this.credentialsService.credentials.token;

    this.registerToEvents(onlineOfflineService);
    this.createDatabase();
  }

  private registerToEvents(onlineOfflineService: OnlineOfflineService) {
    onlineOfflineService.connectionChanged.subscribe((online: any) => {
      if (online) {
        console.log('went online');
        console.log('sending all stored items');
        this.sendItemsFromIndexedDb();
      } else {
        console.log('went offline, storing in indexdb');
      }
    });
  }

  async getAllFromIndexedDb() {
    const allItems: Truck[] = await this.db.trucks.toArray();
    return { data: allItems, error: false, message: '' } as BaseResponse<Truck[]>;
  }

  getAll(): Observable<BaseResponse<Truck[]>> {
    return this.http.get(this.apiPath, this.setHeader()).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  getById(id: number): Observable<BaseResponse<Truck>> {
    const url = `${this.apiPath}/${id}`;

    return this.http.get(url, this.setHeader()).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  create(resource: TruckModel): Observable<BaseResponse<Truck>> {
    if (!this.onlineOfflineService.isOnline) {
      this.addToIndexedDb({ model: resource.model, modelYear: resource.modelYear } as Truck);
      return of({ data: resource, error: false, message: 'Registro incluído com sucesso' } as BaseResponse<Truck>);
    }

    return this.http.post(this.apiPath, resource, this.setHeader()).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  update(id: number, resource: TruckModel): Observable<BaseResponse<Truck>> {
    const url = `${this.apiPath}/${id}`;
    return this.http.put(url, resource, this.setHeader()).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  delete(id: number): Observable<BaseResponse<Truck>> {
    const url = `${this.apiPath}/${id}`;

    if (!this.onlineOfflineService.isOnline) {
      this.db.trucks.delete(id).then(() => {
        console.log(`item ${id} deleted locally`);
      });

      return of({ error: false, message: 'Registro removido com sucesso' } as BaseResponse<Truck>);
    }

    return this.http.delete(url, this.setHeader()).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  protected setHeader(): any {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${this.token}`,
      }),
    };
    return httpOptions;
  }

  private createDatabase() {
    this.db = new Dexie('MyTrucksDatabase');
    this.db.version(1).stores({
      trucks: '++id,model,fabrication,modelYear,chassisCode,engineCode',
    });
  }

  private addToIndexedDb(truck: Truck) {
    this.db.trucks
      .add(truck)
      .then(async () => {
        const allItems: Truck[] = await this.db.trucks.toArray();
        console.log('saved in DB, DB is now', allItems);
      })
      .catch((e: any) => {
        alert('Error: ' + (e.stack || e));
      });
  }

  private async sendItemsFromIndexedDb() {
    const allItems: Truck[] = await this.db.trucks.toArray();

    allItems.forEach((item: Truck) => {
      // send items to backend...
      this.create(item).subscribe((res: BaseResponse<any>) => {
        if (!res.error) {
          this.db.trucks.delete(item.id).then(() => {
            console.log(`item ${item.id} sent and deleted locally`);
          });
        } else {
          console.error(`Error to send ${item.id} to API`);
        }
      });
    });
  }
}
