using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class Producto_Compra
    {
        public int CM_ID { get; set; }
        public int COM_ID { get; set; }
        public int PRO_ID { get; set; }
        public int COM_PRO_CANTIDAD { get; set; }
    }
}