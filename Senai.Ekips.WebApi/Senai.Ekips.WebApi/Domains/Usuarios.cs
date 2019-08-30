using System;
using System.Collections.Generic;

namespace Senai.Ekips.WebApi.Domains
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Funcionarios = new HashSet<Funcionarios>();
        }

        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? IdPermissao { get; set; }

        public Permissoes IdPermissaoNavigation { get; set; }
        public ICollection<Funcionarios> Funcionarios { get; set; }
    }
}
