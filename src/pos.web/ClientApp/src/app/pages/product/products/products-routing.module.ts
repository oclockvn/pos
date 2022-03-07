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
      // {
      //   path: ":id",
      //   component: ProductDetailComponent,
      // },
      // {
      //   path: "create",
      //   component: ProductEditComponent,
      // },
      // {
      //   path: ":id/edit",
      //   component: ProductEditComponent,
      // },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule {}
