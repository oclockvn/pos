import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ProductDetailComponent } from "./product-detail/product-detail.component";
import { ProductEditComponent } from "./product-edit/product-edit.component";
import { ProductListComponent } from "./product-list/product-list.component";

const routes: Routes = [
  {
    path: "",
    component: ProductListComponent,
  },
  {
    path: ":id",
    component: ProductDetailComponent,
  },
  {
    path: "create",
    component: ProductEditComponent,
  },
  {
    path: ":id/edit",
    component: ProductEditComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule {}
