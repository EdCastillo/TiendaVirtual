using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{

    [AllowAnonymous]//Authorize
    [RoutePrefix("api/pc")]
    public class PCController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetAllByCompraId(int id)
        {
            if (id == 0) { return BadRequest(); }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT COM_ID,PRO_ID,COM_PRO_CANTIDAD,CM_ID,CM_PRECIO_PRODUCTO_UNIDAD FROM PRODUCTO_COMPRA WHERE COM_ID=@ID;", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    List<Producto_Compra> productos = new List<Producto_Compra>();
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        productos.Add(new Producto_Compra
                        {
                            COM_ID = sqlDataReader.GetInt32(0),
                            PRO_ID = sqlDataReader.GetInt32(1),
                            COM_PRO_CANTIDAD = sqlDataReader.GetInt32(2),
                            CM_ID = sqlDataReader.GetInt32(3),
                            CM_PRECIO_PRODUCTO_UNIDAD=sqlDataReader.GetInt32(4)
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
        public IHttpActionResult PublicInsert(Producto_Compra producto)
        {
            if (producto == null) { return BadRequest(); }
            if (PrivateInsert(producto)) { return Ok(); }
            else { return InternalServerError(); }
        }
        private bool PrivateInsert(Producto_Compra producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO PRODUCTO_COMPRA(COM_ID,PRO_ID,COM_PRO_CANTIDAD) VALUES(@COM_ID,@PRO_ID,@COM_PRO_CANTIDAD);", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@COM_ID", producto.COM_ID);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", producto.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@COM_PRO_CANTIDAD", producto.COM_PRO_CANTIDAD);
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
            return Ok("Si");
            if (id == 0) { return BadRequest(); }
            if (PrivateDelete(id)) { return Ok(); }
            else { return InternalServerError(); }
        }
        private bool PrivateDelete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE PRODUCTO_COMPRA WHERE CM_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@CM_ID", id);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }

        [HttpPut]
        public IHttpActionResult PublicUpdate(Producto_Compra producto)
        {
            if (producto == null) { return BadRequest(); }
            if (PrivateUpdate(producto)) { return Ok(); }
            else { return InternalServerError(); }
        }
        public bool PrivateUpdate(Producto_Compra producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE PRODUCTO_COMPRA SET COM_ID=@COM_ID,PRO_ID=@PRO_ID,COM_PRO_CANTIDAD=@COM_PRO_CANTIDAD WHERE CM_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", producto.CM_ID);
                sqlCommand.Parameters.AddWithValue("@COM_ID", producto.COM_ID);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", producto.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@COM_PRO_CANTIDAD", producto.COM_PRO_CANTIDAD);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }

    }
}