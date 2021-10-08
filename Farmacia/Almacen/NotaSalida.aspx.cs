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
    public partial class NotaSalida : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarEstadoSesion();
            if (!Page.IsPostBack)
            {
                ConfigPage();
                CargaInicial();
            }
        }

        #region Inicio

        private void CargaInicial()
        {
            hdfIDSucursal.Value = IDSucursal().ToString();

            ArrayList aListaAjuste = new ArrayList();
            if (Session["aListaAjuste"] == null) //Trabajando en Memoria
            {
                Session["aListaAjuste"] = aListaAjuste;
            }
            else
            {
                aListaAjuste = (ArrayList)(Session["aListaAjuste"]);
            }


            if (Session["token"] == null) //Trabajando en Memoria
            {
                GenerarToken();
            }


            txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaInicio.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");

            LlenargvListaAjuste(aListaAjuste);
            CargarDDL(ddlRegMoneda, new BLMoneda().MonedaListar(), "IDMoneda", "Nombre", false);

            CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS, 0);
            CargarDDL(ddlBIDAlmacen, new BLAlmacen().AlmacenListar(IDSucursal()), "IDAlmacen", "Nombre", true, Constantes.TODOS, 0);

            CargarDDL(ddlIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.SELECCIONAR, 0);
            CargarDDL(ddlIDAlmacen, new BLAlmacen().AlmacenListar(IDSucursal()), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR, 0);

            CargarDDL(ddlIDProducto, new BLProducto().ListarPorSucursal(IDSucursal()), "IDProducto", "Nombre", true, Constantes.SELECCIONAR);
            CargarDDL(ddlIDTransaccion, new BLTransaccion().TransaccionNotaSalidaListar("S"), "IDTransaccion", "Nombre", true);
            CalcularTotales();
            Listar();
        }

        protected void GenerarToken()
        {
            Session["token"] = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        protected void ddlIDSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDDL(ddlIDAlmacen, new BLAlmacen().AlmacenListar(Int32.Parse(ddlIDSucursal.SelectedValue)), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR, 0);
        }

        protected void ddlBIDSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDDL(ddlBIDAlmacen, new BLAlmacen().AlmacenListar(Int32.Parse(ddlBIDSucursal.SelectedValue)), "IDAlmacen", "Nombre", true, Constantes.TODOS, 0);
        }

        public void LlenargvListaAjuste(ArrayList DT)
        {
            gvDetalleLista.DataSource = DT;
            gvDetalleLista.DataBind();
            upRegistroAjuste.Update();
        }

        #endregion

        #region Funciones

        public Double ContarIngresado(ArrayList aListaAjuste, Int32 pID)
        {
            Int32 p = 0;
            if (aListaAjuste == null)
            {
                p = 0;
            }
            else
            {
                //Int32 i = 0;
                foreach (BEMovimientoDetalle oBE in aListaAjuste)
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

        #region Listar Nota Ajuste
        private void Listar()
        {
            BLNotaAlmacen oBL = new BLNotaAlmacen();
            BEMovimiento oBE = new BEMovimiento();
            oBE.FechaInicio = txtFechaInicio.Text;
            oBE.FechaFin = txtFechaFin.Text;
            gvLista.DataSource = oBL.NotaSalidaListar(Int32.Parse(ddlBIDSucursal.SelectedValue), Int32.Parse(ddlBIDAlmacen.SelectedValue), txtFechaInicio.Text.Trim(), txtFechaFin.Text.Trim());
            gvLista.DataBind();
            upLista.Update();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            Listar();
        }

        protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            hdfIDMovimiento.Value = e.CommandArgument.ToString();
            BERetornoTran oBERetorno = new BERetornoTran();

            switch (e.CommandName)
            {
                case "Editar":
                    MovimientoAjusteSeleccionar();
                    ListarAjusteDetalle();
                    upRegistroAjuste.Update();
                    registrarScript("ActivarTabxId('tab2');");
                    break;
            }
        }

        #endregion

        #region Registro Ajuste Movimiento



        protected void MovimientoAjusteSeleccionar()
        {
            BEMovimiento oBE = new BLNotaAjuste().NotaAjusteSeleccionar(Int32.Parse(hdfIDMovimiento.Value));
            ddlIDSucursal.SelectedValue = oBE.IDSucursal.ToString();
            CargarDDL(ddlIDAlmacen, new BLAlmacen().AlmacenListar(Int32.Parse(ddlIDSucursal.SelectedValue)), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR, 0);
            ddlIDAlmacen.SelectedValue = oBE.Almacen.IDAlmacen.ToString();
            txtFechaEmision.Text = oBE.Fecha.ToShortDateString();
            ddlIDTransaccion.SelectedValue = oBE.Transaccion.IDTransaccion.ToString();
            txtGlosa.Text = oBE.Observacion;
            lblSubTotal.Text = oBE.SubTotal.ToString();
            hdfSubTotal.Value = oBE.SubTotal.ToString();
            lblTotalIgv.Text = oBE.TotalIGV.ToString();
            hdfTotalIGV.Value = oBE.TotalIGV.ToString();
            lblTotal.Text = oBE.Total.ToString();
            hdfTotalCompra.Value = oBE.Total.ToString();
        }

        private void ListarAjusteDetalle()
        {
            ArrayList aListaDetalle = new ArrayList();
            aListaDetalle = (ArrayList)new BLNotaAjuste().NotaAjusteDetalleListar(Int32.Parse(hdfIDMovimiento.Value));
            LlenargvListaAjuste(aListaDetalle);
            Session["aListaAjuste"] = aListaDetalle;
            gvDetalleLista.SelectedIndex = -1;
            upRegistroAjuste.Update();
        }

        protected void gvDetalleLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalleLista.PageIndex = e.NewPageIndex;
            gvDetalleLista.SelectedIndex = -1;
            ListarAjusteDetalle();
        }

        private void LimpiarAjuste()
        {
            //NumeroCompraSeleccionar();
            hdfIDMovimiento.Value = "0";
            hdfIDMovimientoDetalle.Value = "-1";
            Session["aListaAjuste"] = null;
            Session["token"] = null;
            GenerarToken();
            LlenargvListaAjuste((ArrayList)Session["aListaAjuste"]);
            ddlIDAlmacen.SelectedIndex = -1;
            ddlIDSucursal.SelectedIndex = -1;

            ddlRegMoneda.SelectedIndex = -1;
            txtFechaEmision.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtGlosa.Text = "";
            lblSubTotal.Text = "0.00";
            lblTotalIgv.Text = "0.00";
            lblTotal.Text = "0.00";
            hdfSubTotal.Value = "0";
            hdfTotalIGV.Value = "0";
            hdfTotalCompra.Value = "0";
            upRegistroAjuste.Update();
        }

        protected void gvDetalleLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ArrayList aListaAjuste = new ArrayList();
            aListaAjuste = (ArrayList)(Session["aListaAjuste"]);
            aListaAjuste.RemoveAt(e.RowIndex);
            LlenargvListaAjuste(aListaAjuste);
            gvDetalleLista.SelectedIndex = -1;
            Session["aListaAjuste"] = aListaAjuste;
            CalcularTotales();
        }


        protected void gvDetalleLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            hdfIDMovimientoDetalle.Value = gvDetalleLista.SelectedIndex.ToString();
            hdfIDProducto.Value = gvDetalleLista.SelectedDataKey["IDProducto"].ToString();
            BEProducto oBE = new BLProducto().ProductoSeleccionar(Int32.Parse(hdfIDProducto.Value), IDSucursal());
            GridViewRow row = gvDetalleLista.SelectedRow;
            hdfIDUnidadMedida.Value = oBE.IDUnidadMedidaVenta.ToString();
            txtRegUnidadMedida.Text = oBE.UnidadMedidaVenta;
            ddlIDProducto.SelectedValue = oBE.IDProducto.ToString();
            ddlIDProducto.Enabled = false;
            txtRegPrecioUnitario.Text = oBE.PrecioVenta.ToString("N");
            //txtRegProducto.Text = oBE.CodigoProducto + "-" + oBE.Nombre;
            txtRegStock.Text = oBE.Stock.ToString();


            if (oBE.ControlaLote)
            {
                DivCantidadLote.Visible = true;
                DivCantidad.Visible = false;
                txtRegCantidadLote.Text = ((Label)row.FindControl("lblCantidad")).Text;
            }
            else
            {
                DivCantidadLote.Visible = false;
                DivCantidad.Visible = true;
                txtRegCantidad.Text = ((Label)row.FindControl("lblCantidad")).Text;
            }
            registrarScript("funModalProductoAbrir();");
            upRegistroProducto.Update();
        }

        protected void lnkGuardarAjuste_Click(object sender, EventArgs e)
        {

            StringBuilder pValidaciones = new StringBuilder();
            if (ddlIDAlmacen.SelectedValue == "0" || ddlIDAlmacen.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Almacen</div>");
            if (ddlIDTransaccion.SelectedValue == "0" || ddlIDTransaccion.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Transacción</div>");

            if (pValidaciones.Length > 0)
            {
                msgbox(TipoMsgBox.warning, pValidaciones.ToString());
                return;
            }

            BLNotaAjuste oBLMov = new BLNotaAjuste();
            BEMovimiento oBEMov = new BEMovimiento();
            ArrayList ListaBEAjusteDetalle = new ArrayList();
            oBEMov.IDMovimiento = Int32.Parse(hdfIDMovimiento.Value);
            oBEMov.IDAlmacenOrigen = Int32.Parse(ddlIDAlmacen.SelectedValue);
            oBEMov.IDAlmacenDestino = Int32.Parse(ddlIDAlmacen.SelectedValue);
            oBEMov.IDEntidad = 0;
            oBEMov.Entidad = "NOTA DE INGRESO";
            oBEMov.IDTransaccion = Int32.Parse(ddlIDTransaccion.SelectedValue);
            oBEMov.IDSucursal = Int32.Parse(ddlIDSucursal.SelectedValue);
            oBEMov.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
            oBEMov.Observacion = txtGlosa.Text;
            oBEMov.Fecha = DateTime.Parse(txtFechaEmision.Text);
            oBEMov.IDUsuario = IDUsuario();
            oBEMov.Token = Session["token"].ToString().Trim();
            SetListaBEAjusteDetalle(ref ListaBEAjusteDetalle);

            BERetornoTran oBERetorno = new BERetornoTran();
            if (oBEMov.IDMovimiento == 0)
            {
                oBERetorno = oBLMov.NotaAjusteGuardar(oBEMov, ListaBEAjusteDetalle);
            }
            else
            {
                oBERetorno = oBLMov.NotaAjusteActualizar(oBEMov, ListaBEAjusteDetalle);
            }


            if (oBERetorno.Retorno == "1")
            {
                msgbox(TipoMsgBox.confirmation, "Se grabó correctamente");
                Session["aListaAjuste"] = null;
                Session["token"] = null;
                LlenargvListaAjuste((ArrayList)Session["aListaAjuste"]);
                GenerarToken();
                Listar();
                upRegistroAjuste.Update();
            }
            else if (oBERetorno.Retorno == "-2")
            {
                msgbox(TipoMsgBox.error, "Stock Insuficiente");
            }
            else if (oBERetorno.Retorno == "-3")
            {
                msgbox(TipoMsgBox.error, "Stock de Lote Insuficiente");
            }
        }

        private void SetListaBEAjusteDetalle(ref ArrayList ListaBEAjusteDetalle)
        {
            ArrayList aListaBEAlmacenDetalle = new ArrayList();
            aListaBEAlmacenDetalle = (ArrayList)Session["aListaAjuste"];
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
                    oBE.PrecioUnitario = oBE.PrecioUnitario;
                    oBE.IDUsuario = IDUsuario();
                    ListaBEAjusteDetalle.Add(oBE);

                }
            }
        }

        protected void lnkNuevoAjuste_Click(object sender, EventArgs e)
        {
            LimpiarAjuste();
        }

        #endregion

        #region Agregar Producto
        protected void lnkNuevoItem_Click(object sender, EventArgs e)
        {
            LimpiarProducto();
            registrarScript("funModalProductoAbrir();");
        }

        private void LimpiarProducto()
        {
            hdfIDMovimientoDetalle.Value = "-1";
            ddlIDProducto.Enabled = true;
            hdfIDProducto.Value = "0";
            ddlIDProducto.SelectedIndex = 0;
            txtRegCantidad.Text = "1";
            txtRegStock.Text = "0";
            txtRegUnidadMedida.Text = "";
            txtRegCantidadLote.Text = "0";
            txtRegPrecioUnitario.Text = "";
            hdfIDUnidadMedida.Value = "0";
            DivCantidadLote.Visible = false;
            DivCantidad.Visible = true;
            upRegistroProducto.Update();

        }

        protected void btnCancelarItem_Click(object sender, EventArgs e)
        {
            registrarScript("funModalProductoCerrar();");
        }

        protected void btnAgregarItem_Click(object sender, EventArgs e)
        {

            StringBuilder pValidaciones = new StringBuilder();
            if (hdfIDProducto.Value == "0") pValidaciones.Append("<div>Seleccione un Producto</div>");

            if (DivCantidadLote.Visible)
            {
                if (txtRegCantidadLote.Text == "") pValidaciones.Append("<div>Ingrese Cantidad de Lote</div>");
                if (Convert.ToDecimal(txtRegCantidadLote.Text) <= 0) pValidaciones.Append("<div>La Cantidad no puede ser negativo o cero</div>");

                if (Convert.ToDecimal(txtRegCantidadLote.Text) > Convert.ToDecimal(txtRegStock.Text))
                {
                    pValidaciones.Append("<div>Stock Insuficiente</div>");
                }

            }
            else
            {
                if (txtRegCantidad.Text == "") pValidaciones.Append("<div>Ingrese una Cantidad</div>");
                if (Convert.ToDecimal(txtRegCantidad.Text) <= 0) pValidaciones.Append("<div>La Cantidad no puede ser negativo o cero</div>");

                if (Convert.ToDecimal(txtRegCantidad.Text) > Convert.ToDecimal(txtRegStock.Text))
                {
                    pValidaciones.Append("<div>Stock Insuficiente</div>");
                }

            }
            //if (txtRegPrecioUnitario.Text == "") pValidaciones.Append("<div>Ingrese Precio Unitario</div>");
            //if (txtRegPrecioUnitario.Text.Length > 0)
            //{
            //    if (Decimal.Parse(txtRegPrecioUnitario.Text) <= 0)
            //    {
            //        pValidaciones.Append("<div>El precio unitario no puede ser negativo o cero</div>");
            //    }
            //}

            if (pValidaciones.Length > 0)
            {
                msgbox(TipoMsgBox.warning, pValidaciones.ToString());
                return;
            }

            Decimal Cantidad = (DivCantidad.Visible) ? Convert.ToDecimal(txtRegCantidad.Text) : Convert.ToDecimal(txtRegCantidadLote.Text);

            ArrayList aListaAjuste = new ArrayList();

            aListaAjuste = (ArrayList)(Session["aListaAjuste"]);
            Int32 pID = Int32.Parse(hdfIDMovimientoDetalle.Value);
            Int32 pIDProducto = Int32.Parse(hdfIDProducto.Value);
            BEProducto oBE = new BLProducto().ProductoSeleccionar(pIDProducto, IDSucursal());

            BEMovimientoDetalle oBEMovDet = new BEMovimientoDetalle();

            oBEMovDet.IDProducto = pIDProducto;
            oBEMovDet.ProductoCodigo = oBE.CodigoProducto;
            oBEMovDet.NombreProducto = oBE.Nombre;
            oBEMovDet.StockActual = oBE.Stock;
            oBEMovDet.IDUnidadMedida = oBE.IDUnidadMedidaCompra;
            oBEMovDet.PrecioUnitario = Decimal.Parse(txtRegPrecioUnitario.Text);
            oBEMovDet.Cantidad = Cantidad;
            oBEMovDet.SubTotal = Int32.Parse(txtRegCantidad.Text.Trim()) * Decimal.Parse(txtRegPrecioUnitario.Text);

            if (pID == -1)
            {
                if (ContarIngresado(aListaAjuste, Int32.Parse(hdfIDProducto.Value)) > 0)
                {
                    msgbox(TipoMsgBox.information, "Ya ingresó ese producto.");
                    return;
                }
            }

            if (aListaAjuste == null)
            {
                aListaAjuste = new ArrayList();
            }
            if (hdfIDMovimientoDetalle.Value == "-1")
            {

                aListaAjuste.Add(oBEMovDet);
            }
            else
            {
                aListaAjuste[pID] = oBEMovDet;
            }

            LlenargvListaAjuste(aListaAjuste);
            Session["aListaAjuste"] = aListaAjuste;
            CalcularTotales();
            gvDetalleLista.SelectedIndex = -1;
            LimpiarProducto();
            msgbox(TipoMsgBox.confirmation, "Se agregó satisfactoriamente.");
        }

        protected void ddlIDProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIDSucursal.SelectedValue == "0")
            {
                msgbox(TipoMsgBox.warning, "Seleccione Sucursal");
                return;
            }

            BEProducto oBE = new BLProducto().ProductoSeleccionar(Int32.Parse(ddlIDProducto.SelectedValue), Int32.Parse(ddlIDSucursal.SelectedValue));
            hdfIDProducto.Value = oBE.IDProducto.ToString();
            hdfIDUnidadMedida.Value = oBE.IDUnidadMedidaVenta.ToString();
            txtRegUnidadMedida.Text = oBE.UnidadMedidaVenta;
            txtRegPrecioUnitario.Text = oBE.PrecioVenta.ToString("N");
            txtRegStock.Text = oBE.Stock.ToString();
            txtRegCantidad.Text = "1";
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

            CalcularTotales();
            upRegistroProducto.Update();
        }

        protected void SeleccionarProducto(Int32 IDProducto)
        {

            BEProducto oBE = new BLProducto().ProductoSeleccionar(IDProducto, IDSucursal());
            hdfIDProducto.Value = oBE.IDProducto.ToString();
            hdfIDUnidadMedida.Value = oBE.IDUnidadMedidaVenta.ToString();
            txtRegUnidadMedida.Text = oBE.UnidadMedidaVenta;
            txtRegPrecioUnitario.Text = oBE.PrecioVenta.ToString("N");
            txtRegStock.Text = oBE.Stock.ToString();
            txtRegCantidad.Text = "1";
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

            CalcularTotales();
            upRegistroProducto.Update();

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

        private void MovimientoDetalleLoteListarXProducto()
        {
            BLLote oBLLote = new BLLote();
            BELote oBE = new BELote();
            oBE.IDSucursal = IDSucursal();
            oBE.IDProducto = Int32.Parse(hdIDProductoLote.Value);
            oBE.Token = Session["token"].ToString().Trim();
            gvLoteProducto.DataSource = oBLLote.LotexNotaAjusteTokenListar(oBE);
            gvLoteProducto.DataBind();
            upLoteListar.Update();
        }

        //protected void lnkAplicarLote_Click(object sender, EventArgs e)
        //{
        //    Decimal Cantidad = 0;

        //    foreach (GridViewRow row in gvLoteProducto.Rows)
        //    {
        //        Decimal CantidadLote = 0;
        //        if ((((TextBox)row.Cells[5].FindControl("txtCantidadLote")).Text) != "") {
        //            CantidadLote = Convert.ToDecimal(((TextBox)row.Cells[5].FindControl("txtCantidadLote")).Text);
        //        }
        //        //Registrar en NotaAjusteDetalleLote
        //        BENotaAjusteDetalleLote BENotaAjusteLote = new BENotaAjusteDetalleLote();
        //        BENotaAjusteLote.IDProducto = Int32.Parse(ddlIDProducto.SelectedValue);
        //        BENotaAjusteLote.IDLote = Int32.Parse(((Label)row.Cells[5].FindControl("lblIDLote")).Text);
        //        BENotaAjusteLote.Cantidad = CantidadLote;
        //        BENotaAjusteLote.Estado = false;
        //        BENotaAjusteLote.Token = Session["token"].ToString().Trim();

        //        BLNotaAjusteDetalleLote oBL = new BLNotaAjusteDetalleLote();
        //        BERetornoTran oBERetorno = oBL.NotaAjusteDetalleLoteTemporalGuardar(BENotaAjusteLote);
        //        if (oBERetorno.Retorno != "1") {
        //            msgbox(TipoMsgBox.confirmation, "No se pudo registrar las cantidades del lote");
        //        }
        //        Cantidad += CantidadLote;
        //    }
        //    txtRegCantidadLote.Text = Cantidad.ToString("N");
        //    upRegistroProducto.Update();
        //    registrarScript("funModalListaLoteCerrar();");
        //}

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

        protected BERetornoTran MovimientoDetalleLoteTemporalGuardar(Int32 IDProducto, Int32 IDLote, Decimal Cantidad)
        {

            BEMovimientoDetalleLote BEMovimientoLote = new BEMovimientoDetalleLote();
            BEMovimientoLote.IDProducto = IDProducto;
            BEMovimientoLote.IDLote = IDLote;
            BEMovimientoLote.Cantidad = Cantidad;
            BEMovimientoLote.Estado = false;
            BEMovimientoLote.IDUsuario = IDUsuario();
            BEMovimientoLote.Token = Session["token"].ToString().Trim();
            BLMovimientoDetalleLote oBL = new BLMovimientoDetalleLote();
            BERetornoTran oBERetorno = oBL.MovimientoDetalleLoteTemporalGuardar(BEMovimientoLote);
            return oBERetorno;
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
                oBE.CantidadLote = 0;
                oBE.FechaFabricacion = DateTime.Parse(txtLFechaFabricacion.Text);
                oBE.FechaVencimiento = DateTime.Parse(txtLFechaVencimiento.Text);
                oBE.Estado = true; //PENDIENTE 
                oBE.IDUsuario = IDUsuario();
                BERetornoTran oBERetorno = new BERetornoTran();
                oBERetorno = oBL.LoteAjusteGuardar(oBE);

                if (oBERetorno.Retorno == "1")
                {
                    //Guardar Lote Movimiento Temporal
                    //BERetornoTran oBERetornoTemp = MovimientoDetalleLoteTemporalGuardar(Int32.Parse(hdIDProductoLote.Value), Int32.Parse(hdfIDLote.Value), Decimal.Parse(txtLCantidad.Text.Trim())  );

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