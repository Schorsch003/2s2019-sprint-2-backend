using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Ekips.WebApi.Domains;
using Senai.Ekips.WebApi.Repositories;
using Senai.Ekips.WebApi.ViewModels;

namespace Senai.Ekips.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {

        UsuarioRepository usuarioRepository = new UsuarioRepository();

        [HttpPost]
        public IActionResult Login(LoginViewModel login) {
            //try {
                Usuarios usuarioBuscado = usuarioRepository.BuscarPorEmailESenha(login);
                if (usuarioBuscado == null) {
                    return NotFound(new { mensagem = "Usuário não encontrado" });
                }
            // informacoes referentes ao usuarios
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.IdPermissaoNavigation.Nome),
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ekips-chave-autenticacao"));

                var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "Ekips.WebApi" ,
                    audience: "Ekips.WebApi" ,
                    claims: claims ,
                    expires: DateTime.Now.AddMinutes(30) ,
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            //} catch (Exception ex) {
            //    return BadRequest(new { mensagem = "Erro ao cadastrar: " + ex.Message });
            //}
        }
    }
}