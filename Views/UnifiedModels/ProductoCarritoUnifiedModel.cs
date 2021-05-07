using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Views.UnifiedModels
{
    public class ProductoCarritoUnifiedModel
    {
        public int PCR_ID { get; set; }
        public ProductoUnifiedModel producto { get; set; }
        public int CAR_PRO_CANTIDAD { get; set; }
        
    }
}