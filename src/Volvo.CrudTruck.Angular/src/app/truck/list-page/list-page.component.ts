import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { BaseResponse } from '@app/@shared/models/base-response';
import { CredentialsService } from '@app/auth';
import { I18nService } from '@app/i18n';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { Truck } from '../model/truck';
import { TruckService } from '../services/truck.service';
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

import Swal from 'sweetalert2';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-list-page',
  templateUrl: './list-page.component.html',
  styleUrls: ['./list-page.component.scss'],
})
export class ListPageComponent implements OnInit {
  public playTooltip: string;
  public stopTooltip: string;
  public editTooltip: string;
  public saveTooltip: string;
  public detailsTooltip: string;
  public deleteTooltip: string;
  public unableEditDeleteAlert: string;
  public warning: string;
  public event: string;
  public new: string;
  public searchPlaceholder: string = '';
  public filter: string;

  private token: string;
  public trucks: Truck[];

  displayedColumns = ['model', 'fabrication', 'modelYear', 'chassisCode', 'engineCode', 'actions'];
  public noData = true;
  public selectedModel?: Truck;
  public dataSource = new MatTableDataSource<Truck>([]);

  @ViewChild('paginator', { static: true }) paginator: MatPaginator;
  @ViewChild('sort', { static: true }) sort: MatSort;
  constructor(
    private truckService: TruckService,
    private credentialsService: CredentialsService,
    protected toastrService: ToastrService,
    private readonly translateService: TranslateService,
    private i18nService: I18nService,
    protected spinner: NgxSpinnerService,
    private router: Router
  ) {}

  protected getTranslations(): void {
    this.playTooltip = this.translateService.instant('Play');
    this.stopTooltip = this.translateService.instant('Stop');
    this.editTooltip = this.translateService.instant('Edit');
    this.saveTooltip = this.translateService.instant('Save');
    this.detailsTooltip = this.translateService.instant('Details');
    this.deleteTooltip = this.translateService.instant('Delete');
    this.unableEditDeleteAlert = this.translateService.instant('Unable Edit/Delete Instance');
    this.warning = this.translateService.instant('Warning');

    this.new = this.translateService.instant('New Instance');
    this.searchPlaceholder = this.translateService.instant('Search');
    this.filter = this.translateService.instant('Filter');
  }

  ngOnInit(): void {
    this.token = this.credentialsService.credentials.token;
    this.translateService.onLangChange.subscribe((event: LangChangeEvent) => {
      this.getTranslations();
    });

    this.getTrucks();
  }

  getTrucks() {
    this.spinner.show();
    this.truckService.getAll(this.token).subscribe(
      (res: BaseResponse<Truck[]>) => {
        if (res.error) {
          console.log(res.message);
          return;
        }
        this.trucks = res.data;
        this.renderGrid(this.trucks);
      },
      (err) => {},
      () => {
        this.spinner.hide();
      }
    );
  }
  newItem() {
    this.router.navigate(['truck/new']);
  }
  editItem(item: Truck) {
    this.router.navigate([`truck/${item.id}/edit`]);
  }

  deleteItem(item: Truck) {
    Swal.fire({
      title: this.translateService.instant('Confirm Delete'),
      text: '',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      cancelButtonText: this.translateService.instant('Cancel'),
      confirmButtonText: this.translateService.instant('Yes'),
    }).then((result) => {
      if (result.isConfirmed) {
        this.truckService.delete(item.id, this.token).subscribe((res: BaseResponse<any>) => {
          if (res.error) {
            this.toastrService.error(`${res.message}`, this.event);
            return;
          }

          this.toastrService.success(`${res.message}`, this.event);
          this.refreshTable();
        });
      }
    });
  }

  refreshTable() {
    this.getTrucks();
  }

  search(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  searchData() {
    this.spinner.show();
    this.truckService.getAll(this.token).subscribe((res: BaseResponse<Truck[]>) => {
      this.trucks = res.data;
      this.spinner.hide();
      this.renderGrid(this.trucks);
    });
  }

  select(item: Truck) {
    this.selectedModel = !this.selectedModel || this.selectedModel !== item ? item : null;
  }

  protected renderGrid(tableArr: Truck[]) {
    this.dataSource = new MatTableDataSource<Truck>(tableArr);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.noData = this.dataSource.filteredData.length === 0;
  }
}
