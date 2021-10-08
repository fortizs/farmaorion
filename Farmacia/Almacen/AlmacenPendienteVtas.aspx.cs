using Farmacia.App_Class;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Almacen
{
	public partial class AlmacenPendienteVtas : PageBase
	{
		#region Load
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				txtFechaInicio.Text = DateTime.Today.ToShortDateString();
				txtFechaFin.Text = DateTime.Today.ToShortDateString();
				CargarDDL(ddlIDAlmacenSalida, new BLAlmacen().AlmacenListar(IDSucursal()), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR);
				CargarDDL(ddlIDTransaccion, new BLTransaccion().TransaccionListar("S"), "IDTransaccion", "Nombre", true, Constantes.SELECCIONAR);
				Listar();
			}
		}
		#endregion

		#region Listar 
		private void Listar()
		{
			BLVentaPedido oBLMigracion = new BLVentaPedido();
			gvLista.DataSource = oBLMigracion.VentaPendienteListar(IDSucursal(), txtFechaInicio.Text, txtFechaFin.Text);
			gvLista.DataBind();
			upLista.Update();
		}


		private void ListarNC()
		{
			//BLNotaElectronica oBLMigracion = new BLNotaElectronica();
			//gvListaNC.DataSource = oBLMigracion.NotaElectronicaPendienteListar(IDSucursal(), txtFechaInicioNC.Text, txtFechaFinNC.Text);
			//gvListaNC.DataBind();
			//upLista.Update();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			Listar();
		}

		protected void gvListaNC_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvListaNC.PageIndex = e.NewPageIndex;
			gvListaNC.SelectedIndex = -1;
			ListarNC();
		}

		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{ 
			Int32 pID = Int32.Parse(gvLista.SelectedDataKey["IDVenta"].ToString());
			BLVenta oBL = new BLVenta();
			BEVenta oBE = oBL.VentaSeleccionar(pID);
			txtSerieNumero.Text = oBE.SerieNumero;
			txtCliente.Text = oBE.Cliente;
			txtFechaEmision.Text = oBE.FechaVenta.ToShortDateString();
			txtFechaSalidaAlmacen.Text = oBE.FechaVenta.ToShortDateString();
			ddlIDTransaccion.SelectedValue = Constantes.ID_Tran_S_Ventas.ToString();			 
			lblImporteTotalVenta.Text = oBE.TotalVenta.ToString("N");
			hdfIDVenta.Value = oBE.IDVenta.ToString();
			hdTipoDocumento.Value = "A";
			ListarModal(pID); 
			btnGuardar.Enabled = true;
			upFormulario.Update();
			registrarScript("AbrirModal('ModalSalidaxVenta');");

		}

		protected void gvListaNC_SelectedIndexChanged(object sender, EventArgs e)
		{ 
			//Int32 pID = Int32.Parse(gvListaNC.SelectedDataKey["IDVenta"].ToString());
			//BLCreditoDebito oBL = new BLCreditoDebito();
			//BECreditoDebito oBE = oBL.CreditoDebitoSeleccionar(pID);
			//txtSerieNumero.Text = oBE.SerieNumero;
			//txtCliente.Text = oBE.RazonSocialAdquiriente;
			//txtFechaEmision.Text = oBE.FechaEmision;
			//txtFechaSalidaAlmacen.Text = oBE.FechaEmision;
			//ddlIDTransaccion.SelectedValue = Constantes.ID_Tran_S_Ventas.ToString();
			 
			//lblImporteTotalVenta.Text = oBE.TotalVenta.ToString();
			//hdfIDVenta.Value = oBE.IDCreditoDebito.ToString();
			//hdTipoDocumento.Value = "C";
			//ListarModalNC(pID); 
			//btnGuardar.Enabled = true;
			//upFormulario.Update();
			//registrarScript("AbrirModal('ModalSalidaxVenta');");
		}

		private void ListarModal(Int32 id)
		{ 
			gvDetalleLista.DataSource = new BLVentaDetalle().VentaDetalleListar(id);
			gvDetalleLista.DataBind();
		}


		private void ListarModalNC(Int32 id)
		{
			//BLCreditoDebito oBLMigracion = new BLCreditoDebito();
			//gvDetalleLista.DataSource = oBLMigracion.CreditoDebitoDetalleListar(id);
			//gvDetalleLista.DataBind();
		}
		 
		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			Listar();
		}

		protected void btnBuscarNC_Click(object sender, EventArgs e)
		{
			ListarNC();
		}
		#endregion

		#region Salida x Almacen
		 
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			StringBuilder pValidaciones = new StringBuilder();
			if (ddlIDAlmacenSalida.SelectedValue == "0" || ddlIDAlmacenSalida.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Almacen</div>");

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			BEMovimiento oBE = new BEMovimiento();
			oBE.IDMovimiento = 0;
			oBE.IDAlmacenOrigen = Int32.Parse(ddlIDAlmacenSalida.SelectedValue);
			oBE.IDAlmacenDestino = Int32.Parse(ddlIDAlmacenSalida.SelectedValue);
			oBE.IDEntidad = Int32.Parse(hdfIDVenta.Value);
			oBE.Entidad = "VENTAS";
			oBE.IDTransaccion = Constantes.ID_Tran_S_Ventas;
			oBE.Observacion = txtGlosa.Text.Trim();
			oBE.IDSucursal = IDSucursal();
			oBE.IDEmpresa = IDEmpresa();
			oBE.IDUsuario = IDUsuario();
			oBE.Fecha = DateTime.Today;
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = new BLMovimiento().MovimientoVentaGuardar(oBE);

			if (oBERetorno.Retorno == "1")
			{
				LimpiarFormulario();
				Listar(); 
				btnGuardar.Enabled = false;
				msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				registrarScript("CerrarModal('ModalSalidaxVenta');");
			}
			else
			{
				if (oBERetorno.Retorno == "-1")
				{
					msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
				}
				else {
					RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
				}
			}
			 
		}
		 
		private void LimpiarFormulario()
		{
			hdfIDVenta.Value = "0";
			hdTipoDocumento.Value = "0"; 
			txtSerieNumero.Text = String.Empty;
			txtCliente.Text = String.Empty;
			txtFechaEmision.Text = String.Empty;
			txtFechaSalidaAlmacen.Text = String.Empty;
			ddlIDTransaccion.SelectedIndex = -1; 
		}
		 
		#endregion
	}
}