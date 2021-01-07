using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViewsTiendaVirtual.Controllers;
using ViewsTiendaVirtual.Models;

namespace ViewsTiendaVirtual.Views
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        async protected void Button1_Click(object sender, EventArgs e)
        {
            UsuarioManager usuarioManager = new UsuarioManager();
            Usuario usuario = await usuarioManager.Validar(UsernameTB.Text, PasswordTB.Text);
            JwtSecurityToken securityToken;
            if (!(usuario == null))
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                securityToken = jwtHandler.ReadJwtToken(usuario.token);
                FormsAuthentication.RedirectFromLoginPage(
                    UsernameTB.Text, true);
                VG.usuarioActual = usuario;
            }
            else
            {
                FailureText.Text = "Credenciales inválidas";
                FailureText.Visible = true;
            }
        }
    }
}