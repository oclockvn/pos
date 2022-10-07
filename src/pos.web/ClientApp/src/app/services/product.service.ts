import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, of } from "rxjs";
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
    const { files, ...payload } = product;
    return this.http
      .post<Result<ProductCreateResult>>(
        `api/products/`,
        this.toFormData(payload, files),
      )
      .pipe(
        catchError((err: { statusCode: string }) =>
          of({
            isOk: false,
            statusCode: err.statusCode,
          } as Result<ProductCreateResult>),
        ),
      );
  }

  updateProduct(
    id: number,
    product: ProductCreate,
  ): Observable<Result<ProductCreateResult>> {
    const { files, ...payload } = product;

    return this.http
      .put<Result<ProductCreateResult>>(
        `api/products/${id}`,
        this.toFormData(payload, files),
      )
      .pipe(
        catchError((err: { statusCode: string }) =>
          of({
            isOk: false,
            statusCode: err.statusCode,
          } as Result<ProductCreateResult>),
        ),
      );
  }

  toFormData(product: Partial<ProductCreate>, files: File[]): FormData {
    const formData = new FormData();
    const payload = {
      ...product,
      importPrice: product.importPrice || 0,
      salePrice: product.salePrice || 0,
      wholesalePrice: product.wholesalePrice || 0,
    }

    // todo: check syntax of keyof
    Object.keys(payload).forEach(key => formData.append(key, (payload as any)[key]));

    if (files?.length > 0) {
      for (let f of files) {
        formData.append("Files", f, f.name);
      }
    }

    return formData;
  }

  getProduct(id: number): Observable<ProductDetail> {
    return this.http.get<ProductDetail>(`api/products/${id}`);
  }
}
