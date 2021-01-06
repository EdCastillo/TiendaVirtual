using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vista.Models
{
    public class Usuario
    {
        public int US_ID { get; set; }
        public string US_NOMBRE { get; set; }
        public string US_CORREO { get; set; }
        public string US_USUARIO { get; set; }
        public string US_CONTRASEÑA { get; set; }
    }
}