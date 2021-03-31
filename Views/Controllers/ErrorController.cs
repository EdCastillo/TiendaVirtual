using System.Web.Mvc;

namespace Views.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HandleError]
        public ActionResult NotFound()
        {
            return View();
        }
        [HandleError]
        public ActionResult BadRequest()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}