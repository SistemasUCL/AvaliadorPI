import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {
  MatToolbarModule,
  MatButtonModule,
  MatSidenavModule,
  MatIconModule,
  MatListModule,
  MatCardModule
} from '@angular/material';
import { LayoutModule } from '@angular/cdk/layout';

import { FullLayoutComponent } from './full-layout/full-layout.component';
import { SideNavComponent } from './side-nav/side-nav.component';
import { ComponentsModule } from '../../components/components.module';

@NgModule({
  imports: [
    RouterModule,
    RouterModule,
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    ComponentsModule
  ],
  exports: [FullLayoutComponent, SideNavComponent],
  declarations: [FullLayoutComponent, SideNavComponent],
  providers: []
})
export class LayoutsModule {}
