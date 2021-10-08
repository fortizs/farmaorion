
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using HtmlAgilityPack;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class TipoCambio : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                CargaPeriodo();
				CargarDDL(ddlIDMoneda, new BLMoneda().MonedaListar(), "IDMoneda", "Nombre", true, Constantes.SELECCIONAR);
				TipoCambioListar(); 
            }
        }

        private void CargaPeriodo()
        {
            Int32 AnioActual = DateTime.Now.Year;
            for (int i = AnioActual; i > 2014; i--)
            {
                ddlPeriodoAnio.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            String[] ListaMes = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames;
            String Mes = "";

            for (int i = 0; i < ListaMes.Length - 1; i++)
            {
                Mes = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ListaMes[i]);
                ddlPeriodoMes.Items.Add(new ListItem(Mes, (i + 1).ToString("0#")));
            }
            ddlPeriodoMes.SelectedValue = DateTime.Now.Month.ToString("0#");

        }

        #endregion

        #region Lista

        private void TipoCambioListar()
        {
            BLTipoCambio oBL = new BLTipoCambio();
            gvLista.DataSource = oBL.TipoCambioListar(ddlPeriodoAnio.SelectedValue, ddlPeriodoMes.SelectedValue, Int32.Parse(Session["IDEmpresa"].ToString()));
            gvLista.DataBind();
			upLista.Update();

		}
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
			TipoCambioListar();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
			TipoCambioListar();
        }
       
        #endregion

        #region Registrar

        protected void btnSincronizar_Click(object sender, EventArgs e)
        {
			string sUrl00 = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias";
			string sUrl = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?mes=" + ddlPeriodoMes.SelectedValue + "&anho=" + ddlPeriodoAnio.SelectedValue + "";
			BLTipoCambio oBL = new BLTipoCambio();
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = oBL.TipoCambioSincronizarEliminar(ddlPeriodoAnio.SelectedValue, ddlPeriodoMes.SelectedValue, IDUsuario());

			DataTable dt = new DataTable();
			Encoding objEncoding = Encoding.GetEncoding("ISO-8859-1");
			//  WebProxy objWebProxy = new WebProxy("proxy", 80);
			CookieCollection objCookies = new CookieCollection();

			//USANDO GET
			HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(sUrl);
			//   getRequest.Proxy = objWebProxy;
			getRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
			getRequest.ProtocolVersion = HttpVersion.Version11;
			getRequest.UserAgent = ".NET Framework 4.0";
			getRequest.Method = "GET";

			getRequest.CookieContainer = new CookieContainer();
			getRequest.CookieContainer.Add(objCookies);

			string sGetResponse = string.Empty;

			using (HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse())
			{
				objCookies = getResponse.Cookies;

				using (StreamReader srGetResponse = new StreamReader(getResponse.GetResponseStream(), objEncoding))
				{
					sGetResponse = srGetResponse.ReadToEnd();
				}
			}

			//Obtenemos Informacion
			HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
			document.LoadHtml(sGetResponse);

			HtmlNodeCollection NodesTr = document.DocumentNode.SelectNodes("//table[@class='class=\"form-table\"']//tr");
			if (NodesTr != null)
			{

				dt.Columns.Add("Día", typeof(String));
				dt.Columns.Add("Compra", typeof(String));
				dt.Columns.Add("Venta", typeof(String));

				int iNumFila = 0;
				foreach (HtmlNode Node in NodesTr)
				{
					if (iNumFila > 0)
					{
						int iNumColumna = 0;
						DataRow dr = dt.NewRow();
						foreach (HtmlNode subNode in Node.Elements("td"))
						{

							if (iNumColumna == 0) dr = dt.NewRow();

							string sValue = subNode.InnerHtml.ToString().Trim();
							sValue = System.Text.RegularExpressions.Regex.Replace(sValue, "<.*?>", " ");
							dr[iNumColumna] = sValue;

							iNumColumna++;

							if (iNumColumna == 3)
							{
								dt.Rows.Add(dr);
								iNumColumna = 0;
							}
						}
					}
					iNumFila++;
				}

				dt.AcceptChanges();
				//this.dgvHtml.DataSource = dt;
				//this.dgvHtml.ReadOnly = true;

			}

			// DataTable dtTable = new DataTable();
			//MySQLProcessor.DTTable(mysqlCommand, out dtTable);

			foreach (DataRow dtRow in dt.Rows)
			{
				//foreach(DataColumn dc in dtRow)
				// On all tables' columns
				BETipoCambio oBE = new BETipoCambio();
				oBE.Anio = ddlPeriodoAnio.SelectedValue;
				oBE.Mes = ddlPeriodoMes.SelectedValue;

				foreach (DataColumn dc in dt.Columns)
				{
					if (dc.Caption.Equals("Día"))
					{
						oBE.NumeroDia = Int32.Parse(dtRow[dc].ToString());
					}
					if (dc.Caption.Equals("Compra"))
					{
						oBE.PrecioCompra = Decimal.Parse(dtRow[dc].ToString());
					}
					if (dc.Caption.Equals("Venta"))
					{
						oBE.PrecioVenta = Decimal.Parse(dtRow[dc].ToString());
					}

				}


				oBE.IDUsuario = IDUsuario();
				oBERetorno = oBL.TipoCambioSincronizarGuardar(oBE);

			}
			TipoCambioListar();
            msgbox(TipoMsgBox.confirmation, "Sistema", "La sincronización se realizó con éxito.");
        }


		#endregion

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();  
			registrarScript("AbrirModal('ModalTipoCambio');");
		}

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder(); 
				if (txtFechaPublicacion.Text.Length == 0) validacion.Append("<div>Seleccione fecha de publicación</div>");
				if (ddlIDMoneda.SelectedValue == "0") validacion.Append("<div>Seleccione moneda</div>");
				if (txtPrecioCompra.Text.Length == 0) validacion.Append("<div>Ingrese Precio de Compra</div>");
				if (txtPrecioVenta.Text.Length == 0) validacion.Append("<div>Ingrese Precio de Venta</div>");
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BETipoCambio oBE = new BETipoCambio();
				BLTipoCambio oBL = new BLTipoCambio();
				oBE.IDTipoCambio = Int32.Parse(hdfIDTipoCambio.Value);
				oBE.FechaPublicacion = DateTime.Parse(txtFechaPublicacion.Text.Trim());
				oBE.IDMoneda = ddlIDMoneda.SelectedValue;
				oBE.PrecioCompra = Decimal.Parse(txtPrecioCompra.Text.Trim());
				oBE.PrecioVenta = Decimal.Parse(txtPrecioVenta.Text.Trim());
				oBE.Estado = true;
				oBE.IDEmpresa = IDEmpresa();
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.TipoCambioGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormulario();
					TipoCambioListar();
					msgbox(TipoMsgBox.confirmation, "Sistema", Constantes.MENSAJE_EXITO);
					registrarScript("CerrarModal('ModalTipoCambio');");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else
					{
						RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
					}

				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardar_Click()", ex.Message, true);
			}
		}

		private void LimpiarFormulario()
		{
			hdfIDTipoCambio.Value = "0";
			txtFechaPublicacion.Text = String.Empty;
			ddlIDMoneda.SelectedValue = "USD";
			txtPrecioCompra.Text = "0.00";
			txtPrecioVenta.Text = "0.00";
			upFormulario.Update();
		}
	}
}