using AutoMapper;
using AvaliadorPI.API.ViewModels.Disciplina;
using AvaliadorPI.Domain;
using AvaliadorPI.Domain.RootDisciplina;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AvaliadorPI.API.Controllers
{
    [Authorize(Roles = "Professor, Administrador")]
    public class DisciplinasController : BaseController<Disciplina>
    {
        private readonly IDisciplinaService _disciplinaService;
        private readonly IMapper _mapper;

        public DisciplinasController(IMapper mapper, IDisciplinaService disciplinaService)
        {
            _mapper = mapper;
            _disciplinaService = disciplinaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GridRequest request)
        {
            if (request.IsDirty)
                return Ok(await _disciplinaService.ObterDadosGrid(request));
            return Ok(await _disciplinaService.ObterDadosGrid());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _disciplinaService.ObterPorId(id);

            if (entity == null)
                return RegistroNaoEncontrado(id);

            return Ok(_mapper.Map<DisciplinaViewModel>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DisciplinaFormViewModel model)
        {
            var result = await _disciplinaService
                .Cadastrar(_mapper.Map<Disciplina>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<DisciplinaViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]DisciplinaFormViewModel model)
        {
            if (!await _disciplinaService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _disciplinaService.Editar(id, _mapper.Map<Disciplina>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<DisciplinaViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _disciplinaService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _disciplinaService.Remover(id);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }
    }
}