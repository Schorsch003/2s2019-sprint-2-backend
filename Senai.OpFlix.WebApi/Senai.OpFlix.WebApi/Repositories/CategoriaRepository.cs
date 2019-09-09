using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Repositories {
    public class CategoriaRepository : ICategoriaRepository {
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
    }
}
