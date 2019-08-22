using Senai.Sstop.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Sstop.WebApi.Repositories {
    public class ArtistaRepository {
        private string StringConexao = "Data Source=.\\SqlExpress;Initial Catalog=T_SStop;User Id=sa;Pwd=132;";

        public List<ArtistaDomain> Listar() {
            List<ArtistaDomain> artistas = new List<ArtistaDomain>();
            string Query = "Select A.IdArtista, A.Nome, EM.IdEstiloMusical ,EM.Nome As NomeEstilo From Artistas As A Join EstilosMusicais As EM " +
            "on A.IdEstiloMusical = EM.IdEstiloMusical";
            using (SqlConnection con = new SqlConnection(StringConexao)) {
                SqlDataReader sdr;
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Query,con)) {
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows) {
                        while (sdr.Read()) {
                            ArtistaDomain artista = new ArtistaDomain {
                                IdArtista = Convert.ToInt32(sdr["IdArtista"]),
                                Nome = sdr["Nome"].ToString(),
                                EstiloMusical = new EstiloDomain {
                                    IdEstilo = Convert.ToInt32(sdr["IdEstiloMusical"]),
                                    Nome = sdr["NomeEstilo"].ToString()
                                }
                            };
                            artistas.Add(artista);
                        }
                    }
                }
            }
            return artistas;
        }

        public void Cadastrar(ArtistaDomain artista) {
            using (SqlConnection con = new SqlConnection(StringConexao)) {
                string Query = "Insert into Artistas(Nome, IdEstiloMusical) Values(@Nome, @IdEstiloMusical)";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(Query,con)) {
                    cmd.Parameters.AddWithValue("@Nome", artista.Nome);
                    cmd.Parameters.AddWithValue("@IdEstiloMusical", artista.EstiloId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
