import {
  Component,
  OnInit,
  ViewChild,
  Input,
  OnChanges,
  SimpleChanges,
  SimpleChange
} from '@angular/core';
import { GridComponent } from '../../../../core/components/grid/grid.component';
import { GridModel } from '../../../../core/models/grid-model';
import { ToolbarButtonModel } from '../../../../core/models/toolbar-button.model';
import { MessagesService } from '../../../../core/services/messages.service';
import { RequestErrorModel } from '../../../../core/models/request-error.model';
import { ModalsService } from '../../../../core/services/modals.service';
import { ModalSizeEnum } from '../../../../core/enums/modal-size.enum';
import { ProjetosService } from '../../../../services/projetos.service';
import { GridAvaliadorColumns } from '../../../../shared/grid-columns/grid-avaliador.columns';
import { ModalSelectAvaliadorComponent } from '../../../../modals/modal-select-avaliador/modal-select-avaliador.component';

@Component({
  selector: 'app-grid-avaliador',
  templateUrl: './grid-avaliador.component.html',
  styleUrls: ['./grid-avaliador.component.scss']
})
export class GridAvaliadorComponent implements OnChanges {
  @Input() projetoId: string;

  @ViewChild(GridComponent) grid: GridComponent;
  public gridModel = new GridModel();

  public routeBase: string;
  public toolbarButtons = [] as ToolbarButtonModel[];

  private newButton: ToolbarButtonModel;
  private deleteButton: ToolbarButtonModel;

  constructor(
    private messagesService: MessagesService,
    private projetosService: ProjetosService,
    private modalsService: ModalsService
  ) {}

  ngOnChanges(changes: SimpleChanges) {
    this.projetoId = changes.projetoId.currentValue;
    this.initGrid();
  }

  protected initGrid() {
    this.gridModel = {
      searchUrl: this.projetoId ? `projetos/${this.projetoId}/avaliadores` : '',
      initSearch: true,
      gridColumns: GridAvaliadorColumns,
      toolbarButtons: this.getToolbarButtons()
    } as GridModel;
  }

  protected getToolbarButtons() {
    this.newButton = {
      icon: 'fa fa-plus-square',
      display: 'Adicionar',
      disabled: false,
      action: () => {
        this.showModal();
      }
    } as ToolbarButtonModel;

    this.deleteButton = {
      icon: 'fa fa-trash-o',
      display: 'Remover',
      disabled: true,
      action: () => this.excluir()
    } as ToolbarButtonModel;

    return [this.newButton, this.deleteButton] as ToolbarButtonModel[];
  }

  public rowSelect(quantidade: number) {
    this.deleteButton.disabled = quantidade === 0;
  }

  protected showModal() {
    this.modalsService
      .showModal(
        ModalSelectAvaliadorComponent,
        this.projetoId,
        ModalSizeEnum.Big
      )
      .subscribe((response) => {
        if (response.isOk) {
          this.grid.search();
        }
      });
  }

  protected excluir() {
    this.messagesService.deleteConfirm().subscribe((response) => {
      if (response.isOk) {
        Promise.all(
          this.grid.rowsSelecteds.map((x) =>
            this.projetosService.removeAvaliador(this.projetoId, x.id)
          )
        )
          .then((data) => {
            this.messagesService.delete();
            this.grid.search();
          })
          .catch((data) => {
            this.messagesService.errors(data.error
              .errors as RequestErrorModel[]);
            this.grid.search();
          });
      }
    });
  }
}
