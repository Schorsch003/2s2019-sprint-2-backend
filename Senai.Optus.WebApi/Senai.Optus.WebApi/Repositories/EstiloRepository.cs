using Senai.Optus.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Optus.WebApi.Repositories {
    public class EstiloRepository {
        public List<Estilos> Listar() {
            using (OptusContext ctx = new OptusContext()) {
                return ctx.Estilos.ToList();
            }
        }

        public void Cadastrar(Estilos estilo) {
            using (OptusContext ctx = new OptusContext()) {
                ctx.Estilos.Add(estilo);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (OptusContext ctx = new OptusContext()) {
                var estilo = ctx.Estilos.FirstOrDefault(x => x.IdEstilo == id);
                ctx.Estilos.Remove(estilo);
                ctx.SaveChanges();
            }
        }

        public void Atualizar(Estilos estilo){
            using (OptusContext ctx = new OptusContext()) {
                var estFound = ctx.Estilos.Find(estilo.IdEstilo);
                estFound.Nome = estilo.Nome;
                ctx.Update(estFound);
                ctx.SaveChanges();
            }
        }

    }
}
