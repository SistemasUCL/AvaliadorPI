import { GridColumnModel } from '../../core/models/grid-column.model';

export const GridAdministradorColumns = [
  { display: 'Nome', field: 'nome', width: 200 },
  { display: 'Sobrenome', field: 'sobreNome', width: 200 },
  { display: 'Telefone', field: 'telefone', width: 100 },
  { display: 'E-mail', field: 'email', width: 200 }
] as GridColumnModel[];
