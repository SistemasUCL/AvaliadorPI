import { Component, OnInit, VERSION, ViewChild } from '@angular/core';
import { ZXingScannerComponent } from '@zxing/ngx-scanner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  constructor(private route: Router) {}

  ngOnInit(): void {}

  public avaliar() {
    this.route.navigateByUrl('leitor');
  }
}
