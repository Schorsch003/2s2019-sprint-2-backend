using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Repositories {
    public class PecaRepository : IPecaRepository {

        public void AtualizarPeca (int id , Pecas peca , int idFornecedor , out string message) {
            using (AutoPecasContext ctx = new AutoPecasContext()) {
                var pecaRetornada = BuscarPecaPorId(id);
                if (pecaRetornada.IdFornecedor == idFornecedor) {
                    if (peca.Peso != null) {
                        pecaRetornada.Peso = peca.Peso;
                    }
                    if (peca.Descricao != null) {
                        pecaRetornada.Descricao = peca.Descricao;
                    }
                    if (peca.CodigoPeca != null) {
                        pecaRetornada.CodigoPeca = peca.CodigoPeca;
                    }
                    if (peca.IdFornecedor != null) {
                        pecaRetornada.IdFornecedor = peca.IdFornecedor;
                    }
                    ctx.Pecas.Update(pecaRetornada);
                    ctx.SaveChanges();
                    message = "Peça atualizada com sucesso";
                } else {
                    message = "Peça não pertence ao fornecedor logado";
                }
            }
        }

        public Pecas BuscarPecaPorId (int id) {
            using (AutoPecasContext ctx = new AutoPecasContext()) {
                return ctx.Pecas.Find(id);
            }
        }

        public void CadastrarPeca (int idFornecedor , Pecas peca) {
            using (AutoPecasContext ctx = new AutoPecasContext()) {
                peca.IdFornecedor = idFornecedor;
                ctx.Pecas.Add(peca);
                ctx.SaveChanges();
            }
        }

        public List<Pecas> ListarPecas (int idFornecedor) {
            using (AutoPecasContext ctx = new AutoPecasContext()) {
                return ctx.Pecas.Where(x => x.IdFornecedor == idFornecedor).ToList();
            }
        }

        public void RemoverPeca (int id , int idFornecedor , out string msg) {
            using (AutoPecasContext ctx = new AutoPecasContext()) {
                var peca = BuscarPecaPorId(id);
                if (peca.IdFornecedor == idFornecedor) {
                    ctx.Pecas.Remove(peca);
                    ctx.SaveChanges();
                    msg = "Peça removida com sucesso";
                } else {
                    msg = "A peça que você deseja remover não pertence ao seu usuário";
                }
            }
        }
    }
}
