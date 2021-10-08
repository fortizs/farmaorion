using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Collections;
using System.Web.UI;

namespace Farmacia.Almacen
{
	public partial class Kardex : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
				txtFechaInicio.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
				txtFechaFin.Text = DateTime.Today.ToShortDateString();
			}
		}

		private void CargaInicial()
		{
			lblEmpresa.InnerText = Session["IDEmpresa"].ToString();
			CargarDDL(ddlSucursalOrigen, new BLSucursal().SucursalxEmpresaListar(Int32.Parse(Session["IDEmpresa"].ToString())), "IDSucursal", "Sucursal", true);
			CargarDDL(ddlRegProducto, new BLProducto().ListarPorSucursal(IDSucursal()), "IDProducto", "Nombre", true);
			ddlSucursalOrigen.SelectedValue = Convert.ToString(IDSucursal());
		}

		#endregion

		#region ListaKardex

		protected void lnkBuscarKardex_Click(object sender, EventArgs e)
		{
			ListarKardex();
		}

		private void ListarKardex()
		{
			String pIDProducto = ddlRegProducto.SelectedValue.ToString();
			Int32 pIDSucursal = Int32.Parse(ddlSucursalOrigen.SelectedValue);
			BLKardex oBLKardex = new BLKardex();
            IList lista = oBLKardex.KardexBuscar(pIDSucursal, pIDProducto, txtFechaInicio.Text, txtFechaFin.Text);
            gvDetalleLista.DataSource = lista;
            //gvDetalleLista.DataSource = oBLKardex.KardexBuscar(pIDSucursal, pIDProducto, txtFechaInicio.Text, txtFechaFin.Text);
			gvDetalleLista.DataBind();
            upConsulta.Update();
        }

        #endregion

        protected void gvDetalleLista_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvDetalleLista.PageIndex = e.NewPageIndex;
            gvDetalleLista.SelectedIndex = -1;
            ListarKardex();
        }
    }
}