using System.Collections.Generic;

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