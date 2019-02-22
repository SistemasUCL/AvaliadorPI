import { Component, ViewEncapsulation, Renderer2 } from '@angular/core';
import {
  Router,
  ActivatedRoute,
  NavigationStart,
  NavigationEnd,
  NavigationError,
  ActivatedRouteSnapshot
} from '@angular/router';
import { OnInit, OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';

import { SIDENAVROUTES } from './side-nav.routes';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SideNavComponent implements OnInit, OnDestroy {
  public showSideNav = false;
  public pageRouteBase = '';
  public rotas = SIDENAVROUTES;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private renderer: Renderer2
  ) {
    this.renderer.addClass(document.body, 'm-aside-left--fixed');
  }

  public ngOnInit() {
    this.pageRouteBase = this.getActualRoute();
    this.routeListener();
  }

  public ngOnDestroy(): void {
    this.renderer.addClass(document.body, 'm-aside-left--fixed');
  }

  private getActualRoute(): string {
    const rota = (this.route.snapshot['_routerState'].url as string)
      .split('/')
      .find((x) => !!x);

    return `/${rota}`;
  }

  private routeListener() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        // Show loading indicator
      }

      if (event instanceof NavigationEnd) {
        this.pageRouteBase = this.getActualRoute();
      }

      if (event instanceof NavigationError) {
        // Hide loading indicator
        // Present error to user
        console.log(event.error);
      }
    });
  }
}
