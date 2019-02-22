using AutoMapper;
using AvaliadorPI.API.ViewModels.Professor;
using AvaliadorPI.Domain;
using AvaliadorPI.Domain.RootProfessor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AvaliadorPI.API.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProfessoresController : BaseController<Professor>
    {
        private readonly IProfessorService _professorService;
        private readonly IMapper _mapper;

        public ProfessoresController(IMapper mapper, IProfessorService professorService)
        {
            _mapper = mapper;
            _professorService = professorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GridRequest request)
        {
            if (request.IsDirty)
                return Ok(await _professorService.ObterDadosGrid(request));
            return Ok(await _professorService.ObterDadosGrid());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var Professor = await _professorService.ObterPorId(id);

            if (Professor == null)
                return RegistroNaoEncontrado(id);

            return Ok(_mapper.Map<ProfessorViewModel>(Professor));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProfessorFormViewModel model)
        {
            var result = await _professorService
                .Cadastrar(_mapper.Map<Professor>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<ProfessorViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProfessorFormViewModel model)
        {
            if (!await _professorService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _professorService.Editar(id, _mapper.Map<Professor>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<ProfessorViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _professorService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _professorService.Remover(id);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }
    }
}