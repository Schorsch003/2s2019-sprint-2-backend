using System;
using System.Collections.Generic;
using System.Linq;
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
    public class LancamentosController : ControllerBase {
        ILancamentoRepository lancamentoRepository;
        IUsuarioRepository usuarioRepository;

        public LancamentosController () {
            lancamentoRepository = new LancamentoRepository();
            usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        public IEnumerable<Lancamentos> ListarLancamentos () {
            return lancamentoRepository.ListarLancamentos();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult CadastrarLancamento (Lancamentos lanc) {
            try {
                lancamentoRepository.CadastrarLancamentos(lanc);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult AtualizarLancamento (int id , Lancamentos lanc) {
            try {
                lancamentoRepository.AtualizarLancamento(id , lanc);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro: " + ex.Message });
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult RemoverLancamento (int id) {
            try {
                lancamentoRepository.RemoverLancamentos(id);
                return Ok(new { message = "Lancamento removido com sucesso!" });
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro: " + ex.Message });
            }
        }

        //[HttpGet("{idLancamento}")]
        //public IActionResult BuscarUsuariosPorLancamentoFavorito (int idLancamento) {
        //    return Ok(lancamentoRepository.BuscarUsuariosPorLancamentoFavorito(idLancamento));
        //}

        [HttpGet("fav/{idUsuario}")]
        public IEnumerable<Lancamentos> BuscarLancamentosFavoritos (int idUsuario) {
            return usuarioRepository.BuscarLancamentosFavoritos(idUsuario);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarLancamentoPorId(int id) {
            return Ok(lancamentoRepository.BuscarPorId(id));
        }

        [HttpGet("genero/{id}")]
        public IActionResult BuscarLancamentoPorGenero (int id) {
            return Ok(lancamentoRepository.BuscarPorGenero(id));
        }


        [HttpGet("data/{ano}")]
        public IActionResult FiltrarPorData (int ano) {
            try {
                return Ok(lancamentoRepository.FiltrarPorData(ano));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("plat/{plat}")]
        public IActionResult FiltrarPorPlataforma (string plat) {
            try {
                
                return Ok(lancamentoRepository.FiltrarPorPlataforma(plat));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("recentes")]
        public IEnumerable<Lancamentos> MaisRecentes () {
            return lancamentoRepository.MaisRecentes();
        }
    }
}