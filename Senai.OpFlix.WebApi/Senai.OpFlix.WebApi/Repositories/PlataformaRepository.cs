using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Repositories {
    public class PlataformaRepository : IPlataformaRepository {

        public void AtualizarPlataforma (int id , Plataformas plat) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                var plataforma = BuscarPlataformaPorId(id);
                if (plat.Nome != null) {
                    plataforma.Nome = plat.Nome;
                }
                ctx.Update(plataforma);
                ctx.SaveChanges();
            }
        }

        public Plataformas BuscarPlataformaPorId (int id) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                return ctx.Plataformas.Find(id);
            }
        }

        public void CadastrarPlataforma (Plataformas plat) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                ctx.Plataformas.Add(plat);
                ctx.SaveChanges();
            }
        }

        public List<Plataformas> ListarPlataformas () {
            using (OpFlixContext ctx = new OpFlixContext()) {
                return ctx.Plataformas.ToList();
            }
        }

        public void RemoverPlataforma (int id) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                var plat = BuscarPlataformaPorId(id);
                ctx.Plataformas.Remove(plat);
                ctx.SaveChanges();
            }
        }
    }
}
