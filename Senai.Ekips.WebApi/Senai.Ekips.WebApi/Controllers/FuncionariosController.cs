using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
    public class FuncionariosController : ControllerBase {

        FuncionarioRepository funcionarioRepository = new FuncionarioRepository();

        [Authorize]
        [HttpGet]
        public IActionResult Listar () {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string permissao = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            int id = Convert.ToInt32(identity.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

            if (permissao == "ADMINISTRADOR") {
                return Ok(funcionarioRepository.Listar());
            } else if (permissao == "COMUM") {
                return Ok(funcionarioRepository.BuscarPorId(id));
            } else
                return NotFound();
        }


        [HttpPost]
        public IActionResult Cadastrar(Funcionarios funcionario) {
            funcionarioRepository.Cadastrar(funcionario);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Funcionarios funcionario) {
            funcionarioRepository.Atualizar(id , funcionario);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Deletar(Funcionarios funcionario) {
            funcionarioRepository.Remover(funcionario);
            return Ok();
        }



    }
}