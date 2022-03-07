import { PagingMetadata, ProductListItem } from "src/app/models";

export interface ProductListState {
  products: ProductListItem[];
  metadata: PagingMetadata;
}
