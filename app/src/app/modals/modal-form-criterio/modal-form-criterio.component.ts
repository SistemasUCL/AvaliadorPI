import { Component, OnInit, SimpleChanges } from '@angular/core';

// tslint:disable-next-line:import-blacklist
import { Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';
import { ModalBaseComponent } from '../../core/components/_base/modal-base.component';
import { MessagesService } from '../../core/services/messages.service';
import { CriteriosService } from '../../services/criterios.service';
import { RequestErrorModel } from '../../core/models/request-error.model';
import { CriterioModel } from '../../shared/models/criterio.model';
import { ProjetosService } from '../../services/projetos.service';

@Component({
  selector: 'app-modal-form-criterio',
  templateUrl: './modal-form-criterio.component.html'
})
export class ModalFormCriterioComponent extends ModalBaseComponent
  implements OnInit {
  public model: CriterioModel;

  constructor(
    public _bsModalRef: BsModalRef,
    private criteriosService: CriteriosService,
    private projetosService: ProjetosService,
    private messagesService: MessagesService
  ) {
    super(_bsModalRef);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  public showModal() {
    if (this.params.criterio) {
      this.model = this.params.criterio;
    }

    super.showModal();
  }

  public onConfirm(): void {
    this.projetosService
      .addCriterio(this.params.projetoId, this.model)
      .then((dados) => {
        this.messagesService.save();
        this.return(true, dados);
      })
      .catch((data) => {
        this.messagesService.errors(data.error.errors as RequestErrorModel[]);
      });
  }
}
