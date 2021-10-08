
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General; 
using Farmacia.App_Class.BL.Facturacion;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
	public partial class Producto : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage(); 
				CargarDDL(ddlIDUnidadMedidaCompra, new BLUnidadMedida().UnidadMedidaListar("",IDEmpresa()), "IDUnidadMedida", "Nombre", true, Constantes.SELECCIONAR);
				CargarDDL(ddlIDUnidadMedidaVenta, new BLUnidadMedida().UnidadMedidaListar("",IDEmpresa()), "IDUnidadMedida", "Nombre", true, Constantes.SELECCIONAR); 
				CargarDDL(ddlIDMarca, new BLMarca().Listar(IDEmpresa()), "IDMarca", "Nombre", true, Constantes.SELECCIONAR);
				CargarDDL(ddlIDCategoria, new BLCategoria().Listar(IDEmpresa()), "IDCategoria", "Nombre", true, Constantes.SELECCIONAR);
				CargarDDL(ddlIDTipoImpuesto, new BLTipoAfectacionIgv().TipoAfectacionIgvListar(), "IDTipoAfectacionIgv", "Nombre", true, Constantes.SELECCIONAR);
				CargarDDL(ddlIDTipoPrecio, new BLTipoPrecio().TipoPrecioListar(), "IDTipoPrecio", "Nombre", true, Constantes.SELECCIONAR);
				ddlIDTipoPrecio.SelectedValue = "1";
				ddlIDTipoImpuesto.SelectedValue = "1";
				CargarDDL(ddlPPIDProveedor, new BLProveedor().ProveedorFiltroListar(IDEmpresa(),""), "IDProveedor", "RazonSocial", true, Constantes.SELECCIONAR);
			     CargarDDL(ddlPCIDProductoComp, new BLProducto().ProductoFiltroListar("", IDSucursal()), "IDProducto", "Nombre", true, Constantes.SELECCIONAR);
				ListarProducto();
			}
		}

		protected void txtPrecioCosto_TextChanged(object sender, EventArgs e)
		{
			CalcularPrecioCompra();
		}

		protected void CalcularPrecioCompra() {
			Decimal Base = Decimal.Parse(txtPrecioCosto.Text) / Decimal.Parse("1.18");
			Decimal IGV = Base * Decimal.Parse("0.18");
			txtPrecioCostoTotalSinIgv.Text = Base.ToString("N3");
			txtPrecioCostoUnidadSinIgv.Text = (Base/Int32.Parse(txtFactor.Text)).ToString("N3");
			txtPrecioCostoUnidadConIgv.Text = (Decimal.Parse(txtPrecioCosto.Text) / Int32.Parse(txtFactor.Text)).ToString("N3");
			CalcularMargenUtilidad();

		}

		protected void txtMargenUtilidad_TextChanged(object sender, EventArgs e)
		{
			CalcularPrecioVenta();
		}

		protected void txtPrecioVenta_TextChanged(object sender, EventArgs e)
		{
			CalcularMargenUtilidad();
		}

		protected void CalcularPrecioVenta()
		{
			Decimal PrecioVenta = Decimal.Parse(txtPrecioCostoUnidadConIgv.Text) / (1 - (Decimal.Parse(txtMargenUtilidad.Text) / 100));
			txtPrecioVenta.Text = PrecioVenta.ToString("N");
		}

		protected void CalcularMargenUtilidad()
		{
			if (Decimal.Parse(txtPrecioCostoUnidadConIgv.Text) <= 0) {
				txtPrecioCostoUnidadConIgv.Text = "1";
			}

			Decimal MargenUtilidad = ((Decimal.Parse(txtPrecioVenta.Text) - Decimal.Parse(txtPrecioCostoUnidadConIgv.Text))/ Decimal.Parse(txtPrecioCostoUnidadConIgv.Text)) * 100;
			txtMargenUtilidad.Text = MargenUtilidad.ToString("N");
		}

		protected void ddlIDUnidadMedidaCompra_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlIDUnidadMedidaCompra.SelectedValue == ddlIDUnidadMedidaVenta.SelectedValue)
			{
				txtFactor.Text = "1";
				txtFactor.Enabled = false;
			}
			else {
				txtFactor.Text = "0";
				txtFactor.Enabled = true;
			}
		}

		protected void ddlIDUnidadMedidaVenta_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlIDUnidadMedidaCompra.SelectedValue == ddlIDUnidadMedidaVenta.SelectedValue)
			{
				txtFactor.Text = "1";
				txtFactor.Enabled = false;
			}
			else {
				txtFactor.Text = "0";
				txtFactor.Enabled = true;
			}
		}


		protected void txtFactor_TextChanged(object sender, EventArgs e)
		{
			CalcularPrecioCompra();
		}

		#endregion

		#region Lista

		private void ListarProducto()
		{
			BLProducto oBLProducto = new BLProducto();
			gvLista.DataSource = oBLProducto.ProductoFiltroListar(txtBuscar.Text.Trim(), IDSucursal());
			gvLista.DataBind();
			upLista.Update();
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			ListarProducto();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarProducto();
		}

		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			hdfIDProducto.Value = gvLista.SelectedDataKey["IDProducto"].ToString();
			ProductoSeleccionar();
		}

		protected void ProductoSeleccionar()
		{

			BEProducto oBE = new BLProducto().ProductoSeleccionar(Int32.Parse(hdfIDProducto.Value), IDSucursal());
			txtCodigoBarra.Text = oBE.CodigoBarra;
			txtCodigoAlterna.Text = oBE.CodigoAlterna;
			txtNombre.Text = oBE.Nombre; 
			ddlIDCategoria.SelectedValue = oBE.IDCategoria.ToString();
			ddlIDMarca.SelectedValue = oBE.IDMarca.ToString();
			ddlIDUnidadMedidaCompra.SelectedValue = oBE.IDUnidadMedidaCompra.ToString();
			ddlIDUnidadMedidaVenta.SelectedValue = oBE.IDUnidadMedidaVenta.ToString();
			txtFactor.Text = oBE.Factor.ToString();
			txtStockMinimo.Text = oBE.StockMinimo.ToString();
			txtStock.Text = oBE.Stock.ToString();
			txtLocalizacion.Text = oBE.Localizacion;
			ddlControlaStock.SelectedValue = oBE.ControlStock;
			txtPrecioCosto.Text = oBE.PrecioCosto.ToString("N");
			txtPrecioCostoTotalSinIgv.Text = oBE.PrecioCostoTotalSinIgv.ToString("N3");
			txtPrecioCostoUnidadSinIgv.Text = oBE.PrecioCostoUnidadSinIgv.ToString("N3");
			txtPrecioCostoUnidadConIgv.Text = oBE.PrecioCostoUnidadConIgv.ToString("N3");

			txtMargenUtilidad.Text = oBE.MargenUtilidad.ToString("N");
			txtPrecioVenta.Text = oBE.PrecioVenta.ToString("N");
			txtMayoreoUnidad.Text = oBE.MayoreoUnidad.ToString();

			txtDescripcion.Text = oBE.Descripcion;
			txtPrincipioActivo.Text = oBE.PrincipioActivo;
			chkControlaLote.Checked = oBE.ControlaLote;
			chkVentaConReceta.Checked = oBE.VentaConReceta;
			ddlIDTipoImpuesto.SelectedValue = oBE.IDTipoAfectacionIgv.ToString();
			ddlIDTipoPrecio.SelectedValue = oBE.IDTipoPrecio.ToString();
			pnProductoDetalle.Visible = true;
			txtNombre.Focus();
			PrecioProveedorListar();
			ProductoCompatibleListar();
			upFormulario.Update();
			upProdutoAdicional.Update();
			upProductoDetalle.Update();
			registrarScript("ActivarTabxId('tab2');");
		}

		#endregion

		#region Registrar

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();
			txtNombre.Focus();
		}

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			StringBuilder validacion = new StringBuilder();
			if (txtCodigoBarra.Text.Trim().Length == 0) validacion.Append("<div>Ingrese Código de Barra</div>");
			if (txtNombre.Text.Trim().Length == 0) validacion.Append("<div>Ingrese Nombre Comercial</div>");
			 
			if (ddlIDCategoria.SelectedValue == "0") validacion.Append("<div>Seleccione Categoria</div>");
			if (ddlIDMarca.SelectedValue == "0") validacion.Append("<div>Seleccione Presentación</div>");

			if (ddlIDUnidadMedidaCompra.SelectedValue == "0") validacion.Append("<div>Seleccione Unidad de Compra</div>"); 
			if (ddlIDUnidadMedidaVenta.SelectedValue == "0") validacion.Append("<div>Seleccione Unidad de Venta</div>");
			if (txtFactor.Text.Trim().Length == 0) validacion.Append("<div>Ingrese Factor</div>");
			if (txtStockMinimo.Text.Trim().Length == 0) validacion.Append("<div>Ingrese Stock Mínimo</div>");
			 
			if (validacion.Length > 0)
			{
				msgbox(TipoMsgBox.warning, validacion.ToString());
				return;
			}

			BEProducto oBE = new BEProducto();
			BLProducto oBL = new BLProducto();
			oBE.IDProducto = Int32.Parse(hdfIDProducto.Value);
			oBE.IDLinea = 0;
			oBE.IDMarca = Int32.Parse(ddlIDMarca.SelectedValue);
			oBE.IDCategoria = Int32.Parse(ddlIDCategoria.SelectedValue);
			oBE.IDTipoProducto = 0;
			oBE.IDUnidadMedidaCompra = Int32.Parse(ddlIDUnidadMedidaCompra.SelectedValue);
			oBE.IDUnidadMedidaVenta = Int32.Parse(ddlIDUnidadMedidaVenta.SelectedValue);
			oBE.CodigoBarra = txtCodigoBarra.Text.Trim();
			oBE.CodigoAlterna = txtCodigoAlterna.Text.Trim();
			oBE.Nombre = txtNombre.Text.Trim();
			oBE.NombreCorto = "";
			oBE.Localizacion = txtLocalizacion.Text.Trim();
			oBE.Factor = Int32.Parse(txtFactor.Text.Trim());
			oBE.Estado = true;
			oBE.ControlStock = ddlControlaStock.SelectedValue;
			oBE.Peso = Decimal.Parse("0");
			oBE.StockMinimo = Decimal.Parse(txtStockMinimo.Text);
			oBE.PrecioCosto = Decimal.Parse(txtPrecioCosto.Text);
			oBE.PrecioCostoTotalSinIgv = Decimal.Parse(txtPrecioCostoTotalSinIgv.Text);
			oBE.PrecioCostoUnidadSinIgv = Decimal.Parse(txtPrecioCostoUnidadSinIgv.Text);
			oBE.PrecioCostoUnidadConIgv = Decimal.Parse(txtPrecioCostoUnidadConIgv.Text);

			
			oBE.MargenUtilidad = Decimal.Parse(txtMargenUtilidad.Text);
			oBE.PrecioVenta = Decimal.Parse(txtPrecioVenta.Text);
			oBE.MayoreoUnidad = Int32.Parse(txtMayoreoUnidad.Text);
			oBE.IDSucursal = IDSucursal();
			oBE.IDUsuario = IDUsuario();

			oBE.FechaDUA = DateTime.Today;

			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = oBL.ProductoGuardar(oBE);

			if (oBERetorno.Retorno == "1")
			{
				hdfIDProducto.Value = oBERetorno.Retorno2;
				ProductoSeleccionar();
				ListarProducto();
				msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
			}
			else {

				if (oBERetorno.Retorno != "-1")
				{
					msgbox(TipoMsgBox.warning, "Sistema", "El código ingresado ya se encuentra registrado");
				}
				else {
					RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
				}
			}
		}

		private void LimpiarFormulario()
		{
			hdfIDProducto.Value = "0";
			txtCodigoBarra.Text = String.Empty;
			txtNombre.Text = String.Empty;
			txtStock.Text = "0";
			txtStockMinimo.Text = "0";
			txtPrecioVenta.Text = "0";
			ddlIDTipoImpuesto.SelectedValue = "1";
			ddlIDTipoPrecio.SelectedValue = "1";
			pnProductoDetalle.Visible = false;

			txtCodigoBarra.Text = String.Empty;
			txtCodigoAlterna.Text = String.Empty;
			txtNombre.Text = String.Empty;

			ddlControlaStock.SelectedValue = "SK";
			ddlIDCategoria.SelectedIndex = -1;
			ddlIDMarca.SelectedIndex = -1;
			ddlIDUnidadMedidaCompra.SelectedIndex = -1;
			ddlIDUnidadMedidaVenta.SelectedIndex = -1;
			txtFactor.Text = "1";
			txtStockMinimo.Text = "0";
			txtStock.Text = "0";
			txtLocalizacion.Text = String.Empty;

			txtPrecioCosto.Text = "0.00";
			txtPrecioCostoTotalSinIgv.Text = "0.00";
			txtMargenUtilidad.Text = "0.00";
			txtPrecioVenta.Text = "0.00";
			txtMayoreoUnidad.Text = "0"; 
			txtPrecioCostoUnidadSinIgv.Text = "0.00";
			txtPrecioCostoUnidadConIgv.Text = "0.00";

			txtDescripcion.Text = String.Empty;
			txtPrincipioActivo.Text = String.Empty;
			chkControlaLote.Checked = false;
			chkVentaConReceta.Checked = false;
			//ddlIDTipoImpuesto.SelectedIndex = -1;
			ddlIDTipoPrecio.SelectedIndex = -1;
			pnProductoDetalle.Visible = false;
			upFormulario.Update();
			upProductoDetalle.Update();
		}

		protected void btnGuardarDatosAdicional_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (hdfIDProducto.Value == "0") validacion.Append("<div>Seleccione Producto</div>");
				if (ddlIDTipoImpuesto.SelectedValue == "0") validacion.Append("<div>Seleccione Tipo Impuesto</div>");
				if (ddlIDTipoPrecio.SelectedValue == "0") validacion.Append("<div>Seleccione Tipo Precio</div>");

				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEProducto oBE = new BEProducto();
				oBE.IDProducto = Int32.Parse(hdfIDProducto.Value);
				oBE.Descripcion = txtDescripcion.Text;
				oBE.PrincipioActivo = txtPrincipioActivo.Text;
				oBE.ControlaLote = chkControlaLote.Checked;
				oBE.VentaConReceta = chkVentaConReceta.Checked;
				oBE.IDTipoAfectacionIgv = Int32.Parse(ddlIDTipoImpuesto.SelectedValue);
				oBE.IDTipoPrecio = Int32.Parse(ddlIDTipoPrecio.SelectedValue);
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLProducto().ProductoDatosAdicionalGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				}
				else {

					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", "El código ingresado ya se encuentra registrado");
					}
					else {
						RegistrarLogSistema("btnGuardarDatosAdicional_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarDatosAdicional_Click()", ex.Message, true);
			}
		}

		#endregion

		#region PrecioProveedor

		private void PrecioProveedorListar()
		{
			gvPrecioProveedorListar.DataSource = new BLPrecioProveedor().PrecioProveedorListar(Int32.Parse(hdfIDProducto.Value));
			gvPrecioProveedorListar.DataBind();
			upPrecioProveedorListar.Update();
		}

		protected void gvPrecioProveedorListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvPrecioProveedorListar.PageIndex = e.NewPageIndex;
			gvPrecioProveedorListar.SelectedIndex = -1;
			PrecioProveedorListar();
		}

		protected void btnGuardarPrecioProveedor_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (hdfIDProducto.Value == "0") validacion.Append("<div>Seleccione Producto</div>");
				if (ddlPPIDProveedor.SelectedValue == "0") validacion.Append("<div>Seleccione Proveedor</div>");

				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEPrecioProveedor oBE = new BEPrecioProveedor();
				oBE.IDPrecioProveedor = Int32.Parse(hdfIDPrecioProveedor.Value);
				oBE.IDProducto = Int32.Parse(hdfIDProducto.Value);
				oBE.IDProveedor = Int32.Parse(ddlPPIDProveedor.SelectedValue);
				oBE.FechaUltimoPrecio = DateTime.Today;
				oBE.UltimoPrecioCompra = Decimal.Parse(txtPPUltimoPrecioCompra.Text);
				oBE.IDUsuario = IDUsuario();

				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLPrecioProveedor().PrecioProveedorGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{ 
					ddlPPIDProveedor.SelectedIndex = -1;
					txtPPUltimoPrecioCompra.Text = "0.00";
					PrecioProveedorListar();
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				}
				else {

					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarPrecioProveedor_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarPrecioProveedor_Click()", ex.Message, true);
			}
		}

		protected void gvPrecioProveedorListar_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDPrecioProveedor.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLPrecioProveedor().PrecioProveedorEliminar(Int32.Parse(hdfIDPrecioProveedor.Value));
						if (oBERetorno.Retorno == "1")
						{
							PrecioProveedorListar();
							msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
						}
						else
						{
							if (oBERetorno.Retorno != "-1")
							{
								msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
							}
							else {
								RegistrarLogSistema("gvPrecioProveedorListar_RowCommand()", oBERetorno.ErrorMensaje, true);
							} 
						}
						break;
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvPrecioProveedorListar_RowCommand()", ex.Message, true);
			}
		}


		#endregion

		#region ProductoCompatible

		private void ProductoCompatibleListar()
		{
			gvProductoCompatibleListar.DataSource = new BLProductoCompatible().ProductoCompatibleListar(Int32.Parse(hdfIDProducto.Value), IDSucursal());
			gvProductoCompatibleListar.DataBind();
			upProductoCompatibleListar.Update();
		}

		protected void gvProductoCompatibleListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvProductoCompatibleListar.PageIndex = e.NewPageIndex;
			gvProductoCompatibleListar.SelectedIndex = -1;
			ProductoCompatibleListar();
		}

		protected void gvProductoCompatibleListar_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDProductoCompatible.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLProductoCompatible().ProductoCompatibleEliminar(Int32.Parse(hdfIDProductoCompatible.Value));
						if (oBERetorno.Retorno == "1")
						{
							ProductoCompatibleListar();
							msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
						}
						else
						{
							if (oBERetorno.Retorno != "-1")
							{
								msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
							}
							else {
								RegistrarLogSistema("gvProductoCompatibleListar_RowCommand()", oBERetorno.ErrorMensaje, true);
							}
						}
						break;
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvProductoCompatibleListar_RowCommand()", ex.Message, true);
			}
		}

		protected void btnGuardarProductoCompatible_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (hdfIDProducto.Value == "0") validacion.Append("<div>Seleccione Producto</div>");
				if (ddlPCIDProductoComp.SelectedValue == "0") validacion.Append("<div>Seleccione Producto Compatible</div>");

				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEProductoCompatible oBE = new BEProductoCompatible();
				oBE.IDProductoCompatible = 0;
				oBE.IDProducto = Int32.Parse(hdfIDProducto.Value);
				oBE.IDProductoComp = Int32.Parse(ddlPCIDProductoComp.SelectedValue); 
				oBE.IDUsuario = IDUsuario();

				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLProductoCompatible().ProductoCompatibleGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					ddlPCIDProductoComp.SelectedIndex = -1;
					ProductoCompatibleListar();
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				}
				else {

					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarProductoCompatible_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarProductoCompatible_Click()", ex.Message, true);
			}
		}

		#endregion


		private void ProductoPrecioListar()
		{
			gvProductoPrecioListar.DataSource = new BLProducto().ProductoPrecioListar(Int32.Parse(hdfIDProducto.Value));
			gvProductoPrecioListar.DataBind();
			upProductoPrecioListar.Update();
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDProducto.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLProducto().ProductoEliminar(Int32.Parse(hdfIDProducto.Value));
						if (oBERetorno.Retorno == "1")
						{
							ListarProducto();
							msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
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

					case "VerPrecio":
						registrarScript("AbrirModal('ModalProductoPrecio');");
						ProductoPrecioListar();
						break;
						
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLista_RowCommand()", ex.Message, true);
			}
		}

		protected void btnExtraerCatalogo_Click(object sender, EventArgs e)
		{
			txtFiltroCatalogoProducto.Text = String.Empty;
			ProductoTempListar();
			registrarScript("AbrirModal('ModalCatalogoProducto');");
		}

		private void ProductoTempListar()
		{
			gvCatalogoProductoListar.DataSource = new BLProducto().ProductoTempListar("");
			gvCatalogoProductoListar.DataBind();
			upCatalogoProductoListar.Update();
		}
		 
		protected void btnGuardarProductoCatalogo_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder pRetornoError = new StringBuilder();

				foreach (GridViewRow row in gvCatalogoProductoListar.Rows)
				{
					if (row.RowType == DataControlRowType.DataRow)
					{
						CheckBox chkRow = (CheckBox)row.Cells[0].FindControl("chkCATSeleccionar");
						if (chkRow.Checked)
						{
							BEProducto pBE = new BEProducto();
							pBE.IDProductoTemp = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hfIDProductoTemp")).Value);
							pBE.Codigo = ((HiddenField)row.Cells[0].FindControl("hfCodigo")).Value.Trim();
							pBE.NombreCompleto = ((HiddenField)row.Cells[0].FindControl("hfNombreCompleto")).Value.Trim();
							pBE.Laboratorio = ((HiddenField)row.Cells[0].FindControl("hfLaboratorio")).Value.Trim();
							pBE.IDUsuario = IDUsuario();
							BERetornoTran oBERetornoInsertar = new BLProducto().ProductoTempGuardar(pBE);
							if (oBERetornoInsertar.Retorno != "1")
							{
								pRetornoError.Append("<div>El producto " + pBE.NombreCompleto + "  " + oBERetornoInsertar.ErrorMensaje + ".</div>");
							}
						}
					}
				}

				if (pRetornoError.Length > 0)
				{
					msgbox(TipoMsgBox.warning, "<div style='text-align: left;'><div><b>Se han generado estas observaciones:</b></div>" + pRetornoError.ToString() + "</div>"); 
				}
				else
				{
					txtFiltroCatalogoProducto.Text = String.Empty;
					ProductoTempListar();
					ListarProducto();
					msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarProductoCatalogo_Click()", ex.Message, true);
			}
		}

		protected void btnActualizarProductoPrecio_Click(object sender, EventArgs e)
		{ 
			StringBuilder pRetornoError = new StringBuilder();

			foreach (GridViewRow row in gvProductoPrecioListar.Rows)
			{

				Int32 IDProductoPrecio = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hdfIdProductoPrecio")).Value);
				Decimal PrecioVenta = Decimal.Parse(((TextBox)row.Cells[1].FindControl("txtPrecioVenta")).Text);
			    BERetornoTran oBERetorno = new BLProducto().ProductoPrecioActualizar(IDProductoPrecio, PrecioVenta, IDUsuario());
				if (oBERetorno.Retorno != "1")
				{
					pRetornoError.Append("<div>El productoPrecio " + IDProductoPrecio + "  " + oBERetorno.ErrorMensaje + ".</div>");
				}
			}
			if (pRetornoError.Length > 0)
			{
				msgbox(TipoMsgBox.warning, "<div style='text-align: left;'><div><b>Se han generado estas observaciones:</b></div>" + pRetornoError.ToString() + "</div>");
			}
			else
			{ 
				ListarProducto();
				msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
			}
		}
	}
}