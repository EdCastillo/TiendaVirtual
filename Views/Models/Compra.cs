using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Views.Models
{
    public class Compra
    {
        public int COM_ID { get; set; }
        public DateTime COM_FECHA_COMPRA { get; set; }
        public int US_ID { get; set; }
        public string COM_LUGAR_DE_ENVIO { get; set; }
    }
}