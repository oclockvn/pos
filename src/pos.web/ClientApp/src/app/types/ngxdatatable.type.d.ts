export declare type PageInfo = {
  count: number;
  pageSize: number;
  limit: number;
  offset: number;
};

export declare type SortInfo = {
  column: { prop: string };
  newValue: "asc" | "desc";
};
