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
    public partial class CreditoDebito : PageBase
    {
          
        #region INICIO
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
                //CargaInicial();
                CargarDDL(ddlSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", false);
                CargarDDL(ddlBIDEstadoCompra, new BLEstado().EstadoListar("COM"), "Codigo", "Nombre", true, Constantes.TODOS);
                CargarDDL(ddlIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("CD","CDE"), "IDTipoComprobante", "Nombre", true, Constantes.SELECCIONAR);
                CargarDDL(ddlIDTipoComprobanteAfectado, new BLTipoComprobante().TipoComprobanteListar("DE","VEN"), "IDTipoComprobante", "Nombre", true, Constantes.SELECCIONAR);
                CargarDDL(ddlIDReProducto, new BLProducto().ListarPorSucursal(IDSucursal()), "IDProducto", "Nombre", true, Constantes.SELECCIONAR);
                 
                txtFechaInicio.Text = fecha1.ToShortDateString();
                txtFechaFin.Text = DateTime.Today.ToShortDateString();
                Listar();
            }
        }

        private void CargaSerie()
        {
            String pIDTipoDocumento = ddlIDTipoComprobante.SelectedValue;
            txtSerieNumero.Text = new BLDocumentoSerie().DocumentoSerieListar(pIDTipoDocumento, IDSucursal());
        }

        protected void txtSerieNumero_TextChanged(object sender, EventArgs e)
        {
            txtSerieNumero.Text = txtSerieNumero.Text.PadLeft(4, '0');
        }

        protected void txtNumeroDocumento_TextChanged(object sender, EventArgs e)
        {
            txtNumeroDocumento.Text = txtNumeroDocumento.Text.PadLeft(8, '0');
        }

        #endregion
        #region CONSULTA

        private void Listar()
        {
            BLCompras oBLCompras = new BLCompras();
            gvLista.DataSource = oBLCompras.ComprasNotaCreditoListar(Int32.Parse(ddlSucursal.SelectedValue), 0, txtCOMFiltro.Text.Trim(), txtFechaInicio.Text, txtFechaFin.Text, Int32.Parse(ddlBIDEstadoCompra.SelectedValue), 0, "C");
            gvLista.DataBind();
            upLista.Update();
        }

        //protected void ddlIDTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CargaSerie();
        //}

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
            hdfIDCompra.Value = e.CommandArgument.ToString();
            BERetornoTran oBERetorno = new BERetornoTran();

            switch (e.CommandName)
            {

                case "Editar":
                    CompraSeleccionar();
                    ListarCompraDetalle();
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

        protected void CompraSeleccionar()
        {
            BECompras oBE = new BLCompras().ComprasSeleccionar(Int32.Parse(hdfIDCompra.Value));
            //lblNumeroCompra.Text = oBE.NumeroCompra; 
            txtFechaEmision.Text = oBE.FechaCompra.ToShortDateString();
            txtSerieNumeroAfectado.Text = oBE.SerieDocumento + " - " + " " + oBE.NumeroDocumento;
            ddlIDTipoComprobanteAfectado.SelectedValue = oBE.TipoComprobanteCompra.ToString();
            ddlIDTipoComprobante.SelectedValue = oBE.TipoComprobanteCompra.ToString();
            txtSerieNumero.Text = oBE.SerieDocumento;
            txtNumeroDocumento.Text = oBE.NumeroDocumento;
            txtRegNumeroDocumentoProveedorAfectado.Text = oBE.NumeroDocumento;
            txtRegProveedorAfectado.Text = oBE.ProveedorRazonSocial;
            txtTotalCompraAfectado.Text = oBE.TotalCompra.ToString(); 
            txtMotivo.Text = oBE.Glosa;
            lblSubTotal.Text = oBE.SubTotal.ToString();
            hdfSubTotal.Value = oBE.SubTotal.ToString();
            lblTotalIgv.Text = oBE.TotalIGV.ToString();
            hdfTotalIGV.Value = oBE.TotalIGV.ToString();
            lblTotal.Text = oBE.TotalCompra.ToString();
            hdfTotalCompra.Value = oBE.TotalCompra.ToString();
            ddlIDTipoComprobante.Enabled = true;
            txtSerieNumeroAfectado.Enabled = true;
            btnAgregarItem.Visible = false;
            lnkGuardarNota.Visible = false;
             
        }
        #endregion 
        #region DETALLE COMPRA
        protected void gvDetalleLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdfIDCompraDetalle.Value = gvDetalleLista.SelectedIndex.ToString();
            hdfIDProducto.Value = gvDetalleLista.SelectedDataKey["IDProducto"].ToString();
            BEProducto oBE = new BLProducto().ProductoSeleccionar(Int32.Parse(hdfIDProducto.Value), IDSucursal());
            GridViewRow row = gvDetalleLista.SelectedRow;
            hdfIDUnidadMedida.Value = oBE.IDUnidadMedida.ToString();
            ddlIDReProducto.SelectedValue = oBE.IDProducto.ToString();
            txtRegUnidMedida.Text = oBE.UnidadMedidaCompra;
            //txtRegProducto.Text = oBE.CodigoProducto + "-" + oBE.Nombre;
            txtRegStock.Text = oBE.Stock.ToString();
            txtRegCantidad.Text = ((Label)row.FindControl("lblCantidad")).Text;
            txtRegPrecioCompra.Text = ((Label)row.FindControl("lblPrecioUnitario")).Text;

            registrarScript("funModalProductoAbrir();");
            upRegistroProducto.Update();
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

        private void LimpiarCompra()
        {
           
            hdfIDCompra.Value = "0";
            hdfIDCompraDetalle.Value = "-1";
            Session["aListaCompras"] = null;
            LlenargvListaCompras((ArrayList)Session["aListaCompras"]);
            txtSerieNumeroAfectado.Text = "";
            ddlIDTipoComprobante.SelectedIndex = -1;
            ddlIDTipoComprobanteAfectado.SelectedIndex = -1;
            txtRegNumeroDocumentoProveedorAfectado.Text = "";
            txtRegProveedorAfectado.Text = "";
            txtNumeroDocumento.Text = "";
            txtTotalCompraAfectado.Text = "";
            txtFechaEmision.Text = "";
            txtSerieNumero.Text = "";
            txtMotivo.Text = "";
            lblSubTotal.Text = "0.00";
            lblTotalIgv.Text = "0.00";
            lblTotal.Text = "0.00";
            hdfSubTotal.Value = "0";
            hdfTotalIGV.Value = "0";
            hdfTotalCompra.Value = "0";

            hdfIDCompraDetalle.Value = "-1";
            hdfIDProducto.Value = "0";
            ddlIDReProducto.SelectedIndex = -1;
            txtRegUnidMedida.Text = "";
            txtRegCantidad.Text = "1";
            txtRegPrecioCompra.Text = "";
            txtRegStock.Text = "";
            hdfIDUnidadMedida.Value = "0";

            btnAgregarItem.Visible = true;
            lnkGuardarNota.Visible = true;
            upRegistroProducto.Update();

            upRegistroCompra.Update();
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

        public void LlenargvListaCompras(ArrayList DT)
        {
            gvDetalleLista.DataSource = DT;
            gvDetalleLista.DataBind();
            upRegistroCompra.Update();
        }

        #endregion 
        #region REGISTRO NOTA CREDITO
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

        protected void lnkGuardarNota_Click(object sender, EventArgs e)
        {
            StringBuilder pValidaciones = new StringBuilder();
            if (ddlIDTipoComprobante.SelectedValue == "0" || ddlIDTipoComprobante.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Comprobante</div>");

            if (txtMotivo.Text == "") pValidaciones.Append("<div>Ingrese Motivo</div>");
            if (txtSerieNumeroAfectado.Text == "") pValidaciones.Append("<div>Ingrese Serie Numero</div>");

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
            oBECompras.IDAlmacen = Int32.Parse(hdfIDProveedor.Value);
            oBECompras.IDProveedor = Int32.Parse(hdfIDProveedor.Value);
            oBECompras.IDMoneda = hdfIDMoneda.Value;
            oBECompras.IDTipoDocumento = Int32.Parse(ddlIDTipoComprobante.SelectedValue);
            oBECompras.IDFormaPago = Int32.Parse(hdfIDFormaPago.Value);
            oBECompras.TipoCompra = "C";
            oBECompras.Cuenta = "";
            oBECompras.CuentaCaja = "";
            oBECompras.Glosa = txtMotivo.Text;
            oBECompras.Serie = txtSerieNumero.Text;
            oBECompras.NumeroDocumento = txtNumeroDocumento.Text;
            oBECompras.FechaRegistro = DateTime.Parse(txtFechaEmision.Text);
            oBECompras.FechaCompra = DateTime.Parse(txtFechaEmision.Text);
            oBECompras.FechaVencimiento = DateTime.Now;
            oBECompras.TotalCompra = Decimal.Parse(hdfTotalCompra.Value);
            oBECompras.TotalIGV = Decimal.Parse(hdfTotalIGV.Value);

            oBECompras.TipoDocumentoReferencia = hdfTipoDocumentoReferencia.Value;
            oBECompras.SerieNumeroDocumentoReferencia = txtSerieNumeroAfectado.Text;
            oBECompras.FechaEmisionDocumentoReferencia = DateTime.Now;
            oBECompras.IDUsuario = IDUsuario();

            SetListaBEComprasDetalle(ref ListaBEComprasDetalle);

            BERetornoTran oBERetorno = new BERetornoTran();

            if (hdfIDCompra.Value == "0")
            {
                oBERetorno = oBLCompras.GuardarCompras(oBECompras, ListaBEComprasDetalle);
            }
            else
            {
                oBERetorno = oBLCompras.ComprasActualizar(oBECompras, ListaBEComprasDetalle);
            }

            if (oBERetorno.Retorno == "1")
            {
                msgbox(TipoMsgBox.confirmation, "Compra se Registro correctamente");
                Listar();
                Session["aListaCompras"] = null;
                LlenargvListaCompras((ArrayList)Session["aListaCompras"]);
                LimpiarCompra();
                CalcularTotales();
                upRegistroCompra.Update();
            }
            else if (oBERetorno.Retorno == "2")
            {
                msgbox(TipoMsgBox.warning, "El documento ya existe para ese proveedor");
            }
            else if (oBERetorno.Retorno == "-1")
            {
                RegistrarLogSistema("lnkGuardarNota_Click()", oBERetorno.ErrorMensaje, true);
            }
        }

        protected void lnkNuevaNotaCredito_Click(object sender, EventArgs e)
        {
            LimpiarCompra();
        }
        #endregion
        #region DETALLE PRODUCTO
        protected void ddlIDReProducto_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        #endregion
        #region DOCUMENTO REFERENCIA
        protected void lnkBuscarComprobante_Click(object sender, EventArgs e)
        {
            registrarScript("funModalDocumentoReferenciaAbrir();");
        }

        private void ListarDocumentoElectronico()
        {
            BLCompras oBLCompras = new BLCompras();
            gvDocumentoElectronicoListar.DataSource = oBLCompras.ComprasListarxDocumento(IDSucursal(), 0, txtBFiltro.Text.Trim(), txtBFechaInicio.Text, txtBFechaFin.Text, 0, 0, "C");
            gvDocumentoElectronicoListar.DataBind();
        }
        protected void btnBuscarDocumento_Click(object sender, EventArgs e)
        {
            ListarDocumentoElectronico();
        }

        protected void gvDocumentoElectronicoListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocumentoElectronicoListar.PageIndex = e.NewPageIndex;
            gvDocumentoElectronicoListar.SelectedIndex = -1;
            ListarDocumentoElectronico();
        }

        protected void gvDocumentoElectronicoListar_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdfIDCompraDocumentoModifica.Value = gvDocumentoElectronicoListar.SelectedDataKey["IDCompras"].ToString();
            BECompras oBE = new BLCompras().ComprasSeleccionar(Int32.Parse(hdfIDCompraDocumentoModifica.Value));
            GridViewRow row = gvDocumentoElectronicoListar.SelectedRow;
            //hdfTipoDocumentoReferencia.Value = ((Label)row.FindControl("lblIDTipoComprobanteCS")).Text;
            txtSerieNumeroAfectado.Text = ((Label)row.FindControl("lblSerieNumero")).Text;
            ddlIDTipoComprobanteAfectado.SelectedValue = oBE.TipoComprobanteCompra.ToString();
            hdfIDProveedor.Value = oBE.IDProveedor.ToString();
            hdfIDFormaPago.Value = oBE.IDFormaPago.ToString();
            hdfIDAlmacen.Value = oBE.IDAlmacen.ToString();
            hdfIDMoneda.Value = oBE.IDMoneda;
            hdfIDSucursal.Value = oBE.IDSucursal.ToString();
            //txtFechaDocumentoReferencia.Text = ((Label)row.FindControl("lblFechaCompra")).Text;
            //hdfRegIDComprobanteAfectado.Value = ((HiddenField)row.FindControl("hdfIDComprobante")).Value;
            txtTotalCompraAfectado.Text= ((Label)row.FindControl("lblCompra")).Text;
            txtRegProveedorAfectado.Text = ((Label)row.FindControl("lblProveedor")).Text;
            txtRegNumeroDocumentoProveedorAfectado.Text = ((Label)row.FindControl("lblRucProveedor")).Text;
            upRegistroCompra.Update();
            ListarCompraDetalleDocumentoModifica();
            CalcularTotales();
            registrarScript("funModalDocumentoReferenciaCerrar();");
        }

        #endregion

     
    }
}