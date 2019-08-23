using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Filmes.WebApi.Domains;
using Senai.Filmes.WebApi.Repositories;

namespace Senai.Filmes.WebApi.Controllers {
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]


    public class GenerosController : ControllerBase {

        GenerosRepository generoRepository = new GenerosRepository();


        [HttpGet]
        public IEnumerable<GeneroDomain> ListarTodos() {

            return generoRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id) {
            GeneroDomain genero = generoRepository.BuscarPorId(id);
            if(genero == null) {
                return NotFound();
            }
            return Ok(genero);
        }

        [HttpPost]
        public IActionResult Cadastrar(GeneroDomain genero) {
            generoRepository.Cadastrar(genero);
            return Ok();

        }

        [HttpPut]
        public IActionResult Atualizar(GeneroDomain genero) {
            generoRepository.Atualizar(genero);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Deletar(GeneroDomain genero) {
            generoRepository.Deletar(genero);
            return Ok();
        }

        [HttpGet("{id}/filmes")]
        public IEnumerable<FilmesDomain> FilmesPorGenero(int id) {
            return generoRepository.FilmesPorGenero(id);
        }
    }
}
