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
    public class GenerosController : ControllerBase {

        GenerosRepository generosRepository = new GenerosRepository();

        [HttpGet]
        public IEnumerable<GeneroDomain> Listar() {
            return generosRepository.Listar();
        }

        [HttpPost]
        public IActionResult Cadastrar(GeneroDomain genero) {
            generosRepository.Cadastrar(genero);
            return Ok();
        }

        [HttpGet("buscar/{nome}/livros")]
        public IActionResult BuscarLivrosPorGenero(string nome){
            return Ok(generosRepository.BuscarLivrosPorGenero(nome));
        }
    }
}