using Microsoft.EntityFrameworkCore;
using Senai.Gufos.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.Repositories {
    public class EventoRepository {

        public List<Eventos> Listar()
        {
            using (GufosContext ctx = new GufosContext()){
                return ctx.Eventos.Include(x => x.IdCategoriaNavigation).ToList();
            }
        }

        public void Cadastrar(Eventos evento){
            using (GufosContext ctx = new GufosContext()){
                ctx.Eventos.Add(evento);
                ctx.SaveChanges();
            }
        }
    }
}
