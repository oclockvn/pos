import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function customProductSkuValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const invalidPrefix = control.value?.toLowerCase()?.startsWith("pos");
    return invalidPrefix ? { forbiddenPrefix: true } : null;
  };
}
