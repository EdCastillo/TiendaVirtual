﻿using System.Collections.Generic;
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
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PRO_ID,CAR_PRO_CANTIDAD,PCR_ID FROM PRODUCTO_CARRITO WHERE US_ID=@ID;", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    List<Producto_Carrito> productos = new List<Producto_Carrito>();
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        productos.Add(new Producto_Carrito
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
        public IHttpActionResult PublicInsert(Producto_Carrito producto)
        {
            if (producto == null) { return BadRequest(); }
            if (PrivateInsert(producto)) { return Ok(producto); }
            else { return InternalServerError(); }
        }
        private bool PrivateInsert(Producto_Carrito producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO PRODUCTO_CARRITO(PRO_ID,CAR_PRO_CANTIDAD,US_ID) VALUES(@PRO_ID,@COM_PRO_CANTIDAD,@US_ID);", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@US_ID", producto.US_ID);
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
            else { return Ok(id); }
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
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE Producto_Carrito SET PRO_ID=@PRO_ID,CAR_PRO_CANTIDAD=@COM_PRO_CANTIDAD WHERE PCR_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", producto.PCR_ID);
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