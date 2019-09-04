using Senai.AutoPecas.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Interfaces {
    public interface IPecaRepository {
        List<Pecas> ListarPecas (int idFornecedor);
        Pecas BuscarPecaPorId (int id);
        void CadastrarPeca (int idFornecedor,Pecas peca);
        void AtualizarPeca (int idPeca,Pecas peca ,int idFornecedor, out string mensagem);
        void RemoverPeca (int id,int idFornecedor,out string mensagem);
    }
}
