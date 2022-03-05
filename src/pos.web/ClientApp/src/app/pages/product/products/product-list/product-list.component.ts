import { DatePipe } from "@angular/common";
import { Component, OnInit, TemplateRef, ViewChild } from "@angular/core";
import { TableColumn } from "@swimlane/ngx-datatable";
import { Subject } from "rxjs";
import { ProductIndex, SearchInfo } from "src/app/models";
import { PageInfo, SortInfo } from "src/app/types";

@Component({
  selector: "app-product-list",
  templateUrl: "./product-list.component.html",
  styleUrls: ["./product-list.component.scss"],
})
export class ProductListComponent implements OnInit {
  records: ProductIndex[] = [];
  @ViewChild("colCreatedAt", { static: true }) colCreatedAt!: TemplateRef<any>;
  columns: TableColumn[] = [];

  search$ = new Subject<SearchInfo>();

  constructor() {
    this.connect();
  }

  ngOnInit(): void {
    this.records = [...Array(50).keys()].map(
      i =>
        ({
          id: i,
          availableQty: i * 10,
          brand: `Brand ${i}`,
          category: `Category ${i}`,
          createdAt: new Date(),
          image: "",
          productName: `Product name ${i}`,
          totalQty: i * 10 + 2,
        } as ProductIndex),
    );

    this.initTable();
  }

  connect() {}

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
        // flexGrow: 1,
        canAutoResize: true,
        // maxWidth: 300,
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
    this.search$.next({ page: paging.offset });
  }

  onSort(event: SortInfo) {
    this.search$.next({
      sort: { sortBy: event.column.prop, dir: event.newValue },
    });
  }
}
