import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoaderComponent } from './loader/loader.component';
import { BreadCrumbComponent } from './bread-crumb/bread-crumb.component';
import { TranslateModule } from '@ngx-translate/core';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material.module';
import { I18nModule } from '@app/i18n';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormFieldErrorComponent } from './form-field-error/form-field-error.component';
import { NgxSpinnerModule } from 'ngx-spinner';


@NgModule({
  imports: [CommonModule, TranslateModule, RouterModule, TranslateModule, I18nModule, NgbModule],
  declarations: [LoaderComponent, BreadCrumbComponent, FormFieldErrorComponent],
  exports: [
    LoaderComponent,
    BreadCrumbComponent,
    FormFieldErrorComponent,
    MaterialModule,
    TranslateModule,
    I18nModule,
    NgbModule,
    NgxSpinnerModule,
    
  ],
})
export class SharedModule {}
