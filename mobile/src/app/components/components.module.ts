import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { CollapseModule } from 'ngx-bootstrap';

import { LayoutModule } from '@angular/cdk/layout';
import { MaterialDesignModule } from '../core/material-design/material-design.module';
import { StarRatingComponent } from './star-rating/star-rating.component';
import { CardComponent } from './card/card.component';
import { MenuComponent } from './menu/menu.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  imports: [
    RouterModule,
    BrowserModule,
    BrowserAnimationsModule,
    MaterialDesignModule,
    LayoutModule,
    CollapseModule,
    CoreModule
  ],
  declarations: [StarRatingComponent, CardComponent, MenuComponent],
  exports: [CoreModule, StarRatingComponent, CardComponent, MenuComponent],
  providers: []
})
export class ComponentsModule {}
