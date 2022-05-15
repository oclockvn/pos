import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import {
  ProductListPageState,
  PRODUCT_PAGE_STATE,
} from "./product-list/states/productListPage.state";
import { RxState } from "@rx-angular/state";
import { ProductsComponent } from "./products.component";
import { CategoryModalComponent } from "../shared";
import { ReactiveFormsModule } from "@angular/forms";

const shared = [CategoryModalComponent];

@NgModule({
  declarations: [ProductsComponent, ...shared],
  imports: [CommonModule, ProductsRoutingModule, ReactiveFormsModule],
  providers: [
    {
      provide: PRODUCT_PAGE_STATE,
      useFactory: () => new RxState<ProductListPageState>(),
    },
  ],
})
export class ProductsModule {}
