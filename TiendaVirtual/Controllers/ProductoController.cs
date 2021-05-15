using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    [RoutePrefix("api/producto")]
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
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PRO_ID,PRO_NOMBRE,PRO_MARCA,PRO_STOCK,PRO_PRECIO,PRO_DESCRIPCION FROM PRODUCTO WHERE PRO_ID=@ID;", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlConnection.Open();
                    Producto producto = new Producto();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        producto = new Producto
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
                    if (producto.PRO_ID == 0) { return BadRequest(); }
                    else { return Ok(producto); }
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
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PRO_ID,PRO_NOMBRE,PRO_MARCA,PRO_STOCK,PRO_PRECIO,PRO_DESCRIPCION FROM PRODUCTO;", sqlConnection);
                    sqlConnection.Open();
                    List<Producto> productos = new List<Producto>();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        productos.Add(new Producto
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
        public IHttpActionResult PublicInsert(Producto producto)
        {
            if (producto == null) { return BadRequest(); }
            if (PrivateInsert(producto)) { return Ok(producto); }
            else { return InternalServerError(); }
        }

        private bool PrivateInsert(Producto producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO PRODUCTO(PRO_NOMBRE,PRO_MARCA,PRO_STOCK,PRO_PRECIO,PRO_DESCRIPCION) VALUES(@PRO_NOMBRE,@PRO_MARCA,@PRO_STOCK,@PRO_PRECIO,@PRO_DESCRIPCION);", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PRO_NOMBRE", producto.PRO_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@PRO_MARCA", producto.PRO_MARCA);
                sqlCommand.Parameters.AddWithValue("@PRO_STOCK", producto.PRO_STOCK);
                sqlCommand.Parameters.AddWithValue("@PRO_PRECIO", producto.PRO_PRECIO);
                sqlCommand.Parameters.AddWithValue("@PRO_DESCRIPCION", producto.PRO_DESCRIPCION);
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
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
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
        public IHttpActionResult PublicUpdate(Producto producto)
        {
            if (producto == null) { return BadRequest(); }
            if (PrivateUpdate(producto)) { return Ok(); }
            else { return InternalServerError(); }
        }
        private bool PrivateUpdate(Producto producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE PRODUCTO SET PRO_NOMBRE=@PRO_NOMBRE,PRO_MARCA=@PRO_MARCA,PRO_STOCK=@PRO_STOCK,PRO_PRECIO=@PRO_PRECIO,PRO_DESCRIPCION=@PRO_DESCRIPCION WHERE PRO_ID=@ID", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", producto.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@PRO_NOMBRE", producto.PRO_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@PRO_MARCA", producto.PRO_MARCA);
                sqlCommand.Parameters.AddWithValue("@PRO_STOCK", producto.PRO_STOCK);
                sqlCommand.Parameters.AddWithValue("@PRO_PRECIO", producto.PRO_PRECIO);
                sqlCommand.Parameters.AddWithValue("@PRO_DESCRIPCION", producto.PRO_DESCRIPCION);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }


    }
}