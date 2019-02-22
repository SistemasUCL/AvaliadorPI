import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { CoreModule } from '../core/core.module';
import { ModalModule } from 'ngx-bootstrap';
import { QRCodeModule } from 'angularx-qrcode';

// Modules
import { LocalStorageModule } from 'angular-2-local-storage';
import { ServicesModule } from '../services/services.module';

import { SideNavComponent } from './sidenav/side-nav.component';
import { HeaderNavComponent } from './header-nav.component/header-nav.component';
import { FooterComponent } from './footer/footer.component';
import { PortletToolbarComponent } from './portlet-toolbar/portlet-toolbar.component';

import { PortletGridComponent } from './portlet-grid/portlet-grid.component';
import { QrcodeComponent } from './qrcode/qrcode.component';
import { StarRatingComponent } from './star-rating/star-rating.component';

@NgModule({
  imports: [
    RouterModule,
    BrowserModule,
    CommonModule,
    ServicesModule,
    CoreModule,
    ModalModule,
    QRCodeModule,
    LocalStorageModule.withConfig({
      prefix: 'avaliador-pi',
      storageType: 'localStorage'
    })
  ],
  declarations: [
    SideNavComponent,
    HeaderNavComponent,
    FooterComponent,
    PortletToolbarComponent,
    PortletGridComponent,
    QrcodeComponent,
    StarRatingComponent
  ],
  exports: [
    SideNavComponent,
    HeaderNavComponent,
    FooterComponent,
    PortletToolbarComponent,
    PortletGridComponent,
    QrcodeComponent,
    StarRatingComponent
  ]
})
export class ComponentsModule {}
