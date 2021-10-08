using Farmacia.App_Class;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Almacen
{
    public partial class AlertaProductos : PageBase
    {

		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
				ddlBIDSucursal.SelectedIndex = IDSucursal();
				AlertaProductoxSucursalListar();
				
			}
		}

		private void CargaInicial()
		{
			CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
		}

		private void AlertaProductoxSucursalListar()
		{
			BLProducto oBL = new BLProducto();
			gvLista.DataSource = oBL.AlertaProductoxSucursalListar(Int32.Parse(ddlBIDSucursal.SelectedValue), txtFiltro.Text);
			gvLista.DataBind();
		}

		protected void lnkBuscarStockProducto_Click(object sender, EventArgs e)
		{
			AlertaProductoxSucursalListar();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			AlertaProductoxSucursalListar();
		}
	}
}