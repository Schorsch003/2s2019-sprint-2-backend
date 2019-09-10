using System;
using System.Collections.Generic;

namespace Senai.OpFlix.WebApi.Domains
{
    public partial class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public int? IdPermissao { get; set; }
        public string Imagem { get; set; }

        public Permissoes IdPermissaoNavigation { get; set; }
        public List<FavoritosUsuarios> FavoritosUsuarios { get; set; }
    }
}
