using Microsoft.EntityFrameworkCore;
using Senai.Ekips.WebApi.Domains;
using Senai.Ekips.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories {
    public class UsuarioRepository {

        public List<Usuarios> Listar () {
            using (EkipsContext ctx = new EkipsContext()) {
                return ctx.Usuarios.ToList();
            }
        }

        public Usuarios BuscarPorEmailESenha(LoginViewModel login) {
            using(EkipsContext ctx = new EkipsContext()) {
                return ctx.Usuarios.Include(x => x.IdPermissaoNavigation).FirstOrDefault(x => login.Email == x.Email && login.Senha == x.Senha);
            }
        }
    }
}
