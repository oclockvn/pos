import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PosRoutingModule } from './pos-routing.module';
import { PosComponent } from './pos.component';


@NgModule({
  declarations: [
    PosComponent
  ],
  imports: [
    CommonModule,
    PosRoutingModule
  ]
})
export class PosModule { }
