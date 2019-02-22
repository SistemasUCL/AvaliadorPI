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
import { GridGrupoColumns } from '../../../../shared/grid-columns/grid-grupo.columns';

@Component({
  selector: 'app-grid-grupo',
  templateUrl: './grid-grupo.component.html',
  styleUrls: ['./grid-grupo.component.scss']
})
export class GridGrupoComponent implements OnChanges {
  @Input() projetoId: string;

  @ViewChild(GridComponent) grid: GridComponent;
  public gridModel = new GridModel();

  public routeBase: string;

  constructor() {}

  ngOnChanges(changes: SimpleChanges) {
    this.projetoId = changes.projetoId.currentValue;
    this.initGrid();
  }

  protected initGrid() {
    this.gridModel = {
      searchUrl: this.projetoId ? `projetos/${this.projetoId}/grupos` : '',
      initSearch: true,
      gridColumns: GridGrupoColumns
    } as GridModel;
  }
}
