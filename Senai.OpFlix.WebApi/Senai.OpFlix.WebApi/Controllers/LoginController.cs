using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.OpFlix.WebApi.Repositories;
using Senai.OpFlix.WebApi.ViewModels;

namespace Senai.OpFlix.WebApi.Controllers {
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase {

        UsuarioRepository usuarioRepository = new UsuarioRepository();

        [HttpPost]
        public IActionResult Login(LoginViewModel login) {
            var user = usuarioRepository.BuscarPorEmailESenha(login);
            if (user == null) {
                return NotFound();
            }
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, user.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, user.IdPermissaoNavigation.Tipo)
                };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("opflix-chave-autenticacao"));

            var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "OpFlix.WebApi" ,
                audience: "OpFlix.WebApi" ,
                claims: claims ,
                expires: DateTime.Now.AddHours(2) ,
                signingCredentials: creds
                );
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        
    }
}