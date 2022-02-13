import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorizeGuard } from "./guards/authorize.guard";
import { AuthorizedComponent } from "./layout/authorized/authorized.component";

const routes: Routes = [
  {
    path: "",
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
        path: "product",
        loadChildren: () =>
          import("src/app/pages/product/product.module").then(
            m => m.ProductModule,
          ),
      },
    ],
  },
  {
    path: "pos",
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
