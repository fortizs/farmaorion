
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BL.Seguridad;
using System;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia 
{
	public class PageBase : System.Web.UI.Page
	{
		#region Variables Generales
		public Int32 IDUsuario()
		{
			Int32 _IDUsuario = 0;
			if ((Session["IDUsuario"]) != null)
				_IDUsuario = (Int32)(Session["IDUsuario"]);
			return _IDUsuario;
		}

		public Int32 IDEmpresa()
		{
			Int32 _IDEmpresa = 0;
			if ((Session["IDEmpresa"]) != null)
				_IDEmpresa = (Int32)(Session["IDEmpresa"]);
			return _IDEmpresa;
		}

		public Boolean EsAdmin()
		{
			Boolean _EsAdmin = false;
			if ((Session["EsAdmin"]) != null)
				_EsAdmin = (Boolean)(Session["EsAdmin"]);
			return _EsAdmin;
		}

		public Int32 IDSucursal()
		{
			Int32 _IDSucursal = 0;
			if ((Session["IDSucursal"]) != null)
				_IDSucursal = (Int32)(Session["IDSucursal"]);
			return _IDSucursal;
		}

		public Int32 IDColaborador()
		{
			Int32 _IDColaborador = 0;
			if ((Session["IDColaborador"]) != null)
				_IDColaborador = (Int32)(Session["IDColaborador"]);
			return _IDColaborador;
		}

		public String NombreUsuario()
		{
			String _NombreUsuario = "";
			if ((Session["NombreUsuario"]) != null)
				_NombreUsuario = (Session["NombreUsuario"].ToString());
			return _NombreUsuario;
		}

		public Int32 IDIdioma()
		{
			Int32 _IDIdioma = 1;
			if ((Session["IDIdioma"]) != null)
				_IDIdioma = Int32.Parse(Session["IDIdioma"].ToString());
			return _IDIdioma;
		}

		public void SeleccionarPrimerItem(DropDownList pDDL)
		{
			try
			{
				if (pDDL.Items.Count > 1 && pDDL.Items.Count <= 2)
				{
					pDDL.SelectedIndex = 1;
				}
			}
			catch (Exception e)
			{ }
		}
		#endregion

		public void ConfigPage(Boolean pValQueryString = false, Boolean pValQueryStringIDP = false, bool ValPoliticasSeguridad = true)
		{
			//Validar Estado de la sesión del Usuario
			ValidarEstadoSesion();
			if (ValPoliticasSeguridad)
				ValidarPoliticasSeguridad();
			//Validar Acceso del Usuario al Menu
			String UrlMenu = Page.AppRelativeVirtualPath;
			if (pValQueryString)
			{
				if (Request.QueryString.Count > 0)
				{
					if (pValQueryStringIDP)
						UrlMenu = UrlMenu + "?IDPRO=" + Request.QueryString["IDPRO"].ToString();
					else
						UrlMenu = UrlMenu + "?" + Request.QueryString.ToString();
				}
			}
			BLMenu oBLMenu = new BLMenu();
			BEMenu oBEX = new BEMenu();
			oBEX.IDUsuario = IDUsuario();
			oBEX.Url = UrlMenu;
			oBEX.IDIdioma = IDIdioma();
			BEMenu oBEMenu = oBLMenu.ValidarMenu(oBEX);
			if (oBEMenu.IDMenu != 0)
			{ 
			 //   ((Literal)Page.Master.FindControl("lBreadCrumb")).Text = "<li>" + oBEMenu.Modulo + "</li>" + oBEMenu.RutaNavegacion;
				////((Literal)Page.Master.FindControl("lPageName")).Text = oBEMenu.Nombre;
				////((Literal)Page.Master.FindControl("lPageIcono")).Text = "<i class=\"" + oBEMenu.ModuloIcono + " position-left\"></i>";
				//((HiddenField)Page.Master.FindControl("hfIDModulo")).Value = oBEMenu.IDModulo.ToString();
				//((HiddenField)Page.Master.FindControl("hfIDMenu")).Value = oBEMenu.IDMenu.ToString();


				//((Literal)Page.Master.FindControl("lBreadCrumb")).Text = "<li class='breadcrumb-item'><a href='javascript:void(0);'>" + oBEMenu.Modulo + "</a></li>" + oBEMenu.RutaNavegacion;
				//((Literal)Page.Master.FindControl("lPageName")).Text = oBEMenu.Nombre;
				//((Literal)Page.Master.FindControl("lPageIcono")).Text = "<i class=\"" + oBEMenu.ModuloIcono + " position-left\"></i>";
				((HiddenField)Page.Master.FindControl("hfIDModulo")).Value = oBEMenu.IDModulo.ToString();
				((HiddenField)Page.Master.FindControl("hfIDMenu")).Value = oBEMenu.IDMenu.ToString();

			}
			else
			{
				Response.Redirect("~/Login.aspx", true);
			}
		}

		public void ValidarPoliticasSeguridad()
		{
			bool Val = false;
			try
			{
				Val = bool.Parse(Session["SegPrimerLogin"].ToString()) || bool.Parse(Session["SegClaveCaducada"].ToString());
			}
			catch (Exception)
			{
				Val = false;
			}
			if (Val)
			{
				Response.Redirect("~/Seguridad/CambiarClaveUsuario.aspx", true);
			}
		}

		public void ValidarEstadoSesion()
		{
			if (!this.Page.User.Identity.IsAuthenticated)
			{
				FormsAuthentication.RedirectToLoginPage();
			}
			BEUsuario oBEUsuario = (BEUsuario)Session["BEUsuario"];
			if (oBEUsuario == null)
			{
				Response.Redirect("~/Login.aspx", true);
			}
		}

		public BERetornoTran RegistrarEventoSeguridad(Int32 pIDTipoEvento, String pDetalle = "")
		{
			return RegistrarEventoSeguridad(IDUsuario(), pIDTipoEvento, pDetalle);
		}

		public BERetornoTran RegistrarEventoSeguridad(Int32 pIDUsuario, Int32 pIDTipoEvento, String pDetalle)
		{
			System.Web.HttpBrowserCapabilities NavegadorInfo = Request.Browser;
			BLEvento oBLEvento = new BLEvento();
			BEEvento oBEEvento = new BEEvento();
			oBEEvento.IDUsuario = pIDUsuario;
			oBEEvento.IDTipoEvento = pIDTipoEvento;
			oBEEvento.Detalle = pDetalle;
			oBEEvento.Host = HttpContext.Current.Request.UserHostAddress;
			oBEEvento.HostDetalles = "IP: " + HttpContext.Current.Request.UserHostAddress + "; "
				+ "DNS = " + HttpContext.Current.Request.UserHostName + "; "
				+ "URL = " + HttpContext.Current.Request.Url + "; "
				+ "Agente = " + HttpContext.Current.Request.UserAgent + "; ";
			oBEEvento.Navegador = NavegadorInfo.Browser + " v" + NavegadorInfo.Version + (NavegadorInfo.IsMobileDevice ? " (Mobile)" : "");
			oBEEvento.NavegadorDetalles = "Tipo = " + NavegadorInfo.Type + "; "
				+ "Nombre = " + NavegadorInfo.Browser + "; "
				+ "Versión = " + NavegadorInfo.Version + "; "
				+ "Plataforma = " + NavegadorInfo.Platform + "; "
				+ "Es Mobile = " + (NavegadorInfo.IsMobileDevice ? "SI [" + NavegadorInfo.MobileDeviceModel + " - " + NavegadorInfo.MobileDeviceManufacturer + "]" : "NO") + "; "
				+ "Es Beta = " + (NavegadorInfo.Beta ? "SI" : "NO") + "; "
				+ "Es Win16 = " + (NavegadorInfo.Win16 ? "SI" : "NO") + "; "
				+ "Es Win32 = " + (NavegadorInfo.Win32 ? "SI" : "NO") + "; "
				+ "Soporte JavaScript = " + (NavegadorInfo["JavaScript"].ToString() == "true" ? "SI v" + NavegadorInfo["JavaScriptVersion"] : "NO") + "; "
				+ "Soporte ECMAScript = " + (NavegadorInfo["JavaScript"].ToString() == "true" ? "SI v" + NavegadorInfo.EcmaScriptVersion.ToString() : "NO") + "; "
				+ "Soporte ActiveX Controls = " + (NavegadorInfo.ActiveXControls ? "SI" : "NO") + "; "
				+ "Soporte Cookies = " + (NavegadorInfo.Cookies ? "SI" : "NO") + "; "
				+ "Soporte Tables = " + (NavegadorInfo.Tables ? "SI" : "NO") + "; "
				+ "Soporte VBScript = " + (NavegadorInfo.VBScript ? "SI" : "NO") + "; "
				+ "Soporte Java Applets = " + (NavegadorInfo.JavaApplets ? "SI" : "NO") + "; ";
			return oBLEvento.Insertar(oBEEvento);
		}

		public void RegistrarLogSistema(String pEvento, String MensajeError, Boolean pAlerta)
		{
			String Detalle = "";
			Int32 IDLog = LogSistema(pEvento, Detalle, MensajeError);
			if (pAlerta)
			{
				msgbox(TipoMsgBox.error, "Error N°: " + IDLog.ToString() + " ");
			}

		}

		public String RegistrarLogSistemaVer(String pEvento, String MensajeError, Boolean RetornarID = false)
		{
			String Detalle = "";
			Int32 IDLog = LogSistema(pEvento, Detalle, MensajeError);
			if (RetornarID)
				return IDLog.ToString();
			else
				return "Error N°: " + IDLog.ToString() + " ";
		}

		public Int32 LogSistema(string pEvento, string pDetalle, string pMsgError)
		{
			BELogSistema oBELog = new BELogSistema();
			BLLogSistema oBLLog = new BLLogSistema();
			BERetornoTran oBLRetorno = new BERetornoTran();
			oBELog.Compilado = "v1.1";
			if (Page.Master != null)
				oBELog.IDModulo = Int32.Parse(((HiddenField)Page.Master.FindControl("hfIDModulo")).Value);
			oBELog.Host = HttpContext.Current.Request.UserHostAddress;
			oBELog.IDUsuario = IDUsuario(); //User.Identity.Name;
			HttpContext currentContext = HttpContext.Current;
			if (currentContext != null)
				oBELog.Opcion = currentContext.Request.Url.AbsoluteUri;
			oBELog.Evento = pEvento;
			oBELog.MensajeError = pMsgError;
			oBELog.Detalle = pDetalle;
			oBLRetorno = oBLLog.Insertar(oBELog);
			return Int32.Parse(oBLRetorno.Retorno);
		}


		#region Utilarios Web
		public enum TipoMsgBox
		{
			confirmation,
			error,
			warning,
			information
		};

		public String SerializeString(String pCadena)
		{
			return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(pCadena).Replace("\"", "");
		}

		public void msgbox(TipoMsgBox pTipo, String pTitulo, String pMensaje, Boolean pNotificacion = false)
		{
			if (!pNotificacion)
				ScriptManager.RegisterStartupScript(this, this.GetType(), "NotificacionJS", "Notificacion('" + pTipo.ToString() + "','" + SerializeString(pMensaje) + "');", true);
			else
				ScriptManager.RegisterStartupScript(this, this.GetType(), "MensajeJS", "Mensaje('" + pTipo.ToString() + "','" + SerializeString(pMensaje) + "');", true);
		}

		public void msgbox(Page pPage, TipoMsgBox pTipo, String pTitulo, String pMensaje, Boolean pNotificacion = false)
		{
			if (!pNotificacion)
				ScriptManager.RegisterStartupScript(pPage, pPage.GetType(), "NotificacionJS", "Notificacion('" + pTipo.ToString() + "','" + SerializeString(pMensaje) + "');", true);
			else
				ScriptManager.RegisterStartupScript(pPage, pPage.GetType(), "MensajeJS", "Mensaje('" + pTipo.ToString() + "','" + SerializeString(pMensaje) + "');", true);
		}

		public void msgbox(TipoMsgBox pTipo, String pMensaje, Boolean pNotificacion = false)
		{
			String Titulo = "Facturación";
			switch (pTipo)
			{
				case TipoMsgBox.confirmation:
					Titulo = "Confirmación";//T("Mensajes", "General.TipoMsjConfirmacion");
					break;
				case TipoMsgBox.error:
					Titulo = "Error";//T("Mensajes", "General.TipoMsjError");
					break;
				case TipoMsgBox.warning:
					Titulo = "Advertencia"; //T("Mensajes", "General.TipoMsjAdvertencia");
					break;
				case TipoMsgBox.information:
					Titulo = "Información";// T("Mensajes", "General.TipoMsjInformacion");
					break;
			}
			msgbox(pTipo, Titulo, pMensaje, pNotificacion);
		}

		public void msgbox(Page pPage, TipoMsgBox pTipo, String pMensaje, Boolean pNotificacion = false)
		{
			String Titulo = "Facturación";
			switch (pTipo)
			{
				case TipoMsgBox.confirmation:
					Titulo = "Confirmación";//T("Mensajes", "General.TipoMsjConfirmacion");
					break;
				case TipoMsgBox.error:
					Titulo = "Error";//T("Mensajes", "General.TipoMsjError");
					break;
				case TipoMsgBox.warning:
					Titulo = "Advertencia"; //T("Mensajes", "General.TipoMsjAdvertencia");
					break;
				case TipoMsgBox.information:
					Titulo = "Información";// T("Mensajes", "General.TipoMsjInformacion");
					break;
			}
			msgbox(pPage, pTipo, Titulo, pMensaje, pNotificacion);
		}

		public void registrarScript(String pScript, String ScriptKey = "regscript")
		{
			ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), pScript, true);
		}

		public void registrarScript(Page pPage, String pScript, String ScriptKey = "regscript")
		{
			ScriptManager.RegisterStartupScript(pPage, pPage.GetType(), Guid.NewGuid().ToString(), pScript, true);
		}
		#endregion

		#region Utilarios


		public void CargarDDL(DropDownList pDDL, object pLista, String pID, String pNombre, Boolean pSelText = false, String pDesSelText = "", Int32 pIDDefecto = 0)
		{
			pDDL.DataSource = pLista;
			pDDL.DataValueField = pID;
			pDDL.DataTextField = pNombre;
			pDDL.DataBind();
			if (pDDL.Items.Count > 0)
			{
				if (pSelText)
				{
					if (pDDL.Items.Count > 0)
						pDDL.Items.Insert(0, new ListItem((pDesSelText.Length > 0 ? pDesSelText : Constantes.SELECCIONAR), pIDDefecto.ToString()));
					else
						pDDL.Items.Insert(0, new ListItem(Constantes.NINGUNO, pIDDefecto.ToString()));
				}
			}
			else
			{
				pDDL.Items.Insert(0, new ListItem(Constantes.NINGUNO, pIDDefecto.ToString()));
			}
		}

		public void CargarLB(ListBox pLB, object pLista, String pID, String pNombre, Boolean pSelText = false, String pDesSelText = "", Int32 pIDDefecto = 0)
		{
			pLB.DataSource = pLista;
			pLB.DataValueField = pID;
			pLB.DataTextField = pNombre;
			pLB.DataBind();
			if (pSelText)
				pLB.Items.Insert(0, new ListItem((pDesSelText.Length > 0 ? pDesSelText : "-Seleccionar-"), pIDDefecto.ToString()));
		}

		public static Boolean esInt32(String _valor)
		{
			Int32 resultado = 0;
			Boolean estado = false;
			if (Int32.TryParse(_valor, out resultado))
			{
				estado = true;
			}
			return estado;
		}

		public static Boolean esDouble(String _valor)
		{
			Double resultado = 0D;
			Boolean estado = false;
			if (Double.TryParse(_valor, out resultado))
			{
				estado = true;
			}
			return estado;
		}

		public static Boolean esDecimal(String _valor)
		{
			Decimal resultado = 0;
			Boolean estado = false;
			if (Decimal.TryParse(_valor, out resultado))
			{
				estado = true;
			}
			return estado;
		}

		public static Boolean esFecha(String _valor)
		{
			DateTime resultado;
			Boolean estado = false;
			if (DateTime.TryParse(_valor, out resultado))
			{
				estado = true;
			}
			return estado;
		}

		public static Boolean esFechaAMPM(String _valor)
		{
			DateTime resultado;
			Boolean estado = false;
			if (DateTime.TryParseExact(_valor, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture, 0, out resultado))
			{
				estado = true;
			}
			return estado;
		}

		public static Boolean esEmail(String _valor)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(_valor);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public Boolean validarTamanoArchivo(HttpPostedFile archivoCargado)
		{
			Boolean pEstado = false;
			Double Tamano = Int32.Parse(ConfigurationManager.AppSettings["MaximoArchivoKB"]);
			Double TamanoArchivo = archivoCargado.ContentLength;
			if (TamanoArchivo <= Tamano)
			{
				pEstado = true;
			}
			return pEstado;
		}
		#endregion

		#region SunatReniec 

		public Cliente ConsultarDocumentoSunatReniec(String pIDTipoComprobante, String pNumeroDocumento)
		{
			var response = new Cliente();
			try
			{
				switch (pIDTipoComprobante)
				{
					case "15555":
						Reniec MyInfoReniec = new Reniec();
						MyInfoReniec.GetInfo(pNumeroDocumento);
						switch (MyInfoReniec.GetResul)
						{
							case Reniec.Resul.Ok:
								response.NumeroDocumento = MyInfoReniec.Dni;
								response.NombreCompleto = MyInfoReniec.Nombre.Trim().ToUpper();
								response.Direccion = MyInfoReniec.Distrido + "- " + MyInfoReniec.Provincia + "- " + MyInfoReniec.Departamento;
								response.Estado = "ACTIVO";

								if (MyInfoReniec.Nombre.Trim().ToUpper().Contains("DNI"))
								{
									response.NumeroDocumento = "00000000";
									response.NombreCompleto = "";
									response.Direccion = "";
									response.Estado = "ERROR";
								}
								break;
						}
						break;
					case "3":

						//string sUrlRequest2 = "https://api.sunat.cloud/ruc/" + pNumeroDocumento;
						string sUrlRequest = "https://api.sunat.cloud/ruc/20109922731";
						ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
						ServicePointManager.ServerCertificateValidationCallback = delegate {
							return true;
						};

						var json = new WebClient().DownloadString(sUrlRequest);
						Int32 pNumro = json.Trim().Length;

						if (pNumro < 20)
						{
							response.NumeroDocumento = "*";
							response.NombreCompleto = "*";
							response.Direccion = "*";
							response.Estado = "ERROR";
						}
						else {
							JavaScriptSerializer ser = new JavaScriptSerializer();
							Cliente oBECliente = ser.Deserialize<Cliente>(json);

							response.NumeroDocumento = oBECliente.ruc;
							response.NombreCompleto = oBECliente.razon_social;
							response.Direccion = oBECliente.domicilio_fiscal;
							response.Estado = oBECliente.contribuyente_estado;
						}

						break;

					case "1":

						string sUrlRequest22 = "https://api.reniec.cloud/dni/" + pNumeroDocumento;

						var json22 = new WebClient().DownloadString(sUrlRequest22);
						Int32 pNumro22 = json22.Trim().Length;

						if (pNumro22 < 20)
						{
							response.NumeroDocumento = "*";
							response.NombreCompleto = "*";
							response.Direccion = "*";
							response.Estado = "ERROR";
						}
						else {
							JavaScriptSerializer ser = new JavaScriptSerializer();
							var responseCliente = ser.Deserialize<Cliente>(json22);

							response.NumeroDocumento = responseCliente.dni;
							response.NombreCompleto = responseCliente.nombres + " " + responseCliente.apellido_paterno + " " + responseCliente.apellido_materno;
						    response.Direccion = "";
							response.Estado = "ACTIVO";
							    
						}

						break;

				}


			}
			catch (Exception ex)
			{
				response.NumeroDocumento = "*";
				response.NombreCompleto = "*";
				response.Direccion = "*";
				response.Estado = "ERROR";
				RegistrarLogSistema("ConsultarDocumentoSunatReniec()", ex.ToString(), true);
			}

			return response;

		}

		#endregion


		public string NumeroALetrasTexto(Decimal numberAsString)
		{
			string dec;

			var entero = Convert.ToInt64(Math.Truncate(numberAsString));
			var decimales = Convert.ToInt32(Math.Round((numberAsString - entero) * 100, 2));
			if (decimales > 0)
			{
				//dec = " PESOS CON " + decimales.ToString() + "/100";
				dec = $"   {decimales:0,0} /100 SOLES";
			}
			//Código agregado por mí
			else
			{
				//dec = " PESOS CON " + decimales.ToString() + "/100";
				dec = $"   {decimales:0,0} /100 SOLES";
			}
			var res = NumeroALetras(Convert.ToDouble(entero)) + dec;
			return res;
		}

		private string NumeroALetras(double value)
		{
			string num2Text; value = Math.Truncate(value);
			if (value == 0) num2Text = "CERO";
			else if (value == 1) num2Text = "UNO";
			else if (value == 2) num2Text = "DOS";
			else if (value == 3) num2Text = "TRES";
			else if (value == 4) num2Text = "CUATRO";
			else if (value == 5) num2Text = "CINCO";
			else if (value == 6) num2Text = "SEIS";
			else if (value == 7) num2Text = "SIETE";
			else if (value == 8) num2Text = "OCHO";
			else if (value == 9) num2Text = "NUEVE";
			else if (value == 10) num2Text = "DIEZ";
			else if (value == 11) num2Text = "ONCE";
			else if (value == 12) num2Text = "DOCE";
			else if (value == 13) num2Text = "TRECE";
			else if (value == 14) num2Text = "CATORCE";
			else if (value == 15) num2Text = "QUINCE";
			else if (value < 20) num2Text = "DIECI" + NumeroALetras(value - 10);
			else if (value == 20) num2Text = "VEINTE";
			else if (value < 30) num2Text = "VEINTI" + NumeroALetras(value - 20);
			else if (value == 30) num2Text = "TREINTA";
			else if (value == 40) num2Text = "CUARENTA";
			else if (value == 50) num2Text = "CINCUENTA";
			else if (value == 60) num2Text = "SESENTA";
			else if (value == 70) num2Text = "SETENTA";
			else if (value == 80) num2Text = "OCHENTA";
			else if (value == 90) num2Text = "NOVENTA";
			else if (value < 100) num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
			else if (value == 100) num2Text = "CIEN";
			else if (value < 200) num2Text = "CIENTO " + NumeroALetras(value - 100);
			else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";
			else if (value == 500) num2Text = "QUINIENTOS";
			else if (value == 700) num2Text = "SETECIENTOS";
			else if (value == 900) num2Text = "NOVECIENTOS";
			else if (value < 1000) num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
			else if (value == 1000) num2Text = "MIL";
			else if (value < 2000) num2Text = "MIL " + NumeroALetras(value % 1000);
			else if (value < 1000000)
			{
				num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
				if ((value % 1000) > 0)
				{
					num2Text = num2Text + " " + NumeroALetras(value % 1000);
				}
			}
			else if (value == 1000000)
			{
				num2Text = "UN MILLON";
			}
			else if (value < 2000000)
			{
				num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
			}
			else if (value < 1000000000000)
			{
				num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
				if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
				{
					num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
				}
			}
			else if (value == 1000000000000) num2Text = "UN BILLON";
			else if (value < 2000000000000) num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
			else
			{
				num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
				if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0)
				{
					num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
				}
			}
			return num2Text;
		}

	}
}