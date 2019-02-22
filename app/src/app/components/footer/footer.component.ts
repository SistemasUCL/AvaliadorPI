import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  encapsulation: ViewEncapsulation.None
})
export class FooterComponent {
  public version: any;

  constructor() {
    this.version = environment.VERSION;
  }
}
