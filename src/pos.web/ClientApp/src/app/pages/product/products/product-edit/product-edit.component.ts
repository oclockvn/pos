import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { HotToastService } from "@ngneat/hot-toast";
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
  form!: FormGroup;

  get vm$(): Observable<ProductEditState> {
    return this.state.select();
  }

  constructor(
    private state: RxState<ProductEditState>,
    private cd: ChangeDetectorRef,
    private fb: FormBuilder,
    private toast: HotToastService,
  ) {
    this.state.set({
      showProductDescription: false,
      files: [],
    });
  }

  ngOnInit(): void {
    this.initForm();

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

  initForm() {
    this.form = this.fb.group({
      productType: [],
      productName: ["", [Validators.required, Validators.minLength(10)]],
      sku: [],
      weight: [],
      weightUnit: [],
      barcode: [],
      unit: [],
      description: [],
      importPrice: [],
      salePrice: [0, [Validators.required, Validators.min(10)]],
      wholesalePrice: [],
      inventoryInit: [],
      inventoryBranch: [],
      inventoryInitQty: [],
      inventoryImportPrice: [],
      category: [],
      brand: [],
      tags: [],
      active: [],
      taxable: [],
    });
  }

  submitForm() {
    const valid = this.form.valid;

    if (valid) {
      // this.formSubmit$.next(this.form.value);
      this.toast.error(`Ok`);
    } else {
      this.toast.error(`Invalid form. Please recheck and try again!`);
      this.form.revalidateControls([]);
    }
  }
}
