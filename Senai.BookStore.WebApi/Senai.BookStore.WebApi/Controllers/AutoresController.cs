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
    public class AutoresController : ControllerBase {

        AutoresRepository autorRepository = new AutoresRepository();

        [HttpGet]
        public IEnumerable<AutorDomain> Listar() {
            return autorRepository.Listar();
        }

        [HttpPost]
        public IActionResult Cadastrar(AutorDomain autor) {
            return Ok(autor);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id) {
            return Ok(autorRepository.BuscarPorId(id));
        }

        [HttpGet("{id}/livros")]
        public IEnumerable<LivroDomain> ListarLivrosPorArtista(int id){
            return autorRepository.BuscarLivroPorAutor(id);
        }

        [HttpGet("ativos")]
        public IActionResult BuscarAutoresAtivos(){
            if(autorRepository.BuscarAutoresAtivos() == null)
            {
            return NotFound();
            }
                return Ok(autorRepository.BuscarAutoresAtivos());
        }

        [HttpGet("{id}/ativo/livros")]
        public IActionResult LivrosAutoresAtivos(int id){
            return Ok(autorRepository.LivrosAutorAtivo(id));
        }

        [HttpGet("{ano}/nascimento")]
        public IActionResult AutoresNascidosEm(int ano)
        {
            return Ok(autorRepository.AutoresNascidosEm(ano));
        }


    }
}