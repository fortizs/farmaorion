using Farmacia.App_Class;
using Farmacia.App_Class.BE.Compras;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.Compras;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Compras
{
	public partial class Compras : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				DateTime fechatemp;
				DateTime fecha1;
				fechatemp = DateTime.Today;
				fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);
				ConfigPage();
				CargaInicial();
				CargarDDL(ddlSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", false);

                if (EsAdmin()) {
                    CargarDDL(ddlIDAlmacen, new BLAlmacen().AlmacenListar(0), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR);
                }
                else
                {
                    CargarDDL(ddlIDAlmacen, new BLAlmacen().AlmacenListar(IDSucursal()), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR);
                }
			    

			    CargarDDL(ddlBIDEstadoCompra, new BLEstado().EstadoListar("COM"), "Codigo", "Nombre", true, Constantes.TODOS);
				txtFechaInicio.Text = fecha1.ToShortDateString();
				txtFechaFin.Text = DateTime.Today.ToShortDateString();
				Listar();
                CargarDDL(ddlIDPTipoDocumento, new BLTipoDocumento().TipoDocumentoListar(), "IDTipoDocumento", "Nombre");
            }
		}

		private void CargaInicial()
		{
          
                hdfIDSucursal.Value = IDSucursal().ToString();
                ArrayList aListaCompras = new ArrayList();
                if (Session["aListaCompras"] == null) //Trabajando en Memoria
                {
                    Session["aListaCompras"] = aListaCompras;
                }
                else
                {
                    aListaCompras = (ArrayList)(Session["aListaCompras"]);
                }
                LlenargvListaCompras(aListaCompras);
                CargarDDL(ddlRegMoneda, new BLMoneda().MonedaListar(), "IDMoneda", "Nombre", false);
                CargarDDL(ddlRegIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("DE","VEN"), "IDTipoComprobante", "Nombre", true, Constantes.SELECCIONAR);
                CargarDDL(ddlRegFormaPago, new BLFormaPago().FormaPagoListar(), "IDFormaPago", "Nombre", true);
                CargarDDL(ddlRegIDTipoDocumento, new BLTipoDocumento().TipoDocumentoListar(), "IDTipoDocumento", "Nombre", false);
                CargarDDL(ddlIDReProducto, new BLProducto().ListarPorSucursal(IDSucursal()), "IDProducto", "Nombre", true, Constantes.SELECCIONAR); 
                ddlRegIDTipoDocumento.SelectedValue = "3";
                CalcularTotales();
                NumeroCompraSeleccionar();
                         
		}
		protected void NumeroCompraSeleccionar()
		{
			lblNumeroCompra.Text = new BLCompras().ComprasNumeroSeleccionar("C");
		}

		protected void txtSerieDocumento_TextChanged(object sender, EventArgs e)
		{
			txtSerieDocumento.Text = txtSerieDocumento.Text.PadLeft(4, '0');
		}

		protected void txtNumeroDocumento_TextChanged(object sender, EventArgs e)
		{
			txtNumeroDocumento.Text = txtNumeroDocumento.Text.PadLeft(8, '0');
		}

		#endregion

		#region Listar 
		private void Listar()
		{
			BLCompras oBLCompras = new BLCompras();
			gvLista.DataSource = oBLCompras.ComprasListar(Int32.Parse(ddlSucursal.SelectedValue),0, txtCOMFiltro.Text.Trim(), txtFechaInicio.Text, txtFechaFin.Text, Int32.Parse(ddlBIDEstadoCompra.SelectedValue) , 0, "C");
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
            //GridViewRow clickedRow = ((GridViewRow)sender).NamingContainer as GridViewRow;
            //LinkButton lnkApro = (LinkButton)clickedRow.FindControl("lnkAprobar");
            //GridViewRow row = gvLista.Rows[Int32.Parse(hdfIDCompra.Value)];
            //LinkButton lnk2 = (LinkButton)row.FindControl("lnkAprobar");
            //LinkButton lnkBtn = (LinkButton)e.CommandSource;
            //GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;
            //GridView myGrid = (GridView)sender;
            //LinkButton pic = (LinkButton)gvLista.FindControl("lnkAprobar");

            switch (e.CommandName)
			{

				case "Editar":
					CompraSeleccionar();
					ListarCompraDetalle();
                    lnkGuardarCompra.Visible = false;
                    upRegistroCompra.Update();
					registrarScript("ActivarTabxId('tab2');");
					break;
				case "Aprobar": 
					oBERetorno = new BLCompras().ComprasCambiarEstado(Int32.Parse(hdfIDCompra.Value), 2, IDUsuario());
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
					oBERetorno = new BLCompras().ComprasCambiarEstado(Int32.Parse(hdfIDCompra.Value), 3, IDUsuario());
					if (oBERetorno.Retorno == "1")
					{
                        //LinkButton btnSelect = (sender as LinkButton);
                        //LinkButton lnk2 = (LinkButton)GridViewRow.FindControl("lnkAprobar"); 
                        //LinkButton button = gvLista.FindControl("DeleteButton") as button;
                        msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
                        //.Enabled = false; 
                        Listar();
                        //pic.Visible = false;

                    }
                    else if (oBERetorno.Retorno == "-1")
					{
						RegistrarLogSistema("gvLista_RowCommand()", oBERetorno.ErrorMensaje, true);
					}
					break;
                case "VerMotivo":
                    CompraSeleccionar();
                    ListarCompraDetalle();
                    lnkGuardarCompra.Visible = false;
                    upRegistroCompra.Update();
                    registrarScript("ActivarTabxId('tab2');");
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
			aListaDetalle = (ArrayList)new BLCompras().ComprasDetalleListar(Int32.Parse(hdfIDCompra.Value));
			LlenargvListaCompras(aListaDetalle);
			Session["aListaCompras"] = aListaDetalle;
			gvDetalleLista.SelectedIndex = -1;
			upRegistroCompra.Update();
		}

		private void ListarCompraDetalleDocumentoModifica()
		{
			ArrayList aListaDetalle = new ArrayList();
			aListaDetalle = (ArrayList)new BLCompras().ComprasDetalleListar(Int32.Parse(hdfIDCompraDocumentoModifica.Value));
			LlenargvListaCompras(aListaDetalle);
			Session["aListaCompras"] = aListaDetalle;
			gvDetalleLista.SelectedIndex = -1;
			upRegistroCompra.Update();
		}



		protected void CompraSeleccionar()
		{
			BECompras oBE = new BLCompras().ComprasSeleccionar(Int32.Parse(hdfIDCompra.Value));
			lblNumeroCompra.Text = oBE.NumeroCompra;
			ddlIDAlmacen.SelectedValue = oBE.IDAlmacen.ToString();
			ddlRegIDTipoComprobante.SelectedValue = oBE.IDTipoDocumento.ToString();
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
			//txtCuenta.Text = oBE.Cuenta;
			//txtCuentaCaja.Text = oBE.CuentaCaja;
			//txtGlosa.Text = oBE.Glosa;
			lblSubTotal.Text = oBE.SubTotal.ToString();
			hdfSubTotal.Value = oBE.SubTotal.ToString();
			lblTotalIgv.Text = oBE.TotalIGV.ToString();
			hdfTotalIGV.Value = oBE.TotalIGV.ToString();
			lblTotal.Text = oBE.TotalCompra.ToString();
			hdfTotalCompra.Value = oBE.TotalCompra.ToString();

			//hdfTipoDocumentoReferencia.Value = oBE.TipoDocumentoReferencia;
			//txtDocumentoReferencia.Text = oBE.SerieNumeroDocumentoReferencia;

			//if (oBE.FechaEmisionDocumentoReferencia == DateTime.Parse("1900-01-01"))
			//{
			//	txtFechaDocumentoReferencia.Text = "";
			//}
			//else {
			//	txtFechaDocumentoReferencia.Text = oBE.FechaEmisionDocumentoReferencia.ToShortDateString();
			//}

		}

		#endregion

		#region RegistroCompra

		protected void rblTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
		{
			NumeroCompraSeleccionar();
		}

		//protected void ddlRegIDTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	txtDocumentoReferencia.Text = "";
		//	txtFechaDocumentoReferencia.Text = "";

		//	if (ddlRegIDTipoComprobante.SelectedValue == "3" || ddlRegIDTipoComprobante.SelectedValue == "4")
		//	{
		//		txtDocumentoReferencia.Enabled = true;
		//		lnkBuscarDocumentoReferencia.Enabled = true;
		//	}
		//	else {
		//		txtDocumentoReferencia.Enabled = false;
		//		lnkBuscarDocumentoReferencia.Enabled = false;
		//	}
		//}

		protected void lnkBusProveedor_Click(object sender, EventArgs e)
		{
			try
			{
				hdfRegIDProveedor.Value = "0";
				txtRegProveedor.Text = "";
                registrarScript("funModalProveedorAbrir();");
				if (txtRegNumeroDocumento.Text.Trim().Length == 0 || txtRegNumeroDocumento.Text.Trim().Equals("*")
					|| ddlRegIDTipoDocumento.SelectedValue.Equals("2") || ddlRegIDTipoDocumento.SelectedValue.Equals("4")
					|| ddlRegIDTipoDocumento.SelectedValue.Equals("5") || ddlRegIDTipoDocumento.SelectedValue.Equals("6"))
				{
					
				}
				else {
					StringBuilder pValidaciones = new StringBuilder();
					//if (ddlRegIDTipoDocumento.SelectedValue == "1" && txtRegNumeroDocumento.Text.Trim().Length != 8) pValidaciones.Append("<div>El número dni no es válido.</div>");
					//if (ddlRegIDTipoDocumento.SelectedValue == "3" && txtRegNumeroDocumento.Text.Trim().Length != 11) pValidaciones.Append("<div>El número ruc no es válido.</div>");

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
			catch (Exception ex)
			{
				RegistrarLogSistema("lnkBusProveedor_Click()", ex.ToString(), true);
			}

		}

		protected void lnkNuevaCompra_Click(object sender, EventArgs e)
		{
			LimpiarCompra();
		}

		protected void lnkGuardarCompra_Click(object sender, EventArgs e)
		{
			StringBuilder pValidaciones = new StringBuilder();
			if (ddlIDAlmacen.SelectedValue == "0" || ddlIDAlmacen.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Almacen</div>");
			if (ddlRegIDTipoComprobante.SelectedValue == "0" || ddlRegIDTipoComprobante.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Tipo Comprobante</div>");
			if (txtSerieDocumento.Text == "") pValidaciones.Append("<div>Ingrese Serie Documento</div>");
			if (txtSerieDocumento.Text.Length != 4) pValidaciones.Append("<div>Ingrese Serie Válida</div>");
			if (txtNumeroDocumento.Text == "") pValidaciones.Append("<div>Ingrese Numero Documento</div>");
			if (ddlRegFormaPago.SelectedValue == "0") pValidaciones.Append("<div>Seleccione Forma de Pago</div>");
			if (hdfRegIDProveedor.Value == "0") pValidaciones.Append("<div>Seleccione un Proveedor</div>");
			//if (txtCuenta.Text == "") pValidaciones.Append("<div>Ingrese Cuenta</div>");

			ArrayList aListaDetalleCompra = new ArrayList();
			if (Session["aListaCompras"] == null) //Trabajando en Memoria
			{
				Session["aListaCompras"] = aListaDetalleCompra;
			}
			else
			{
				aListaDetalleCompra = (ArrayList)(Session["aListaCompras"]);
			}
			if (aListaDetalleCompra.Count == 0) pValidaciones.Append("<div>El detalle no puede estar vacío!.</div>");

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}
            
            BLCompras oBLCompras = new BLCompras();
			BECompras oBECompras = new BECompras();
			ArrayList ListaBEComprasDetalle = new ArrayList();

			oBECompras.IDCompras = Int32.Parse(hdfIDCompra.Value);
			oBECompras.IDSucursal = IDSucursal();
			oBECompras.IDAlmacen = Int32.Parse(ddlIDAlmacen.SelectedValue);
			oBECompras.IDProveedor = Int32.Parse(hdfRegIDProveedor.Value);
			oBECompras.IDMoneda = ddlRegMoneda.SelectedValue;
			oBECompras.IDTipoDocumento = Int32.Parse(ddlRegIDTipoComprobante.SelectedValue);
			oBECompras.IDFormaPago = Int32.Parse(ddlRegFormaPago.SelectedValue); 
			oBECompras.TipoCompra = "C";
			oBECompras.Cuenta = "";
			oBECompras.CuentaCaja = "";
			oBECompras.Glosa = "";
			oBECompras.Serie = txtSerieDocumento.Text;
			oBECompras.NumeroDocumento = txtNumeroDocumento.Text;
			oBECompras.FechaRegistro = DateTime.Parse(txtFechaRegistro.Text);
			oBECompras.FechaCompra = DateTime.Parse(txtFechaEmision.Text);
			oBECompras.FechaVencimiento = DateTime.Parse(txtFechaVencimiento.Text);
			oBECompras.TotalCompra = Decimal.Parse(hdfTotalCompra.Value);
			oBECompras.TotalIGV = Decimal.Parse(hdfTotalIGV.Value);

			oBECompras.TipoDocumentoReferencia ="";
			oBECompras.SerieNumeroDocumentoReferencia = "";
            oBECompras.FechaEmisionDocumentoReferencia = DateTime.Parse(Constantes.FECHA_NULA);
            oBECompras.IDUsuario = IDUsuario();

			SetListaBEComprasDetalle(ref ListaBEComprasDetalle);

			BERetornoTran oBERetorno = new BERetornoTran();

			if (hdfIDCompra.Value == "0")
			{
				oBERetorno = oBLCompras.GuardarCompras(oBECompras, ListaBEComprasDetalle);
			}
			else {
				oBERetorno = oBLCompras.ComprasActualizar(oBECompras, ListaBEComprasDetalle);
			}

			if (oBERetorno.Retorno == "1")
			{
				msgbox(TipoMsgBox.confirmation, "Registro de Compra grabo correctamente");
				Listar();
				Session["aListaCompras"] = null;
				LlenargvListaCompras((ArrayList)Session["aListaCompras"]);
				LimpiarCompra();
				CalcularTotales();
				NumeroCompraSeleccionar();
				upRegistroCompra.Update();
			}
			else if (oBERetorno.Retorno == "2")
			{
				msgbox(TipoMsgBox.warning, "El documento ya existe para ese proveedor");
			}
			else if (oBERetorno.Retorno == "-1")
			{
				RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
			}
		}

		public void LlenargvListaCompras(ArrayList DT)
		{
			gvDetalleLista.DataSource = DT;
			gvDetalleLista.DataBind();
			upRegistroCompra.Update();
		}

		private void SetListaBEComprasDetalle(ref ArrayList ListaBEComprasDetalle)
		{
			ArrayList aListaBEComprasDetalle = new ArrayList();
			aListaBEComprasDetalle = (ArrayList)Session["aListaCompras"];
			if (aListaBEComprasDetalle != null)
			{
				Int32 p = 0;

				foreach (BEComprasDetalle oBE in aListaBEComprasDetalle)
				{
					p += 1;
					oBE.IDComprasDetalle = 0;
					oBE.IDCompras = 0;
					oBE.IDProducto = oBE.IDProducto;
					oBE.DetalleProducto = "";
					oBE.Item = p;
					oBE.IDUnidadMedida = oBE.IDUnidadMedida;
					oBE.Cantidad = oBE.Cantidad;
					oBE.PrecioUnitario = oBE.PrecioUnitario;
					oBE.IDUsuario = IDUsuario();
					ListaBEComprasDetalle.Add(oBE);

				}
			}
		}

		protected void gvDetalleLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			ArrayList aListaCompras = new ArrayList();
			aListaCompras = (ArrayList)(Session["aListaCompras"]);
			aListaCompras.RemoveAt(e.RowIndex);
			LlenargvListaCompras(aListaCompras);
			gvDetalleLista.SelectedIndex = -1;
			Session["aListaCompras"] = aListaCompras;
			CalcularTotales();
		}

		protected void gvDetalleLista_SelectedIndexChanged(object sender, EventArgs e)
		{
            hdfIDCompraDetalle.Value = gvDetalleLista.SelectedIndex.ToString();
            hdfIDProducto.Value = gvDetalleLista.SelectedDataKey["IDProducto"].ToString();
            BEProducto oBE = new BLProducto().ProductoSeleccionar(Int32.Parse(hdfIDProducto.Value), IDSucursal());
            GridViewRow row = gvDetalleLista.SelectedRow;
            hdfIDUnidadMedida.Value = oBE.IDUnidadMedida.ToString();
            ddlIDReProducto.SelectedValue = oBE.IDProducto.ToString();
            //txtRegProducto.Text = oBE.CodigoProducto + "-" + oBE.Nombre;
            txtRegStock.Text = oBE.Stock.ToString();
            txtRegCantidad.Text = ((Label)row.FindControl("lblCantidad")).Text;
            txtRegPrecioCompra.Text = ((Label)row.FindControl("lblPrecioUnitario")).Text;

            registrarScript("funModalProductoAbrir();");
            upRegistroProducto.Update();

        }

		private void LimpiarCompra()
		{
			NumeroCompraSeleccionar();
			hdfIDCompra.Value = "0";
			hdfIDCompraDetalle.Value = "-1";
			Session["aListaCompras"] = null;
			LlenargvListaCompras((ArrayList)Session["aListaCompras"]);
			ddlRegIDTipoComprobante.SelectedIndex = -1;
			ddlIDAlmacen.SelectedIndex = -1;
			txtSerieDocumento.Text = "";
			txtNumeroDocumento.Text = "";
			ddlRegMoneda.SelectedIndex = -1;
			hdfRegIDProveedor.Value = "0";
			txtRegNumeroDocumento.Text = "";
			txtRegProveedor.Text = "";
			txtRegProveedorNumeroDocumento.Text = "";
			ddlRegFormaPago.SelectedIndex = -1;
			//txtCuenta.Text = "";
			//txtCuentaCaja.Text = "";
			//txtGlosa.Text = "";
			lblSubTotal.Text = "0.00";
			lblTotalIgv.Text = "0.00";
			lblTotal.Text = "0.00";
			hdfSubTotal.Value = "0";
			hdfTotalIGV.Value = "0";
			hdfTotalCompra.Value = "0";
            txtRegUnidMedida.Text = "";
            lnkGuardarCompra.Visible = true;




   //         hdfTipoDocumentoReferencia.Value = "";
			//txtDocumentoReferencia.Text = "";
			//txtFechaDocumentoReferencia.Text = ;
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
			if (ddlIDReProducto.SelectedValue == "0") pValidaciones.Append("<div>Seleccione un Producto</div>");
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

			ArrayList aListaCompras = new ArrayList();

			aListaCompras = (ArrayList)(Session["aListaCompras"]);
			Int32 pID = Int32.Parse(hdfIDCompraDetalle.Value);
			Int32 pIDProducto = Int32.Parse(ddlIDReProducto.SelectedValue);
			BEProducto oBE = new BLProducto().ProductoSeleccionar(pIDProducto, IDSucursal());

			BEComprasDetalle oBEVentaDet = new BEComprasDetalle(); 

			oBEVentaDet.IDProducto = pIDProducto;
			oBEVentaDet.CodigoProducto = oBE.CodigoProducto;
			oBEVentaDet.Producto = oBE.Nombre;
			oBEVentaDet.ProductoDetalle = "";
			oBEVentaDet.Stock = oBE.Stock;
			oBEVentaDet.IDUnidadMedida = oBE.IDUnidadMedidaCompra;
			oBEVentaDet.PrecioUnitario = Decimal.Parse(txtRegPrecioCompra.Text);
			oBEVentaDet.Cantidad = Int32.Parse(txtRegCantidad.Text.Trim());
			oBEVentaDet.SubTotal = Int32.Parse(txtRegCantidad.Text.Trim()) * Decimal.Parse(txtRegPrecioCompra.Text);

			if (pID == -1)
			{
				if (ContarIngresado(aListaCompras, Int32.Parse(hdfIDProducto.Value)) > 0)
				{
					msgbox(TipoMsgBox.information, "Ya ingresó ese producto.");
					return;
				}
			}

			if (aListaCompras == null)
			{
				aListaCompras = new ArrayList();
			}
			if (hdfIDCompraDetalle.Value == "-1")
			{

				aListaCompras.Add(oBEVentaDet);
			}
			else
			{
				aListaCompras[pID] = oBEVentaDet;
			}


			LlenargvListaCompras(aListaCompras);
			Session["aListaCompras"] = aListaCompras;
			CalcularTotales();
			gvDetalleLista.SelectedIndex = -1;
			LimpiarProducto();
			msgbox(TipoMsgBox.confirmation, "Se agregó satisfactoriamente.");

		}

		private void LimpiarProducto()
		{
			hdfIDCompraDetalle.Value = "-1";
			hdfIDProducto.Value = "0"; 
            ddlIDReProducto.SelectedIndex = -1;
            txtRegUnidMedida.Text = "";
			txtRegCantidad.Text = "1";
			txtRegPrecioCompra.Text = "";
			txtRegStock.Text = "";
			hdfIDUnidadMedida.Value = "0";
			upRegistroProducto.Update(); 
		}

        protected void ddlIDReProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 pIDProducto = Int32.Parse(ddlIDReProducto.SelectedValue);
            BEProducto oBE = new BLProducto().ProductoSeleccionar(pIDProducto, IDSucursal());
            txtRegStock.Text = oBE.Stock.ToString();
            hdfIDUnidadMedida.Value = oBE.IDUnidadMedida.ToString();
            txtRegUnidMedida.Text = oBE.UnidadMedidaCompra;
            txtRegPrecioCompra.Text = oBE.PrecioCosto.ToString(); 
            upRegistroProducto.Update();
        }

        protected void ddlRegIDProducto_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32 pIDProducto = Int32.Parse(hdfIDProducto.Value);
			BEProducto oBE = new BLProducto().ProductoSeleccionar(pIDProducto,IDSucursal());
			txtRegStock.Text = oBE.Stock.ToString();
			hdfIDUnidadMedida.Value = oBE.IDUnidadMedida.ToString();
            txtRegUnidMedida.Text = oBE.UnidadMedida;
			txtRegPrecioCompra.Text = oBE.PrecioVenta.ToString();
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

		protected void gvListadoProveedor_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvListadoProveedor.PageIndex = e.NewPageIndex;
			gvListadoProveedor.SelectedIndex = -1;
			ListarProveedor();
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

		public Double ContarIngresado(ArrayList aListaCompras, Int32 pID)
		{
			Int32 p = 0;
			if (aListaCompras == null)
			{
				p = 0;
			}
			else
			{
				//Int32 i = 0;
				foreach (BEComprasDetalle oBE in aListaCompras)
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

		protected void CalcularTotales()
		{
			ArrayList ImporteTotales = new ArrayList();
			Decimal OperacionTotal = Decimal.Parse("0.00");
			Decimal pIgv = Decimal.Parse("1.18");

			foreach (GridViewRow fila in gvDetalleLista.Rows)
			{
				String hfSubTotal = ((HiddenField)fila.Cells[0].FindControl("hfSubTotal")).Value;
				OperacionTotal += Math.Round(Decimal.Parse(hfSubTotal), 2, MidpointRounding.AwayFromZero);
			}

			lblTotal.Text = Math.Round(OperacionTotal, 2, MidpointRounding.AwayFromZero).ToString();
			hdfTotalCompra.Value = Math.Round(OperacionTotal, 2, MidpointRounding.AwayFromZero).ToString();

			lblSubTotal.Text = Math.Round((OperacionTotal / pIgv), 2, MidpointRounding.AwayFromZero).ToString();
			hdfSubTotal.Value = Math.Round((OperacionTotal / pIgv), 2, MidpointRounding.AwayFromZero).ToString();

			lblTotalIgv.Text = Math.Round((Decimal.Parse(hdfTotalCompra.Value) - Decimal.Parse(hdfSubTotal.Value)), 2, MidpointRounding.AwayFromZero).ToString();
			hdfTotalIGV.Value = Math.Round((Decimal.Parse(hdfTotalCompra.Value) - Decimal.Parse(hdfSubTotal.Value)), 2, MidpointRounding.AwayFromZero).ToString();



		}

        #endregion

        //#region DocumentoReferencia

        //protected void lnkBuscarDocumentoReferencia_Click(object sender, EventArgs e)
        //{

        //	registrarScript("funModalDocumentoReferenciaAbrir();");
        //}

        //private void ListarDocumentoElectronico()
        //{
        //	BLCompras oBLCompras = new BLCompras();
        //	gvDocumentoElectronicoListar.DataSource = oBLCompras.ComprasListar(IDSucursal(),0, txtBFiltro.Text.Trim(), txtBFechaInicio.Text, txtBFechaFin.Text,0, 0, "C");
        //	gvDocumentoElectronicoListar.DataBind();
        //}

        //protected void gvDocumentoElectronicoListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //	gvDocumentoElectronicoListar.PageIndex = e.NewPageIndex;
        //	gvDocumentoElectronicoListar.SelectedIndex = -1;
        //	ListarDocumentoElectronico();
        //}

        //protected void btnBuscarDocumento_Click(object sender, EventArgs e)
        //{
        //	ListarDocumentoElectronico();
        //}

        //protected void gvDocumentoElectronicoListar_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //	hdfIDCompraDocumentoModifica.Value = gvDocumentoElectronicoListar.SelectedDataKey["IDCompras"].ToString();
        //	GridViewRow row = gvDocumentoElectronicoListar.SelectedRow;
        //	hdfTipoDocumentoReferencia.Value = ((Label)row.FindControl("lblIDTipoComprobanteCS")).Text;
        //	txtDocumentoReferencia.Text = ((Label)row.FindControl("lblSerieNumero")).Text;
        //	txtFechaDocumentoReferencia.Text = ((Label)row.FindControl("lblFechaCompra")).Text;
        //	hdfRegIDProveedor.Value = ((HiddenField)row.FindControl("hdfIDProveedor")).Value;
        //	txtRegProveedorNumeroDocumento.Text = ((Label)row.FindControl("lblRucProveedor")).Text;
        //	txtRegProveedor.Text = ((Label)row.FindControl("lblProveedor")).Text;
        //	txtRegNumeroDocumento.Text = ((Label)row.FindControl("lblRucProveedor")).Text;
        //	upRegistroCompra.Update();
        //	ListarCompraDetalleDocumentoModifica();
        //	CalcularTotales();
        //	registrarScript("funModalDocumentoReferenciaCerrar();");
        //}

        //      #endregion

        #region Lote


        #region Listar
        //private void ListarLote()
        //{
        //    BLLote oBLLote = new BLLote();
        //    gvLote.DataSource = oBLLote.LoteListar(Int32.Parse(ddlIDReProducto.SelectedValue));
        //    gvLote.DataBind();
        //    upLista.Update();

        //}


        //private void SeleccionarLote()
        //{
        //    //hdfIDLote.Value = gvLista.SelectedDataKey["IDLote"].ToString();
        //    BELote oBE = new BLLote().LoteSeleccionar(Int32.Parse(hdfIDLote.Value)); 
        //    txtLote.Text = oBE.Lote;
        //    txtLCantidad.Text = oBE.CantidadLote.ToString();
        //    txtLFechaVencimiento.Text = oBE.FechaVencimiento.ToShortDateString();
        //    txtLFechaFabricacion.Text = oBE.FechaFabricacion.ToShortDateString();
        //    UpRegistroLote.Update();
        //    registrarScript("funModalLoteAbrir();");
        //}



        //protected void gvLote_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvLote.PageIndex = e.NewPageIndex;
        //    gvLote.SelectedIndex = -1;
        //    ListarLote();
        //}

        //protected void gvLote_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //    try
        //    {

        //        hdfIDLote.Value = e.CommandArgument.ToString();

        //        BERetornoTran oBERetorno = new BERetornoTran();
        //        switch (e.CommandName)
        //        {
        //            case "Editar":
        //                SeleccionarLote(); 
        //                registrarScript("funModalLoteAbrir();");
        //                break;
        //            case "Seleccionar":

        //                break;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        RegistrarLogSistema("gvLote_RowCommand()", ex.Message, true);
        //    }

        //}

        #endregion
        //protected void lnkNuevoLote_Click(object sender, EventArgs e)
        //{
        //    LimpiarLote();
        //    registrarScript("funModalLoteAbrir();");
        //}

        //private void LimpiarLote()
        //{
        //    hdfIDLote.Value = "0";
        //    txtLote.Text = "";
        //    txtLCantidad.Text = "";
        //    txtLFechaFabricacion.Text = "";
        //    txtLFechaVencimiento.Text = "";
        //    UpRegistroLote.Update();
        //}

        //protected void btnGuardarLote_Click(object sender, EventArgs e)
        //{
        //    StringBuilder validacion = new StringBuilder();
        //    if (txtLote.Text.Length == 0) validacion.Append("<div>Ingrese Lote.</div>");
        //    if (txtLCantidad.Text.Length == 0) validacion.Append("<div>Ingrese Cantidad.</div>");
        //    if (validacion.Length > 0)
        //    {
        //        msgbox(TipoMsgBox.warning, validacion.ToString());
        //        return;
        //    }

        //    BELote oBE = new BELote();
        //    BLLote oBL = new BLLote();
        //    oBE.IDLote = Int32.Parse(hdfIDLote.Value);
        //    oBE.IDProducto = Int32.Parse(ddlIDReProducto.SelectedValue);
        //    oBE.IDSucursal = IDSucursal();
        //    oBE.Lote = txtLote.Text.Trim();
        //    oBE.CantidadLote = Int32.Parse(txtLCantidad.Text);
        //    oBE.FechaFabricacion = DateTime.Parse(txtLFechaFabricacion.Text);
        //    oBE.FechaVencimiento = DateTime.Parse(txtLFechaVencimiento.Text);

        //    oBE.IDUsuario = IDUsuario();
        //    BERetornoTran oBERetorno = new BERetornoTran();
        //    oBERetorno = oBL.LoteGuardar(oBE);

        //    if (oBERetorno.Retorno == "1")
        //    {

        //        msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
        //        registrarScript("funModalLoteCerrar();");
        //        LimpiarLote();
        //        ListarLote();
        //        upRegistroProducto.Update();
        //    }
        //    else
        //    {
        //        if (oBERetorno.Retorno != "-1")
        //        {
        //            msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
        //        }
        //        else
        //        {
        //            RegistrarLogSistema("btnGuardarLote_Click()", oBERetorno.ErrorMensaje, true);
        //        }

        //    }
        //}
        #endregion

        #region Proveedor
        protected void lnkAgregarProveedor_Click(object sender, EventArgs e)
        { 
            LimpiarFormulario();
            txtNumeroDocumento.Focus(); 
            registrarScript("funModalAbrirProve();");
        }

        private void LimpiarFormulario()
        {
            hdfIDProveedor.Value = "0";
            hdfRegIDUbigeo.Value = "";
            txtRegUbigeo.Text = "";
            txtNumeroDocumento.Text = String.Empty;
            txtPRazonSocial.Text = String.Empty;
            txtPNombreComercial.Text = String.Empty;
            txtPDireccion.Text = String.Empty;
            txtPUrbanizacion.Text = String.Empty;
            txtPCorreo.Text = String.Empty;
            txtPCelular.Text = String.Empty;
            ddlIDPTipoDocumento.SelectedIndex = -1;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            registrarScript("funModalCerrarProve();");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            StringBuilder validacion = new StringBuilder();
            if (txtPNroDocumento.Text.Length == 0) validacion.Append("<div>Ingrese Numero Documento.</div>");
            if (txtPRazonSocial.Text.Length == 0) validacion.Append("<div>Ingrese nombre o razon social.</div>");
            if (txtPNombreComercial.Text.Length == 0) validacion.Append("<div>Ingrese Nombre Comercial</div>");

            if (ddlIDPTipoDocumento.SelectedValue.Equals("3"))
            {
                if (txtPNroDocumento.Text.Trim().Length != 11 || !esDouble(txtPNroDocumento.Text.Trim())) validacion.Append("<div>Ingrese un Ruc válido.</div>");
            }
            else
            {
                if (txtPNroDocumento.Text.Length != 8 || !esDouble(txtPNroDocumento.Text.Trim())) validacion.Append("<div>Ingrese un número Documento válido.</div>");
            }

            if (validacion.Length > 0)
            {
                msgbox(TipoMsgBox.warning, validacion.ToString());
                return;
            }

            BEProveedor oBE = new BEProveedor();
            BLProveedor oBL = new BLProveedor();
            oBE.IDProveedor = Int32.Parse(hdfIDProveedor.Value);
            oBE.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
            oBE.IDTipoDocumento = Int32.Parse(ddlIDPTipoDocumento.SelectedValue);
            oBE.NumeroDocumento = txtPNroDocumento.Text.Trim();
            oBE.RazonSocial = txtPRazonSocial.Text.Trim();
            oBE.NombreComercial = txtPNombreComercial.Text.Trim();
            oBE.IDUbigeo = hdfRegIDUbigeo.Value;
            oBE.Direccion = txtPDireccion.Text;
            oBE.Urbanizacion = txtPUrbanizacion.Text;
            oBE.Correo = txtPCorreo.Text;
            oBE.Celular = txtPCelular.Text;
            oBE.Estado = true;
            oBE.IDUsuario = IDUsuario();
            BERetornoTran oBERetorno = new BERetornoTran();
            if (oBE.IDProveedor == 0)
            {
                oBERetorno = oBL.ProveedorGuardar(oBE);
            }
            else
            {
                oBERetorno = oBL.ProveedorActualizar(oBE);
            }

            if (oBERetorno.Retorno != "-1")
            {
                LimpiarFormulario();
                ListarProveedor();
                upLista.Update();
                msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
                registrarScript("funModalCerrarProve();");
            }
            else
            {
                RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
            }
        }

        #endregion


    }
}