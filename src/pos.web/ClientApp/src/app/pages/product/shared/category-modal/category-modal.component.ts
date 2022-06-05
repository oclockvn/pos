import { ChangeDetectionStrategy, Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { RxState } from "@rx-angular/state";

import { BsModalService, BsModalRef, ModalOptions } from "ngx-bootstrap/modal";
import {
  catchError,
  filter,
  Observable,
  of,
  partition,
  Subject,
  switchMap,
  tap,
} from "rxjs";
import { CategoryService } from "src/app/services";

declare type ModalState = {
  hasError: boolean;
};

@Component({
  selector: "app-category-modal",
  templateUrl: "./category-modal.component.html",
  styleUrls: ["./category-modal.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [RxState],
})
export class CategoryModalComponent implements OnInit {
  form!: FormGroup;
  simpleForm = false;
  submit$ = new Subject<FormGroup>();

  public get hasError$(): Observable<boolean> {
    return this.state.select("hasError");
  }

  constructor(
    private fb: FormBuilder,
    public bsModalRef: BsModalRef,
    private state: RxState<ModalState>,
    private categoryService: CategoryService,
  ) {}

  ngOnInit(): void {
    this.initForm();

    const [$valid, $invalid] = partition(this.submit$, f => f.valid);

    this.state.connect(
      $invalid.pipe(tap(() => this.form.revalidateControls())),
      () => ({
        hasError: true,
      }),
    );

    this.state.connect(
      $valid
        .pipe(
          switchMap(form =>
            this.categoryService
              .addCategory(form.value)
              .pipe(catchError(() => of({ isOk: false, data: null }))),
          ),
        )
        .pipe(
          filter(result => result.isOk),
          tap(result => {
            this.bsModalRef.onHide?.emit({
              id: result?.data?.id,
              name: result?.data?.name,
              success: true,
            });
            this.bsModalRef.hide();
          }),
        ),
      (prev, curr) => ({
        hasError: !curr.isOk,
      }),
    );
  }

  initForm() {
    this.form = this.fb.group({
      name: [null, [Validators.required]],
      code: [null],
      note: [null],
    });
  }
}
