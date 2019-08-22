using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Sstop.WebApi.Domains;
using Senai.Sstop.WebApi.Repositories;

namespace Senai.Sstop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ArtistasController : ControllerBase{

        // /api/artistas => retornar a lista de artistas

        ArtistaRepository artistaRepository = new ArtistaRepository();

        [HttpGet]
        public IActionResult Listar() {
            return Ok(artistaRepository.Listar());
        }

        [HttpPost]
        public IActionResult Cadastrar(ArtistaDomain artista) {
            try {
                artistaRepository.Cadastrar(artista);
            } catch(Exception Ex) {
                return BadRequest(new { mensagem = "Deu ruim, foi o " + Ex.Message });
            }
            return Ok();
        }
    }
}