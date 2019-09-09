using Microsoft.EntityFrameworkCore;
using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Repositories {
    public class LancamentoRepository : ILancamentoRepository {
        
        public void CadastrarLancamentos (Lancamentos lanc) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                ctx.Lancamentos.Add(lanc);
                ctx.SaveChanges();
            }
        }

        public List<Lancamentos> ListarLancamentos () {
            using (OpFlixContext ctx = new OpFlixContext()) {
                var lista = ctx.Lancamentos.Include(x => x.IdCategoriaNavigation).Include(x => x.IdTipoNavigation)
                    .Include(x => x.PlataformaNavigation).ToList();
                foreach(var item in lista) {
                    item.IdCategoriaNavigation.Lancamentos = null;
                    item.IdTipoNavigation.Lancamentos = null;
                    item.PlataformaNavigation.Lancamentos = null;
                }
                return lista;
            }
        }
    }
}
