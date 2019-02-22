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
import { ModalsService } from '../../../../core/services/modals.service';
import { GridColumnModel } from '../../../../core/models/grid-column.model';
import { Router } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { GruposService } from '../../../../services/grupos.service';

@Component({
  selector: 'app-grid-avaliacao-grupo',
  templateUrl: './grid-avaliacao-grupo.component.html',
  styleUrls: ['./grid-avaliacao-grupo.component.scss']
})
export class GridAvaliacaoGrupoComponent implements OnChanges {

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
    private modalsService: ModalsService,
  ) { }

  ngOnChanges(changes: SimpleChanges) {
    this.grupoId = changes.grupoId.currentValue;
    this.initGrid();
  }

  protected initGrid() {
    this.gridModel = {
      searchUrl: this.grupoId ? `grupos/${this.grupoId}/avaliacoes` : '',
      initSearch: true,
      gridColumns: [
        { display: 'Critério', field: 'criterio', width: 200 }
      ] as GridColumnModel[],
      customizeColumns: this.customizeColumns,
      customizeRows: this.customizeRows,
      toolbarButtons: null,//this.toolbarButtons
    } as GridModel;
  }

  public customizeColumns(rows: any[]): GridColumnModel[] {
    const columns = [
      { display: 'Critério', field: 'criterio', width: 200 }
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
      display: `Média`,
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
      const item = { criterio: row.criterio };
      for (let index = 0; index < row.notas.length; index++) {
        item[`nota${index + 1}`] = row.notas[index];
      }
      item['mediaFinal'] = row.mediaFinal;
      rowsFormated.push(item);
    });

    return rowsFormated;
  }
}
