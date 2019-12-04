using Microsoft.EntityFrameworkCore;
using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.Interfaces;
using Senai.OpFlix.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                    userRetornado.Senha = user.Senha;
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
                return ctx.Usuarios.Include(x => x.IdPermissaoNavigation).FirstOrDefault(x => x.IdUsuario == id);
            }
        }

        public void CadastrarUsuarios (Usuarios user , bool permissao) {
            Byte[] cript = System.Text.Encoding.ASCII.GetBytes(user.Senha);
            string senhaCrip = Convert.ToBase64String(cript);
            user.Senha = senhaCrip;
            using (OpFlixContext ctx = new OpFlixContext()) {
                if (permissao) {
                    user.IdPermissao = 1;
                } else {
                    user.IdPermissao = 2;
                }
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

        public List<Lancamentos> BuscarLancamentosFavoritos (int idUsuario) {
            //using (OpFlixContext ctx = new OpFlixContext()) {
            //    var lista = ctx.LancamentosFavoritos.Where(x => x.IdUsuario == idUsuario).Include(x => x.Lancamento).ToList();
            //    var novaLista = new List<Lancamentos>();
            //    foreach (var item in lista) {
            //        novaLista.Add(item.Lancamento);
            //    }
            //    return novaLista;
            //}
            List<Lancamentos> lista = new List<Lancamentos>();
            using (SqlConnection con = new SqlConnection("Data Source=.\\SqlExpress;Initial Catalog=T_OpFlix;User Id=sa;Pwd=132")) {
                con.Open();
                string query = "Select L.DataLancamento, L.IdLancamento,L.Titulo,L.Sinopse,L.TempoDuracao,L.IdTipo,L.IdCategoria, L.Plataforma, L.Imagem,P.Nome as Plat,C.Nome as Cat,T.Nome as Tipo" +
                    " From LancamentosFavoritos Join Usuarios U on LancamentosFavoritos.IdUsuario = U.IdUsuario" +
                    " Join Lancamentos L on L.IdLancamento = LancamentosFavoritos.IdLancamento Join Categoria C on C.IdCategoria = L.IdCategoria " +
                    "Join Plataformas P on  P.IdPlataforma = L.Plataforma Join Tipo T on T.IdTipo = L.IdTipo Where LancamentosFavoritos.IdUsuario =  @Id";
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(query , con)) {
                    cmd.Parameters.AddWithValue("@Id" , idUsuario);
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows) {
                        while (sdr.Read()) {
                            Lancamentos l = new Lancamentos {
                                IdLancamento = Convert.ToInt32(sdr["IdLancamento"]) ,
                                Titulo = sdr["Titulo"].ToString() ,
                                Sinopse = sdr["Sinopse"].ToString() ,
                                TempoDuracao = Convert.ToInt32(sdr["TempoDuracao"]) ,
                                IdTipo = Convert.ToInt32(sdr["IdTipo"]) ,
                                IdCategoria = Convert.ToInt32(sdr["IdCategoria"]) ,
                                Plataforma = Convert.ToInt32(sdr["Plataforma"]) ,
                                IdCategoriaNavigation = new Categoria {
                                    IdCategoria = Convert.ToInt32(sdr["IdCategoria"]) ,
                                    Nome = sdr["Cat"].ToString() ,
                                } ,
                                PlataformaNavigation = new Plataformas {
                                    IdPlataforma = Convert.ToInt32(sdr["Plataforma"]) ,
                                    Nome = sdr["Plat"].ToString() ,

                                } ,
                                DataLancamento = Convert.ToDateTime(sdr["DataLancamento"]) ,
                                IdTipoNavigation = new Tipo(){
                                    IdTipo = Convert.ToInt32(sdr["IdTipo"]) ,
                                    Nome = sdr["Tipo"].ToString() ,
                                } ,
                                Imagem = sdr["Imagem"].ToString()
                            };
                            lista.Add(l);
                        }
                    }
                }
                return lista;
            }
        }

        public void FavoritarLancamento (int idUsuario , int idLancamento) {
            using (OpFlixContext ctx = new OpFlixContext()) {
                var lancamentoFav = new LancamentosFavoritos {
                    IdUsuario = idUsuario,
                    IdLancamento = idLancamento
                };
                ctx.LancamentosFavoritos.Add(lancamentoFav);
                ctx.SaveChanges();
            }
        }
    }
}
