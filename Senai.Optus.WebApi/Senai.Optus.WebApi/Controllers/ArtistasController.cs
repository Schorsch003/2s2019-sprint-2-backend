using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Optus.WebApi.Domains;
using Senai.Optus.WebApi.Repositories;

namespace Senai.Optus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]


    public class ArtistasController : ControllerBase {

        ArtistaRepository artistaRepository = new ArtistaRepository();
        
        [HttpGet]
        public IEnumerable<Artistas> Listar(){
            return artistaRepository.Listar();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Cadastrar(Artistas artista) {
            try {
                artistaRepository.Cadastrar(artista);
                return Ok();
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}