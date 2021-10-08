
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.General;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Ventas
{
	public partial class MigrarVentas : PageBase
	{
		#region Inicio

		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
			}
		}

		private void CargaInicial()
		{
			CargarDDL(ddlBIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("DE", "VEN"), "IDTipoComprobante", "Nombre", true, Constantes.TODOS);
			CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
			CargarDDL(ddlBIDEstado, new BLEstado().EstadoListar("VEN"), "Codigo", "Nombre", true, Constantes.TODOS); 
			VentaListar();
		}

		#endregion

		#region VentaListar

		private void VentaListar()
		{
			BEVenta oBE = new BEVenta();
			oBE.Filtro = txtBFiltro.Text.Trim();
			oBE.IDSucursal = Int32.Parse(ddlBIDSucursal.SelectedValue);
			oBE.IDTipoComprobante = Int32.Parse(ddlBIDTipoComprobante.SelectedValue);
			oBE.IDEstado = Int32.Parse(ddlBIDEstado.SelectedValue); 
			oBE.FechaInicio = txtFechaInicio.Text;
			oBE.FechaFin = txtFechaFin.Text; 
			gvLista.DataSource = new BLVenta().VentasMigrarListar(oBE);
			gvLista.DataBind();
			upLista.Update();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			VentaListar();
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			VentaListar();
		}

		protected void btnMigrar_Click(object sender, EventArgs e)
		{
			try
			{
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLVenta().VentasMigrarInsertar(0, txtFechaInicio.Text, txtFechaFin.Text, IDUsuario(), IDEmpresa(), Int32.Parse(ddlBIDSucursal.SelectedValue));
				if (oBERetorno.Retorno == "1")
				{
					VentaListar();
					msgbox(TipoMsgBox.confirmation, "Facturacion", "La migración se realizó con éxito");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnMigrar_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnMigrar_Click()", ex.ToString(), true);
			}
		}

		#endregion

	}
}