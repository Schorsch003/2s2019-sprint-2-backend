using Microsoft.EntityFrameworkCore;
using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using Senai.OpFlix.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Repositories {
    public class UsuarioRepository : IUsuarioRepository {

        public void AtualizarUsuarios (int id , Usuarios user) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                var userRetornado = BuscarPorId(id);
                if (user.Nome != null) {
                    userRetornado.Nome = user.Nome;
                }
                if (user.Email != null) {
                    userRetornado.Email = user.Email;
                }
                if (user.Senha != null) {
                    userRetornado.Senha= user.Senha;
                }
                if (user.DataNascimento != null) {
                    userRetornado.DataNascimento = user.DataNascimento;
                }
                if (user.Imagem != null) {
                    userRetornado.Imagem = user.Imagem;
                }
                ctx.Usuarios.Update(userRetornado);
                ctx.SaveChanges();
            }
        }

        public Usuarios BuscarPorEmailESenha (LoginViewModel login) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                return ctx.Usuarios.Include(x => x.IdPermissaoNavigation).FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
            }
        }

        public Usuarios BuscarPorId (int id) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                return ctx.Usuarios.Find(id);
            }
        }

        public void CadastrarUsuarios (Usuarios user) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                ctx.Usuarios.Add(user);
                ctx.SaveChanges();
            }
        }

        public List<Usuarios> ListarUsuarios () {
            using (OpFlixContext ctx = new OpFlixContext()) {
                var lista = ctx.Usuarios.Include(x => x.IdPermissaoNavigation).ToList();
                foreach (var item in lista) {
                    item.IdPermissaoNavigation.Usuarios = null;
                }
                return lista;
            }
        }

        public void RemoverUsuarios (int id) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                ctx.Usuarios.Remove(ctx.Usuarios.Find(id));
                ctx.SaveChanges();
                }
            }
    }
}
