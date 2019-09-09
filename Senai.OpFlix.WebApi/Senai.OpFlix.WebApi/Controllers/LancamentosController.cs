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
    [ApiController]
    public class LancamentosController : ControllerBase {
        ILancamentoRepository lancamentoRepository;

        public LancamentosController () {
            lancamentoRepository = new LancamentoRepository();
        }

        [HttpGet]
        public IEnumerable<Lancamentos> ListarLancamentos () {
            return lancamentoRepository.ListarLancamentos();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult CadastrarLancamento (Lancamentos lanc) {
            try { 
                lancamentoRepository.CadastrarLancamentos(lanc);
                return Ok();
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}