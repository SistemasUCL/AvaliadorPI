import { BaseModel } from '../../core/models/base.model';
import { AvaliacaoCriterioModel } from './avaliacao-criterio.model';

export class AvaliacaoModel extends BaseModel {
  alunoId: string;
  avaliadorId: string;
  grupoId: string;
  avaliacoesCriterios: AvaliacaoCriterioModel[];
}
