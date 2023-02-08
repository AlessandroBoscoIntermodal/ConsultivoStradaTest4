using System;
using System.Collections.Generic;

namespace ConsultivoStradaTest4.Models.CS
{
    public partial class AbbinamentoTrattore
    {
        public string FkeyTrattore { get; set; }
        public int? FkeyAutista { get; set; }
        public string FkeyGestore { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public int KeyDataInizio { get; set; }
    }
}
