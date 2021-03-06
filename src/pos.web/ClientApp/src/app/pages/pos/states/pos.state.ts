import { InjectionToken } from "@angular/core";
import { RxState } from "@rx-angular/state";
import { Customer, OrderItem, Product } from "src/app/models";

export enum DeliverySource {
  WebOrder = 1,
  Instagram = 2,
  Sendo = 3,
  Other = 4,
  Shopee = 5,
  Tiki = 6,
  Lazada = 7,
  Zalo = 8,
  Facebook = 9,
  Web = 10,
}

export interface PosState {
  subTotal: number;
  vat: number;
  discount: number;
  total: number;
  pay: number;
  return: number;
  notes: string;
  delivery: boolean;
  deliverySource: DeliverySource;
  customer: Customer;
  products: Product[];
  cart: OrderItem[];
}

export const POS_STATE = new InjectionToken<RxState<PosState>>("POS_STATE");
