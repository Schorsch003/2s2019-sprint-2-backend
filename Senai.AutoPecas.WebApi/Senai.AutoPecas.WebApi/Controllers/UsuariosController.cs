using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using Senai.AutoPecas.WebApi.Repositories;

namespace Senai.AutoPecas.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase {

        public IUsuarioRepository usuarioRepository { get; set; }
        
        public UsuariosController () {
            usuarioRepository = new UsuarioRepository(); 
        }

        [HttpGet]
        public IEnumerable<Usuarios> Listar () {
            return usuarioRepository.ListarUsuarios();
        }
    }
}