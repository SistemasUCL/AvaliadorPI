import { BaseModel } from '../../core/models/base.model';
import { UsuarioModel } from './usuario.model';

export class AdministradorModel extends BaseModel {
  public usuarioId: string;
}
