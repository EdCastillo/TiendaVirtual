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
    [AllowAnonymous]//Authorize
    [RoutePrefix("api/pcar")]
    public class PCARController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllByCompraId(int id)
        {
            if (id == 0) { return BadRequest(); }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT CAR_ID,PRO_ID,CAR_PRO_CANTIDAD,PCR_ID FROM Producto_Carrito WHERE PCR_ID=@ID;", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    List<Producto_Carrito> productos = new List<Producto_Carrito>();
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        productos.Add(new Producto_Carrito
                        {

                            CAR_ID = sqlDataReader.GetInt32(0),
                            PRO_ID = sqlDataReader.GetInt32(1),
                            CAR_PRO_CANTIDAD = sqlDataReader.GetInt32(2),
                            PCR_ID = sqlDataReader.GetInt32(3)
                        });
                    }
                    sqlConnection.Close();
                    return Ok(productos);
                }
            }
            catch
            {
                return InternalServerError();
            }

        }
        [HttpPost]
        [Route("insertar")]
        public IHttpActionResult PublicInsert(Producto_Carrito producto)
        {
            if (producto == null) { return BadRequest(); }
            if (PrivateInsert(producto)) { return Ok(); }
            else { return InternalServerError(); }
        }
        private bool PrivateInsert(Producto_Carrito producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Producto_Carrito(CAR_ID,PRO_ID,CAR_PRO_CANTIDAD) VALUES(@COM_ID,@PRO_ID,@COM_PRO_CANTIDAD);", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@COM_ID", producto.CAR_ID);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", producto.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@COM_PRO_CANTIDAD", producto.CAR_PRO_CANTIDAD);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }

        [HttpDelete]
        public IHttpActionResult PublicDelete(int id)
        {
            if (id == 0) { return BadRequest(); }
            if (PrivateDelete(id)) { return Ok(); }
            else { return InternalServerError(); }
        }
        private bool PrivateDelete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE Producto_Carrito WHERE PCR_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", id);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }

        [HttpPut]
        public IHttpActionResult PublicUpdate(Producto_Carrito producto)
        {
            if (producto == null) { return BadRequest(); }
            if (PrivateUpdate(producto)) { return Ok(); }
            else { return InternalServerError(); }
        }
        public bool PrivateUpdate(Producto_Carrito producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE Producto_Carrito SET CAR_ID=@COM_ID,PRO_ID=@PRO_ID,CAR_PRO_CANTIDAD=@COM_PRO_CANTIDAD WHERE PCR_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", producto.PCR_ID);
                sqlCommand.Parameters.AddWithValue("@COM_ID", producto.CAR_ID);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", producto.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@COM_PRO_CANTIDAD", producto.CAR_PRO_CANTIDAD);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }
    }
}
