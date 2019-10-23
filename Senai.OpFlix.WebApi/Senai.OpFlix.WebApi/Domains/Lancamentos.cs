using System;
using System.Collections.Generic;

namespace Senai.OpFlix.WebApi.Domains
{
    public partial class Lancamentos
    {
        public int IdLancamento { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public int? IdTipo { get; set; }
        public int? IdCategoria { get; set; }
        public DateTime DataLancamento { get; set; }
        public int TempoDuracao { get; set; }
        public int? Plataforma { get; set; }
        public string Imagem { get; set; }

        public Categoria IdCategoriaNavigation { get; set; }
        public List<LancamentosFavoritos> LancamentosFavoritos { get; set; }
        //public Usuarios Usuario { get; set; }
        public Tipo IdTipoNavigation { get; set; }
        public Plataformas PlataformaNavigation { get; set; }
    }
}
