import { BaseModel } from '../../core/models/base.model';

export class UsuarioModel extends BaseModel {
  public nome: string;
  public sobreNome: string;
  public email: string;
  public telefone: string;
}
