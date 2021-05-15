using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Views.Managers;
using Views.Models;
using Views.UnifiedModels;

namespace Views.Controllers
{
    public class CompraController : Controller
    {
        public async Task<ActionResult> VerCompras(int id,string token)
        {
            CompraManager manager = new CompraManager();
            PCManager pcManager = new PCManager();
            ProductoManager productoManager = new ProductoManager();

            IEnumerable<Compra> compras=await manager.getAllCompras(id.ToString(),token);
            List<CompraUnifiedModel> model = new List<CompraUnifiedModel>();
            
            
            foreach (Compra item in compras) {
                model.Add(new CompraUnifiedModel { COM_ID = item.COM_ID, US_ID = item.US_ID,COM_FECHA_COMPRA=item.COM_FECHA_COMPRA,COM_LUGAR_DE_ENVIO=item.COM_LUGAR_DE_ENVIO });
            }
            foreach (var i in model) {
                IEnumerable<Producto_Compra> list = await pcManager.getAllItemsByCompraID(i.COM_ID.ToString(), token);
                List<ProductoUnifiedModel> items = new List<ProductoUnifiedModel>();
                foreach (var a in list) {
                    Producto product = await productoManager.ObtenerProducto(a.PRO_ID.ToString());
                    items.Add(new ProductoUnifiedModel { PRO_ID = a.PRO_ID, PRO_CANTIDAD_UNIDADES = a.COM_PRO_CANTIDAD, PRO_DESCRIPCION = product.PRO_DESCRIPCION, PRO_MARCA = product.PRO_MARCA, PRO_NOMBRE = product.PRO_NOMBRE, PRO_PRECIO = a.CM_PRECIO_PRODUCTO_UNIDAD });
                }
                i.itemList = items;
            }
            return View(model);
        }
        public async Task<ActionResult> EfectuarCompra() {
            return View();
        }

        public async Task<ActionResult> VerCompra(string id, string token) {
            CompraManager manager = new CompraManager();
            PCManager pcManager = new PCManager();
            ProductoManager productoManager = new ProductoManager();
            Compra compra = await manager.GetCompra(id, token);
            IEnumerable<Producto_Compra>list=await pcManager.getAllItemsByCompraID(compra.COM_ID.ToString(),token);
            CompraUnifiedModel model = new CompraUnifiedModel {US_ID=compra.US_ID,COM_FECHA_COMPRA=compra.COM_FECHA_COMPRA,COM_ID=compra.COM_ID,COM_LUGAR_DE_ENVIO=compra.COM_LUGAR_DE_ENVIO };
            List<ProductoUnifiedModel> listFinal = new List<ProductoUnifiedModel>();
            foreach (var i in list) {
                Producto producto = await productoManager.ObtenerProducto(i.PRO_ID.ToString());
                ProductoUnifiedModel productoUnifiedModel = new ProductoUnifiedModel {PRO_ID=producto.PRO_ID,PRO_CANTIDAD_UNIDADES=i.COM_PRO_CANTIDAD,PRO_DESCRIPCION=producto.PRO_DESCRIPCION,PRO_MARCA=producto.PRO_MARCA,PRO_NOMBRE=producto.PRO_NOMBRE,PRO_PRECIO=i.CM_PRECIO_PRODUCTO_UNIDAD };
                listFinal.Add(productoUnifiedModel);
            }
            model.itemList = listFinal;
            return View(model);
        }

        public ActionResult CapturarCompra(string token, string PayerID) {
            dynamic model = new ExpandoObject();
            model.token = token;
            model.PayerId = PayerID;
            return View(model);
        }
    }
}