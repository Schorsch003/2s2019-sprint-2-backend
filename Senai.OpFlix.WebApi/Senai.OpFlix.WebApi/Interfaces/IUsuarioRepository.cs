using Senai.OpFlix.WebApi.Domains;
using Senai.OpFlix.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces {
    public interface IUsuarioRepository {
        List<Usuarios> ListarUsuarios ();
        List<Lancamentos> BuscarLancamentosFavoritos (int idUsuario);
        Usuarios BuscarPorId (int id);
        Usuarios BuscarPorEmailESenha (LoginViewModel login);
        void CadastrarUsuarios (Usuarios user, bool permissao);
        void AtualizarUsuarios (int id , Usuarios user);
        void RemoverUsuarios (int id);
        void FavoritarLancamento (int idUsuario , int idLancamento);
    }
}
