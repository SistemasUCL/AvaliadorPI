import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { ToolbarButtonModel } from '../../core/models/toolbar-button.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-portlet-toolbar',
  templateUrl: './portlet-toolbar.component.html',
  styleUrls: ['./portlet-toolbar.component.scss']
})
export class PortletToolbarComponent implements OnInit {
  @Input() buttons = [] as ToolbarButtonModel[];

  constructor(private router: Router) {}

  ngOnInit() {}

  onClick(button: ToolbarButtonModel) {
    if (button.href) {
      this.router.navigateByUrl(button.href);
    } else {
      button.action();
    }
  }
}
