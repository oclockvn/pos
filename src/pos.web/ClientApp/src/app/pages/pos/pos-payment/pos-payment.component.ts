import { Component, Inject, OnInit } from "@angular/core";
import { RxState } from "@rx-angular/state";
import { Observable, Subject, switchMap } from "rxjs";
import { PosService } from "src/app/services/pos.service";
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

  onQuickAmountClicked$ = new Subject<number>();
  pay$ = new Subject<void>();

  constructor(
    @Inject(POS_STATE) private posState: RxState<PosState>,
    private posService: PosService,
  ) {
    this.connect();
  }

  ngOnInit(): void {}

  private connect(): void {
    this.posState.hold(this.onQuickAmountClicked$, value => {
      this.posState.set(prev => ({
        pay: (prev.pay || 0) + value,
      }));
    });

    this.posState.connect(
      this.pay$.pipe(switchMap(() => this.posService.pay())),
      (prev, curr) => {
        // assume pay success
        return {
          cart: [],
        };
      },
    );
  }
}
