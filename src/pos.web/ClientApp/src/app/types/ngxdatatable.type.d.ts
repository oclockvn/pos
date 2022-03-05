import { PipeTransform, TemplateRef } from "@angular/core";

export declare type NgxColumn = {
  name: string;
  prop: string;
  flexGrow: number;
  minWidth: number;
  maxWidth: number;
  width: number;
  resizeable: boolean;
  // comparator: any;
  sortable: boolean;
  draggable: boolean;
  canAutoResize: boolean;
  cellTemplate: TemplateRef<any>;
  headerTemplate: TemplateRef<any>;
  checkboxable: boolean;
  headerCheckboxable: boolean;
  headerClass: string | Function;
  cellClass: string | Function;
  frozenLeft: boolean;
  frozenRight: boolean;
  pipe: PipeTransform;
};

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
