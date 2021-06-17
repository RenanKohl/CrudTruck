import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Logger } from '@app/@core';
import { BaseResponse } from '@app/@shared/models/base-response';
import { CredentialsService } from '@app/auth';
import { I18nService } from '@app/i18n';
import { TranslateService } from '@ngx-translate/core';
import { parseInt } from 'lodash';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { switchMap } from 'rxjs/internal/operators/switchMap';
import Swal from 'sweetalert2';
import { Truck } from '../model/truck';
import { TruckService } from '../services/truck.service';

const log = new Logger('FormPageComponent');
@Component({
  selector: 'app-form-page',
  templateUrl: './form-page.component.html',
  styleUrls: ['./form-page.component.scss'],
})
export class FormPageComponent implements OnInit {
  protected editLabel: string;
  protected newLabel: string;

  truck: Truck;
  currentAction: string;
  formData: FormGroup;
  pageTitle: string;
  serverErrorMessages: string[] = null;
  submittingForm = false;

  protected token: string;

  constructor(
    private truckService: TruckService,
    private credentialsService: CredentialsService,
    protected toastr: ToastrService,
    protected spinner: NgxSpinnerService,
    private readonly translateService: TranslateService,
    private i18nService: I18nService,
    private router: Router,
    private route: ActivatedRoute,
    protected formBuilder: FormBuilder
  ) {}
  public getTranslations(): void {
    this.editLabel = this.translateService.instant('Edit');
    this.newLabel = this.translateService.instant('New');
  }

  ngOnInit(): void {
    this.token = this.credentialsService.credentials.token;
    this.getTranslations();
    this.setCurrentAction();
    this.buildForm();
    this.loadResource(this.token);
  }

  protected loadResource(token: string) {
    this.spinner.show();
    if (this.currentAction == 'edit') {
      this.route.paramMap
        .pipe(
          switchMap((params) => {
            return this.truckService.getById(parseInt(params.get('id')), token);
          })
        )
        .subscribe(
          (resource: BaseResponse<Truck>) => {
            this.truck = resource.data;
            this.formData.patchValue(resource.data);
            this.spinner.hide();
          },
          (error) => {
            Swal.fire('Aviso', 'Ocorreu um erro no servidor, tente mais tarde.');
            console.log(error);
          }
        );
    }
    this.spinner.hide();
  }

  submitForm(event: any) {
    this.spinner.show();
    this.submittingForm = true;
    console.log(this.submittingForm);
    if (this.currentAction === 'new') {
      this.create();
    } else {
      this.update();
    }

    // this.spinner.hide();
  }

  ngAfterContentChecked() {
    this.setPageTitle();
  }
  back() {
    this.router.navigateByUrl('/truck');
  }
  get yearModels(): number[] {
    // const year = new Date().getFullYear();
    let yearModels = [];
    for (let year = new Date().getFullYear(); year < year + 2; year++) {
      yearModels.push(year);
    }

    return yearModels;
  }

  protected create() {
    const truck: Truck = Object.assign(Truck, this.formData.value);

    this.truckService.create(truck, this.token).subscribe(
      (response) => this.actionsForSuccess(response),
      (error) => this.actionsForError(error)
    );
  }

  protected update() {
    const truck: Truck = Object.assign(Truck, this.formData.value);
    this.truckService.update(truck, this.token).subscribe(
      (response) => this.actionsForSuccess(response),
      (error) => this.actionsForError(error)
    );
  }

  protected actionsForSuccess(response: BaseResponse<Truck>) {
    if (!response.error) {
      this.toastr.success(response.message, '');
    }

    const baseComponentPath: string = this.route.snapshot.parent.url[0].path;
    this.router.navigateByUrl(baseComponentPath, { skipLocationChange: true }).then(() => {
      this.router.navigate([baseComponentPath, this.truck.id, 'edit']);
    });
  }

  protected actionsForError(error: any) {
    this.submittingForm = false;
    this.spinner.hide();
    log.error(error);
  }

  protected buildForm(): void {
    this.formData = this.formBuilder.group({
      id: [null],
      model: [null, [Validators.required]],
      modelYear: [null, [Validators.required]],
    });
  }

  protected setCurrentAction() {
    if (this.route.snapshot.url[0].path === 'new') {
      this.currentAction = 'new';
    } else {
      this.currentAction = 'edit';
    }
  }

  protected setPageTitle() {
    if (this.currentAction === 'new') {
      this.pageTitle = this.newLabel;
    } else {
      this.pageTitle = this.editLabel;
    }
  }
}
