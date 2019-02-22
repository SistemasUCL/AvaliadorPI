import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { UsuariosService } from './usuarios.service';
import { CoreModule } from '../core/core.module';
import { GruposService } from './grupos.service';
import { DisciplinasService } from './disciplinas.service';
import { ProjetosService } from './projetos.service';
import { ProfessoresService } from './professores.service';
import { CriteriosService } from './criterios.service';
import { AlunosService } from './alunos.service';
import { AvaliadoresService } from './avaliadores.service';
import { AdministradoresService } from './administradores.service';
import { AvaliacoesService } from './avaliacoes.service';

@NgModule({
  imports: [HttpModule, HttpClientModule, CoreModule],
  providers: [
    UsuariosService,
    GruposService,
    DisciplinasService,
    ProjetosService,
    ProfessoresService,
    CriteriosService,
    AlunosService,
    AvaliadoresService,
    AdministradoresService,
    AvaliacoesService
  ]
})
export class ServicesModule {}
