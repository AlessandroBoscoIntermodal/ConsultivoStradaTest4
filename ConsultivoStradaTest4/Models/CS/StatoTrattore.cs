using System;
using System.Collections.Generic;

namespace ConsultivoStradaTest4.Models.CS
{
    public partial class StatoTrattore
    {
        public string FkeyTrattore { get; set; }
        public DateTime ValidoDal { get; set; }
        public DateTime? ValidoAl { get; set; }
        public int KeyValidoDal { get; set; }
    }
}
