import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

import { ToolbarButtonModel } from '../../models/toolbar-button.model';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {
  @Input() buttons = [] as ToolbarButtonModel[];
  public query = '';

  constructor(private router: Router) {}

  ngOnInit() {}

  onClick(button: ToolbarButtonModel) {
    if (button.href) {
      button.useGridQuery
        ? this.router.navigateByUrl(`${button.href}/${this.query}`)
        : this.router.navigateByUrl(button.href);
    } else {
      button.useGridQuery ? button.actionQuery(this.query) : button.action();
    }
  }
}
