using AutoMapper;
using AvaliadorPI.API.ViewModels.Administrador;
using AvaliadorPI.Domain;
using AvaliadorPI.Domain.RootAdministrador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AvaliadorPI.API.Controllers
{
    [Authorize(Roles = "Professor, Administrador")]
    public class AdministradoresController : BaseController<Administrador>
    {
        private readonly IAdministradorService _administradorService;
        private readonly IMapper _mapper;

        public AdministradoresController(IMapper mapper, IAdministradorService administradorService)
        {
            _mapper = mapper;
            _administradorService = administradorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GridRequest request)
        {
            if (request.IsDirty)
                return Ok(await _administradorService.ObterDadosGrid(request));
            return Ok(await _administradorService.ObterDadosGrid());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _administradorService.ObterPorId(id);

            if (entity == null)
                return RegistroNaoEncontrado(id);

            return Ok(_mapper.Map<AdministradorViewModel>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AdministradorFormViewModel model)
        {
            var result = await _administradorService.Cadastrar(_mapper.Map<Administrador>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<AdministradorViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put(Guid id, [FromBody]AdministradorFormViewModel model)
        {
            return BadRequest("Não permitido alterar Administrador!");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _administradorService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _administradorService.Remover(id);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }
    }
}