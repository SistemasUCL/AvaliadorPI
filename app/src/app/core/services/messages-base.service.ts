import { Injectable } from '@angular/core';
import { ToastsManager, Toast } from 'ng2-toastr';

@Injectable()
export class MessagesBaseService {
  constructor(public toastr: ToastsManager) {}

  public showSuccess(message: string): Promise<Toast> {
    return this.toastr.success(message, 'Sucesso', this.toastr.success);
  }

  public showError(message: string): Promise<Toast> {
    return this.toastr.error(message, 'Erro', this.toastr.error);
  }

  public showErrors(messages: string[]): Array<Promise<Toast>> {
    return messages.map((message) => this.showError(message));
  }

  public showWarning(message: string): Promise<Toast> {
    return this.toastr.warning(message, 'Aviso', this.toastr.warning);
  }

  public showWarnings(messages: string[]): Array<Promise<Toast>> {
    return messages.map((message) => this.showWarning(message));
  }

  public showInfo(message: string): Promise<Toast> {
    return this.toastr.info(message, 'Informativo', this.toastr.info);
  }

  public showInfos(messages: string[]): Array<Promise<Toast>> {
    return messages.map((message) => this.showInfo(message));
  }
}
