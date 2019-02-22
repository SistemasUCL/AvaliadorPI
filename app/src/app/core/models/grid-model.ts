import { GridColumnModel } from './grid-column.model';
import { ToolbarButtonModel } from './toolbar-button.model';

export class GridModel {
  public searchUrl: string;
  public title: string;
  public urlLink: string;
  public gridColumns: GridColumnModel[];
  public toolbarButtons: ToolbarButtonModel[];
  public initSearch: boolean;
  public actionClick: () => {};
  public customizeColumns: (rows: any[]) => GridColumnModel[];
  public customizeRows: (rows: any[]) => any[];
}
