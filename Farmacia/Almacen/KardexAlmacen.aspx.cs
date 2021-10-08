using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Almacen
{
    public partial class KardexAlmacen : PageBase
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
			CargarDDL(ddlSucursalOrigen, new BLSucursal().SucursalxEmpresaListar(Int32.Parse(Session["IDEmpresa"].ToString())), "IDSucursal", "Sucursal", true);			
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
			Int32 pIDSucursal = Int32.Parse(ddlSucursalOrigen.SelectedValue);
			BLKardex oBLKardex = new BLKardex();
			gvDetalleLista.DataSource = oBLKardex.KardexAlmacenListar(pIDSucursal, 0, txtFechaInicio.Text, txtFechaFin.Text);
			gvDetalleLista.DataBind();
		}

		#endregion


	}
}