import { AvaliacaoNotaModel } from './avaliacao-nota.model';

export class CriterioModel {
  public id: string;
  public titulo: string;
  public peso: number;
  public avaliacoes: AvaliacaoNotaModel[];
}
