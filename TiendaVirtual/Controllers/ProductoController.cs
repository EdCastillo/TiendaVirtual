using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    [RoutePrefix("api/producto")]
    public class ProductoController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetProductoById(int id) {
            
        
        }
    }
}