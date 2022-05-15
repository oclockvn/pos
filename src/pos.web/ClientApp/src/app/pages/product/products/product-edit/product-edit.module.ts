import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ProductEditRoutingModule } from "./product-edit-routing.module";

import { ProductEditComponent } from "./product-edit.component";

import { NgSelectModule } from "@ng-select/ng-select";
import { QuillModule } from "ngx-quill";
import { LetModule } from "@rx-angular/template";
import { NgxDropzoneModule } from "ngx-dropzone";
import { HotToastModule } from "@ngneat/hot-toast";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { ModalModule } from "ngx-bootstrap/modal";

const lib = [NgSelectModule, QuillModule, LetModule, NgxDropzoneModule];

@NgModule({
  declarations: [ProductEditComponent],
  imports: [
    CommonModule,
    ProductEditRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HotToastModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ...lib,
  ],
})
export class ProductEditModule {}
