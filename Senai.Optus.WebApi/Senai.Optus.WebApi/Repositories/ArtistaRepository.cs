using Microsoft.EntityFrameworkCore;
using Senai.Optus.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Optus.WebApi.Repositories {
    public class ArtistaRepository {
        public List<Artistas> Listar() {
            using (OptusContext ctx = new OptusContext()){
                return ctx.Artistas.Include(x => x.IdEstiloNavigation).ToList();
            }
        }

        public void Cadastrar(Artistas artist) {
            using (OptusContext ctx = new OptusContext()) {
                ctx.Artistas.Add(artist);
                ctx.SaveChanges();
            }
        }


    }
}
