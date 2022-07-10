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
  filter,
  map,
  Observable,
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
  ProductDetail,
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
  loading: boolean;
  categories: Category[];
  product: ProductDetail;
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
  submit$ = new Subject<{
    form: FormGroup;
    redirect: boolean;
    isUpdate: boolean;
  }>();
  categoryAdded$ = new Subject<{ id: number; name: string }>();
  id = 0;
  skuValidator = customProductSkuValidator();

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

    this.state.connect(
      this.activatedRoute.paramMap.pipe(
        filter(p => p.has("id") && Number(p.get("id")) > 0),
        map(p => Number(p.get("id"))),
        switchMap(id =>
          this.productService
            .getProduct(id)
            .pipe(tap(() => this.state.set({ loading: true }))),
        ),
        tap(product => this.patchForm(product)),
        tap(product => {
          this.id = product.id;
          // remove sku validation
          this.form.get("sku")?.removeValidators(this.skuValidator);
          this.form.get("sku")?.disable();
        }),
      ),
      (prev, curr) => ({
        product: curr,
        loading: false,
      }),
    );
  }

  get vm$(): Observable<ProductEditState> {
    return this.state.select();
  }

  get categories$(): Observable<IdValue[]> {
    return this.productPageState.select("categories");
  }

  get submitting$(): Observable<boolean> {
    return this.state.select("submitting");
  }

  ngOnInit(): void {
    this.buildForm();

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
        switchMap(({ form, redirect, isUpdate }) => {
          if (isUpdate) {
            return this.productService
              .updateProduct(this.id, form.value as ProductCreate)
              .pipe(map(r => ({ ...r, redirect, isUpdate })));
          }

          return this.productService
            .addProduct(form.value as ProductCreate)
            .pipe(map(r => ({ ...r, redirect, isUpdate })));
        }),
        tap(result => {
          if (!result.isOk) {
            return;
          }

          this.toast.success(
            `product ${result.isUpdate ? "updated" : "created"} successfully`,
          );

          if (result.redirect) {
            // redirect to product detail page
            this.router.navigate(["../", result?.data?.id], {
              relativeTo: this.activatedRoute,
            });
          } else {
            // reset the form
            this.form.reset();
            this.buildForm();
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

    this.productPageState.connect(
      this.categoryAdded$.pipe(
        tap(category => {
          this.form.get("category")?.setValue(category.id);
        }),
      ),
      (prev, curr) => ({
        categories: [...prev.categories, { id: curr.id, value: curr.name }],
      }),
    );
  }

  buildForm() {
    this.form = this.fb.group({
      productType: [ProductType.Normal],
      productName: ["", [Validators.required, Validators.minLength(3)]],
      sku: [null, [this.skuValidator]],
      weight: [],
      weightUnit: [],
      barcode: [],
      unit: [],
      description: [],
      importPrice: [],
      salePrice: [0, [Validators.required, Validators.min(1)]],
      wholesalePrice: [],
      inventoryInit: [],
      inventoryBranch: [],
      inventoryInitQty: [],
      inventoryImportPrice: [],
      categoryId: [],
      brand: [],
      tags: [],
      sellable: [true],
      taxable: [false],
    });
  }

  patchForm(product: Partial<ProductDetail>) {
    this.form.patchValue(product);
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
