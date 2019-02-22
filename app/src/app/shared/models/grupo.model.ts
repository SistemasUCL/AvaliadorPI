import { BaseModel } from '../../core/models/base.model';

export class GrupoModel extends BaseModel {
  public nome: string;
  public nomeProjeto: string;
  public projetoId: string;
  public descricao: string;
}
