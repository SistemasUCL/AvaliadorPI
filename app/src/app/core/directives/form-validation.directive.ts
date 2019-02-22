import { OnInit, ElementRef, Output, EventEmitter } from '@angular/core';
import { Directive, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MessagesBaseService } from '../services/messages-base.service';

@Directive({
  selector: '[appFormValidation]'
})
export class FormValidationDirective implements OnInit {
  @Input() appFormValidation: FormGroup;
  @Output() action = new EventEmitter();
  @Output() formInvalid = new EventEmitter<boolean>();
  private classErroName = 'invalid-element';

  constructor(
    private el: ElementRef,
    private messagesService: MessagesBaseService
  ) {}

  ngOnInit(): void {
    this.el.nativeElement.addEventListener('submit', () => {
      this.validateForm();
    });
  }

  private validateForm(): void {
    // tslint:disable-next-line:forin
    for (const key in this.appFormValidation.controls) {
      const status: string = this.appFormValidation.controls[key].status;
      const element: HTMLElement = document.getElementsByName(key)[0];
      const data: NodeListOf<HTMLElement> = document.getElementsByName(key);

      for (let i = 0; i < data.length; i++) {
        let radioElement = data.item(i);

        if (
          radioElement.tagName.toLowerCase() !== 'input' &&
          radioElement.tagName.toLowerCase() !== 'textarea' &&
          radioElement.tagName.toLowerCase() !== 'select'
        ) {
          radioElement = radioElement.parentElement;
        }

        if (status === 'VALID') {
          radioElement.classList.remove(this.classErroName);
        } else if (status === 'INVALID') {
          radioElement.classList.add(this.classErroName);
        }
      }
    }

    if (this.appFormValidation.valid) {
      this.action.emit();
    } else {
      this.messagesService.showWarning('Preencha o formulário corretamente.');
    }

    this.formInvalid.emit(this.appFormValidation.invalid);
  }
}
