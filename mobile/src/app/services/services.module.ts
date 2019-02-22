import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';
import { GruposService } from './grupos.service';

@NgModule({
  imports: [CoreModule],
  providers: [GruposService]
})
export class ServicesModule {}
