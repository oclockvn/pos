import { Component, Inject, OnInit } from "@angular/core";
import { remove, RxState } from "@rx-angular/state";
import { Observable, Subject, switchMap } from "rxjs";
import { PosState, POS_STATE, ProductItem } from "../states/pos.state";

@Component({
  selector: "app-pos-cart",
  templateUrl: "./pos-cart.component.html",
  styleUrls: ["./pos-cart.component.scss"],
  providers: [RxState],
})
export class PosCartComponent implements OnInit {
  remove$ = new Subject<ProductItem>();

  constructor(@Inject(POS_STATE) private posState: RxState<PosState>) {
    this.connect();
  }

  get vm$(): Observable<PosState> {
    return this.posState.select();
  }

  ngOnInit(): void {}

  connect() {
    this.posState.connect(
      this.remove$
        .pipe
        // switchMap(item => )
        (),
      (prev, item) => {
        console.log(`removing item`, item);
        return {
          cart: prev.cart.filter(i => i.sku !== item.sku),
        };
      },
    );
  }

  // onRemove(p: ProductItem) {
  //   this.posState.set(curr => ({
  //     cart: curr.cart.filter(i => i.sku !== p.sku),
  //   }));
  // }
}
