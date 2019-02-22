using AvaliadorPI.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AvaliadorPI.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public abstract class BaseController<TEntity> : Controller where TEntity : Entity<TEntity>, new()
    {
        public NotFoundObjectResult RegistroNaoEncontrado(Guid id)
        {
            return RegistroNaoEncontrado<TEntity>(id);
        }

        public NotFoundObjectResult RegistroNaoEncontrado<T>(Guid id)
        {
            return base.NotFound($"Não foi encontrado nenhum registro de '{typeof(T).Name}' com id: {id}");
        }
    }
}