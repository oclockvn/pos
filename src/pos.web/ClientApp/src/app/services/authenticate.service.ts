import { Injectable } from "@angular/core";
import {
  BehaviorSubject,
  concat,
  filter,
  map,
  Observable,
  of,
  take,
  tap,
} from "rxjs";
import { CurrentUser } from "../models/user.model";
import { StorageService } from "./storage.service";
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: "root",
})
export class AuthenticateService {
  constructor(private _storage: StorageService) {}

  persistToken(token: string) {
    this._storage.set("token", token);
  }

  logout(): void {
    this._storage.set("token", null); // redirect to login page
  }

  private _user$ = new BehaviorSubject<CurrentUser | null>(null);
  isAuthenticated(): Observable<boolean> {
    return this.getUser().pipe(map(u => !!u));
  }

  getUser(): Observable<CurrentUser | null> {
    return concat(
      this._user$.pipe(
        take(1),
        filter(u => !!u),
      ),
      this.getCurrentUser().pipe(
        filter(u => !!u),
        tap(u => this._user$.next(u)),
      ),
      this._user$.asObservable(),
    );
  }

  getCurrentUser(): Observable<CurrentUser | null> {
    const token = this._storage.get("token");
    if (!token) {
      return of(null);
    }

    let claims: any;

    try {
      claims = jwt_decode(token);
    } catch {
      return of(null);
    }

    // check expiry
    if (!claims || Date.now().valueOf() > claims.exp * 1000) {
      return of(null);
    }

    const user: CurrentUser = {
      username: claims[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
      ] as string,
      fullName: claims[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
      ] as string,
      role: claims[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ] as string,
    };

    return of(user);
  }
}
