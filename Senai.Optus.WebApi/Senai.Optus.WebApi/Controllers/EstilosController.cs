using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Optus.WebApi.Domains;
using Senai.Optus.WebApi.Repositories;

namespace Senai.Optus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EstilosController : ControllerBase {
        EstiloRepository estiloRepository = new EstiloRepository();


        [HttpGet]
        public IEnumerable<Estilos> Listar() {
            return estiloRepository.Listar();
        }

        [HttpPost]
        public IActionResult Cadastrar(Estilos estilo) {
            estiloRepository.Cadastrar(estilo);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id) {
            estiloRepository.Deletar(id);
            return Ok();
        }

        [HttpPut()]
        public IActionResult Atualizar(Estilos estilo){
            estiloRepository.Atualizar(estilo);
            return Ok();
        }
    }
}