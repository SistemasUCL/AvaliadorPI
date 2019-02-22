using AutoMapper;
using AvaliadorPI.API.ViewModels.Shared;
using AvaliadorPI.Domain;
using AvaliadorPI.Domain.RootUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AvaliadorPI.API.Controllers
{
    [Authorize(Roles = "Professor, Administrador")]
    public class UsuariosController : BaseController<Usuario>
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IMapper mapper, IUsuarioService usuarioService)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GridRequest request)
        {
            if (request.IsDirty)
                return Ok(await _usuarioService.ObterDadosGrid(request));
            return Ok(await _usuarioService.ObterDadosGrid());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _usuarioService.ObterPorId(id);

            if (entity == null)
                return RegistroNaoEncontrado(id);

            return Ok(_mapper.Map<UsuarioViewModel>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UsuarioViewModel model)
        {
            var result = await _usuarioService.Cadastrar(_mapper.Map<Usuario>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<UsuarioViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]UsuarioViewModel model)
        {
            if (!await _usuarioService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _usuarioService.Editar(id, _mapper.Map<Usuario>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<UsuarioViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _usuarioService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _usuarioService.Remover(id);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }
    }
}