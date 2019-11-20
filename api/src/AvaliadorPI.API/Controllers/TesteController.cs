using AvaliadorPI.Domain.RootProjeto;
using Microsoft.AspNetCore.Mvc;

namespace AvaliadorPI.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Teste")]
    public class TesteController : Controller
    {
        private readonly IProjetoService _projetoService;

        public TesteController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        // GET: api/Teste
        [HttpGet]
        public IActionResult Get()
        {
            var x = _projetoService.ObterDadosGrid(null).Result;
            return Ok(x);
        }

        // GET: api/Teste/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Teste
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Teste/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
