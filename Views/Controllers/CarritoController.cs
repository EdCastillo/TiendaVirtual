using System;
using System.Collections.Generic;
using System.Dynamic;
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
            Producto_Carrito carrito = new Producto_Carrito { PRO_ID = id, CAR_PRO_CANTIDAD = cantidad };
            return View(carrito);
        }

        public async Task<ActionResult> VerCarrito(string json) {
            if (json.Equals("[]"))
            {
                return View();
            }
            else {
                IEnumerable<Producto_Carrito> list = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Producto_Carrito>>(json);
                List<Producto> productos = new List<Producto>();
                ProductoManager manager = new ProductoManager();
                foreach (var i in list) {
                    Producto p=await manager.ObtenerProducto(i.PRO_ID.ToString());
                    productos.Add(p);
                }
                return View(productos);
            }

            
        }
        public async Task<ActionResult> BorrarCarrito(int id, string token) {
            if (token.Equals("notValid")) {
                dynamic model = new ExpandoObject();
                model.Auth = "false";
                model.id = id;
                return View(model);
            }
            else {
                CarritoManager manager = new CarritoManager();
                await manager.Eliminar(id.ToString(),token);
                dynamic model = new ExpandoObject();
                model.Auth = "true";
                model.id = id;
                return View(model);
            }
            
        }

    }
}