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
    public partial class ReporteVendedoresTop : PageBase
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
            txtBFechaInicio.Text = DateTime.Today.AddDays(-30).ToShortDateString();
            txtBFechaFin.Text = DateTime.Today.ToShortDateString();

        }

        private void Listar()
        {
            BLReporte oBL = new BLReporte();
            gvListaColaborador.DataSource = oBL.ReporteVendedoresTopPorSucursal(Int32.Parse(ddlBIDSucursal.SelectedValue), txtBFechaInicio.Text.Trim(), txtBFechaFin.Text.Trim());
            gvListaColaborador.DataBind();
            upLista.Update();
        }

        protected void gvListaColaborador_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListaColaborador.PageIndex = e.NewPageIndex;
            gvListaColaborador.SelectedIndex = -1;
            Listar();
        }
  
        protected void lnkImprimirPDF_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = true;
            pnListarGrid.Visible = false;
            iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlBIDSucursal.SelectedValue + "&FechaInicio=" + txtBFechaInicio.Text + "&FechaFin=" + txtBFechaFin.Text  + "&Tipo=10";
            div_iframe.Attributes.Add("class", "loading-iframe");
            upLista.Update();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = false;
            pnListarGrid.Visible = true;
            Listar();
        }
    }
}