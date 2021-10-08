using Farmacia.App_Class;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Almacen
{
	public partial class TransferenciaDirecta : PageBase
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

			ArrayList aListaAlmacen = new ArrayList();
			if (Session["aListaAlmacen"] == null) //Trabajando en Memoria
			{
				Session["aListaAlmacen"] = aListaAlmacen;
			}
			else
			{
				aListaAlmacen = (ArrayList)(Session["aListaAlmacen"]);
			}
			if (Session["token"] == null) //Trabajando en Memoria
			{
				GenerarToken();
			}
			LlenargvListaAlmacen(aListaAlmacen);            
            CargarDDL(ddlIDAlmacenOrigen, new BLAlmacen().AlmacenListar(IDSucursal()), "IDAlmacen", "Nombre", true);
            CargarDDL(ddlIDAlmacenDestino, new BLAlmacen().AlmacenListar(0), "IDAlmacen", "Nombre", true);
			CargarDDL(ddlIDTransaccion, new BLTransaccion().TransaccionListar("S"), "IDTransaccion", "Nombre", true);
			CargarDDL(ddlIDProducto, new BLProducto().ListarPorSucursal(IDSucursal()), "IDProducto", "Nombre", true, Constantes.SELECCIONAR);
			ddlIDAlmacenOrigen.SelectedValue = Convert.ToString(IDSucursal());
			ddlIDTransaccion.SelectedValue = "32";
			hdfIDSucursal.Value = IDSucursal().ToString();
		}

		protected void GenerarToken()
		{
			Session["token"] = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
		}

		#endregion

		#region Listar

		public void LlenargvListaAlmacen(ArrayList DT)
		{
			gvDetalleLista.DataSource = DT;
			gvDetalleLista.DataBind();
			upRegistro.Update();
		}


		protected void gvDetalleLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			ArrayList aListaAlmacen = new ArrayList();
			aListaAlmacen = (ArrayList)(Session["aListaAlmacen"]);
			aListaAlmacen.RemoveAt(e.RowIndex);
			LlenargvListaAlmacen(aListaAlmacen);
			gvDetalleLista.SelectedIndex = -1;
			Session["aListaAlmacen"] = aListaAlmacen;
			//CalcularTotales(); 
		}

		#endregion

		#region Registro

		protected void lnkNuevoItem_Click(object sender, EventArgs e)
		{
			LimpiarProducto();
			registrarScript("AbrirModal('DatosProducto');");
		}

		protected void ddlIDProducto_SelectedIndexChanged(object sender, EventArgs e)
		{
			BEProducto oBE = new BLProducto().ProductoSeleccionar(Int32.Parse(ddlIDProducto.SelectedValue), IDSucursal());
			hdfIDProducto.Value = oBE.IDProducto.ToString();
			hdfIDUnidadMedida.Value = oBE.IDUnidadMedidaVenta.ToString();
			txtUnidadMedida.Text = oBE.UnidadMedidaVenta;
			txtStockActual.Text = oBE.Stock.ToString();
			txtCantidad.Text = "1";
			txtRegCantidadLote.Text = "0";
			if (oBE.ControlaLote)
			{
				DivCantidadLote.Visible = true;
				DivCantidad.Visible = false;
			}
			else
			{
				DivCantidadLote.Visible = false;
				DivCantidad.Visible = true;
			}			
			upRegistroProducto.Update();
		}

		protected void LimpiarProducto()
		{
			hdfIDProducto.Value = "0";
			hdfIDUnidadMedida.Value = "0";
			ddlIDProducto.SelectedValue = "0";
			txtStockActual.Text = "0.00";
			txtUnidadMedida.Text = String.Empty;
			txtCantidad.Text = "0.00";
			upRegistroProducto.Update();
		}

		protected void lnkCancelarProducto_Click(object sender, EventArgs e)
		{
			registrarScript("CerrarModal('DatosProducto');");
		}

		protected void lnkNuevoProducto_Click(object sender, EventArgs e)
		{
			LimpiarProducto();
		}

		protected void lnkAgregarProducto_Click(object sender, EventArgs e)
		{
			StringBuilder pValidaciones = new StringBuilder();
			if (hdfIDProducto.Value == "0") pValidaciones.Append("<div>Seleccione un Producto</div>");
			if (txtCantidad.Text.Length == 0) pValidaciones.Append("<div>Ingrese una Cantidad</div>");
			if (txtCantidad.Text.Length > 0) if (!esDecimal(txtCantidad.Text)) pValidaciones.Append("<div>Ingrese una Cantidad válida</div>");
			if (Decimal.Parse(txtCantidad.Text) > Decimal.Parse(txtStockActual.Text)) pValidaciones.Append("<div>La cantidad no puede ser mayor al stock actual del producto</div>");

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			Decimal Cantidad = (DivCantidad.Visible) ? Convert.ToDecimal(txtCantidad.Text) : Convert.ToDecimal(txtRegCantidadLote.Text);

			ArrayList aListaAlmacen = new ArrayList();

			aListaAlmacen = (ArrayList)(Session["aListaAlmacen"]);
			Int32 pID = Int32.Parse(hdfIDProducto.Value);
			BEProducto oBE = new BLProducto().ProductoSeleccionar(pID, IDSucursal());
			BEMovimientoDetalle oBEMovDet = new BEMovimientoDetalle();
			oBEMovDet.IDProducto = pID;
			oBEMovDet.Codigo = oBE.Codigo;
			oBEMovDet.NombreProducto = oBE.Nombre;
			oBEMovDet.StockActual = Decimal.Parse(txtStockActual.Text.Trim());
			oBEMovDet.IDUnidadMedida = oBE.IDUnidadMedidaCompra;
			oBEMovDet.UnidadMedida = txtUnidadMedida.Text.Trim();
			oBEMovDet.Cantidad = Cantidad;

			if (ContarIngresado(aListaAlmacen, Int32.Parse(hdfIDProducto.Value)) > 0)
			{
				msgbox(TipoMsgBox.information, "Ya ingresó ese producto.");
				return;
			}

			if (aListaAlmacen == null)
			{
				aListaAlmacen = new ArrayList();
			}
			aListaAlmacen.Add(oBEMovDet);
			LlenargvListaAlmacen(aListaAlmacen);
			Session["aListaAlmacen"] = aListaAlmacen;
			gvDetalleLista.SelectedIndex = -1;
			LimpiarProducto();
			msgbox(TipoMsgBox.confirmation, "El producto se agregó con éxito");
		}

		protected void lnkNuevaTransferencia_Click(object sender, EventArgs e)
		{
			LimpiarFormularioTransferencia();
		}

		protected void lnkGuardarTransferencia_Click(object sender, EventArgs e)
		{
			StringBuilder pValidaciones = new StringBuilder();
			if (ddlIDAlmacenOrigen.SelectedValue == "0" || ddlIDAlmacenOrigen.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Almacen Origen</div>");
			if (ddlIDAlmacenDestino.SelectedValue == "0" || ddlIDAlmacenDestino.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Almacen Destino</div>");
			if (ddlIDTransaccion.SelectedValue == "0" || ddlIDTransaccion.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Transacción</div>");
			if (ddlIDAlmacenOrigen.SelectedValue == ddlIDAlmacenDestino.SelectedValue) pValidaciones.Append("<div>Seleccione Almacen de Destino diferente al Almacén de Origen</div>");

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			BLMovimiento oBLMov = new BLMovimiento();
			BEMovimiento oBEMov = new BEMovimiento();
			ArrayList ListaBEAlmacenDetalle = new ArrayList();
			oBEMov.IDMovimiento = Int32.Parse(hdfIDMovimiento.Value);
			oBEMov.IDAlmacenOrigen = Int32.Parse(ddlIDAlmacenOrigen.SelectedValue);
			oBEMov.IDAlmacenDestino = Int32.Parse(ddlIDAlmacenDestino.SelectedValue);
			oBEMov.IDEntidad = 0;
			oBEMov.Entidad = "TRANSFERENCIA";			
            oBEMov.IDTransaccion = Constantes.ID_Tran_S_Transferencia;
            oBEMov.TipoMovimiento = "S";
			oBEMov.IDSucursal = IDSucursal();
			oBEMov.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
			oBEMov.Observacion = txtMotivo.Text;
			oBEMov.Fecha = DateTime.Parse(txtFechaMovimiento.Text);
			oBEMov.IDTipoComprobante = 18;
			oBEMov.IDTipoComprobanteReferencia = 22;
			oBEMov.NumeroReferencia = "";
			oBEMov.Token = Session["token"].ToString().Trim();

			oBEMov.IDUsuario = IDUsuario();
			SetListaBEAlmacenDetalle(ref ListaBEAlmacenDetalle);

			BERetornoTran oBERetorno = new BERetornoTran();

			if (oBEMov.IDMovimiento.Equals(0))
			{
				oBERetorno = oBLMov.MovimientoTransferenciaGuardar(oBEMov, ListaBEAlmacenDetalle);
			}

            if (oBERetorno.Retorno == "1")
			{
				oBEMov = new BEMovimiento(); ;
				oBEMov.IDMovimiento = 0;
				oBEMov.IDAlmacenOrigen = Int32.Parse(ddlIDAlmacenOrigen.SelectedValue);
				oBEMov.IDAlmacenDestino = Int32.Parse(ddlIDAlmacenDestino.SelectedValue);
				oBEMov.IDEntidad = 0;
				oBEMov.Entidad = "TRANSFERENCIA";
				oBEMov.IDTransaccion = Constantes.ID_Tran_I_Transferencia;
                oBEMov.TipoMovimiento = "I";
				oBEMov.IDSucursal = IDSucursal();
				oBEMov.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
				oBEMov.Observacion = txtMotivo.Text;
				oBEMov.Fecha = DateTime.Parse(txtFechaMovimiento.Text);
				oBEMov.Token = Session["token"].ToString().Trim();

				oBEMov.IDTipoComprobante = 22;
				oBEMov.IDTipoComprobanteReferencia = 18;
				oBEMov.NumeroReferencia = oBERetorno.Retorno2;

				oBEMov.IDUsuario = IDUsuario();
				oBERetorno = oBLMov.MovimientoTransferenciaGuardar(oBEMov, ListaBEAlmacenDetalle);
				if (oBERetorno.Retorno == "1")
				{

					msgbox(TipoMsgBox.confirmation, "Transferencia Se grabo correctamente");
					Session["aListaAlmacen"] = null;
					LlenargvListaAlmacen((ArrayList)Session["aListaAlmacen"]);
					upRegistro.Update();
				}
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

		public Double ContarIngresado(ArrayList aListaAlmacen, Int32 pID)
		{
			Int32 p = 0;
			if (aListaAlmacen == null)
			{
				p = 0;
			}
			else
			{
				//Int32 i = 0;
				foreach (BEMovimientoDetalle oBE in aListaAlmacen)
				{
					if (Int32.Parse(oBE.IDProducto.ToString()) == pID)
					{
						p += 1;
						break;
					}

				}
			}
			return p;
		}

		private void LimpiarFormularioTransferencia()
		{
			ddlIDAlmacenDestino.SelectedIndex = -1;
			txtFechaMovimiento.Text = DateTime.Today.ToShortDateString();
			txtNumeroDocumento.Text = String.Empty;
			txtNumeroReferencia.Text = String.Empty;
			txtMotivo.Text = String.Empty;
			ddlIDAlmacenOrigen.SelectedValue = Convert.ToString(IDSucursal());
			ddlIDTransaccion.SelectedValue = "32";
			hdfIDSucursal.Value = IDSucursal().ToString();
			Session["aListaAlmacen"] = null;
			Session["token"] = null;
			GenerarToken();
			LlenargvListaAlmacen((ArrayList)Session["aListaAlmacen"]);
			upRegistro.Update();
		}

		private void SetListaBEAlmacenDetalle(ref ArrayList ListaBEAlmacenDetalle)
		{
			ArrayList aListaBEAlmacenDetalle = new ArrayList();
			aListaBEAlmacenDetalle = (ArrayList)Session["aListaAlmacen"];
			if (aListaBEAlmacenDetalle != null)
			{
				Int32 p = 0;

				foreach (BEMovimientoDetalle oBE in aListaBEAlmacenDetalle)
				{
					p += 1;
					oBE.IDMovimientoDetalle = 0;
					oBE.IDMovimiento = 0;
					oBE.IDProducto = oBE.IDProducto;
					oBE.NombreProducto = oBE.NombreProducto;
					oBE.IDUnidadMedida = oBE.IDUnidadMedida;
					oBE.Cantidad = oBE.Cantidad;
					oBE.IDUsuario = IDUsuario();
					ListaBEAlmacenDetalle.Add(oBE);

				}
			}
		}

        #endregion

        

        #region Gestionar Lote

        protected void lnkAbrirLote_Click(object sender, EventArgs e)
        {
			StringBuilder pValidaciones = new StringBuilder();
			if (ddlIDProducto.SelectedValue == "0") pValidaciones.Append("<div>Seleccione Producto</div>");

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			MovimientoDetalleLoteListar();
			registrarScript("funModalListaLoteAbrir();");
		}

		private void MovimientoDetalleLoteListar()
		{
			hdIDProductoLote.Value = ddlIDProducto.SelectedValue;

			BLMovimientoDetalleLote oBLLote = new BLMovimientoDetalleLote();
			BEMovimientoDetalleLote oBE = new BEMovimientoDetalleLote();
			oBE.IDMovimiento = Int32.Parse(hdfIDMovimiento.Value);
			oBE.IDMovimientoDetalle = Int32.Parse(hdfIDMovimientoDetalle.Value);
			oBE.IDSucursal = IDSucursal();
			oBE.IDProducto = Int32.Parse(hdIDProductoLote.Value);
			oBE.Token = Session["token"].ToString().Trim();
			gvLoteProducto.DataSource = oBLLote.MovimientoDetalleLoteListar(oBE);
			gvLoteProducto.DataBind();
			upLoteListar.Update();
		}

		protected void lnkAplicarLote_Click(object sender, EventArgs e)
		{
			Decimal Cantidad = 0;

			foreach (GridViewRow row in gvLoteProducto.Rows)
			{
				Decimal CantidadLote = 0;
				if ((((TextBox)row.Cells[2].FindControl("txtCantidadLote")).Text) != "")
				{
					CantidadLote = Convert.ToDecimal(((TextBox)row.Cells[2].FindControl("txtCantidadLote")).Text);
				}
				//Registrar en NotaAjusteDetalleLote
				BEMovimientoDetalleLote BEMovimientoLote = new BEMovimientoDetalleLote();
				BEMovimientoLote.IDProducto = Int32.Parse(ddlIDProducto.SelectedValue);
				BEMovimientoLote.IDLote = Int32.Parse(((Label)row.Cells[0].FindControl("lblIDLote")).Text);
				BEMovimientoLote.Cantidad = CantidadLote;
				BEMovimientoLote.Estado = false;
				BEMovimientoLote.IDUsuario = IDUsuario();
				BEMovimientoLote.Token = Session["token"].ToString().Trim();
				BLMovimientoDetalleLote oBL = new BLMovimientoDetalleLote();
				BERetornoTran oBERetorno = oBL.MovimientoDetalleLoteTemporalGuardar(BEMovimientoLote);
				if (oBERetorno.Retorno != "1")
				{
					msgbox(TipoMsgBox.confirmation, "No se pudo registrar las cantidades del lote");
				}
				Cantidad += CantidadLote;
			}
			txtRegCantidadLote.Text = Cantidad.ToString("N");
			upRegistroProducto.Update();
			registrarScript("funModalListaLoteCerrar();");
		}

		protected void btnCancelarLote_Click(object sender, EventArgs e)
		{
			registrarScript("funModalLoteCerrar();");
		}

		protected void lnkNuevoLote_Click(object sender, EventArgs e)
		{
			LimpiarLote();
			registrarScript("funModalLoteAbrir();");
		}

		private void LimpiarLote()
		{
			hdfIDLote.Value = "0";
			txtLote.Text = "";
			txtLFechaFabricacion.Text = "";
			txtLFechaVencimiento.Text = "";
			//txtLCantidad.Text = "0";
			UpRegistroLote.Update();
		}

		protected void gvLoteProducto_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{

				hdfIDLote.Value = e.CommandArgument.ToString();

				BERetornoTran oBERetorno = new BERetornoTran();
				switch (e.CommandName)
				{
					case "Editar":
						SeleccionarLote();
						registrarScript("funModalLoteAbrir();");
						break;
					case "Eliminar":
						oBERetorno = new BLLote().LoteEliminar(Int32.Parse(hdfIDLote.Value));
						if (oBERetorno.Retorno == "1")
						{
							msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
							MovimientoDetalleLoteListar();
						}
						else
						{
							if (oBERetorno.Retorno != "-1")
							{
								msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
							}
							else
							{
								RegistrarLogSistema("gvLista_RowCommand()", oBERetorno.ErrorMensaje, true);
							}
						}

						break;
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLoteProducto_RowCommand()", ex.Message, true);
			}
		}

		private void SeleccionarLote()
		{
			BELote oBE = new BLLote().LoteSeleccionar(Int32.Parse(hdfIDLote.Value));
			txtLote.Text = oBE.Lote;
			//txtLCantidad.Text = oBE.CantidadLote.ToString();
			txtLFechaVencimiento.Text = oBE.FechaVencimiento.ToShortDateString();
			txtLFechaFabricacion.Text = oBE.FechaFabricacion.ToShortDateString();
			//txtLCantidad.Enabled = false;
			UpRegistroLote.Update();
			registrarScript("funModalLoteAbrir();");
		}

		protected void btnGuardarLote_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (txtLote.Text.Length == 0) validacion.Append("<div>Ingrese Lote.</div>");
				//if (txtLCantidad.Text.Length == 0) validacion.Append("<div>Ingrese Cantidad.</div>");
				if (txtLFechaFabricacion.Text.Length == 0) validacion.Append("<div>Ingrese Fecha Fabricación.</div>");
				if (txtLFechaVencimiento.Text.Length == 0) validacion.Append("<div>Ingrese Fecha Vencimiento.</div>");
				if (Convert.ToDateTime(txtLFechaVencimiento.Text) <= Convert.ToDateTime(txtLFechaFabricacion.Text)) validacion.Append("<div>Fecha Vencimiento debe ser mayor a Fecha Fabricación.</div>");

				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BELote oBE = new BELote();
				BLLote oBL = new BLLote();
				oBE.Token = hdfToken.Value;
				oBE.IDLote = Int32.Parse(hdfIDLote.Value);
				oBE.IDProducto = Int32.Parse(hdIDProductoLote.Value);
				oBE.IDSucursal = IDSucursal();
				oBE.Lote = txtLote.Text.Trim();
				//oBE.CantidadLote = Decimal.Parse(txtLCantidad.Text.Trim());
				oBE.FechaFabricacion = DateTime.Parse(txtLFechaFabricacion.Text);
				oBE.FechaVencimiento = DateTime.Parse(txtLFechaVencimiento.Text);
				oBE.Estado = true; //PENDIENTE 
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.LoteAjusteGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{

					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
					registrarScript("funModalLoteCerrar();");
					LimpiarLote();
					MovimientoDetalleLoteListar();
					upLoteListar.Update();
				}
				else
				{
					msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					RegistrarLogSistema("btnGuardarLote_Click()", oBERetorno.ErrorMensaje, true);
				}

			}
			catch (Exception ex)
			{

				RegistrarLogSistema("btnGuardarLote_Click()", ex.Message, true);
			}
		}

		#endregion

	}
}