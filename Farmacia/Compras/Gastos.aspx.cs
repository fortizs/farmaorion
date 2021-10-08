using Farmacia.App_Class;
using Farmacia.App_Class.BE.Compras;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.Compras;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Compras
{
	public partial class Gastos : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
				CargarDDL(ddlSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", false);
				txtFechaInicio.Text = DateTime.Today.ToShortDateString();
				txtFechaFin.Text = DateTime.Today.ToShortDateString();
				Listar();
			}
		}

		private void CargaInicial()
		{
			hdfIDSucursal.Value = IDSucursal().ToString();
			ArrayList aListaGasto = new ArrayList();
			if (Session["aListaGasto"] == null) //Trabajando en Memoria
			{
				Session["aListaGasto"] = aListaGasto;
			}
			else
			{
				aListaGasto = (ArrayList)(Session["aListaGasto"]);
			}
			LlenargvListaGasto(aListaGasto);
			CargarDDL(ddlRegMoneda, new BLMoneda().MonedaListar(), "IDMoneda", "Nombre", false);
			CargarDDL(ddlRegTipoDocumento, new BLTipoComprobante().TipoComprobanteListar("DE","VEN"), "IDTipoComprobante", "Nombre", true, Constantes.SELECCIONAR);
			CargarDDL(ddlRegFormaPago, new BLFormaPago().FormaPagoListar(), "IDFormaPago", "Nombre", true);
			CargarDDL(ddlRegIDTipoDocumento, new BLTipoDocumento().TipoDocumentoListar(), "IDTipoDocumento", "Nombre", false);
			ddlRegIDTipoDocumento.SelectedValue = "3";
			CalcularTotales();
			NumeroGastoeleccionar();

		}

		protected void NumeroGastoeleccionar()
		{
			lblNumeroCompra.Text = new BLGasto().GastoNumeroSeleccionar("G");
		}

		#endregion

		#region Listar 
		private void Listar()
		{
			BLGasto oBLGasto = new BLGasto();
			gvLista.DataSource = oBLGasto.GastoListar(Int32.Parse(ddlSucursal.SelectedValue), txtCOMFiltro.Text.Trim(), txtFechaInicio.Text, txtFechaFin.Text);
			gvLista.DataBind();
			upLista.Update();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			Listar();
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			hdfIDCompra.Value = e.CommandArgument.ToString();
			BERetornoTran oBERetorno = new BERetornoTran();

			switch (e.CommandName)
			{
				case "Editar":
					GastoSeleccionar();
					ListarCompraDetalle();
					upRegistroCompra.Update();
					registrarScript("ActivarTabxId('tab2');");
					break;
				case "Aprobar":
					oBERetorno = new BLGasto().GastoCambiarEstado(Int32.Parse(hdfIDCompra.Value), 8, IDUsuario());
					if (oBERetorno.Retorno == "1")
					{
						msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
						Listar();
					}
					else if (oBERetorno.Retorno == "-1")
					{
						RegistrarLogSistema("gvLista_RowCommand()", oBERetorno.ErrorMensaje, true);
					}
					break;
				case "Anular":
					oBERetorno = new BLGasto().GastoCambiarEstado(Int32.Parse(hdfIDCompra.Value), 9, IDUsuario());
					if (oBERetorno.Retorno == "1")
					{
						msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
						Listar();
					}
					else if (oBERetorno.Retorno == "-1")
					{
						RegistrarLogSistema("gvLista_RowCommand()", oBERetorno.ErrorMensaje, true);
					}
					break;
			}

		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			Listar();
		}

		private void ListarCompraDetalle()
		{
			ArrayList aListaDetalle = new ArrayList();
			aListaDetalle = (ArrayList)new BLGasto().GastoDetalleListar(Int32.Parse(hdfIDCompra.Value));
			LlenargvListaGasto(aListaDetalle);
			Session["aListaGasto"] = aListaDetalle;
			gvDetalleLista.SelectedIndex = -1;
			upRegistroCompra.Update();
		}

		protected void GastoSeleccionar()
		{

			BEGasto oBE = new BLGasto().GastoSeleccionar(Int32.Parse(hdfIDCompra.Value));
			lblNumeroCompra.Text = oBE.NumeroCompra;
			ddlRegTipoDocumento.SelectedValue = oBE.IDTipoDocumento.ToString();
			txtSerieDocumento.Text = oBE.SerieDocumento;
			txtNumeroDocumento.Text = oBE.NumeroDocumento;
			txtFechaEmision.Text = oBE.FechaCompra.ToShortDateString();
			txtFechaVencimiento.Text = oBE.FechaVencimiento.ToShortDateString();
			txtFechaRegistro.Text = oBE.FechaRegistro.ToShortDateString();
			ddlRegMoneda.SelectedValue = oBE.IDMoneda;
			ddlRegFormaPago.SelectedValue = oBE.IDFormaPago.ToString();
			hdfRegIDProveedor.Value = oBE.IDProveedor.ToString();
			ddlRegIDTipoDocumento.SelectedValue = oBE.ProveedorIDTipoDocumento.ToString();
			txtRegNumeroDocumento.Text = oBE.ProveedorNumeroDocumento;
			txtRegProveedor.Text = oBE.ProveedorRazonSocial;
			txtRegProveedorNumeroDocumento.Text = oBE.ProveedorNumeroDocumento;
			txtCuenta.Text = oBE.Cuenta;
			txtCuentaCaja.Text = oBE.CuentaCaja;
			txtGlosa.Text = oBE.Glosa;

			lblSubTotal.Text = oBE.SubTotal.ToString();
			hdfSubTotal.Value = oBE.SubTotal.ToString();
			lblTotalIgv.Text = oBE.TotalIGV.ToString();
			hdfTotalIGV.Value = oBE.TotalIGV.ToString();
			lblTotal.Text = oBE.TotalCompra.ToString();
			hdfTotalCompra.Value = oBE.TotalCompra.ToString();
		}

		#endregion

		#region RegistroCompra

		protected void rblTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
		{
			NumeroGastoeleccionar();
		}


		protected void lnkBusProveedor_Click(object sender, EventArgs e)
		{
			hdfRegIDProveedor.Value = "0";
			txtRegProveedor.Text = "";

			if (txtRegNumeroDocumento.Text.Trim().Length == 0 || txtRegNumeroDocumento.Text.Trim().Equals("*")
				|| ddlRegIDTipoDocumento.SelectedValue.Equals("2") || ddlRegIDTipoDocumento.SelectedValue.Equals("4")
				|| ddlRegIDTipoDocumento.SelectedValue.Equals("5") || ddlRegIDTipoDocumento.SelectedValue.Equals("6"))
			{
				registrarScript("funModalProveedorAbrir();");
			}
			else {
				StringBuilder pValidaciones = new StringBuilder();
				if (ddlRegIDTipoDocumento.SelectedValue == "1" && txtRegNumeroDocumento.Text.Trim().Length != 8) pValidaciones.Append("<div>El número dni no es válido.</div>");
				if (ddlRegIDTipoDocumento.SelectedValue == "3" && txtRegNumeroDocumento.Text.Trim().Length != 11) pValidaciones.Append("<div>El número ruc no es válido.</div>");

				if (pValidaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, pValidaciones.ToString());
					return;
				}

				BEProveedor oBE = new BLProveedor().ProveedorxNumeroDocumentoSeleccionar(txtRegNumeroDocumento.Text.Trim(), IDEmpresa());

				if (oBE.IDProveedor == 0)
				{
					//Buscamos en la reniec o sunat
					var response = ConsultarDocumentoSunatReniec(ddlRegIDTipoDocumento.SelectedValue, txtRegNumeroDocumento.Text.Trim());

					if (response.Estado.Equals("ERROR")) pValidaciones.Append("<div>El número documento no existe.</div>");

					if (pValidaciones.Length > 0)
					{
						msgbox(TipoMsgBox.warning, pValidaciones.ToString());
						return;
					}

					BEProveedor oBEProveedor = new BEProveedor();
					oBEProveedor.NumeroDocumento = response.NumeroDocumento;
					oBEProveedor.IDTipoDocumento = Int32.Parse(ddlRegIDTipoDocumento.SelectedValue);
					oBEProveedor.RazonSocial = response.NombreCompleto;
					oBEProveedor.Direccion = response.Direccion;
					oBEProveedor.IDEmpresa = IDEmpresa();
					BERetornoTran oBERetorno = new BERetornoTran();
					oBERetorno = new BLProveedor().ProveedorRapidoGuardar(oBEProveedor);

					BEProveedor oBEBus = new BLProveedor().ProveedorxNumeroDocumentoSeleccionar(txtRegNumeroDocumento.Text.Trim(), IDEmpresa());
					hdfRegIDProveedor.Value = oBEBus.IDProveedor.ToString();
					txtRegNumeroDocumento.Text = oBEBus.NumeroDocumento;
					txtRegProveedor.Text = oBEBus.RazonSocial.Trim();
					txtRegProveedorNumeroDocumento.Text = oBEBus.NumeroDocumento.Trim();
				}
				else {

					hdfRegIDProveedor.Value = oBE.IDProveedor.ToString();
					txtRegNumeroDocumento.Text = oBE.NumeroDocumento;
					txtRegProveedor.Text = oBE.RazonSocial.Trim();
					txtRegProveedorNumeroDocumento.Text = oBE.NumeroDocumento.Trim();
				}

			}

		}

		protected void lnkNuevaCompra_Click(object sender, EventArgs e)
		{
			LimpiarCompra();
		}

		protected void lnkGuardarCompra_Click(object sender, EventArgs e)
		{
			StringBuilder pValidaciones = new StringBuilder();
			if (ddlRegTipoDocumento.SelectedValue == "0" || ddlRegTipoDocumento.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Tipo Comprobante</div>");
			if (txtSerieDocumento.Text == "") pValidaciones.Append("<div>Ingrese Serie Documento</div>");
			if (txtSerieDocumento.Text.Length != 4) pValidaciones.Append("<div>Ingrese Serie Válida</div>");
			if (txtNumeroDocumento.Text == "") pValidaciones.Append("<div>Ingrese Numero Documento</div>");
			if (ddlRegFormaPago.SelectedValue == "0") pValidaciones.Append("<div>Seleccione Forma de Pago</div>");
			if (hdfRegIDProveedor.Value == "0") pValidaciones.Append("<div>Seleccione un Proveedor</div>");
			//if (txtCuenta.Text == "") pValidaciones.Append("<div>Ingrese Cuenta</div>");

			ArrayList aListaDetalleCompra = new ArrayList();
			if (Session["aListaGasto"] == null) //Trabajando en Memoria
			{
				Session["aListaGasto"] = aListaDetalleCompra;
			}
			else
			{
				aListaDetalleCompra = (ArrayList)(Session["aListaGasto"]);
			}
			if (aListaDetalleCompra.Count == 0) pValidaciones.Append("<div>El detalle no puede estar vacío!.</div>");

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			BLGasto oBLGasto = new BLGasto();
			BEGasto oBEGasto = new BEGasto();
			ArrayList ListaBEGastoDetalle = new ArrayList();

			oBEGasto.IDGasto = Int32.Parse(hdfIDCompra.Value);
			oBEGasto.IDSucursal = IDSucursal(); ;
			oBEGasto.IDProveedor = Int32.Parse(hdfRegIDProveedor.Value);
			oBEGasto.IDMoneda = ddlRegMoneda.SelectedValue;
			oBEGasto.IDTipoDocumento = Int32.Parse(ddlRegTipoDocumento.SelectedValue);
			oBEGasto.IDFormaPago = Int32.Parse(ddlRegFormaPago.SelectedValue);
			oBEGasto.TipoCompra = "G";
			oBEGasto.Cuenta = txtCuenta.Text;
			oBEGasto.CuentaCaja = txtCuentaCaja.Text;
			oBEGasto.Glosa = txtGlosa.Text;
			oBEGasto.Serie = txtSerieDocumento.Text;
			oBEGasto.NumeroDocumento = txtNumeroDocumento.Text;
			oBEGasto.FechaRegistro = DateTime.Parse(txtFechaRegistro.Text);
			oBEGasto.FechaCompra = DateTime.Parse(txtFechaEmision.Text);
			oBEGasto.FechaVencimiento = DateTime.Parse(txtFechaVencimiento.Text);
			oBEGasto.TotalCompra = Decimal.Parse(hdfTotalCompra.Value);
			oBEGasto.TotalIGV = Decimal.Parse(hdfTotalIGV.Value);
			oBEGasto.IDUsuario = IDUsuario();

			SetListaBEGastoDetalle(ref ListaBEGastoDetalle);

			BERetornoTran oBERetorno = new BERetornoTran();

			if (hdfIDCompra.Value == "0")
			{
				oBERetorno = oBLGasto.GuardarGasto(oBEGasto, ListaBEGastoDetalle);
			}
			else {
				oBERetorno = oBLGasto.GastoActualizar(oBEGasto, ListaBEGastoDetalle);
			}

			if (oBERetorno.Retorno == "1")
			{
				msgbox(TipoMsgBox.confirmation, "Orden de Compra Se grabo correctamente");
				Listar();
				Session["aListaGasto"] = null;
				LlenargvListaGasto((ArrayList)Session["aListaGasto"]);
				LimpiarCompra();
				CalcularTotales();
				NumeroGastoeleccionar();
				upRegistroCompra.Update();
			}
			else if (oBERetorno.Retorno == "-2")
			{
				msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
			}
			else if (oBERetorno.Retorno == "-1")
			{
				RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
			}
		}

		public void LlenargvListaGasto(ArrayList DT)
		{
			gvDetalleLista.DataSource = DT;
			gvDetalleLista.DataBind();
			upRegistroCompra.Update();
		}

		private void SetListaBEGastoDetalle(ref ArrayList ListaBEGastoDetalle)
		{
			ArrayList aListaBEGastoDetalle = new ArrayList();
			aListaBEGastoDetalle = (ArrayList)Session["aListaGasto"];
			if (aListaBEGastoDetalle != null)
			{
				Int32 p = 0;

				foreach (BEGastoDetalle oBE in aListaBEGastoDetalle)
				{
					p += 1;
					oBE.IDGastoDetalle = 0;
					oBE.IDGasto = 0;
					oBE.IDProducto = oBE.IDProducto;
					oBE.DetalleProducto = "";
					oBE.Item = p;
					oBE.IDUnidadMedida = oBE.IDUnidadMedida;
					oBE.Cantidad = oBE.Cantidad;
					oBE.PrecioUnitario = oBE.PrecioUnitario;
					oBE.AplicaIgv = oBE.AplicaIgv;
					oBE.IDUsuario = IDUsuario();
					ListaBEGastoDetalle.Add(oBE);

				}
			}
		}

		protected void gvDetalleLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			ArrayList aListaGasto = new ArrayList();
			aListaGasto = (ArrayList)(Session["aListaGasto"]);
			aListaGasto.RemoveAt(e.RowIndex);
			LlenargvListaGasto(aListaGasto);
			gvDetalleLista.SelectedIndex = -1;
			Session["aListaGasto"] = aListaGasto;
			CalcularTotales();
		}

		protected void gvDetalleLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			hdfIDCompraDetalle.Value = gvDetalleLista.SelectedIndex.ToString();

			GridViewRow row = gvDetalleLista.SelectedRow;
			hdfIDUnidadMedida.Value = "1";
			txtRegProductoDetalle.Text = ((Label)row.FindControl("lblProductoDetalle")).Text;
			txtRegCantidad.Text = ((Label)row.FindControl("lblCantidad")).Text;
			txtRegPrecioCompra.Text = ((Label)row.FindControl("lblPrecioUnitario")).Text;
			chkAplicaIgv.Checked = Boolean.Parse(((Label)row.FindControl("lblAplicaIgv")).Text);


			registrarScript("funModalProductoAbrir();");
			upRegistroProducto.Update();

		}

		private void LimpiarCompra()
		{
			NumeroGastoeleccionar();
			hdfIDCompra.Value = "0";
			hdfIDCompraDetalle.Value = "-1";
			Session["aListaGasto"] = null;
			LlenargvListaGasto((ArrayList)Session["aListaGasto"]);
			ddlRegTipoDocumento.SelectedIndex = -1;
			txtSerieDocumento.Text = "";
			txtNumeroDocumento.Text = "";
			ddlRegMoneda.SelectedIndex = -1;
			hdfRegIDProveedor.Value = "0";
			txtRegNumeroDocumento.Text = "";
			txtRegProveedor.Text = "";
			txtRegProveedorNumeroDocumento.Text = "";
			ddlRegFormaPago.SelectedIndex = -1;
			txtCuenta.Text = "";
			txtCuentaCaja.Text = "";
			txtGlosa.Text = "";
			lblSubTotal.Text = "0.00";
			lblTotalIgv.Text = "0.00";
			lblTotal.Text = "0.00";
			hdfSubTotal.Value = "0";
			hdfTotalIGV.Value = "0";
			hdfTotalCompra.Value = "0";
			upRegistroCompra.Update();
		}

		#endregion

		#region AgregarProducto
		protected void lnkNuevoItem_Click(object sender, EventArgs e)
		{
			LimpiarProducto();
			registrarScript("funModalProductoAbrir();");
		}

		protected void btnCancelarItem_Click(object sender, EventArgs e)
		{
			registrarScript("funModalProductoCerrar();");
		}

		protected void btnAgregarItem_Click(object sender, EventArgs e)
		{

			StringBuilder pValidaciones = new StringBuilder();
			if (txtRegCantidad.Text == "") pValidaciones.Append("<div>Ingrese una Cantidad</div>");
			if (txtRegCantidad.Text.Length > 0)
			{
				if (Int32.Parse(txtRegCantidad.Text) <= 0)
				{
					pValidaciones.Append("<div>La Cantidad no puede ser negativo o cero</div>");
				}
			}
			if (txtRegPrecioCompra.Text == "") pValidaciones.Append("<div>Ingrese Precio de Compra</div>");
			if (txtRegPrecioCompra.Text.Length > 0)
			{
				if (Decimal.Parse(txtRegPrecioCompra.Text) <= 0)
				{
					pValidaciones.Append("<div>El precio de Compra no puede ser negativo o cero</div>");
				}
			}

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			ArrayList aListaGasto = new ArrayList();

			aListaGasto = (ArrayList)(Session["aListaGasto"]);
			Int32 pID = Int32.Parse(hdfIDCompraDetalle.Value);
			BEGastoDetalle oBEVentaDet = new BEGastoDetalle();

			oBEVentaDet.ProductoDetalle = txtRegProductoDetalle.Text.Trim();
			oBEVentaDet.Stock = 1;
			oBEVentaDet.AplicaIgv = chkAplicaIgv.Checked;
			oBEVentaDet.IDUnidadMedida = 1;

			if (chkAplicaIgv.Checked)
			{
				oBEVentaDet.Total = Math.Round((Int32.Parse(txtRegCantidad.Text.Trim()) * Decimal.Parse(txtRegPrecioCompra.Text)), 2, MidpointRounding.AwayFromZero);
				oBEVentaDet.SubTotal = Math.Round(((Int32.Parse(txtRegCantidad.Text.Trim()) * Decimal.Parse(txtRegPrecioCompra.Text)) / Decimal.Parse("1.18")), 2, MidpointRounding.AwayFromZero);
				oBEVentaDet.Igv = oBEVentaDet.Total - oBEVentaDet.SubTotal;
			}
			else {
				oBEVentaDet.Total = Math.Round((Int32.Parse(txtRegCantidad.Text.Trim()) * Decimal.Parse(txtRegPrecioCompra.Text)), 2, MidpointRounding.AwayFromZero);
				oBEVentaDet.SubTotal = Math.Round((Int32.Parse(txtRegCantidad.Text.Trim()) * Decimal.Parse(txtRegPrecioCompra.Text)), 2, MidpointRounding.AwayFromZero);
				oBEVentaDet.Igv = 0;
			}

			oBEVentaDet.PrecioUnitario = Decimal.Parse(txtRegPrecioCompra.Text);
			oBEVentaDet.Cantidad = Int32.Parse(txtRegCantidad.Text.Trim());

			if (pID == -1)
			{
				if (ContarIngresado(aListaGasto, txtRegProductoDetalle.Text.Trim()) > 0)
				{
					msgbox(TipoMsgBox.information, "Ya ingresó ese producto.");
					return;
				}
			}

			if (aListaGasto == null)
			{
				aListaGasto = new ArrayList();
			}
			if (hdfIDCompraDetalle.Value == "-1")
			{

				aListaGasto.Add(oBEVentaDet);
			}
			else
			{
				aListaGasto[pID] = oBEVentaDet;
			}


			LlenargvListaGasto(aListaGasto);
			Session["aListaGasto"] = aListaGasto;
			CalcularTotales();
			gvDetalleLista.SelectedIndex = -1;
			msgbox(TipoMsgBox.confirmation, "Se agregó satisfactoriamente.");
			//registrarScript("funModalProductoCerrar();");

		}

		private void LimpiarProducto()
		{
			chkAplicaIgv.Checked = true;
			hdfIDCompraDetalle.Value = "-1";
			txtRegProductoDetalle.Text = "";
			txtRegCantidad.Text = "1";
			txtRegPrecioCompra.Text = "";
			hdfIDUnidadMedida.Value = "0";
			upRegistroProducto.Update();
		}

		#endregion

		#region Proveedor 
		private void ListarProveedor()
		{
			BLProveedor oBLProveedor = new BLProveedor();
			gvListadoProveedor.DataSource = oBLProveedor.ProveedorFiltroListar(IDEmpresa(), txtFiltro.Text.Trim());
			gvListadoProveedor.DataBind();
		}

		protected void lnkBuscarProveedor_Click(object sender, EventArgs e)
		{
			ListarProveedor();
		}

		protected void gvListadoProveedor_SelectedIndexChanged(object sender, EventArgs e)
		{
			GridViewRow row = gvListadoProveedor.SelectedRow;
			hdfRegIDProveedor.Value = ((HiddenField)row.FindControl("hdfIDProveedor")).Value;
			ddlRegIDTipoDocumento.SelectedValue = ((HiddenField)row.FindControl("hdfIDTipoDocumento")).Value;
			txtRegNumeroDocumento.Text = ((Label)row.FindControl("lblNumeroDocumento")).Text;
			txtRegProveedor.Text = ((Label)row.FindControl("lblRazonSocial")).Text;
			txtRegProveedorNumeroDocumento.Text = ((Label)row.FindControl("lblNumeroDocumento")).Text;
			upRegistroCompra.Update();
			registrarScript("funModalProveedorCerrar();");
		}

		#endregion

		#region Funciones

		public Double ContarIngresado(ArrayList aListaGasto, String pProducto)
		{
			Int32 p = 0;
			if (aListaGasto == null)
			{
				p = 0;
			}
			else
			{
				//Int32 i = 0;
				foreach (BEGastoDetalle oBE in aListaGasto)
				{
					if (oBE.ProductoDetalle == pProducto)
					{
						p += 1;
						break;
					}

				}
			}
			return p;
		}

		protected void CalcularTotales()
		{
			ArrayList ImporteTotales = new ArrayList();
			Decimal OperacionSubTotal = Decimal.Parse("0.00");
			Decimal pIgv = Decimal.Parse("0.00");
			Decimal OperacionTotal = Decimal.Parse("0.00");


			foreach (GridViewRow fila in gvDetalleLista.Rows)
			{
				String hfSubTotal = ((HiddenField)fila.Cells[0].FindControl("hfSubTotal")).Value;
				String lblIgv = ((Label)fila.Cells[0].FindControl("lblIgv")).Text;
				String hfTotal = ((HiddenField)fila.Cells[0].FindControl("hfTotal")).Value;
				OperacionTotal += Math.Round(Decimal.Parse(hfTotal), 2, MidpointRounding.AwayFromZero);
				OperacionSubTotal += Math.Round(Decimal.Parse(hfSubTotal), 2, MidpointRounding.AwayFromZero);
				pIgv += Math.Round(Decimal.Parse(lblIgv), 2, MidpointRounding.AwayFromZero);


			}

			lblTotal.Text = Math.Round(OperacionTotal, 2, MidpointRounding.AwayFromZero).ToString();
			hdfTotalCompra.Value = Math.Round(OperacionTotal, 2, MidpointRounding.AwayFromZero).ToString();

			lblTotalIgv.Text = Math.Round(pIgv, 2, MidpointRounding.AwayFromZero).ToString();
			hdfTotalIGV.Value = Math.Round(pIgv, 2, MidpointRounding.AwayFromZero).ToString();

			lblSubTotal.Text = Math.Round((OperacionSubTotal), 2, MidpointRounding.AwayFromZero).ToString();
			hdfSubTotal.Value = Math.Round((OperacionSubTotal), 2, MidpointRounding.AwayFromZero).ToString();

		}

		#endregion

		protected void txtSerieDocumento_TextChanged(object sender, EventArgs e)
		{
			txtSerieDocumento.Text = txtSerieDocumento.Text.PadLeft(4, '0');
		}

		protected void txtNumeroDocumento_TextChanged(object sender, EventArgs e)
		{
			txtNumeroDocumento.Text = txtNumeroDocumento.Text.PadLeft(8, '0');
		}
	}
}