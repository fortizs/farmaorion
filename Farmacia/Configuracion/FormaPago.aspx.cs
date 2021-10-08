
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class FormaPago : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarFormaPago();
            }
        }

        #endregion

        #region Lista

        private void ListarFormaPago()
        {
            BLFormaPago oBL = new BLFormaPago();
            gvLista.DataSource = oBL.FormaPagoListar();
            gvLista.DataBind();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarFormaPago();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarFormaPago();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 pIDFormaPago = Int32.Parse(gvLista.SelectedDataKey["IDFormaPago"].ToString());
            BLFormaPago oBL = new BLFormaPago();
            BEFormaPago oBE = oBL.FormaPagoSeleccionar(pIDFormaPago);
            hdfIDFormaPago.Value = pIDFormaPago.ToString();
            txtCodigo.Text = oBE.Codigo;
            txtNombre.Text = oBE.Nombre;
            txtNumeroDia.Text = oBE.NumeroDia.ToString();
            chkEstado.Checked = oBE.Estado;
            txtCodigo.Focus();
            upFormulario.Update();
            registrarScript("funModalAbrir();");
        }

        #endregion

        #region Registrar

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
            gvLista.SelectedIndex = -1;
            registrarScript("funModalAbrir();");
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            registrarScript("funModalCerrar();");
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            StringBuilder validacion = new StringBuilder();
            if (txtCodigo.Text.Length == 0) validacion.Append("<div>Ingrese Código.</div>");
            if (txtNombre.Text.Length == 0) validacion.Append("<div>Ingrese nombre.</div>");
            if (txtNumeroDia.Text.Length == 0) validacion.Append("<div>Ingrese Números de dias.</div>");

            if (txtNumeroDia.Text.Length > 0) {
                if (!esInt32(txtNumeroDia.Text)) validacion.Append("<div>El Número de dias no es válido.</div>"); 
            }

            if (validacion.Length > 0)
            {
                msgbox(TipoMsgBox.warning, validacion.ToString());
                return;
            }

            BEFormaPago oBE = new BEFormaPago();
            BLFormaPago oBL = new BLFormaPago();
            oBE.IDFormaPago = Int32.Parse(hdfIDFormaPago.Value);
            oBE.Codigo = txtCodigo.Text.Trim();
            oBE.Nombre = txtNombre.Text.Trim();
            oBE.NumeroDia = Int32.Parse(txtNumeroDia.Text);
            oBE.Estado = chkEstado.Checked;
            oBE.IDUsuario = IDUsuario();
            BERetornoTran oBERetorno = new BERetornoTran();
            if (oBE.IDFormaPago == 0)
            {
                oBERetorno = oBL.FormaPagoGuardar(oBE);
            }
            else
            {
                oBERetorno = oBL.FormaPagoActualizar(oBE);
            }

            if (oBERetorno.Retorno != "-1")
            {
                LimpiarFormulario();
                ListarFormaPago();
                upLista.Update();
                msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
                registrarScript("funModalCerrar();");
            }
            else
            {
                RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
            }
        }
        private void LimpiarFormulario()
        {
            hdfIDFormaPago.Value = "0";
            txtCodigo.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtNumeroDia.Text = "0";
            chkEstado.Checked = true;
            upFormulario.Update();
        }

        #endregion

    }
}