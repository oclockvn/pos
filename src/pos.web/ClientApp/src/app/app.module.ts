import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { AuthorizedComponent } from "./layout/authorized/authorized.component";
import { AuthorizeInterceptor } from "./interceptors";

import { QuillModule } from "ngx-quill";

const lib = [QuillModule.forRoot()];

@NgModule({
  declarations: [AppComponent, AuthorizedComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ...lib,
  ],
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
