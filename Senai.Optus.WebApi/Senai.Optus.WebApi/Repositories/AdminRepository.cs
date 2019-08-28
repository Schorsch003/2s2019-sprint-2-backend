using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Senai.Optus.WebApi.Domains;

namespace Senai.Optus.WebApi.Repositories {
    public class AdminRepository {
        public string QuantidadeEstilosEArtistas () {
            using(OptusContext ctx = new OptusContext()) {
                string vai = "Quantidade de artistas: " + ctx.Artistas.Count() + "\n Quantidade de Estilos: " + ctx.Estilos.Count();
                return  vai;
            }
        }
    }
}
