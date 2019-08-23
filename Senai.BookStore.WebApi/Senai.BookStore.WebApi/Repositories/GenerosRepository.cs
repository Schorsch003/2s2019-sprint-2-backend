using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories {
    public class GenerosRepository {
        private string Conexao = "Data Source=.\\SqlExpress;Initial Catalog=T_BookStore;User Id=sa;Pwd=132";

        public List<GeneroDomain> Listar()
        {
            string Query = "Select IdGenero, Descricao From Generos";
            var lista = new List<GeneroDomain>();

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
                            GeneroDomain genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Descricao = sdr["Descricao"].ToString()
                            };
                            lista.Add(genero);
                        }
                    }
                }
            }

            return lista;
        }

        public List<LivroDomain> BuscarLivrosPorGenero(string nome)
        {
            List<LivroDomain> lista = new List<LivroDomain>();
            string Query = "Select L.IdLivro,L.Titulo,L.IdAutor,L.IdGenero, A.Nome,A.Email,A.Ativo,A.DataNascimento, G.Descricao" +
            "From Livros L join Autores A on A.IdAutor = L.IdAutor join Generos G on G.IdGenero = L.IdGenero Where G.Descricao = @Nome";

            SqlConnection con = new SqlConnection(Conexao);
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader sdr;
            cmd.Parameters.AddWithValue("@Nome", nome);
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

        public void Cadastrar(GeneroDomain genero)
        {
            string Query = "Insert into Generos Values (@Nome)";
            using (SqlConnection con = new SqlConnection(Conexao))
            {
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@Nome", genero.Descricao);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
