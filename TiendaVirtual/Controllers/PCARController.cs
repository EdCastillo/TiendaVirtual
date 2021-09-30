using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    [Authorize]//Authorize
    [RoutePrefix("api/pcar")]
    public class PCARController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllByUserId(int id)
        {
            if (id == 0) { return BadRequest(); }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PRO_ID,CAR_PRO_CANTIDAD,PCR_ID FROM PRODUCTO_CARRITO WHERE US_ID=@ID;", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    List<PRODUCTO_CARRITO> productos = new List<PRODUCTO_CARRITO>();
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        productos.Add(new PRODUCTO_CARRITO
                        {
                            PRO_ID = sqlDataReader.GetInt32(0),
                            CAR_PRO_CANTIDAD = sqlDataReader.GetInt32(1),
                            PCR_ID = sqlDataReader.GetInt32(2)
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
        public IHttpActionResult PublicInsert(PRODUCTO_CARRITO PRODUCTO)
        {
            if (PRODUCTO == null) { return BadRequest(); }
            if (PrivateInsert(PRODUCTO)) {
                PRODUCTO.PCR_ID = getPCRByParameters(PRODUCTO.US_ID, PRODUCTO.PRO_ID);
                return Ok(PRODUCTO); }
            else { return InternalServerError(); }
        }
        private bool PrivateInsert(PRODUCTO_CARRITO PRODUCTO)
        {
            PRODUCTO_CARRITO car=new PRODUCTO_CARRITO();
            car.PCR_ID = 0;
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection())) {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT PCR_ID,US_ID,PRO_ID FROM PRODUCTO_CARRITO WHERE PRO_ID=@PRO_ID AND US_ID=@US_ID", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", PRODUCTO.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@US_ID", PRODUCTO.US_ID);
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read()) {
                    car = new PRODUCTO_CARRITO {US_ID=reader.GetInt32(1),PCR_ID=reader.GetInt32(0),PRO_ID=reader.GetInt32(2) };
                }
                sqlConnection.Close();
            }
            if (car.PCR_ID == 0)
            {
                using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO PRODUCTO_CARRITO(PRO_ID,CAR_PRO_CANTIDAD,US_ID) VALUES(@PRO_ID,@COM_PRO_CANTIDAD,@US_ID);", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@US_ID", PRODUCTO.US_ID);
                    sqlCommand.Parameters.AddWithValue("@PRO_ID", PRODUCTO.PRO_ID);
                    sqlCommand.Parameters.AddWithValue("@COM_PRO_CANTIDAD", PRODUCTO.CAR_PRO_CANTIDAD);
                    sqlConnection.Open();
                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (filasAfectadas > 0) { return true; }
                    else { return false; }
                }
            }
            else {
                using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection())) {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE PRODUCTO_CARRITO SET CAR_PRO_CANTIDAD=@CANTIDAD WHERE PCR_ID=@ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", car.PCR_ID);
                    sqlCommand.Parameters.AddWithValue("@CANTIDAD", PRODUCTO.CAR_PRO_CANTIDAD);
                    sqlConnection.Open();
                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (filasAfectadas > 0)
                    {
                        return true;
                    }
                    else { return false; }
                }
                
            }
            
        }
        public int getPCRByParameters(int US_ID, int PRO_ID) {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection())) {
                int result = 0;
                SqlCommand sqlCommand = new SqlCommand(@"SELECT PCR_ID,US_ID,PRO_ID FROM PRODUCTO_CARRITO WHERE PRO_ID=@PRO_ID AND US_ID=@US_ID", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", PRO_ID);
                sqlCommand.Parameters.AddWithValue("@US_ID",US_ID);
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
                sqlConnection.Close();
                return result;
            }

        }
        [HttpDelete]
        [Route("deleteByUser")]
        public IHttpActionResult DeleteAllByUserID(int id) {
            if (id == 0)
            {
                return BadRequest();
            }
            else {
                if (PrivateDeleteByUser(id)) {
                    return Ok();   
                }
                else
                {
                    return InternalServerError();
                }
            }
        
        }
        private bool PrivateDeleteByUser(int id) {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection())) {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE PRODUCTO_CARRITO WHERE US_ID=@ID",sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID",id);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
        }


        [HttpDelete]
        public IHttpActionResult PublicDelete(int id)
        {
            if (id == 0) { return BadRequest(); }
            if (PrivateDelete(id)) { return Ok(); }
            else { return Ok(id); }
        }
        private bool PrivateDelete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
            {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE PRODUCTO_CARRITO WHERE PCR_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", id);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }

        [HttpPut]
        public IHttpActionResult PublicUpdate(PRODUCTO_CARRITO PRODUCTO)
        {
            if (PRODUCTO == null) { return BadRequest(); }
            if (PrivateUpdate(PRODUCTO)) { return Ok(); }
            else { return InternalServerError(); }
        }
        public bool PrivateUpdate(PRODUCTO_CARRITO PRODUCTO)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE PRODUCTO_CARRITO SET PRO_ID=@PRO_ID,CAR_PRO_CANTIDAD=@COM_PRO_CANTIDAD WHERE PCR_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", PRODUCTO.PCR_ID);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", PRODUCTO.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@COM_PRO_CANTIDAD", PRODUCTO.CAR_PRO_CANTIDAD);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }
    }
}
