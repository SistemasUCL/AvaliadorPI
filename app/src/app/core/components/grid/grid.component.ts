import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChanges
} from '@angular/core';
import { GridService } from '../../services/grid.service';
import { forEach } from '@angular/router/src/utils/collection';
import { Router } from '@angular/router';
import { GridModel } from '../../models/grid-model';
import { OrderByDirectionEnum } from '../../enums/order-by-direction.enum';
import { ToolbarButtonModel } from '../../models/toolbar-button.model';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnChanges {
  @Input() gridModel: GridModel;
  @Input() disabled = false;
  @Output() rowSelect = new EventEmitter<number>();

  private searchTerm = '';
  private page = 1;
  private pageSize = 10;
  private total = 0;
  private totalPages = 1;
  private orderBy = '';
  private direction = OrderByDirectionEnum.Asc;
  private textoTotal = '0 - 0 de 0 registros';

  public rows = [];
  public rowsSelecteds = [];

  constructor(private gridService: GridService, private router: Router) {}

  ngOnChanges(changes: SimpleChanges): void {
    this.gridModel = changes.gridModel.currentValue;
    if (this.gridModel.initSearch) {
      this.search();
    }
  }

  public search() {
    if (!this.gridModel || !this.gridModel.searchUrl) {
      return;
    }

    this.gridService
      .search(
        this.gridModel.searchUrl,
        this.searchTerm,
        this.page,
        this.pageSize,
        this.orderBy,
        this.direction
      )
      .then((data) => {
        this.customizeRows(data.data);
        this.customizeColumns(data.data);

        this.total = data.total;
        this.totalPages = Math.ceil(this.total / this.pageSize);
        this.initTextoTotal();
        this.rowsSelecteds = [];
        this.rowSelect.emit(0);
      });
  }

  public setPage(page: number) {
    page = Math.floor(page);

    if (page < 1) {
      page = 1;
    } else if (page > this.totalPages) {
      page = this.totalPages;
    }

    this.page = page;
    this.search();
  }

  public setPageSize(pageSize: number) {
    if (this.pageSize === pageSize) {
      return;
    }

    this.pageSize = pageSize;
    this.search();
  }

  public rowSelected(row: any) {
    row.selected = !row.selected;

    if (row.selected) {
      this.rowsSelecteds.push(row);
    } else {
      this.rowsSelecteds = this.rowsSelecteds.filter(
        (x) => x['id'] !== row['id']
      );
    }
    this.rowSelect.emit(this.rowsSelecteds.length);
  }

  private customizeRows(data: any[]) {
    if (this.gridModel.customizeRows) {
      this.rows = this.gridModel.customizeRows(data);
    } else {
      this.rows = data;
    }
  }

  private customizeColumns(data: any[]) {
    if (this.gridModel.customizeColumns) {
      this.gridModel.gridColumns = this.gridModel.customizeColumns(data);
    }
  }

  private initTextoTotal() {
    this.textoTotal = `${this.page * this.pageSize - this.pageSize + 1} -
    ${
      this.page * this.pageSize < this.total
        ? this.page * this.pageSize
        : this.total
    } de ${this.total} registros`;
  }

  private clickContent(id: number) {
    if (this.gridModel.urlLink) {
      this.router.navigateByUrl(
        this.gridModel.urlLink.replace(':id', id.toString())
      );
    } else if (this.gridModel.actionClick) {
      this.gridModel.actionClick();
    }
  }

  private order(field: string) {
    if (!this.gridModel.searchUrl) {
      return;
    }

    if (this.orderBy === field) {
      this.direction =
        this.direction === OrderByDirectionEnum.Asc
          ? OrderByDirectionEnum.Desc
          : OrderByDirectionEnum.Asc;
    } else {
      this.direction = OrderByDirectionEnum.Asc;
      this.orderBy = field;
    }
    this.search();
  }
}
