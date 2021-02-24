using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Views.Managers;
using Views.Models;

namespace Views.Controllers
{
    public class CarritoController : Controller
    {
        public async Task<ActionResult> AñadirProducto(int id, int cantidad, string userID, string token)
        {
            Carrito carrito = new Carrito { PRO_ID = id, PRO_CANTIDAD = cantidad };
            try
            {
                int idUser = Int32.Parse(userID);
                CarritoManager manager = new CarritoManager();
                await manager.Ingresar(new Producto_Carrito { US_ID = idUser, CAR_PRO_CANTIDAD = cantidad, PRO_ID = id }, token);

            }
            catch
            {

            }
            return View(carrito);
        }
        public ActionResult AñadirProducto(int id, int cantidad)
        {
            Carrito carrito = new Carrito { PRO_ID = id, PRO_CANTIDAD = cantidad };
            return View(carrito);
        }
    }
}