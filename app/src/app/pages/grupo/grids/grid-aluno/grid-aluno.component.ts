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
import { ModalFormCriterioComponent } from '../../../../modals/modal-form-criterio/modal-form-criterio.component';
import { ModalSizeEnum } from '../../../../core/enums/modal-size.enum';
import { CriterioModel } from '../../../../shared/models/criterio.model';
import { GridAlunoColumns } from '../../../../shared/grid-columns/grid-aluno.columns';
import { GruposService } from '../../../../services/grupos.service';
import { ModalSelectAlunoComponent } from '../../../../modals/modal-select-aluno/modal-select-aluno.component';

@Component({
  selector: 'app-grid-aluno',
  templateUrl: './grid-aluno.component.html',
  styleUrls: ['./grid-aluno.component.scss']
})
export class GridAlunoComponent implements OnChanges {
  @Input() grupoId: string;

  @ViewChild(GridComponent) grid: GridComponent;
  public gridModel = new GridModel();

  public routeBase: string;
  public toolbarButtons = [] as ToolbarButtonModel[];

  private newButton: ToolbarButtonModel;
  private deleteButton: ToolbarButtonModel;

  constructor(
    private messagesService: MessagesService,
    private gruposService: GruposService,
    private modalsService: ModalsService
  ) {}

  ngOnChanges(changes: SimpleChanges) {
    this.grupoId = changes.grupoId.currentValue;
    this.initGrid();
  }

  protected initGrid() {
    this.gridModel = {
      searchUrl: this.grupoId ? `grupos/${this.grupoId}/alunos` : '',
      initSearch: true,
      gridColumns: GridAlunoColumns,
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
      .showModal(ModalSelectAlunoComponent, this.grupoId, ModalSizeEnum.Big)
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
            this.gruposService.removeAluno(this.grupoId, x.id)
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
