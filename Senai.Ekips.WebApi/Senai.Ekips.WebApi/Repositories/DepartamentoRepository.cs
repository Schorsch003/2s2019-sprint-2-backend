using Senai.Ekips.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories {
    public class DepartamentoRepository {

        public List<Departamentos> Listar () {
            using (EkipsContext ctx = new EkipsContext()) {
                return ctx.Departamentos.ToList();
            }
        }

        public Departamentos BuscarPorId(int id) {
            using (EkipsContext ctx = new EkipsContext()) {
                return ctx.Departamentos.Find(id);
            }
        }

        public void Cadastrar(Departamentos dep) {
            using (EkipsContext ctx = new EkipsContext()) {
                ctx.Departamentos.Add(dep);
                ctx.SaveChanges();
            }
        }
    }
}
