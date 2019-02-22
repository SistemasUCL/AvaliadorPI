import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { CoreServicesModule } from './services/core-services.module';
import { CoreComponentsModule } from './components/core-components.module';
import { CoreDirectivesModule } from './directives/core-directives.module';

import { NG_SELECT_DEFAULT_CONFIG, NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  imports: [
    HttpModule,
    CoreComponentsModule,
    CoreServicesModule,
    CoreDirectivesModule,
    NgSelectModule
  ],
  exports: [
    HttpModule,
    CoreComponentsModule,
    CoreServicesModule,
    CoreDirectivesModule,
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
