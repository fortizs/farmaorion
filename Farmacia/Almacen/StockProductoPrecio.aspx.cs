using Farmacia.App_Class;
using Farmacia.App_Class.BL.General;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Farmacia.Almacen
{
    public partial class StockProductoPrecio : PageBase
	{
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
				StockProductoxSucursalV2Listar();
			}
		}

		private void CargaInicial()
		{
			CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
		}

		private void StockProductoxSucursalV2Listar()
		{
			BLProducto oBL = new BLProducto();
            String TipoConsulta = ddlTipoConsulta.SelectedValue;
            gvLista.DataSource = oBL.StockProductoxSucursalV2Listar(Int32.Parse(ddlBIDSucursal.SelectedValue), txtFiltro.Text, TipoConsulta);
			gvLista.DataBind();
		}

		protected void lnkBuscarStockProducto_Click(object sender, EventArgs e)
		{
			StockProductoxSucursalV2Listar();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			StockProductoxSucursalV2Listar();
		}
	}
}