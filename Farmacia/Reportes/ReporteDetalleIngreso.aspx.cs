using Farmacia.App_Class;
using Farmacia.App_Class.BL.General;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Reportes
{
	public partial class ReporteDetalleIngreso : PageBase
	{
		#region Load
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
				ListarIngresos();
			}
		}

		private void CargaInicial()
		{
			txtFechaInicio.Text = DateTime.Today.ToShortDateString();
			txtFechaFin.Text = DateTime.Today.ToShortDateString();
			CargarDDL(ddlIDSucursal, new BLSucursal().SucursalxEmpresaListar(Int32.Parse(Session["IDEmpresa"].ToString())), "IDSucursal", "Sucursal", true, Constantes.TODOS);
			//CargarDDL(ddlIDEstadoSunat, new BLEstado().EstadoListar("S"), "IDEstado", "Nombre", true, Constantes.TODOS);

		}

		#endregion

		#region Listar 
		private void ListarIngresos()
		{
			BLReporte oBL = new BLReporte();
			gvLista.DataSource = oBL.ReporteDetalleIngresos(Int32.Parse(ddlIDSucursal.SelectedValue), txtFechaInicio.Text, txtFechaFin.Text);
			gvLista.DataBind();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarIngresos();
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			ListarIngresos();
			ltSucursal.Text = ddlIDSucursal.SelectedItem.Text.ToUpper();
			ltFechaInicio.Text = txtFechaInicio.Text;
			ltFechaFin.Text = txtFechaFin.Text;
		}
		#endregion

	}
}