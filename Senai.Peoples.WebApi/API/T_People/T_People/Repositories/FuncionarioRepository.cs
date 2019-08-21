using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using T_People.Domains;

namespace T_People.Repositories {
    public class FuncionarioRepository {

        private string Conexao = "Data Source=.\\SqlExpress;Initial Catalog=T_Peoples;User Id=sa;Pwd=132;";


        public List<FuncionarioDomain> Listar() {
            List<FuncionarioDomain> listaFuncionarios = new List<FuncionarioDomain>();
            string Query = "Select * From Funcionarios";
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(Conexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    con.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows) {
                        while (sdr.Read()) {
                            DateTime data = DateTime.Parse(sdr["DataNascimento"].ToString());
                            FuncionarioDomain func = new FuncionarioDomain {
                                IdFuncionario = Convert.ToInt32(sdr["IdFuncionario"]),
                                Nome = sdr["Nome"].ToString(),
                                Sobrenome = sdr["Sobrenome"].ToString(),
                                DataNascimento = DateTime.Parse(data.ToShortDateString())

                            };
                            listaFuncionarios.Add(func);
                        }
                    }
                }
            }
            return listaFuncionarios;
        }

        public List<FuncionarioDomain> NomesCompletos() {
            List<FuncionarioDomain> listaFuncionarios = new List<FuncionarioDomain>();
            string Query = "Select * From Funcionarios";
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(Conexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    con.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows) {
                        while (sdr.Read()) {
                            DateTime data = DateTime.Parse(sdr["DataNascimento"].ToString());
                            FuncionarioDomain func = new FuncionarioDomain {
                                IdFuncionario = Convert.ToInt32(sdr["IdFuncionario"]),
                                Nome = sdr["Nome"].ToString(),
                                Sobrenome = sdr["Sobrenome"].ToString(),
                                NomeCompleto = sdr["Nome"].ToString() + " " + sdr["Sobrenome"].ToString(),
                                DataNascimento = DateTime.Parse(data.ToShortDateString())

                            };
                            listaFuncionarios.Add(func);
                        }
                    }
                }
                return listaFuncionarios;
            }
        }

        public FuncionarioDomain BuscarPorId(int id) {


            string Query = "Select * From Funcionarios Where IdFuncionario = @Id";
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(Conexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows) {
                        while (sdr.Read()) {
                            FuncionarioDomain func = new FuncionarioDomain {
                                IdFuncionario = Convert.ToInt32(sdr["IdFuncionario"]),
                                Nome = sdr["Nome"].ToString(),
                                Sobrenome = sdr["Sobrenome"].ToString()
                            };
                            return func;
                        }
                    }
                }
            }
            return null;
        }

        public FuncionarioDomain BuscarPorNome(string nome) {

            string Query = "Select * From Funcionarios Where Nome = @Id";
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(Conexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    cmd.Parameters.AddWithValue("@Id", nome);
                    con.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows) {
                        while (sdr.Read()) {
                            FuncionarioDomain func = new FuncionarioDomain {
                                IdFuncionario = Convert.ToInt32(sdr["IdFuncionario"]),
                                Nome = sdr["Nome"].ToString(),
                                Sobrenome = sdr["Sobrenome"].ToString()
                            };
                            return func;
                        }
                    }
                }
            }
            return null;
        }

        public void Cadastrar(FuncionarioDomain funcionario) {
            string Query = "Insert into Funcionarios (Nome,Sobrenome,DataNascimento) Values (@Nome,@Sobrenome,@DataNascimento)";

            using (SqlConnection con = new SqlConnection(Conexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);
                    cmd.Parameters.AddWithValue("@DataNascimento", funcionario.DataNascimento);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(FuncionarioDomain funcionario) {
            string Query = "Update Funcionarios Set Nome = @Nome Where IdFuncionario = @Id";
            string Query2 = "Update Funcionarios Set Sobrenome = @Sobrenome Where IdFuncionario = @Id";

            using (SqlConnection con = new SqlConnection(Conexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    SqlCommand cmd2 = new SqlCommand(Query2, con);
                    cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@Id", funcionario.IdFuncionario);
                    cmd2.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);
                    cmd2.Parameters.AddWithValue("@Id", funcionario.IdFuncionario);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(FuncionarioDomain funcionario) {
            string Query = "Delete From Funcionarios Where IdFuncionario = @Id";

            using (SqlConnection con = new SqlConnection(Conexao)) {
                using (SqlCommand cmd = new SqlCommand(Query, con)) {
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id", funcionario.IdFuncionario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FuncionarioDomain> ListarPorOrder(string ordem, out bool ordemCerta) {
            var listaFuncionarios = new List<FuncionarioDomain>();
            string QueryAsc = "Select * From Funcionarios Order By Nome asc";
            string QueryDesc = "Select * From Funcionarios Order By Nome desc";

            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(Conexao)) {

                SqlCommand cmd; 
                    if (ordem.Equals("asc")) {
                    cmd = new SqlCommand(QueryAsc, con);
                    ordemCerta = true;
                    } else if (ordem.Equals("desc")) {
                    cmd = new SqlCommand(QueryDesc, con);
                    ordemCerta = true;
                    } else {
                    cmd = new SqlCommand(QueryAsc, con);
                    ordemCerta = false;
                    }

                    con.Open();

                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows) {
                        while (sdr.Read()) {
                            DateTime data = DateTime.Parse(sdr["DataNascimento"].ToString());
                            FuncionarioDomain func = new FuncionarioDomain {
                                IdFuncionario = Convert.ToInt32(sdr["IdFuncionario"]),
                                Nome = sdr["Nome"].ToString(),
                                Sobrenome = sdr["Sobrenome"].ToString(),
                                DataNascimento = DateTime.Parse(data.ToShortDateString())

                            };
                            listaFuncionarios.Add(func);
                        }
                    }
                
                return listaFuncionarios;
            }
        }
    }
}
