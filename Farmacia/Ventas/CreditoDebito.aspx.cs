
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Ventas
{
	public partial class CreditoDebito : PageBase
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
			pnMensaje.Visible = oBERetorno.Retorno == "1" ? false : true;
			pnRegistro.Visible = oBERetorno.Retorno == "1" ? true : false;
			if (oBERetorno.Retorno == "1")
			{
				txtFechaInicio.Text = DateTime.Today.ToShortDateString();
				txtFechaFin.Text = DateTime.Today.ToShortDateString();
				CargarDDL(ddlIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("CD", "VEN"), "IDTipoComprobante", "Nombre", false);
				CargarDDL(ddlIDTipoComprobanteAfectado, new BLTipoComprobante().TipoComprobanteListar("DE", "CDE"), "IDTipoComprobante", "Nombre", false);
				CargarDDL(ddlVEIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("DE","CDE"), "IDTipoComprobante", "Nombre", true, Constantes.TODOS);
				CargarDDL(ddlTipoMotivo, new BLTipoMotivo().TipoMotivoListar(Int32.Parse(ddlIDTipoComprobante.SelectedValue)), "IDTipoMotivo", "Nombre", true, Constantes.SELECCIONAR);
				CargarDDL(ddlBIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("CD","VEN"), "IDTipoComprobante", "Nombre", true, Constantes.TODOS);
				CargarDDL(ddlBIDCliente, new BLCliente().ClienteListar("", IDEmpresa()), "IDCliente", "RazonSocial", true, Constantes.TODOS);
				CargarDDL(ddlBIDEstado, new BLEstado().EstadoListar("VEN"), "Codigo", "Nombre", true, Constantes.TODOS);
				CargarDDL(ddlBIDEstadoCobranza, new BLEstado().EstadoListar("COB"), "Codigo", "Nombre", true, Constantes.TODOS);
				CargarDDL(ddlVIDFormaPago, new BLFormaPago().FormaPagoListar(), "IDFormaPago", "Nombre", true, Constantes.SELECCIONAR);
				pnVentaDetalleTempListar.Visible = true;
				pnVentaDetalleListar.Visible = false;
				CargaSerie();
				VentaListar();
				VentaDetalleTempListar();
			}
				

		}

		private void CargaSerie()
		{
			String pIDTipoDocumento = ddlIDTipoComprobante.SelectedValue;
			String pIDTipoComprobanteAfectado = ddlIDTipoComprobanteAfectado.SelectedValue;
			txtSerieNumero.Text = new BLDocumentoSerie().DocumentoSerieNotaSeleccionar(pIDTipoDocumento, pIDTipoComprobanteAfectado,  IDSucursal());
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
			oBE.Accion = "CREDITO";
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
			ddlIDTipoComprobante.SelectedValue = oBE.IDTipoComprobante.ToString();
			txtSerieNumero.Text = oBE.SerieNumero; 
			hdfIDCliente.Value = oBE.IDCliente.ToString();
			 
			txtRegClienteAfectado.Text = oBE.Cliente;
			  
			lblImporteOperacionExonerada.Text = oBE.TotalOperacionExonerada.ToString("N");
			lblImporteSubTotal.Text = oBE.TotalOperacionGravada.ToString("N");
			lblImporteOperacionInafecta.Text = oBE.TotalOperacionInafecta.ToString("N");
			lblImporteOperacionGratuita.Text = oBE.TotalOperacionGratuita.ToString("N");
			lblImporteTotalIgv.Text = oBE.TotalIGV.ToString("N");
			lblImporteTotalVenta.Text = oBE.TotalVenta.ToString("N");

			ddlIDTipoComprobante.Enabled = false;
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
		    if (hdfIDVentaAfectado.Value == "0") pValidaciones.Append("<div>Seleccione Comprobante afectado</div>");
			if (ddlIDTipoComprobante.SelectedValue == "0") pValidaciones.Append("<div>Seleccione un Tipo Comprobante</div>");
			if (ddlTipoMotivo.SelectedValue == "0") pValidaciones.Append("<div>Seleccione Tipo Motivo</div>");
		    if (txtMotivo.Text.Trim().Length == 0) pValidaciones.Append("<div>Ingrese Motivo</div>");
			if (txtFechaEmision.Text.Trim().Length == 0) pValidaciones.Append("<div>Seleccione Fecha Emisión</div>");
			if (txtFechaEmision.Text.Trim().Length > 0) if (!esFecha(txtFechaEmision.Text.Trim())) pValidaciones.Append("<div>Seleccione Fecha Emisión válida</div>");
			 
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
			//if (txtRegNumeroDocumento.Text == "") pValidaciones.Append("<div>Seleccione un Cliente</div>");
			//if (txtFechaVenta.Text == "") pValidaciones.Append("<div>Seleccione una Fecha Venta</div>");
			if (ddlVIDFormaPago.SelectedValue == "0") pValidaciones.Append("<div>Seleccione Forma de Pago</div>");

			if (Decimal.Parse(ltVuelto.Text) > 0 || Decimal.Parse(ltVuelto.Text) < 0) pValidaciones.Append("<div>El monto amortizado no es igual al total a pagar.</div>");

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
			oBEVenta.IDMoneda = hdfIDMoneda.Value;
			oBEVenta.TipoCambio = Int32.Parse("1");
			oBEVenta.IDTipoComprobante = Int32.Parse(ddlIDTipoComprobante.SelectedValue);
			oBEVenta.SerieNumero = txtSerieNumero.Text;
			oBEVenta.FechaVenta = DateTime.Today;
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
			oBEVenta.TotalDescuentos = 0;
			oBEVenta.TotalOtrosTributos = 0;
			oBEVenta.TotalVenta = Decimal.Parse(lblImporteTotalVenta.Text);

			oBEVenta.Migrado = "N";
			oBEVenta.Proceso = "NOTA";
			oBEVenta.IDVentaRelacionado = Int32.Parse(hdfIDVentaAfectado.Value);
			oBEVenta.IDUsuario = IDUsuario();
			oBEVenta.IDTipoMotivo = Int32.Parse(ddlTipoMotivo.SelectedValue);
			oBEVenta.MotivoNota = txtMotivo.Text.Trim();
			 
			BEVentaFormaPago oVFP = new BEVentaFormaPago();
			  
			oVFP.IDVenta = 0;
			oVFP.IDFormaPago = 0;
			oVFP.IDTarjetaCredito = 0;
			oVFP.NumeroOperacion = String.Empty;
			oVFP.MontoPagado = Decimal.Parse(txtEfectivo.Text);
			oVFP.Efectivo = Decimal.Parse(txtEfectivo.Text);
			oVFP.Tarjeta = Decimal.Parse(txtTarjeta.Text);
			oVFP.Transferencia = Decimal.Parse(txtTransferencia.Text);
			oVFP.Credito = 0;
			oVFP.Referencia = txtReferencia.Text.Trim();
			oVFP.DiaCredito = 0;
			oVFP.FechaVencimiento = DateTime.Parse(Constantes.FECHA_NULA);
			oVFP.IDUsuario = IDUsuario();
			 
			BEVentaRecetaMedica oVRE = new BEVentaRecetaMedica();
			oVRE.IDVentaRecetaMedica = 99;
			oVRE.IDVenta = 0;
			oVRE.FolioReceta = "";
			oVRE.RecetaRetenida = false;
			oVRE.NumeroDocumento = "";
			oVRE.NombresCompleto = "";
			oVRE.Direccion = "";
			oVRE.CMP = "";
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
				registrarScript("CerrarModal('ModalFormaPago')");
			}
			else if (oBERetorno.Retorno == "2")
			{
				msgbox(TipoMsgBox.warning, "Facturacion", "No puedes realizar una factura a un cliente con DNI");
			}
			else if (oBERetorno.Retorno == "-1")
			{
				RegistrarLogSistema("lnkGuardarVenta_Click()", oBERetorno.ErrorMensaje, true);
			}
		}

		private void LimpiarFormularioVenta()
		{
			hdfIDVenta.Value = "0";
			hdfIDVentaDetalle.Value = "0";
			txtSerieNumeroAfectado.Text = String.Empty;
			ddlIDTipoComprobanteAfectado.SelectedIndex = -1;
			 
			txtRegClienteAfectado.Text = String.Empty; 
			hdfIDCliente.Value = "0";
			hdfIDMoneda.Value = "0";
			ddlIDTipoComprobante.SelectedIndex = -1;
			ddlIDTipoComprobante.Enabled = true;
			txtSerieNumero.Text = String.Empty;
			ddlTipoMotivo.SelectedIndex = -1;
			txtMotivo.Text = String.Empty;

			pnVentaDetalleListar.Visible = false;
			pnVentaDetalleTempListar.Visible = true;
			lnkPagoRapido.Visible = true;
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = new BLVentaDetalle().VentaDetalleTempxUsuarioEliminar(IDUsuario(),"NOTA");
			VentaDetalleTempListar(); 
			upRegistroVenta.Update();
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
				oBEVD.Proceso = "NOTA";
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLVentaDetalle().VentaDetalleTempGuardar(oBEVD);

				if (oBERetorno.Retorno == "1")
				{
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
						RegistrarLogSistema("AgregarProductoDetalleTemp()", oBERetorno.ErrorMensaje, true);
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
			ltVuelto.Text = ((Decimal.Parse(txtEfectivo.Text) + Decimal.Parse(txtTarjeta.Text) + Decimal.Parse(txtTransferencia.Text)) - Decimal.Parse(lblImporteTotalVenta.Text)).ToString("N");
		}

		#endregion

		#region DetalleVentaTemp

		private void VentaDetalleTempListar()
		{
			gvVentaDetalleTempListar.DataSource = new BLVentaDetalle().VentaDetalleTempListar(IDUsuario(), "NOTA");
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
						txtVDCodigoBarra.Text = cmdArgumentos[1].ToString();
						txtVDProducto.Text = cmdArgumentos[2].ToString();
						txtVDCantidad.Text = cmdArgumentos[4].ToString();
						upVentaDetalleActualizar.Update();
						registrarScript("AbrirModal('ModalVentaDetalle');");
					}

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

		protected void btnAgregarLote_Click(object sender, EventArgs e)
		{
			try
			{
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
				oBEVD.Proceso = "NOTA";
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

			foreach (GridViewRow fila in gvVentaDetalleTempListar.Rows)
			{
				TotalVenta += Decimal.Parse(((HiddenField)fila.Cells[0].FindControl("hfImporteTotal")).Value);
				Descuento += Decimal.Parse(((HiddenField)fila.Cells[0].FindControl("hfDescuento")).Value); 
			}

			Decimal SubTotal = Math.Round(TotalVenta / Decimal.Parse("1.18"), 2, MidpointRounding.AwayFromZero);
			Decimal TotalIgv = Math.Round(TotalVenta - SubTotal, 2, MidpointRounding.AwayFromZero);

			lblImporteOperacionExonerada.Text = "0.00";
			lblImporteOperacionInafecta.Text = "0.00";
			lblImporteOperacionGratuita.Text = "0.00";
			lblImporteSubTotal.Text = Math.Round(SubTotal, 2, MidpointRounding.AwayFromZero).ToString("N");
			lblImporteTotalIgv.Text = TotalIgv.ToString("N2");
			lblDescuento.Text = Descuento.ToString("N");
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
			StringBuilder pValidaciones = new StringBuilder();
			BEVentaDetalleLote oBE;
			ArrayList aLoteProducto = new ArrayList();
			foreach (GridViewRow row in gvVentaDetalleLoteTempListar.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					oBE = new BEVentaDetalleLote();
					oBE.IDVentaDetalleLoteTemp = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hfVDTIDVentaDetalleLoteTemp")).Value);
					oBE.IDLote = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hfVDTIDLote")).Value);
					Decimal CantidadVendida = Decimal.Parse(((HiddenField)row.Cells[0].FindControl("hfVDTCantidad")).Value); 
					oBE.CantidadLote = Decimal.Parse(((TextBox)row.Cells[5].FindControl("txtVDTSalidaManual")).Text); 
					if (oBE.CantidadLote > CantidadVendida) pValidaciones.Append("<div>La cantidad a devolver no puede ser superior a la cantidad del item</div>");

					if (pValidaciones.Length > 0)
					{
						msgbox(TipoMsgBox.warning, pValidaciones.ToString());
						return;
					}
					 
					if (aLoteProducto == null)
					{
						aLoteProducto = new ArrayList();
					}
					aLoteProducto.Add(oBE);

				}
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
			StringBuilder pValidaciones = new StringBuilder(); 
			if (Decimal.Parse(txtVDDevolver.Text) > Decimal.Parse(txtVDCantidad.Text)) pValidaciones.Append("<div>La cantidad a devolver no puede ser superior a la cantidad del item</div>");
			 
			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}
			 
			BERetornoTran oBERetorno = new BERetornoTran();
			BEVentaDetalle oBEVD = new BEVentaDetalle();
			oBEVD.IDVentaDetalleTemp = Int32.Parse(hdfIDVentaDetalleTemp.Value);
			oBEVD.Cantidad = Decimal.Parse(txtVDDevolver.Text);
			oBEVD.IDUsuario = IDUsuario();
			oBERetorno = new BLVentaDetalle().VentaDetalleTempActualizar(oBEVD);
			VentaDetalleTempListar();
			msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
			registrarScript("CerrarModal('ModalVentaDetalle')");
		}
		#endregion
		  
		#region DocumentoElectronico

		private void VentasAfectadoListar()
		{
			BEVenta oBE = new BEVenta();
			oBE.Filtro = txtVEFiltro.Text.Trim();
			oBE.IDSucursal = IDSucursal();
			oBE.IDCliente = 0;
			oBE.IDTipoComprobante = Int32.Parse(ddlVEIDTipoComprobante.SelectedValue);
			oBE.IDEstado = 0;
			oBE.IDEstadoCobranza = 0;
			oBE.FechaInicio = "01/01/2000";
			oBE.FechaFin = DateTime.Today.ToShortDateString();
			oBE.Accion = "SINNOTA";
			oBE.IDUsuario = IDUsuario();
			gvDocumentoElectronicoListar.DataSource = new BLVenta().VentasListar(oBE);
			gvDocumentoElectronicoListar.DataBind();
			upVentaAfectadaListar.Update();
		}

		protected void gvDocumentoElectronicoListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvDocumentoElectronicoListar.PageIndex = e.NewPageIndex;
			gvDocumentoElectronicoListar.SelectedIndex = -1;
			VentasAfectadoListar();
		}

		protected void btnBuscarComprobanteAfectado_Click(object sender, EventArgs e)
		{
			VentasAfectadoListar();
		}

		protected void gvDocumentoElectronicoListar_SelectedIndexChanged(object sender, EventArgs e)
		{
			hdfIDVentaAfectado.Value = gvDocumentoElectronicoListar.SelectedDataKey["IDVenta"].ToString(); 
			BEVenta oBE = new BLVenta().VentaSeleccionar(Int32.Parse(hdfIDVentaAfectado.Value));
			ddlIDTipoComprobanteAfectado.SelectedValue = oBE.IDTipoComprobante.ToString();
			txtSerieNumeroAfectado.Text = oBE.SerieNumero; 
			txtRegClienteAfectado.Text = oBE.Cliente; 
			hdfIDCliente.Value = oBE.IDCliente.ToString();
			hdfIDMoneda.Value = oBE.IDMoneda.ToString();

			lblImporteOperacionExonerada.Text = oBE.TotalOperacionExonerada.ToString("N");
			lblImporteSubTotal.Text = oBE.TotalOperacionGravada.ToString("N");
			lblImporteOperacionInafecta.Text = oBE.TotalOperacionInafecta.ToString("N");
			lblImporteOperacionGratuita.Text = oBE.TotalOperacionGratuita.ToString("N");
			lblImporteTotalIgv.Text = oBE.TotalIGV.ToString("N");
			lblImporteTotalVenta.Text = oBE.TotalVenta.ToString("N");
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = new BLVentaDetalle().VentaDetalleTempNotaCreditoGuardar(Int32.Parse(hdfIDVentaAfectado.Value), IDUsuario());
			VentaDetalleTempListar();
			CargaSerie();
			pnVentaDetalleTempListar.Visible = true;
			registrarScript("CerrarModal('ModalVentaAfectado')");

		}

		#endregion

		protected void lnkBuscarComprobante_Click(object sender, EventArgs e)
		{
			VentasAfectadoListar(); 
			registrarScript("AbrirModal('ModalVentaAfectado')");
		}

		protected void ddlIDTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
		{
			CargaSerie();
		    CargarDDL(ddlTipoMotivo, new BLTipoMotivo().TipoMotivoListar(Int32.Parse(ddlIDTipoComprobante.SelectedValue)), "IDTipoMotivo", "Nombre", true, Constantes.SELECCIONAR);
		}


		protected void btnMigrar_Click(object sender, EventArgs e)
		{
			try
			{
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLVenta().CreditoDebitoMigrarInsertar(0, txtFechaInicio.Text, txtFechaFin.Text, IDUsuario(), IDEmpresa(), IDSucursal());
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
					else
					{
						RegistrarLogSistema("btnMigrar_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnMigrar_Click()", ex.ToString(), true);
			}
		}



	}
}