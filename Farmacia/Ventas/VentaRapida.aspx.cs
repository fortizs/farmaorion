
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Farmacia.Ventas
{
	public partial class VentaRapida : PageBase
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
			BERetornoTran oBERetorno = new BLVenta().VentaValidarCaja(IDUsuario(), IDSucursal());
			ltMensaje.Text = oBERetorno.ErrorMensaje;
			//oBERetorno.Retorno = "1";
			pnMensaje.Visible = oBERetorno.Retorno == "1" ? false : true;
			pnRegistro.Visible = oBERetorno.Retorno == "1" ? true : false;

			if (oBERetorno.Retorno == "1")
			{
				txtFechaInicio.Text = DateTime.Today.ToShortDateString();
				txtFechaFin.Text = DateTime.Today.ToShortDateString();
				CargarDDL(ddlIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("DE","VEN"), "IDTipoComprobante", "Nombre", false);
				CargarDDL(ddlRegTipoDocumento, new BLTipoComprobante().TipoComprobanteListar("DE","VEN"), "IDTipoComprobante", "Nombre", false);
				CargarDDL(ddlBIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("DE","VEN"), "IDTipoComprobante", "Nombre", true, Constantes.TODOS);
				CargarDDL(ddlBIDCliente, new BLCliente().ClienteListar("", IDEmpresa()), "IDCliente", "RazonSocial", true, Constantes.TODOS);

				CargarDDL(ddlBIDEstado, new BLEstado().EstadoListar("VEN"), "Codigo", "Nombre", true, Constantes.TODOS);
				CargarDDL(ddlBIDEstadoCobranza, new BLEstado().EstadoListar("COB"), "Codigo", "Nombre", true, Constantes.TODOS);

				CargarDDL(ddlRegMoneda, new BLCombo().LLenarCombo("MON"), "Codigo", "Nombre", false);

				ddlRegTipoDocumento.SelectedValue = "70";
				hdfIDCliente.Value = "1";
				txtRegNumeroDocumento.Text = "00000000";
				txtRegCliente.Text = "PÚBLICO EN GENERAL";
				pnVentaDetalleTempListar.Visible = true;
				pnVentaDetalleListar.Visible = false;
				CargaSerie();
				VentaListar();
				VentaDetalleTempListar();
			}
		}

		private void CargaSerie()
		{
			String pIDTipoDocumento = ddlRegTipoDocumento.SelectedValue;
			txtSerieNumero.Text = new BLDocumentoSerie().DocumentoSerieListar(pIDTipoDocumento, IDSucursal());
		}

		protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
		{
			CargaSerie();
		}

		protected void ddlRegMoneda_SelectedIndexChanged(object sender, EventArgs e)
		{
			lblMoneda1.Text = ddlRegMoneda.SelectedValue == "PEN" ? "S/" : "$";
			lblMoneda2.Text = ddlRegMoneda.SelectedValue == "PEN" ? "S/" : "$";
			lblMoneda3.Text = ddlRegMoneda.SelectedValue == "PEN" ? "S/" : "$";
			lblMoneda4.Text = ddlRegMoneda.SelectedValue == "PEN" ? "S/" : "$";
			lblMoneda5.Text = ddlRegMoneda.SelectedValue == "PEN" ? "S/" : "$";
			lblMoneda6.Text = ddlRegMoneda.SelectedValue == "PEN" ? "S/" : "$";

			BETipoCambio oBE = new BLTipoCambio().TipoCambioSeleccionar(DateTime.Parse(txtFechaVenta.Text));
			txtTipoCambio.Text = ddlRegMoneda.SelectedValue == "PEN" ? "1" : oBE.PrecioVenta.ToString("N");
		}
		 
		protected void lnkBuscarCli_Click(object sender, EventArgs e)
		{
			hdfIDCliente.Value = "0";
			txtRegCliente.Text = "";

			if (txtRegNumeroDocumento.Text.Trim().Length == 0 || txtRegNumeroDocumento.Text.Trim().Equals("*"))
			{ 
				registrarScript("BuscarCliente()"); 
			}
			else {
				BECliente oBE = new BLCliente().ClienteXNumeroDocumentoSeleccionar(txtRegNumeroDocumento.Text.Trim(), IDEmpresa());
				if (oBE.IDCliente == 0)
				{
					registrarScript("BuscarCliente()");
				}
				else {
					hdfIDCliente.Value = oBE.IDCliente.ToString();
					txtRegNumeroDocumento.Text = oBE.NumeroDocumento;
					txtRegCliente.Text = oBE.RazonSocial.Trim();
				}
			}
		}

		#endregion

		#region VentaListar

		private void VentaListar()
		{
			BEVenta oBE = new BEVenta();
			oBE.Filtro = txtBFiltro.Text.Trim();
			oBE.IDSucursal = IDSucursal();
			oBE.IDCliente = Int32.Parse(ddlBIDCliente.SelectedValue);
			oBE.IDTipoComprobante = Int32.Parse(ddlBIDTipoComprobante.SelectedValue);
			oBE.IDEstado = Int32.Parse(ddlBIDEstado.SelectedValue);
			oBE.IDEstadoCobranza = Int32.Parse(ddlBIDEstadoCobranza.SelectedValue);
			oBE.FechaInicio = txtFechaInicio.Text;
			oBE.FechaFin = txtFechaFin.Text;
			oBE.Accion = "VENTA";
			oBE.IDUsuario = IDUsuario();
			gvLista.DataSource = new BLVenta().VentasListar(oBE);
			gvLista.DataBind();
			upLista.Update();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			VentaListar();
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDVenta.Value = e.CommandArgument.ToString();

				switch (e.CommandName)
				{
					case "Editar":
						VentaSeleccionar();
						VentaDetalleListar();
						pnVentaDetalleListar.Visible = true;
						pnVentaDetalleTempListar.Visible = false;
						lnkPagoRapido.Visible = false;
						upRegistroVenta.Update();
						registrarScript("ActivarTabxId('tab2');");
						break;
					case "Anular":
						BEVenta oBE = new BLVenta().VentaSeleccionar(Int32.Parse(hdfIDVenta.Value));
						ddlIDTipoComprobante.SelectedValue = oBE.IDTipoComprobante.ToString();
						txtNumeroComprobante.Text = oBE.SerieNumero;
						txtMotivoAnulacion.Text = oBE.MotivoAnulacion;
						btnGuardarAnulacion.Visible = true;
						txtMotivoAnulacion.Enabled = true;
						upRegistroVenta.Update();
						upRegistroAnularVenta.Update();
						registrarScript("AbrirModal('DatosVentaAnular');");
						break;
					case "VerMotivoAnulacion":
						BEVenta oBEx = new BLVenta().VentaSeleccionar(Int32.Parse(hdfIDVenta.Value));
						ddlIDTipoComprobante.SelectedValue = oBEx.IDTipoComprobante.ToString();
						txtNumeroComprobante.Text = oBEx.SerieNumero;
						txtMotivoAnulacion.Text = oBEx.MotivoAnulacion;
						btnGuardarAnulacion.Visible = false;
						txtMotivoAnulacion.Enabled = false;
						upRegistroAnularVenta.Update();
						registrarScript("AbrirModal('DatosVentaAnular');");
						break;
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLista_RowCommand()", ex.Message, true);
			}
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			VentaListar();
		}

		protected void VentaSeleccionar()
		{
			BEVenta oBE = new BLVenta().VentaSeleccionar(Int32.Parse(hdfIDVenta.Value));
			ddlRegTipoDocumento.SelectedValue = oBE.IDTipoComprobante.ToString();
			txtSerieNumero.Text = oBE.SerieNumero;

			hdfIDCliente.Value = oBE.IDCliente.ToString();
			txtRegNumeroDocumento.Text = oBE.NumeroDocumento;
			txtRegCliente.Text = oBE.Cliente;
			ddlRegMoneda.SelectedValue = oBE.IDMoneda;
			txtTipoCambio.Text = oBE.TipoCambio.ToString();
			txtFechaVenta.Text = oBE.FechaVenta.ToShortDateString();
			txtObservacion.Text = oBE.Observacion.Trim();

			lblImporteOperacionExonerada.Text = oBE.TotalOperacionExonerada.ToString("N");
			lblImporteSubTotal.Text = oBE.TotalOperacionGravada.ToString("N");
			lblImporteOperacionInafecta.Text = oBE.TotalOperacionInafecta.ToString("N");
			lblImporteOperacionGratuita.Text = oBE.TotalOperacionGratuita.ToString("N");
			lblImporteTotalIgv.Text = oBE.TotalIGV.ToString("N");
			lblDescuento.Text = oBE.TotalDescuentos.ToString("N");
			lblImporteTotalVenta.Text = oBE.TotalVenta.ToString("N");


			ddlRegTipoDocumento.Enabled = false;
		}

		protected void btnImprimirVenta_Click(object sender, EventArgs e)
		{
			registrarScript("Imprimir('" + hdfIDVenta.Value + "','VEN');");
		}

		private void VentaDetalleListar()
		{
			gvVentaDetalleListar.DataSource = new BLVentaDetalle().VentaDetalleListar(Int32.Parse(hdfIDVenta.Value));
			gvVentaDetalleListar.DataBind();
			upRegistroVenta.Update();
		}

		protected void gvVentaDetalleListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvVentaDetalleListar.PageIndex = e.NewPageIndex;
			gvVentaDetalleListar.SelectedIndex = -1;
			VentaListar();
		}

		protected void gvVentaDetalleListar_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "VerLote":
						String[] cmdArgumentos = e.CommandArgument.ToString().Split(new Char[] { ';' });
						hdfIDVentaDetalle.Value = cmdArgumentos[0].ToString();
						txtVDLCodigoBarra.Text = cmdArgumentos[1].ToString();
						txtVDLProducto.Text = cmdArgumentos[2].ToString();
						VentaDetalleLoteListar();
						registrarScript("AbrirModal('ModalVentaDetalleLote');");
						break;
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLista_RowCommand()", ex.Message, true);
			}
		}

		private void VentaDetalleLoteListar()
		{
			gvVentaDetalleLoteListar.DataSource = new BLVentaDetalleLote().VentaDetalleLoteListar(Int32.Parse(hdfIDVentaDetalle.Value));
			gvVentaDetalleLoteListar.DataBind();
			upVentaDetalleLoteListar.Update();
		}

		#endregion

		#region Registro

		protected void lnkNuevaVenta_Click(object sender, EventArgs e)
		{
			LimpiarFormularioVenta();
			CargaSerie();
		}

		protected void lnkPagoRapido_Click(object sender, EventArgs e)
		{
			StringBuilder pValidaciones = new StringBuilder();

			if (ddlRegMoneda.SelectedValue == "0" || ddlRegMoneda.SelectedValue == "-1") pValidaciones.Append("<div>Seleccione una Moneda</div>");
			if (ddlRegTipoDocumento.SelectedValue == "0" || ddlRegTipoDocumento.SelectedValue == "-1") pValidaciones.Append("<div>Seleccione Tipo Comprobante</div>");
			if (txtSerieNumero.Text.Trim().Length <= 2) pValidaciones.Append("<div>Configure la Serie-Número del Comprobante</div>");
			if (hdfIDCliente.Value == "0") pValidaciones.Append("<div>Seleccione Cliente</div>");
			if (txtFechaVenta.Text.Trim().Length == 0) pValidaciones.Append("<div>Seleccione una Fecha Venta</div>");

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			txtEfectivo.Text = "0.00";
			txtTarjeta.Text = "0.00";
			txtTransferencia.Text = "0.00";
			txtCRTotalCredito.Text = "0.00";
			ltTotalCredito.Text = "0.00";
			ltVuelto.Text = "0.00";
			txtCRDiasCredito.Text = "0";
			txtReferencia.Text = "";

			hdfTotalPagar.Value = lblImporteTotalVenta.Text;
			ltTotalPagar.Text = lblImporteTotalVenta.Text;
			ltImporteLetras.Text = NumeroALetrasTexto(Decimal.Parse(ltTotalPagar.Text));
			txtEfectivo.Text = lblImporteTotalVenta.Text;
			ltClienteCredito.Text = txtRegCliente.Text;
			txtCRFechaVencimiento.Text = DateTime.Today.ToShortDateString();
			CalcularVuelto();
			upVentaFormaPago.Update();
			registrarScript("AbrirModal('ModalFormaPago')");
		}

		protected void btnConfirmarVenta_Click(object sender, EventArgs e)
		{
			try
			{
				//Validaciones
				StringBuilder pValidaciones = new StringBuilder();
				if (txtRegNumeroDocumento.Text == "") pValidaciones.Append("<div>Seleccione un Cliente</div>");
				if (txtFechaVenta.Text == "") pValidaciones.Append("<div>Seleccione una Fecha Venta</div>");

				//if (Decimal.Parse(ltVuelto.Text) > 0 || Decimal.Parse(ltVuelto.Text) < 0) pValidaciones.Append("<div>El monto amortizado no es igual al total a pagar.</div>");
				Decimal ImportePagado = 0;
				if (Decimal.Parse(txtTarjeta.Text) > 0 || Decimal.Parse(txtTransferencia.Text) > 0 || Decimal.Parse(txtCRTotalCredito.Text) > 0)
				{
					ImportePagado = Decimal.Parse(txtEfectivo.Text) + Decimal.Parse(txtTarjeta.Text) + Decimal.Parse(txtTransferencia.Text) + Decimal.Parse(txtCRTotalCredito.Text);
					if (ImportePagado != Decimal.Parse(hdfTotalPagar.Value))
					{
						pValidaciones.Append("<div>El importe pagado debe ser igual al total de la venta</div>");
					}
				}

				//if (Decimal.Parse(txtCRTotalCredito.Text) > 0)
				//{
				//	if (Int32.Parse(txtDiasCredito.Text) <= 0) pValidaciones.Append("<div>Dias de Crédito del Cliente no puede ser <= 0 </div>");
				//	if (Decimal.Parse(txtLimiteCredito.Text) <= 0) pValidaciones.Append("<div>Límite de Crédito del Cliente no puede ser <= 0 </div>");
				//	if ((Decimal.Parse(txtCreditoDisponible.Text) - Decimal.Parse(txtCRTotalCredito.Text)) <= 0) pValidaciones.Append("<div>El cliente ya superó el límite de crédito </div>");
				//}

				if (pValidaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, pValidaciones.ToString());
					return;
				}
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLVenta().VentaRecetaAplica(IDUsuario(), "VENTA");
				if (oBERetorno.Retorno == "1")
				{
					LimpiarVentaReceta();
					registrarScript("AbrirModal('ModalRecetaMedica')");
					return;
				}

				BLVenta oBLVenta = new BLVenta();
				BEVenta oBEVenta = new BEVenta();
				ArrayList ListaBEVentaDetalle = new ArrayList();
				ArrayList ListaBEVentaFormaPago = new ArrayList();
				ArrayList ListaBEVentaDetalleLote = new ArrayList();

				oBEVenta.IDVenta = Int32.Parse(hdfIDVenta.Value);
				oBEVenta.IDEmpresa = IDEmpresa();
				oBEVenta.IDSucursal = IDSucursal();
				oBEVenta.IDCliente = Int32.Parse(hdfIDCliente.Value);
				oBEVenta.IDMoneda = ddlRegMoneda.SelectedValue;
				oBEVenta.TipoCambio = Decimal.Parse(txtTipoCambio.Text);
				oBEVenta.IDTipoComprobante = Int32.Parse(ddlRegTipoDocumento.SelectedValue);
				oBEVenta.SerieNumero = txtSerieNumero.Text;
				oBEVenta.FechaVenta = DateTime.Parse(txtFechaVenta.Text);
				oBEVenta.CalculoIGV = Decimal.Parse("0.18");
				oBEVenta.CalculoISC = Decimal.Parse("0.10");

				oBEVenta.CalculoDetraccion = Decimal.Parse("0.10");
				oBEVenta.MontoDetraccion = 0;
				if (Decimal.Parse(lblImporteTotalVenta.Text) >= 700)
				{
					oBEVenta.MontoDetraccion = Decimal.Parse(lblImporteTotalVenta.Text) * oBEVenta.CalculoDetraccion;
				}
				oBEVenta.TotalOperacionGravada = Decimal.Parse(lblImporteSubTotal.Text);
				oBEVenta.TotalOperacionExonerada = Decimal.Parse(lblImporteOperacionExonerada.Text);
				oBEVenta.TotalOperacionInafecta = Decimal.Parse(lblImporteOperacionInafecta.Text);
				oBEVenta.TotalOperacionGratuita = Decimal.Parse(lblImporteOperacionGratuita.Text);
				oBEVenta.TotalIGV = Decimal.Parse(lblImporteTotalIgv.Text);
				oBEVenta.TotalISC = 0;
				oBEVenta.TotalDescuentos = Decimal.Parse(lblDescuento.Text);
				oBEVenta.TotalOtrosTributos = 0;
				oBEVenta.TotalVenta = Decimal.Parse(lblImporteTotalVenta.Text);

				oBEVenta.Migrado = "N";
				oBEVenta.Proceso = "VENTA";
				oBEVenta.IDVentaRelacionado = 0;
				oBEVenta.IDUsuario = IDUsuario();
				oBEVenta.IDMedioPago = 0;
				oBEVenta.IDTipoMotivo = 0;
				oBEVenta.MotivoNota = String.Empty;
				oBEVenta.Vuelto = Decimal.Parse(ltVuelto.Text);
				oBEVenta.Observacion = txtObservacion.Text.Trim();
				 
				BEVentaFormaPago oVFP = new BEVentaFormaPago();
				oVFP.IDVenta = 0;
				oVFP.IDFormaPago = 0;
				oVFP.IDTarjetaCredito = 0;
				oVFP.NumeroOperacion = String.Empty;
				oVFP.MontoPagado = Decimal.Parse(txtEfectivo.Text);
				oVFP.Efectivo = Decimal.Parse(txtEfectivo.Text);
				oVFP.Tarjeta = Decimal.Parse(txtTarjeta.Text);
				oVFP.Transferencia = Decimal.Parse(txtTransferencia.Text);
				oVFP.Credito = Decimal.Parse(txtCRTotalCredito.Text);
				oVFP.Referencia = txtReferencia.Text.Trim();
				oVFP.DiaCredito = Int32.Parse(txtCRDiasCredito.Text);
				oVFP.FechaVencimiento = DateTime.Parse(txtCRFechaVencimiento.Text);
				oVFP.IDUsuario = IDUsuario();


				BEVentaRecetaMedica oVRE = new BEVentaRecetaMedica();
				oVRE.IDVentaRecetaMedica = 99;
				oVRE.IDVenta = 0;
				oVRE.FolioReceta = txtREFolioReceta.Text;
				oVRE.RecetaRetenida = chkRERecetaRetenida.Checked;
				oVRE.NumeroDocumento = txtRENumeroDocumento.Text.Trim();
				oVRE.NombresCompleto = txtRENombresCompleto.Text.Trim();
				oVRE.Direccion = txtREDireccion.Text.Trim();
				oVRE.CMP = txtRECMP.Text.Trim();
				oVRE.IDUsuario = IDUsuario();

				if (hdfIDVenta.Value.Equals("0"))
				{
					oBERetorno = oBLVenta.VentaRapidaGuardar(oBEVenta, oVFP, oVRE);
					hdfIDVenta.Value = oBERetorno.Retorno2;
				}
				else {
					oBERetorno = oBLVenta.ActualizarVentaV2(oBEVenta, ListaBEVentaDetalle, oVFP, ListaBEVentaDetalleLote);
				}

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormularioVenta();
					VentaDetalleTempListar();
					CargaSerie();
					VentaListar();
					msgbox(TipoMsgBox.confirmation, "Facturacion", "La operación se ha registrado con éxito");
					registrarScript("CerrarModal('ModalFormaPago')");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnConfirmarVenta_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnConfirmarVenta_Click()", ex.Message, true);
			}
		}

		protected void btnGuardarVentaRecetaMedica_Click(object sender, EventArgs e)
		{
			try
			{
				//Validaciones
				StringBuilder pValidaciones = new StringBuilder();
				if (txtRegNumeroDocumento.Text == "") pValidaciones.Append("<div>Seleccione un Cliente</div>");
				if (txtFechaVenta.Text == "") pValidaciones.Append("<div>Seleccione una Fecha Venta</div>");

				if (txtREFolioReceta.Text.Trim().Length == 0) pValidaciones.Append("<div>Ingrese Folio de la Receta</div>");
				if (txtRENombresCompleto.Text.Trim().Length == 0) pValidaciones.Append("<div>Ingrese Nombres del Médico</div>");

				//if (Decimal.Parse(ltVuelto.Text) > 0 || Decimal.Parse(ltVuelto.Text) < 0) pValidaciones.Append("<div>El monto amortizado no es igual al total a pagar.</div>");

				if (pValidaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, pValidaciones.ToString());
					return;
				}

				BLVenta oBLVenta = new BLVenta();
				BEVenta oBEVenta = new BEVenta();
				ArrayList ListaBEVentaDetalle = new ArrayList();
				ArrayList ListaBEVentaFormaPago = new ArrayList();
				ArrayList ListaBEVentaDetalleLote = new ArrayList();


				oBEVenta.IDVenta = Int32.Parse(hdfIDVenta.Value);
				oBEVenta.IDEmpresa = IDEmpresa();
				oBEVenta.IDSucursal = IDSucursal();
				oBEVenta.IDCliente = Int32.Parse(hdfIDCliente.Value);
				oBEVenta.IDMoneda = ddlRegMoneda.SelectedValue;
				oBEVenta.TipoCambio = Decimal.Parse(txtTipoCambio.Text);
				oBEVenta.IDTipoComprobante = Int32.Parse(ddlRegTipoDocumento.SelectedValue);
				oBEVenta.SerieNumero = txtSerieNumero.Text;
				oBEVenta.FechaVenta = DateTime.Parse(txtFechaVenta.Text);
				oBEVenta.CalculoIGV = Decimal.Parse("0.18");
				oBEVenta.CalculoISC = Decimal.Parse("0.10");

				oBEVenta.CalculoDetraccion = Decimal.Parse("0.10");
				oBEVenta.MontoDetraccion = 0;
				if (Decimal.Parse(lblImporteTotalVenta.Text) >= 700)
				{
					oBEVenta.MontoDetraccion = Decimal.Parse(lblImporteTotalVenta.Text) * oBEVenta.CalculoDetraccion;
				}
				oBEVenta.TotalOperacionGravada = Decimal.Parse(lblImporteSubTotal.Text);
				oBEVenta.TotalOperacionExonerada = Decimal.Parse(lblImporteOperacionExonerada.Text);
				oBEVenta.TotalOperacionInafecta = Decimal.Parse(lblImporteOperacionInafecta.Text);
				oBEVenta.TotalOperacionGratuita = Decimal.Parse(lblImporteOperacionGratuita.Text);
				oBEVenta.TotalIGV = Decimal.Parse(lblImporteTotalIgv.Text);
				oBEVenta.TotalISC = 0;
				oBEVenta.TotalDescuentos = Decimal.Parse(lblDescuento.Text);
				oBEVenta.TotalOtrosTributos = 0;
				oBEVenta.TotalVenta = Decimal.Parse(lblImporteTotalVenta.Text);

				oBEVenta.Migrado = "N";
				oBEVenta.Proceso = "VENTA";
				oBEVenta.IDVentaRelacionado = 0;
				oBEVenta.IDUsuario = IDUsuario();
				oBEVenta.IDMedioPago = 0;
				oBEVenta.IDTipoMotivo = 0;
				oBEVenta.MotivoNota = String.Empty;
				oBEVenta.Vuelto = Decimal.Parse(ltVuelto.Text);
				oBEVenta.Observacion = txtObservacion.Text.Trim();
				 

				BEVentaFormaPago oVFP = new BEVentaFormaPago();
				oVFP.IDVenta = 0;
				oVFP.IDFormaPago = 0;
				oVFP.IDTarjetaCredito = 0;
				oVFP.NumeroOperacion = String.Empty;
				oVFP.MontoPagado = Decimal.Parse(txtEfectivo.Text);
				oVFP.Efectivo = Decimal.Parse(txtEfectivo.Text);
				oVFP.Tarjeta = Decimal.Parse(txtTarjeta.Text);
				oVFP.Transferencia = Decimal.Parse(txtTransferencia.Text);
				oVFP.Credito = Decimal.Parse(txtCRTotalCredito.Text);
				oVFP.Referencia = txtReferencia.Text.Trim();
				oVFP.DiaCredito = Int32.Parse(txtCRDiasCredito.Text);
				oVFP.FechaVencimiento = DateTime.Parse(txtCRFechaVencimiento.Text);
				oVFP.IDUsuario = IDUsuario();

				BEVentaRecetaMedica oVRE = new BEVentaRecetaMedica();
				oVRE.IDVentaRecetaMedica = 99;
				oVRE.IDVenta = 0;
				oVRE.FolioReceta = txtREFolioReceta.Text;
				oVRE.RecetaRetenida = chkRERecetaRetenida.Checked;
				oVRE.NumeroDocumento = txtRENumeroDocumento.Text.Trim();
				oVRE.NombresCompleto = txtRENombresCompleto.Text.Trim();
				oVRE.Direccion = txtREDireccion.Text.Trim();
				oVRE.CMP = txtRECMP.Text.Trim();
				oVRE.IDUsuario = IDUsuario();

				BERetornoTran oBERetorno = new BERetornoTran();

				if (hdfIDVenta.Value.Equals("0"))
				{
					oBERetorno = oBLVenta.VentaRapidaGuardar(oBEVenta, oVFP, oVRE);
					hdfIDVenta.Value = oBERetorno.Retorno2;
				}
				else {
					oBERetorno = oBLVenta.ActualizarVentaV2(oBEVenta, ListaBEVentaDetalle, oVFP, ListaBEVentaDetalleLote);
				}

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormularioVenta();
					VentaDetalleTempListar();
					CargaSerie();
					VentaListar();
					msgbox(TipoMsgBox.confirmation, "Facturacion", "La operación se ha registrado con éxito");
					registrarScript("CerrarModal('ModalRecetaMedica')");
					registrarScript("CerrarModal('ModalFormaPago')");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnConfirmarVenta_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnConfirmarVenta_Click()", ex.Message, true);
			}
		}

		protected void LimpiarVentaReceta()
		{
			txtREFolioReceta.Text = String.Empty;
			txtREFolioInterno.Text = String.Empty;
			chkRERecetaRetenida.Checked = false;
			txtRENumeroDocumento.Text = String.Empty;
			txtRENombresCompleto.Text = String.Empty;
			txtRECMP.Text = String.Empty;
			txtREDireccion.Text = String.Empty;
			upVentaRecetaMedica.Update();
		}

		private void LimpiarFormularioVenta()
		{
			hdfIDVenta.Value = "0";
			hdfIDVentaDetalle.Value = "0";
			txtFiltroProducto.Text = String.Empty;
			hdfIDCliente.Value = "1";
			txtRegNumeroDocumento.Text = "00000000";
			txtRegCliente.Text = "PÚBLICO EN GENERAL";
			ddlRegMoneda.SelectedIndex = -1;
			ddlRegMoneda.Enabled = true;
			txtTipoCambio.Text = "1";
			txtFechaVenta.Text = DateTime.Today.ToShortDateString();
			txtObservacion.Text = String.Empty;
			ddlRegTipoDocumento.SelectedValue = "70";
			ddlRegTipoDocumento.Enabled = true;
			txtSerieNumero.Text = String.Empty;
			pnVentaDetalleListar.Visible = false;
			pnVentaDetalleTempListar.Visible = true;
			lnkPagoRapido.Visible = true;
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = new BLVentaDetalle().VentaDetalleTempxUsuarioEliminar(IDUsuario(), "VENTA");
			VentaDetalleTempListar();
			upRegistroVenta.Update();
		}

		#endregion
		 
		#region AnularVenta

		protected void btnGuardarAnulacion_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder pValidaciones = new StringBuilder();
				if (hdfIDVenta.Value == "0") pValidaciones.Append("<div>Seleccione una Venta a Anular</div>");
				if (txtMotivoAnulacion.Text == "") pValidaciones.Append("<div>Ingrese Motivo Anulación</div>");
				if (pValidaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, pValidaciones.ToString());
					return;
				}

				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLVenta().VentaAnular(Int32.Parse(hdfIDVenta.Value), txtMotivoAnulacion.Text.Trim(), IDUsuario());

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormularioAnularVenta();
					VentaListar();
					msgbox(TipoMsgBox.confirmation, "Facturacion", "La Anulación se ha realizado con éxito");
					registrarScript("CerrarModal('DatosVentaAnular')");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarAnulacion_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarAnulacion_Click()", ex.Message, true);
			}
		}
		 
		protected void LimpiarFormularioAnularVenta()
		{
			hdfIDVenta.Value = "0";
			ddlIDTipoComprobante.SelectedIndex = -1;
			txtNumeroComprobante.Text = "";
			txtMotivoAnulacion.Text = "";
			upRegistroAnularVenta.Update();
			upRegistroVenta.Update();
		}

		#endregion

		#region Producto

		protected void txtFiltroPro_TextChanged(object sender, EventArgs e)
		{
			ProductoFiltroListar();
		}

		protected void txtFiltroProducto_TextChanged(object sender, EventArgs e)
		{
			IList ListaProducto = new BLProducto().ProductoFiltroListar(txtFiltroProducto.Text.Trim(), IDSucursal());
			if (ListaProducto.Count == 1)
			{
				foreach (BEProducto item in ListaProducto)
				{
					hdfIDProducto.Value = item.IDProducto.ToString();
					AgregarProductoDetalleTemp(Int32.Parse(hdfIDProducto.Value));
				}
			}
			else {
				txtFiltroPro.Text = txtFiltroProducto.Text.Trim();
				ProductoFiltroListar();
				registrarScript("AbrirModal('ModalProducto')");
			}
		}

		protected void AgregarProductoDetalleTemp(Int32 pIDProducto)
		{
			BEProducto itemProd = new BLProducto().ProductoSeleccionar(pIDProducto, IDSucursal());
			StringBuilder pValidaciones = new StringBuilder();
			if (itemProd.ControlStock.Equals("SK"))
			{
				if (itemProd.Stock <= 0) pValidaciones.Append("<div>No existe stock para ese producto</div>");
				if (pValidaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, pValidaciones.ToString());
					return;
				}
			}

			if (itemProd.ControlaLote)
			{
				hfSLIDProducto.Value = itemProd.IDProducto.ToString();
				txtSLCodigoBarra.Text = itemProd.CodigoBarra.Trim();
				txtSLProducto.Text = itemProd.Nombre.Trim();
				LotexProductoListar();
				registrarScript("AbrirModal('ModalSalidaManualLote')");
			}
			else {
				BEVentaDetalle oBEVD = new BEVentaDetalle();
				oBEVD.IDSucursal = IDSucursal();
				oBEVD.Token = "";
				oBEVD.IDProducto = pIDProducto;
				oBEVD.IDUnidadMedidaVenta = itemProd.IDUnidadMedidaVenta;
				oBEVD.Cantidad = Decimal.Parse("1");
				oBEVD.ValorUnitario = itemProd.PrecioCostoUnidadSinIgv;
				oBEVD.Igv = Math.Round((itemProd.PrecioVenta / Decimal.Parse("1.18")) * Decimal.Parse("0.18"), 2, MidpointRounding.AwayFromZero);
				oBEVD.PrecioVenta = itemProd.PrecioVenta;
				oBEVD.Descuento = 0;
				oBEVD.IDTipoPrecio = itemProd.IDTipoPrecio.ToString();
				oBEVD.IDTipoImpuesto = itemProd.IDTipoAfectacionIgv.ToString();
				oBEVD.IDUsuario = IDUsuario();
				oBEVD.Proceso = "VENTA";
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLVentaDetalle().VentaDetalleTempGuardar(oBEVD);

				if (oBERetorno.Retorno == "1")
				{
					pnVentaDetalleTempListar.Visible = true;
					pnVentaDetalleListar.Visible = false;
					VentaDetalleTempListar();
					//CalcularTotales();
					msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
				}
				else {
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("txtFiltroProducto_TextChanged()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
		}

		protected void lnkBuscarProducto_Click(object sender, EventArgs e)
		{
			IList ListaProducto = new BLProducto().ProductoFiltroListar(txtFiltroProducto.Text.Trim(), IDSucursal());
			if (ListaProducto.Count == 1)
			{
				foreach (BEProducto item in ListaProducto)
				{
					hdfIDProducto.Value = item.IDProducto.ToString();
					AgregarProductoDetalleTemp(Int32.Parse(hdfIDProducto.Value));
				}
			}
			else {
				txtFiltroPro.Text = txtFiltroProducto.Text.Trim();
				ProductoFiltroListar();
				registrarScript("AbrirModal('ModalProducto')");
			}
		}

		private void ProductoFiltroListar()
		{
			gvProductoListar.DataSource = new BLProducto().ProductoFiltroListar(txtFiltroPro.Text.Trim(), IDSucursal());
			gvProductoListar.DataBind();
			upProductoListar.Update();
		}

		protected void gvProductoListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvProductoListar.PageIndex = e.NewPageIndex;
			gvProductoListar.SelectedIndex = -1;
			ProductoFiltroListar();
		}

		protected void gvProductoListar_SelectedIndexChanged(object sender, EventArgs e)
		{
			hfIDProducto.Value = gvProductoListar.SelectedDataKey["IDProducto"].ToString();
			AgregarProductoDetalleTemp(Int32.Parse(hfIDProducto.Value));
		}

		protected void btnBuscarProducto_Click(object sender, EventArgs e)
		{
			ProductoFiltroListar();
		}

		private void LotexProductoListar()
		{
			gvLotexProducto.DataSource = new BLLote().LoteListar(Int32.Parse(hfSLIDProducto.Value), IDSucursal());
			gvLotexProducto.DataBind();
			upSalidaManualLote.Update();
		}

		#endregion

		#region FormaPago

		protected void txtEfectivo_TextChanged(object sender, EventArgs e)
		{
			CalcularVuelto();
		}

		protected void txtTarjeta_TextChanged(object sender, EventArgs e)
		{
			CalcularVuelto();
		}

		protected void txtTransferencia_TextChanged(object sender, EventArgs e)
		{
			CalcularVuelto();
		}

		protected void CalcularVuelto()
		{
			txtCRTotalCredito.Text = "0.00";
			ltTotalCredito.Text = "0.00";
			ltVuelto.Text = ((Decimal.Parse(txtEfectivo.Text) + Decimal.Parse(txtTarjeta.Text) + Decimal.Parse(txtTransferencia.Text)) - Decimal.Parse(lblImporteTotalVenta.Text)).ToString("N");
			if (Decimal.Parse(ltVuelto.Text) < 0)
			{
				lbCambio.Style.Add("color", "#FF2D00");
			}
			else {
				lbCambio.Style.Add("color", "#247a00");
			}
		}

		#endregion

		#region DetalleVentaTemp

		private void VentaDetalleTempListar()
		{
			gvVentaDetalleTempListar.DataSource = new BLVentaDetalle().VentaDetalleTempListar(IDUsuario(), "VENTA");
			gvVentaDetalleTempListar.DataBind();
			CalcularTotalesTemp();
			upRegistroVenta.Update();
		}

		protected void gvVentaDetalleTempListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvVentaDetalleTempListar.PageIndex = e.NewPageIndex;
			gvVentaDetalleTempListar.SelectedIndex = -1;
			VentaDetalleTempListar();
		}

		protected void gvVentaDetalleTempListar_RowCommand(object sender, GridViewCommandEventArgs e)
		{

			BERetornoTran oBERetorno = new BERetornoTran();

			switch (e.CommandName)
			{
				case "Editar":
					String[] cmdArgumentos = e.CommandArgument.ToString().Split(new Char[] { ';' });
					hdfIDVentaDetalleTemp.Value = cmdArgumentos[0].ToString();
					txtVLCodigoBarra.Text = cmdArgumentos[1].ToString();
					txtVLProducto.Text = cmdArgumentos[2].ToString();
					Boolean ControlaLote = Boolean.Parse(cmdArgumentos[3].ToString());
					if (ControlaLote)
					{
						VentaDetalleLoteTempListar();
						registrarScript("AbrirModal('ModalVentaDetalleLoteTemp');");
					}
					else {
                        Int32 IDProducto = Int32.Parse(cmdArgumentos[5]);
                        BEProducto itemProd = new BLProducto().ProductoSeleccionar(IDProducto, IDSucursal());
                        txtVDCodigoBarra.Text = cmdArgumentos[1].ToString();
						txtVDProducto.Text = cmdArgumentos[2].ToString();
						txtVDCantidad.Text = cmdArgumentos[4].ToString();
                        hdfVDStockDisponible.Value = itemProd.Stock.ToString();
                        upVentaDetalleActualizar.Update();
						registrarScript("AbrirModal('ModalVentaDetalle');");
					}
					break;

				case "Descuento":
					LimpiarProductoDescuento();
					String[] cmdArgumentosDES = e.CommandArgument.ToString().Split(new Char[] { ';' });
					hdfIDVentaDetalleTemp.Value = cmdArgumentosDES[0].ToString();
					txtDIProducto.Text = cmdArgumentosDES[1].ToString();
					txtDIPrecioVenta.Text = cmdArgumentosDES[2].ToString();
					txtDIPrecioConDescuento.Text = cmdArgumentosDES[2].ToString();
					txtDIPorcentajeDescuento.Text = "0.00";
					upDescuentoItemRegistrar.Update();
					registrarScript("AbrirModal('ModalDescuentoItem');");
					break;

				case "Caracteristica":
					String[] cmdArgumentosCA = e.CommandArgument.ToString().Split(new Char[] { ';' });
					txtCProducto.Text = cmdArgumentosCA[0].ToString();
					txtCCaracteristica.Text = cmdArgumentosCA[1].ToString();
					txtCPrincipioActivo.Text = cmdArgumentosCA[2].ToString();
					upProductoCaracteristica.Update();
					registrarScript("AbrirModal('ModalCaracteristica');");
					break;

				case "Eliminar":
					hdfIDVentaDetalleTemp.Value = e.CommandArgument.ToString();
					oBERetorno = new BLVentaDetalle().VentaDetalleTempEliminar(Int32.Parse(hdfIDVentaDetalleTemp.Value));
					if (oBERetorno.Retorno == "1")
					{
						VentaDetalleTempListar();
						msgbox(TipoMsgBox.confirmation, Constantes.MENSAJE_EXITO);
					}
					else
					{
						if (oBERetorno.Retorno != "-1")
						{
							msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
						}
						else {
							RegistrarLogSistema("gvVentaDetalleTempListar_RowCommand()", oBERetorno.ErrorMensaje, true);
						}
					}
					break;
			}
		}

		protected void LimpiarProductoDescuento()
		{
			txtDIProducto.Text = String.Empty;
			txtDIPrecioVenta.Text = "0.00";
			txtDIPorcentajeDescuento.Text = "0.00";
			txtDIPrecioConDescuento.Text = "0.00";
		}

		protected void btnAgregarLote_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder pValidaciones = new StringBuilder();

				ArrayList aLoteProducto = new ArrayList();
				BEVentaDetalleLote oBE;
				Decimal pCantidad = 0;
				foreach (GridViewRow row in gvLotexProducto.Rows)
				{
					if (row.RowType == DataControlRowType.DataRow)
					{
						oBE = new BEVentaDetalleLote();
						oBE.IDVentaDetalleLoteTemp = 0;
						oBE.IDProducto = Int32.Parse(hfSLIDProducto.Value);
						oBE.IDLote = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hfIDLote")).Value);
						oBE.StockActualLote = Decimal.Parse(((HiddenField)row.Cells[0].FindControl("hfStockActual")).Value);
						oBE.IDSucursal = IDSucursal();
						oBE.CantidadLote = Decimal.Parse(((TextBox)row.Cells[4].FindControl("txtSLSalidaManual")).Text);
						oBE.IDUsuario = IDUsuario();
						pCantidad += oBE.CantidadLote;
						if (oBE.CantidadLote <= 0)
						{
							pValidaciones.Append("<div>La cantidad no puede ser menor o igual a cero</div>");
							break;
						}

						if (oBE.CantidadLote > 0 && oBE.CantidadLote <= oBE.StockActualLote)
						{
							if (aLoteProducto == null)
							{
								aLoteProducto = new ArrayList();
							}
							aLoteProducto.Add(oBE);
						}
						if (oBE.CantidadLote > 0)
						{
							if (oBE.CantidadLote > oBE.StockActualLote)
							{
								pValidaciones.Append("<div>No existe stock para ese producto</div>");
							}
						}
					}
				}

				if (pValidaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, pValidaciones.ToString());
					return;
				}

				BEProducto itemProd = new BLProducto().ProductoSeleccionar(Int32.Parse(hfSLIDProducto.Value), IDSucursal());
				BEVentaDetalle oBEVD = new BEVentaDetalle();

				oBEVD.IDSucursal = IDSucursal();
				oBEVD.Token = "";
				oBEVD.IDProducto = Int32.Parse(hfSLIDProducto.Value);
				oBEVD.IDUnidadMedidaVenta = itemProd.IDUnidadMedidaVenta;
				oBEVD.Cantidad = pCantidad;
				oBEVD.ValorUnitario = itemProd.PrecioCostoUnidadSinIgv;
				oBEVD.Igv = Math.Round((itemProd.PrecioVenta / Decimal.Parse("1.18")) * Decimal.Parse("0.18"), 2, MidpointRounding.AwayFromZero);
				oBEVD.PrecioVenta = itemProd.PrecioVenta;
				oBEVD.Descuento = 0;
				oBEVD.IDTipoPrecio = itemProd.IDTipoPrecio.ToString();
				oBEVD.IDTipoImpuesto = itemProd.IDTipoAfectacionIgv.ToString();
				oBEVD.IDUsuario = IDUsuario();
				oBEVD.Proceso = "VENTA";
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLVentaDetalle().VentaDetalleTempLoteGuardar(oBEVD, aLoteProducto);

				if (oBERetorno.Retorno == "1")
				{
					VentaDetalleTempListar();
					//CalcularTotales();
					msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
					registrarScript("CerrarModal('ModalSalidaManualLote')");
				}
				else {
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnAgregarLote_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnAgregarLote_Click()", ex.Message, true);
			}
		}

		protected void CalcularTotalesTemp()
		{
			Decimal TotalVenta = 0;
			Decimal Descuento = 0;
			Decimal Cantidad = 0;
			Decimal TotalDescuento = 0;

			foreach (GridViewRow fila in gvVentaDetalleTempListar.Rows)
			{
				TotalVenta += Decimal.Parse(((HiddenField)fila.Cells[0].FindControl("hfImporteTotal")).Value);
				Descuento = Decimal.Parse(((HiddenField)fila.Cells[0].FindControl("hfDescuento")).Value);
				Cantidad = Decimal.Parse(((HiddenField)fila.Cells[0].FindControl("hfCantidad")).Value);
				if (Descuento > 0)
				{
					TotalDescuento += Descuento;
				}
			}

			Decimal SubTotal = Math.Round(TotalVenta / Decimal.Parse("1.18"), 2, MidpointRounding.AwayFromZero);
			Decimal TotalIgv = Math.Round(TotalVenta - SubTotal, 2, MidpointRounding.AwayFromZero);

			lblImporteOperacionExonerada.Text = "0.00";
			lblImporteOperacionInafecta.Text = "0.00";
			lblImporteOperacionGratuita.Text = "0.00";
			lblImporteSubTotal.Text = Math.Round(SubTotal, 2, MidpointRounding.AwayFromZero).ToString("N");
			lblImporteTotalIgv.Text = TotalIgv.ToString("N2");
			lblDescuento.Text = TotalDescuento.ToString("N");
			lblImporteTotalVenta.Text = TotalVenta.ToString("N");
		}

		#endregion

		#region DetalleVentaLote

		private void VentaDetalleLoteTempListar()
		{
			gvVentaDetalleLoteTempListar.DataSource = new BLVentaDetalleLote().VentaDetalleLoteTempListar(Int32.Parse(hdfIDVentaDetalleTemp.Value));
			gvVentaDetalleLoteTempListar.DataBind();
			upVentaDetalleLoteTempListar.Update();
		}

		protected void btnActualizarVentaDetalleLote_Click(object sender, EventArgs e)
		{
			BEVentaDetalleLote oBE;
			ArrayList aLoteProducto = new ArrayList();
			StringBuilder pValidaciones = new StringBuilder();

			foreach (GridViewRow row in gvVentaDetalleLoteTempListar.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					oBE = new BEVentaDetalleLote();
					oBE.IDVentaDetalleLoteTemp = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hfVDTIDVentaDetalleLoteTemp")).Value);
					oBE.IDLote = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hfVDTIDLote")).Value);
					oBE.CantidadLote = Decimal.Parse(((TextBox)row.Cells[4].FindControl("txtVDTSalidaManual")).Text);
					oBE.StockActualLote = Decimal.Parse(((HiddenField)row.Cells[0].FindControl("hfVDTStockActual")).Value);

					//if (aLoteProducto == null)
					//{
					//	aLoteProducto = new ArrayList();
					//}
					//aLoteProducto.Add(oBE);

					if (oBE.CantidadLote > 0 && oBE.CantidadLote <= oBE.StockActualLote)
					{
						if (aLoteProducto == null)
						{
							aLoteProducto = new ArrayList();
						}
						aLoteProducto.Add(oBE);
					}

					if (oBE.CantidadLote > oBE.StockActualLote)
					{
						pValidaciones.Append("<div>No existe stock para ese producto</div>");
					}

				}
			}

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			BERetornoTran oBERetorno = new BERetornoTran();
			foreach (BEVentaDetalleLote item in aLoteProducto)
			{

				BEVentaDetalleLote oBEVDL = new BEVentaDetalleLote();
				oBEVDL.IDVentaDetalleLoteTemp = item.IDVentaDetalleLoteTemp;
				oBEVDL.IDVentaDetalleTemp = 0;
				oBEVDL.IDProducto = 0;
				oBEVDL.IDLote = 0;
				oBEVDL.IDSucursal = 0;
				oBEVDL.Cantidad = item.CantidadLote;
				oBEVDL.IDUsuario = IDUsuario();
				oBERetorno = new BLVentaDetalleLote().VentaDetalleLoteTempGuardar(oBEVDL);
			}

			if (oBERetorno.Retorno == "1")
			{
				BEVentaDetalle oBEVD = new BEVentaDetalle();
				oBEVD.IDVentaDetalleTemp = Int32.Parse(hdfIDVentaDetalleTemp.Value);
				oBEVD.Cantidad = 0;
				oBEVD.IDUsuario = IDUsuario();
				oBERetorno = new BLVentaDetalle().VentaDetalleTempActualizar(oBEVD);
				VentaDetalleTempListar();
				msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
				registrarScript("CerrarModal('ModalVentaDetalleLoteTemp')");
			}
			else {
				if (oBERetorno.Retorno != "-1")
				{
					msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
				}
				else {
					RegistrarLogSistema("btnActualizarVentaDetalleLote_Click()", oBERetorno.ErrorMensaje, true);
				}
			}
		}

		protected void btnActualizarVentaDetalle_Click(object sender, EventArgs e)
		{
			try
			{
                

				BERetornoTran oBERetorno = new BERetornoTran();
				BEVentaDetalle oBEVD = new BEVentaDetalle();
				oBEVD.IDVentaDetalleTemp = Int32.Parse(hdfIDVentaDetalleTemp.Value);
				oBEVD.Cantidad = Decimal.Parse(txtVDCantidad.Text);
				oBEVD.IDUsuario = IDUsuario();

                Decimal StockDisponible = Decimal.Parse(hdfVDStockDisponible.Value);
                if (oBEVD.Cantidad > StockDisponible) {
                    msgbox(TipoMsgBox.warning, "Facturacion", "No existe stock para ese producto");
                    return;
                }


				oBERetorno = new BLVentaDetalle().VentaDetalleTempActualizar(oBEVD);
				if (oBERetorno.Retorno == "1")
				{
					VentaDetalleTempListar();
					msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
					registrarScript("CerrarModal('ModalVentaDetalle')");
				}
				else {
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnActualizarVentaDetalle_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnActualizarVentaDetalle_Click()", ex.Message, true);
			} 
		}



		#endregion

		#region DescuentoItem

		protected void lnkAplicarDescuentoGeneral_Click(object sender, EventArgs e)
		{
			try
			{
				BERetornoTran oBERetorno = new BERetornoTran();
				BEVentaDetalle oBEVDL = new BEVentaDetalle();
				oBEVDL.PorcentajeDescuento = Decimal.Parse(txtPorcentajeDescuentoGeneral.Text);
				oBEVDL.IDUsuario = IDUsuario();
				oBERetorno = new BLVentaDetalle().VentaDetalleTempDescuentoGeneral(oBEVDL);

				if (oBERetorno.Retorno == "1")
				{
					VentaDetalleTempListar();
					msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
				}
				else {
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						msgbox(TipoMsgBox.error, "Facturacion", oBERetorno.ErrorMensaje);
						RegistrarLogSistema("lnkAplicarDescuentoGeneral_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("lnkAplicarDescuentoGeneral_Click()", ex.Message, true);
			}
		}

		protected void btnGuardarDescuentoItem_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder pValidaciones = new StringBuilder();
				if (Decimal.Parse(txtDIPrecioConDescuento.Text) > Decimal.Parse(txtDIPrecioVenta.Text)) pValidaciones.Append("<div>El precio con Descuento no puede ser mayor al precio de Venta</div>");
				if (pValidaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, pValidaciones.ToString());
					return;
				}

				BERetornoTran oBERetorno = new BERetornoTran();
				BEVentaDetalle oBEVDL = new BEVentaDetalle();
				oBEVDL.IDVentaDetalleTemp = Int32.Parse(hdfIDVentaDetalleTemp.Value);
				oBEVDL.PorcentajeDescuento = Decimal.Parse(txtDIPorcentajeDescuento.Text);
				oBEVDL.PrecioConDescuento = Decimal.Parse(txtDIPrecioConDescuento.Text);
				oBEVDL.IDUsuario = IDUsuario();
				oBEVDL.IDSucursal = IDSucursal();
				oBERetorno = new BLVentaDetalle().VentaDetalleTempDescuentoItem(oBEVDL);

				if (oBERetorno.Retorno == "1")
				{
					VentaDetalleTempListar();
					msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
					registrarScript("CerrarModal('ModalDescuentoItem')");
				}
				else {
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarDescuentoItem_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarDescuentoItem_Click()", ex.Message, true);
			}
		}

		protected void txtDIPorcentajeDescuento_TextChanged(object sender, EventArgs e)
		{
			txtDIPrecioConDescuento.Text = (Decimal.Parse(txtDIPrecioVenta.Text) - (Decimal.Parse(txtDIPrecioVenta.Text) * (Decimal.Parse(txtDIPorcentajeDescuento.Text) / 100))).ToString("N");
		}

		protected void txtDIPrecioConDescuento_TextChanged(object sender, EventArgs e)
		{
			txtDIPorcentajeDescuento.Text = (((Decimal.Parse(txtDIPrecioVenta.Text) - Decimal.Parse(txtDIPrecioConDescuento.Text)) * 100) / Decimal.Parse(txtDIPrecioVenta.Text)).ToString("N");
		}

		#endregion

		protected void btnMigrar_Click(object sender, EventArgs e)
		{
			try
			{
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLVenta().VentasMigrarInsertar(0, txtFechaInicio.Text, txtFechaFin.Text, IDUsuario(), IDEmpresa(), IDSucursal());
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

		protected void gvProductoListar_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			BERetornoTran oBERetorno = new BERetornoTran();

			switch (e.CommandName)
			{
				case "Caracteristica":
					String[] cmdArgumentosCA = e.CommandArgument.ToString().Split(new Char[] { ';' });
					txtCProducto.Text = cmdArgumentosCA[0].ToString();
					txtCCaracteristica.Text = cmdArgumentosCA[1].ToString();
					txtCPrincipioActivo.Text = cmdArgumentosCA[2].ToString();
					upProductoCaracteristica.Update();
					registrarScript("AbrirModal('ModalCaracteristica');");
					break;
			}
		}

		protected void gvProductoListar_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				HiddenField sk = (HiddenField)e.Row.FindControl("hfStock");
				HiddenField ControlStock = (HiddenField)e.Row.FindControl("hfControlStock");
				Decimal Stock = Decimal.Parse(sk.Value);
				if (ControlStock.Value.Equals("SK"))
				{
					if (Stock <= 0)
					{
						e.Row.ForeColor = Color.Red;
					}
				}
			}
		}

		protected void lnkLimpiarCliente_Click(object sender, EventArgs e)
		{
			hdfIDCliente.Value = "0";
			txtRegNumeroDocumento.Text = String.Empty;
			txtRegCliente.Text = String.Empty;
		}

		protected void lnkAsignarCredito_Click(object sender, EventArgs e)
		{
			pnVentaCredito.Visible = (pnVentaCredito.Visible == true) ? false : true;
			CalcularCredito();
			BECliente oBE = new BLCliente().ClienteSeleccionar(Int32.Parse(hdfIDCliente.Value));
			txtDiasCredito.Text = oBE.DiasCredito.ToString();
			txtLimiteCredito.Text = oBE.LimiteCredito.ToString("N");
			txtCreditoDisponible.Text = oBE.CreditoDisponible.ToString("N");
			txtCRTotalCredito.Text = ltTotalCredito.Text;
			txtCRDiasCredito.Text = oBE.DiasCredito.ToString();
			txtCRFechaVencimiento.Text = new BLCliente().FechaVencimientoxNumeroDiaCreditoCliente(Int32.Parse(txtCRDiasCredito.Text)).FechaVencimiento.ToShortDateString();
			if (!pnVentaCredito.Visible)
			{
				txtEfectivo.Text = hdfTotalPagar.Value;
				txtTarjeta.Text = "0.00";
				txtTransferencia.Text = "0.00";
				txtCRTotalCredito.Text = "0.00";
				ltTotalCredito.Text = "0.00";
				ltVuelto.Text = "0.00";
				txtCRDiasCredito.Text = "0";
			}
		}

		protected void CalcularCredito()
		{
			if (Decimal.Parse(ltVuelto.Text) == 0 || Decimal.Parse(ltVuelto.Text) > 0)
			{
				txtEfectivo.Text = "0.00";
				txtTarjeta.Text = "0.00";
				txtTransferencia.Text = "0.00";
				txtCRTotalCredito.Text = hdfTotalPagar.Value;
				ltTotalCredito.Text = hdfTotalPagar.Value;
				ltVuelto.Text = "0.00";
			}
			else {
				txtCRTotalCredito.Text = (Decimal.Parse(ltVuelto.Text) * -1).ToString("N");
				ltTotalCredito.Text = (Decimal.Parse(ltVuelto.Text) * -1).ToString("N");
				ltVuelto.Text = "0.00";
			}

			if (Decimal.Parse(ltVuelto.Text) < 0)
			{
				lbCambio.Style.Add("color", "#FF2D00");
			}
			else {
				lbCambio.Style.Add("color", "#247a00");
			}
		}

		protected void txtCRDiasCredito_TextChanged(object sender, EventArgs e)
		{
			txtCRFechaVencimiento.Text = new BLCliente().FechaVencimientoxNumeroDiaCreditoCliente(Int32.Parse(txtCRDiasCredito.Text)).FechaVencimiento.ToShortDateString();
		}
	}
}