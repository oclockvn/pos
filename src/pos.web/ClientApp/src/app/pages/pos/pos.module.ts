import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PosRoutingModule } from './pos-routing.module';
import { PosComponent } from "./pos.component";
import { PosHeaderComponent } from "./pos-header/pos-header.component";
import { PosPaymentComponent } from "./pos-payment/pos-payment.component";
import { PosCartComponent } from "./pos-cart/pos-cart.component";
import { PosState, POS_STATE } from "./states/pos.state";
import { RxState } from "@rx-angular/state";
import { NgSelectModule } from "@ng-select/ng-select";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@NgModule({
  declarations: [
    PosComponent,
    PosHeaderComponent,
    PosPaymentComponent,
    PosCartComponent,
  ],
  imports: [
    CommonModule,
    PosRoutingModule,
    NgSelectModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    {
      provide: POS_STATE,
      useFactory: () => new RxState<PosState>(),
    },
  ],
})
export class PosModule {}
