import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Paging, ProductListItem } from "../models";
import { Product, ProductListSearch } from "../models";

@Injectable({
  providedIn: "root",
})
export class ProductService {
  constructor(private httpClient: HttpClient) {}

  // todo: move to pos service
  getPosProducts(): Observable<Product[]> {
    return this.httpClient.get<Product[]>("api/inventory/products");
  }

  getProducts(search: ProductListSearch): Observable<Paging<ProductListItem>> {
    console.log(search);
    return of({
      records: [...Array(200).keys()].map(
        i =>
          ({
            id: i,
            availableQty: i * 10,
            brand: `Brand ${i}`,
            category: `Category ${i}`,
            createdAt: new Date(),
            image: "",
            productName: `Product name ${i}`,
            totalQty: i * 10 + 2,
          } as ProductListItem),
      ),
      metadata: {
        currentPage: 0,
        itemPerPage: 25,
        count: 200,
      },
    } as Paging<ProductListItem>);
  }
}
