import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-full-layout',
  templateUrl: './full-layout.component.html',
  styleUrls: ['./full-layout.component.scss']
})
export class FullLayoutComponent implements OnInit {
  constructor(public router: Router) {}

  ngOnInit() {
    if (this.router.url === '/') {
      this.router.navigate(['']);
    }
  }
}
