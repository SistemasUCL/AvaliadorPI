import { Injectable } from '@angular/core';
import { ToastsManager, Toast } from 'ng2-toastr';
import { MessagesBaseService } from './messages-base.service';
import { ModalsService } from './modals.service';
import { ModalConfirmComponent } from '../components/modal-confirm/modal-confirm.component';
import { ModalConfirmModel } from '../models/modal-confirm.model';
import { Observable } from 'rxjs/Observable';
import { RequestErrorModel } from '../models/request-error.model';
import { ResponseModalModel } from '../models/response-modal.model';

@Injectable()
export class MessagesService extends MessagesBaseService {
  constructor(
    public toastr: ToastsManager,
    private modalsService: ModalsService
  ) {
    super(toastr);
  }

  public save(message: string = 'Registro salvo.'): Promise<Toast> {
    return this.showSuccess(message);
  }

  public delete(message: string = 'Registro(s) excluído(s).'): Promise<Toast> {
    return this.showSuccess(message);
  }

  public errors(errors?: RequestErrorModel[]): Array<Promise<Toast>> {
    const messages = [];

    if (!errors || !errors.length) {
      messages.push('Por favor, contate o administrador do sistema.');
    } else {
      errors.map((error) => messages.push(error.errorMessage));
    }

    return super.showErrors(messages);
  }

  public deleteConfirm(
    message: string = 'Confirme a exclusão do(s) registro(s).'
  ): Observable<ResponseModalModel> {
    const model = {
      title: 'Confirmação de exclusão',
      text: message
    } as ModalConfirmModel;

    return this.modalsService.showModal(ModalConfirmComponent, model);
  }
}
