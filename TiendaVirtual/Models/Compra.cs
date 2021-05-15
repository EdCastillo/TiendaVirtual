using System;

namespace TiendaVirtual.Models
{
    public class Compra
    {
        //COM_ID,COM_FECHA_COMPRA,US_ID,COM_LUGAR_DE_ENVIO
        public int COM_ID { get; set; }
        public DateTime COM_FECHA_COMPRA { get; set; }
        public int US_ID { get; set; }
        public string COM_LUGAR_DE_ENVIO { get; set; }
        public string PayPal_Token { get; set; }
        public string PayPal_PayerID { get; set; }
    }
}