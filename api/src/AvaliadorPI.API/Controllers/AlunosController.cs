using AutoMapper;
using AvaliadorPI.API.ViewModels.Aluno;
using AvaliadorPI.Domain;
using AvaliadorPI.Domain.RootAluno;
using AvaliadorPI.Domain.RootAvaliacao;
using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootCriterio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaliadorPI.API.Controllers
{
    [Authorize(Roles = "Professor, Administrador")]
    public class AlunosController : BaseController<Aluno>
    {
        private readonly IAlunoService _alunoService;
        private readonly IAvaliadorService _avaliadorService;
        private readonly ICriterioService _criterioService;
        private readonly IAvaliacaoService _avaliacaoService;
        private readonly IMapper _mapper;

        public AlunosController(IMapper mapper,
            IAlunoService alunoService,
            IAvaliadorService avaliadorService,
            ICriterioService criterioService,
            IAvaliacaoService avaliacaoService)
        {
            _mapper = mapper;
            _alunoService = alunoService;
            _avaliadorService = avaliadorService;
            _criterioService = criterioService;
            _avaliacaoService = avaliacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GridRequest request)
        {
            if (request.IsDirty)
                return Ok(await _alunoService.ObterDadosGrid(request));
            return Ok(await _alunoService.ObterDadosGrid());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _alunoService.ObterPorId(id);

            if (entity == null)
                return RegistroNaoEncontrado(id);

            return Ok(_mapper.Map<AlunoViewModel>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AlunoFormViewModel model)
        {
            var result = await _alunoService.Cadastrar(_mapper.Map<Aluno>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<AlunoViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]AlunoFormViewModel model)
        {
            if (!await _alunoService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _alunoService.Editar(id, _mapper.Map<Aluno>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<AlunoViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _alunoService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _alunoService.Remover(id);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }

        [HttpGet("{id:guid}/avaliacao")]
        public async Task<IActionResult> GetAvaliacao(Guid id)
        {
            if (!await _alunoService.Existe(id))
                return RegistroNaoEncontrado(id);

            return Ok(_mapper.Map<AvaliacaoAlunoViewModel>(await _alunoService.ObterAvaliacao(id)));
        }

        [Authorize(Roles = "Professor, Administrador, Avaliador")]
        [HttpPost("{alunoId:guid}/avaliacao")]
        public async Task<IActionResult> PostAvaliacao(Guid alunoId, [FromBody]ResultadoAvaliacaoViewModel resultado)
        {
            var data = DateTime.Now;

            List<Avaliacao> avaliacoes = new List<Avaliacao>();

            foreach (var criterio in resultado.AvaliacoesCriterios)
            {
                avaliacoes.Add(new Avaliacao
                {
                    AvaliadorId = resultado.AvaliadorId,
                    AlunoId = resultado.AlunoId,
                    GrupoId = resultado.GrupoId,
                    CriterioId = criterio.CriterioId,
                    Data = data,
                    Nota = criterio.Nota * 2,
                    Tipo = Avaliacao.EnumTipo.Aluno
                });
            }

            var result = await _avaliacaoService.RealizarAvaliacaoAluno(avaliacoes);

            if (result.IsValid)
                return Ok(true);

            return BadRequest(result);
        }

        [HttpGet("{alunoId:guid}/avaliacoes")]
        public async Task<IActionResult> GetAvaliacoes(Guid alunoId, [FromQuery] GridRequest request)
        {
            return Ok(await _alunoService.ObterDadosAvaliacoes(alunoId, request));
        }
    }
}


