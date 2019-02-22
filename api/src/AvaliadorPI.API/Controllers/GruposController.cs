using AutoMapper;
using AvaliadorPI.API.ViewModels.Grupo;
using AvaliadorPI.Domain;
using AvaliadorPI.Domain.RootAvaliacao;
using AvaliadorPI.Domain.RootGrupo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliadorPI.API.Controllers
{
    [Authorize(Roles = "Professor, Administrador, Avaliador")]
    public class GruposController : BaseController<Grupo>
    {
        private readonly IGrupoService _grupoService;
        private readonly IAvaliacaoService _avaliacaoService;
        private readonly IMapper _mapper;

        public GruposController(IMapper mapper, IGrupoService grupoService, IAvaliacaoService avaliacaoService)
        {
            _mapper = mapper;
            _grupoService = grupoService;
            _avaliacaoService = avaliacaoService;
        }

        [HttpGet]
        [Authorize(Roles = "Professor, Administrador")]
        public async Task<IActionResult> Get([FromQuery]GridRequest request)
        {
            if (request.IsDirty)
                return Ok(await _grupoService.ObterDadosGrid(request));
            return Ok(await _grupoService.ObterDadosGrid());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _grupoService.ObterPorId(id);

            if (entity == null)
                return RegistroNaoEncontrado(id);

            return Ok(_mapper.Map<GrupoViewModel>(entity));
        }

        [HttpGet("{id:guid}/QRCode")]
        [AllowAnonymous]
        public async Task<IActionResult> QRCode(Guid id)
        {
            var entity = await _grupoService.ObterPorId(id);

            if (entity == null)
                return RegistroNaoEncontrado(id);

            var qr = Util.GenerateQRCode(entity.Id);

            return File(qr, "application/octet-stream", entity.Nome + ".png");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GrupoFormViewModel model)
        {
            var entity = _mapper.Map<Grupo>(model);

            var result = await _grupoService.Cadastrar(entity);

            if (result.IsValid)
                return Ok(_mapper.Map<GrupoViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]GrupoFormViewModel model)
        {
            if (!await _grupoService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _grupoService.Editar(id, _mapper.Map<Grupo>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<GrupoViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _grupoService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _grupoService.Remover(id);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }

        [HttpDelete("{grupoId:guid}/alunos/{alunoId:guid}")]
        public async Task<IActionResult> Delete(Guid grupoId, Guid alunoId)
        {
            if (!await _grupoService.Existe(grupoId))
                return RegistroNaoEncontrado(grupoId);

            var result = await _grupoService.RemoverAluno(grupoId, alunoId);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }

        [HttpPost("{grupoId:guid}/alunos")]
        public async Task<IActionResult> PostAlunos(Guid grupoId, [FromBody]AssociacaoAlunoGrupoViewModel model)
        {
            var grupo = await _grupoService.ObterPorId(grupoId);

            if (grupo == null)
                RegistroNaoEncontrado(grupoId);

            var result = await _grupoService.AssociarAluno(grupoId, model.AlunoId);

            if (result.IsValid)
                return Ok(_mapper.Map<GrupoViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpGet("{grupoId:guid}/alunos")]
        public async Task<IActionResult> GetAlunos(Guid grupoId, [FromQuery]GridRequest request)
        {
            return Ok(await _grupoService.ObterDadosGridAlunos(grupoId, request));
        }

        [HttpGet("{grupoId:guid}/avaliacoes")]
        public async Task<IActionResult> GetAvaliacoes(Guid grupoId, [FromQuery] GridRequest request)
        {
            return Ok(await _grupoService.ObterDadosAvaliacoes(grupoId, request));
        }

        [Authorize(Roles = "Professor, Administrador, Avaliador")]
        [HttpGet("{grupoId:guid}/avaliacao")]
        public async Task<IActionResult> GetAvaliacao(Guid grupoId)
        {
            var result = await _grupoService.ValidarAvaliador(grupoId, User.GetUserId());

            if (result.IsValid)
                return Ok(_mapper.Map<AvaliacaoGrupoViewModel>(await _grupoService.ObterAvaliacaoParaGrupo(grupoId)));

            return BadRequest(result);
        }

        [Authorize(Roles = "Professor, Administrador, Avaliador")]
        [HttpPost("{grupoId:guid}/avaliacao")]
        public async Task<IActionResult> PostAvaliacao(Guid grupoId, [FromBody]ResultadoAvaliacaoViewModel resultado)
        {
            var data = DateTime.Now;

            List<Avaliacao> avaliacoes = new List<Avaliacao>();

            foreach (var c in resultado.Criterios)
            {
                var avaliacaoGrupo = c.Avaliacoes.FirstOrDefault(x => x.AvaliadoId == grupoId);

                foreach (var a in c.Avaliacoes.Where(x => x.AvaliadoId != avaliacaoGrupo.AvaliadoId))
                {
                    if (a.Nota == 0)
                        avaliacoes.Add(new Avaliacao
                        {
                            AvaliadorId = User.GetUserId(),
                            AlunoId = a.AvaliadoId,
                            GrupoId = resultado.GrupoId,
                            CriterioId = c.Id,
                            Data = data,
                            Nota = avaliacaoGrupo.Nota * 2,
                            Tipo = Avaliacao.EnumTipo.Aluno

                        });
                    else
                        avaliacoes.Add(new Avaliacao
                        {
                            AvaliadorId = User.GetUserId(),
                            AlunoId = a.AvaliadoId,
                            GrupoId = resultado.GrupoId,
                            CriterioId = c.Id,
                            Data = data,
                            Nota = a.Nota * 2,
                            Tipo = Avaliacao.EnumTipo.Grupo
                        });
                }
            }

            var result = await _avaliacaoService.RealizarAvaliacao(grupoId, avaliacoes);

            if (result.IsValid)
                return Ok(true);

            return BadRequest(result);
        }
    }
}