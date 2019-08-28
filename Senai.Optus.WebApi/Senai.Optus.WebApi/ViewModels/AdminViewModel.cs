using Senai.Optus.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Optus.WebApi.ViewModels {
    public class AdminViewModel {
        public List<Estilos> Estilos { get; set; }
        public List<Artistas> Artistas { get; set; }
    }
}
