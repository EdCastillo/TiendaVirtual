//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TiendaVirtual.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCTO_COMPRA
    {
        public int CM_ID { get; set; }
        public int COM_ID { get; set; }
        public int PRO_ID { get; set; }
        public int COM_PRO_CANTIDAD { get; set; }
        public int CM_PRECIO_PRODUCTO_UNIDAD { get; set; }
    
        public virtual PRODUCTO PRODUCTO { get; set; }
    }
}
