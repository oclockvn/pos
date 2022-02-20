import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Product } from "../models/product.model";

@Injectable({
  providedIn: "root",
})
export class ProductService {
  constructor() {}

  getPosProducts(): Observable<Product[]> {
    return of(
      [1, 2, 3, 4, 5, 6, 7, 8].map(
        i =>
          ({
            id: i,
            importPrice: 0,
            productName: `Lorem ipsum dolor sit amet consectetur adipisicing elit ${i}`,
            salesPrice: (i + 2) * 10000,
            sku: `P0000${i}`,
            wholesalePrice: 0,
            inventory: i * 10,
          } as Product),
      ),
    );
  }
}
