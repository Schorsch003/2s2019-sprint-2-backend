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
    [Produces("application/json")]
    [Authorize]
    public class DepartamentosController : ControllerBase {

        DepartamentoRepository departamentoRepository = new DepartamentoRepository();

        [HttpGet]
        public IEnumerable<Departamentos> Listar () {
            return departamentoRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id) {
            return Ok(departamentoRepository.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Cadastrar (Departamentos dep) {
            departamentoRepository.Cadastrar(dep);
            return Ok();
        }

    }
}