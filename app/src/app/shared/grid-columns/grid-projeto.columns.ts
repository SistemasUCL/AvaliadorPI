import { GridColumnModel } from '../../core/models/grid-column.model';

export const GridProjetoColumns = [
  { display: 'Período', field: 'periodo', width: 100 },
  {
    display: 'Tema',
    field: 'tema',
    width: 200
  },
  {
    display: 'Descrição',
    field: 'descricao',
    width: 200
  }
  // { display: 'Professor', field: 'professor.nome', width: 200 }
] as GridColumnModel[];
