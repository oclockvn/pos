import { Component, Inject, OnInit } from "@angular/core";
import { RxState } from "@rx-angular/state";
import { Observable, Subject } from "rxjs";
import { PosState, POS_STATE } from "../states/pos.state";

declare type Amount = {
  label: string;
  value: number;
};

@Component({
  selector: "app-pos-payment",
  templateUrl: "./pos-payment.component.html",
  styleUrls: ["./pos-payment.component.scss"],
})
export class PosPaymentComponent implements OnInit {
  quickAmounts: Amount[] = [1, 2, 5, 10, 20, 50, 100, 200, 500].map(x => ({
    label: `${x}k`,
    value: x * 1000,
  }));

  get posState$(): Observable<PosState> {
    return this.posState.select();
  }

  onQuickAmountClicked = new Subject<number>();

  constructor(@Inject(POS_STATE) private posState: RxState<PosState>) {}

  ngOnInit(): void {
    this.manageEvents();
  }

  private manageEvents(): void {
    this.posState.hold(this.onQuickAmountClicked, value => {
      this.posState.set(prev => ({
        pay: (prev.pay || 0) + value,
      }));
    });
  }
}
