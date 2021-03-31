using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Views.Models;

namespace TiendaVirtual.Controllers
{
    public class CompraRepository
    {

        public Compra GetCompraByID(int id)
        {
            if (id == 0) { return null; }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT COM_ID,COM_FECHA_COMPRA,US_ID,COM_LUGAR_DE_ENVIO FROM COMPRA WHERE COM_ID=@ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    Compra compra = new Compra();
                    while (sqlDataReader.Read())
                    {
                        compra = new Compra
                        {
                            COM_ID = sqlDataReader.GetInt32(0),
                            COM_FECHA_COMPRA = sqlDataReader.GetDateTime(1),
                            US_ID = sqlDataReader.GetInt32(2),
                            COM_LUGAR_DE_ENVIO = sqlDataReader.GetString(3)
                        };
                    }
                    sqlConnection.Close();
                    if (compra.COM_ID == 0) { return null; }
                    else { return compra; }
                }
            }
            catch
            {
                return null;
            }
        }




        public IEnumerable<Compra> GetAllComprasByUser(int id)
        {
            
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT COM_ID,COM_FECHA_COMPRA,US_ID,COM_LUGAR_DE_ENVIO FROM COMPRA WHERE US_ID=@ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    List<Compra> compras = new List<Compra>();
                    while (sqlDataReader.Read())
                    {
                        compras.Add(new Compra
                        {
                            COM_ID = sqlDataReader.GetInt32(0),
                            COM_FECHA_COMPRA = sqlDataReader.GetDateTime(1),
                            US_ID = sqlDataReader.GetInt32(2),
                            COM_LUGAR_DE_ENVIO = sqlDataReader.GetString(3)
                        });
                    }
                    sqlConnection.Close();
                    return compras;
                }
            }
            catch
            {
                return null;
            }
        }

        public void PublicIngresar(Compra compra)
        {
            if (compra == null) {  }
            if (PrivateIngresar(compra)) {  }
            else {  }
        }

        private bool PrivateIngresar(Compra compra)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Tienda"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO COMPRA(COM_FECHA_COMPRA,US_ID,COM_LUGAR_DE_ENVIO) VALUES(@COM_FECHA_COMPRA,@US_ID,@COM_LUGAR_DE_ENVIO);", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@COM_FECHA_COMPRA", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@US_ID", compra.US_ID);
                sqlCommand.Parameters.AddWithValue("@COM_LUGAR_DE_ENVIO", compra.COM_LUGAR_DE_ENVIO);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                if (filasAfectadas > 0) { return true; }
                else { return false; }
            }
        }



    }
}
