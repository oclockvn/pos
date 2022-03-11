import { Component, OnInit } from "@angular/core";
import { RxState } from "@rx-angular/state";
import { Observable, Subject } from "rxjs";

interface ProductEditState {
  showProductDescription: boolean;
}

@Component({
  selector: "app-product-edit",
  templateUrl: "./product-edit.component.html",
  styleUrls: ["./product-edit.component.scss"],
  providers: [RxState],
})
export class ProductEditComponent implements OnInit {
  toggleDescription$ = new Subject<void>();
  get vm$(): Observable<ProductEditState> {
    return this.state.select();
  }

  constructor(private state: RxState<ProductEditState>) {
    this.state.set({
      showProductDescription: false,
    });
  }

  ngOnInit(): void {
    this.state.connect(this.toggleDescription$, (prev, _) => ({
      showProductDescription: !prev.showProductDescription,
    }));
  }
}
