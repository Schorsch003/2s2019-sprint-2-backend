using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Optus.WebApi.Domains;
using Senai.Optus.WebApi.Repositories;

namespace Senai.Optus.WebApi.Controllers {
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class EstilosController : ControllerBase {
        EstiloRepository estiloRepository = new EstiloRepository();

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Estilos> Listar () {
            return estiloRepository.Listar();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId (int id) {
            try {
                return Ok(estiloRepository.BuscarPorId(id));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}/artistas")]
        public IActionResult BuscarArtistasPorEstilo (int id) {
            return Ok(estiloRepository.BuscarArtistasPorId(id));
        }

        [AllowAnonymous]
        [HttpGet("buscar/{nome}/artistas")]
        public IActionResult BuscarArtistasPorEstilo (string nome) {
            return Ok(estiloRepository.BuscarArtistasPorNome(nome));
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public IActionResult Cadastrar (Estilos estilo) {
            estiloRepository.Cadastrar(estilo);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("quantidade")]
        public IActionResult ContadorEstilos () {
            return Ok(estiloRepository.EstilosQuantidade());
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("{id}")]
        public IActionResult Deletar (int id) {
            estiloRepository.Deletar(id);
            return Ok();
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPut()]
        public IActionResult Atualizar (Estilos estilo) {
            estiloRepository.Atualizar(estilo);
            return Ok();
        }
    }
}