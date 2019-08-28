using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Optus.WebApi.Repositories;

namespace Senai.Optus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase {

        AdminRepository adminRepository = new AdminRepository();
        EstiloRepository EstiloRepository = new EstiloRepository();

        [HttpGet("dashboard")]
        public IActionResult Bagulhete () {
            return Ok(new { qtdEstilos = EstiloRepository.EstilosQuantidade(), qtdArtistas = 10 });
        }
    }
}