import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";

declare type LoginResponse = {
  token: string;
};

@Injectable({
  providedIn: "root",
})
export class UserService {
  constructor(private _httpClient: HttpClient) {}

  login(username: string, password: string): Observable<string> {
    return this._httpClient
      .post<LoginResponse>("api/auth/login", {
        username,
        password,
      })
      .pipe(map(response => response.token));
  }
}
