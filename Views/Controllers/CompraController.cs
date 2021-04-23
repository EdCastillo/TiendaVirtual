using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Views.Managers;
using Views.Models;

namespace Views.Controllers
{
    public class CompraController : Controller
    {
        public async Task<ActionResult> VerCompras(int id,string token)
        {
            CompraManager manager = new CompraManager();
            IEnumerable<Compra> compras=await manager.getAllCompras(id.ToString(),token);
            PCManager pcManager = new PCManager();
            IEnumerable<Producto_Compra> items= await pcManager.getAllItemsByCompraID(id.ToString(),token);
            dynamic model = new ExpandoObject();
            model.Compra = compras;
            model.Items = items;
            return View(model);
        }
    }
}