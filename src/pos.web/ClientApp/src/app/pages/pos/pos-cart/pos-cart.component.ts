import { Component, Inject, OnInit } from "@angular/core";
import { RxState } from "@rx-angular/state";
import { Observable } from "rxjs";
import { PosState, POS_STATE, ProductItem } from "../states/pos.state";

@Component({
  selector: "app-pos-cart",
  templateUrl: "./pos-cart.component.html",
  styleUrls: ["./pos-cart.component.scss"],
  providers: [RxState],
})
export class PosCartComponent implements OnInit {
  constructor(@Inject(POS_STATE) private posState: RxState<PosState>) {}

  get vm$(): Observable<PosState> {
    return this.posState.select();
  }

  ngOnInit(): void {}
}
