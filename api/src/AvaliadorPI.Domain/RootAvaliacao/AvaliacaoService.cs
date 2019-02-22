using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootGrupo;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAvaliacao
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IGrupoRepository _grupoRepository;

        public AvaliacaoService(IUnitOfWork UoW, IAvaliacaoRepository avaliacaoRepository, IGrupoRepository grupoRepository)
        {
            _uow = UoW;
            _grupoRepository = grupoRepository;
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<Avaliacao> ObterPorId(Guid id)
        {
            return await _avaliacaoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Avaliacao>> ObterDadosGrid(GridRequest request = null)
        {
            return await _avaliacaoRepository.GetAllAsync();
        }

        public async Task<ValidationResult> RealizarAvaliacao(Guid grupoId, IEnumerable<Avaliacao> avaliacoes)
        {
            foreach (var avaliacao in avaliacoes)
            {
                _avaliacaoRepository.Add(avaliacao);
            }

            await _uow.CommitAsync();

            return new ValidationResult();
        }

        public async Task<SubmitResult<Avaliacao>> Editar(Guid id, int nota)
        {
            var result = new ValidationResult();

            if (nota < 0)
                result.Errors.Add(new ValidationFailure("Nota", "A nota é obrigatória!"));

            if (result.IsValid)
            {
                var avaliacao = _avaliacaoRepository.GetById(id);
                avaliacao.Nota = nota;
                await _uow.CommitAsync();
                return new SubmitResult<Avaliacao>(avaliacao, result);
            }

            return new SubmitResult<Avaliacao>(null, result);
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _avaliacaoRepository.AnyAsync(x => x.Id == id);
        }

        public async Task<ValidationResult> RealizarAvaliacaoAluno(List<Avaliacao> avaliacoes)
        {
            foreach (var avaliacao in avaliacoes)
            {
                await _avaliacaoRepository.AddOrUpdateAvaliacaoAluno(avaliacao);
            }

            await _uow.CommitAsync();

            return new ValidationResult();
        }
    }
}
