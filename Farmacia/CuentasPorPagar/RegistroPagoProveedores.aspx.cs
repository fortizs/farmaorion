using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.Compras;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.CuentasPorPagar
{
    public partial class RegistroPagoProveedores : PageBase
    { 
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            { 
                ConfigPage();
                CargaComboRegistro();
                Listar();
                LimpiarPago(); 
            }
        }

         
        private void CargaComboRegistro()
        {
            CargarDDL(ddlBIDProveedor, new BLProveedor().ProveedorFiltroListar(IDEmpresa(), ""), "IDProveedor", "RazonSocial", true, Constantes.TODOS);
            //CargarDDL(ddlBIDConceptoPago, new BLConcepto().ConceptoListar("PAGO", ""), "IDConcepto", "Nombre", true, Constantes.TODOS);
            CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", false);
            CargarDDL(ddlIDProveedor, new BLProveedor().ProveedorFiltroListar(IDEmpresa(), ""), "IDProveedor", "RazonSocial", true, Constantes.TODOS);
            //CargarDDL(ddlIDConceptoPago, new BLConcepto().ConceptoListar("PAGO", ""), "IDConcepto", "Nombre", true, Constantes.SELECCIONAR);
            CargarDDL(ddlIDMedioPago, new BLMedioPago().MedioPagoListar(""), "IDMedioPago", "Nombre", true, Constantes.SELECCIONAR);
            CargarDDL(ddlIDBanco, new BLBanco().BancoListar(""), "IDBanco", "Nombre", true, Constantes.NINGUNO);
            CargarDDL(ddlIDMonedaPago, new BLMoneda().MonedaListar(), "IDMoneda", "Nombre", true, Constantes.SELECCIONAR);
            txtBFechaInicio.Text = DateTime.Today.AddDays(-60).ToShortDateString();
            txtBFechaFin.Text = DateTime.Today.ToShortDateString();

        }

        #endregion
         
        #region Consultas


        private void Listar()
        {
            BLCuentaPorPagar oBL = new BLCuentaPorPagar();
            gvLista.DataSource = oBL.PagoListar(Int32.Parse(ddlBIDSucursal.SelectedValue),Int32.Parse(ddlBIDProveedor.SelectedValue), txtBFechaInicio.Text.Trim(), txtBFechaFin.Text.Trim());
            gvLista.DataBind();
            upPagoListar.Update();
        }


        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            Listar();
        }

        protected void lnkBuscarDocumentoPago_Click(object sender, EventArgs e)
        {
            Listar();
        }

        #endregion

        #region Registro


        protected void LimpiarPago()
        {
            ddlIDProveedor.SelectedIndex = -1;
            txtEmail.Text = String.Empty;
            txtCelular.Text = String.Empty;
            ddlTipoFiltro.SelectedIndex = -1;
            txtFechaInicio.Text = DateTime.Today.AddDays(-30).ToShortDateString();
            txtFechaFin.Text = DateTime.Today.ToShortDateString();
            txtFiltroDocumento.Text = String.Empty;
            CompraPendientePagoXProveedorListar();
            //ddlIDConceptoPago.SelectedIndex = -1;
            ddlIDMedioPago.SelectedIndex = -1;
            ddlIDBanco.SelectedIndex = -1;
            txtCuentaCorriente.Text = String.Empty;
            txtNumeroOperacion.Text = String.Empty;
            ddlIDMonedaPago.SelectedIndex = -1;
            txtFechaPago.Text = DateTime.Today.ToShortDateString();
            txtGlosa.Text = String.Empty;
            upPagoRegistro.Update();
        }
        protected void lnkNuevoPago_Click(object sender, EventArgs e)
        {
            LimpiarPago();
        }

        protected void lnkGuardarPago_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder pValidaciones = new StringBuilder();
                if (ddlIDProveedor.SelectedValue == "0" || ddlIDProveedor.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Proveedor</div>");
                //if (ddlIDConceptoPago.SelectedValue == "0" || ddlIDConceptoPago.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Concepto de Pago</div>");
                if (ddlIDMedioPago.SelectedValue == "0" || ddlIDMedioPago.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Medio de Pago</div>");
                if (ddlIDMonedaPago.SelectedValue == "0" || ddlIDMonedaPago.SelectedIndex == -1) pValidaciones.Append("<div>Seleccione Moneda de Pago</div>");
                if (txtFechaPago.Text.Trim().Length == 0) pValidaciones.Append("<div>Ingrese Fecha de Pago</div>");
                if (txtFechaPago.Text.Trim().Length > 0) if (!esFecha(txtFechaPago.Text.Trim())) pValidaciones.Append("<div>Ingrese Fecha de Pago válida</div>");

                Boolean DocumentoSeleccionado = false;
                foreach (GridViewRow row in gvCompraPendienteProveedor.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (CheckBox)row.Cells[0].FindControl("chkSeleccionar");
                        if (chkRow.Checked)
                        {
                            DocumentoSeleccionado = true;
                        }
                    }
                }

                if (!DocumentoSeleccionado) pValidaciones.Append("<div>Debe seleccionar al menos un documento por pagar</div>");

                if (pValidaciones.Length > 0)
                {
                    msgbox(TipoMsgBox.warning, pValidaciones.ToString());
                    return;
                }


                StringBuilder pRetornoError = new StringBuilder();
                Boolean Insertar = true;

                foreach (GridViewRow row in gvCompraPendienteProveedor.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (CheckBox)row.Cells[0].FindControl("chkSeleccionar");
                        if (chkRow.Checked)
                        {
                            BEPago pBE = new BEPago();
                            pBE.IDPago = 0;
                            pBE.IDCompra = Int32.Parse(((HiddenField)row.Cells[0].FindControl("hfIDCompra")).Value);
                            pBE.IDSucursal = IDSucursal();
                            pBE.IDProveedor = Int32.Parse(ddlIDProveedor.SelectedValue);
                            //pBE.IDConceptoPago = Int32.Parse(ddlIDConceptoPago.SelectedValue);
                            pBE.IDConceptoPago = 0;
                            pBE.IDMedioPago = Int32.Parse(ddlIDMedioPago.SelectedValue);
                            pBE.IDBanco = Int32.Parse(ddlIDBanco.SelectedValue);
                            pBE.CuentaCorriente = txtCuentaCorriente.Text.Trim();
                            pBE.NumeroOperacion = txtNumeroOperacion.Text.Trim();
                            pBE.IDMonedaPago = ddlIDMonedaPago.SelectedValue;
                            pBE.FechaPago = DateTime.Parse(txtFechaPago.Text.Trim());
                            pBE.ImportePagado = Decimal.Parse(((TextBox)row.Cells[0].FindControl("txtImportePagar")).Text.Trim());
                            pBE.Saldo = Decimal.Parse(((HiddenField)row.Cells[0].FindControl("hfSaldo")).Value);
                            pBE.Glosa = txtGlosa.Text.Trim();
                            pBE.IDUsuario = IDUsuario();
                            String Serie = ((HiddenField)row.Cells[0].FindControl("hfSerie")).Value;
                            String NumeroComprobante = ((HiddenField)row.Cells[0].FindControl("hfNumeroComprobante")).Value;

                            if (pBE.ImportePagado <= 0)
                            {
                                Insertar = false;
                                pRetornoError.Append("<div>" + Serie + "-" + NumeroComprobante + " : El importe pagado no puede ser negativo o cero</div>");
                            }
                            if (pBE.ImportePagado > pBE.Saldo)
                            {
                                Insertar = false;
                                pRetornoError.Append("<div>" + Serie + "-" + NumeroComprobante + " : El importe pagado no puede ser mayor al saldo</div>");
                            }
                            if (Insertar)
                            {
                                BERetornoTran pBERetornoInsertar = new BLCuentaPorPagar().PagoGuardar(pBE);
                                if (pBERetornoInsertar.Retorno == "-1")
                                {
                                    pRetornoError.Append("<div>El documento =" + Serie + "-" + NumeroComprobante + " del proveedor " + ddlIDProveedor.SelectedItem + " no pudo ser registrado [" + RegistrarLogSistemaVer("lnkGuardarPago_Click()", pBERetornoInsertar.ErrorMensaje, false).Trim() + "].</div>");
                                }
                            }
                            Insertar = true;
                        }
                    }
                }

                if (pRetornoError.Length > 0)
                {
                    msgbox(TipoMsgBox.warning, "<div style='text-align: left;'><div><b>Se han generado estas observaciones:</b></div>" + pRetornoError.ToString() + "</div>");
                }
                else
                { 
                    msgbox(TipoMsgBox.confirmation, "La operación se realizó correctamente.");
                    Listar();
                    LimpiarPago();
                    upPagoRegistro.Update();
                }
            }
            catch (Exception ex)
            {
                RegistrarLogSistema("lnkGuardarPago_Click()", ex.ToString(), true);
            }
        }



        #endregion

        #region CompraPendientePagoProveedor
        private void CompraPendientePagoXProveedorListar()
        {
            BLCompras oBL = new BLCompras();
            gvCompraPendienteProveedor.DataSource = oBL.CompraPendientePagoXProveedorListar(Int32.Parse(ddlIDProveedor.SelectedValue), ddlTipoFiltro.SelectedValue, txtFechaInicio.Text.Trim(), txtFechaFin.Text.Trim(), txtFiltroDocumento.Text.Trim(),Constantes.ID_Forma_Pago_Credito);
            gvCompraPendienteProveedor.DataBind();
        }
        #endregion

        protected void gvCompraPendienteProveedor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompraPendienteProveedor.PageIndex = e.NewPageIndex;
            gvCompraPendienteProveedor.SelectedIndex = -1;
            CompraPendientePagoXProveedorListar();
        }

        protected void lnkBuscarDocumento_Click(object sender, EventArgs e)
        {
            CompraPendientePagoXProveedorListar();
        }

        protected void ddlIDProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            BEProveedor oBE = new BLProveedor().ProveedorSeleccionar(Int32.Parse(ddlIDProveedor.SelectedValue));
            txtEmail.Text = oBE.Correo;
            txtCelular.Text = oBE.Celular;
            CompraPendientePagoXProveedorListar();
        }
    }
}