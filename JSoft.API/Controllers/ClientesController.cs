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
    public class ClientesController : ControllerBase
    {
        private readonly ClientesRepository _repository;

        public ClientesController(ClientesRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpPost]
        public async Task Post([FromBody] Clientes value)
        {
            await _repository.InsertClientes(value);
        }

        [HttpPut]
        public async Task Put([FromBody] Clientes value, long id)
        {
            await _repository.UpdateClientes(value, id);
        }
    }
}
