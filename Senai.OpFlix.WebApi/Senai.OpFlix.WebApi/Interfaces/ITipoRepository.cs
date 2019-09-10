using Senai.OpFlix.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces {
    public interface ITipoRepository {
        List<Tipo> ListarTipos ();
        Tipo BuscarTipoPorId (int id);
        void CadastrarTipo (Tipo tipo);
        void AtualizarTipo (int id , Tipo type);
        void RemoverTipo (int id);


    }
}
