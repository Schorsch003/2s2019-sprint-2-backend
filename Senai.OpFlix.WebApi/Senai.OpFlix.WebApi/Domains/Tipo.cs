using System;
using System.Collections.Generic;

namespace Senai.OpFlix.WebApi.Domains
{
    public partial class Tipo
    {
        public Tipo()
        {
            Lancamentos = new HashSet<Lancamentos>();
        }

        public int IdTipo { get; set; }
        public string Nome { get; set; }

        public ICollection<Lancamentos> Lancamentos { get; set; }
    }
}
