import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Paging, ProductDetail, ProductListItem } from "../models";
import { Product, ProductListSearch } from "../models";
import { stringify } from "query-string";

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
    const q = stringify({
      keyword: search.keyword,
      categories: search.categories || [],
      currentPage: search.currentPage,
      sortBy: search.sort?.sortBy,
      sortDir: search.sort?.dir,
    });

    return this.http.get<Paging<ProductListItem>>(`api/products/products?` + q);
  }

  addProduct(product: ProductDetail): Observable<boolean> {
    return this.http.post<boolean>(`api/products/`, product);
  }
}
