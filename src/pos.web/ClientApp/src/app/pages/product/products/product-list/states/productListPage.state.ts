import { InjectionToken } from "@angular/core";
import { RxState } from "@rx-angular/state";
import { IdValue } from "src/app/models";

export interface ProductListPageState {
  categories: IdValue[];
}

export const PRODUCT_PAGE_STATE = new InjectionToken<
  RxState<ProductListPageState>
>("PRODUCT_PAGE_STATE");
