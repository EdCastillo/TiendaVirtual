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
    [RoutePrefix("api/carrito")]
    [AllowAnonymous]
    public class CarritoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetCarritoByUserID(int id) {
                if (id == 0) { return BadRequest(); }
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"SELECT CAR_ID,US_ID FROM CARRITO WHERE US_ID=@ID", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@ID", id);
                        sqlConnection.Open();
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                        Carrito compra = new Carrito();
                        while (sqlDataReader.Read())
                        {
                            compra = new Carrito
                            {
                                CAR_ID = sqlDataReader.GetInt32(0),
                                US_ID=sqlDataReader.GetInt32(1)
                            };
                        }
                        sqlConnection.Close();
                        if (compra.CAR_ID == 0) { return BadRequest(); }
                        else { return Ok(compra); }
                    }
                }
                catch
                {
                    return InternalServerError();
                }

        }
    }
}
