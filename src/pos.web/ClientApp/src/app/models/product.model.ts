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

export interface ProductIndex {
  image: string;
  id: number;
  productName: string;
  category: string;
  brand: string;
  availableQty: number;
  totalQty: number;
  createdAt: Date;
}
