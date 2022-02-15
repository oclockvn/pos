import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PosRoutingModule } from './pos-routing.module';
import { PosComponent } from './pos.component';
import { PaymentComponent } from './payment/payment.component';


@NgModule({
  declarations: [
    PosComponent,
    PaymentComponent
  ],
  imports: [
    CommonModule,
    PosRoutingModule
  ]
})
export class PosModule { }
