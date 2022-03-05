import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { ProductListRoutingModule } from "./product-list-routing.module";
import { ProductListComponent } from "./product-list.component";
import { NgSelectModule } from "@ng-select/ng-select";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { NgxDatatableModule } from "@swimlane/ngx-datatable";

const lib = [NgSelectModule, NgxDatatableModule];

@NgModule({
  declarations: [ProductListComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ProductListRoutingModule,
    ...lib,
  ],
})
export class ProductListModule {}
