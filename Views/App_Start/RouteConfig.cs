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
                name:"Home",
                url:"Home",
                defaults:new { controller="Home",action="Index" });
            routes.MapRoute(name: "Imagenes", url: "Imagen", defaults: new { controller = "Imagen", action = "New" });
            routes.MapRoute(
                name:"Carrito",
                url:"Carrito/{action}",
                defaults:new { controller="Carrito"});
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
                defaults: new { controller = "Login",action="Login" }
            );
            routes.MapRoute(
                name: "Authenticate", url: "Login/Authenticate",
                defaults: new { controller = "Login", action = "Authenticate" }
            );


        }
    }
}
