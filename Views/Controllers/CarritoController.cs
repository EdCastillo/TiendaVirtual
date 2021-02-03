using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Views.Models;

namespace Views.Controllers
{
    public class CarritoController : Controller
    {
        public ActionResult AñadirProducto(int id)
        {
            if (VG.usuarioActual == null)
            {

                return RedirectToRoute(new { controller = "Login", action = "Login" ,Valid="false"});
            }
            else
            {
                return RedirectToRoute(new { controller = "Producto", action = "AllProducts" });
            }
        }
    }
}