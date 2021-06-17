import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TruckRoutingModule } from './truck-routing.module';
import { ListPageComponent } from './list-page/list-page.component';
import { FormPageComponent } from './form-page/form-page.component';
import { SharedModule } from '@app/@shared';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [ListPageComponent, FormPageComponent],
  imports: [CommonModule, SharedModule, TruckRoutingModule, ReactiveFormsModule],
})
export class TruckModule {}
