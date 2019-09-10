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

    public class PlataformasController : ControllerBase {

        IPlataformaRepository plataformaRepository;

        public PlataformasController () {
            plataformaRepository = new PlataformaRepository();
        }

        [HttpGet]
        public IEnumerable<Plataformas> ListarPlataformas () {
            return plataformaRepository.ListarPlataformas();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult CadastrarPlataforma (Plataformas plat) {
            try {
                plataformaRepository.CadastrarPlataforma(plat);
                return Ok();
            }catch(Exception ex) {
                return BadRequest(new { message = "Erro" + ex.Message });
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult AtualizarPlataforma(int id, Plataformas plat) {
            try {
                plataformaRepository.AtualizarPlataforma(id , plat);
                return Ok();
            } catch(Exception ex) {
                return BadRequest(new { message = "Erro: " + ex.Message });
            }
        }


        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult RemoverPlataforma(int id) {
            try {
                plataformaRepository.RemoverPlataforma(id);
                return Ok();
            } catch(Exception ex) {
                return BadRequest(new { Message = "Erro: " + ex.Message });
            }
        }

    }
}