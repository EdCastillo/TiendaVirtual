using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Views.UnifiedModels
{
    public class ProductoUnifiedModel
    {
        public int PRO_ID { get; set; }
        public string PRO_NOMBRE { get; set; }
        public string PRO_MARCA { get; set; }
        public Decimal PRO_PRECIO { get; set; }
        public int PRO_CANTIDAD_UNIDADES { get; set; }
        public string PRO_DESCRIPCION { get; set; }
    }
}