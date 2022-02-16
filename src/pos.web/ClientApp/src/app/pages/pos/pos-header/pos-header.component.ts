import { Component, Inject, OnInit } from "@angular/core";
import { RxState, selectSlice } from "@rx-angular/state";
import { Observable } from "rxjs";
import { PosState, POS_STATE } from "../states/pos.state";

@Component({
  selector: "app-pos-header",
  templateUrl: "./pos-header.component.html",
  styleUrls: ["./pos-header.component.scss"],
  providers: [RxState],
})
export class PosHeaderComponent implements OnInit {
  constructor(@Inject(POS_STATE) private posState: RxState<PosState>) {}

  get vm$(): Observable<Partial<PosState>> {
    return this.posState.select().pipe(selectSlice(["products"]));
  }

  ngOnInit(): void {}
}
