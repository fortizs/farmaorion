using Farmacia.App_Class;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class KitDesarmar : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                CargarDDL(ddlIDKit, new BLKit().KitListar(IDSucursal(), ""), "IDKit", "NombreProducto", true, Constantes.SELECCIONAR);
                CargarDDL(ddlIDTransaccion, new BLTransaccion().TransaccionListar("S"), "IDTransaccion", "Nombre", true, Constantes.SELECCIONAR);
                CargarDDL(ddlIDAlmacenIngreso, new BLAlmacen().AlmacenListar(IDSucursal()), "IDAlmacen", "Nombre", true, Constantes.SELECCIONAR);
                CargaInicial();
            }
        }

        private void CargaInicial()
        {

            ddlIDTransaccion.SelectedValue = Constantes.ID_Tran_S_Desarmar_Kit.ToString();

            ArrayList aListaDetalle = new ArrayList();
            if (Session["aListaDetalle"] == null) //Trabajando en Memoria
            {
                Session["aListaDetalle"] = aListaDetalle;
            }
            else
            {
                //aListaDetalle = (ArrayList)(Session["aListaDetalle"]);
                Session["aListaDetalle"] = aListaDetalle;
            }

            if (Session["token"] == null) //Trabajando en Memoria
            {
                GenerarToken();
            }

            LlenargvLista(aListaDetalle);
        }

        protected void GenerarToken()
        {
            Session["token"] = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        protected void ddlIDKit_SelectedIndexChanged(object sender, EventArgs e)
        {

            BEKit oBE = new BLKit().KitSeleccionar(Int32.Parse(ddlIDKit.SelectedValue), IDSucursal());
            hdfIDProductoKit.Value = oBE.IDProducto.ToString();
            txtUnidadMedida.Text = oBE.UnidadMedida;
            hdfIDUnidadMedida.Value = oBE.IDUnidadMedida.ToString();
            KitDetalleListar(Int32.Parse(ddlIDKit.SelectedValue));
            txtKitStock.Text = oBE.Cantidad.ToString("N");
            upIngreso.Update();
        }

        private void KitDetalleListar(Int32 IDKit)
        {
            ArrayList aListaDetalle = new ArrayList();
            aListaDetalle = (ArrayList)new BLKitDetalle().KitDetalleListar(IDKit, IDSucursal());
            Decimal precioCosto = 0;
            foreach (BEKitDetalle oBE in aListaDetalle)
            {
                precioCosto += oBE.Producto.PrecioCosto;
            }
            txtPrecioCosto.Text = precioCosto.ToString("N");

            LlenargvLista(aListaDetalle);
            Session["aListaDetalle"] = aListaDetalle;
            gvDetalleLista.SelectedIndex = -1;
            upIngreso.Update();
        }

        public void LlenargvLista(ArrayList DT)
        {
            gvDetalleLista.DataSource = DT;
            gvDetalleLista.DataBind();
            upIngreso.Update();
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            Decimal Cantidad = Decimal.Parse(txtCantidad.Text);
            Decimal KitStock = Decimal.Parse(txtKitStock.Text);

            if (Cantidad > KitStock ) {
                msgbox(TipoMsgBox.warning, "Cantidad a Desarmar debe ser menor o igual al Stock del Kit");
                return;
            }

            foreach (GridViewRow row in gvDetalleLista.Rows)
            {

                Decimal CantidadReg = Decimal.Parse(((Label)row.Cells[0].FindControl("lblCantidadReg")).Text);
                Decimal CantidadArmada = Cantidad * CantidadReg;


                Int32 IDProducto = Int32.Parse(((Label)row.Cells[0].FindControl("lblIDProducto")).Text);

                Boolean ControlaLote = Boolean.Parse(((Label)row.Cells[0].FindControl("lblControlaLote")).Text);

                if (ControlaLote)
                {
                    //Muestra Popup Lote
                    ((Label)row.Cells[0].FindControl("lblCantidadArmado")).Text = "0.00";
                    ((Label)row.Cells[0].FindControl("lblCantidadArmadoSaldo")).Text = CantidadArmada.ToString("N");                    
                }
                else
                {
                    //Calcula Directo
                    ((Label)row.Cells[0].FindControl("lblCantidadArmado")).Text = CantidadArmada.ToString("N");
                    ((Label)row.Cells[0].FindControl("lblCantidadArmadoSaldo")).Text = "0.00";
                }
            }
        }



        private void SetListaDetalle(ref ArrayList ListaDetalle)
        {


            StringBuilder pValidaciones = new StringBuilder();
            Decimal CantidadKit = Decimal.Parse(txtCantidad.Text);

            foreach (GridViewRow row in gvDetalleLista.Rows)
            {

                Decimal CantidadReg = Decimal.Parse(((Label)row.Cells[0].FindControl("lblCantidadReg")).Text);
                Decimal CantidadDisponible = Decimal.Parse(((Label)row.Cells[2].FindControl("lblCantidadDisponible")).Text);
                Decimal CantidadArmado = Decimal.Parse(((Label)row.Cells[2].FindControl("lblCantidadArmado")).Text);
                Decimal TotalArmado = CantidadReg * CantidadKit;
                if (CantidadArmado == 0) pValidaciones.Append("<div>Cantidad a desarmar debe ser mayor a cero</div>");
                if (CantidadDisponible < TotalArmado) pValidaciones.Append("<div>Cantidad de Armado no puede ser mayor a la cantidad disponible</div>");
                if (TotalArmado != CantidadArmado) pValidaciones.Append("<div>Cantidad de Armado supera lo permitido</div>");
                

                if (pValidaciones.Length > 0)
                {
                    msgbox(TipoMsgBox.warning, pValidaciones.ToString());
                    return;
                }

                //Agregar a Lista
                BEMovimientoDetalle oBE = new BEMovimientoDetalle();
                oBE.IDMovimientoDetalle = 0;
                oBE.IDMovimiento = 0;
                oBE.IDProducto = Int32.Parse(((Label)row.Cells[2].FindControl("lblIDProducto")).Text);
                oBE.NombreProducto = ((Label)row.Cells[2].FindControl("lblNombreProducto")).Text.Trim();
                oBE.IDUnidadMedida = Int32.Parse(((Label)row.Cells[2].FindControl("lblIDUnidadMedida")).Text); ;
                oBE.Cantidad = CantidadArmado;
                oBE.IDUsuario = IDUsuario();
                ListaDetalle.Add(oBE);
            }
        }

        #region Gestionar Lotes

        private void MovimientoDetalleLoteListar(Int32 IDProducto)
        {
            hdIDProductoLote.Value = IDProducto.ToString();

            BLMovimientoDetalleLote oBLLote = new BLMovimientoDetalleLote();
            BEMovimientoDetalleLote oBE = new BEMovimientoDetalleLote();
            oBE.IDMovimiento = Int32.Parse(hdfIDMovimiento.Value);
            oBE.IDMovimientoDetalle = 0;
            oBE.IDSucursal = IDSucursal();
            oBE.IDProducto = Int32.Parse(hdIDProductoLote.Value);
            oBE.Token = Session["token"].ToString().Trim();
            gvLoteProducto.DataSource = oBLLote.MovimientoDetalleLoteListar(oBE);
            gvLoteProducto.DataBind();
            upLoteListar.Update();
        }





        #endregion

        protected void gvDetalleLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            BELote oBE = new BELote();
            hdfIDProductoSeleccionado.Value = gvDetalleLista.SelectedIndex.ToString();
            hdIDProductoLote.Value = gvDetalleLista.SelectedDataKey["IDProducto"].ToString();

            if (txtCantidad.Text == "" || txtCantidad.Text == "0")
            {
                msgbox(TipoMsgBox.warning, "La cantidad del kit debe ser mayor a cero");
                return;
            }

            Decimal CantidadKit = Decimal.Parse(txtCantidad.Text);

            Decimal CantidadReg = Decimal.Parse(((Label)this.gvDetalleLista.Rows[Int32.Parse(hdfIDProductoSeleccionado.Value)].FindControl("lblCantidadReg")).Text);
            Decimal SaldoCantidadLote = CantidadKit * CantidadReg;
            txtSaldoCantidadLote.Text = SaldoCantidadLote.ToString("N");
            MovimientoDetalleLoteListar(Int32.Parse(hdIDProductoLote.Value));
            registrarScript("funModalListaLoteAbrir();");
        }

        protected void lnkAplicarLote_Click(object sender, EventArgs e)
        {

            Decimal Cantidad = 0;
            Int32 i = 0;                       

            foreach (GridViewRow row in gvLoteProducto.Rows)
            {
                Decimal CantidadLote = 0;
                Decimal StockLote = Decimal.Parse(((Label)row.Cells[0].FindControl("lblStockLote")).Text);

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
                BEMovimientoLote.Token = Session["token"].ToString().Trim();
                BLMovimientoDetalleLote oBL = new BLMovimientoDetalleLote();
                BERetornoTran oBERetorno = oBL.MovimientoDetalleLoteTemporalGuardar(BEMovimientoLote);
                if (oBERetorno.Retorno != "1")
                {
                    msgbox(TipoMsgBox.confirmation, "No se pudo registrar las cantidades del lote");
                }
                Cantidad += CantidadLote;
                i++;
            }            
            String var = ((Label)this.gvDetalleLista.Rows[Int32.Parse(hdfIDProductoSeleccionado.Value)].FindControl("lblCantidadArmado")).Text;
            ((Label)this.gvDetalleLista.Rows[Int32.Parse(hdfIDProductoSeleccionado.Value)].FindControl("lblCantidadArmado")).Text = Cantidad.ToString("N");
            upIngreso.Update();
            registrarScript("funModalListaLoteCerrar();");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            StringBuilder pValidaciones = new StringBuilder();
            if (ddlIDAlmacenIngreso.SelectedValue == "0" || ddlIDAlmacenIngreso.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Almacen Ingreso</div>");
            if (ddlIDTransaccion.SelectedValue == "0" || ddlIDTransaccion.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Transacción</div>");
            if (txtCantidad.Text == "0" || txtCantidad.Text == "") pValidaciones.Append("<div>Ingrese Cantidad a Desarmar</div>");
            if (txtCantidad.Text != "") {
                Decimal Cantidad = Decimal.Parse(txtCantidad.Text);
                Decimal KitStock = Decimal.Parse(txtKitStock.Text);
                if (Cantidad > KitStock) pValidaciones.Append("<div>Cantidad a Desarmar debe ser menor o igual al Stock del Kit</div>");                
            } 
            

            if (pValidaciones.Length > 0)
            {
                msgbox(TipoMsgBox.warning, pValidaciones.ToString());
                return;
            }

            ArrayList ListaDetalle = new ArrayList();
            SetListaDetalle(ref ListaDetalle);

            BLMovimiento oBLMov = new BLMovimiento();
            BEMovimiento oBEMov = new BEMovimiento();
            oBEMov.IDMovimiento = Int32.Parse(hdfIDMovimiento.Value);
            oBEMov.IDAlmacenOrigen = Int32.Parse(ddlIDAlmacenIngreso.SelectedValue);
            oBEMov.IDAlmacenDestino = Int32.Parse(ddlIDAlmacenIngreso.SelectedValue);
            oBEMov.IDEntidad = 0;
            oBEMov.Entidad = "DESARMAR KIT";
            oBEMov.IDTransaccion = Constantes.ID_Tran_I_Desarmar_Kit; //Ingreso por Desarmar Kit
            oBEMov.TipoMovimiento = "I";
            oBEMov.Fecha = DateTime.Parse(txtFechaEmision.Text);
            oBEMov.Observacion = "";
            oBEMov.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
            oBEMov.IDUsuario = IDUsuario();

            BERetornoTran oBERetorno = new BERetornoTran();

            if (oBEMov.IDMovimiento.Equals(0))
            {
                oBERetorno = oBLMov.MovimientoGuardar(oBEMov);

                if (oBERetorno.Retorno == "1")
                {

                    foreach (BEMovimientoDetalle oBEDet in ListaDetalle)
                    {
                        oBEDet.IDMovimiento = Int32.Parse(oBERetorno.Retorno2);
                        oBEDet.Token = Session["token"].ToString();
                        oBEDet.FechaMovimiento = DateTime.Parse(txtFechaEmision.Text);
                        oBEDet.IDUsuario = IDUsuario();
                        BERetornoTran oBERetornoDetalle = new BERetornoTran();
                        oBERetornoDetalle = oBLMov.MovimientoDetalleKitArmadoGuardar(oBEDet);
                        if (oBERetornoDetalle.Retorno != "1")
                        {

                        }
                    }

                    BLMovimiento oBLMovI = new BLMovimiento();
                    BEMovimiento oBEMovI = new BEMovimiento();

                    oBEMovI.IDMovimiento = Int32.Parse(hdfIDMovimiento.Value);
                    oBEMovI.IDAlmacenOrigen = Int32.Parse(ddlIDAlmacenIngreso.SelectedValue);
                    oBEMovI.IDAlmacenDestino = Int32.Parse(ddlIDAlmacenIngreso.SelectedValue);
                    oBEMovI.IDEntidad = 0;
                    oBEMovI.Entidad = "DESARMAR KIT";
                    oBEMovI.IDTransaccion = Constantes.ID_Tran_S_Desarmar_Kit; //Salida para Armar Kit
                    oBEMovI.TipoMovimiento = "S";
                    oBEMovI.Fecha = DateTime.Parse(txtFechaEmision.Text);
                    oBEMovI.Observacion = "";
                    oBEMovI.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
                    oBEMovI.IDUsuario = IDUsuario();

                    BERetornoTran oBERetornoIngreso = new BERetornoTran();

                    oBERetornoIngreso = oBLMovI.MovimientoGuardar(oBEMovI);

                    if (oBERetornoIngreso.Retorno == "1")
                    {

                        BEMovimientoDetalle oBEDetI = new BEMovimientoDetalle();
                        oBEDetI.IDMovimiento = Int32.Parse(oBERetornoIngreso.Retorno2);
                        oBEDetI.IDProducto = Int32.Parse(hdfIDProductoKit.Value);
                        oBEDetI.IDUnidadMedida = Int32.Parse(hdfIDUnidadMedida.Value);
                        oBEDetI.PrecioCosto = Decimal.Parse(txtPrecioCosto.Text);
                        oBEDetI.Cantidad = Decimal.Parse(txtCantidad.Text);
                        oBEDetI.FechaMovimiento = DateTime.Parse(txtFechaEmision.Text);
                        oBEDetI.IDUsuario = IDUsuario();

                        BERetornoTran oBERetornoI = oBLMovI.MovimientoDetalleKitArmadoIngresoGuardar(oBEDetI);

                        if (oBERetornoI.Retorno != "1")
                        {


                        }

                    }

                    msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
                    //Response.Redirect(Request.RawUrl);


                }
                else
                {

                    if (oBERetorno.Retorno != "-1")
                    {
                        RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
                    }
                    else
                    {
                        msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
                    }


                }


            }

        }

    }
}