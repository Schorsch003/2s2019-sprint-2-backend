using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Ekips.WebApi.Domains;
using Senai.Ekips.WebApi.Repositories;

namespace Senai.Ekips.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase {

        FuncionarioRepository funcionarioRepository = new FuncionarioRepository();

        [Authorize(Roles = "COMUM")]
        [HttpGet]
        public IEnumerable<Funcionarios> Listar () {
            return funcionarioRepository.Listar();
        }

        //[Authorize(Roles = "COMUM")]
      //  [HttpGet]
    //    public IActionResult ListarComum () {
  //          return Ok(funcionarioRepository.Listar());
//        }

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