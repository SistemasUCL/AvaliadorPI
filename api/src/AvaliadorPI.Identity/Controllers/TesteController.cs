using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AvaliadorPI.Identity.Controllers
{
    [Produces("application/json")]
    [Route("api/Teste")]
    public class TesteController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public TesteController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/Teste
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _userManager.Users.Select(x => x.UserName);

            return new string[] { "value1", "value2", };
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
