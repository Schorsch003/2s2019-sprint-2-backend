using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Sstop.WebApi.Domains;
using Senai.Sstop.WebApi.Repositories;

namespace Senai.Sstop.WebApi.Controllers {
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]

    public class EstilosController : ControllerBase {

        EstiloRepository estiloRepository = new EstiloRepository();


        [HttpGet]
        public IEnumerable<EstiloDomain> Get() {

            return estiloRepository.Listar();
        }


        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id) {
            EstiloDomain estilo = estiloRepository.Listar().Find(x => x.IdEstilo == id);
            if (estilo == null) {
                return NotFound();
            }
            return Ok(estilo);
        }


        [HttpPost]
        public IActionResult Cadastrar(EstiloDomain estilo) {
            estiloRepository.Cadastrar(estilo);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id) {
            estiloRepository.Deletar(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar(EstiloDomain estilo) {
            estiloRepository.Atualizar(estilo);
            return Ok();
        }
    }
}