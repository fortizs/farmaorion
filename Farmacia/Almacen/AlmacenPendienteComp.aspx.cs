using Farmacia.App_Class;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Compras;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.Compras;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Almacen
{
	public partial class AlmacenPendienteComp : PageBase
	{
		#region Load
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				txtFechaInicio.Text = DateTime.Today.AddDays(-10).ToShortDateString();
				txtFechaFin.Text = DateTime.Today.ToShortDateString();

                if (EsAdmin())
                {
                    CargarDDL(ddlIDAlmacen, new BLAlmacen().AlmacenListar(0), "IDAlmacen", "Nombre", true, Constantes.TODOS);
                    CargarDDL(ddlIDAlmacenIngreso, new BLAlmacen().AlmacenListar(0), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR);
                }
                else
                {
                    CargarDDL(ddlIDAlmacen, new BLAlmacen().AlmacenListar(IDSucursal()), "IDAlmacen", "Nombre", true, Constantes.TODOS);
                    CargarDDL(ddlIDAlmacenIngreso, new BLAlmacen().AlmacenListar(IDSucursal()), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR);
                }
                
				CargarDDL(ddlIDEstadoAlmacen, new BLEstado().EstadoListar("EAL"), "Codigo", "Nombre", true, Constantes.TODOS);

				
				CargarDDL(ddlIDTransaccion, new BLTransaccion().TransaccionListar("I"), "IDTransaccion", "Nombre", true, Constantes.SELECCIONAR);
                if (Session["tokenIngreso"] == null) //Trabajando en Memoria
                {
                    GenerarToken();
                }
                ListarCompras();
			}
		}

        protected void GenerarToken()
        {
            Session["tokenIngreso"] = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
        #endregion

        #region Compras
        private void ListarCompras()
		{
			BLCompras oBL = new BLCompras();
			gvLista.DataSource = oBL.ComprasListar(IDSucursal(), Int32.Parse(ddlIDAlmacen.SelectedValue), txtCOMFiltro.Text.Trim(), txtFechaInicio.Text, txtFechaFin.Text, 2, Int32.Parse(ddlIDEstadoAlmacen.SelectedValue),"C");
			gvLista.DataBind();
			upLista.Update();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarCompras();
		}

		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32 pID = Int32.Parse(gvLista.SelectedDataKey["IDCompras"].ToString());
			BLCompras oBL = new BLCompras();
			BECompras oBE = oBL.ComprasSeleccionar(pID); 
			txtNumeroOrdenCompra.Text = oBE.NumeroCompra;
			txtProveedor.Text = oBE.ProveedorRazonSocial;
			txtFechaEmision.Text = oBE.FechaCompra.ToShortDateString();
			ddlIDTransaccion.SelectedValue = Constantes.ID_Tran_I_Productos_Nacional.ToString(); 			
			hdfIDCompra.Value = oBE.IDCompras.ToString();
			DetalleCompraListar(pID); 
			btnGuardar.Enabled = true;
			upIngresoAlmacenCompra.Update();
			registrarScript("AbrirModal('ModalIngresoCompra');");
		}

		private void DetalleCompraListar(Int32 id)
		{
			BLCompras oBL = new BLCompras();
            IList oCom = oBL.ComprasDetalleListar(id);
            gvDetalleLista.DataSource = oCom;
            gvDetalleLista.DataBind();
		}
		 
		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			ListarCompras();
		}

		#endregion

		#region Ingreso a Almacen

		protected void btnGuardar_Click(object sender, EventArgs e)
		{ 
            StringBuilder pValidaciones = new StringBuilder();
            if (ddlIDAlmacenIngreso.SelectedValue == "0" || ddlIDAlmacenIngreso.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Almacen</div>");

            Decimal SaldoCantidad = 0;

            foreach (GridViewRow row in gvDetalleLista.Rows)
            {
                SaldoCantidad += Convert.ToDecimal(((Label)row.Cells[6].FindControl("lblSaldoCantidad")).Text);                
            }

            if (SaldoCantidad != 0) pValidaciones.Append("<div>El Saldo Cantidad debe estar en cero</div>");

            if (pValidaciones.Length > 0)
            {
                msgbox(TipoMsgBox.warning, pValidaciones.ToString());
                return;
            } 

            BEMovimiento oBE = new BEMovimiento();
			BLMovimiento oBL = new BLMovimiento();

			oBE.IDMovimiento = 0;
			oBE.IDAlmacenOrigen = Int32.Parse(ddlIDAlmacenIngreso.SelectedValue);  
			oBE.IDAlmacenDestino = Int32.Parse(ddlIDAlmacenIngreso.SelectedValue);
			oBE.IDEntidad = Int32.Parse(hdfIDCompra.Value);
			oBE.Entidad = "COMPRAS";
			oBE.IDTransaccion = Constantes.ID_Tran_I_Productos_Nacional;
			oBE.Observacion = txtGlosa.Text.Trim();
			oBE.IDSucursal = IDSucursal();
			oBE.IDEmpresa = IDEmpresa();
			oBE.IDUsuario = IDUsuario();
			oBE.Fecha = DateTime.Today;
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = oBL.MovimientoAlmacenGuardar(oBE);

			if (oBERetorno.Retorno == "1")
			{
                
                BLCompras oBLCompra = new BLCompras();
                IList oCom = oBLCompra.ComprasDetalleListar(Int32.Parse(hdfIDCompra.Value));

                foreach (BEComprasDetalle dCompra in oCom) {

                    BEMovimientoDetalle oBEDetalle = new BEMovimientoDetalle();
                    oBEDetalle.IDMovimiento = Int32.Parse(oBERetorno.Retorno2);
                    oBEDetalle.IDProducto = dCompra.IDProducto;
                    oBEDetalle.Token = Session["tokenIngreso"].ToString().Trim();
                    oBEDetalle.IDUnidadMedida = dCompra.IDUnidadMedida;
                    oBEDetalle.Cantidad = dCompra.Cantidad;
                    oBEDetalle.FechaMovimiento = DateTime.Today;
                    oBEDetalle.IDUsuario = IDUsuario();

                    BERetornoTran oBERetornoMovimientoDetalle = oBL.MovimientoDetalleAlmacenGuardar(oBEDetalle);
                    if (oBERetornoMovimientoDetalle.Retorno != "1") {                        
                        msgbox(TipoMsgBox.warning, "Sistema", oBERetornoMovimientoDetalle.ErrorMensaje);
                    }
                }

                MovimientoDetalleLoteTempEliminar(Session["tokenIngreso"].ToString().Trim());
                LimpiarFormulario();
				ListarCompras(); 
				btnGuardar.Enabled = false;
				msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				registrarScript("CerrarModal('ModalIngresoCompra');");
			}
			else
			{
				if (oBERetorno.Retorno != "-1")
				{
                    RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);                    
				}
				else {
                    msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
                } 
			}
		}

        private void MovimientoDetalleLoteTempEliminar(string Token) {
            BLMovimientoDetalleLote oBL = new BLMovimientoDetalleLote();
            BERetornoTran oBE = oBL.MovimientoDetalleLoteTemporalEliminar(Token);
            if (oBE.Retorno != "1") {
                msgbox(TipoMsgBox.warning, "Sistema", oBE.ErrorMensaje);
                RegistrarLogSistema("btnGuardar_Click()", oBE.ErrorMensaje, true);
            }
        }
		 
		private void LimpiarFormulario()
		{
			hdfIDCompra.Value = "0";
			txtNumeroOrdenCompra.Text = String.Empty;
			txtProveedor.Text = String.Empty;
			txtFechaEmision.Text = String.Empty;
			txtFechaIngresoAlmacen.Text = String.Empty;
			ddlIDTransaccion.SelectedIndex = -1;
			ddlIDAlmacenIngreso.SelectedIndex = -1;
			upIngresoAlmacenCompra.Update();
		}


        #endregion


        #region Lote

        //Abrir Lista de Lotes
        protected void gvDetalleLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            BELote oBE = new BELote();
            hdfIDProductoSeleccionado.Value =  gvDetalleLista.SelectedIndex.ToString();
            hdIDProductoLote.Value = gvDetalleLista.SelectedDataKey["IDProducto"].ToString();
            hdfTotalLote.Value = gvDetalleLista.SelectedDataKey["CantidadVenta"].ToString();
            MovimientoDetalleLoteListar();
            registrarScript("funModalListaLoteAbrir();");

        }

        //Lista de Lotes
        private void MovimientoDetalleLoteListar()
        {            

            BLMovimientoDetalleLote oBLLote = new BLMovimientoDetalleLote();
            BEMovimientoDetalleLote oBE = new BEMovimientoDetalleLote();
            oBE.IDMovimiento = Int32.Parse(hdfIDMovimiento.Value);
            oBE.IDMovimientoDetalle = 0;
            oBE.IDSucursal = IDSucursal();
            oBE.IDProducto = Int32.Parse(hdIDProductoLote.Value);
            oBE.Token = Session["tokenIngreso"].ToString().Trim();
            gvLoteProducto.DataSource = oBLLote.MovimientoDetalleLoteListar(oBE);
            gvLoteProducto.DataBind();
            upLoteListar.Update();
        }

        //private void ListarLote()
        //{          

        //    BLLote oBLLote = new BLLote();
        //    gvLoteProducto.DataSource = oBLLote.LotexTokenListar(Int32.Parse(hdIDProductoLote.Value), hdfToken.Value.Trim());
        //    gvLoteProducto.DataBind();
        //    upRegistroProducto.Update();
        //    CalcularCantidadLote();
        //}

        //private void CalcularCantidadLote() {
        //    Decimal CantidadLote = 0;
        //    Decimal Saldo = 0;
        //    foreach (GridViewRow row in gvLoteProducto.Rows)
        //    {
        //        CantidadLote += Convert.ToDecimal((((Label)row.Cells[2].FindControl("lblCantidadLote")).Text));
        //    }
        //    Saldo = Convert.ToDecimal(hdfTotalLote.Value) - CantidadLote;
        //    txtLTotal.Text = Saldo.ToString("N");
        //    ((Label)this.gvDetalleLista.Rows[Int32.Parse(hdfIDProductoSeleccionado.Value)].FindControl("lblSaldoCantidad")).Text = Saldo.ToString("N");
        //    upIngresoAlmacenCompra.Update();
        //    //ActualizarSaldoCantidad(Int32.Parse(hdfIDProductoSeleccionado.Value), Saldo);
        //}      

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
            //txtLCantidad.Text = "";
            //txtLCantidadAnterior.Text = "0";
            UpRegistroLote.Update();
        }

        

        #endregion

        protected void ActualizarSaldoCantidad(Int32 Posicion, Decimal Saldo) {
            Int32 i = 0;
            foreach (GridViewRow row in gvDetalleLista.Rows)
            {
                if (i == Posicion)
                {                    
                    ((Label)row.Cells[6].FindControl("lblSaldoCantidad")).Text = Saldo.ToString("N");
                }
                i++;
            }
            upIngresoAlmacenCompra.Update();
        }


        protected void btnGuardarLote_Click(object sender, EventArgs e)
        {

            try
            {
                StringBuilder validacion = new StringBuilder();
                if (txtLote.Text.Length == 0) validacion.Append("<div>Ingrese Lote.</div>");                
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
                oBE.Token = Session["tokenIngreso"].ToString().Trim();
                oBE.IDLote = Int32.Parse(hdfIDLote.Value);
                oBE.IDProducto = Int32.Parse(hdIDProductoLote.Value);
                oBE.IDSucursal = IDSucursal();
                oBE.Lote = txtLote.Text.Trim();
                oBE.CantidadLote = 0;
                oBE.FechaFabricacion = DateTime.Parse(txtLFechaFabricacion.Text);
                oBE.FechaVencimiento = DateTime.Parse(txtLFechaVencimiento.Text);
                oBE.Estado = true; 
                oBE.IDUsuario = IDUsuario();
                BERetornoTran oBERetorno = new BERetornoTran();
                oBERetorno = oBL.LoteGuardar(oBE);

                if (oBERetorno.Retorno == "1")
                {
                    ////Guardar Lote Temporal
                    //Int32 IDLote = Int32.Parse(oBERetorno.Retorno2);
                    //BERetornoTran oBERetornoTemp = MovimientoDetalleLoteTemporalGuardar(Int32.Parse(hdIDProductoLote.Value), IDLote, Decimal.Parse(txtLCantidad.Text.Trim()));

                    //if (oBERetornoTemp.Retorno == "1") {

                        msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
                        registrarScript("funModalLoteCerrar();");
                        LimpiarLote();
                        MovimientoDetalleLoteListar();
                        upIngresoAlmacenCompra.Update();
                        upLoteListar.Update();
                    //}

                    //ActualizarSaldoCantidad(Int32.Parse(hdfIDProductoSeleccionado.Value));

                    //int i = 0;
                    //Decimal SaldoCantidad = 0;
                    //Decimal SaldoCantidadActualizado = 0;
                    //foreach (GridViewRow row in gvDetalleLista.Rows)
                    //{
                    //    if (i == Convert.ToInt32(hdfIDProductoSeleccionado.Value)) {
                    //        SaldoCantidad = Convert.ToDecimal(((Label)row.Cells[6].FindControl("lblSaldoCantidad")).Text);
                    //        SaldoCantidadActualizado = SaldoCantidad - oBE.CantidadLote;
                    //        ((Label)row.Cells[6].FindControl("lblSaldoCantidad")).Text = SaldoCantidadActualizado.ToString("N");
                    //    }                        
                    //    i++;
                    //}

                    
                    //Decimal CalcularTotal = Convert.ToDecimal(hdfTotalLote.Value) - oBE.CantidadLote;
                    //hdfTotalLote.Value = CalcularTotal.ToString();
                    //txtLTotal.Text= CalcularTotal.ToString();
                    
                }
                else
                {                    
                        msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
                        RegistrarLogSistema("btnGuardarLote_Click()", oBERetorno.ErrorMensaje, true);
                }

            }
            catch (Exception ex) {

                RegistrarLogSistema("btnGuardarLote_Click()", ex.Message, true);
            }
        }

        protected BERetornoTran MovimientoDetalleLoteTemporalGuardar(Int32 IDProducto, Int32 IDLote, Decimal Cantidad)
        {

            BEMovimientoDetalleLote BEMovimientoLote = new BEMovimientoDetalleLote();
            BEMovimientoLote.IDProducto = IDProducto;
            BEMovimientoLote.IDLote = IDLote;
            BEMovimientoLote.Cantidad = Cantidad;
            BEMovimientoLote.Estado = false;
            BEMovimientoLote.IDUsuario = IDUsuario();
            BEMovimientoLote.Token = Session["tokenIngreso"].ToString().Trim();
            BLMovimientoDetalleLote oBL = new BLMovimientoDetalleLote();
            BERetornoTran oBERetorno = oBL.MovimientoDetalleLoteTemporalGuardar(BEMovimientoLote);
            return oBERetorno;
        }

       


        private void SeleccionarLote()
        {            
            BELote oBE = new BLLote().LoteSeleccionar(Int32.Parse(hdfIDLote.Value));
            txtLote.Text = oBE.Lote;
            //txtLCantidad.Text = oBE.CantidadLote.ToString();
            //txtLCantidadAnterior.Text = oBE.CantidadLote.ToString();
            txtLFechaVencimiento.Text = oBE.FechaVencimiento.ToShortDateString();
            txtLFechaFabricacion.Text = oBE.FechaFabricacion.ToShortDateString();
            UpRegistroLote.Update();
            registrarScript("funModalLoteAbrir();");
        }

        protected void gvLoteProducto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                hdfIDLote.Value = e.CommandArgument.ToString();

                BERetornoTran oBERetorno = new BERetornoTran();
                switch (e.CommandName)
                {
                    case "Seleccionar":
                        //int i = gvDetalleLista.SelectedIndex;
                        //GridViewRow row = gvLoteProducto.SelectedRow;
                        ////int id = Convert.ToInt32(gvLoteProducto.DataKeys[0].Values["IDLote"]);
                        //String IDLote = gvLoteProducto.SelectedDataKey["IDLote"].ToString();
                        //String Lote = gvLoteProducto.SelectedDataKey["Lote"].ToString();
                        //((Label)this.gvDetalleLista.Rows[Int32.Parse(hdfIDProductoSeleccionado.Value)].FindControl("lblLote")).Text = Lote;
                        //registrarScript("CerrarModal('DatosAgregarLoteProducto');");
                        //String valorlote = ((Label)this.gvDetalleLista.Rows[Int32.Parse(hdfIDProductoSeleccionado.Value)].FindControl("lblLote")).Text;
                        //upIngresoAlmacenCompra.Update();
                        break;
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
                            if (oBERetorno.Retorno == "-1")
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

        //protected void lnkAplicarLote_Click(object sender, EventArgs e)
        //{
        //    StringBuilder validacion = new StringBuilder();
        //    if (Convert.ToDecimal(txtLTotal.Text) != 0) validacion.Append("<div>El saldo debe ser igual a cero.</div>");

        //    if (validacion.Length > 0)
        //    {
        //        msgbox(TipoMsgBox.warning, validacion.ToString());
        //        return;
        //    }

        //    ActualizarSaldoCantidad(Int32.Parse(hdfIDProductoSeleccionado.Value), Convert.ToDecimal(txtLTotal.Text));

        //    String NombreLote = "";            

        //    int i = 0;

        //    foreach (GridViewRow row in gvLoteProducto.Rows)
        //    {
        //        if (i == 0) {
        //            NombreLote += (((Label)row.Cells[1].FindControl("lblNombreLote")).Text);
        //        }else
        //        {
        //            NombreLote += ","+(((Label)row.Cells[1].FindControl("lblNombreLote")).Text);
        //        }
        //        i++;
        //    }
        //    ((Label)this.gvDetalleLista.Rows[Int32.Parse(hdfIDProductoSeleccionado.Value)].FindControl("lblLote")).Text = NombreLote;
        //    upIngresoAlmacenCompra.Update();
        //    registrarScript("funModalProductoCerrar();");
        //}

        protected void lnkAplicarLote_Click(object sender, EventArgs e)
        {
            

            Decimal Cantidad = 0;
            Int32 i = 0;
            String NombreLote = "";

            foreach (GridViewRow row in gvLoteProducto.Rows)
            {
                Decimal CantidadLote = 0;
                

                if (i == 0)
                {
                    NombreLote += (((Label)row.Cells[0].FindControl("lblNombreLote")).Text);
                }
                else
                {
                    NombreLote += "," + (((Label)row.Cells[0].FindControl("lblNombreLote")).Text);
                }

                if ((((TextBox)row.Cells[2].FindControl("txtCantidadLote")).Text) != "")
                {
                    CantidadLote = Convert.ToDecimal(((TextBox)row.Cells[2].FindControl("txtCantidadLote")).Text);
                }

                //Registrar en NotaAjusteDetalleLoteTemporal
                BEMovimientoDetalleLote BEMovimientoLote = new BEMovimientoDetalleLote();
                BEMovimientoLote.IDProducto = Int32.Parse(hdIDProductoLote.Value);
                BEMovimientoLote.IDLote = Int32.Parse(((Label)row.Cells[0].FindControl("lblIDLote")).Text);
                BEMovimientoLote.Cantidad = CantidadLote;
                BEMovimientoLote.Estado = false;
                BEMovimientoLote.IDUsuario = IDUsuario();
                BEMovimientoLote.Token = Session["tokenIngreso"].ToString().Trim();
                BLMovimientoDetalleLote oBL = new BLMovimientoDetalleLote();
                BERetornoTran oBERetorno = oBL.MovimientoDetalleLoteTemporalGuardar(BEMovimientoLote);
                if (oBERetorno.Retorno != "1")
                {
                    msgbox(TipoMsgBox.confirmation, "No se pudo registrar las cantidades del lote");
                }
                Cantidad += CantidadLote;
                i++;
            }
            Decimal Saldo = Convert.ToDecimal(hdfTotalLote.Value) - Cantidad;
            ActualizarSaldoCantidad(Int32.Parse(hdfIDProductoSeleccionado.Value), Saldo);            
            registrarScript("funModalListaLoteCerrar();");
        }

        protected void btnCancelarLote_Click(object sender, EventArgs e)
        {
            registrarScript("funModalLoteCerrar();");
        }
    }
}