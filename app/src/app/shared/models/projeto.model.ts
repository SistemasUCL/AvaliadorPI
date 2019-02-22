import { BaseModel } from '../../core/models/base.model';
import { EstadoProjetoEnum } from '../enums/estado-projeto.enum';

export class ProjetoModel extends BaseModel {
  public periodo: string;
  public tema: string;
  public descricao: string;
  public professorId: string;
  public disciplinas: string[];
  public estado: EstadoProjetoEnum;
}
