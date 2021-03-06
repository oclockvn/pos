import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from "./guards/authorize.guard";
import { AuthorizedComponent } from "./layout/authorized/authorized.component";

const routes: Routes = [
  {
    path: "",
    redirectTo: "admin",
    pathMatch: "full",
  },
  {
    path: "admin",
    component: AuthorizedComponent,
    canActivate: [AuthorizeGuard],
    children: [
      {
        path: "",
        redirectTo: "dashboard",
        pathMatch: "full",
      },
      {
        path: "dashboard",
        loadChildren: () =>
          import("src/app/pages/dashboard/dashboard.module").then(
            m => m.DashboardModule,
          ),
      },
      {
        path: "customer",
        loadChildren: () =>
          import("src/app/pages/customer/customer.module").then(
            m => m.CustomerModule,
          ),
      },
      {
        path: "products",
        loadChildren: () =>
          import("src/app/pages/product/products/products.module").then(
            m => m.ProductsModule,
          ),
      },
    ],
  },
  {
    path: "pos",
    canActivate: [AuthorizeGuard],
    loadChildren: () =>
      import("src/app/pages/pos/pos.module").then(m => m.PosModule),
  },
  {
    path: "login",
    loadChildren: () =>
      import("src/app/layout/unauthorized/unauthorized.module").then(
        m => m.UnauthorizedModule,
      ),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
