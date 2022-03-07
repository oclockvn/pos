export interface SearchInfo {
  keyword?: string;
  sort?: { sortBy: string; dir: "asc" | "desc" };
  currentPage?: number;
}

export interface IdValue {
  id: number;
  value: string;
}

export interface PagingMetadata {
  currentPage: number;
  count: number;
  itemPerPage: number;
}

export interface Paging<T> {
  records: T[];
  metadata: PagingMetadata;
}