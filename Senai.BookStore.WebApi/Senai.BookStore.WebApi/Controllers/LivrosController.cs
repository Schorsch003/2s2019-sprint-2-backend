using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.BookStore.WebApi.Domains;
using Senai.BookStore.WebApi.Repositories;

namespace Senai.BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase {
        LivrosRepository livrosRepository = new LivrosRepository();

        [HttpGet]
        public IEnumerable<LivroDomain> Listar() {
            return livrosRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id) {
            return Ok(livrosRepository.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Cadastrar(LivroDomain livro) {
            livrosRepository.Cadastrar(livro);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(LivroDomain livro, int id){
            livrosRepository.Atualizar(livro, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id){
            livrosRepository.Deletar(id);
            return Ok();
        }

        

    }
}