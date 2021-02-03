using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
                dynamic products = new ExpandoObject();
                products.Producto = product;
                return View(products);
            }
            else {
                return View(VG.usuarioActual);
            }
        }
        public  ActionResult ManageShowProduct(int id) {
            if(VG.usuarioActual == null)//!=
            {

                return View();
            }
            else
            {
                return Redirect("~/Producto/AllProducts");
            }
        }
        public async Task<ActionResult> ManagePutProduct(int id)
        {

            return View();
        }

    }
}