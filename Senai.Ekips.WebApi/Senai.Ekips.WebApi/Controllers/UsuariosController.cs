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
    public class UsuariosController : ControllerBase {

        UsuarioRepository usuarioRepository = new UsuarioRepository();

        [Authorize(Roles = "COMUM")]
        [HttpGet]
        public IEnumerable<Usuarios> Listar () {
            return usuarioRepository.Listar();
        }


    }
}