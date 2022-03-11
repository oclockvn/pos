import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { AuthorizedComponent } from "./layout/authorized/authorized.component";
import { AuthorizeInterceptor } from "./interceptors";


import { QuillModule } from "ngx-quill";

const lib = [QuillModule.forRoot()];

@NgModule({
  declarations: [AppComponent, AuthorizedComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, ...lib],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizeInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
