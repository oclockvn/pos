import { Component, Inject, OnInit } from "@angular/core";
import { FormControl } from "@angular/forms";
import { RxState, selectSlice } from "@rx-angular/state";
import { filter, Observable, Subject } from "rxjs";
import { Product } from "src/app/models/product.model";
import { PosState, POS_STATE } from "../states/pos.state";

@Component({
  selector: "app-pos-header",
  templateUrl: "./pos-header.component.html",
  styleUrls: ["./pos-header.component.scss"],
  providers: [RxState],
})
export class PosHeaderComponent implements OnInit {
  selectedItem$ = new Subject<Product>();
  productControl = new FormControl();

  constructor(@Inject(POS_STATE) private posState: RxState<PosState>) {
    this.connect();
  }

  get vm$(): Observable<Partial<PosState>> {
    return this.posState.select().pipe(selectSlice(["products"]));
  }

  get products$(): Observable<Product[]> {
    return this.posState.select("products");
  }

  ngOnInit(): void {}

  private connect(): void {
    this.posState.connect(
      this.selectedItem$.pipe(filter(x => !!x)),
      (prev, product) => {
        let currCart = prev.cart || [];
        const orderItem = currCart.find(c => c.id === product.id);

        if (orderItem == null) {
          currCart = [
            ...currCart,
            {
              productName: product.productName,
              qty: 1,
              sku: product.sku,
              subTotal: product.salesPrice, // todo: replace by unitPrice
              total: product.salesPrice,
              barcode: product.barcode,
              id: product.id,
              // inventory: product.inventory,
              unitPrice: product.salesPrice,
            },
          ];
        } else {
          currCart = currCart.map(i =>
            i.sku === product.sku
              ? {
                  ...i,
                  qty: i.qty + 1,
                }
              : i,
          );
        }

        return {
          ...prev,
          cart: currCart,
        };
      },
    );
  }

  onSelectProduct(product: Product) {
    this.selectedItem$.next(product);
    this.productControl.reset();
  }
}
