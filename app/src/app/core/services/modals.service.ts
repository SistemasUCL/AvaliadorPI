import { Injectable } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ModalSizeEnum } from '../enums/modal-size.enum';
import { Observable } from 'rxjs/Observable';
import { ResponseModalModel } from '../models/response-modal.model';

@Injectable()
export class ModalsService {
  constructor(private modalService: BsModalService) {}

  public showModal(
    modalComponent: any,
    params?: any,
    modalSize: ModalSizeEnum = ModalSizeEnum.Default,
    disabled: boolean = false
  ): Observable<ResponseModalModel> {
    const modal = this.modalService.show(
      modalComponent,
      Object.assign({ class: this.getModalSize(modalSize) })
    );
    if (params) {
      modal.content.params = params;
    }
    modal.content.disabled = disabled;
    modal.content.showModal();

    return modal.content.onClose;
  }

  private getModalSize(modalSize: ModalSizeEnum): string {
    switch (modalSize) {
      case ModalSizeEnum.Small:
        return 'modal-sm';
      case ModalSizeEnum.Big:
        return 'modal-lg';
      default:
        return '';
    }
  }
}
