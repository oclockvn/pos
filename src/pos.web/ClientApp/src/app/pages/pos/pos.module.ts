import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PosRoutingModule } from './pos-routing.module';
import { PosComponent } from './pos.component';
import { PaymentComponent } from './payment/payment.component';
import { PosHeaderComponent } from './pos-header/pos-header.component';
import { PosPaymentComponent } from './pos-payment/pos-payment.component';
import { PosCartComponent } from './pos-cart/pos-cart.component';


@NgModule({
  declarations: [
    PosComponent,
    PaymentComponent,
    PosHeaderComponent,
    PosPaymentComponent,
    PosCartComponent
  ],
  imports: [
    CommonModule,
    PosRoutingModule
  ]
})
export class PosModule { }
