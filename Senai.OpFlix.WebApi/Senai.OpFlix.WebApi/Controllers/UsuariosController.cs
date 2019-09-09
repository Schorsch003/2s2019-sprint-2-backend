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
    [Produces("application/json")]
    [ApiController]
    public class UsuariosController : ControllerBase {

        IUsuarioRepository usuarioRepository;

        public UsuariosController () {
            usuarioRepository = new UsuarioRepository();
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IEnumerable<Usuarios> ListarUsuarios () {
            return usuarioRepository.ListarUsuarios();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult CadastrarUsuario (Usuarios user) {
            try { 
                usuarioRepository.CadastrarUsuarios(user);
                return Ok();
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult AtualizarUsuario(int id, Usuarios user) {
            try {
                if (usuarioRepository.BuscarPorId(id) == null) {
                    return NotFound();
                }
                usuarioRepository.AtualizarUsuarios(id , user);
                return Ok();
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}