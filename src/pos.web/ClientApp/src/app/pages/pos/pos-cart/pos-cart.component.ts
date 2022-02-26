import { Component, Inject, OnInit } from "@angular/core";
import { RxState } from "@rx-angular/state";
import { Observable, Subject } from "rxjs";
import { PosState, POS_STATE, ProductItem } from "../states/pos.state";

@Component({
  selector: "app-pos-cart",
  templateUrl: "./pos-cart.component.html",
  styleUrls: ["./pos-cart.component.scss"],
  providers: [RxState],
})
export class PosCartComponent implements OnInit {
  remove$ = new Subject<ProductItem>();
  changeQty$ = new Subject<{ p: ProductItem; qty: number }>();
  setQty$ = new Subject<{ p: ProductItem; e: any }>();

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
      (prev, item) => ({
        cart: prev.cart.filter(i => i.sku !== item.sku),
      }),
    );

    this.posState.connect(this.changeQty$.pipe(), (prev, curr) => ({
      cart: prev.cart.map(i =>
        i.sku === curr.p.sku ? { ...i, qty: i.qty + curr.qty } : i,
      ),
    }));

    this.posState.connect(this.setQty$.pipe(), (prev, evt) => {
      const qty = Number(evt.e.target.value);
      return {
        cart: prev.cart.map(i =>
          i.sku === evt.p.sku ? { ...i, qty: qty } : i,
        ),
      };
    });
  }
}
