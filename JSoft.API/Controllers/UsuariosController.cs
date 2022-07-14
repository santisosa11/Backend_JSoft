using JSoft.Core.Model;
using JSoft.Infraestructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosRepository _repository;

        public UsuariosController(UsuariosRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpPost]
        public async Task Post([FromBody] Usuarios value)
        {
            await _repository.InsertUsuarios(value);
        }

        [HttpPut]
        public async Task Put([FromBody] Usuarios value, long id)
        {
            await _repository.UpdateUsuarios(value, id);
        }
    }
}
