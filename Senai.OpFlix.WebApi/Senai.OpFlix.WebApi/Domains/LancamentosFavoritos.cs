using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Domains {
    public class LancamentosFavoritos {
        public int IdLancamento { get; set; }
        public int IdUsuario { get; set; }

        public Lancamentos Lancamento { get; set; }
        public Usuarios Usuario { get; set; }
    }
}
