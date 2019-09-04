using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using Senai.AutoPecas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Repositories {
    public class UsuarioRepository : IUsuarioRepository {

        public Usuarios BuscarPorEmailESenha (LoginViewModel login) {
            using (AutoPecasContext ctx = new AutoPecasContext()) {
                return ctx.Usuarios.FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
            }
        }

        public Usuarios BuscarPorId (int id) {
            using (AutoPecasContext ctx = new AutoPecasContext()) {
                return ctx.Usuarios.Find(id);
            }
        }

        public List<Usuarios> ListarUsuarios () {
            using (AutoPecasContext ctx = new AutoPecasContext()) {
                var lista = ctx.Usuarios.ToList();
                foreach(var item in lista) {
                    item.Senha = null;
                }
                return lista;
            }
        }
    }
}
