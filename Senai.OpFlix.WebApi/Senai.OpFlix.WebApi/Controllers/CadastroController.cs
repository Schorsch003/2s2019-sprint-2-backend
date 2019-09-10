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
    [ApiController]
    public class CadastroController : ControllerBase {
        IUsuarioRepository usuarioRepository;

        public CadastroController () {
            usuarioRepository = new UsuarioRepository();
        }
        [HttpPost]
        public IActionResult Cadastrar (Usuarios user) {
            //try {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity.Claims.Count() != 0) {

                var id = Convert.ToInt32(identity.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
                var usuario = usuarioRepository.BuscarPorId(id);
                if (usuario != null) {
                    if (usuario.IdPermissaoNavigation.Tipo == "Administrador") {
                        usuarioRepository.CadastrarUsuarios(user , true);
                    } else {
                        usuarioRepository.CadastrarUsuarios(user , false);
                    }
                    return Ok();
                }
            }
            usuarioRepository.CadastrarUsuarios(user , false);

            return Ok();
            //} catch (Exception ex) {
            //   return BadRequest(new { Message = "Erro: " + ex.Message });
            //}
        }
    }
}