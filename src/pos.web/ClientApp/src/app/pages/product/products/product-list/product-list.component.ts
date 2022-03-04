import { Component, OnInit } from '@angular/core';
import { ProductIndex } from "src/app/models";

@Component({
  selector: "app-product-list",
  templateUrl: "./product-list.component.html",
  styleUrls: ["./product-list.component.scss"],
})
export class ProductListComponent implements OnInit {
  records: ProductIndex[] = [];
  columns: any[] = [
    {
      prop: "image",
    },
    {
      prop: "productName",
    },
    {
      prop: "category",
    },
    {
      prop: "brand",
    },
    {
      prop: "availableQty",
    },
    {
      prop: "totalQty",
    },
    {
      prop: "createdAt",
    },
  ];

  constructor() {}

  ngOnInit(): void {
    this.records = [1, 2, 3, 4, 5, 6].map(
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
  }

  onPage(event: any) {}
}
