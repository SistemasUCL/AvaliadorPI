import { Component, OnInit, ViewChild } from '@angular/core';

import { PortletGridComponent } from '../../components/portlet-grid/portlet-grid.component';
import { GridModel } from '../../core/models/grid-model';
import { ToolbarButtonModel } from '../../core/models/toolbar-button.model';
import { Router } from '@angular/router';
import { MessagesService } from '../../core/services/messages.service';
import { ProjetosService } from '../../services/projetos.service';
import { GridColumnModel } from '../../core/models/grid-column.model';
import { BaseModel } from '../../core/models/base.model';
import { RequestService } from '../../core/services/request.service';
import { RequestErrorModel } from '../../core/models/request-error.model';

@Component({
  template: ''
})
export class ListPageComponent<T extends BaseModel> implements OnInit {
  @ViewChild(PortletGridComponent) public portletGrid: PortletGridComponent;

  public routeBase: string;

  public grid = new GridModel();
  public toolbarButtons = [] as ToolbarButtonModel[];

  protected newButton: ToolbarButtonModel;
  protected editButton: ToolbarButtonModel;
  protected deleteButton: ToolbarButtonModel;

  constructor(
    protected router: Router,
    protected messagesService: MessagesService,
    protected modelService: RequestService<T>
  ) {}

  public ngOnInit() {
    this.initToolbar();
  }

  protected initGrid(columns: GridColumnModel[], initSearch: boolean = true) {
    this.grid = {
      searchUrl: this.routeBase,
      urlLink: `${this.routeBase}/:id`,
      initSearch: initSearch,
      gridColumns: columns
    } as GridModel;
  }

  protected initToolbar() {
    this.newButton = {
      icon: 'fa fa-plus-square',
      display: 'Novo',
      disabled: false,
      href: `${this.routeBase}/novo`
    } as ToolbarButtonModel;

    this.editButton = {
      icon: 'fa fa-edit',
      display: 'Editar',
      disabled: true,
      action: () => this.editar()
    } as ToolbarButtonModel;

    this.deleteButton = {
      icon: 'fa fa-trash-o',
      display: 'Excluir',
      disabled: true,
      action: () => this.excluir()
    } as ToolbarButtonModel;

    this.toolbarButtons = [
      this.newButton,
      this.editButton,
      this.deleteButton
    ] as ToolbarButtonModel[];
  }

  public rowSelect(quantidade: number) {
    this.editButton.disabled = quantidade !== 1;
    this.deleteButton.disabled = quantidade === 0;
  }

  protected editar() {
    this.router.navigateByUrl(
      `${this.routeBase}/${this.portletGrid.grid.rowsSelecteds[0].id}`
    );
  }

  protected excluir() {
    this.messagesService.deleteConfirm().subscribe((response) => {
      if (response.isOk) {
        Promise.all(
          this.portletGrid.grid.rowsSelecteds.map((x) =>
            this.modelService.delete(x.id)
          )
        )
          .then((data) => {
            this.messagesService.delete();
            this.portletGrid.grid.search();
          })
          .catch((data) => {
            this.messagesService.errors(data.error
              .errors as RequestErrorModel[]);
            this.portletGrid.grid.search();
          });
      }
    });
  }
}
