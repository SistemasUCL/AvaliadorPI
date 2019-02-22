using AutoMapper;
using AvaliadorPI.API.ViewModels.Projeto;
using AvaliadorPI.Domain;
using AvaliadorPI.Domain.RootAvaliacao;
using AvaliadorPI.Domain.RootCriterio;
using AvaliadorPI.Domain.RootProjeto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliadorPI.API.Controllers
{
    [Authorize(Roles = "Professor, Administrador")]
    public class ProjetosController : BaseController<Projeto>
    {
        private readonly IProjetoService _projetoService;
        private readonly ICriterioService _criterioService;
        private readonly IMapper _mapper;

        public ProjetosController(IMapper mapper, IProjetoService projetoService, ICriterioService criterioService)
        {
            _mapper = mapper;
            _projetoService = projetoService;
            _criterioService = criterioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GridRequest request)
        {
            if (request.IsDirty)
                return Ok(await _projetoService.ObterDadosGrid(request));
            return Ok(await _projetoService.ObterDadosGrid());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _projetoService.ObterPorId(id);

            if (entity == null)
                return RegistroNaoEncontrado(id);

            return Ok(_mapper.Map<ProjetoViewModel>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjetoFormViewModel model)
        {
            var result = await _projetoService.Cadastrar(_mapper.Map<Projeto>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<ProjetoViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProjetoFormViewModel model)
        {
            if (!await _projetoService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _projetoService.Editar(id, _mapper.Map<Projeto>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<ProjetoViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _projetoService.Existe(id))
                return RegistroNaoEncontrado(id);

            var result = await _projetoService.Remover(id);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }

        [HttpPost("{projetoId:guid}/criterios")]
        public async Task<IActionResult> PostCriterios(Guid projetoId, [FromBody] CriterioFormViewModel model)
        {
            var projeto = await _projetoService.ObterPorId(projetoId);

            if (projeto == null)
                RegistroNaoEncontrado(projetoId);

            model.ProjetoId = projeto.Id;

            var result = await _criterioService.Cadastrar(_mapper.Map<Criterio>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<CriterioViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpPut("{projetoId:guid}/criterios/{id:guid}")]
        public async Task<IActionResult> PutCriterios(Guid projetoId, Guid id, [FromBody] CriterioFormViewModel model)
        {
            if (!await _projetoService.Existe(projetoId))
                RegistroNaoEncontrado(projetoId);

            var entity = await _criterioService.ObterPorId(id);

            if (entity == null)
                RegistroNaoEncontrado(id);

            var result = await _criterioService.Editar(id, _mapper.Map<Criterio>(model));

            if (result.IsValid)
                return Ok(_mapper.Map<CriterioViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpGet("{projetoId:guid}/criterios")]
        public async Task<IActionResult> GetCriterios(Guid projetoId, [FromQuery] GridRequest request)
        {
            return Ok(await _criterioService.ObterTodosPorProjeto(projetoId, request));
        }
        [HttpGet("{projetoId:guid}/avaliadores")]
        public async Task<IActionResult> GetAvaliadores(Guid projetoId, [FromQuery] GridRequest request)
        {
            return Ok(await _projetoService.ObterDadosGridAvaliadores(projetoId, request));
        }
        [HttpGet("{projetoId:guid}/grupos")]
        public async Task<IActionResult> GetGrupos(Guid projetoId, [FromQuery] GridRequest request)
        {
            return Ok(await _projetoService.ObterDadosGridGrupos(projetoId, request));
        }
        [HttpGet("{projetoId:guid}/avaliacoes")]
        public async Task<IActionResult> GetAvaliacoes(Guid projetoId, [FromQuery] GridRequest request)
        {
            return Ok(await _projetoService.ObterDadosAvaliacoes(projetoId, request));
        }

        [HttpGet("{projetoId:guid}/excel")]
        [AllowAnonymous]
        public async Task<IActionResult> GetExcel(Guid projetoId)
        {
            var result = await _projetoService.ObterDadosAvaliacoes(projetoId);

            if (result.Total == 0)
                return BadRequest();

            DataTable table = new DataTable("Tabela");
            table.Columns.Add("Grupo");
            table.Columns.Add("Aluno");

            bool first = true;
            foreach (var row in result.Data)
            {
                var aux = (ResultadoAvaliacaoProjeto)row;

                if (first)
                {
                    for (int i = 1; i <= row.Notas.Count; i++)
                    {
                        table.Columns.Add("Avaliação " + i).DataType = typeof(double);
                    }

                    table.Columns.Add("Média Final").DataType = typeof(double);

                    first = false;
                }

                table.Rows.Add(new object[] { aux.Grupo, aux.Aluno }.Concat(aux.Notas.Cast<object>()).Concat(new object[] { aux.MediaFinal }).ToArray());
            }

            OfficeOpenXml.ExcelPackage excel = new OfficeOpenXml.ExcelPackage();
            var sheet = excel.Workbook.Worksheets.Add("Plan1");
            sheet.Cells["A1"].LoadFromDataTable(table, true, OfficeOpenXml.Table.TableStyles.Medium11);
            sheet.Cells.AutoFitColumns();

            var projeto = await _projetoService.ObterPorId(projetoId);

            string filename = projeto.Tema + ".xlsx";

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(filename, out contentType);

            return File(excel.GetAsByteArray(), contentType ?? "application/octet-stream", filename);
        }

        [HttpPost("{projetoId:guid}/avaliadores")]
        public async Task<IActionResult> AssociarAvaliadores(Guid projetoId, [FromBody] AssociacaoProjetoAvaliadorViewModel model)
        {
            var projeto = await _projetoService.ObterPorId(projetoId);

            if (projeto == null)
                RegistroNaoEncontrado(projetoId);

            var result = await _projetoService.AssociarAvaliador(projetoId, model.AvaliadorId);

            if (result.IsValid)
                return Ok(_mapper.Map<ProjetoViewModel>(result.Entity));

            return BadRequest(result.Result);
        }

        [HttpDelete("{projetoId:guid}/criterios/{id:guid}")]
        public async Task<IActionResult> DeleteCriterios(Guid projetoId, Guid id)
        {
            if (!await _projetoService.Existe(projetoId))
                return RegistroNaoEncontrado(projetoId);

            var result = await _criterioService.Remove(id);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }

        [HttpPost("{projetoId:guid}/estado-projeto")]
        public async Task<IActionResult> SetEstado(Guid projetoId, [FromBody] ProjetoFormViewModel model)
        {
            if (!await _projetoService.Existe(projetoId))
                return RegistroNaoEncontrado(projetoId);

            await _projetoService.AtualizarEstado(projetoId, (int)model.Estado);

            return Ok();
        }

        [HttpDelete("{projetoId:guid}/avaliadores/{id:guid}")]
        public async Task<IActionResult> DeleteAvaliadores(Guid projetoId, Guid id)
        {
            if (!await _projetoService.Existe(projetoId))
                return RegistroNaoEncontrado(projetoId);

            var result = await _projetoService.RemoverAvaliador(projetoId, id);

            if (result.IsValid)
                return Ok();

            return BadRequest(result.Result);
        }
    }
}