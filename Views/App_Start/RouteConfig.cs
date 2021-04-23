using System.Web.Mvc;
using System.Web.Routing;

namespace Views
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //TEST PAGE
            routes.MapRoute(
                name: "Test",
                url: "Test",
                defaults: new { controller = "Test", action = "Index" });

            //LOGIN CONTROLLER
            routes.MapRoute(
                name: "LoginRoutes",
                url: "Login/{action}",
                defaults: new { controller = "Login" }
                );

            routes.MapRoute(
                name: "Login", url: "Login",
                defaults: new { controller = "Login", action = "Login" }
            );
            routes.MapRoute(
                name: "Registro", url: "Login/Registrar",
                defaults: new { controller = "Login", action = "Registrar" }
            );
            routes.MapRoute(
                name: "Logout", url: "Login/logout",
                defaults: new { controller = "Login", action = "Logout" }
            );

            //GENERAL
            routes.MapRoute(
                name: "Home",
                url: "Home",
                defaults: new { controller = "Home", action = "Index" });
            routes.MapRoute(
                name: "Error", url: "Error/{action}",
                defaults: new { controller = "Error" }
            );
            //PRODUCTO CONTROLLER
            routes.MapRoute(name: "Imagenes", url: "Imagen", defaults: new { controller = "Imagen", action = "New" });

            routes.MapRoute(
                name: "Producto",
                url: "producto/{action}",
                defaults: new { controller = "Producto" }
            );

            //CARRITO CONTROLLER
            routes.MapRoute(
                name: "Carrito",
                url: "Carrito/Add",
                defaults: new { controller = "Carrito", action = "AñadirProducto" });
            routes.MapRoute(
                name: "CarritoLog",
                url: "Carrito/AddLog",
                defaults: new { controller = "Carrito", action = "AñadirProductoLog" });
            routes.MapRoute(
                name:"VerCarrito",
                url: "Carrito/Show",
                defaults: new { controller="Carrito",action="VerCarrito"}
                );
            routes.MapRoute(
                name:"BorrarDeCarrito",
                url:"Carrito/Borrar",
                defaults: new { controller="Carrito",action="BorrarCarrito"}
                );


            //COMPRA CONTROLLER
            routes.MapRoute(
                name:"VerCompras",
                url:"Usuario/Compras",
                defaults:new { controller="Compra",action="VerCompras"}
                );






        }
    }
}
