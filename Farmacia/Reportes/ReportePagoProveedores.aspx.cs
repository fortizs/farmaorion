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
    public partial class ReportePagoProveedores : PageBase
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
            CargarDDL(ddlBIDProveedor, new BLProveedor().ProveedorFiltroListar(IDEmpresa(), ""), "IDProveedor", "RazonSocial", true, Constantes.TODOS); 
            CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true,Constantes.TODOS);
          
            txtBFechaInicio.Text = DateTime.Today.AddDays(-60).ToShortDateString();
            txtBFechaFin.Text = DateTime.Today.ToShortDateString();

        }

        private void Listar()
        {
            BLCuentaPorPagar oBL = new BLCuentaPorPagar();
            gvLista.DataSource = oBL.ReportePagoListar(Int32.Parse(ddlBIDProveedor.SelectedValue), Int32.Parse(ddlBIDSucursal.SelectedValue),  txtBFechaInicio.Text.Trim(), txtBFechaFin.Text.Trim());
            gvLista.DataBind();
            upLista.Update();
        }


        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            Listar();
        }
          
        protected void lnkImprimirPDF_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = true;
            pnListarGrid.Visible = false;
            iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlBIDSucursal.SelectedValue + "&IDProveedor=" + ddlBIDProveedor.SelectedValue + "&FechaInicio=" + txtBFechaInicio.Text + "&FechaFin=" + txtBFechaFin.Text + "&Tipo=6";
            div_iframe.Attributes.Add("class", "loading-iframe");
            upLista.Update();
        }

        protected void btnBuscarPago_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = false;
            pnListarGrid.Visible = true;
            Listar();
        }
    }
}