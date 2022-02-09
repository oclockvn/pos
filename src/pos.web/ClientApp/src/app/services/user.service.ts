import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class UserService {
  constructor(private _httpClient: HttpClient) {}

  login(username: string, password: string): Observable<string> {
    return this._httpClient.post<string>("auth/login", { username, password });
  }
}
