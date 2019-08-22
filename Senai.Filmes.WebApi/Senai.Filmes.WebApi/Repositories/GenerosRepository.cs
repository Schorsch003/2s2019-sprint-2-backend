using Senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Repositories {
    public class GenerosRepository {
        private string StringConexao = "Data Source=.\\SqlExpress;Initial Catalog=RoteiroFilmes;User Id=sa;Pwd=132;";

        public List<GeneroDomain> Listar() {
            List<GeneroDomain> lista = new List<GeneroDomain>();

            SqlConnection con = new SqlConnection(StringConexao);
            string Query = "SELECT * FROM Generos";
            con.Open();

            SqlDataReader sdr;

            SqlCommand cmd = new SqlCommand(Query, con);

            sdr = cmd.ExecuteReader();
            while (sdr.Read()) {
                GeneroDomain genero = new GeneroDomain {
                    IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                    Nome = sdr["Nome"].ToString()
                };
                lista.Add(genero);
            }
            return lista;
        }

        public GeneroDomain BuscarPorId(int id) {
            string Query = "SELECT IdGenero,Nome FROM Generos WHERE IdGenero = @Id";
            using (SqlConnection con = new SqlConnection(StringConexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {

                    SqlDataReader sdr;


                    con.Open();

                    cmd.Parameters.AddWithValue("@Id", id);
                    sdr = cmd.ExecuteReader();

                    while (sdr.HasRows) {

                        if (sdr.Read()) {
                            GeneroDomain genero = new GeneroDomain {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Nome = sdr["Nome"].ToString()
                            };
                            return genero;
                        }
                    }

                    return null;

                }
            }
        }
        
        public List<FilmesDomain> FilmesPorGenero(int id) {
            string Query = "Select F.IdFilme, F.Titulo, F.IdGenero As GeneroID ,G.Nome as Genero from Filmes F Join Generos G on G.IdGenero = F.IdGenero Where F.IdGenero = @Id";
            List<FilmesDomain> filmes = new List<FilmesDomain>();
            using (SqlConnection con = new SqlConnection(StringConexao)) {
                con.Open();
                SqlDataReader sdr;
                using(SqlCommand cmd = new SqlCommand(Query, con)) {
                    cmd.Parameters.AddWithValue("@Id", id);
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows) {
                        while (sdr.Read()) {
                            FilmesDomain filme = new FilmesDomain {
                                IdFilme = Convert.ToInt32(sdr["IdFilme"]),
                                Titulo = sdr["Titulo"].ToString(),
                                GeneroId = Convert.ToInt32(sdr["GeneroID"]),
                                Genero = new GeneroDomain {
                                    IdGenero = Convert.ToInt32(sdr["GeneroID"]),
                                    Nome = sdr["Genero"].ToString()
                                }
                            };
                            filmes.Add(filme);
                        }
                    }
                }
            }
            return filmes;
        }

        public void Cadastrar(GeneroDomain genero) {
            string Query = "INSERT INTO Generos (Nome) Values (@Genero)";
            using (SqlConnection con = new SqlConnection(StringConexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    con.Open();
                    cmd.Parameters.AddWithValue("@Genero", genero.Nome);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(GeneroDomain genero) {
            string Query = "DELETE FROM Generos WHERE IdGenero = @Id";
            using (SqlConnection con = new SqlConnection(StringConexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    cmd.Parameters.AddWithValue("@Id", genero.IdGenero);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Corrigir o método
        public void Atualizar(GeneroDomain genero) {
            //GeneroDomain genero = BuscarPorId(id);
            string Query = "UPDATE Generos SET Nome = @Nome WHERE IdGenero=@Id";
            using (SqlConnection con = new SqlConnection(StringConexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                    cmd.Parameters.AddWithValue("@Id", genero.IdGenero);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

