using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Farmacia.App_Class;
using Farmacia.App_Class.BL.General;

namespace Farmacia.Almacen
{
    public partial class ProductoxVencer : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                CargaInicial();
                ddlBIDSucursal.SelectedIndex = IDSucursal();
                AlertaProductoxVencerListar();

            }
        }

        private void CargaInicial()
        {
            CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
        }

        private void AlertaProductoxVencerListar()
        {
            BLProducto oBL = new BLProducto();
            gvLista.DataSource = oBL.AlertaProductoxVencerListar(Int32.Parse(ddlBIDSucursal.SelectedValue), txtFiltro.Text);
            gvLista.DataBind();
        }

        protected void lnkBuscarStockProducto_Click(object sender, EventArgs e)
        {
            AlertaProductoxVencerListar();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            AlertaProductoxVencerListar();
        }
    }
}