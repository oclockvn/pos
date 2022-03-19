import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ProductEditRoutingModule } from "./product-edit-routing.module";

import { ProductEditComponent } from "./product-edit.component";

import { NgSelectModule } from "@ng-select/ng-select";
import { QuillModule } from "ngx-quill";
import { HotToastModule } from "@ngneat/hot-toast";

const lib = [NgSelectModule, QuillModule];

@NgModule({
  declarations: [ProductEditComponent],
  imports: [
    CommonModule,
    ProductEditRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HotToastModule.forRoot(),
    ...lib,
  ],
})
export class ProductEditModule {}
