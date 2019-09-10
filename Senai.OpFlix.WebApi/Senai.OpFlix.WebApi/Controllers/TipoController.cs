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
    public class TipoController : ControllerBase {

        ITipoRepository tipoRepository;

        public TipoController () {
            tipoRepository = new TipoRepository();
        }

        [HttpGet]
        public IEnumerable<Tipo> ListarTipos () {
            return tipoRepository.ListarTipos();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult CadastrarTipo (Tipo tipo) {
            try {
                tipoRepository.CadastrarTipo(tipo);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { Message = "Erro: " + ex.Message });
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult AtualizarTipo (int id , Tipo tipo) {
            try {
                tipoRepository.AtualizarTipo(id , tipo);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { Message = "Erro: " + ex.Message });
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult RemoverTipo (int id) {
            try {
                tipoRepository.RemoverTipo(id);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { Message = "Erro: " + ex.Message });
            }
        }

    }
}