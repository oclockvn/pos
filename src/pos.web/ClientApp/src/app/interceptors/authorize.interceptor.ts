import { Inject, Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from "@angular/common/http";
import { catchError, mergeMap, Observable, throwError } from "rxjs";
import { Router } from "@angular/router";
import { AuthenticateService } from "../services/authenticate.service";
import stringHelper from "../helpers/string.helper";

@Injectable()
export class AuthorizeInterceptor implements HttpInterceptor {
  constructor(
    @Inject("BASE_URL") private _baseUrl: string,
    private _authService: AuthenticateService,
    private _router: Router,
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler,
  ): Observable<HttpEvent<unknown>> {
    return this._authService
      .getToken()
      .pipe(
        mergeMap(token => this.processRequestWithToken(token, request, next)),
      );
  }

  private processRequestWithToken(
    token: string | null,
    req: HttpRequest<any>,
    next: HttpHandler,
  ) {
    req = req.clone({
      url:
        stringHelper.trimEnding(this._baseUrl, "/") +
        "/" +
        stringHelper.trimLeading(req.url, "/"),
    });

    if (!!token && token.length > 0 && this.isSameOrigin(req)) {
      // check if same origin
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
    }

    return next.handle(req).pipe(catchError(this.handleError));
  }

  private handleError = (err: any) => {
    if (err.status === 401) {
      // return login page and clear old token
      this._router.navigate(["login"]);
      this._authService.clearToken();
    }

    // todo: show toast message in case of server error

    // throw error for component to handle
    return throwError(() => ({
      success: false,
      statusCode: "internal_server_error",
    }));
  };

  private isSameOrigin(req: any): boolean {
    // absolute url with the same origin
    if (req.url.startsWith(`${window.location.origin}/`)) {
      return true;
    }

    // a protocol relative url with the same origin
    if (req.url.startsWith(`//${window.location.host}/`)) {
      return true;
    }

    // relative url
    if (/^\/[^\/].*/.test(req.url)) {
      return true;
    }

    return false;
  }
}
