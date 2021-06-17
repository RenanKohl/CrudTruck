import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-form-field-error',
  template: `<div class="text-danger mt-2 mb-2">
    {{ errorMessage }}
  </div>`,
  styleUrls: ['./form-field-error.component.scss'],
})
export class FormFieldErrorComponent implements OnInit {
  @Input('form-control') formControl: FormControl;

  constructor() {}

  ngOnInit(): void {}

  public get errorMessage(): string | null {
    if (this.mustShowErrorMessage()) {
      return this.getErrorMessage();
    } else {
      return null;
    }
  }

  private mustShowErrorMessage(): boolean {
    return this.formControl.invalid && this.formControl.touched;
  }

  private getErrorMessage(): string | null {
    if (this.formControl.errors.required) {
      return 'Dado Obrigatório';
    } else if (this.formControl.errors.email) {
      return 'Formato de E-mail Inválido';
    } else if (this.formControl.errors.min) {
      const requiredValue = this.formControl.errors.min.min;
      return `O valor Mínimo é ${requiredValue} `;
    } else if (this.formControl.errors.max) {
      const requiredValue = this.formControl.errors.max.max;
      return `O valor Máximo é ${requiredValue} `;
    } else if (this.formControl.errors.minlength) {
      const requiredLength = this.formControl.errors.minlength.requiredLength;
      return `Deve Ter no Mínimo ${requiredLength} Caracteres`;
    } else if (this.formControl.errors.maxlength) {
      const requiredLength = this.formControl.errors.maxlength.requiredLength;
      return `Deve Ter no Máximo ${requiredLength} Caracteres`;
    } else if (this.formControl.errors.mustMatch) {
      return `As senhas não conferem`;
    }
  }
}
