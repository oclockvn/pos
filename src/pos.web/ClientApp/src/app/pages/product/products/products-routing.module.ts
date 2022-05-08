import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ProductsComponent } from "./products.component";

const routes: Routes = [
  {
    path: "",
    component: ProductsComponent,
    children: [
      {
        path: "",
        loadChildren: () =>
          import("./product-list/product-list.module").then(
            m => m.ProductListModule,
          ),
      },
      {
        path: "create",
        loadChildren: () =>
          import("./product-edit/product-edit.module").then(
            m => m.ProductEditModule,
          ),
      },
      {
        path: ":id",
        loadChildren: () =>
          import("./product-edit/product-edit.module").then(
            m => m.ProductEditModule,
          ),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule {}
