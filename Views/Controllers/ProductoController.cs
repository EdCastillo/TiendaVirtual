using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Views.Managers;
using Views.Models;

namespace Views.Controllers
{
    public class ProductoController : Controller
    {

        public async Task<ActionResult> AllProducts()
        {
            if (VG.usuarioActual == null)
            {
                ProductoManager manager = new ProductoManager();
                IEnumerable<Producto> product = await manager.ObtenerProductos();
                return View(product);
            }
            else { 
                return View(); 
            }
        }
        public async Task<ActionResult> ShowProduct(int id)
        {
            if (VG.usuarioActual == null)
            {
                ProductoManager manager = new ProductoManager();
                Producto product = await manager.ObtenerProducto(id.ToString());
                return View(product);
            }
            else {
                return View(VG.usuarioActual);
            }
        }
        
    }
}