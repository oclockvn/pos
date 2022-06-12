import { FormArray, FormControl, FormGroup } from "@angular/forms";

declare module "@angular/forms" {
  interface FormGroup {
    revalidateControls(): void;
    hasError(key: string): boolean;
    hasErrorOf(key: string, validationType: string): boolean;
  }
}

FormGroup.prototype.revalidateControls = function (): void {
  const form = this as FormGroup;
  const keys = Object.keys(form.controls);

  if (!keys.length) {
    return;
  }

  for (let key of keys) {
    const control = form.get(key);

    if (control instanceof FormControl) {
      control.markAsTouched({ onlySelf: true });
    } else if (control instanceof FormArray) {
      const arr = control as FormArray;
      for (let fc of arr.controls) {
        (fc as FormGroup).revalidateControls();
      }
    } else if (control instanceof FormGroup) {
      control.revalidateControls();
    }
  }
};

FormGroup.prototype.hasError = function (key: string): boolean {
  const form = this as FormGroup;
  const control = form.get(key);

  if (!control) {
    throw new Error(`No control found under name ${key}`);
  }

  return control.invalid && (control.dirty || control.touched);
};

FormGroup.prototype.hasErrorOf = function (
  key: string,
  validationType: string,
): boolean {
  const form = this as FormGroup;
  const control = form.get(key);

  if (!control) {
    throw new Error(`No control found under name ${key}`);
  }

  return control.errors && control.invalid && (control.dirty || control.touched)
    ? control?.errors[validationType]
    : false;
};
