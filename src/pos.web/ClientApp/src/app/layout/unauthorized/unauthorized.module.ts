import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { UnauthorizedRoutingModule } from "./unauthorized-routing.module";
import { UnauthorizedComponent } from "./unauthorized.component";
import { LoginComponent } from "./login/login.component";
import { HttpClientModule } from "@angular/common/http";
import { ReactiveFormsModule } from "@angular/forms";

@NgModule({
  declarations: [UnauthorizedComponent, LoginComponent],
  imports: [
    CommonModule,
    UnauthorizedRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
})
export class UnauthorizedModule {}
