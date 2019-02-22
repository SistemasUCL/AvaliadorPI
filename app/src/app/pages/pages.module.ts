import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

// Modules - Application
import { ComponentsModule } from '../components/components.module';
import { ModalsModule } from '../modals/modals.module';
import { NgxMaskModule } from 'ngx-mask';

// Components
import { HomeComponent } from './home/home.component';
import { CallbackComponent } from '../components/callback/callback.component';
import { SilentComponent } from '../components/callback/silent.component';
import { GruposComponent } from './grupos/grupos.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { UsuarioComponent } from './usuario/usuario.component';
import { CoreModule } from '../core/core.module';
import { GrupoComponent } from './grupo/grupo.component';
import { DisciplinasComponent } from './disciplinas/disciplinas.component';
import { DisciplinaComponent } from './disciplina/disciplina.component';
import { ProjetosComponent } from './projetos/projetos.component';
import { ProjetoComponent } from './projeto/projeto.component';
import { ListPageComponent } from './_base/list-page.component';
import { FormPageComponent } from './_base/form-page.component';
import { ProfessoresComponent } from './professores/professores.component';
import { ProfessorComponent } from './professor/professor.component';
import { GridCriterioComponent } from './projeto/grids/grid-criterio/grid-criterio.component';
import { AlunosComponent } from './alunos/alunos.component';
import { AlunoComponent } from './aluno/aluno.component';
import { AvaliadoresComponent } from './avaliadores/avaliadores.component';
import { AvaliadorComponent } from './avaliador/avaliador.component';
import { GridAlunoComponent } from './grupo/grids/grid-aluno/grid-aluno.component';
import { GridAvaliadorComponent } from './projeto/grids/grid-avaliador/grid-avaliador.component';
import { GridGrupoComponent } from './projeto/grids/grid-grupo/grid-grupo.component';
import { AdministradoresComponent } from './administradores/administradores.component';
import { AdministradorComponent } from './administrador/administrador.component';
import { GridAvaliacaoComponent } from './projeto/grids/grid-avaliacao/grid-avaliacao.component';
import { GridAvaliacaoGrupoComponent } from './grupo/grids/grid-avaliacao/grid-avaliacao-grupo.component';
import { GridAvaliacaoAlunoComponent } from './aluno/grids/grid-avaliacao/grid-avaliacao-aluno.component';
import { AvaliacaoModalComponent } from './aluno/modals/avaliacao-modal/avaliacao-modal.component';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    ComponentsModule,
    CoreModule,
    ModalsModule,
    NgxMaskModule.forRoot()
  ],
  declarations: [
    HomeComponent,
    CallbackComponent,
    SilentComponent,
    ListPageComponent,
    FormPageComponent,
    GruposComponent,
    UsuariosComponent,
    UsuarioComponent,
    GrupoComponent,
    DisciplinasComponent,
    DisciplinaComponent,
    ProjetosComponent,
    ProjetoComponent,
    ProfessoresComponent,
    ProfessorComponent,
    AlunosComponent,
    AlunoComponent,
    AvaliadoresComponent,
    AvaliadorComponent,
    AdministradoresComponent,
    AdministradorComponent,

    GridCriterioComponent,
    GridAlunoComponent,
    GridAvaliadorComponent,
    GridGrupoComponent,
    GridAvaliacaoComponent,
    GridAvaliacaoGrupoComponent,
    GridAvaliacaoAlunoComponent,

    AvaliacaoModalComponent
  ],
  entryComponents: [AvaliacaoModalComponent]
})
export class PagesModule { }
