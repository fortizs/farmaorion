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
    public partial class ReporteDigemid : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {

                ConfigPage();
				CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", false);
				Listar();

            }
        }
          
        private void Listar()
        {
            BLProducto oBL = new BLProducto();
            gvListaProductoDigemid.DataSource = oBL.ReporteProductoDigemid(Int32.Parse(ddlBIDSucursal.SelectedValue));
            gvListaProductoDigemid.DataBind();
            upLista.Update();
        }
         
        protected void gvListaProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListaProductoDigemid.PageIndex = e.NewPageIndex;
            gvListaProductoDigemid.SelectedIndex = -1;
            Listar();
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
            iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlBIDSucursal.SelectedValue + "&Tipo=12";
            div_iframe.Attributes.Add("class", "loading-iframe");
            upLista.Update();
        }

     
    }
}