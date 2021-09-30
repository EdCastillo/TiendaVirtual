using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    [RoutePrefix("api/IMAGEN")]
    [AllowAnonymous]
    public class ImagenController : ApiController
    {
        [HttpPost]
        public IHttpActionResult PublicInsert(IMAGEN IMAGEN)
        {
            if (IMAGEN == null) { return BadRequest(); }
            if (PrivateInsert(IMAGEN)) { return Ok(); }
            else { return InternalServerError(); }
        }
        private bool PrivateInsert(IMAGEN IMAGEN)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO IMAGEN(PRO_ID,IMG_NOMBRE,IMG_FORMATO,IMG_FILE) VALUES(@PRO_ID,@IMG_NOMBRE,@IMG_FORMATO,@IMG_FILE);", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", IMAGEN.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@IMG_NOMBRE", IMAGEN.IMG_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@IMG_FORMATO", IMAGEN.IMG_FORMATO);
                sqlCommand.Parameters.AddWithValue("@IMG_FILE", "");
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
                SqlCommand sqlCommand = new SqlCommand(@"DELETE IMAGEN WHERE IMG_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", id);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }


    }
}
