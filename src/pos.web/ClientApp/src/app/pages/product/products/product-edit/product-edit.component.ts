import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { HotToastService } from "@ngneat/hot-toast";
import { RxState } from "@rx-angular/state";
import { switchMap } from "rxjs";
import { Subject } from "rxjs";
import { ProductDetail, ProductListSearch } from "src/app/models";
import { ProductService } from "src/app/services";

export interface ProductDetailState {}

@Component({
  selector: "app-product-edit",
  templateUrl: "./product-edit.component.html",
  styleUrls: ["./product-edit.component.scss"],
  providers: [RxState],
})
export class ProductEditComponent implements OnInit {
  form!: FormGroup;
  productSaved$ = new Subject<ProductDetail>();

  constructor(
    private fb: FormBuilder,
    private toast: HotToastService,
    private state: RxState<ProductDetailState>,
    private productService: ProductService,
  ) {}

  ngOnInit(): void {
    this.initForm();

    this.state.connect(
      this.productSaved$.pipe(
        switchMap(p => this.productService.addProduct(p)),
      ),
      (prev, curr) => ({}),
    );
  }

  initForm() {
    this.form = this.fb.group({
      productType: [],
      productName: ["", [Validators.required]],
      sku: [],
      weight: [],
      weightUnit: [],
      barcode: [],
      unit: [],
      description: [],
      importPrice: [],
      salePrice: [],
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
    console.log(
      `form valid state = ${valid}`,
      this.form.value,
      this.form.errors,
    );
    // console.log(this.form.value);
    if (valid) {
      this.formSubmit$.next(this.form.value);
    } else {
      this.toast.error(`Invalid form`);
    }
  }
}
