export interface SearchInfo {
  keyword?: string;
  sort?: { sortBy: string; dir: "asc" | "desc" };
  page?: number;
}
