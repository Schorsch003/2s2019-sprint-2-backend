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
    [Produces("application/json")]
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


        [HttpGet("buscar/{nome}")]
        public IActionResult BuscarPorNome(string nome) {
            var funcionario = funcionarioRepository.BuscarPorNome(nome);
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
        
        // /api/funcionarios/1
        [HttpGet("nomescompletos")]
        public IEnumerable<FuncionarioDomain> NomesCompletos() {
            return funcionarioRepository.NomesCompletos();
        }

        [HttpGet("ordenacao/{ordem}")]
        public IActionResult Ordenacao(string ordem) {
            bool foi;
            var lista = funcionarioRepository.ListarPorOrder(ordem, out foi);
            if (foi) {
                return Ok(lista);
            } else {
                return BadRequest();
            }
        }
    }
}