using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Views.Models;

namespace Views.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login(string Valid)
        {
            if (!String.IsNullOrEmpty(Valid))
            {
                if (Valid == "false") {
                    return View(new PopUp { Estado = "false", Titulo = "Hola!", Body = "Para agregar un producto en el carrito tienes que estar registrado!" });
                }
                else{ return View(new PopUp { Estado = "true", Titulo = "null", Body = "null" }); }
            }
            else
            {
                return View(new PopUp { Estado="true",Titulo="null",Body="null"});
            }
        }
        public ActionResult Authenticate() {
            return View();
        }
    }
}