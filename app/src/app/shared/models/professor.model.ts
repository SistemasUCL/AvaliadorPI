import { BaseModel } from '../../core/models/base.model';
import { UsuarioModel } from './usuario.model';

export class ProfessorModel extends BaseModel {
  public matricula: string;
  public usuarioId: string;
  public disciplinas: string[];
}
