using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Filmes.WebApi.Domains;
using Senai.Filmes.WebApi.Repositories;

namespace Senai.Filmes.WebApi.Controllers {
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]


    public class GenerosController : ControllerBase {

        GenerosRepository generoRepository = new GenerosRepository();


        [HttpGet]
        public IEnumerable<GeneroDomain> Get() {
            
            return generoRepository.Listar();
            }
    }
}
