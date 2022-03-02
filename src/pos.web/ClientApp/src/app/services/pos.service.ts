import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { OrderItem, OrderResult } from "../models/cart.model";

@Injectable({
  providedIn: "root",
})
export class PosService {
  constructor(private httpClient: HttpClient) {}

  pay(order: OrderItem[]): Observable<OrderResult> {
    return this.httpClient.post<OrderResult>("api/orders/pay", {
      items: order,
    });
  }
}
