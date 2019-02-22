import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BlankLayoutComponent } from './blank-layout/blank-layout.component';
import { FullLayoutComponent } from './full-layout/full-layout.component';
import { ComponentsModule } from '../../components/components.module';

@NgModule({
  imports: [RouterModule, ComponentsModule],
  exports: [BlankLayoutComponent, FullLayoutComponent],
  declarations: [BlankLayoutComponent, FullLayoutComponent]
})
export class CoreLayoutsModule {}
