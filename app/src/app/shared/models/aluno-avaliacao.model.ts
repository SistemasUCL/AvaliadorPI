import { BaseModel } from '../../core/models/base.model';
import { CriterioModel } from './criterio.model';
import { AvaliadorModel } from './avaliador.model';

export class AlunoAvaliacaoModel extends BaseModel {
  tema: string;
  nomeGrupo: string;
  grupoId: string;
  nomeProjeto: string;
  criterios: CriterioModel[];
  avaliadores: AvaliadorModel[];
}
