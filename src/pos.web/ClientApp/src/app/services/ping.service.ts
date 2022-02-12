import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class PingService {
  constructor(private _httpClient: HttpClient) {}

  ping(): Observable<string> {
    return this._httpClient.get<string>("api/ping/ping");
  }

  pingAnonymous(): Observable<string> {
    return this._httpClient.get<string>("api/ping/ping-anonymous");
  }
}
