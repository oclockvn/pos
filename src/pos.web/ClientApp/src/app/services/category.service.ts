import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable, of } from "rxjs";
import { Category, IdValue, PagingResponse, Result } from "../models";

@Injectable({
  providedIn: "root",
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  getCategories(): Observable<IdValue[]> {
    return this.http
      .get<PagingResponse<Category>>(`api/categories`)
      .pipe(
        map(paging =>
          paging.records.map(r => ({ id: r.id, value: r.name } as IdValue)),
        ),
      );
  }

  addCategory(
    category: Partial<Category>,
  ): Observable<Result<Partial<Category>>> {
    return this.http.post<Result<Category>>(`api/categories`, category);
  }
}
