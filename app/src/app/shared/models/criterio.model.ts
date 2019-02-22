import { BaseModel } from '../../core/models/base.model';

export class CriterioModel extends BaseModel {
  public titulo: string;
  public descricao: string;
  public peso: number;
  public ordem: number;
  public projetoId: string;
}
