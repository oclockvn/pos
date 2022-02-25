import { Component, Inject, OnInit } from "@angular/core";
import { RxState, selectSlice } from "@rx-angular/state";
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
  ) {
    this.connect();
  }

  ngOnInit(): void {
    this.posState.set({
      cart: [],
    });
  }

  private connect(): void {
    this.posState.connect(
      this._productService.getPosProducts(),
      (prev, curr) => ({
        ...prev,
        cart: [],
        products: curr,
        total: 0,
        return: 0,
        pay: 0,
        readonlyTotal: 0,
      }),
    );

    this.posState.connect(
      "total",
      this.posState.select("cart"),
      curr =>
        (curr.total = curr.cart.reduce((p, c) => p + c.qty * c.subTotal, 0)),
    );

    this.posState.connect(
      "return",
      this.posState.select().pipe(selectSlice(["pay", "total"])),
      prev => {
        return prev.pay - prev.total;
      },
    );
  }
}
