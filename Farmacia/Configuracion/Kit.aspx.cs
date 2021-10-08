using Farmacia.App_Class;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Compras;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.Compras;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Collections;

using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class Kit : PageBase
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
				Listar();
			}
		}

		private void CargaInicial()
		{
			hdfIDSucursal.Value = IDSucursal().ToString();
			ArrayList aListaDetalle = new ArrayList();
			if (Session["aListaDetalle"] == null) //Trabajando en Memoria
			{
				Session["aListaDetalle"] = aListaDetalle;
			}
			else
			{
				aListaDetalle = (ArrayList)(Session["aListaDetalle"]);
			}
			LlenargvLista(aListaDetalle);						
			CargarDDL(ddlIDReProducto, new BLProducto().ListarPorSucursal(IDSucursal()), "IDProducto", "Nombre", true, Constantes.SELECCIONAR);
			CargarDDL(ddlIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", false);
			CargarDDL(ddlIDProducto, new BLProducto().ListarPorSucursal(IDSucursal()), "IDProducto", "Nombre", true, Constantes.SELECCIONAR);
			//CalcularTotales();
			NumeroKitSeleccionar();
		}

		protected void NumeroKitSeleccionar()
		{
			lblNumeroKit.Text = new BLKit().KitNumeroSeleccionar();
		}		

		#endregion

		#region Listar 
		private void Listar()
		{
			BLKit oBL = new BLKit();
			gvLista.DataSource = oBL.KitListar(IDSucursal(), txtCOMFiltro.Text.Trim());
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
			BERetornoTran oBERetorno = new BERetornoTran();
            hdfIDKit.Value = e.CommandArgument.ToString();

            switch (e.CommandName)
			{

                case "Editar":

                    pnKitRegistro.Enabled = false;
                    BEKit oBE = new BLKit().KitSeleccionar(Int32.Parse(hdfIDKit.Value), IDSucursal());

                    int numero = int.Parse(hdfIDKit.Value);                    
                    lblNumeroKit.Text = numero.ToString("D8");

                    if (oBE != null)
                    {
                        ddlIDSucursal.SelectedValue = oBE.IDSucursal.ToString();
                        ddlIDProducto.SelectedValue = oBE.IDProducto.ToString();
                        ddlIDSucursal.Enabled = false;
                        ddlIDProducto.Enabled = false;
                        txtUnidadMedida.Text = oBE.UnidadMedida;
                        hdfIDUnidadMedidaVenta.Value = oBE.IDUnidadMedida.ToString();
                        txtGlosa.Text = oBE.Glosa.Trim();
                        KitDetalleListar();



                    }

                    upRegistroCompra.Update();
                    registrarScript("ActivarTabxId('tab2');");

                    break;
							
				case "Eliminar":

                    oBERetorno = new BLKit().KitEliminar(Int32.Parse(hdfIDKit.Value));

                    if (oBERetorno.Retorno == "1")
                    {
                        msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
                        Listar();
                    }
                    else if (oBERetorno.Retorno == "2") {
                        msgbox(TipoMsgBox.warning, "El Kit no se puede eliminar porque tiene stock");
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

		private void KitDetalleListar()
		{
			ArrayList aListaDetalle = new ArrayList();
			aListaDetalle = (ArrayList)new BLKitDetalle().KitDetalleListar(Int32.Parse(hdfIDKit.Value), IDSucursal());
			LlenargvLista(aListaDetalle);
			Session["aListaDetalle"] = aListaDetalle;
			gvDetalleLista.SelectedIndex = -1;
			upRegistroCompra.Update();
		}

		private void ListarCompraDetalleDocumentoModifica()
		{
			//ArrayList aListaDetalle = new ArrayList();
			//aListaDetalle = (ArrayList)new BLCompras().ComprasDetalleListar(Int32.Parse(hdfIDCompraDocumentoModifica.Value));
			//LlenargvLista(aListaDetalle);
			//Session["aListaDetalle"] = aListaDetalle;
			//gvDetalleLista.SelectedIndex = -1;
			//upRegistroCompra.Update();
		}



		protected void KitSeleccionar()
		{
			//BECompras oBE = new BLCompras().ComprasSeleccionar(Int32.Parse(hdfIDCompra.Value));
			//lblNumeroCompra.Text = oBE.NumeroCompra;
			//ddlIDAlmacen.SelectedValue = oBE.IDAlmacen.ToString();
			//ddlRegIDTipoComprobante.SelectedValue = oBE.IDTipoDocumento.ToString();
			//txtSerieDocumento.Text = oBE.SerieDocumento;
			//txtNumeroDocumento.Text = oBE.NumeroDocumento;
			//txtFechaEmision.Text = oBE.FechaCompra.ToShortDateString();
			//txtFechaVencimiento.Text = oBE.FechaVencimiento.ToShortDateString();
			//txtFechaRegistro.Text = oBE.FechaRegistro.ToShortDateString();
			//ddlRegMoneda.SelectedValue = oBE.IDMoneda;
			//ddlRegFormaPago.SelectedValue = oBE.IDFormaPago.ToString();
			//hdfRegIDProveedor.Value = oBE.IDProveedor.ToString();
			//ddlRegIDTipoDocumento.SelectedValue = oBE.ProveedorIDTipoDocumento.ToString();
			//txtRegNumeroDocumento.Text = oBE.ProveedorNumeroDocumento;
			//txtRegProveedor.Text = oBE.ProveedorRazonSocial;
			//txtRegProveedorNumeroDocumento.Text = oBE.ProveedorNumeroDocumento;
			//txtCuenta.Text = oBE.Cuenta;
			//txtCuentaCaja.Text = oBE.CuentaCaja;
			//txtGlosa.Text = oBE.Glosa;
			//lblSubTotal.Text = oBE.SubTotal.ToString();
			//hdfSubTotal.Value = oBE.SubTotal.ToString();
			//lblTotalIgv.Text = oBE.TotalIGV.ToString();
			//hdfTotalIGV.Value = oBE.TotalIGV.ToString();
			//lblTotal.Text = oBE.TotalCompra.ToString();
			//hdfTotalCompra.Value = oBE.TotalCompra.ToString();

			//hdfTipoDocumentoReferencia.Value = oBE.TipoDocumentoReferencia;
			//txtDocumentoReferencia.Text = oBE.SerieNumeroDocumentoReferencia;

			//if (oBE.FechaEmisionDocumentoReferencia == DateTime.Parse("1900-01-01"))
			//{
			//	txtFechaDocumentoReferencia.Text = "";
			//}
			//else
			//{
			//	txtFechaDocumentoReferencia.Text = oBE.FechaEmisionDocumentoReferencia.ToShortDateString();
			//}

		}

		#endregion

		#region RegistroKit

		protected void rblTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
		{
			NumeroKitSeleccionar();
		}			

		protected void lnkNuevoKit_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();
		}		

		protected void lnkGuardarKit_Click(object sender, EventArgs e)
		{
            StringBuilder pValidaciones = new StringBuilder();
            if (ddlIDSucursal.SelectedValue == "0") pValidaciones.Append("<div>Seleccione una Sucursal</div>");
            if (ddlIDProducto.SelectedValue == "0") pValidaciones.Append("<div>Seleccione un Producto</div>");

            ArrayList aListaDetalleCompra = new ArrayList();
            if (Session["aListaDetalle"] == null) //Trabajando en Memoria
            {
                Session["aListaDetalle"] = aListaDetalleCompra;
            }
            else
            {
                aListaDetalleCompra = (ArrayList)(Session["aListaDetalle"]);
            }
            if (aListaDetalleCompra.Count == 0) pValidaciones.Append("<div>El detalle no puede estar vacío!.</div>");

            if (pValidaciones.Length > 0)
            {
                msgbox(TipoMsgBox.warning, pValidaciones.ToString());
                return;
            }

            BLKit oBLKit = new BLKit();
            BECompras oBECompras = new BECompras();
            ArrayList ListaBEKitDetalle = new ArrayList();

			BEKit oBEKit = new BEKit();

            oBEKit.IDKit = Int32.Parse(hdfIDKit.Value);
			oBEKit.IDSucursal = IDSucursal();
			oBEKit.IDProducto = Int32.Parse(ddlIDProducto.SelectedValue);
			oBEKit.NombreProducto = ddlIDProducto.SelectedItem.Text;
			oBEKit.IDUnidadMedida = Int32.Parse(hdfIDUnidadMedidaVenta.Value);
			oBEKit.Glosa = txtGlosa.Text;
			oBEKit.Cantidad = 0;
			oBEKit.Estado = true;			
            oBEKit.IDUsuario = IDUsuario();
            SetListaDetalle(ref ListaBEKitDetalle);

            BERetornoTran oBERetorno = new BERetornoTran();

            if (hdfIDKit.Value == "0")
            {
                oBERetorno = oBLKit.GuardarKits(oBEKit, ListaBEKitDetalle);
            }
            else
            {
                //oBERetorno = oBLKit. Compras Actualizar(oBECompras, ListaBEComprasDetalle);
            }

            if (oBERetorno.Retorno == "1")
            {
                msgbox(TipoMsgBox.confirmation, "Kit se grabo correctamente");
                Listar();
                Session["aListaDetalle"] = null;
                LlenargvLista((ArrayList)Session["aListaDetalle"]);
                LimpiarFormulario();
                //CalcularTotales();
                NumeroKitSeleccionar();
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

		public void LlenargvLista(ArrayList DT)
		{
			gvDetalleLista.DataSource = DT;
			gvDetalleLista.DataBind();
			upRegistroCompra.Update();
		}

		private void SetListaDetalle(ref ArrayList ListaBEComprasDetalle)
		{
			ArrayList aListaBEKitDetalle = new ArrayList();
			aListaBEKitDetalle = (ArrayList)Session["aListaDetalle"];
			if (aListaBEKitDetalle != null)
			{
				Int32 p = 0;

				foreach (BEKitDetalle oBE in aListaBEKitDetalle)
				{
					p += 1;
					oBE.IDKitDetalle = 0;
					oBE.IDKit = 0;
					oBE.IDProducto = oBE.IDProducto;
					oBE.NombreProducto = oBE.NombreProducto;					
					oBE.IDUnidadMedida = oBE.IDUnidadMedida;
					oBE.CantidadReg = oBE.CantidadReg;
					oBE.CantidadArmado = oBE.CantidadArmado;
					oBE.CantidadDisponible = oBE.CantidadDisponible;
					oBE.IDUsuario = IDUsuario();
					ListaBEComprasDetalle.Add(oBE);
				}
			}
		}

		protected void gvDetalleLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			ArrayList aListaDetalle = new ArrayList();
			aListaDetalle = (ArrayList)(Session["aListaDetalle"]);
			aListaDetalle.RemoveAt(e.RowIndex);
			LlenargvLista(aListaDetalle);
			gvDetalleLista.SelectedIndex = -1;
			Session["aListaDetalle"] = aListaDetalle;
			//CalcularTotales();
		}

		protected void gvDetalleLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			hdfIDKitDetalle.Value = gvDetalleLista.SelectedIndex.ToString();
			hdfIDProducto.Value = gvDetalleLista.SelectedDataKey["IDProducto"].ToString();
			BEProducto oBE = new BLProducto().ProductoSeleccionar(Int32.Parse(hdfIDProducto.Value), IDSucursal());
			GridViewRow row = gvDetalleLista.SelectedRow;
			hdfIDUnidadMedida.Value = oBE.IDUnidadMedida.ToString();
			ddlIDReProducto.SelectedValue = oBE.IDProducto.ToString();
			//txtRegProducto.Text = oBE.CodigoProducto + "-" + oBE.Nombre;
			txtRegStock.Text = oBE.Stock.ToString();
			txtRegCantidad.Text = ((Label)row.FindControl("lblCantidad")).Text;
			//txtRegPrecioCompra.Text = ((Label)row.FindControl("lblPrecioUnitario")).Text;

			registrarScript("funModalProductoAbrir();");
			upRegistroProducto.Update();

		}

		private void LimpiarFormulario()
		{
			NumeroKitSeleccionar();
			hdfIDKit.Value = "0";
			hdfIDKitDetalle.Value = "-1";
			Session["aListaDetalle"] = null;
			LlenargvLista((ArrayList)Session["aListaDetalle"]);
            ddlIDSucursal.Enabled = true;
            ddlIDProducto.Enabled = true;
			ddlIDSucursal.SelectedIndex = -1;
            ddlIDProducto.SelectedIndex = -1;           
            txtGlosa.Text = "";
            txtUnidadMedida.Text = "";
            hdfIDUnidadMedida.Value = "0";            
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
			

			if (pValidaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, pValidaciones.ToString());
				return;
			}

			ArrayList aListaDetalle = new ArrayList();

			aListaDetalle = (ArrayList)(Session["aListaDetalle"]);
			Int32 pID = Int32.Parse(hdfIDKitDetalle.Value);
			Int32 pIDProducto = Int32.Parse(ddlIDReProducto.SelectedValue);
			String pNombreProducto = ddlIDReProducto.SelectedItem.Text.Trim();
			BEProducto oBE = new BLProducto().ProductoSeleccionar(pIDProducto, IDSucursal());

			BEKitDetalle oBEKitDet = new BEKitDetalle();

			oBEKitDet.IDProducto = pIDProducto;
			oBEKitDet.NombreProducto = pNombreProducto;
			oBEKitDet.IDUnidadMedida = oBE.IDUnidadMedidaVenta;
			oBEKitDet.CantidadReg = Convert.ToDecimal(txtRegCantidad.Text);
			oBEKitDet.CantidadArmado = 0;
			oBEKitDet.CantidadDisponible = 0;

			if (pID == -1)
			{
				if (ContarIngresado(aListaDetalle, Int32.Parse(hdfIDProducto.Value)) > 0)
				{
					msgbox(TipoMsgBox.information, "Ya ingresó ese producto.");
					return;
				}
			}

			if (aListaDetalle == null)
			{
				aListaDetalle = new ArrayList();
			}
			if (hdfIDKitDetalle.Value == "-1")
			{
				aListaDetalle.Add(oBEKitDet);
			}
			else
			{
				aListaDetalle[pID] = oBEKitDet;
			}

			LlenargvLista(aListaDetalle);
			Session["aListaDetalle"] = aListaDetalle;
			//CalcularTotales();
			gvDetalleLista.SelectedIndex = -1;
			LimpiarProducto();
			msgbox(TipoMsgBox.confirmation, "Se agregó satisfactoriamente.");
		}

		private void LimpiarProducto()
		{
			hdfIDKitDetalle.Value = "-1";
			hdfIDProducto.Value = "0";
			ddlIDReProducto.SelectedIndex = -1;
			//txtRegProducto.Text = "";
			txtRegCantidad.Text = "1";
			txtRegUnidMedida.Text = "";			
			txtRegStock.Text = "";
			hdfIDUnidadMedida.Value = "0";
			upRegistroProducto.Update();
		}

		protected void ddlIDReProducto_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32 pIDProducto = Int32.Parse(ddlIDReProducto.SelectedValue);
			BEProducto oBE = new BLProducto().ProductoSeleccionar(pIDProducto, IDSucursal());
			txtRegStock.Text = oBE.Stock.ToString();
			hdfIDUnidadMedida.Value = oBE.IDUnidadMedidaVenta.ToString();
			txtRegUnidMedida.Text = oBE.UnidadMedidaVenta;			
			upRegistroProducto.Update();
		}		

		#endregion

		
		#region Funciones

		public Double ContarIngresado(ArrayList aListaDetalle, Int32 pID)
		{
			Int32 p = 0;
			if (aListaDetalle == null)
			{
				p = 0;
			}
			else
			{				
				foreach (BEKitDetalle oBE in aListaDetalle)
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
		

        protected void ddlIDProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
			Int32 pIDProducto = Int32.Parse(ddlIDProducto.SelectedValue);
			BEProducto oBE = new BLProducto().ProductoSeleccionar(pIDProducto, IDSucursal());
			txtUnidadMedida.Text = oBE.UnidadMedidaVenta;
			hdfIDUnidadMedidaVenta.Value = oBE.IDUnidadMedidaVenta.ToString();
			upRegistroCompra.Update();
		}



        #endregion

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
        //#endregion
    }
}