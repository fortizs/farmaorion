using Farmacia.App_Class;
using Farmacia.App_Class.BL.General;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Reportes
{
    public partial class ReporteDetalleVenta : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
				ListarVentas();
			}
		}

		private void CargaInicial()
		{
			CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
			CargarDDL(ddlIDColaborador, new BLColaborador().ColaboradorListar(), "IDColaborador", "Colaborador", true, Constantes.TODOS);
			CargarDDL(ddlBIDEstado, new BLEstado().EstadoListar("VEN"), "Codigo", "Nombre", true, Constantes.TODOS);
			CargarDDL(ddlBIDEstadoCobranza, new BLEstado().EstadoListar("COB"), "Codigo", "Nombre", true, Constantes.TODOS);
			txtBFechaInicio.Text = DateTime.Today.ToShortDateString();
			txtBFechaFin.Text = DateTime.Today.ToShortDateString();
			ListarVentas();
		}

		private void ListarVentas()
		{
			BLReporte oBL = new BLReporte();
			gvLista.DataSource = oBL.VentasResumidasPorSucursal(Int32.Parse(ddlBIDSucursal.SelectedValue), Int32.Parse(ddlIDColaborador.SelectedValue),0, txtBFechaInicio.Text, txtBFechaFin.Text, Int32.Parse(ddlBIDEstado.SelectedValue), Int32.Parse(ddlBIDEstadoCobranza.SelectedValue), txtFiltro.Text.Trim());
			gvLista.DataBind();
			upLista.Update();

		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarVentas();
		}
		protected void btnBuscarVentas_Click(object sender, EventArgs e)
		{
			pnImprimirPDF.Visible = false;
			pnListarGrid.Visible = true;
			ListarVentas();
		}

		protected void lnkImprimirPDF_Click(object sender, EventArgs e)
		{
			pnImprimirPDF.Visible = true;
			pnListarGrid.Visible = false;
			iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlBIDSucursal.SelectedValue + "&IDColaborador=" + ddlIDColaborador.SelectedValue + "&FechaInicio=" + txtBFechaInicio.Text + "&FechaFin=" + txtBFechaFin.Text + "&Filtro=" + txtFiltro.Text + "&Tipo=3";
			div_iframe.Attributes.Add("class", "loading-iframe");
			upLista.Update();
		}


	}
}