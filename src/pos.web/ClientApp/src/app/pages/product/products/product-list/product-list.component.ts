import {
  ChangeDetectionStrategy,
  Component,
  Inject,
  OnInit,
  TemplateRef,
  ViewChild,
} from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { RxState } from "@rx-angular/state";
import { DatatableComponent, TableColumn } from "@swimlane/ngx-datatable";
import { BehaviorSubject, Observable, Subject, switchMap } from "rxjs";
import {
  IdValue,
  PagingMetadata,
  ProductListItem,
  ProductListSearch,
} from "src/app/models";
import { ProductService } from "src/app/services/product.service";
import { PageInfo, SortInfo } from "src/app/types";
import {
  ProductListPageState,
  ProductListState,
  PRODUCT_PAGE_STATE,
} from "./states";

declare type FormType = {
  keyword: string;
  categories: number[];
};

@Component({
  selector: "app-product-list",
  templateUrl: "./product-list.component.html",
  styleUrls: ["./product-list.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProductListComponent implements OnInit {
  @ViewChild("colCreatedAt", { static: true }) colCreatedAt!: TemplateRef<any>;
  columns: TableColumn[] = [];
  searchForm = new FormGroup({
    keyword: new FormControl(),
    categories: new FormControl(),
  });

  search$ = new BehaviorSubject<ProductListSearch>({});
  submitSearch$ = new Subject<Partial<FormType>>();
  resetSearch$ = new Subject<void>();

  get categories$(): Observable<IdValue[]> {
    return this.productPageState.select("categories"); // todo: recheck getter with change detection
  }

  get products$(): Observable<ProductListItem[]> {
    return this.productListState.select("products");
  }

  get metadata$(): Observable<PagingMetadata> {
    return this.productListState.select("metadata");
  }

  @ViewChild(DatatableComponent) table!: DatatableComponent;

  constructor(
    @Inject(PRODUCT_PAGE_STATE)
    private productPageState: RxState<ProductListPageState>,
    private productListState: RxState<ProductListState>,
    private productService: ProductService,
  ) {}

  ngOnInit(): void {
    this.initTable();

    this.productListState.connect(
      this.search$.pipe(switchMap(s => this.productService.getProducts(s))),
      (_, result) => ({
        products: result.records,
        metadata: { ...result.metadata },
      }),
    );

    this.productListState.hold(this.submitSearch$, form => {
      this.search$.next({
        ...this.search$.getValue(),
        ...form,
        currentPage: 0,
      });
      this.table.offset = 0;
    });

    this.productListState.hold(this.resetSearch$, () => {
      this.searchForm.reset();
      this.submitSearch$.next({});
      this.table.offset = 0;
    });
  }

  initTable() {
    this.columns = [
      {
        prop: "image",
        width: 50,
        maxWidth: 80,
        sortable: false,
      },
      {
        prop: "productName",
        canAutoResize: true,
        sortable: false,
      },
      {
        prop: "category",
        maxWidth: 150,
        sortable: false,
      },
      {
        prop: "brand",
        maxWidth: 150,
        sortable: false,
      },
      {
        prop: "availableQty",
        name: "Avaialbe",
        maxWidth: 100,
        sortable: true,
      },
      {
        prop: "totalQty",
        maxWidth: 100,
        sortable: true,
      },
      {
        prop: "createdAt",
        cellTemplate: this.colCreatedAt,
        sortable: true,
        maxWidth: 120,
      },
    ];
  }

  onPage(paging: PageInfo) {
    this.search$.next({
      ...this.search$.getValue(),
      currentPage: paging.offset,
    });
  }

  onSort(event: SortInfo) {
    this.search$.next({
      ...this.search$.getValue(),
      sort: { sortBy: event.column.prop, dir: event.newValue },
    });
  }
}
