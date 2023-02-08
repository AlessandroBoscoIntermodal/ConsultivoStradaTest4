using System;
using System.Collections.Generic;

namespace ConsultivoStradaTest4.Models.CS
{
    public partial class CostoFisso
    {
        public string FkeyVeicolo { get; set; }
        public int FkeyCausale { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public int KeyDataInizio { get; set; }
    }
}
