import { Component, Inject, OnInit } from "@angular/core";
import { RxState } from "@rx-angular/state";
import { PosState, POS_STATE } from "./states/pos.state";

@Component({
  selector: "app-pos",
  templateUrl: "./pos.component.html",
  styleUrls: ["./pos.component.scss"],
})
export class PosComponent implements OnInit {
  constructor(@Inject(POS_STATE) private posState: RxState<PosState>) {}

  ngOnInit(): void {
    this.posState.set({
      total: 2000_000,
    });
  }
}
