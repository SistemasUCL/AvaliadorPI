import {
  Component,
  OnInit,
  ViewChild,
  Input,
  Output,
  EventEmitter
} from '@angular/core';
import { GridComponent } from '../../core/components/grid/grid.component';
import { ToolbarButtonModel } from '../../core/models/toolbar-button.model';
import { GridModel } from '../../core/models/grid-model';

@Component({
  selector: 'app-portlet-grid',
  templateUrl: './portlet-grid.component.html',
  styleUrls: ['./portlet-grid.component.scss']
})
export class PortletGridComponent implements OnInit {
  @Input() title: string;
  @Input() toolbarButtons = [] as ToolbarButtonModel[];
  @Input() gridModel = new GridModel();

  @Output() rowSelect = new EventEmitter<number>();

  @ViewChild(GridComponent) public grid: GridComponent;

  constructor() {}

  ngOnInit() {}

  public select(amount: number) {
    this.rowSelect.emit(amount);
  }
}
