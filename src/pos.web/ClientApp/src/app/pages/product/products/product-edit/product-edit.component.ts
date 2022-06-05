import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  Inject,
  OnInit,
} from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { HotToastService } from "@ngneat/hot-toast";
import { RxState } from "@rx-angular/state";
import { BsModalService } from "ngx-bootstrap/modal";
import {
  catchError,
  filter,
  map,
  Observable,
  of,
  partition,
  Subject,
  switchMap,
  take,
  tap,
} from "rxjs";
import { ProductType } from "src/app/common/enums";
import { customProductSkuValidator } from "src/app/common/validations";
import {
  Category,
  IdValue,
  ProductCreate,
  ProductCreateResult,
  Result,
} from "src/app/models";
import { ProductService } from "src/app/services";
import { CategoryModalComponent } from "../../shared";
import {
  ProductListPageState,
  PRODUCT_PAGE_STATE,
} from "../product-list/states";

interface ProductEditState {
  showProductDescription: boolean;
  files: File[];
  error?: string;
  submitting: boolean;
  categories: Category[];
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
  submit$ = new Subject<{ form: FormGroup; redirect: boolean }>();
  categoryAdded$ = new Subject<{ id: number; name: string }>();

  get vm$(): Observable<ProductEditState> {
    return this.state.select();
  }

  get categories$(): Observable<IdValue[]> {
    return this.productPageState.select("categories");
  }

  constructor(
    @Inject(PRODUCT_PAGE_STATE)
    private productPageState: RxState<ProductListPageState>,
    private state: RxState<ProductEditState>,
    private cd: ChangeDetectorRef,
    private fb: FormBuilder,
    private toast: HotToastService,
    private productService: ProductService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private modalService: BsModalService,
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

    const [valid$, invalid$] = partition(
      this.submit$,
      ({ form }) => form.valid,
    );

    this.state.connect(
      valid$.pipe(
        tap(() => this.state.set({ submitting: true })),
        switchMap(({ form, redirect }) =>
          this.productService.addProduct(form.value as ProductCreate).pipe(
            catchError((err: Result<ProductCreateResult>) => of(err)),
            map(r => ({ ...r, redirect })),
          ),
        ),
        tap(result => {
          if (!result.isOk) {
            return;
          }

          this.toast.success(`product created successfully`);

          if (result.redirect) {
            // redirect to product detail page
            this.router.navigate(["../", result?.data?.id], {
              relativeTo: this.activatedRoute,
            });
          } else {
            // reset the form
            this.form.reset();
            this.initForm();
          }
        }),
      ),
      (_prev, curr) => ({
        error: curr.statusCode,
        submitting: false,
      }),
    );

    this.state.hold(invalid$.pipe(), ({ form }) => {
      this.toast.error(`Invalid form. Please recheck and try again!`);
      form.revalidateControls();
    });

    this.productPageState.connect(this.categoryAdded$, (prev, curr) => ({
      categories: [...prev.categories, { id: curr.id, value: curr.name }],
    }));
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

  showAddCategory() {
    const bsModalRef = this.modalService.show(CategoryModalComponent, {
      initialState: {
        simpleForm: true,
      },
    });

    bsModalRef.onHide
      ?.pipe(
        take(1),
        filter(s => (s as any).success),
      )
      .subscribe({
        next: result => {
          const categoryAdded = result as { id: number; name: string };
          this.categoryAdded$.next({
            id: categoryAdded.id,
            name: categoryAdded.name,
          });
        },
      });
  }
}
