using Farmacia.App_Class;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Reportes
{
    public partial class ReporteProductosMasVendidos : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {

                ConfigPage();
                CargaComboListar();
                Listar();

            }
        }

        private void CargaComboListar()
        {
            
            CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true,Constantes.TODOS);
            CargarDDL(ddlBIDCategoria, new BLCategoria().Listar(IDEmpresa()), "IDCategoria", "Nombre", true, Constantes.TODOS);  
            txtBFechaInicio.Text = DateTime.Today.AddDays(-60).ToShortDateString();
            txtBFechaFin.Text = DateTime.Today.ToShortDateString();

        }
         
        private void Listar()
        {
            BLProducto oBL = new BLProducto();
            gvListaProducto.DataSource = oBL.ReporteProductosMasVendidosListar(Int32.Parse(ddlBIDSucursal.SelectedValue), Int32.Parse(ddlBIDCategoria.SelectedValue), txtBFechaInicio.Text.Trim(), txtBFechaFin.Text.Trim());
            gvListaProducto.DataBind();
            upLista.Update();
        }
         
        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = false;
            pnListarGrid.Visible = true;
            Listar();
        }

        protected void lnkImprimirPDF_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = true;
            pnListarGrid.Visible = false;
            iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlBIDSucursal.SelectedValue + "&IDCategoria=" + ddlBIDCategoria.SelectedValue + "&FechaInicio=" + txtBFechaInicio.Text + "&FechaFin=" + txtBFechaFin.Text + "&Tipo=5";
            div_iframe.Attributes.Add("class", "loading-iframe");
            upLista.Update();
        }

		protected void gvListaProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvListaProducto.PageIndex = e.NewPageIndex;
			gvListaProducto.SelectedIndex = -1;
			Listar();
		}
	}
}