
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Farmacia.Reserva
{
	public partial class ReservaRapida : PageBase
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
			txtFechaInicio.Text = DateTime.Today.ToShortDateString();
			txtFechaFin.Text = DateTime.Today.ToShortDateString();
			CargarDDL(ddlIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("DE","VEN"), "IDTipoComprobante", "Nombre", false); 
			CargarDDL(ddlRegTipoDocumento, new BLTipoComprobante().TipoComprobanteListar("DE","VEN"), "IDTipoComprobante", "Nombre", false);
			CargarDDL(ddlBIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("DE","VEN"), "IDTipoComprobante", "Nombre", true, Constantes.TODOS);
			CargarDDL(ddlBIDCliente, new BLCliente().ClienteListar("", IDEmpresa()), "IDCliente", "RazonSocial", true, Constantes.TODOS);
			CargarDDL(ddlIDTipoDocumento, new BLTipoDocumento().TipoDocumentoListar(), "IDTipoDocumento", "Nombre", false);

			CargarDDL(ddlBIDEstado, new BLEstado().EstadoListar("VEN"), "Codigo", "Nombre", true, Constantes.TODOS);
			CargarDDL(ddlBIDEstadoCobranza, new BLEstado().EstadoListar("COB"), "Codigo", "Nombre", true, Constantes.TODOS);

			CargarDDL(ddlRegMoneda, new BLCombo().LLenarCombo("MON"), "Codigo", "Nombre", false);
			CargarDDL(ddlVIDFormaPago, new BLFormaPago().FormaPagoListar(), "IDFormaPago", "Nombre", true, Constantes.SELECCIONAR);

			ddlIDTipoDocumento.SelectedValue = "1";
			ddlRegTipoDocumento.SelectedValue = "2";
			hdfIDCliente.Value = "1";
			txtRegNumeroDocumento.Text = "00000000";
			txtRegCliente.Text = "PÚBLICO EN GENERAL";
			pnVentaDetalleTempListar.Visible = true;
			pnVentaDetalleListar.Visible = false;
			CargaSerie();
			ReservasListar();
            ReservaDetalleTempListar();

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

			if (txtRegNumeroDocumento.Text.Trim().Length == 0 || txtRegNumeroDocumento.Text.Trim().Equals("*")
				|| ddlIDTipoDocumento.SelectedValue.Equals("2") || ddlIDTipoDocumento.SelectedValue.Equals("4")
				|| ddlIDTipoDocumento.SelectedValue.Equals("5") || ddlIDTipoDocumento.SelectedValue.Equals("6"))
			{
				registrarScript("AbrirModal('DatosCliente')");
			}
			else {
				StringBuilder pValidaciones = new StringBuilder();
				if (ddlIDTipoDocumento.SelectedValue == "1" && txtRegNumeroDocumento.Text.Trim().Length != 8) pValidaciones.Append("<div>El número dni no es válido.</div>");
				if (ddlIDTipoDocumento.SelectedValue == "3" && txtRegNumeroDocumento.Text.Trim().Length != 11) pValidaciones.Append("<div>El número ruc no es válido.</div>");

				if (pValidaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, pValidaciones.ToString());
					return;
				}

				BECliente oBE = new BLCliente().ClienteXNumeroDocumentoSeleccionar(txtRegNumeroDocumento.Text.Trim(), IDEmpresa());

				if (oBE.IDCliente == 0)
				{
					//Buscamos en la reniec o sunat
					var response = ConsultarDocumentoSunatReniec(ddlIDTipoDocumento.SelectedValue, txtRegNumeroDocumento.Text.Trim());

					if (response.Estado.Equals("ERROR")) pValidaciones.Append("<div>El número documento no existe.</div>");

					if (pValidaciones.Length > 0)
					{
						msgbox(TipoMsgBox.warning, pValidaciones.ToString());
						return;
					}

					BECliente oBECliente = new BECliente();
					oBECliente.NumeroDocumento = response.NumeroDocumento;
					oBECliente.IDTipoDocumento = Int32.Parse(ddlIDTipoDocumento.SelectedValue);
					oBECliente.RazonSocial = response.NombreCompleto;
					oBECliente.Direccion = response.Direccion;
					oBECliente.IDEmpresa = IDEmpresa();
					BERetornoTran oBERetorno = new BERetornoTran();
					oBERetorno = new BLCliente().ClienteRapidoGuardar(oBECliente);
					BECliente oBEBus = new BLCliente().ClienteXNumeroDocumentoSeleccionar(txtRegNumeroDocumento.Text.Trim(), IDEmpresa());
					hdfIDCliente.Value = oBEBus.IDCliente.ToString();
					txtRegNumeroDocumento.Text = oBEBus.NumeroDocumento;
					txtRegCliente.Text = oBEBus.RazonSocial.Trim();
				}
				else {

					hdfIDCliente.Value = oBE.IDCliente.ToString();
					txtRegNumeroDocumento.Text = oBE.NumeroDocumento;
					txtRegCliente.Text = oBE.RazonSocial.Trim();
				}

			}
		}
		 
		#endregion

		#region ReservaListar

		private void ReservasListar()
		{
			BEReserva oBE = new BEReserva();
			oBE.Filtro = txtBFiltro.Text.Trim();
			oBE.IDSucursal = IDSucursal();
			oBE.IDCliente = Int32.Parse(ddlBIDCliente.SelectedValue);
			oBE.IDTipoComprobante = Int32.Parse(ddlBIDTipoComprobante.SelectedValue);
			oBE.IDEstado = Int32.Parse(ddlBIDEstado.SelectedValue);
			oBE.IDEstadoCobranza = Int32.Parse(ddlBIDEstadoCobranza.SelectedValue);
			oBE.FechaInicio = txtFechaInicio.Text;
			oBE.FechaFin = txtFechaFin.Text;
			gvLista.DataSource = new BLReserva().ReservasListar(oBE);
			gvLista.DataBind();
			upLista.Update();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
            ReservasListar();
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
                hdfIDReserva.Value = e.CommandArgument.ToString();

				switch (e.CommandName)
				{
					case "Editar":
                        ReservaSeleccionar();
                        ReservaDetalleListar();
						pnVentaDetalleListar.Visible = true;
						pnVentaDetalleTempListar.Visible = false;
						lnkPagoRapido.Visible = false;
						upRegistroVenta.Update();
						registrarScript("ActivarTabxId('tab2');");
						break;
					case "Anular":
                        BEReserva oBE = new BLReserva().ReservaSeleccionar(Int32.Parse(hdfIDReserva.Value));
                        ddlIDTipoComprobante.SelectedValue = oBE.IDTipoComprobante.ToString();
						txtNumeroComprobante.Text = oBE.SerieNumero;
						txtMotivoAnulacion.Text = oBE.MotivoAnulacion;
						btnGuardarAnulacion.Visible = true;
						upRegistroVenta.Update();
						upRegistroAnularVenta.Update();
						registrarScript("AbrirModal('DatosVentaAnular');");
						break;
					case "VerMotivoAnulacion":
						BEReserva oBEx = new BLReserva().ReservaSeleccionar(Int32.Parse(hdfIDReserva.Value));
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
			ReservasListar();
		}

		protected void ReservaSeleccionar()
		{
			BEReserva oBE = new BLReserva().ReservaSeleccionar(Int32.Parse(hdfIDReserva.Value));
			ddlRegTipoDocumento.SelectedValue = oBE.IDTipoComprobante.ToString();
			txtSerieNumero.Text = oBE.SerieNumero;
			ddlIDTipoDocumento.SelectedValue = oBE.IDTipoDocumento.ToString();
			hdfIDCliente.Value = oBE.IDCliente.ToString();
			txtRegNumeroDocumento.Text = oBE.NumeroDocumento;
			txtRegCliente.Text = oBE.Cliente;
			ddlRegMoneda.SelectedValue = oBE.IDMoneda;
			txtTipoCambio.Text = oBE.TipoCambio.ToString();
			txtFechaVenta.Text = oBE.FechaVenta.ToShortDateString();

			lblImporteOperacionExonerada.Text = oBE.TotalOperacionExonerada.ToString("N");
			lblImporteSubTotal.Text = oBE.TotalOperacionGravada.ToString("N");
			lblImporteOperacionInafecta.Text = oBE.TotalOperacionInafecta.ToString("N");
			lblImporteOperacionGratuita.Text = oBE.TotalOperacionGratuita.ToString("N");
			lblImporteTotalIgv.Text = oBE.TotalIGV.ToString("N");
			lblImporteTotalVenta.Text = oBE.TotalVenta.ToString("N");

			ddlRegTipoDocumento.Enabled = false;
		}

		protected void btnImprimirVenta_Click(object sender, EventArgs e)
		{
			registrarScript("Imprimir('" + hdfIDReserva.Value + "','VEN');");
		}
		 
		private void ReservaDetalleListar()
		{ 
			gvVentaDetalleListar.DataSource = new BLReservaDetalle().ReservaDetalleListar(Int32.Parse(hdfIDReserva.Value));
			gvVentaDetalleListar.DataBind();
			upRegistroVenta.Update();
		}

		protected void gvVentaDetalleListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvVentaDetalleListar.PageIndex = e.NewPageIndex;
			gvVentaDetalleListar.SelectedIndex = -1;
			ReservasListar(); 
		}

		protected void gvVentaDetalleListar_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{ 
				switch (e.CommandName)
				{
					case "VerLote":
						String[] cmdArgumentos = e.CommandArgument.ToString().Split(new Char[] { ';' });
                        hdfIDReservaDetalle.Value = cmdArgumentos[0].ToString();
						txtVDLCodigoBarra.Text = cmdArgumentos[1].ToString();
						txtVDLProducto.Text = cmdArgumentos[2].ToString();
                        ReservaDetalleLoteListar();
						registrarScript("AbrirModal('ModalVentaDetalleLote');");
						break; 
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLista_RowCommand()", ex.Message, true);
			}
		}

		private void ReservaDetalleLoteListar()
		{
			gvVentaDetalleLoteListar.DataSource = new BLReservaDetalleLote().ReservaDetalleLoteListar(Int32.Parse(hdfIDReservaDetalle.Value));
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
			if (txtRegNumeroDocumento.Text == "") pValidaciones.Append("<div>Seleccione un Cliente</div>");
			if (txtFechaVenta.Text == "") pValidaciones.Append("<div>Seleccione una Fecha Venta</div>");

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}
			ddlVIDFormaPago.SelectedValue = "1";
			ltTotalPagar.Text = lblImporteTotalVenta.Text;
			ltImporteLetras.Text = NumeroALetrasTexto(Decimal.Parse(ltTotalPagar.Text));
			txtEfectivo.Text = lblImporteTotalVenta.Text;
			CalcularVuelto();
			upVentaFormaPago.Update();
			registrarScript("AbrirModal('ModalFormaPago')");
		}

		protected void btnConfirmarVenta_Click(object sender, EventArgs e)
		{
			//Validaciones
			StringBuilder pValidaciones = new StringBuilder();
			if (txtRegNumeroDocumento.Text == "") pValidaciones.Append("<div>Seleccione un Cliente</div>");
			if (txtFechaVenta.Text == "") pValidaciones.Append("<div>Seleccione una Fecha Reserva</div>");
			if (ddlVIDFormaPago.SelectedValue == "0") pValidaciones.Append("<div>Seleccione Forma de Pago</div>");

			if (Decimal.Parse(ltVuelto.Text) > 0 || Decimal.Parse(ltVuelto.Text) < 0) pValidaciones.Append("<div>El monto amortizado no es igual al total a pagar.</div>");

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			BLReserva oBLReserv = new BLReserva();
			BEReserva oBEReserv = new BEReserva();
			ArrayList ListaBEReservaDetalle = new ArrayList();
			ArrayList ListaBEReservaFormaPago = new ArrayList();
			ArrayList ListaBEReservaDetalleLote = new ArrayList();


			oBEReserv.IDReserva = Int32.Parse(hdfIDReserva.Value);
			oBEReserv.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
			oBEReserv.IDSucursal = IDSucursal();
			oBEReserv.IDCliente = Int32.Parse(hdfIDCliente.Value);
			oBEReserv.IDMoneda = ddlRegMoneda.SelectedValue;
			oBEReserv.TipoCambio = Decimal.Parse(txtTipoCambio.Text);
			oBEReserv.IDTipoComprobante = Int32.Parse(ddlRegTipoDocumento.SelectedValue);
			oBEReserv.SerieNumero = txtSerieNumero.Text;
			oBEReserv.FechaVenta = DateTime.Parse(txtFechaVenta.Text);
			oBEReserv.CalculoIGV = Decimal.Parse("0.18");
            oBEReserv.CalculoISC = Decimal.Parse("0.10");

            oBEReserv.CalculoDetraccion = Decimal.Parse("0.10");
            oBEReserv.MontoDetraccion = 0;
			if (Decimal.Parse(lblImporteTotalVenta.Text) >= 700)
			{
                oBEReserv.MontoDetraccion = Decimal.Parse(lblImporteTotalVenta.Text) * oBEReserv.CalculoDetraccion;
			}
			oBEReserv.TotalOperacionGravada = Decimal.Parse(lblImporteSubTotal.Text);
			oBEReserv.TotalOperacionExonerada = Decimal.Parse(lblImporteOperacionExonerada.Text);
			oBEReserv.TotalOperacionInafecta = Decimal.Parse(lblImporteOperacionInafecta.Text);
			oBEReserv.TotalOperacionGratuita = Decimal.Parse(lblImporteOperacionGratuita.Text);
			oBEReserv.TotalIGV = Decimal.Parse(lblImporteTotalIgv.Text);
			oBEReserv.TotalISC = 0;
			oBEReserv.TotalDescuentos = 0;
			oBEReserv.TotalOtrosTributos = 0;
			oBEReserv.TotalVenta = Decimal.Parse(lblImporteTotalVenta.Text);
            
			oBEReserv.Migrado = "N";
			oBEReserv.Proceso = "RESERVA";
			oBEReserv.IDReservaRelacionado = 0;
            oBEReserv.IDUsuario = IDUsuario();

			BEReservaFormaPago oRFP = new BEReservaFormaPago();
			oRFP.IDReserva = 0;
			oRFP.IDFormaPago = Int32.Parse(ddlVIDFormaPago.SelectedValue);
			oRFP.IDTarjetaCredito = 0;
			oRFP.NumeroOperacion = String.Empty;
			oRFP.MontoPagado = 0;
			oRFP.Efectivo = Decimal.Parse(txtEfectivo.Text);
			oRFP.Tarjeta = Decimal.Parse(txtTarjeta.Text);
			oRFP.Transferencia = Decimal.Parse(txtTransferencia.Text);
			oRFP.Referencia = txtReferencia.Text.Trim();
            oRFP.IDUsuario = IDUsuario();

			BERetornoTran oBERetorno = new BERetornoTran();

			if (hdfIDReserva.Value.Equals("0"))
			{
				oBERetorno = oBLReserv.ReservaRapidaGuardar(oBEReserv, oRFP);
                hdfIDReserva.Value = oBERetorno.Retorno2;
			}
			else {
				oBERetorno = oBLReserv.ActualizarReserva(oBEReserv, ListaBEReservaDetalle, oRFP, ListaBEReservaDetalleLote);
			}

			if (oBERetorno.Retorno == "1")
			{
				LimpiarFormularioVenta();
                ReservaDetalleTempListar();
				CargaSerie();
				ReservasListar();
				msgbox(TipoMsgBox.confirmation, "Sistema", "La operación se ha registrado con éxito");
				registrarScript("CerrarModal('ModalFormaPago')");
			}
			else if (oBERetorno.Retorno == "2")
			{
				msgbox(TipoMsgBox.warning, "Sistema", "No puedes realizar una factura a un cliente con DNI");
			}
			else if (oBERetorno.Retorno == "-1")
			{
				RegistrarLogSistema("lnkGuardarVenta_Click()", oBERetorno.ErrorMensaje, true);
			}
		}
		  
		private void LimpiarFormularioVenta()
		{
            hdfIDReserva.Value = "0";
			hdfIDReservaDetalle.Value = "0";
			txtFiltroProducto.Text = String.Empty;
			ddlIDTipoDocumento.SelectedIndex = -1;
			hdfIDCliente.Value = "1";
			txtRegNumeroDocumento.Text = "00000000";
			txtRegCliente.Text = "PÚBLICO EN GENERAL";
			ddlRegMoneda.SelectedIndex = -1;
			ddlRegMoneda.Enabled = true;
			txtTipoCambio.Text = "1";
			txtFechaVenta.Text = DateTime.Today.ToShortDateString();
			ddlRegTipoDocumento.SelectedValue = "2";
			ddlRegTipoDocumento.Enabled = true;
			txtSerieNumero.Text = String.Empty;
			pnVentaDetalleListar.Visible = false;
			pnVentaDetalleTempListar.Visible = true;
			lnkPagoRapido.Visible = true;
			CalcularTotalesTemp();
			upRegistroVenta.Update();
		}
		 
		#endregion

		#region Cliente
		 
		private void ListarCliente()
		{
			BLCliente oBLCliente = new BLCliente();
			gvListadoCliente.DataSource = oBLCliente.ClienteListar(txtFiltro.Text.Trim(), IDEmpresa());
			gvListadoCliente.DataBind();
		}

		protected void gvListadoCliente_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvListadoCliente.PageIndex = e.NewPageIndex;
			gvListadoCliente.SelectedIndex = -1;
			ListarCliente();
		}

		protected void btnBuscarCliente_Click(object sender, EventArgs e)
		{
			ListarCliente();
		}

		protected void gvListadoCliente_SelectedIndexChanged(object sender, EventArgs e)
		{
			int i = gvListadoCliente.SelectedIndex;
			hdfIDCliente.Value = ((Label)this.gvListadoCliente.Rows[i].Cells[0].Controls[1]).Text;
			txtRegNumeroDocumento.Text = ((Label)this.gvListadoCliente.Rows[i].Cells[1].Controls[1]).Text;
			txtRegCliente.Text = ((Label)this.gvListadoCliente.Rows[i].Cells[2].Controls[1]).Text;
			upRegistroVenta.Update();
			registrarScript("CerrarModal('DatosCliente');");
		}

		#endregion

		#region AnularVenta

		protected void btnGuardarAnulacion_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder pValidaciones = new StringBuilder();
				if (hdfIDReserva.Value == "0") pValidaciones.Append("<div>Seleccione una Venta a Anular</div>");
				if (txtMotivoAnulacion.Text == "") pValidaciones.Append("<div>Ingrese Motivo Anulación</div>");
				if (pValidaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, pValidaciones.ToString());
					return;
				}

				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLVenta().VentaAnular(Int32.Parse(hdfIDReserva.Value), txtMotivoAnulacion.Text.Trim(), IDUsuario());

				if (oBERetorno.Retorno == "1")
				{ 
					ReservasListar();
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

		protected void btnCancelarAnulacion_Click(object sender, EventArgs e)
		{
			registrarScript("funModalVentaAnularCerrar()");
		}

		protected void LimpiarFormularioAnularVenta()
		{
			hdfIDReserva.Value = "0";
			ddlIDTipoComprobante.SelectedIndex = -1;
			txtNumeroComprobante.Text = "";
			txtMotivoAnulacion.Text = "";
			upRegistroAnularVenta.Update();
		}

		#endregion

		#region Producto

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
			if (itemProd.ControlaLote)
			{
				hfSLIDProducto.Value = itemProd.IDProducto.ToString();
				txtSLCodigoBarra.Text = itemProd.CodigoBarra.Trim();
				txtSLProducto.Text = itemProd.Nombre.Trim();
				LotexProductoListar();
				registrarScript("AbrirModal('ModalSalidaManualLote')");
			}
			else {
				BEReservaDetalle oBEVD = new BEReservaDetalle();
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

				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLReservaDetalle().ReservaDetalleTempGuardar(oBEVD);

				if (oBERetorno.Retorno == "1")
				{ 
					pnVentaDetalleTempListar.Visible = true;
					pnVentaDetalleListar.Visible = false;
                    ReservaDetalleTempListar();
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
			gvLotexProducto.DataSource = new BLLote().LoteListar(Int32.Parse(hfSLIDProducto.Value),IDSucursal());
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
			ltVuelto.Text = ((Decimal.Parse(txtEfectivo.Text) + Decimal.Parse(txtTarjeta.Text) + Decimal.Parse(txtTransferencia.Text)) - Decimal.Parse(lblImporteTotalVenta.Text)).ToString("N");
		}

		#endregion

		#region DetalleVentaTemp

		private void ReservaDetalleTempListar()
		{
			gvVentaDetalleTempListar.DataSource = new BLReservaDetalle().ReservaDetalleTempListar(IDUsuario(), "");
			gvVentaDetalleTempListar.DataBind();
			CalcularTotalesTemp();
			upRegistroVenta.Update();
		}

		protected void gvVentaDetalleTempListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvVentaDetalleTempListar.PageIndex = e.NewPageIndex;
			gvVentaDetalleTempListar.SelectedIndex = -1;
            ReservaDetalleTempListar();
		}

		protected void gvVentaDetalleTempListar_RowCommand(object sender, GridViewCommandEventArgs e)
		{

			BERetornoTran oBERetorno = new BERetornoTran();

			switch (e.CommandName)
			{
				case "Editar":
					String[] cmdArgumentos = e.CommandArgument.ToString().Split(new Char[] { ';' });
                    hdfIDReservaDetalleTemp.Value = cmdArgumentos[0].ToString();
					txtVLCodigoBarra.Text = cmdArgumentos[1].ToString();
					txtVLProducto.Text = cmdArgumentos[2].ToString();
					Boolean ControlaLote = Boolean.Parse(cmdArgumentos[3].ToString());
					if (ControlaLote)
					{
                        ReservaDetalleLoteTempListar();
						registrarScript("AbrirModal('ModalVentaDetalleLoteTemp');");
					}
					else {
						txtVDCodigoBarra.Text = cmdArgumentos[1].ToString();
						txtVDProducto.Text = cmdArgumentos[2].ToString();
						txtVDCantidad.Text = cmdArgumentos[4].ToString();
						upVentaDetalleActualizar.Update();
						registrarScript("AbrirModal('ModalVentaDetalle');");
					}

					break;
				case "Eliminar":
                    hdfIDReservaDetalleTemp.Value = e.CommandArgument.ToString();
					oBERetorno = new BLReservaDetalle().ReservaDetalleTempEliminar(Int32.Parse(hdfIDReservaDetalleTemp.Value));
					if (oBERetorno.Retorno == "1")
					{
                        ReservaDetalleTempListar();
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

		protected void btnAgregarLote_Click(object sender, EventArgs e)
		{
			try
			{
				ArrayList aLoteProducto = new ArrayList();
                BEReservaDetalleLote oBE;
				Decimal pCantidad = 0;
				foreach (GridViewRow row in gvLotexProducto.Rows)
				{
					if (row.RowType == DataControlRowType.DataRow)
					{
						oBE = new BEReservaDetalleLote();
						oBE.IDReservaDetalleLoteTemp = 0;
						oBE.IDProducto = Int32.Parse(hfSLIDProducto.Value);
						oBE.IDLote = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hfIDLote")).Value);
						oBE.IDSucursal = IDSucursal();
						oBE.CantidadLote = Int32.Parse(((TextBox)row.Cells[4].FindControl("txtSLSalidaManual")).Text);
						oBE.IDUsuario = IDUsuario();
						pCantidad += Decimal.Parse(oBE.CantidadLote.ToString());
						if (oBE.CantidadLote > 0)
						{
							if (aLoteProducto == null)
							{
								aLoteProducto = new ArrayList();
							}
							aLoteProducto.Add(oBE);
						}
					}
				}

				BEProducto itemProd = new BLProducto().ProductoSeleccionar(Int32.Parse(hfSLIDProducto.Value), IDSucursal());
				BEReservaDetalle oBEVD = new BEReservaDetalle();
				
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

				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLReservaDetalle().ReservaDetalleTempLoteGuardar(oBEVD, aLoteProducto);

				if (oBERetorno.Retorno == "1")
				{
					ReservaDetalleTempListar();
					//CalcularTotales();
					msgbox(TipoMsgBox.confirmation, "Sistema", Constantes.MENSAJE_EXITO);
					registrarScript("CerrarModal('ModalSalidaManualLote')");
				}
				else {
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
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
			Decimal SubTotal = 0;
			foreach (GridViewRow fila in gvVentaDetalleTempListar.Rows)
			{
				SubTotal += Decimal.Parse(((HiddenField)fila.Cells[0].FindControl("hfSubTotal")).Value);
			}

			Decimal TotalIgv = Math.Round(SubTotal * Decimal.Parse("0.18"), 2, MidpointRounding.AwayFromZero);
			Decimal TotalVenta = Math.Round(TotalIgv + SubTotal, 2, MidpointRounding.AwayFromZero);

			lblImporteOperacionExonerada.Text = "0.00";
			lblImporteOperacionInafecta.Text = "0.00";
			lblImporteOperacionGratuita.Text = "0.00";
			lblImporteSubTotal.Text = Math.Round(SubTotal, 2, MidpointRounding.AwayFromZero).ToString("N");
			lblImporteTotalIgv.Text = TotalIgv.ToString("N2");
			lblImporteTotalVenta.Text = TotalVenta.ToString("N");
		}

		#endregion

		#region DetalleVentaLote

		private void ReservaDetalleLoteTempListar()
		{
			gvVentaDetalleLoteTempListar.DataSource = new BLReservaDetalleLote().ReservaDetalleLoteTempListar(Int32.Parse(hdfIDReservaDetalleTemp.Value));
			gvVentaDetalleLoteTempListar.DataBind();
			upVentaDetalleLoteTempListar.Update();
		}

		protected void btnActualizarVentaDetalleLote_Click(object sender, EventArgs e)
		{
			BEReservaDetalleLote oBE;
			ArrayList aLoteProducto = new ArrayList();
			foreach (GridViewRow row in gvVentaDetalleLoteTempListar.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					oBE = new BEReservaDetalleLote();
					oBE.IDReservaDetalleLoteTemp = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hfVDTIDVentaDetalleLoteTemp")).Value);
					oBE.IDLote = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hfVDTIDLote")).Value);
					oBE.CantidadLote = Decimal.Parse(((TextBox)row.Cells[4].FindControl("txtVDTSalidaManual")).Text);

					if (aLoteProducto == null)
					{
						aLoteProducto = new ArrayList();
					}
					aLoteProducto.Add(oBE);

				}
			}
			BERetornoTran oBERetorno = new BERetornoTran();
			foreach (BEReservaDetalleLote item in aLoteProducto)
			{

                BEReservaDetalleLote oBEVDL = new BEReservaDetalleLote();
				oBEVDL.IDReservaDetalleLoteTemp = item.IDReservaDetalleLoteTemp;
				oBEVDL.IDReservaDetalleTemp = 0;
				oBEVDL.IDProducto = 0;
				oBEVDL.IDLote = 0;
				oBEVDL.IDSucursal = 0;
				oBEVDL.Cantidad = item.CantidadLote;
				oBEVDL.IDUsuario = IDUsuario();
				oBERetorno = new BLReservaDetalleLote().ReservaDetalleLoteTempGuardar(oBEVDL);
			}

			if (oBERetorno.Retorno == "1")
			{
				BEReservaDetalle oBEVD = new BEReservaDetalle();
				oBEVD.IDReservaDetalleTemp = Int32.Parse(hdfIDReservaDetalleTemp.Value);
				oBEVD.Cantidad = 0;
				oBEVD.IDUsuario = IDUsuario();
				oBERetorno = new BLReservaDetalle().ReservaDetalleTempActualizar(oBEVD);
                ReservaDetalleTempListar();
				msgbox(TipoMsgBox.confirmation, "Sistema", Constantes.MENSAJE_EXITO);
				registrarScript("CerrarModal('ModalVentaDetalleLoteTemp')");
			}
			else {
				if (oBERetorno.Retorno != "-1")
				{
					msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
				}
				else {
					RegistrarLogSistema("btnActualizarVentaDetalleLote_Click()", oBERetorno.ErrorMensaje, true);
				}
			}
		}

		protected void btnActualizarVentaDetalle_Click(object sender, EventArgs e)
		{
			BERetornoTran oBERetorno = new BERetornoTran();
			BEReservaDetalle oBEVD = new BEReservaDetalle();
			oBEVD.IDReservaDetalleTemp = Int32.Parse(hdfIDReservaDetalleTemp.Value);
			oBEVD.Cantidad = Decimal.Parse(txtVDCantidad.Text);
			oBEVD.IDUsuario = IDUsuario();
			oBERetorno = new BLReservaDetalle().ReservaDetalleTempActualizar(oBEVD);
            ReservaDetalleTempListar();
			msgbox(TipoMsgBox.confirmation, "Sistema", Constantes.MENSAJE_EXITO);
			registrarScript("CerrarModal('ModalVentaDetalle')");
		}
		#endregion
		 
	}
}