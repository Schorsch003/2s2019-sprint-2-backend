using System;
using System.Collections.Generic;

namespace Senai.OpFlix.WebApi.Domains
{
    public partial class Permissoes
    {
        public Permissoes()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        public int IdPermissao { get; set; }
        public string Tipo { get; set; }

        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
