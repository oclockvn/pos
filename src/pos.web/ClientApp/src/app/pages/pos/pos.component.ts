import { Component, Inject, OnInit } from "@angular/core";
import { RxState } from "@rx-angular/state";
import { ProductService } from "src/app/services/product.service";
import { PosState, POS_STATE } from "./states/pos.state";

@Component({
  selector: "app-pos",
  templateUrl: "./pos.component.html",
  styleUrls: ["./pos.component.scss"],
})
export class PosComponent implements OnInit {
  constructor(
    @Inject(POS_STATE) private posState: RxState<PosState>,
    private _productService: ProductService,
  ) {}

  ngOnInit(): void {
    this.connect();
  }

  private connect(): void {
    this.posState.connect(
      this._productService.getPosProducts(),
      (prev, curr) => ({
        ...prev,
        products: curr,
        total: 0,
        return: 0,
        pay: 0,
      }),
    );
  }
}
