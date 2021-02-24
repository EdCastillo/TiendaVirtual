using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Views
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Test",
                url: "Test",
                defaults: new { controller = "Test", action = "Index" });
            routes.MapRoute(
                name: "LoginRoutes",
                url: "Login/{action}",
                defaults: new { controller = "Login" }
                );
            routes.MapRoute(
                name:"Home",
                url:"Home",
                defaults:new { controller="Home",action="Index" });
            routes.MapRoute(name: "Imagenes", url: "Imagen", defaults: new { controller = "Imagen", action = "New" });
            routes.MapRoute(
                name:"Carrito",
                url:"Carrito/Add",
                defaults:new { controller="Carrito",action="AñadirProducto"});
            routes.MapRoute(
                name: "Producto",
                url: "producto/{action}",
                defaults: new { controller = "Producto"}
            );
            routes.MapRoute(
                name:"Error",url:"Error/{action}",
                defaults:new { controller="Error"}
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


        }
    }
}
