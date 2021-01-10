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
    [RoutePrefix("api/imagen")]
    [AllowAnonymous]
    public class ImagenController : ApiController
    {
        [HttpPost]
        public IHttpActionResult PublicInsert(Imagen imagen) {
            if (imagen == null) { return BadRequest(); }
            if (PrivateInsert(imagen)) { return Ok(); }
            else { return InternalServerError(); }
        }
        private bool PrivateInsert(Imagen imagen) {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO IMAGEN(PRO_ID,IMG_NOMBRE,IMG_FORMATO,IMG_FILE) VALUES(@PRO_ID,@IMG_NOMBRE,@IMG_FORMATO,@IMG_FILE);", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PRO_ID", imagen.PRO_ID);
                sqlCommand.Parameters.AddWithValue("@IMG_NOMBRE", imagen.IMG_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@IMG_FORMATO", imagen.IMG_FORMATO);
                sqlCommand.Parameters.AddWithValue("@IMG_FILE","");
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }
        [HttpDelete]
        public IHttpActionResult PublicDelete(int id) {
            if (id == 0) { return BadRequest(); }
            if (PrivateDelete(id)) { return Ok(); }
            else { return InternalServerError(); }
        }
        private bool PrivateDelete(int id) {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
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
