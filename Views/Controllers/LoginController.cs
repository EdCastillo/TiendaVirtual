using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Views.Managers;
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
                if (Valid == "RValid") {
                    return View(new PopUp { Estado = "true", Titulo = "Bienvenido(a)!", Body = "Registrado correctamente! Por favor, ahora inicie sesion" });    
                }
                if (Valid == "RNotValid")
                {
                    return View(new PopUp { Estado = "true", Titulo = "Error!", Body = "Debe de llenar todos los espacios!" });
                }
                if (Valid == "true") {
                    return View(new PopUp { Estado = "true", Titulo = "Hola!", Body = "Sesion iniciada correctamente!" });
                }
                if (Valid == "false") {
                    return View(new PopUp { Estado = "true", Titulo = "Hola!", Body = "Credenciales Incorrectas" });
                }
                else
                {
                    return View(new PopUp { Estado = "false", Titulo = "null", Body = "null" });
                }
            }
            else
            {
                return View(new PopUp { Estado="false",Titulo="null",Body="null"});
            }
        }
        public async Task<ActionResult> Registrar(string nombre, string correo, string usuario, string contrasena)
        {
            if (!(nombre == null || correo == null || usuario == null || contrasena == null))
            {
                UsuarioManager manager = new UsuarioManager();
                var result = await manager.Registrar(new Usuario { US_NOMBRE = nombre, US_CORREO = correo, US_CONTRASENA = contrasena, US_USUARIO = usuario });
                return Redirect("~/Login?Valid=RValid");
            }
            else {
                return Redirect("~/Login?Valid=RNotValid");
            }
        }


        public async Task<ActionResult> Authorize(string username, string password) {
            UsuarioManager manager = new UsuarioManager();
            Usuario usuario=await manager.Validar(username,password);
            if (usuario == null) {
                return Redirect("~/Login?Valid=false");
            }
            else {
                return View(usuario);
            }
            
            
        
        }
        public ActionResult Logout() {
            return View();
        }

    }
}