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
    public class ServidoresController : ControllerBase
    {
        private readonly ServidoresRepository _repository;

        public ServidoresController(ServidoresRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servidores>>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpPost]
        public async Task Post([FromBody] Servidores value)
        {
            await _repository.InsertServidores(value);
        }

        [HttpPut]
        public async Task Put([FromBody] Servidores value, long id)
        {
            await _repository.UpdateServidores(value, id);
        }
    }
}
