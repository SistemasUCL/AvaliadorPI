using AvaliadorPI.Domain.Associacoes;
using AvaliadorPI.Domain.Extensions;
using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootAvaliacao;
using AvaliadorPI.Domain.RootGrupo.Validators;
using AvaliadorPI.Domain.RootProjeto;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootGrupo
{
    public class GrupoService : IGrupoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGrupoRepository _grupoRepository;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public GrupoService(IUnitOfWork UoW, IGrupoRepository grupoRepository,
            IProjetoRepository projetoRepository, IAvaliacaoRepository avaliacaoRepository)
        {
            _uow = UoW;
            _projetoRepository = projetoRepository;
            _avaliacaoRepository = avaliacaoRepository;
            _grupoRepository = grupoRepository;
        }

        public async Task<Grupo> ObterPorId(Guid id)
        {
            return await _grupoRepository.GetByIdAsync(id);
        }

        public async Task<GridResult> ObterDadosGrid(GridRequest request = null)
        {
            var result = new GridResult();

            if (request == null)
            {
                result.Total = await _grupoRepository.CountAsync();
                result.Data = (await _grupoRepository.GetAllAsync())
                    .Select(x => new { x.Id, x.Projeto.Periodo, x.Projeto.Tema, x.Nome, x.NomeProjeto, x.Descricao })
                    .OrderByDescending(x => x.Periodo).ThenBy(x => x.Tema).ThenBy(x => x.Nome)
                    .ToList();
                return result;
            }

            var data = string.IsNullOrWhiteSpace(request.Search) ?
                    await _grupoRepository.GetAllAsync() :
                    await _grupoRepository.SearchAsync(x =>
                        x.Nome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Descricao.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Projeto.Periodo.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.NomeProjeto.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0);

            result.Total = data.Count();

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderByDescending(x => x.Nome) :
                     request.OrderBy == "periodo" ? data.OrderByDescending(x => x.Projeto.Periodo) :
                     request.OrderBy == "nomeProjeto" ? data.OrderByDescending(x => x.NomeProjeto) :
                     request.OrderBy == "descricao" ? data.OrderByDescending(x => x.Descricao) : data;
                }
                else
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderBy(x => x.Nome) :
                     request.OrderBy == "tema" ? data.OrderBy(x => x.Nome) :
                     request.OrderBy == "periodo" ? data.OrderBy(x => x.Projeto.Periodo) :
                     request.OrderBy == "nomeProjeto" ? data.OrderBy(x => x.NomeProjeto) :
                     request.OrderBy == "descricao" ? data.OrderBy(x => x.Descricao) : data;
                }
                result.Data = data
                    .Select(x => new { x.Id, x.Projeto.Periodo, x.Projeto.Tema, x.Nome, x.NomeProjeto, x.Descricao }).ToList();
            }
            else
            {
                result.Data = data
                    .Select(x => new { x.Id, x.Projeto.Periodo, x.Projeto.Tema, x.Nome, x.NomeProjeto, x.Descricao })
                    .OrderByDescending(x => x.Periodo).ThenBy(x => x.Tema).ThenBy(x => x.Nome)
                    .ToList();
            }

            if (request.PageSize > 0 && request.Page > 0)
                result.Data = result.Data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize).ToList();

            return result;
        }

        public async Task<SubmitResult<Grupo>> Cadastrar(Grupo entity)
        {
            var result = await new CadastrarGrupoValidator(_grupoRepository).ValidateAsync(entity);

            if (result.IsValid)
            {
                _grupoRepository.Add(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Grupo>(await ObterPorId(entity.Id), result);
        }

        public async Task<SubmitResult<Grupo>> Editar(Guid id, Grupo entity)
        {
            var atual = await ObterPorId(id);

            atual.Descricao = entity.Descricao;
            atual.Nome = entity.Nome;
            atual.NomeProjeto = entity.NomeProjeto;
            atual.ProjetoId = entity.ProjetoId;

            var result = await new EditarGrupoValidator(_grupoRepository).ValidateAsync(atual);

            if (result.IsValid)
            {
                _grupoRepository.Update(atual);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Grupo>(atual, result);
        }

        public async Task<SubmitResult<Grupo>> Remover(Guid id)
        {
            var result = new ValidationResult();

            if (result.IsValid)
            {
                _grupoRepository.Remove(id);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Grupo>(null, result);
        }

        public async Task<SubmitResult<Grupo>> AssociarAluno(Guid grupoId, Guid alunoId)
        {
            var entity = _grupoRepository.GetById(grupoId);

            entity.AssociacaoAlunoGrupo.Add(new AssociacaoAlunoGrupo()
            {
                GrupoId = grupoId,
                AlunoId = alunoId,
            });

            var result = await new AssociarAlunoValidator(_grupoRepository).ValidateAsync(entity);

            if (result.IsValid)
            {
                _grupoRepository.Update(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Grupo>(entity, result);
        }

        public async Task<GridResult> ObterDadosGridAlunos(Guid grupoId, GridRequest request)
        {
            var result = new GridResult();

            var grupo = await _grupoRepository.GetByIdAsync(grupoId);

            if (request == null)
            {
                result.Total = grupo.AssociacaoAlunoGrupo.Count;
                result.Data = grupo.AssociacaoAlunoGrupo
                    .Select(x => new
                    {
                        x.AlunoId,
                        x.Aluno.Matricula,
                        x.Aluno.Usuario.Nome,
                        x.Aluno.Usuario.SobreNome,
                        x.Aluno.Usuario.Telefone,
                        x.Aluno.Usuario.Email
                    })
                    .OrderBy(x => x.Nome);
                return result;
            }

            var data = string.IsNullOrWhiteSpace(request.Search) ?
                     grupo.AssociacaoAlunoGrupo.Select(x => x.Aluno) :
                     grupo.AssociacaoAlunoGrupo.Select(x => x.Aluno)
                     .Where(x =>
                        x.Matricula.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.Nome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.Telefone.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.SobreNome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.Email.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0);

            result.Total = data.Count();

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderByDescending(x => x.Usuario.Nome) :
                     request.OrderBy == "sobreNome" ? data.OrderByDescending(x => x.Usuario.SobreNome) :
                     request.OrderBy == "matricula" ? data.OrderByDescending(x => x.Matricula) :
                     request.OrderBy == "telefone" ? data.OrderByDescending(x => x.Usuario.Telefone) :
                     request.OrderBy == "email" ? data.OrderByDescending(x => x.Usuario.Email) : data;
                }
                else
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderBy(x => x.Usuario.Nome) :
                     request.OrderBy == "sobreNome" ? data.OrderBy(x => x.Usuario.SobreNome) :
                     request.OrderBy == "matricula" ? data.OrderBy(x => x.Matricula) :
                     request.OrderBy == "telefone" ? data.OrderBy(x => x.Usuario.Telefone) :
                     request.OrderBy == "email" ? data.OrderBy(x => x.Usuario.Email) : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize);

            result.Data = data.Select(x => new
            {
                x.Id,
                x.Matricula,
                x.Usuario.Nome,
                x.Usuario.SobreNome,
                x.Usuario.Telefone,
                x.Usuario.Email
            });

            return result;
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _grupoRepository.AnyAsync(x => x.Id == id);
        }

        public async Task<SubmitResult<Grupo>> RemoverAluno(Guid grupoId, Guid alunoId)
        {
            var entity = await _grupoRepository.GetByIdAsync(grupoId);

            var result = new ValidationResult();

            if (result.IsValid)
            {
                var associacao = entity.AssociacaoAlunoGrupo.First(x => x.AlunoId == alunoId);
                entity.AssociacaoAlunoGrupo.Remove(associacao);
                _grupoRepository.Update(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Grupo>(entity, result);
        }

        public async Task<AvaliacaoGrupo> ObterAvaliacaoParaGrupo(Guid id)
        {
            var grupo = await _grupoRepository.ObterGrupoParaAvaliacao(id);

            return new AvaliacaoGrupo
            {
                GrupoId = grupo.Id,
                Nome = grupo.Nome,
                Tema = grupo.Projeto.Tema,
                Projeto = grupo.NomeProjeto,
                Alunos = grupo.AssociacaoAlunoGrupo.Select(x => x.Aluno).OrderBy(x => x.Usuario.Nome),
                Criterios = grupo.Projeto.Criterios.OrderBy(x => x.Ordem),
            };
        }

        public async Task<ValidationResult> ValidarAvaliador(Guid grupoId, Guid avaliadorId)
        {
            var grupo = await _grupoRepository.GetByIdAsync(grupoId);
            return await new AvaliarGrupoValidator(_grupoRepository, _avaliacaoRepository, avaliadorId).ValidateAsync(grupo);
        }

        public async Task<GridResult> ObterDadosAvaliacoes(Guid id, GridRequest request = null)
        {
            var data = new List<ResultadoAvaliacaoCriterio>();

            var avaliacoesGrupo = await _avaliacaoRepository.ObterAvaliacoesPorGrupo(id);
            var idsAvaliadores = avaliacoesGrupo.Select(x => x.AvaliadorId).Distinct().ToList();

            foreach (var avaliacaoCriterio in avaliacoesGrupo.GroupBy(x => x.Criterio))
            {
                var resultado = new ResultadoAvaliacaoCriterio
                {
                    Criterio = avaliacaoCriterio.Key,
                };

                foreach (var avaliadorId in idsAvaliadores)
                {
                    var avaliacoesAvaliador = avaliacaoCriterio.Where(x => x.AvaliadorId == avaliadorId);
                    var nota = avaliacoesAvaliador.Sum(x => x.Nota * x.Peso) / (avaliacoesAvaliador.Sum(x => x.Peso) * 1.0);
                    resultado.Notas.Add(double.IsNaN(nota) ? (double?)null : Math.Round(nota, 2));
                }

                var media = resultado.Notas.Average();
                resultado.MediaFinal = media == null ? (double?)null : Math.Round(media.Value, 2);

                data.Add(resultado);
            }

            var result = new GridResult();

            if (request == null)
            {
                result.Total = data.Count();
                result.Data = data;
                return result;
            }

            data = string.IsNullOrWhiteSpace(request.Search) ?
                    data :
                    data.Where(x =>
                        x.Criterio.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            result.Total = data.Count();

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "mediaFinal" ? data.OrderByDescending(x => x.MediaFinal).ToList() :
                     request.OrderBy == "criterio" ? data.OrderByDescending(x => x.Criterio).ToList() : data;
                }
                else
                {
                    data =
                     request.OrderBy == "mediaFinal" ? data.OrderBy(x => x.MediaFinal).ToList() :
                     request.OrderBy == "criterio" ? data.OrderBy(x => x.Criterio).ToList() : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize).ToList();

            if (data.Count > 0)
            {
                var total = new ResultadoAvaliacaoCriterio { Criterio = "Média Total" };

                for (int i = 0; i < idsAvaliadores.Count; i++)
                {
                    total.Notas.Add(data.Select(x => x.Notas[i]).Average());
                }

                total.MediaFinal = Math.Round(total.Notas.Average().Value, 2);

                data.Add(total);
            }

            result.Data = data;

            return result;
        }
    }
}
