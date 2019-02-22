import { Component, OnInit } from '@angular/core';

import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  public isCollapsed = true;
  constructor(private auth: AuthService) {}

  ngOnInit() {}

  logout() {
    this.auth.signOut();
  }
}
