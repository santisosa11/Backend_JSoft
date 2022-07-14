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
    public class ServiciosController : ControllerBase
    {
        private readonly ServiciosRepository _repository;

        public ServiciosController(ServiciosRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET api/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicios>>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servicios>> GetServiciosById(long id)
        {
            var response = await _repository.GetServiciosById(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        [HttpPost]
        public async Task Post([FromBody] Servicios value)
        {
            await _repository.InsertServicios(value);
        }
    }
}
