import { BaseModel } from '../../core/models/base.model';
import { UsuarioModel } from './usuario.model';

export class AlunoModel extends BaseModel {
  public matricula: string;
  public usuarioId: string;
}
