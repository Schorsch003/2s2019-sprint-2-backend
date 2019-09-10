using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Repositories {
    public class CategoriaRepository : ICategoriaRepository {
        public void AtualizarCategoria (int id , Categoria cat) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                var categoria = ctx.Categoria.Find(id);
                if (cat.Nome != null) {
                    categoria.Nome = cat.Nome;
                }
                ctx.Update(categoria);
                ctx.SaveChanges();
            }
        }

        public Categoria BuscarCategoriaPorId (int id) {
            using(OpFlixContext ctx = new OpFlixContext()) {
                return ctx.Categoria.Find(id);
            }
        }

        public void CadastrarCategoria (Categoria cat) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                ctx.Categoria.Add(cat);
                ctx.SaveChanges();
            }
        }

        public List<Categoria> ListarCategorias () {
            using (OpFlixContext ctx = new OpFlixContext()) {
                return ctx.Categoria.ToList();
            }
        }

        public void RemoverCategoria (int id) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                var cat = ctx.Categoria.Find(id);
                ctx.Remove(cat);
                ctx.SaveChanges();
            }
        }
    }
}
