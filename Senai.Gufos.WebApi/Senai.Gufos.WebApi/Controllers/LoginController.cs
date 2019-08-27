using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.Repositories;
using Senai.Gufos.WebApi.ViewModel;

namespace Senai.Gufos.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase {
        UsuarioRepository usuarioRepository = new UsuarioRepository();

        [HttpPost]
        public IActionResult Login (LoginViewModel login) {
            try {
                Usuarios usuarioBuscado = usuarioRepository.BuscarPorEmailESenha(login);
                if (usuarioBuscado == null) {
                    return NotFound(new { mensagem = "Usuário não encontrado" });
                }
                // informacoes referentes ao usuarios
                var claims = new[] {
                    new Claim("chave", "0123456789"),
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.Permissao),
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("gufos-chave-autenticacao"));

                var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "Gufos.WebApi" ,
                    audience: "Gufos.WebApi" ,
                    claims: claims ,
                    expires: DateTime.Now.AddMinutes(30) ,
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            } catch (Exception ex) {
                return BadRequest(new { mensagem = "Erro ao cadastrar: " + ex.Message });
            }
        }
    }
}