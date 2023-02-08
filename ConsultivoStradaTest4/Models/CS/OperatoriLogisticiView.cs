using System;
using System.Collections.Generic;

namespace ConsultivoStradaTest4.Models.CS
{
    public partial class OperatoriLogisticiView
    {
        public string Idutente { get; set; }
        public string Nominativo { get; set; }
        public string EmailAziendale { get; set; }
        public int? FkeyAnagrafica { get; set; }
    }
}
