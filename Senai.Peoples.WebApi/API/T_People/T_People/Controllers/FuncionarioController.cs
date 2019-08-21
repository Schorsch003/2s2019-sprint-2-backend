using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using T_People.Domains;
using T_People.Repositories;

namespace T_People.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase {

        FuncionarioRepository funcionarioRepository = new FuncionarioRepository();

        [HttpGet]
        public IEnumerable<FuncionarioDomain> Listar() {

            return funcionarioRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id) {
            var funcionario = funcionarioRepository.BuscarPorId(id);
            return Ok(funcionario);
        }

        [HttpPost]
        public IActionResult Cadastrar(FuncionarioDomain funcionario) {
            funcionarioRepository.Cadastrar(funcionario);
            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar(FuncionarioDomain funcionario) {
            funcionarioRepository.Atualizar(funcionario);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id) {
            funcionarioRepository.Deletar(funcionarioRepository.BuscarPorId(id));
            return Ok();
        }
    }
}