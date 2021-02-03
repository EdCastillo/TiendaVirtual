using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Views.Models
{
    public class Imagen
    {
        public int IMG_ID { get; set; }
        public int PRO_ID { get; set; }
        public string IMG_NOMBRE { get; set; }
        public string IMG_FORMATO { get; set; }
        public string IMG_FILE { get; set; }
    }
}