export interface OrderItem {
  id: number;
  sku: string;
  productName: string;
  barcode: string;
  inventory: number; // available items
  unitPrice: number;
  qty: number;
  subTotal: number;
  total: number;
  unit?: string;
}

export interface OrderResult {}
