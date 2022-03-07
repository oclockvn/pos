import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import {
  ProductListPageState,
  PRODUCT_PAGE_STATE,
} from "./product-list/states/productListPage.state";
import { RxState } from "@rx-angular/state";
import { ProductsComponent } from "./products.component";

@NgModule({
  declarations: [ProductsComponent],
  imports: [CommonModule, ProductsRoutingModule],
  providers: [
    {
      provide: PRODUCT_PAGE_STATE,
      useFactory: () => new RxState<ProductListPageState>(),
    },
  ],
})
export class ProductsModule {}
