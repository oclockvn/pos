import { SearchInfo } from ".";

export interface Product {
  id: number;
  productName: string;
  sku: string;
  barcode: string;
  salesPrice: number;
  wholesalePrice: number;
  importPrice: number;
  availableQty: number;
}

export interface ProductListItem {
  id: number;
  image: string;
  productName: string;
  category: string;
  brand: string;
  availableQty: number;
  totalQty: number;
  createdAt: Date;
}

export interface ProductListSearch extends SearchInfo {
  categories?: number[];
}

export enum ProductType {
  normal = 0,
  expiry = 1,
  serial = 2,
}

export interface ProductDetail {
  id: number;
  productType: ProductType;
  productName: string;
  sku: string;
  weight: string;
  weightUnit: "" | "g" | "kg";
  barcode: string;
  unit: string;
  description: string;
  importPrice: number;
  salePrice: number;
  wholesalePrice: number;
  inventoryInit: boolean;
  inventoryBranch: string;
  inventoryInitQty: number;
  inventoryImportPrice: number;
  category?: number;
  brand?: number;
  tags?: string;
  ableToSell: boolean;
  applyTax: boolean;
}
