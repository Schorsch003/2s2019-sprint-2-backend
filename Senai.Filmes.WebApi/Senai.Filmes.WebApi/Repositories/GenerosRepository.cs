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


    }
}
