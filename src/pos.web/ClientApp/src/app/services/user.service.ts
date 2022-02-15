import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

declare type LoginResponse = {
  token: string;
  statusCode: string;
};

@Injectable({
  providedIn: "root",
})
export class UserService {
  constructor(private _httpClient: HttpClient) {}

  login(username: string, password: string): Observable<LoginResponse> {
    return this._httpClient.post<LoginResponse>("api/auth/login", {
      username,
      password,
    });
  }
}
