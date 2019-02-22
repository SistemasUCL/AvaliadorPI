import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ToastsManager } from 'ng2-toastr';

import { ToolbarButtonModel } from '../../core/models/toolbar-button.model';
import { UsuariosService } from '../../services/usuarios.service';
import { MessagesService } from '../../core/services/messages.service';
import { RequestErrorModel } from '../../core/models/request-error.model';
import { UsuarioModel } from '../../shared/models/usuario.model';
import { BaseModel } from '../../core/models/base.model';
import { RequestService } from '../../core/services/request.service';

@Component({
  template: ''
})
export class FormPageComponent<T extends BaseModel> implements OnInit {
  public model = {} as T;
  public toolbarButtons = [] as ToolbarButtonModel[];

  public routeBase: string;
  public editMod: boolean;
  public hideSave: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected modelsService: RequestService<T>,
    protected router: Router,
    protected messagesService: MessagesService
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.initModel(params['id']).then(() => {
        this.initToolbar();
      });
    });
  }

  protected async initModel(id: number) {
    if (id) {
      this.model = await this.modelsService.getById(id);
      this.editMod = true;
    }
  }

  protected initToolbar() {
    this.toolbarButtons = [
      {
        display: 'Salvar',
        icon: 'fa fa-save',
        submit: true,
        action: this.save,
        hidden: this.hideSave
      } as ToolbarButtonModel,
      {
        display: 'Excluir',
        icon: 'fa fa-trash',
        action: () => this.excluir(),
        disabled: !this.model.id
      } as ToolbarButtonModel
    ] as ToolbarButtonModel[];
  }

  protected addButtonsToolbar(buttons: ToolbarButtonModel[]) {
    this.toolbarButtons.push(...buttons);
  }

  public save() {
    this.modelsService
      .save(this.model)
      .then((dados) => {
        this.messagesService.save();
        this.model = dados;
        if (!this.editMod) {
          this.router.navigateByUrl(`${this.routeBase}/${dados.id}`);
        }
      })
      .catch((data) => {
        this.messagesService.errors(data.error.errors as RequestErrorModel[]);
      });
  }

  protected excluir() {
    this.messagesService.deleteConfirm().subscribe((response) => {
      if (response.isOk) {
        this.modelsService
          .delete(this.model.id)
          .then((data) => {
            this.messagesService.delete();
            this.router.navigateByUrl(this.routeBase);
          })
          .catch((data) => {
            this.messagesService.errors(data.error
              .errors as RequestErrorModel[]);
          });
      }
    });
  }
}
