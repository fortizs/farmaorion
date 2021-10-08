
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class Moneda : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarMoneda();
            }
        }

        #endregion

        #region Lista

        private void ListarMoneda()
        {
            BLMoneda oBL = new BLMoneda();
            gvLista.DataSource = oBL.MonedaListar();
            gvLista.DataBind();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarMoneda();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarMoneda();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            String pIDMoneda = gvLista.SelectedDataKey["IDMoneda"].ToString();
            BLMoneda oBL = new BLMoneda();
            BEMoneda oBE = oBL.MonedaSeleccionar(pIDMoneda);
            hdfIDMoneda.Value = pIDMoneda.ToString();
            txtCodigo.Text = oBE.IDMoneda;
            txtNombre.Text = oBE.Nombre;
            txtSimbolo.Text = oBE.NombreCorto;
            chkEstado.Checked = oBE.Estado;
            txtCodigo.Enabled = false;
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
            if (txtSimbolo.Text.Length == 0) validacion.Append("<div>Ingrese Simbolo.</div>");
            if (validacion.Length > 0)
            {
                msgbox(TipoMsgBox.warning, validacion.ToString());
                return;
            }

            BEMoneda oBE = new BEMoneda();
            BLMoneda oBL = new BLMoneda();
            oBE.IDMoneda = txtCodigo.Text.Trim();
            oBE.NombreCorto = txtSimbolo.Text.Trim();
            oBE.Nombre = txtNombre.Text.Trim(); 
            oBE.Estado = chkEstado.Checked;
            oBE.IDUsuario = IDUsuario();
            BERetornoTran oBERetorno = new BERetornoTran();
            if (hdfIDMoneda.Value == "0")
            {
                oBERetorno = oBL.MonedaGuardar(oBE);
            }
            else
            {
                oBERetorno = oBL.MonedaActualizar(oBE);
            }

            if (oBERetorno.Retorno != "-1")
            {
                LimpiarFormulario();
                ListarMoneda();
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
            hdfIDMoneda.Value = "0";
            txtCodigo.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtSimbolo.Text = String.Empty;
            chkEstado.Checked = true;
            txtCodigo.Enabled = true;
            upFormulario.Update();
        }

        #endregion

    }
}