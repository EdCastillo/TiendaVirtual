using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViewsTiendaVirtual.Controllers
{
    public class ProductoController : Controller
    {
        
        public ActionResult AllProducts()
        {
            return View();
        }
        public ActionResult Product(int id)
        {
            return View();
        }
    }
}