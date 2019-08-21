using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T_People.Domains {
    public class FuncionarioDomain {
        public int IdFuncionario{ get; set; }
        [Required(ErrorMessage ="O Nome é obrigatório")]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
