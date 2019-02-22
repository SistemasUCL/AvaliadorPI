import {
  Component,
  ViewEncapsulation,
  OnInit,
  AfterViewInit,
  Renderer2
} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'angular-2-local-storage';

import { AuthService } from '../../core/services/auth.service'

@Component({
  selector: 'app-header-nav',
  templateUrl: './header-nav.component.html',
  styleUrls: ['./header-nav.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HeaderNavComponent implements OnInit {

  public _loggedIn: boolean = false;

  public validacaoPage = false;
  public sidebarMinimized = false;
  public sidebarShowing = false;
  public expandedProfile = false;

  constructor(
    private auth: AuthService,
    private route: ActivatedRoute,
    private renderer: Renderer2,
    private localStorageService: LocalStorageService
  ) {
    this.sidebarMinimized = this.localStorageService.get<boolean>('sidebarMinimized') || false;
  }

  public ngOnInit() {
    if (this.sidebarMinimized) {
      const side = document.getElementById('m_aside_left_minimize_toggle');

      this.renderer.addClass(document.body, 'm-brand--minimize');
      this.renderer.addClass(document.body, 'm-aside-left--minimize');
      this.renderer.addClass(side, 'm-brand__toggler--active');
    }
  }

  public minimizarSideBar() {
    this.sidebarMinimized = !this.sidebarMinimized;
    this.localStorageService.set('sidebarMinimized', this.sidebarMinimized);

    const side = document.getElementById('m_aside_left_minimize_toggle');

    if (this.sidebarMinimized) {
      this.renderer.addClass(document.body, 'm-brand--minimize');
      this.renderer.addClass(document.body, 'm-aside-left--minimize');
      this.renderer.addClass(side, 'm-brand__toggler--active');
    } else {
      this.renderer.removeClass(document.body, 'm-brand--minimize');
      this.renderer.removeClass(document.body, 'm-aside-left--minimize');
      this.renderer.removeClass(side, 'm-brand__toggler--active');
    }
  }

  logout() {
    this.auth.signOut();
  }

  public showSideBarMenu() {
    this.sidebarShowing = !this.sidebarShowing;
    const sideLeft = document.getElementById('m_aside_left');
    this.sidebarShowing
      ? this.renderer.addClass(sideLeft, 'm-aside-left--on')
      : this.renderer.removeClass(sideLeft, 'm-aside-left--on');
  }

  public dropdownProfile(): void {
    this.expandedProfile = !this.expandedProfile;
  }
}
