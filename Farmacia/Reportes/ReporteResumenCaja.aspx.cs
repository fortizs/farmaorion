using Farmacia.App_Class;
using Farmacia.App_Class.BL.General;
using Muebleria.App_Class.BL.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Reportes
{
    public partial class ReporteResumenCaja : PageBase
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
            CargarDDL(ddlBIDCaja, new BLCaja().CajaMecanicaListar("", IDSucursal()), "IDCajaMecanica", "Nombre", true, Constantes.TODOS);
            CargarDDL(ddlBIDCajero, new BLColaborador().ColaboradorListar(), "IDColaborador", "Colaborador", true, Constantes.TODOS); 
            txtBFechaInicio.Text = DateTime.Today.ToShortDateString();
			txtBFechaFin.Text = DateTime.Today.ToShortDateString(); 
        }

        private void Listar()
        {
            BLReporte oBL = new BLReporte();
            gvListaResumenCaja.DataSource = oBL.ReporteCajaResumenListar(Int32.Parse(ddlBIDSucursal.SelectedValue) , Int32.Parse(ddlBIDCaja.SelectedValue), Int32.Parse(ddlBIDCajero.SelectedValue), txtBFechaInicio.Text.Trim(), txtBFechaFin.Text.Trim());
            gvListaResumenCaja.DataBind();
            upLista.Update();
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        protected void gvListaResumenCaja_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListaResumenCaja.PageIndex = e.NewPageIndex;
            gvListaResumenCaja.SelectedIndex = -1;
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
            iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlBIDSucursal.SelectedValue + "&IDCaja=" + ddlBIDCaja.SelectedValue + "&IDColaborador="+ ddlBIDCajero.SelectedValue + "&FechaInicio=" + txtBFechaInicio.Text + "&FechaFin=" + txtBFechaFin.Text + "&Tipo=4";
            div_iframe.Attributes.Add("class", "loading-iframe");
            upLista.Update();
        }

        protected void ddlBIDSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDDL(ddlBIDCaja, new BLCaja().ListarCajaxSucursal(Int32.Parse(ddlBIDSucursal.SelectedValue)), "IDCajaMecanica", "NombreCaja", false);

        }
    }
}