using Microsoft.EntityFrameworkCore;
using Senai.Ekips.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories {
    public class FuncionarioRepository {

        public List<Funcionarios> Listar () {
            using (EkipsContext ctx = new EkipsContext()) {
                return ctx.Funcionarios.Include(x => x.IdCargoNavigation).Include(x => x.IdDepartamentoNavigation).ToList();
            }

        }
        public void Cadastrar (Funcionarios funci) {
            using (EkipsContext ctx = new EkipsContext()) {
                ctx.Funcionarios.Add(funci);
                ctx.SaveChanges();
            }
        }

        public void Atualizar (int id, Funcionarios funcionario) {
            using (EkipsContext ctx = new EkipsContext()) {
                var user = ctx.Funcionarios.Find(id);
                user = funcionario;
                ctx.Update(user);
                ctx.SaveChanges();
            }
        }

        public void Remover (Funcionarios funcionario) {
            using (EkipsContext ctx = new EkipsContext()) {
                ctx.Remove(funcionario);
            }
        }
    }

}
