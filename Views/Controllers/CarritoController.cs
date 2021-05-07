using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Views.Managers;
using Views.Models;
using Views.UnifiedModels;

namespace Views.Controllers
{
    public class CarritoController : Controller
    {
        public async Task<ActionResult> AñadirProductoLog(int id, int cantidad, string userID, string token)
        {
            Producto_Carrito carrito = new Producto_Carrito { PRO_ID = id, CAR_PRO_CANTIDAD = cantidad };
            try
            {
                int idUser = Int32.Parse(userID);
                CarritoManager manager = new CarritoManager();
                var response = await manager.Ingresar(new Producto_Carrito { US_ID = idUser, CAR_PRO_CANTIDAD = cantidad, PRO_ID = id,PCR_ID=0 }, token);
                carrito.US_ID = Int32.Parse(userID);
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
                List<ProductoCarritoUnifiedModel> productoList = new List<ProductoCarritoUnifiedModel>();
                ProductoManager manager = new ProductoManager();
                foreach (var i in list) {
                    ProductoCarritoUnifiedModel model = new ProductoCarritoUnifiedModel();
                    model.PCR_ID = i.PCR_ID;
                    model.producto = new ProductoUnifiedModel();
                    model.CAR_PRO_CANTIDAD = i.CAR_PRO_CANTIDAD;
                    Producto producto = await manager.ObtenerProducto(i.PRO_ID.ToString());
                    model.producto.PRO_MARCA = producto.PRO_MARCA;
                    model.producto.PRO_NOMBRE = producto.PRO_NOMBRE;
                    model.producto.PRO_PRECIO = producto.PRO_PRECIO;
                    model.producto.PRO_CANTIDAD_UNIDADES = i.CAR_PRO_CANTIDAD;
                    model.producto.PRO_DESCRIPCION = producto.PRO_DESCRIPCION;
                    model.producto.PRO_ID = i.PRO_ID;
                    productoList.Add(model);
                }
                return View(productoList);
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