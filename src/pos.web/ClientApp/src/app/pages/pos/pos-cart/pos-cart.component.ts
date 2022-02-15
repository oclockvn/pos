import { Component, OnInit } from "@angular/core";
import { Product } from "../models";

@Component({
  selector: "app-pos-cart",
  templateUrl: "./pos-cart.component.html",
  styleUrls: ["./pos-cart.component.scss"],
})
export class PosCartComponent implements OnInit {
  products: Product[] = [
    {
      order: 1,
      productName: "Mixing machine",
      sku: "PVN02",
      unit: "item",
      qty: 1,
      subTotal: 1400000,
      total: 1400000,
    },
    {
      order: 2,
      productName: "Egg mixing machine",
      sku: "PVN01",
      unit: "item",
      qty: 1,
      subTotal: 150000,
      total: 150000,
    },
    {
      order: 3,
      productName: "Egg mixing machine 2",
      sku: "PVN01",
      unit: "item",
      qty: 1,
      subTotal: 150000,
      total: 150000,
    },
    {
      order: 4,
      productName: "Egg mixing machine 3",
      sku: "PVN01",
      unit: "item",
      qty: 1,
      subTotal: 150000,
      total: 150000,
    },
  ];

  constructor() {}

  ngOnInit(): void {}
}
