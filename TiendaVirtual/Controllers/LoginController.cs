using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Threading;
using System.Web.Http;
using TiendaVirtual.Models;
namespace TiendaVirtual.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            if (!(GetUser(login) == null))
            {
                if (login.Password.Equals(GetUser(login).US_CONTRASENA))
                {
                    var token = TokenGenerator.GenerateTokenJwt(login.Username);
                    USUARIO USUARIO = GetUserByID(GetUser(login).US_ID);
                    USUARIO.token = token;
                    return Ok(USUARIO);
                }
                else { return Unauthorized(); }
            }
            else
            {
                return BadRequest();
            }
        }
        private USUARIO GetUser(LoginRequest login)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT US_USUARIO,US_CONTRASENA,US_ID FROM USUARIO WHERE US_USUARIO=@USERNAME", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@USERNAME", login.Username);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                USUARIO USUARIO = new USUARIO();
                while (sqlDataReader.Read())
                {
                    USUARIO = new USUARIO { US_USUARIO = sqlDataReader.GetString(0), US_CONTRASENA = sqlDataReader.GetString(1), US_ID = sqlDataReader.GetInt32(2) };
                }
                sqlConnection.Close();
                return USUARIO;
            }

        }
        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult NewUser(USUARIO USUARIO)
        {
            if (USUARIO == null) { return BadRequest(); }
            try
            {
                //Validar inexistencia de username
                USUARIO user = GetUser(new LoginRequest { Username = USUARIO.US_USUARIO });
                if (!(string.IsNullOrEmpty(user.US_USUARIO)))
                {
                    return BadRequest("Este nombre de USUARIO ya está en uso.");
                }
                using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO USUARIO(US_NOMBRE,US_CORREO,US_USUARIO,US_CONTRASENA) VALUES(@US_NOMBRE,@US_CORREO,@US_USUARIO,@US_CONTRASENA);", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@US_NOMBRE", USUARIO.US_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@US_CORREO", USUARIO.US_CORREO);
                    sqlCommand.Parameters.AddWithValue("@US_USUARIO", USUARIO.US_USUARIO);
                    sqlCommand.Parameters.AddWithValue("@US_CONTRASENA", USUARIO.US_CONTRASENA);
                    sqlConnection.Open();
                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (filasAfectadas > 0)
                    {
                        return Ok(USUARIO);
                    }
                    else { return InternalServerError(); }
                }
            }
            catch
            {
                return InternalServerError();
            }
        }

        private USUARIO GetUserByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Utilities.GetConnection()))
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT US_ID,US_NOMBRE,US_CORREO,US_USUARIO FROM USUARIO WHERE US_ID=@ID;", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ID", id);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                USUARIO USUARIO = new USUARIO();
                while (sqlDataReader.Read())
                {
                    USUARIO = new USUARIO { US_ID = sqlDataReader.GetInt32(0), US_NOMBRE = sqlDataReader.GetString(1), US_CORREO = sqlDataReader.GetString(2), US_USUARIO = sqlDataReader.GetString(3) };
                }
                sqlConnection.Close();
                return USUARIO;

            }
        }



    }
}