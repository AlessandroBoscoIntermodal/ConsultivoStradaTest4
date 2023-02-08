using System;
using System.Collections.Generic;

namespace ConsultivoStradaTest4.Models.CS
{
    public partial class VeicoliAziendaliView
    {
        public string IdveicoloTarga { get; set; }
        public string FkeyTipoVeicolo { get; set; }
        public string TipoVeicolo { get; set; }
        public int FkeyContainerDescrizione { get; set; }
        public string Container { get; set; }
    }
}
