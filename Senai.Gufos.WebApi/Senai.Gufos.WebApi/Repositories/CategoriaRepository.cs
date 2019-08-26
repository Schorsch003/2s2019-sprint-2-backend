using Senai.Gufos.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.Repositories {
    public class CategoriaRepository {
        public List<Categorias> Listar(){
            using (GufosContext ctx = new GufosContext()){
                return ctx.Categorias.ToList();
            } 
        }

        public void Cadastrar(Categorias cat){
            using(GufosContext ctx = new GufosContext()){
                ctx.Categorias.Add(cat);
                ctx.SaveChanges();
            }

        }

        public Categorias BuscarPorId(int id){
            using(GufosContext ctx = new GufosContext()){
                return ctx.Categorias.FirstOrDefault(x => x.IdCategoria == id);
            }
        }

        public void Deletar(int id) {
            using (GufosContext ctx = new GufosContext()){
                var cat = ctx.Categorias.Find(id);

                ctx.Categorias.Remove(cat);

                ctx.SaveChanges();
            }
        }

        public void Atualizar(Categorias cat){
            using (GufosContext ctx = new GufosContext()){
                var catFound = ctx.Categorias.Find(cat.IdCategoria);
                catFound.Nome = cat.Nome;
                ctx.Update(catFound);
                ctx.SaveChanges();
            }
        }

    }
}