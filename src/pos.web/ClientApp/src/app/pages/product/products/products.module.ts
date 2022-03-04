import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductCreateComponent } from './product-create/product-create.component';
import { ProductUpdateComponent } from './product-update/product-update.component';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { NgxDatatableModule } from "@swimlane/ngx-datatable";

const lib = [NgxDatatableModule];

@NgModule({
  declarations: [
    ProductListComponent,
    ProductDetailComponent,
    ProductCreateComponent,
    ProductUpdateComponent,
    ProductEditComponent,
  ],
  imports: [CommonModule, ProductsRoutingModule, ...lib],
})
export class ProductsModule {}
