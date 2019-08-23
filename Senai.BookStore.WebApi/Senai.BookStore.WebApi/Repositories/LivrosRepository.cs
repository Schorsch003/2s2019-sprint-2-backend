using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories {
    public class LivrosRepository {
        private string Conexao = "Data Source=.\\SqlExpress;Initial Catalog=T_BookStore;User Id=sa;Pwd=132";


        public List<LivroDomain> Listar(){

            AutoresRepository autoresRepository = new AutoresRepository();

            var lista = new List<LivroDomain>();
            using (SqlConnection con = new SqlConnection(Conexao)){
                string Query = "Select L.IdLivro,L.Titulo,L.IdAutor,L.IdGenero, A.Nome,A.Email,A.Ativo,A.DataNascimento, G.Descricao " +
                    "From Livros L join Autores A on A.IdAutor = L.IdAutor join Generos G on G.IdGenero = L.IdGenero";
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con)){
                    con.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows){
                        while (sdr.Read()){
                            LivroDomain livro = new LivroDomain {
                                IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                                Titulo = sdr["Titulo"].ToString(),
                                GeneroId = Convert.ToInt32(sdr["IdGenero"]),
                                Genero = new GeneroDomain
                                {
                                    IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                    Descricao = sdr["Descricao"].ToString()
                                },
                                Autor = new AutorDomain {
                                    IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                    Nome = sdr["Nome"].ToString(),
                                    Email = sdr["Email"].ToString(),
                                    Ativo = Convert.ToBoolean(sdr["Ativo"]),
                                    DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                                },
                                AutorId = Convert.ToInt32(sdr["IdAutor"])
                            };
                            lista.Add(livro);
                        }
                    }
                }
            }

                return lista;
        }
        
        public LivroDomain BuscarPorId(int id){
            string Query = "Select L.IdLivro,L.Titulo,L.IdAutor,L.IdGenero, A.Nome,A.Email,A.Ativo,A.DataNascimento, G.Descricao " +
                   "From Livros L join Autores A on A.IdAutor = L.IdAutor join Generos G on G.IdGenero = L.IdGenero Where IdLivro = @IdLivro";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@IdLivro", id);
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            LivroDomain livro = new LivroDomain
                            {
                                IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                                Titulo = sdr["Titulo"].ToString(),
                                GeneroId = Convert.ToInt32(sdr["IdGenero"]),
                                Genero = new GeneroDomain
                                {
                                    IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                    Descricao = sdr["Descricao"].ToString()
                                },
                                Autor = new AutorDomain
                                {
                                    IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                    Nome = sdr["Nome"].ToString(),
                                    Email = sdr["Email"].ToString(),
                                    Ativo = Convert.ToBoolean(sdr["Ativo"]),
                                    DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                                },
                                AutorId = Convert.ToInt32(sdr["IdAutor"])
                            };
                            return livro;
                        }
                    }
                }
            }
            return null;
        }

        public void Cadastrar(LivroDomain livro){
            string Query = "Insert into Livros (Titulo,IdAutor,IdGenero) Values(@Titulo,@IdAutor,@IdGenero)";
            using (SqlConnection con = new SqlConnection(Conexao)){
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    cmd.Parameters.AddWithValue("@Titulo", livro.Titulo);
                    cmd.Parameters.AddWithValue("@IdAutor", livro.AutorId);
                    cmd.Parameters.AddWithValue("@IdGenero", livro.GeneroId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(LivroDomain livro, int id)
        {
            string QueryNome = "Update Livros SET Titulo = @Nome Where IdLivro = @Id";
            string QueryAutor = "Update Livros SET IdAutor = @Autor Where IdLivro = @Id";
            string QueryGenero = "Update Livros SET IdGenero = @Genero Where IdLivro = @Id";

            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlCommand cmdNome = new SqlCommand(QueryNome, con);
                SqlCommand cmdAutor = new SqlCommand(QueryAutor, con);
                SqlCommand cmdGenero = new SqlCommand(QueryGenero, con);

                cmdNome.Parameters.AddWithValue("@Nome", livro.Titulo);
                cmdNome.Parameters.AddWithValue("@Id", id);

                cmdAutor.Parameters.AddWithValue("@Autor", livro.AutorId);
                cmdAutor.Parameters.AddWithValue("@Id", id);

                cmdGenero.Parameters.AddWithValue("@Genero", livro.GeneroId);
                cmdGenero.Parameters.AddWithValue("@Id", id);

                cmdAutor.ExecuteNonQuery();
                cmdGenero.ExecuteNonQuery();
                cmdNome.ExecuteNonQuery();

            }
        }

        public void Deletar (int id){

            string Query = "Delete From Livros Where IdLivro = @Id";


            using (SqlConnection con = new SqlConnection(Conexao))
            {


                con.Open();
                SqlCommand cmd = new SqlCommand(Query, con);
                
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();

            }
        }

    }
}
