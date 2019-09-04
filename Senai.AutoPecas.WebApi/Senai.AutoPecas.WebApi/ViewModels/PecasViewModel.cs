using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.ViewModels {
    public class PecasViewModel {
        public int QuantidadePecas { get; set; }
        public decimal? PrecoCusto { get; set; }
        public decimal? PrecoVenda { get; set; }
    }
}
