using Farmacia.App_Class;
using Farmacia.App_Class.BL.General;
using System;

namespace Farmacia.Indicador
{
	public partial class IndicadorVenta : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			ValidarEstadoSesion();
			ValidarPoliticasSeguridad();
			if (!IsPostBack)
			{
				//ConfigPage();
				hdfIDEmpresa.Value = IDEmpresa().ToString();
				hdfIDUsuario.Value = IDUsuario().ToString();
				CargarDDL(ddlSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
				CargarDDL(ddlCliente, new BLCliente().ClienteListar("", IDEmpresa()), "IDCliente", "RazonSocial", true, Constantes.TODOS);
				DateTime orderDate = DateTime.Now;                
                txtFechaFin.Text = orderDate.ToString("dd/MM/yyyy");
				txtFechaInicio.Text = orderDate.AddMonths(-1).ToString("dd/MM/yyyy");
				IBFormaPagoxSucursalListar(); 
			}
		}

		private void IBFormaPagoxSucursalListar()
		{ 
			gvFormaPagoListar.DataSource = new BLIBVentas().IBFormaPagoxSucursalListar(txtFechaInicio.Text, txtFechaFin.Text, Int32.Parse(ddlSucursal.SelectedValue));
			gvFormaPagoListar.DataBind(); 
		}
		 
	}
}