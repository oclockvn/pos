import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class PosService {
  constructor(private httpClient: HttpClient) {}

  pay(): Observable<any> {
    return of({
      success: true,
    });
  }
}
