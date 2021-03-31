using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Views.Managers;
using Views.Models;

namespace Views.Controllers
{
    public class CarritoController : Controller
    {
        public async Task<ActionResult> AñadirProductoLog(int id, int cantidad, string userID, string token)
        {
            Carrito carrito = new Carrito { PRO_ID = id, PRO_CANTIDAD = cantidad };
            try
            {
                Thread.Sleep(3000);
                int idUser = Int32.Parse(userID);
                CarritoManager manager = new CarritoManager();
                var response = await manager.Ingresar(new Producto_Carrito { US_ID = idUser, CAR_PRO_CANTIDAD = cantidad, PRO_ID = id,PCR_ID=0 }, token);
                return View(carrito);
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