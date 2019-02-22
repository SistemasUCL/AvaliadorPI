import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { CoreServicesModule } from './services/core-services.module';
import { CoreComponentsModule } from './components/core-components.module';

import { NG_SELECT_DEFAULT_CONFIG, NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  imports: [
    HttpModule,
    HttpClientModule,
    CoreServicesModule,
    CoreComponentsModule,
    NgSelectModule
  ],
  exports: [
    HttpModule,
    HttpClientModule,
    CoreServicesModule,
    CoreComponentsModule,
    NgSelectModule
  ],
  providers: [
    {
      provide: NG_SELECT_DEFAULT_CONFIG,
      useValue: {
        notFoundText: 'Registros não encontrados'
      }
    }
  ]
})
export class CoreModule {}
