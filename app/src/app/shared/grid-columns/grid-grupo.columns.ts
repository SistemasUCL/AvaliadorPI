import { GridColumnModel } from '../../core/models/grid-column.model';

export const GridGrupoColumns = [
  { display: 'Nome', field: 'nome', width: 200 },
  { display: 'Nome do Projeto', field: 'nomeProjeto', width: 200 },
  { display: 'Período', field: 'periodo', width: 100 },
  { display: 'Tema', field: 'tema', width: 200 },
  { display: 'Descrição', field: 'descricao', width: 300 }
] as GridColumnModel[];
