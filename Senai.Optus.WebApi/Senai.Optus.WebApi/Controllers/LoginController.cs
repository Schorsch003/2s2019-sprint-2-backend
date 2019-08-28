using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Optus.WebApi.Repositories;
using Senai.Optus.WebApi.ViewModels;

namespace Senai.Optus.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase {

        UsuarioRepository usuarioRepository = new UsuarioRepository();

        [HttpPost]
        public IActionResult BuscarPorEmailESenha (LoginViewModel login) {
            try {
                var usuarioBuscado = usuarioRepository.BuscarPorEmailESenha(login);
                if (usuarioBuscado == null) {
                    return NotFound(new {message = "foi aqui no 404 - Not Found" });
                }

                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.Permissao),
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("optus-chave-autenticacao"));

                var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "Optus.WebApi" ,
                    audience: "Optus.WebApi" ,
                    claims: claims ,
                    expires: DateTime.Now.AddMinutes(30) ,
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}