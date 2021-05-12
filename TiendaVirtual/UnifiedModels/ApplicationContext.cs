using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaVirtual.UnifiedModels
{
    public class ApplicationContext
    {
        public string brand_name { get; set; }
        public string landing_page { get; set; }
        public string user_action { get; set; }
        public string return_url { get; set; }
        public string cancel_url { get; set; }
    }
}