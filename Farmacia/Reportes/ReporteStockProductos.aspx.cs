using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Reportes;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Reportes
{
	public partial class ReporteStockProductos : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
				StockProductoxSucursalListar();
			}
		}


		private void CargaInicial()
		{
			CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
			CargarDDL(ddlBIDCategoria, new BLCategoria().CategoriaFiltroListar("", IDEmpresa()), "IDCategoria", "Nombre", true, Constantes.TODOS);
			CargarDDL(ddlBIDMarca, new BLMarca().MarcaFiltroListar("", IDEmpresa()), "IDMarca", "Nombre", true, Constantes.TODOS);
		}

		protected void CabeceraReporteSeleccionar()
		{
			spSucursal.InnerText = "TODOS";
			spCategoria.InnerText = "TODOS";
			spLaboratorio.InnerText = "TODOS";
			spTipoReporte.InnerText = "Productos con Stock";
			spNroProductos.InnerText = "0";
			spValorizado.InnerText = "0.00";

			if (ddlBIDSucursal.SelectedValue != "0") spSucursal.InnerText = ddlBIDSucursal.SelectedItem.Text;
			if (ddlBIDCategoria.SelectedValue != "0") spCategoria.InnerText = ddlBIDCategoria.SelectedItem.Text;
			if (ddlBIDMarca.SelectedValue != "0") spLaboratorio.InnerText = ddlBIDMarca.SelectedItem.Text;
			if (ddlTipoReporte.SelectedValue != "0") spTipoReporte.InnerText = ddlTipoReporte.SelectedItem.Text;

			IList Lista = new BLReporte().ReporteStockProductoxSucursalListar(Int32.Parse(ddlBIDCategoria.SelectedValue), Int32.Parse(ddlBIDMarca.SelectedValue), Int32.Parse(ddlBIDSucursal.SelectedValue), txtFiltroProducto.Text, ddlTipoReporte.SelectedValue);
			Decimal TotalStockActual = 0;
			Decimal TotalValorizado = 0;
			foreach (BEProducto item in Lista)
			{
				TotalStockActual += item.StockActual;
				TotalValorizado += item.Valorizado; 
			}
			spNroProductos.InnerText = TotalStockActual.ToString();
			spValorizado.InnerText = TotalValorizado.ToString("N");
		}


		private void StockProductoxSucursalListar()
		{
			BLReporte oBL = new BLReporte();
			gvLista.DataSource = oBL.ReporteStockProductoxSucursalListar(Int32.Parse(ddlBIDCategoria.SelectedValue), Int32.Parse(ddlBIDMarca.SelectedValue), Int32.Parse(ddlBIDSucursal.SelectedValue), txtFiltroProducto.Text, ddlTipoReporte.SelectedValue);
			gvLista.DataBind();
		}
		protected void btnBuscarStock_Click(object sender, EventArgs e)
		{
			pnImprimirPDF.Visible = false;
			pnListarGrid.Visible = true;
			CabeceraReporteSeleccionar();
			StockProductoxSucursalListar();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			StockProductoxSucursalListar();
		}

		protected void lnkImprimirPDF_Click(object sender, EventArgs e)
		{
			pnImprimirPDF.Visible = true;
			pnListarGrid.Visible = false;
			iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlBIDSucursal.SelectedValue + "&Filtro=" + txtFiltroProducto.Text + "&Tipo=13";
			div_iframe.Attributes.Add("class", "loading-iframe");
			upLista.Update();
		}
	}
}