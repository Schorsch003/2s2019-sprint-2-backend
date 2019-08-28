using Senai.Ekips.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories {
    public class CargosRepository {

        public List<Cargos> Listar () {
            using (EkipsContext ctx = new EkipsContext()) {
                return ctx.Cargos.ToList();
            }
        }

        public Cargos BuscarPorId (int id) {
            using (EkipsContext ctx = new EkipsContext()) {
                return ctx.Cargos.FirstOrDefault(c => c.IdCargo == id);
            }
        }
        
        public void Cadastrar(Cargos cargo) {
            using(EkipsContext ctx = new EkipsContext()) {
                ctx.Cargos.Add(cargo);
                ctx.SaveChanges();
            }
        }

        public void Atualizar(int id,Cargos cargo) {
            using (EkipsContext ctx = new EkipsContext()) {
                var cargoFound = ctx.Cargos.Find(id);
                cargoFound.Nome = cargo.Nome;
                cargoFound.Ativo = cargo.Ativo;
                ctx.Update(cargoFound);
                ctx.SaveChanges();
            }
        }
    }
}
