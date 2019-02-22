import {
  Component,
  OnInit,
  OnChanges,
  Input,
  ViewChild,
  SimpleChanges
} from '@angular/core';
import { GridComponent } from '../../../../core/components/grid/grid.component';
import { GridModel } from '../../../../core/models/grid-model';
import { ToolbarButtonModel } from '../../../../core/models/toolbar-button.model';
import { MessagesService } from '../../../../core/services/messages.service';
import { ProjetosService } from '../../../../services/projetos.service';
import { ModalsService } from '../../../../core/services/modals.service';
import { GridColumnModel } from '../../../../core/models/grid-column.model';
import { Router } from '@angular/router';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-grid-avaliacao',
  templateUrl: './grid-avaliacao.component.html',
  styleUrls: ['./grid-avaliacao.component.scss']
})
export class GridAvaliacaoComponent implements OnChanges {
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
    private modalsService: ModalsService,
    private router: Router
  ) { }

  ngOnChanges(changes: SimpleChanges) {
    this.projetoId = changes.projetoId.currentValue;
    this.initGrid();
  }

  protected initGrid() {
    this.gridModel = {
      searchUrl: this.projetoId ? `projetos/${this.projetoId}/avaliacoes` : '',
      initSearch: true,
      gridColumns: [
        { display: 'Grupo', field: 'grupo', width: 200 },
        { display: 'Aluno', field: 'aluno', width: 200 }
      ] as GridColumnModel[],
      customizeColumns: this.customizeColumns,
      customizeRows: this.customizeRows,
      toolbarButtons: this.getToolbarButtons()
    } as GridModel;
  }

  protected getToolbarButtons() {
    const downloadButton = {
      icon: 'fa fa-download',
      display: 'Planilha',
      disabled: false,
      action: () => {
        this.baixarAvaliacoes();
      }
    } as ToolbarButtonModel;

    return [downloadButton] as ToolbarButtonModel[];
  }

  public baixarAvaliacoes() {
    window.open(
      `${environment.API_URL}/projetos/${this.projetoId}/excel`,
      '_blank'
    );
  }

  public customizeColumns(rows: any[]): GridColumnModel[] {
    const columns = [
      { display: 'Grupo', field: 'grupo', width: 200 },
      { display: 'Aluno', field: 'aluno', width: 200 }
    ] as GridColumnModel[];

    if (!rows) {
      return columns;
    }

    for (let index = 0; index < rows[0].notas.length; index++) {
      columns.push({
        display: `Nota ${index + 1}`,
        field: `nota${index + 1}`,
        width: 70
      } as GridColumnModel);
    }

    columns.push({
      display: `Média Final`,
      field: `mediaFinal`,
      width: 70
    } as GridColumnModel);

    return columns;
  }

  public customizeRows(rows: any[]): any[] {
    if (!rows) {
      return rows;
    }

    const rowsFormated = [];

    rows.forEach((row) => {
      const item = { grupo: row.grupo, aluno: row.aluno };
      for (let index = 0; index < row.notas.length; index++) {
        item[`nota${index + 1}`] = row.notas[index];
      }
      item['mediaFinal'] = row.mediaFinal;
      rowsFormated.push(item);
    });

    return rowsFormated;
  }
}
