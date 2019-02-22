import { Component, Input, OnChanges } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-qrcode',
  templateUrl: './qrcode.component.html',
  styleUrls: ['./qrcode.component.scss']
})
export class QrcodeComponent implements OnChanges {
  @Input() qrdata: string;
  @Input() size = 120;
  @Input() level = 'M';

  public qrcodeUrl: string;
  constructor() {}

  ngOnChanges() {
    this.qrcodeUrl = `${environment.API_URL}/grupos/${this.qrdata}/qrcode`;
  }
}
