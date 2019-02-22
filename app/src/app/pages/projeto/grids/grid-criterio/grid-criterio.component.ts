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
import { GridCriterioColumns } from '../../../../shared/grid-columns/grid-criterio.columns';
import { MessagesService } from '../../../../core/services/messages.service';
import { RequestErrorModel } from '../../../../core/models/request-error.model';
import { ModalsService } from '../../../../core/services/modals.service';
import { ModalFormCriterioComponent } from '../../../../modals/modal-form-criterio/modal-form-criterio.component';
import { ModalSizeEnum } from '../../../../core/enums/modal-size.enum';
import { CriterioModel } from '../../../../shared/models/criterio.model';
import { ProjetosService } from '../../../../services/projetos.service';

@Component({
  selector: 'app-grid-criterio',
  templateUrl: './grid-criterio.component.html',
  styleUrls: ['./grid-criterio.component.scss']
})
export class GridCriterioComponent implements OnChanges {
  @Input() projetoId: string;

  @ViewChild(GridComponent) grid: GridComponent;
  public gridModel = new GridModel();

  public routeBase: string;
  public toolbarButtons = [] as ToolbarButtonModel[];

  private newButton: ToolbarButtonModel;
  private editButton: ToolbarButtonModel;
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
      searchUrl: this.projetoId ? `projetos/${this.projetoId}/criterios` : '',
      initSearch: true,
      gridColumns: GridCriterioColumns,
      toolbarButtons: this.getToolbarButtons()
    } as GridModel;
  }

  protected getToolbarButtons() {
    this.newButton = {
      icon: 'fa fa-plus-square',
      display: 'Novo',
      disabled: false,
      action: () => {
        this.showModal();
      }
    } as ToolbarButtonModel;

    this.editButton = {
      icon: 'fa fa-edit',
      display: 'Editar',
      disabled: true,
      action: () => this.showModal(this.grid.rowsSelecteds[0])
    } as ToolbarButtonModel;

    this.deleteButton = {
      icon: 'fa fa-trash-o',
      display: 'Excluir',
      disabled: true,
      action: () => this.excluir()
    } as ToolbarButtonModel;

    return [
      this.newButton,
      this.editButton,
      this.deleteButton
    ] as ToolbarButtonModel[];
  }

  public rowSelect(quantidade: number) {
    this.editButton.disabled = quantidade !== 1;
    this.deleteButton.disabled = quantidade === 0;
  }

  protected showModal(criterio?: CriterioModel) {
    this.modalsService
      .showModal(
        ModalFormCriterioComponent,
        { projetoId: this.projetoId, criterio: criterio },
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
            this.projetosService.removeCriterio(this.projetoId, x.id)
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
