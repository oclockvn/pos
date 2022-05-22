import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Category, IdValue, Result } from "../models";

@Injectable({
  providedIn: "root",
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  getCategories(): Observable<IdValue[]> {
    return of(
      [...Array(6).keys()].map(x => ({
        id: x,
        value: `Category ${x}`,
      })),
    );
  }

  addCategory(
    category: Partial<Category>,
  ): Observable<Result<Partial<Category>>> {
    return of({
      data: category,
      isOk: true,
    });
  }
}
