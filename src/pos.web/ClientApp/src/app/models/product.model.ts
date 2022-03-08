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
