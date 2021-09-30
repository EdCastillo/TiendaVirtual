using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{

    [Authorize]//Authorize
    [RoutePrefix("api/pc")]
    public class PCController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetAllByCompraId(int id)
        {
            if (id == 0) { return BadRequest(); }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT COM_ID,PRO_ID,COM_PRO_CANTIDAD,CM_ID,CM_PRECIO_PRODUCTO_UNIDAD FROM PRODUCTO_COMPRA WHERE COM_ID=@ID;", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    List<PRODUCTO_COMPRA> productos = new List<PRODUCTO_COMPRA>();
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        productos.Add(new PRODUCTO_COMPRA
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
        public IHttpActionResult PublicInsert(PRODUCTO_COMPRA PRODUCTO)
        {
            if (PRODUCTO == null) { return BadRequest(); }
            if (PrivateInsert(PRODUCTO)) { return Ok(); }
            else { return InternalServerError(); }
        }
        private bool PrivateInsert(PRODUCTO_COMPRA PRODUCTO)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO PRODUCTO_COMPRA(COM_ID,PRO_ID,COM_PRO_CANTIDAD,CM_PRECIO_PRODUCTO_UNIDAD) VALUES(@COM_ID,@PRO_ID,@COM_PRO_CANTIDAD,@CM_PRECIO_PRODUCTO_UNIDAD);", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@COM_ID", PRODUCTO.COM_ID);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", PRODUCTO.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@COM_PRO_CANTIDAD", PRODUCTO.COM_PRO_CANTIDAD);
                sqlCommand.Parameters.AddWithValue("@CM_PRECIO_PRODUCTO_UNIDAD", PRODUCTO.CM_PRECIO_PRODUCTO_UNIDAD);
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
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
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
        public IHttpActionResult PublicUpdate(PRODUCTO_COMPRA PRODUCTO)
        {
            if (PRODUCTO == null) { return BadRequest(); }
            if (PrivateUpdate(PRODUCTO)) { return Ok(); }
            else { return InternalServerError(); }
        }
        public bool PrivateUpdate(PRODUCTO_COMPRA PRODUCTO)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE PRODUCTO_COMPRA SET COM_ID=@COM_ID,PRO_ID=@PRO_ID,COM_PRO_CANTIDAD=@COM_PRO_CANTIDAD WHERE CM_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", PRODUCTO.CM_ID);
                sqlCommand.Parameters.AddWithValue("@COM_ID", PRODUCTO.COM_ID);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", PRODUCTO.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@COM_PRO_CANTIDAD", PRODUCTO.COM_PRO_CANTIDAD);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }

    }
}