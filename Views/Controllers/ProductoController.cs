using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Views.Managers;
using Views.Models;

namespace Views.Controllers
{
    public class ProductoController : Controller
    {

        public async Task<ActionResult> AllProducts()
        {

            ProductoManager manager = new ProductoManager();
            IEnumerable<Producto> product = await manager.ObtenerProductos();
            return View(product);

        }
        public async Task<ActionResult> ShowProduct(int id)
        {

            ProductoManager manager = new ProductoManager();
            Producto product = await manager.ObtenerProducto(id.ToString());
            dynamic products = new ExpandoObject();
            products.Producto = product;
            return View(products);

        }
        public ActionResult ManageShowProduct(int id)
        {

            return Redirect("~/Producto/AllProducts");

        }
        public async Task<ActionResult> ManagePutProduct(int id)
        {

            return View();
        }

    }
}