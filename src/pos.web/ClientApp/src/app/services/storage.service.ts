import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class StorageService {
  constructor() {}

  set(key: string, value: string | null) {
    if (!value) {
      localStorage.removeItem(key);
    } else {
      localStorage.setItem(key, value);
    }
  }

  get(key: string): string | null {
    return localStorage.getItem(key);
  }
}
