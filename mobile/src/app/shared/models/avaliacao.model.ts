import { CriterioModel } from './criterio.model';
import { AlunoModel } from './aluno.model';

export class AvaliacaoModel {
  public grupoId: string;
  public nome: string;
  public sobreNome: string;
  public projeto: string;
  public criterios: CriterioModel[];
  public alunos: AlunoModel[];
}
