using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Repositories {
    public class TipoRepository : ITipoRepository {

        public void AtualizarTipo (int id , Tipo type) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                var tipo = BuscarTipoPorId(id);
                if (type.Nome != null) {
                    tipo.Nome = type.Nome;
                }
                ctx.Update(tipo);
                ctx.SaveChanges();
            }
        }

        public Tipo BuscarTipoPorId (int id) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                return ctx.Tipo.Find(id);
            }
        }

        public void CadastrarTipo (Tipo tipo) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                ctx.Tipo.Add(tipo);
                ctx.SaveChanges();
            }
        }

        public List<Tipo> ListarTipos () {
            using (OpFlixContext ctx = new OpFlixContext()) {
                return ctx.Tipo.ToList();
            }
        }

        public void RemoverTipo (int id) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                var tipo = BuscarTipoPorId(id);
                ctx.Tipo.Remove(tipo);
                ctx.SaveChanges();
            }
        }
    }
}
