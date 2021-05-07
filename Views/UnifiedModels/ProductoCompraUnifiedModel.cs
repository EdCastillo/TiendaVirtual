using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Views.Models
{
    public class ProductoCompraUnifiedModel
    {
        public int CM_ID { get; set; }
        public int COM_ID { get; set; }
        public Producto producto { get; set; }
        public int COM_PRO_CANTIDAD { get; set; }
        public int CM_PRECIO_PRODUCTO_UNIDAD { get; set; }
    }
}