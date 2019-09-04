    using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Interfaces {
    public interface IUsuarioRepository {
        Usuarios BuscarPorEmailESenha (LoginViewModel login);
        Usuarios BuscarPorId (int id);
        List<Usuarios> ListarUsuarios ();
    }
}
