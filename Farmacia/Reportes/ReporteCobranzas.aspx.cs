using Farmacia.App_Class;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Reportes
{
    public partial class ReporteCobranzas : PageBase
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
            CargarDDL(ddlBIDCliente, new BLCliente ().ClienteListar("",IDEmpresa()), "IDCliente", "RazonSocial", true, Constantes.TODOS);
            CargarDDL(ddlBIDMedioPago, new BLMedioPago ().MedioPagoListar(""), "IDMedioPago", "Nombre", true, Constantes.TODOS);
            CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);

            txtBFechaInicio.Text = DateTime.Today.AddDays(-60).ToShortDateString();
            txtBFechaFin.Text = DateTime.Today.ToShortDateString();

        }


        private void Listar()
        {
            BLCobranza oBL = new BLCobranza();
            gvLista.DataSource = oBL.ReporteCobranzaListar(Int32.Parse(ddlBIDSucursal.SelectedValue), Int32.Parse(ddlBIDMedioPago.SelectedValue), Int32.Parse(ddlBIDCliente.SelectedValue), txtBFechaInicio.Text.Trim(), txtBFechaFin.Text.Trim());
            gvLista.DataBind();
            upLista.Update();
        }


        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            Listar();
        }
         

        protected void btnBuscarCobranza_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = false;
            pnListarGrid.Visible = true;
            Listar();
        }

        protected void lnkImprimirPDF_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = true;
            pnListarGrid.Visible = false;
            iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlBIDSucursal.SelectedValue + "&IDMedioPago=" + ddlBIDMedioPago.SelectedValue + "&IDCliente=" +  ddlBIDCliente.SelectedValue +"&FechaInicio=" + txtBFechaInicio.Text + "&FechaFin=" + txtBFechaFin.Text + "&Tipo=2";
            div_iframe.Attributes.Add("class", "loading-iframe");
            upLista.Update();
        }
    }
}