import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import {
  PagingRequest,
  PagingResponse,
  ProductCreate,
  ProductCreateResult,
  ProductDetail,
  ProductListItem,
  Result,
} from "../models";
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

  getProductPaging(
    request: PagingRequest<ProductListSearch>,
  ): Observable<PagingResponse<ProductListItem>> {
    const q = stringify({
      keyword: request.keyword,
      query: request.query,
      currentPage: request.currentPage,
      sortBy: request.sortBy,
      sortDir: request.sortDir,
    });

    return this.http.get<PagingResponse<ProductListItem>>(
      `api/products/paging?` + q,
    );
  }

  addProduct(product: ProductCreate): Observable<Result<ProductCreateResult>> {
    return this.http.post<Result<ProductCreateResult>>(
      `api/products/`,
      product,
    );
  }

  getProduct(id: number): Observable<ProductDetail> {
    return this.http.get<ProductDetail>(`api/products/${id}`);
  }
}
