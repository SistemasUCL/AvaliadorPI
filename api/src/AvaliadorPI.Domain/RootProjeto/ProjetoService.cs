using AvaliadorPI.Domain.Associacoes;
using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootAvaliacao;
using AvaliadorPI.Domain.RootProjeto.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProjeto
{
    public class ProjetoService : IProjetoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public ProjetoService(IUnitOfWork UoW, IProjetoRepository projetoRepository,
            IAvaliacaoRepository avaliacaoRepository)
        {
            _uow = UoW;
            _projetoRepository = projetoRepository;
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<Projeto> ObterPorId(Guid id)
        {
            return await _projetoRepository.GetByIdAsync(id);
        }

        public async Task<GridResult> ObterDadosGrid(GridRequest request = null)
        {
            var result = new GridResult();

            if (request == null)
            {
                result.Total = await _projetoRepository.CountAsync();
                result.Data = (await _projetoRepository.GetAllAsync())
                    .Select(x => new { x.Id, x.Tema, x.Descricao, x.Periodo, NomeProfessor = x.Professor.Usuario.Nome });
                return result;
            }

            var data = string.IsNullOrWhiteSpace(request.Search) ?
                    await _projetoRepository.GetAllAsync() :
                    await _projetoRepository.SearchAsync(x =>
                        x.Tema.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Descricao.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Periodo.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Professor.Usuario.Nome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0);

            result.Total = data.Count();

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "tema" ? data.OrderByDescending(x => x.Tema) :
                     request.OrderBy == "descricao" ? data.OrderByDescending(x => x.Descricao) :
                     request.OrderBy == "periodo" ? data.OrderByDescending(x => x.Periodo) :
                     request.OrderBy == "nomeProfessor" ? data.OrderByDescending(x => x.Professor.Usuario.Nome) : data;
                }
                else
                {
                    data =
                     request.OrderBy == "tema" ? data.OrderBy(x => x.Tema) :
                     request.OrderBy == "descricao" ? data.OrderBy(x => x.Descricao) :
                     request.OrderBy == "periodo" ? data.OrderBy(x => x.Periodo) :
                     request.OrderBy == "nomeProfessor" ? data.OrderBy(x => x.Professor.Usuario.Nome) : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize);

            result.Data = data.Select(x => new { x.Id, x.Tema, x.Descricao, x.Periodo, NomeProfessor = x.Professor.Usuario.Nome });

            return result;
        }

        public async Task<SubmitResult<Projeto>> Cadastrar(Projeto entity)
        {
            var result = await new CadastrarProjetoValidator(_projetoRepository).ValidateAsync(entity);

            if (result.IsValid)
            {
                _projetoRepository.Add(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Projeto>(await ObterPorId(entity.Id), result);
        }

        public async Task<SubmitResult<Projeto>> Editar(Guid id, Projeto entity)
        {
            var atual = await ObterPorId(id);

            atual.Descricao = entity.Descricao;
            atual.Periodo = entity.Periodo;
            atual.ProfessorId = entity.ProfessorId;
            atual.Tema = entity.Tema;

            var result = await new EditarProjetoValidator(_projetoRepository).ValidateAsync(atual);

            if (result.IsValid)
            {
                _projetoRepository.Update(atual);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Projeto>(atual, result);
        }

        public async Task<SubmitResult<Projeto>> Remover(Guid id)
        {
            var result = new ValidationResult();

            if (result.IsValid)
            {
                _projetoRepository.Remove(id);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Projeto>(null, result);
        }

        public async Task<SubmitResult<Projeto>> AssociarAvaliador(Guid projetoId, Guid avaliadorId)
        {
            var entity = _projetoRepository.GetById(projetoId);

            entity.AssociacaoAvaliadorProjeto.Add(new AssociacaoAvaliadorProjeto()
            {
                AvaliadorId = avaliadorId,
                ProjetoId = projetoId,
            });

            var result = await new AssociarAvaliadorValidator().ValidateAsync(entity);

            if (result.IsValid)
            {
                _projetoRepository.Update(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Projeto>(entity, result);
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _projetoRepository.AnyAsync(x => x.Id == id);
        }

        public async Task<SubmitResult<Projeto>> RemoverAvaliador(Guid projetoId, Guid id)
        {
            var entity = await _projetoRepository.GetByIdAsync(projetoId);

            var result = new ValidationResult();

            if (result.IsValid)
            {
                var associacao = entity.AssociacaoAvaliadorProjeto.First(x => x.AvaliadorId == id);
                entity.AssociacaoAvaliadorProjeto.Remove(associacao);
                _projetoRepository.Update(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Projeto>(entity, result);
        }

        public async Task<GridResult> ObterDadosGridAvaliadores(Guid projetoId, GridRequest request = null)
        {
            var result = new GridResult();

            var data = (await ObterPorId(projetoId)).AssociacaoAvaliadorProjeto.OrderBy(x => x.Avaliador.Usuario.Nome).Select(x => x.Avaliador);

            if (request == null)
            {
                result.Total = data.Count();
                result.Data = data.Select(x => new { x.Id, x.Usuario.Nome, x.Usuario.SobreNome, x.Usuario.Telefone, x.Usuario.Email });
                return result;
            }

            data = string.IsNullOrWhiteSpace(request.Search) ?
                    data : data.Where(x =>
                        x.Usuario.Nome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.SobreNome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.Email.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.Telefone.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0);

            result.Total = data.Count();
            data = data.OrderBy(x => x.Usuario.Nome);

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderByDescending(x => x.Usuario.Nome) :
                     request.OrderBy == "email" ? data.OrderByDescending(x => x.Usuario.Email) :
                     request.OrderBy == "telefone" ? data.OrderByDescending(x => x.Usuario.Telefone) :
                     request.OrderBy == "sobreNome" ? data.OrderByDescending(x => x.Usuario.SobreNome) : data;
                }
                else
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderBy(x => x.Usuario.Nome) :
                     request.OrderBy == "email" ? data.OrderBy(x => x.Usuario.Email) :
                     request.OrderBy == "telefone" ? data.OrderBy(x => x.Usuario.Telefone) :
                     request.OrderBy == "sobreNome" ? data.OrderBy(x => x.Usuario.SobreNome) : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize);

            result.Data = data.Select(x => new { x.Id, x.Usuario.Nome, x.Usuario.SobreNome, x.Usuario.Telefone, x.Usuario.Email });

            return result;
        }

        public async Task<GridResult> ObterDadosGridGrupos(Guid projetoId, GridRequest request = null)
        {
            var result = new GridResult();

            var data = (await ObterPorId(projetoId)).Grupos.OrderBy(x => x.Nome).Select(x => x);

            if (request == null)
            {
                result.Total = data.Count();
                result.Data = data.Select(x => new { x.Id, x.Nome, x.Descricao, x.NomeProjeto, x.Projeto.Periodo });
                return result;
            }

            data = string.IsNullOrWhiteSpace(request.Search) ?
                    data :
                    data.Where(x =>
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
                     request.OrderBy == "periodo" ? data.OrderBy(x => x.Projeto.Periodo) :
                     request.OrderBy == "nomeProjeto" ? data.OrderBy(x => x.NomeProjeto) :
                     request.OrderBy == "descricao" ? data.OrderBy(x => x.Descricao) : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize);

            result.Data = data.Select(x => new { x.Id, x.Nome, x.Descricao, x.NomeProjeto, x.Projeto.Periodo });

            return result;
        }

        public async Task<GridResult> ObterDadosAvaliacoes(Guid projetoId, GridRequest request = null)
        {
            var data = new List<ResultadoAvaliacaoProjeto>();

            var avaliacoes = await _avaliacaoRepository.ObterAvaliacoesPorProjeto(projetoId);
            var idsAvaliadores = avaliacoes.Select(x => x.AvaliadorId).Distinct().ToList();

            foreach (var avaliacaoAluno in avaliacoes.GroupBy(x => new { x.Grupo, x.Aluno }))
            {
                var resultado = new ResultadoAvaliacaoProjeto
                {
                    Grupo = avaliacaoAluno.Key.Grupo,
                    Aluno = avaliacaoAluno.Key.Aluno,
                };

                foreach (var id in idsAvaliadores)
                {
                    var avaliacoesAvaliador = avaliacaoAluno.Where(x => x.AvaliadorId == id);
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
                        x.Aluno.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Grupo.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            result.Total = data.Count();

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "aluno" ? data.OrderByDescending(x => x.Aluno).ToList() :
                     request.OrderBy == "mediaFinal" ? data.OrderByDescending(x => x.MediaFinal).ToList() :
                     request.OrderBy == "grupo" ? data.OrderByDescending(x => x.Grupo).ToList() : data;
                }
                else
                {
                    data =
                     request.OrderBy == "aluno" ? data.OrderBy(x => x.Aluno).ToList() :
                     request.OrderBy == "mediaFinal" ? data.OrderBy(x => x.MediaFinal).ToList() :
                     request.OrderBy == "grupo" ? data.OrderBy(x => x.Grupo).ToList() : data;
                }
            }
            else
            {
                data = data.OrderBy(x => x.Grupo).ThenBy(x => x.Aluno).ToList();
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize).ToList();

            result.Data = data;

            return result;
        }

        public async Task AtualizarEstado(Guid projetoId, int estado)
        {
            var projeto = await _projetoRepository.GetByIdAsync(projetoId);
            projeto.Estado = (Projeto.EnumEstado)estado;
            await _uow.CommitAsync();
        }
    }
}
