using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaVirtual.UnifiedModels
{
    public class FullBody
    {
        public string intent { get; set; }
        public List<PurchaseUnit> purchase_units { get; set; }
        public ApplicationContext application_context { get; set; }
    }
}