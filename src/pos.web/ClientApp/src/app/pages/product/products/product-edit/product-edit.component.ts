import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from "@angular/core";
import { RxState } from "@rx-angular/state";
import { Observable, Subject, tap } from "rxjs";

interface ProductEditState {
  showProductDescription: boolean;
  files: File[];
}

@Component({
  selector: "app-product-edit",
  templateUrl: "./product-edit.component.html",
  styleUrls: ["./product-edit.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [RxState],
})
export class ProductEditComponent implements OnInit {
  toggleDescription$ = new Subject<void>();
  selectedFiles$ = new Subject<File[]>();
  removedFile$ = new Subject<File>();

  get vm$(): Observable<ProductEditState> {
    return this.state.select();
  }

  constructor(
    private state: RxState<ProductEditState>,
    private cd: ChangeDetectorRef,
  ) {
    this.state.set({
      showProductDescription: false,
      files: [],
    });
  }

  ngOnInit(): void {
    this.state.connect(this.toggleDescription$, (prev, _) => ({
      showProductDescription: !prev.showProductDescription,
    }));

    this.state.connect(
      this.selectedFiles$.pipe(
        tap(() => setTimeout(() => this.cd.detectChanges(), 100)),
      ),
      (prev, curr) => ({
        files: [...prev.files, ...curr],
      }),
    );

    this.state.connect(
      this.removedFile$.pipe(
        tap(() => setTimeout(() => this.cd.markForCheck(), 100)),
      ),
      (prev, curr) => {
        prev.files.splice(prev.files.indexOf(curr), 1);
        return {
          files: prev.files,
        };
      },
    );
  }
}
