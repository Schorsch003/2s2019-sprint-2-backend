using Senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Repositories {
    public class FilmesRepository {
        private string StringConexao = "Data Source=localhost;Initial Catalog=RoteiroFilmes;User Id=sa;Pwd=132;";

        public List<FilmesDomain> Listar() {
            var lista = new List<FilmesDomain>();
            using (SqlConnection con = new SqlConnection(StringConexao)) {
                string Query = "Select F.IdFilme, F.Titulo, F.IdGenero ,G.Nome as Genero from Filmes F Join Generos G" +
                    " on F.IdGenero = G.IdGenero";
                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    con.Open();
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows) {
                        while (sdr.Read()) {


                            FilmesDomain filme = new FilmesDomain {
                                IdFilme = Convert.ToInt32(sdr["IdFilme"]),
                                Titulo = sdr["Titulo"].ToString(),
                                GeneroId = Convert.ToInt32(sdr["IdGenero"]),
                                Genero = new GeneroDomain {
                                    IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                    Nome = sdr["Genero"].ToString()
                                }
                            }
;
                            lista.Add(filme);
                        }

                    };
                }
            }
            return lista;
        }

        public FilmesDomain BuscarPorId(int id) {
            using (SqlConnection con = new SqlConnection(StringConexao)) {
                string Query = "Select F.IdFilme, F.Titulo, F.IdGenero ,G.Nome as Genero from Filmes F Join Generos G on G.IdGenero = F.IdGenero Where F.IdFilme = @Id";
                SqlDataReader sdr;
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    cmd.Parameters.AddWithValue("@Id", id);
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows) {
                        while (sdr.Read()) {
                            FilmesDomain filme = new FilmesDomain {
                                IdFilme = Convert.ToInt32(sdr["IdFilme"]),
                                Titulo = sdr["Titulo"].ToString(),
                                GeneroId = Convert.ToInt32(sdr["IdGenero"]),
                                Genero = new GeneroDomain {
                                    IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                    Nome = sdr["Genero"].ToString()
                                }
                            };
                            return filme;
                        }
                    }
                }
                return null;
            }
        }

        public void Cadastrar(FilmesDomain filme) {
            string Query = "Insert into Filmes (Titulo,IdGenero) Values(@Titulo,@GeneroId)";
            using(SqlConnection con = new SqlConnection(StringConexao)) {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    cmd.Parameters.AddWithValue("@Titulo",filme.Titulo);
                    cmd.Parameters.AddWithValue("@GeneroId", filme.GeneroId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
