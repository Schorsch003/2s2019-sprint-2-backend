using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using Senai.OpFlix.WebApi.Repositories;

namespace Senai.OpFlix.WebApi.Controllers {
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriasController : ControllerBase {
        ICategoriaRepository categoriaRepository;

        public CategoriasController () {
            categoriaRepository = new CategoriaRepository();
        }

        [HttpGet]
        public IEnumerable<Categoria> ListarCategorias () {
            return categoriaRepository.ListarCategorias();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult CadastrarCategoria (Categoria cat) {
            try {
                categoriaRepository.CadastrarCategoria(cat);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}