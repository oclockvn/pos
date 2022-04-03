export interface PagingRequest<T> {
  keyword?: string;
  sortBy?: string;
  sortDir?: "asc" | "desc";
  currentPage?: number;
  query?: T;
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

export interface PagingResponse<T> {
  records: T[];
  metadata: PagingMetadata;
}

export interface Result<T> {
  data?: T;
  statusCode?: string;
  isOk: boolean;
}
