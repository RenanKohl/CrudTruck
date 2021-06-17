import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse } from '@app/@shared/models/base-response';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Truck } from '../model/truck';

@Injectable({
  providedIn: 'root',
})
export class TruckService {
  apiPath: string = `${environment.serverUrl}/trucks`;
  constructor(private http: HttpClient) {}

  getAll(token: string): Observable<BaseResponse<Truck[]>> {
    return this.http.get(this.apiPath, this.setHeader(token)).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  getById(id: number, token: string): Observable<BaseResponse<Truck>> {
    const url = `${this.apiPath}/${id}`;

    return this.http.get(url, this.setHeader(token)).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  create(resource: Truck, token: string): Observable<BaseResponse<Truck>> {
    return this.http.post(this.apiPath, resource, this.setHeader(token)).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  update(resource: Truck, token: string): Observable<BaseResponse<Truck>> {
    const url = `${this.apiPath}/${resource.id}`;
    return this.http.put(url, resource, this.setHeader(token)).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  delete(id: number, token: string): Observable<BaseResponse<Truck>> {
    const url = `${this.apiPath}/${id}`;

    return this.http.delete(url, this.setHeader(token)).pipe(
      map((res: any) => {
        return res;
      })
    );
  }

  protected setHeader(token: string): any {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      }),
    };
    return httpOptions;
  }
}
