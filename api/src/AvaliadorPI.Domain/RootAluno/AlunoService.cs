using AvaliadorPI.Domain.Extensions;
using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootAluno.Validators;
using AvaliadorPI.Domain.RootAvaliacao;
using AvaliadorPI.Domain.RootGrupo;
using AvaliadorPI.Domain.RootProjeto;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAluno
{
    public class AlunoService : IAlunoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IGrupoRepository _grupoRepository;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public AlunoService(IUnitOfWork UoW, IAlunoRepository alunoRepository, IGrupoRepository grupoRepository,
            IProjetoRepository projetoRepository, IAvaliacaoRepository avaliacaoRepository)
        {
            _uow = UoW;
            _alunoRepository = alunoRepository;
            _grupoRepository = grupoRepository;
            _projetoRepository = projetoRepository;
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<Aluno> ObterPorId(Guid id)
        {
            return await _alunoRepository.GetByIdAsync(id);
        }

        public async Task<GridResult> ObterDadosGrid(GridRequest request = null)
        {
            var result = new GridResult();

            if (request == null)
            {
                result.Total = await _alunoRepository.CountAsync();
                result.Data = (await _alunoRepository.GetAllAsync())
                    .Select(x => new { x.Id, x.Matricula, x.Usuario.Nome, x.Usuario.SobreNome, x.Usuario.Telefone, x.Usuario.Email });
                return result;
            }

            var data = string.IsNullOrWhiteSpace(request.Search) ?
                    await _alunoRepository.GetAllAsync() :
                    await _alunoRepository.SearchAsync(x =>
                        x.Matricula.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.Nome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.SobreNome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.Email.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.Telefone.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0);

            result.Total = data.Count();

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "matricula" ? data.OrderByDescending(x => x.Matricula) :
                     request.OrderBy == "nome" ? data.OrderByDescending(x => x.Usuario.Nome) :
                     request.OrderBy == "email" ? data.OrderByDescending(x => x.Usuario.Email) :
                     request.OrderBy == "telefone" ? data.OrderByDescending(x => x.Usuario.Telefone) :
                     request.OrderBy == "sobreNome" ? data.OrderByDescending(x => x.Usuario.SobreNome) : data;
                }
                else
                {
                    data =
                     request.OrderBy == "matricula" ? data.OrderBy(x => x.Matricula) :
                     request.OrderBy == "nome" ? data.OrderBy(x => x.Usuario.Nome) :
                     request.OrderBy == "email" ? data.OrderBy(x => x.Usuario.Email) :
                     request.OrderBy == "telefone" ? data.OrderBy(x => x.Usuario.Telefone) :
                     request.OrderBy == "sobreNome" ? data.OrderBy(x => x.Usuario.SobreNome) : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize);

            result.Data = data.Select(x => new { x.Id, x.Matricula, x.Usuario.Nome, x.Usuario.SobreNome, x.Usuario.Telefone, x.Usuario.Email });

            return result;
        }

        public async Task<SubmitResult<Aluno>> Cadastrar(Aluno entity)
        {
            var result = await new CadastrarAlunoValidator(_alunoRepository).ValidateAsync(entity);

            if (result.IsValid)
            {
                _alunoRepository.Add(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Aluno>(await ObterPorId(entity.Id), result);
        }

        public async Task<SubmitResult<Aluno>> Remover(Guid id)
        {
            var result = new ValidationResult();

            if (result.IsValid)
            {
                _alunoRepository.Remove(id);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Aluno>(null, result);
        }

        public async Task<SubmitResult<Aluno>> Editar(Guid id, Aluno entity)
        {
            var atual = await _alunoRepository.GetByIdAsync(id);

            atual.Matricula = entity.Matricula;

            var result = new EditarAlunoValidator().Validate(atual);

            if (result.IsValid)
            {
                _alunoRepository.Update(atual);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Aluno>(atual, result);
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _alunoRepository.AnyAsync(x => x.Id == id);
        }

        public async Task<GridResult> ObterDadosAvaliacoes(Guid id, GridRequest request = null)
        {
            var data = new List<ResultadoAvaliacaoCriterio>();

            var aluno = await _alunoRepository.GetByIdAsync(id);

            var projetos = await _projetoRepository.GetAllAsync();

            var projetoAtual = (await _projetoRepository.SearchAsync(x =>
                x.Estado == Projeto.EnumEstado.Avaliacao &&
                x.Grupos.Any(g => g.AssociacaoAlunoGrupo.Any(a => a.AlunoId == id))
            )).SingleOrDefault();

            if (projetoAtual == null)
                return new GridResult();

            var grupo = await _grupoRepository.ObterGrupo(id, projetoAtual.Id);

            if (grupo == null)
                return new GridResult();

            var avaliacoesAluno = await _avaliacaoRepository.ObterAvaliacoesPorAluno(id, grupo.Id);
            var idsAvaliadores = avaliacoesAluno.Select(x => x.AvaliadorId).Distinct().ToList();

            foreach (var avaliacaoCriterio in avaliacoesAluno.GroupBy(x => x.Criterio))
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
                     request.OrderBy == "aluno" ? data.OrderByDescending(x => x.Criterio).ToList() : data;
                }
                else
                {
                    data =
                     request.OrderBy == "mediaFinal" ? data.OrderBy(x => x.MediaFinal).ToList() :
                     request.OrderBy == "aluno" ? data.OrderBy(x => x.Criterio).ToList() : data;
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

        public async Task<AvaliacaoAluno> ObterAvaliacao(Guid id)
        {
            var projeto = await _projetoRepository.ObterProjetoAtivoPorAluno(id);

            if (projeto == null)
                return null;

            var grupo = (await _grupoRepository
                .SearchAsync(x => x.ProjetoId == projeto.Id &&
                                  x.AssociacaoAlunoGrupo.Any(aluno => aluno.AlunoId == id)))
                .FirstOrDefault();

            return new AvaliacaoAluno
            {
                AlunoId = id,
                Tema = projeto.Tema,
                NomeGrupo = grupo.Nome,
                GrupoId = grupo.Id,
                NomeProjeto = grupo.NomeProjeto,
                Criterios = projeto.Criterios,
                Avaliadores = projeto.AssociacaoAvaliadorProjeto.Select(x => x.Avaliador),
            };
        }
    }
}
