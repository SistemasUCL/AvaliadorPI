import { Routes } from '@angular/router';
import { FullLayoutComponent } from './core/layouts/full-layout/full-layout.component';

import { HomeComponent } from './pages/home/home.component';
import { GruposComponent } from './pages/grupos/grupos.component';
import { GrupoComponent } from './pages/grupo/grupo.component';
import { UsuariosComponent } from './pages/usuarios/usuarios.component';
import { UsuarioComponent } from './pages/usuario/usuario.component';
import { DisciplinasComponent } from './pages/disciplinas/disciplinas.component';
import { DisciplinaComponent } from './pages/disciplina/disciplina.component';
import { ProjetosComponent } from './pages/projetos/projetos.component';
import { ProjetoComponent } from './pages/projeto/projeto.component';
import { ProfessoresComponent } from './pages/professores/professores.component';
import { ProfessorComponent } from './pages/professor/professor.component';
import { AlunosComponent } from './pages/alunos/alunos.component';
import { AlunoComponent } from './pages/aluno/aluno.component';
import { AvaliadoresComponent } from './pages/avaliadores/avaliadores.component';
import { AvaliadorComponent } from './pages/avaliador/avaliador.component';
import { AdministradoresComponent } from './pages/administradores/administradores.component';
import { AdministradorComponent } from './pages/administrador/administrador.component';

import { CallbackComponent } from './components/callback/callback.component';
import { SilentComponent } from './components/callback/silent.component';

import { AuthService } from './core/services/auth.service';
import { AuthGuardService } from './core/services/auth-guard.service';

export const ROUTES: Routes = [
  { path: 'callback', component: CallbackComponent },
  { path: 'silent', component: SilentComponent },
  {
    path: '',
    component: FullLayoutComponent,
    canActivate: [AuthGuardService],
    children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'administradores', component: AdministradoresComponent },
      { path: 'administradores/novo', component: AdministradorComponent },
      { path: 'administradores/:id', component: AdministradorComponent },
      { path: 'alunos', component: AlunosComponent },
      { path: 'alunos/novo', component: AlunoComponent },
      { path: 'alunos/:id', component: AlunoComponent },
      { path: 'avaliadores', component: AvaliadoresComponent },
      { path: 'avaliadores/novo', component: AvaliadorComponent },
      { path: 'avaliadores/:id', component: AvaliadorComponent },
      { path: 'disciplinas', component: DisciplinasComponent },
      { path: 'disciplinas/novo', component: DisciplinaComponent },
      { path: 'disciplinas/:id', component: DisciplinaComponent },
      { path: 'disciplinas', component: DisciplinasComponent },
      { path: 'disciplinas/novo', component: DisciplinaComponent },
      { path: 'disciplinas/:id', component: DisciplinaComponent },
      { path: 'grupos', component: GruposComponent },
      { path: 'grupos/novo', component: GrupoComponent },
      { path: 'grupos/:id', component: GrupoComponent },
      { path: 'professores', component: ProfessoresComponent },
      { path: 'professores/novo', component: ProfessorComponent },
      { path: 'professores/:id', component: ProfessorComponent },
      { path: 'projetos', component: ProjetosComponent },
      { path: 'projetos/novo', component: ProjetoComponent },
      { path: 'projetos/:id', component: ProjetoComponent },
      { path: 'usuarios', component: UsuariosComponent },
      { path: 'usuarios/novo', component: UsuarioComponent },
      { path: 'usuarios/:id', component: UsuarioComponent }
    ]
  }
];
