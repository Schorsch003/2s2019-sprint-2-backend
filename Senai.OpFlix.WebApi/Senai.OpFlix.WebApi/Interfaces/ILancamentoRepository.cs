using Senai.OpFlix.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces {
    public interface ILancamentoRepository {
        List<Lancamentos> ListarLancamentos ();
        List<Lancamentos> FiltrarPorData (int ano);
        List<Lancamentos> MaisRecentes ();
        List<Lancamentos> BuscarPorGenero (int idGenero);
        List<Lancamentos> FiltrarPorPlataforma (string plat);
        List<LancamentosFavoritos> BuscarUsuariosPorLancamentoFavorito (int idLancamento);
        Lancamentos BuscarPorId (int id);
        void CadastrarLancamentos (Lancamentos lanc);
        void AtualizarLancamento (int id , Lancamentos lanc);
        void RemoverLancamentos (int id);
    }
}
