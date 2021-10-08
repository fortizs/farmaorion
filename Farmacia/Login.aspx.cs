
using Farmacia.App_Class;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BL.Seguridad;
using System;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;


namespace Farmacia
{
	public partial class Login : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!(Page.IsPostBack))
			{
				if ((Request.QueryString["Logout"] == "SO"))
				{
					//try
					//{
					//Registar Logout
					RegistrarEventoSeguridad(3);
					//}
					//catch (Exception ex)
					//{
					//}
					Session.Abandon();
					System.Web.Security.FormsAuthentication.SignOut();
				}
				//else
				//{
				//    BLUsuario oBLUsuario = new BLUsuario();
				//    BEUsuario oBEUsuario = oBLUsuario.ValidarUsuario("123", "123", "20600921763");
				//    RegistrarIngreso(oBEUsuario);
				//    Response.Redirect("~/Proceso/VentaDirecta.aspx");
				//}

				//Inicio Comentario 
				/*Fin Comentario */
			}
		}

		protected void lnkIngresar_Click(object sender, EventArgs e)
		{
			litMensaje.Text = String.Empty;
			if (txtUsuario.Text.Trim() == "" || txtClave.Text.Trim() == "")
			{
				litMensaje.Text = "<div class='log-err'>Por favor, ingrese Usuario/Contraseña</div>";
				return;
			}
			Ingresar();
			if (String.IsNullOrWhiteSpace(txtUsuario.Text.Trim()))
				txtUsuario.Focus();
			else
				txtClave.Focus();
		}

		protected void Ingresar()
		{
			String pRuc = "20600921763";
			String pUsuario = txtUsuario.Text.Trim();
			String pClave = txtClave.Text.Trim();
			BLUsuario oBLUsuario = new BLUsuario();
			BEUsuario oBEUsuario = oBLUsuario.ValidarUsuario(pUsuario, pClave, pRuc);
			if (oBEUsuario.Mensaje.Length > 0)
			{
				litMensaje.Text = "<div class='log-err'> " + oBEUsuario.Mensaje + " </div>";
			}
			else
			{
				if (oBEUsuario.IDUsuario < 0)
				{
					litMensaje.Text = "<div class='log-err'> Login incorrecto </div>";
				}
				else if (oBEUsuario.IDUsuario == 0)
				{
					litMensaje.Text = "<div class='log-err'>Login incorrecto.</div>";
				}
				else
				{
					if (!oBEUsuario.Bloqueado)
					{
						if (oBEUsuario.Acceso)
						{
						   litMensaje.Text = "<div class='log-con'>OK.</div>";
						   RegistrarIngreso(oBEUsuario); 
						}
						else
						{
							litMensaje.Text = "<div class='log-err'>Login incorrecto.</div>";
							RegistrarEventoSeguridad(oBEUsuario.IDUsuario, 1, "");
						}
					}
					else
					{
						litMensaje.Text = "<div class='log-err'>El inicio de sesión ha sido bloqueado.</div>";
					}
				}
			}
		}
		private void RegistrarIngreso(BEUsuario pBEUsuario, bool pLoginDominio = false)
		{
			Session["BEUsuario"] = pBEUsuario;
			Session["IDUsuario"] = pBEUsuario.IDUsuario;
			Session["IDEmpresa"] = pBEUsuario.IDEmpresa;
			Session["IDColaborador"] = pBEUsuario.IDColaborador;
			Session["IDSucursal"] = pBEUsuario.IDSucursal;
			Session["IDIdioma"] = pBEUsuario.IDIdioma;
			Session["NombreUsuario"] = pBEUsuario.NombreCompleto;
			Session["EmpresaEsPrincipal"] = pBEUsuario.EsPrincipal;

            //BEUsuario oBEUsuario = oBLUsuario.ValidarUsuario(pUsuario, pClave, pRuc);
            BLUsuario oBLUsuario = new BLUsuario();
            Boolean EsAdmin = false;

            EsAdmin = oBLUsuario.UsuarioEsAdmin(pBEUsuario.IDUsuario, Constantes.ID_Rol_Admin);
            Session["EsAdmin"] = EsAdmin;



            //Parámetros de configuración de Seguridad de la Aplicación por Entidad----------------------
            BLParametroConfig oBLSegConfig = new BLParametroConfig();
			IList ListaSegConfig = oBLSegConfig.ListarxEmpresa(pBEUsuario.IDEmpresa);

			//CONFIGURAR PARÁMETROS APLICACIÓN----------------------------------------------------------
			//Tiempo (minutos) para la desconexión automática de la sesión de usuario
			Session.Timeout = Int32.Parse(oBLSegConfig.Parametro("SES02", ListaSegConfig));

			//EVALUAR PARÁMETROS----------
			BLEvento oBLSegEvento = new BLEvento();
			Session["SegPrimerLogin"] = false;
			Session["SegClaveCaducada"] = false;
			//bool pLoginDominio = true; pBEUsuario.IDUsuario !=1 (SuperAdmin)
			if (pLoginDominio == false && pBEUsuario.IDUsuario != 1)
			{
				//El cambio obligatorio de las contraseñas de acceso en el primer inicio de sesión.
				if (Convert.ToBoolean(Int32.Parse(oBLSegConfig.Parametro("CON01", ListaSegConfig))))
				{
					//Consultar si es la primera vez que el usuario inicia sesión
					Session["SegPrimerLogin"] = oBLSegEvento.PrimerLogin(pBEUsuario.IDUsuario);
				}
				//Cambio de contraseña por defecto
				if (pBEUsuario.CambiarClave)
				{
					Session["SegPrimerLogin"] = true;
				}
				//El intervalo de caducidad automática de la contraseña en días.
				Int32 pNumDiasCaduca = Int32.Parse(oBLSegConfig.Parametro("CON05", ListaSegConfig));
				if (pNumDiasCaduca != 0 && !(Boolean.Parse(Session["SegPrimerLogin"].ToString())))
				{
					//Consultar si la contraseña ha caducado
					Session["SegClaveCaducada"] = oBLSegEvento.ClaveCaducada(pBEUsuario.IDUsuario, pNumDiasCaduca);
				}
			}
			if (Boolean.Parse(Session["SegPrimerLogin"].ToString()))
			{
				//Registar Login
				RegistrarEventoSeguridad(2);
			}

			//Registar Login
			RegistrarEventoSeguridad(2);

			//Configuración Regional
			HttpCookie cookie = new HttpCookie("CultureInfo");
			cookie.Value = pBEUsuario.CodigoCultura;
			Response.Cookies.Add(cookie);

			bool isPersistent = false;
			string userData = "keyUD";
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
											pBEUsuario.Usuario,
											DateTime.Now,
											DateTime.Now.AddMinutes(Session.Timeout),
											isPersistent,
											userData,
											FormsAuthentication.FormsCookiePath);
			// Encrypt the ticket.
			string encTicket = FormsAuthentication.Encrypt(ticket);
			// Create the cookie.
			Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
			FormsAuthentication.RedirectFromLoginPage(pBEUsuario.IDUsuario.ToString(), false);

		}

	}
}