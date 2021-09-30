using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.OData;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    [EnableQuery]
    [RoutePrefix("api/COMPRA")]
    [AllowAnonymous]//Authorize
    public class CompraController : ApiController
    {
        private TiendaVirtualEntities db = new TiendaVirtualEntities();
        

        [HttpGet]
        [Route("COM_ID")]
        public IHttpActionResult GetCompraByID(int id)
        {
            return Ok(db.COMPRA.F(id));
            if (id == 0) { return BadRequest(); }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT COM_ID,COM_FECHA_COMPRA,US_ID,COM_LUGAR_DE_ENVIO FROM COMPRA WHERE COM_ID=@ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    COMPRA COMPRA = new COMPRA();
                    while (sqlDataReader.Read())
                    {
                        COMPRA = new COMPRA
                        {
                            COM_ID = sqlDataReader.GetInt32(0),
                            COM_FECHA_COMPRA = sqlDataReader.GetDateTime(1),
                            US_ID = sqlDataReader.GetInt32(2),
                            COM_LUGAR_DE_ENVIO = sqlDataReader.GetString(3)
                        };
                    }
                    sqlConnection.Close();
                    if (COMPRA.COM_ID == 0) { return BadRequest(); }
                    else { return Ok(COMPRA); }
                }
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllComprasByUser(int id)
        {
            if (id == 0) { return BadRequest(); }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT COM_ID,COM_FECHA_COMPRA,US_ID,COM_LUGAR_DE_ENVIO FROM COMPRA WHERE US_ID=@ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    List<COMPRA> compras = new List<COMPRA>();
                    while (sqlDataReader.Read())
                    {
                        compras.Add(new COMPRA
                        {
                            COM_ID = sqlDataReader.GetInt32(0),
                            COM_FECHA_COMPRA = sqlDataReader.GetDateTime(1),
                            US_ID = sqlDataReader.GetInt32(2),
                            COM_LUGAR_DE_ENVIO = sqlDataReader.GetString(3)
                        });
                    }
                    sqlConnection.Close();
                    return Ok(compras);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        private int GetCompraByPayPalToken(string id){
                using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT COM_ID FROM COMPRA WHERE PayPal_Token=@ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    int result=0;
                    while (sqlDataReader.Read())
                    {
                    result = sqlDataReader.GetInt32(0);
                    }
                    sqlConnection.Close();
                return result;
                }
        }
        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult PublicIngresar(COMPRA COMPRA)
        {
            if (COMPRA == null) { return BadRequest(); }
            if (PrivateIngresar(COMPRA)) { return Ok(GetCompraByPayPalToken(COMPRA.PayPal_Token)); }
            else { return InternalServerError(); }
        }
        private bool PrivateIngresar(COMPRA COMPRA)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO COMPRA(COM_FECHA_COMPRA,US_ID,COM_LUGAR_DE_ENVIO,PAYPAL_Token,PayPal_PayerID) VALUES(@COM_FECHA_COMPRA,@US_ID,@COM_LUGAR_DE_ENVIO,@PAYPAL_TOKEN,@PAYPAL_PAYERID);", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@COM_FECHA_COMPRA", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@US_ID", COMPRA.US_ID);
                sqlCommand.Parameters.AddWithValue("@COM_LUGAR_DE_ENVIO", COMPRA.COM_LUGAR_DE_ENVIO);
                sqlCommand.Parameters.AddWithValue("@PAYPAL_TOKEN",COMPRA.PayPal_Token);
                sqlCommand.Parameters.AddWithValue("@PAYPAL_PAYERID", COMPRA.PayPal_PayerID);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }
    }
}
