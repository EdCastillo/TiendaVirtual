﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaVirtual.Models
{
    public class Producto_Carrito
    {
        public int PCR_ID { get; set; }
        public int CAR_ID { get; set; }
        public int PRO_ID { get; set; }
        public int CAR_PRO_CANTIDAD { get; set; }
    }
}