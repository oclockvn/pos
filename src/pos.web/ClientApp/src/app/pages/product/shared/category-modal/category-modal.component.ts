import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { BsModalService, BsModalRef, ModalOptions } from "ngx-bootstrap/modal";

@Component({
  selector: "app-category-modal",
  templateUrl: "./category-modal.component.html",
  styleUrls: ["./category-modal.component.scss"],
})
export class CategoryModalComponent implements OnInit {
  form!: FormGroup;
  simpleForm = false;

  constructor(private fb: FormBuilder, public bsModalRef: BsModalRef) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.form = this.fb.group({
      name: [null, [Validators.required]],
      code: [null],
      note: [null],
    });
  }

  submit() {}
}
