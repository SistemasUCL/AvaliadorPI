// import { RouteInfo } from './sidebar.metadata';

export const SIDENAVROUTES = [
  {
    title: '',
    itens: [{ path: '/home', title: 'Home', icon: 'flaticon-analytics' }]
  },
  {
    title: 'Cadastros',
    itens: [
      { path: '/disciplinas', title: 'Disciplinas', icon: 'la la-book' },
      { path: '/grupos', title: 'Grupos', icon: 'la	la-group' },
      { path: '/projetos', title: 'Projetos', icon: 'la	la-cubes' }
    ]
  },
  {
    title: 'Usuários',
    itens: [
      {
        path: '/administradores',
        title: 'Administradores',
        icon: 'la la-black-tie'
      },
      { path: '/alunos', title: 'Alunos', icon: 'la la-graduation-cap' },
      { path: '/avaliadores', title: 'Avaliadores', icon: 'la la-list-alt' },
      { path: '/professores', title: 'Professores', icon: 'la la-apple' },
      { path: '/usuarios', title: 'Usuários', icon: 'flaticon-users' }
    ]
  }
];
