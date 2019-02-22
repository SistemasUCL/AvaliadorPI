import { BaseModel } from '../../core/models/base.model';
import { UsuarioModel } from './usuario.model';

export class AlunoGrupoModel extends BaseModel {
  public alunoId: string;
  public grupoId: string;
}
