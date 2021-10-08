using Farmacia.App_Class;
using Farmacia.App_Class.BE.Reportes;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Seguridad;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Reportes
{
    public partial class ReporteVentas : PageBase
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
			CargarDDL(ddlIDPedido, new BLUsuario().UsuarioListar("",Int32.Parse(ddlBIDSucursal.SelectedValue)), "IDUsuario", "NombreCompleto", true, Constantes.TODOS);
			CargarDDL(ddlBIDEstado, new BLEstado().EstadoListar("VEN"), "Codigo", "Nombre", true, Constantes.TODOS);
			CargarDDL(ddlBIDEstadoCobranza, new BLEstado().EstadoListar("COB"), "Codigo", "Nombre", true, Constantes.TODOS);
			txtBFechaInicio.Text = DateTime.Today.ToShortDateString();
			txtBFechaFin.Text = DateTime.Today.ToShortDateString(); 
			ListarVentas(); 
		}

        private void ListarVentas()
        {
            BLReporte oBL = new BLReporte();
            gvLista.DataSource = oBL.VentasResumidasPorSucursal(Int32.Parse(ddlBIDSucursal.SelectedValue), Int32.Parse(ddlIDColaborador.SelectedValue), Int32.Parse(ddlIDPedido.SelectedValue), txtBFechaInicio.Text, txtBFechaFin.Text, Int32.Parse(ddlBIDEstado.SelectedValue),Int32.Parse(ddlBIDEstadoCobranza.SelectedValue), txtFiltro.Text.Trim());
            gvLista.DataBind();
			upLista.Update();

		}
     
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarVentas();
        }

		protected void CabeceraReporteSeleccionar()
		{
			spSucursal.InnerText = "TODOS";
			spColaborador.InnerText = "TODOS";
			spEstadoVenta.InnerText = "TODOS";
			spEstadoCobranza.InnerText = "TODOS";
			spFechaInicio.InnerText = txtBFechaInicio.Text;
			spFechaFin.InnerText = txtBFechaFin.Text;
			spTotalVenta.InnerText = "0.00";
			spUtilidad.InnerText = "0.00";

			if (ddlBIDSucursal.SelectedValue != "0") spSucursal.InnerText = ddlBIDSucursal.SelectedItem.Text;
			if (ddlIDColaborador.SelectedValue != "0") spColaborador.InnerText = ddlIDColaborador.SelectedItem.Text;
			if (ddlBIDEstado.SelectedValue != "0") spEstadoVenta.InnerText = ddlBIDEstado.SelectedItem.Text;
			if (ddlBIDEstadoCobranza.SelectedValue != "0") spEstadoCobranza.InnerText = ddlBIDEstadoCobranza.SelectedItem.Text;

			IList Lista = new BLReporte().VentasResumidasPorSucursal(Int32.Parse(ddlBIDSucursal.SelectedValue), Int32.Parse(ddlIDColaborador.SelectedValue), Int32.Parse(ddlIDPedido.SelectedValue), txtBFechaInicio.Text, txtBFechaFin.Text, Int32.Parse(ddlBIDEstado.SelectedValue), Int32.Parse(ddlBIDEstadoCobranza.SelectedValue), txtFiltro.Text.Trim());
			Decimal TotalTotalVenta = 0;
			Decimal TotalUtilidad = 0;
			foreach (BEReporte item in Lista)
			{
				TotalTotalVenta += item.TotalVenta;
				TotalUtilidad += item.Utilidad;
			}
			spTotalVenta.InnerText = TotalTotalVenta.ToString("N");
			spUtilidad.InnerText = TotalUtilidad.ToString("N");
		}


		protected void btnBuscarVentas_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = false;
            pnListarGrid.Visible = true;
			CabeceraReporteSeleccionar(); 
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