using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
    public class UsuariosController : ControllerBase {

        IUsuarioRepository usuarioRepository;

        public UsuariosController () {
            usuarioRepository = new UsuarioRepository();
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IEnumerable<Usuarios> ListarUsuarios () {
            return usuarioRepository.ListarUsuarios();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult AtualizarUsuario(int id, Usuarios user) {
            try {
                if (usuarioRepository.BuscarPorId(id) == null) {
                    return NotFound();
                }
                usuarioRepository.AtualizarUsuarios(id , user);
                return Ok();
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult RemoverUsuario(int id) {
            try {
                usuarioRepository.RemoverUsuarios(id);
                return Ok(new { message = "Usuario removido com sucesso"});
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("fav/{idLancamento}")]
        public IActionResult FavoritarLancamento(int idLancamento) {
            try {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var idUsuario = Convert.ToInt32(identity.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
                usuarioRepository.FavoritarLancamento(idUsuario , idLancamento);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        
    }
}