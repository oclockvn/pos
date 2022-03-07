import { Component, Inject, OnInit } from "@angular/core";
import { RxState } from "@rx-angular/state";
import { CategoryService } from "src/app/services/category.service";
import {
  ProductListPageState,
  PRODUCT_PAGE_STATE,
} from "./product-list/states";

@Component({
  selector: "app-products",
  templateUrl: "./products.component.html",
  styleUrls: ["./products.component.scss"],
  providers: [RxState],
})
export class ProductsComponent implements OnInit {
  constructor(
    private categoryService: CategoryService,
    @Inject(PRODUCT_PAGE_STATE)
    private productPageState: RxState<ProductListPageState>,
  ) {}

  ngOnInit(): void {
    this.productPageState.connect(
      this.categoryService.getCategories(),
      (_, curr) => ({
        categories: curr,
      }),
    );
  }
}
