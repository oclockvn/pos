import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { HotToastService } from "@ngneat/hot-toast";
import { RxState } from "@rx-angular/state";
import {
  catchError,
  Observable,
  of,
  partition,
  Subject,
  switchMap,
  tap,
} from "rxjs";
import { ProductType } from "src/app/common/enums";
import { customProductSkuValidator } from "src/app/common/validations";
import { ProductCreate, ProductCreateResult, Result } from "src/app/models";
import { ProductService } from "src/app/services";

interface ProductEditState {
  showProductDescription: boolean;
  files: File[];
  error?: string;
  submitting: boolean;
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
  formSubmit$ = new Subject<FormGroup>();
  submit$ = new Subject<FormGroup>();

  get vm$(): Observable<ProductEditState> {
    return this.state.select();
  }

  constructor(
    private state: RxState<ProductEditState>,
    private cd: ChangeDetectorRef,
    private fb: FormBuilder,
    private toast: HotToastService,
    private productService: ProductService,
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

    const [valid$, invalid$] = partition(this.submit$, f => f.valid);

    this.state.connect(
      valid$.pipe(
        tap(() => this.state.set({ submitting: true })),
        switchMap(f =>
          this.productService
            .addProduct(f.value as ProductCreate)
            .pipe(catchError((err: Result<ProductCreateResult>) => of(err))),
        ),
        tap(result => {
          if (result.isOk) {
            // todo: redirect to product detail page
            this.toast.success(`product created successfully`);
          }
        }),
      ),
      (_prev, curr) => ({
        error: curr.statusCode,
        submitting: false,
      }),
    );

    this.state.hold(invalid$.pipe(), form => {
      this.toast.error(`Invalid form. Please recheck and try again!`);
      form.revalidateControls();
    });
  }

  initForm() {
    this.form = this.fb.group({
      productType: [ProductType.Normal],
      productName: ["", [Validators.required, Validators.minLength(10)]],
      sku: [null, [customProductSkuValidator()]],
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
      sellable: [true],
      taxable: [false],
    });
  }
}
