using Farmacia.App_Class;
using Farmacia.App_Class.BL.Compras;
using Farmacia.App_Class.BL.General;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Reportes
{
    public partial class ReporteCompras : PageBase
    {
        
        string path = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                CargaInicial();
                ListarComprasxProveedor();
            }
        }
		 
        private void CargaInicial()
        {
            txtFechaInicio.Text = DateTime.Today.ToShortDateString();
            txtFechaFin.Text = DateTime.Today.ToShortDateString();
            CargarDDL(ddlIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
            CargarDDL(ddlIDProveedor, new BLProveedor().ProveedorFiltroListar(IDEmpresa(), ""), "IDProveedor", "RazonSocial", true, Constantes.TODOS);
        }

        #region Listar 

        private void ListarComprasxProveedor()
        {
            BLCompras oBL = new BLCompras();
            gvLista.DataSource = oBL.ReporteComprasxProveedorListar(Int32.Parse(ddlIDSucursal.SelectedValue), Int32.Parse(ddlIDProveedor.SelectedValue), txtFechaInicio.Text, txtFechaFin.Text);
            gvLista.DataBind();  
            upLista.Update(); 
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = false;
            pnListar.Visible = true;
            ListarComprasxProveedor();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        { 
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarComprasxProveedor();
        }
		 
        #endregion
          
        protected void lnkImprimirPDF_Click(object sender, EventArgs e)
        {
            pnImprimirPDF.Visible = true;
            pnListar.Visible = false;
            iframe.Src = "~/Reportes/Imprimir.aspx?IDSucursal=" + ddlIDSucursal.SelectedValue + "&IDProveedor=" + ddlIDProveedor.SelectedValue + "&FechaInicio=" + txtFechaInicio.Text + "&FechaFin=" + txtFechaFin.Text + "&Tipo=1";
            div_iframe.Attributes.Add("class", "loading-iframe");
            upLista.Update();

            //IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
            //IDProveedor = Int32.Parse(Request.QueryString["IDProveedor"]);
            //FechaInicio = Request.QueryString["FechaInicio"];
            //FechaFin = Request.QueryString["FechaFin"];
        }
    }
}