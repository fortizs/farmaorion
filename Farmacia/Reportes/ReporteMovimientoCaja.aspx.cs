using Farmacia.App_Class;
using Farmacia.App_Class.BL.General;
using Muebleria.App_Class.BL.Caja;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Reportes
{
	public partial class ReporteMovimientoCaja : PageBase
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
            CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
            CargarDDL(ddlBIDCaja, new BLCaja().CajaMecanicaListar("", IDSucursal()), "IDCajaMecanica", "Nombre", false);
            txtBFechaInicio.Text = DateTime.Today.AddDays(-30).ToShortDateString();
            txtBFechaFin.Text = DateTime.Today.ToShortDateString();
        }

        private void Listar()
        {
            BLReporte oBL = new BLReporte();
            gvListaMovimientoCaja.DataSource = oBL.ReporteMovimientoCajaListar(Int32.Parse(ddlBIDSucursal.SelectedValue), ddlBTipoMovimiento.SelectedValue, Int32.Parse(ddlBIDCaja.SelectedValue), txtBFechaInicio.Text.Trim(), txtBFechaFin.Text.Trim());
            gvListaMovimientoCaja.DataBind();
            upLista.Update();
        }

        protected void gvListaMovimientoCaja_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListaMovimientoCaja.PageIndex = e.NewPageIndex;
            gvListaMovimientoCaja.SelectedIndex = -1;
            Listar();
        }
         
        protected void lnkImprimirPDF_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = true;
            pnListarGrid.Visible = false;
            iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlBIDSucursal.SelectedValue + "&IDCaja=" + ddlBIDCaja.SelectedValue +"&TipoMovimiento=" + ddlBTipoMovimiento.SelectedValue + "&FechaInicio=" + txtBFechaInicio.Text + "&FechaFin=" + txtBFechaFin.Text + "&Tipo=8";
            div_iframe.Attributes.Add("class", "loading-iframe");
            upLista.Update();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = false;
            pnListarGrid.Visible = true;
            Listar();
        }

        protected void ddlBIDSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDDL(ddlBIDCaja, new BLCaja().ListarCajaxSucursal(Int32.Parse(ddlBIDSucursal.SelectedValue)), "IDCajaMecanica", "NombreCaja", true, Constantes.TODOS);

        }
         
    }
}