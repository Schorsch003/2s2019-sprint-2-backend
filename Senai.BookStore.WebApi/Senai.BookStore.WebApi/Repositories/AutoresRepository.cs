using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories {
    public class AutoresRepository {
        private string Conexao = "Data Source=.\\SqlExpress;Initial Catalog=T_BookStore;User Id=sa;Pwd=132";

        public List<AutorDomain> Listar()
        {
            string Query = "Select IdAutor, Nome, Email, Ativo, DataNascimento From Autores";
            var lista = new List<AutorDomain>();
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    con.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            AutorDomain autor = new AutorDomain
                            {
                                IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                Nome = sdr["Nome"].ToString(),
                                Email = sdr["Email"].ToString(),
                                Ativo = Convert.ToBoolean(sdr["Ativo"]),
                                DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                            };
                            lista.Add(autor);
                        }
                    }
                }
                return lista;
            }
        }

        public List<LivroDomain> LivrosAutorAtivo(int id){
            List<LivroDomain> lista = new List<LivroDomain>();
            string Query = "Select L.IdLivro,L.Titulo,L.IdAutor,L.IdGenero, A.Nome,A.Email,A.Ativo,A.DataNascimento, G.Descricao " +
                   "From Livros L join Autores A on A.IdAutor = L.IdAutor join Generos G on G.IdGenero = L.IdGenero Where L.IdAutor = @Id " +
                   "and A.Ativo = 1 ";
            SqlConnection con = new SqlConnection(Conexao);
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader sdr;
            cmd.Parameters.AddWithValue("@Id", id);
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
                    lista.Add(livro);
                }
            }
            return lista;
        }

        public List<AutorDomain> AutoresNascidosEm(int ano){
            List<AutorDomain> lista = new List<AutorDomain>();
            string Query = "Select IdAutor, Nome, Email, Ativo, DataNascimento From Autores Where Year(DataNascimento) = @Ano";
            SqlConnection con = new SqlConnection(Conexao);
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader sdr;
            cmd.Parameters.AddWithValue("@Ano", ano);
            sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    AutorDomain autor = new AutorDomain
                    {
                        IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                        Nome = sdr["Nome"].ToString(),
                        Email = sdr["Email"].ToString(),
                        Ativo = Convert.ToBoolean(sdr["Ativo"]),
                        DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                    };
                    lista.Add(autor);
                }
            }
            return lista;
        }

        public AutorDomain BuscarPorId(int id)
        {
            string Query = "Select IdAutor, Nome, Email, Ativo, DataNascimento From Autores Where IdAutor = @Id";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            AutorDomain autor = new AutorDomain
                            {
                                IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                Nome = sdr["Nome"].ToString(),
                                Email = sdr["Email"].ToString(),
                                Ativo = Convert.ToBoolean(sdr["Ativo"]),
                                DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                            };
                            return autor;
                        }
                    }
                }
            }
            return null;
        }

        public void Cadastrar(AutorDomain autor)
        {
            string Query = "Insert into Autores (Nome,Email,Ativo,DataNascimento) Values (@Nome,@Email,@Ativo,@DataNascimento)";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", autor.Nome);
                    cmd.Parameters.AddWithValue("@Email", autor.Email);
                    cmd.Parameters.AddWithValue("@Ativo", autor.Ativo);
                    cmd.Parameters.AddWithValue("@DataNascimento", autor.DataNascimento);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<LivroDomain> BuscarLivroPorAutor(int id)
        {
            string Query = "Select L.IdLivro,L.Titulo,L.IdAutor,L.IdGenero, A.Nome,G.Descricao From Livros L" +
                " join Autores A on A.IdAutor = L.IdAutor join Generos G on G.IdGenero = L.IdGenero Where L.IdAutor = @Id";
            var lista = new List<LivroDomain>();
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
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
                                    },
                                AutorId = Convert.ToInt32(sdr["IdAutor"])
                            };
                            lista.Add(livro);
                        }
                    }
                }
                return lista;
            }
        }

        public List<AutorDomain> BuscarAutoresAtivos()
        {
            string Query = "Select IdAutor, Nome, Email, Ativo, DataNascimento From Autores Where Ativo = 1";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                List<AutorDomain> lista = new List<AutorDomain>();
                con.Open();
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            AutorDomain autor = new AutorDomain
                            {
                                IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                Nome = sdr["Nome"].ToString(),
                                Email = sdr["Email"].ToString(),
                                Ativo = Convert.ToBoolean(sdr["Ativo"]),
                                DataNascimento = Convert.ToDateTime(sdr["DataNascimento"])
                            };
                            lista.Add(autor);
                        }
                    }
                }
            return lista;
            }
        }


    }
}