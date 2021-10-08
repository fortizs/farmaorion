using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BL.Seguridad;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Seguridad
{
	public partial class LogSistema : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				txtFechaDesde.Text = DateTime.Today.ToString("dd/MM/yyyy");
				txtFechaHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");
				ListaModulo();
				BuscarLogSistema();
			}
		}

		private void ListaModulo()
		{
			CargarDDL(ddlModulo, new BLModulo().ModuloListar(), "IDModulo", "Nombre", true, "-Todos-");
		}

		protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (ddlFiltro.SelectedValue)
			{
				case "IDLogSistema":
				case "Usuario":
					lblFiltro.Text = ddlFiltro.SelectedItem.Text + ":";
					lblFiltro.Visible = true;
					txtBuscar.Visible = true;
					txtBuscar.Text = "";
					cFiltro.Visible = true;
					break;
				case "Ninguno":
					cFiltro.Visible = false;
					lblFiltro.Visible = false;
					txtBuscar.Visible = false;
					break;
			}
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.SelectedIndex = -1;
			gvLista.PageIndex = e.NewPageIndex;
			BuscarLogSistema();
		}

		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32 pID = Int32.Parse(gvLista.SelectedDataKey["IDLogSistema"].ToString());
			BELogSistema oBE = new BLLogSistema().Seleccionar(pID);
			lblIDLogSistema.Text = oBE.IDLogSistema.ToString();
			lblCompilado.Text = oBE.Compilado;
			lblModulo.Text = oBE.Modulo;
			lblFecha.Text = oBE.Fecha.ToString("dd/MM/yyyy hh:mm:ss tt");
			lblEvento.Text = oBE.Evento;
			lblUsuario.Text = oBE.Usuario;
			lblMensajeError.Text = Server.HtmlEncode(oBE.MensajeError).Replace(Environment.NewLine, "<br />");
			lblOpcion.Text = oBE.Opcion;
			lblHost.Text = oBE.Host;
			upFormulario.Update();
			registrarScript("verLog();");
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			gvLista.SelectedIndex = -1;
			BuscarLogSistema();
		}

		private void BuscarLogSistema()
		{
			BLLogSistema oBL = new BLLogSistema();
			gvLista.DataSource = oBL.Listar(Int32.Parse(ddlModulo.SelectedValue), ddlFiltro.SelectedValue, txtBuscar.Text.Trim(), DateTime.Parse(txtFechaDesde.Text + " 00:00:00"), DateTime.Parse(txtFechaHasta.Text + " 23:59:59"));
			gvLista.DataBind();
		}


	}
}