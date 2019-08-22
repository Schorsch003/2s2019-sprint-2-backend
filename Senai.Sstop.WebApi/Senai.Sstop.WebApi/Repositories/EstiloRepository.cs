using Senai.Sstop.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Sstop.WebApi.Repositories {
    public class EstiloRepository {

        //aonde será feita a comunicação
        private string StringConexao = "Data Source=.\\SqlExpress;Initial Catalog=T_SStop;User Id=sa;Pwd=132;";

        public List<EstiloDomain> Listar() {
            List<EstiloDomain> estilos = new List<EstiloDomain>();

            // Conectar com o banco
            using (SqlConnection con = new SqlConnection(StringConexao)) {
                string Query = "SELECT IdEstiloMusical, Nome FROM EstilosMusicais";

                //Abrir a conexão
                con.Open();

                //Percorre a lista
                SqlDataReader sdr;

                //Comando a ser executado na conexão especificada
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    //Pegar os valores da tabela e trazer para o backend
                    sdr = cmd.ExecuteReader();
                }

                while (sdr.Read()) {
                    EstiloDomain estilo = new EstiloDomain {
                        IdEstilo = Convert.ToInt32(sdr["IdEstiloMusical"]),
                        Nome = sdr["Nome"].ToString()
                    };
                    estilos.Add(estilo);
                }
            }
            // Executar o Select
            // Retornar as informações


            return estilos;
        }
    
        public EstiloDomain BuscarPorId(int id){
            string Query = "SELECT * FROM EstilosMusicais WHERE IdEstiloMusical = @IdEstiloMusical";
            using(SqlConnection con = new SqlConnection(StringConexao)){
                con.Open();
                SqlDataReader sdr;
                using(SqlCommand cmd = new SqlCommand(Query,con)){

                cmd.Parameters.AddWithValue("@IdEstiloMusical",id);

                sdr = cmd.ExecuteReader();
                }

                if(sdr.HasRows){   
                    while(sdr.Read()){
                        return new EstiloDomain{
                        IdEstilo = Convert.ToInt32(sdr["IdEstiloMusical"]),
                        Nome = sdr["Nome"].ToString()
                        };
                    }            
                }
            }
            return null;
        }

        public void Cadastrar(EstiloDomain estilo){
            string Query = "INSERT INTO EstilosMusicais (Nome) VALUES (@Nome)";

            using(SqlConnection con = new SqlConnection(StringConexao)) {
                SqlCommand cmd = new SqlCommand(Query, con);
                con.Open();
                cmd.Parameters.AddWithValue("@Nome", estilo.Nome);
                cmd.ExecuteNonQuery();
                
            }
        }

        public void Deletar(int id) {
            string Query = "DELETE FROM EstilosMusicais WHERE IdEstiloMusical = @IdEstiloMusical";

            using(SqlConnection con = new SqlConnection(StringConexao)) {
                using(SqlCommand cmd = new SqlCommand(Query, con)) {
                    cmd.Parameters.AddWithValue("@IdEstiloMusical", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                } 

            }
        }

        public void Atualizar (EstiloDomain estilo) {
            string Query = "UPDATE EstilosMusicais SET Nome = @Nome WHERE IdEstiloMusical = @Id";

            using (SqlConnection con = new SqlConnection(StringConexao)) {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome", estilo.Nome);
                cmd.Parameters.AddWithValue("@Id", estilo.IdEstilo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
