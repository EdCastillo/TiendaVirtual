using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Views.Models
{
    public class UserCookie
    {
        public int US_ID { get; set; }
        public string Username { get; set; }
        public string JsonToken { get; set; }
        public List<Carrito> Products { get; set; }
    }
}