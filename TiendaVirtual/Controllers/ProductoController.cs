using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    [RoutePrefix("api/PRODUCTO")]
    public class ProductoController : ApiController
    {

        //PRO_ID,PRO_NOMBRE,PRO_MARCA,PRO_STOCK,PRO_PRECIO,PRO_DESCRIPCION


        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetProductoById(int id)
        {
            if (id == 0) { return BadRequest(); }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PRO_ID,PRO_NOMBRE,PRO_MARCA,PRO_STOCK,PRO_PRECIO,PRO_DESCRIPCION FROM PRODUCTO WHERE PRO_ID=@ID;", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlConnection.Open();
                    PRODUCTO PRODUCTO = new PRODUCTO();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        PRODUCTO = new PRODUCTO
                        {
                            PRO_ID = sqlDataReader.GetInt32(0),
                            PRO_NOMBRE = sqlDataReader.GetString(1),
                            PRO_MARCA = sqlDataReader.GetString(2),
                            PRO_STOCK = sqlDataReader.GetInt32(3),
                            PRO_PRECIO = sqlDataReader.GetDecimal(4),
                            PRO_DESCRIPCION = sqlDataReader.GetString(5)
                        };
                    }
                    sqlConnection.Close();
                    if (PRODUCTO.PRO_ID == 0) { return BadRequest(); }
                    else { return Ok(PRODUCTO); }
                }
            }
            catch
            {
                return InternalServerError();
            }

        }
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAllProductos()
        {

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PRO_ID,PRO_NOMBRE,PRO_MARCA,PRO_STOCK,PRO_PRECIO,PRO_DESCRIPCION FROM PRODUCTO;", sqlConnection);
                    sqlConnection.Open();
                    List<PRODUCTO> productos = new List<PRODUCTO>();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        productos.Add(new PRODUCTO
                        {
                            PRO_ID = sqlDataReader.GetInt32(0),
                            PRO_NOMBRE = sqlDataReader.GetString(1),
                            PRO_MARCA = sqlDataReader.GetString(2),
                            PRO_STOCK = sqlDataReader.GetInt32(3),
                            PRO_PRECIO = sqlDataReader.GetDecimal(4),
                            PRO_DESCRIPCION = sqlDataReader.GetString(5)
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
        [Authorize]//Authorize
        [Route("ingresar")]
        public IHttpActionResult PublicInsert(PRODUCTO PRODUCTO)
        {
            if (PRODUCTO == null) { return BadRequest(); }
            if (PrivateInsert(PRODUCTO)) { return Ok(PRODUCTO); }
            else { return InternalServerError(); }
        }

        private bool PrivateInsert(PRODUCTO PRODUCTO)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO PRODUCTO(PRO_NOMBRE,PRO_MARCA,PRO_STOCK,PRO_PRECIO,PRO_DESCRIPCION) VALUES(@PRO_NOMBRE,@PRO_MARCA,@PRO_STOCK,@PRO_PRECIO,@PRO_DESCRIPCION);", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PRO_NOMBRE", PRODUCTO.PRO_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@PRO_MARCA", PRODUCTO.PRO_MARCA);
                sqlCommand.Parameters.AddWithValue("@PRO_STOCK", PRODUCTO.PRO_STOCK);
                sqlCommand.Parameters.AddWithValue("@PRO_PRECIO", PRODUCTO.PRO_PRECIO);
                sqlCommand.Parameters.AddWithValue("@PRO_DESCRIPCION", PRODUCTO.PRO_DESCRIPCION);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }

        [HttpDelete]
        [Authorize]//Authorize
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
                SqlCommand sqlCommand = new SqlCommand(@"DELETE PRODUCTO WHERE PRO_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", id);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }

        }
        [HttpPut]
        [Authorize]//Authorize
        public IHttpActionResult PublicUpdate(PRODUCTO PRODUCTO)
        {
            if (PRODUCTO == null) { return BadRequest(); }
            if (PrivateUpdate(PRODUCTO)) { return Ok(); }
            else { return InternalServerError(); }
        }
        private bool PrivateUpdate(PRODUCTO PRODUCTO)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE PRODUCTO SET PRO_NOMBRE=@PRO_NOMBRE,PRO_MARCA=@PRO_MARCA,PRO_STOCK=@PRO_STOCK,PRO_PRECIO=@PRO_PRECIO,PRO_DESCRIPCION=@PRO_DESCRIPCION WHERE PRO_ID=@ID", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", PRODUCTO.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@PRO_NOMBRE", PRODUCTO.PRO_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@PRO_MARCA", PRODUCTO.PRO_MARCA);
                sqlCommand.Parameters.AddWithValue("@PRO_STOCK", PRODUCTO.PRO_STOCK);
                sqlCommand.Parameters.AddWithValue("@PRO_PRECIO", PRODUCTO.PRO_PRECIO);
                sqlCommand.Parameters.AddWithValue("@PRO_DESCRIPCION", PRODUCTO.PRO_DESCRIPCION);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }


    }
}