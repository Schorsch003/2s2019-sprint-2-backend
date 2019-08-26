using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.Repositories;

namespace Senai.Gufos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class CategoriasController : ControllerBase {

        CategoriaRepository categoriasRepository = new CategoriaRepository();


        /// <summary>
        /// Listar todas as categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Categorias> Listar(){
            return categoriasRepository.Listar();
        }

        [HttpPost]
        public IActionResult Cadastrar(Categorias cat) {
            try{
                categoriasRepository.Cadastrar(cat);
                return Ok();
            }catch(Exception ex){
                return BadRequest("Deu erro mano ;-;. O erro foi: " + ex.Message);   
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id) {
            Categorias cat = categoriasRepository.BuscarPorId(id);
            if (cat == null)
                return NotFound();
            return Ok(cat);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id) {
            try {
                categoriasRepository.Deletar(id);
                return Ok();
            }catch(Exception ex){
                return BadRequest("Deu erro mano ;-;. O erro foi: " + ex.Message);
            }
        }


    }
}