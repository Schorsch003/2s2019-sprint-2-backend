using Senai.OpFlix.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces {
    public interface ICategoriaRepository {
        List<Categoria> ListarCategorias ();
        Categoria BuscarCategoriaPorId (int id);
        void CadastrarCategoria (Categoria cat);

    }
}
