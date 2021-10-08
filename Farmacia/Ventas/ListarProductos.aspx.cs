using Farmacia.App_Class.BL.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Ventas
{
    public partial class ListarProductos : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarProducto();
            }
        }

        private void ListarProducto()
        {
            BLProducto oBLProducto = new BLProducto();
            gvLista.DataSource = oBLProducto.ProductoFiltroListar(txtBuscar.Text.Trim(), IDSucursal());
            gvLista.DataBind();
            upLista.Update();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarProducto();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarProducto();
        }
    }
}