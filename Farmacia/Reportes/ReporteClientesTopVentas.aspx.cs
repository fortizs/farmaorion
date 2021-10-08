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
    public partial class ReporteClientesTopVentas : PageBase
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

            CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal",true,Constantes.TODOS);
            txtBFechaInicio.Text = DateTime.Today.AddDays(-30).ToShortDateString();
            txtBFechaFin.Text = DateTime.Today.ToShortDateString();

        }


        private void Listar()
        {
            BLReporte oBL = new BLReporte();
            gvListaClientesTop.DataSource = oBL.ReporteClientesTop10Ventas(Int32.Parse(ddlBIDSucursal.SelectedValue), 0, txtBFechaInicio.Text.Trim(), txtBFechaFin.Text.Trim());
            gvListaClientesTop.DataBind();
            upLista.Update();
        }

        protected void gvListaClientesTop_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListaClientesTop.PageIndex = e.NewPageIndex;
            gvListaClientesTop.SelectedIndex = -1;
            Listar();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = false;
            pnListarGrid.Visible = true;
            Listar();
        }

        protected void lnkImprimirPDF_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = true;
            pnListarGrid.Visible = false;
            iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlBIDSucursal.SelectedValue + "&FechaInicio=" + txtBFechaInicio.Text + "&FechaFin=" + txtBFechaFin.Text + "&Tipo=11";
            div_iframe.Attributes.Add("class", "loading-iframe");
            upLista.Update();
        }  
    }
}