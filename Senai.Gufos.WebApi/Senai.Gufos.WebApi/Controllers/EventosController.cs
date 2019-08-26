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
    [Produces("application/json")]
    [ApiController]
    public class EventosController : ControllerBase {
        EventoRepository eventoRepository = new EventoRepository();

        [HttpGet]
        public IEnumerable<Eventos> Listar() {
            return eventoRepository.Listar();
        }

        [HttpPost]
        public IActionResult Cadastrar(Eventos evento){
            try{
                eventoRepository.Cadastrar(evento);
                return Ok();
            }catch(Exception ex){
                return BadRequest("Ocorreu um erro ao cadastrar: " + ex.Message);
            }
        }

    }
}