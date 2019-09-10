using Senai.OpFlix.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Interfaces {
    public interface IPlataformaRepository {
        List<Plataformas> ListarPlataformas ();
        Plataformas BuscarPlataformaPorId (int id);
        void CadastrarPlataforma (Plataformas plat);
        void RemoverPlataforma (int id);
        void AtualizarPlataforma (int id , Plataformas plat);

    }
}
