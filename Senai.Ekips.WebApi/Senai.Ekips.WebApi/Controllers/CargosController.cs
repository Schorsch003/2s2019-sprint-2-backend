using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Ekips.WebApi.Domains;
using Senai.Ekips.WebApi.Repositories;

namespace Senai.Ekips.WebApi.Controllers {
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class CargosController : ControllerBase {

        CargosRepository cargosRepository = new CargosRepository();

        [HttpGet]
        public IEnumerable<Cargos> Listar () {
            return cargosRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId (int id) {
            try {
                var cargo = cargosRepository.BuscarPorId(id);
                if (cargo == null) {
                    return NotFound(new { mensagem = "Cargo não encontrado" });
                }
                return Ok(cargo);
            } catch (Exception ex) {
                return BadRequest(new { mensagem = "Erro: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Cadastrar (Cargos cargo) {
            try {
                cargosRepository.Cadastrar(cargo);
                return Ok(cargo);
            } catch (Exception ex) {
                return BadRequest("Erro: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Cargos cargos) {
            cargosRepository.Atualizar(id,cargos);
            return Ok();
        }


    }
}