using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TiendaVirtual
{
    public class Utilities
    {
        public static string GetConnection() {
            string SELECT="Local";
            return ConfigurationManager.ConnectionStrings[SELECT].ConnectionString;
        }
    }
}