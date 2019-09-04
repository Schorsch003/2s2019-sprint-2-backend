using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using Senai.AutoPecas.WebApi.Repositories;
using Senai.AutoPecas.WebApi.ViewModels;

namespace Senai.AutoPecas.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase {
        public IUsuarioRepository usuarioRepository { get; set; }

        public LoginController () {
            usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login (LoginViewModel login) {
            var user = usuarioRepository.BuscarPorEmailESenha(login);
            if(user == null) {
                return NotFound();
            }
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, user.IdUsuario.ToString())
                };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("autopecas-chave-autenticacao"));

            var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "AutoPecas.WebApi" ,
                audience: "AutoPecas.WebApi" ,
                claims: claims ,
                expires: DateTime.Now.AddHours(2) ,
                signingCredentials: creds
                );
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}