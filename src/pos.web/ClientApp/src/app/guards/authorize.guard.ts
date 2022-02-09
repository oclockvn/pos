import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from "@angular/router";
import { Observable, tap } from "rxjs";
import { AuthenticateService } from "../services/authenticate.service";

@Injectable({
  providedIn: "root",
})
export class AuthorizeGuard implements CanActivate {
  constructor(
    private authService: AuthenticateService,
    private router: Router,
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.authService
      .isAuthenticated()
      .pipe(tap(authenticated => this.handleAuth(authenticated, state)));
  }

  private handleAuth(isAuthenticated: boolean, state: RouterStateSnapshot) {
    if (!isAuthenticated) {
      // redirect to login
      this.router.navigate(["login"], {
        queryParams: { returnUrl: state.url },
      });
    }
  }
}
