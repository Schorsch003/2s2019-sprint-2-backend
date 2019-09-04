using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using Senai.AutoPecas.WebApi.Repositories;

namespace Senai.AutoPecas.WebApi.Controllers {
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PecasController : ControllerBase {

        public IPecaRepository pecaRepository { get; set; }


        public PecasController () {
            pecaRepository = new PecaRepository();
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<Pecas> ListarPecas () {
            var id = RecuperarIdUsuario();
            return pecaRepository.ListarPecas(id);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult BuscarPecaPorId(int id) {
            var peca = pecaRepository.BuscarPecaPorId(id);
            return Ok(peca);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CadastrarPeca(Pecas pecas) {
            try{
                var id = RecuperarIdUsuario();
                pecaRepository.CadastrarPeca(id , pecas);
                return Ok(new { message = "Peça cadastrada com sucesso" });
            }catch(Exception ex) {
                return BadRequest(new { message = "Erro ao cadastrar: " + ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult AtualizarPeca (int id, Pecas peca) {
            var idFornecedor = RecuperarIdUsuario();
            try {
                string aa;
                pecaRepository.AtualizarPeca(id , peca,idFornecedor, out aa);
                return Ok(new { message = aa});
            }catch(Exception ex) {
                return BadRequest(new { message = "Erro ao atualizar a peça: " + ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult RemoverPeca (int id) {
            var idFornecedor = RecuperarIdUsuario();
            try {
                string aa;
                pecaRepository.RemoverPeca(id,idFornecedor, out aa);
                return Ok(new { message = aa });
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro ao remover a peça: " + ex.Message });
            }
        }

        private int RecuperarIdUsuario () {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return Convert.ToInt32(identity.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
        }
    }
}