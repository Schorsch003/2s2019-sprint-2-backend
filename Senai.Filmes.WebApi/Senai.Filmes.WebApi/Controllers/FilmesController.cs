using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Senai.Filmes.WebApi.Domains;
using Senai.Filmes.WebApi.Repositories;

namespace Senai.Filmes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase {

        FilmesRepository filmesRepository = new FilmesRepository();

        [HttpGet]
        public IActionResult Listar() {
            return Ok(filmesRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id) {
            return Ok(filmesRepository.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Cadastrar(FilmesDomain filme) {
            try {
                filmesRepository.Cadastrar(filme);
            }catch(Exception e) {
                return BadRequest(new {mensagem ="Erro: " + e.Message });
            }
            return Ok();
        }
    }
}