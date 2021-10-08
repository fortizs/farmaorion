using Farmacia.App_Class;
using Farmacia.App_Class.BE.Caja;
using Farmacia.App_Class.BE.General; 
using Farmacia.App_Class.BL.Caja;
using Farmacia.App_Class.BL.General;
using Muebleria.App_Class.BL.Caja;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.CajaBanco
{
	public partial class MovimientoCaja : PageBase
	{
		#region Load
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				Inicio();
				MovimientoCajaListar();
			}
		}

		protected void Inicio()
		{ 
			CargarDDL(ddlIDOperacion, new BLOperacion().OperacionListar("", ddlTipoMovimiento.SelectedValue), "IDOperacion", "Nombre", true);
			CargarDDL(ddlBIDFormaPago, new BLFormaPago().FormaPagoListar(), "IDFormaPago", "Nombre", true, Constantes.TODOS);
		    CargarDDL(ddlIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("0","VEN"), "IDTipoComprobante", "Nombre", true, Constantes.NINGUNO);
			CargarDDL(ddlIDMoneda, new BLMoneda().MonedaListar(), "IDMoneda", "Nombre", true);
			CargarDDL(ddlIDFormaPago, new BLFormaPago().FormaPagoListar(), "IDFormaPago", "Nombre", true, Constantes.TODOS);
			 
			txtFechaInicio.Text = DateTime.Today.ToShortDateString();
			txtFechaFin.Text = DateTime.Today.ToShortDateString(); 
		}

		protected void ddlTipoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
		{
			CargarDDL(ddlIDOperacion, new BLOperacion().OperacionListar("",ddlTipoMovimiento.SelectedValue), "IDOperacion", "Nombre", true);
		}
		#endregion

		#region Listar 
		private void MovimientoCajaListar()
		{
			BEMovimientoCaja oBE = new BLMovimientoCaja().MovimientoCajaTotalListar(0,txtFiltro.Text.Trim(), ddlBTipoMovimiento.SelectedValue, Int32.Parse(ddlBIDFormaPago.SelectedValue), txtFechaInicio.Text, txtFechaFin.Text, IDSucursal(), IDUsuario());
			txtTotalIngresos.Text = oBE.TotalIngreso.ToString("N");
			txtTotalEgresos.Text = oBE.TotalSalida.ToString("N");
			txtSaldo.Text = oBE.TotalSaldo.ToString("N");

			BLMovimientoCaja oBLc = new BLMovimientoCaja();
			gvLista.DataSource = oBLc.MovimientoCajaListar(0,txtFiltro.Text.Trim(), ddlBTipoMovimiento.SelectedValue, Int32.Parse(ddlBIDFormaPago.SelectedValue), txtFechaInicio.Text, txtFechaFin.Text, IDSucursal(), IDUsuario());
			gvLista.DataBind();
			upLista.Update();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			MovimientoCajaListar();
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			hdfIDMovimientoCaja.Value = e.CommandArgument.ToString();
			BERetornoTran oBERetorno = new BERetornoTran();

			switch (e.CommandName)
			{
				case "EditarMovCaja":
					MovimientoCajaSeleccionar();
					registrarScript("AbrirModal('ModalMovimientoCaja');");
					break;
				case "EliminarMovCaja":
					oBERetorno = new BLMovimientoCaja().MovimientoCajaEliminar(Int32.Parse(hdfIDMovimientoCaja.Value));
					if (oBERetorno.Retorno == "1")
					{
						msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
						MovimientoCajaListar();
					}
					else
					{
						if (oBERetorno.Retorno != "-1")
						{
							msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
						}
						else {
							RegistrarLogSistema("gvLista_RowCommand()", oBERetorno.ErrorMensaje, true);
						}
					}
					break; 
			}
		}

		protected void MovimientoCajaSeleccionar()
		{

			BEMovimientoCaja oBE = new BLMovimientoCaja().MovimientoCajaSeleccionar(Int32.Parse(hdfIDMovimientoCaja.Value));
			txtCaja.Text = oBE.CajaMecanica;
			txtCajero.Text = oBE.Cajero;
			ddlTipoMovimiento.SelectedValue = oBE.TipoMovimiento;
			CargarDDL(ddlIDOperacion, new BLOperacion().OperacionListar("", ddlTipoMovimiento.SelectedValue), "IDOperacion", "Nombre", true);
			ddlIDOperacion.SelectedValue = oBE.IDOperacion.ToString();
			txtFechaMovimiento.Text = oBE.FechaMovimiento.ToString("dd/MM/yyyy hh:MM:ss");
			ddlIDTipoComprobante.SelectedValue = oBE.IDTipoComprobante.ToString();
			txtSerie.Text = oBE.Serie;
			txtNumero.Text = oBE.Numero;
			ddlIDMoneda.SelectedValue = oBE.IDMoneda.Trim();
			txtMonto.Text = oBE.Monto.ToString("N");
			txtObservacion.Text = oBE.Observacion;
			ddlIDFormaPago.SelectedValue = oBE.IDFormaPago.ToString();
			upMovimientoCaja.Update();
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			MovimientoCajaListar();
		}
		#endregion
 
		#region Registro
		  
		protected void LimpiarMovimientoCaja()
		{
			hdfIDMovimientoCaja.Value = "0"; 
			ddlTipoMovimiento.SelectedIndex = -1;
			ddlIDOperacion.SelectedIndex = -1;
			txtFechaMovimiento.Text = DateTime.Today.ToShortDateString();
			ddlIDTipoComprobante.SelectedIndex = -1;
			txtSerie.Text = String.Empty;
			txtNumero.Text = String.Empty;
			ddlIDMoneda.SelectedValue = "PEN";
			txtMonto.Text = "0.00";
			txtObservacion.Text = String.Empty;
			ddlIDFormaPago.SelectedIndex = -1;
			txtCaja.Text = String.Empty;
			txtCajero.Text = String.Empty;
			upMovimientoCaja.Update();
		}
		  
		protected void btnNuevoMovimientoCaja_Click(object sender, EventArgs e)
		{
			LimpiarMovimientoCaja();
			BECaja oBE = new BLCaja().CajaAbiertaSeleccionar(IDUsuario(), IDSucursal());
			txtCaja.Text = oBE.CajaMecanica;
			txtCajero.Text = oBE.Cajero;
			upMovimientoCaja.Update(); 
			registrarScript("AbrirModal('ModalMovimientoCaja');");
		}

		protected void btnGuardarMovimientoCaja_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder(); 
				if (ddlTipoMovimiento.SelectedValue == "0") validacion.Append("<div>Seleccione Tipo Movimiento</div>");
				if (ddlIDOperacion.SelectedValue == "0") validacion.Append("<div>Seleccione Operación</div>");
				if (ddlIDFormaPago.SelectedValue == "0") validacion.Append("<div>Seleccione Forma de Pago</div>");
				if (ddlIDMoneda.SelectedValue == "0") validacion.Append("<div>Seleccione Moneda</div>");
				if (ddlIDTipoComprobante.SelectedValue != "0") {
					if (txtSerie.Text.Trim().Length == 0) validacion.Append("<div>Ingrese Serie</div>");
					if (txtNumero.Text.Trim().Length == 0) validacion.Append("<div>Ingrese Número</div>");
				}
				if (Decimal.Parse(txtMonto.Text) <= 0) validacion.Append("<div>El monto no puede ser <= 0 </div>");
				 
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEMovimientoCaja oBE = new BEMovimientoCaja();
				oBE.IDMovimientoCaja = Int32.Parse(hdfIDMovimientoCaja.Value); 
				oBE.IDOperacion = Int32.Parse(ddlIDOperacion.SelectedValue);
				oBE.IDFormaPago = Int32.Parse(ddlIDFormaPago.SelectedValue);
				oBE.FechaMovimiento = DateTime.Parse(txtFechaMovimiento.Text);
				oBE.IDTipoComprobante = Int32.Parse(ddlIDTipoComprobante.SelectedValue);
				oBE.Serie = txtSerie.Text;
				oBE.Numero = txtNumero.Text;
				oBE.IDMoneda = ddlIDMoneda.SelectedValue;
				oBE.Monto = Decimal.Parse(txtMonto.Text);
				oBE.Observacion = txtObservacion.Text;
				oBE.IDSucursal = IDSucursal();
				oBE.IDUsuario = IDUsuario();  
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLMovimientoCaja().MovimientoCajaGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					MovimientoCajaListar();
					msgbox(TipoMsgBox.confirmation, "Sistema", Constantes.MENSAJE_EXITO);
					registrarScript("CerrarModal('ModalMovimientoCaja');");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarMovimientoCaja_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarMovimientoCaja_Click()", ex.ToString(), true);
			}
		}

		#endregion
		 
	}
}