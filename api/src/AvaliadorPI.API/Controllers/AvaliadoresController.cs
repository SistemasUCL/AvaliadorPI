using AutoMapper;
using AvaliadorPI.API.ViewModels.Avaliador;
using AvaliadorPI.Domain;
using AvaliadorPI.Domain.RootAvaliador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AvaliadorPI.API.Controllers
{
    [Authorize(Roles = "Professor, Administrador")]
    public class AvaliadoresController : BaseController<Avaliador>
    {
        private readonly IAvaliadorService _avaliadorService;
        private readonly IMapper _mapper;

        public AvaliadoresController(IMapper mapper, IAvaliadorService avaliadorService)
        {
            _mapper = mapper;
            _avaliadorService = avaliadorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GridRequest request)
        {
            if (request.IsDirty)
                return Ok(await _avaliadorService.ObterDadosGrid(request));
            return Ok(await _avaliadorService.ObterDadosGrid());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _avaliadorService.ObterPorId(id);

            if (entity == null)
                return RegistroNaoEncontrado(id);

            return Ok(_mapper.Map<AvaliadorViewModel>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AvaliadorFormViewModel model)
        {
            var result = await _avaliadorService.Cadastrar(_mapper.Map<Avaliador>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<AvaliadorViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put(Guid id, [FromBody]AvaliadorFormViewModel model)
        {
            return BadRequest("Não permitido alterar avaliador!");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _avaliadorService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _avaliadorService.Remover(id);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }
    }
}