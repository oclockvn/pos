import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Paging, ProductListItem } from "../models";
import { Product, ProductListSearch } from "../models";

@Injectable({
  providedIn: "root",
})
export class ProductService {
  constructor(private http: HttpClient) {}

  // todo: move to pos service
  getPosProducts(): Observable<Product[]> {
    return this.http.get<Product[]>("api/inventory/products");
  }

  getProducts(search: ProductListSearch): Observable<Paging<ProductListItem>> {
    return this.http.get<Paging<ProductListItem>>(`api/products/products`);
  }
}
