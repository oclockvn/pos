import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ProductEditComponent } from "./product-edit.component";

const routes: Routes = [
  {
    path: "",
    component: ProductEditComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductEditRoutingModule {}
