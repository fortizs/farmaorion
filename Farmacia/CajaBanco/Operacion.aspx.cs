
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.CajaBanco
{
    public partial class Operacion : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarOperacion();
            }
        }

        #endregion

        #region Lista

        private void ListarOperacion()
        {
            BLOperacion oBL = new BLOperacion();
            gvLista.DataSource = oBL.OperacionListar(txtBuscar.Text.Trim(),"0");
            gvLista.DataBind();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarOperacion();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarOperacion();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 pIDOperacion = Int32.Parse(gvLista.SelectedDataKey["IDOperacion"].ToString());
            BLOperacion oBL = new BLOperacion();
            BEOperacion oBE = oBL.OperacionSeleccionar(pIDOperacion);
            hdfIDOperacion.Value = pIDOperacion.ToString();
            ddlTipoOperacion.SelectedValue = oBE.TipoOperacion;
            txtCodigo.Text = oBE.Codigo;
            txtNombre.Text = oBE.Nombre;
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
            if (ddlTipoOperacion.SelectedValue == "0") validacion.Append("<div>Seleccione Tipo Operación.</div>");
            if (txtCodigo.Text.Length == 0) validacion.Append("<div>Ingrese Código.</div>");
            if (txtNombre.Text.Length == 0) validacion.Append("<div>Ingrese nombre.</div>");
            if (validacion.Length > 0)
            {
                msgbox(TipoMsgBox.warning, validacion.ToString());
                return;
            }

            BEOperacion oBE = new BEOperacion();
            BLOperacion oBL = new BLOperacion();
            oBE.IDOperacion = Int32.Parse(hdfIDOperacion.Value);
            oBE.TipoOperacion = ddlTipoOperacion.SelectedValue;
            oBE.Codigo = txtCodigo.Text.Trim();
            oBE.Nombre = txtNombre.Text.Trim();
            oBE.Estado = chkEstado.Checked;
            oBE.IDUsuario = IDUsuario();
            BERetornoTran oBERetorno = new BERetornoTran();
            if (oBE.IDOperacion == 0)
            {
                oBERetorno = oBL.OperacionGuardar(oBE);
            }
            else
            {
                oBERetorno = oBL.OperacionActualizar(oBE);
            }

            if (oBERetorno.Retorno != "-1")
            {
                LimpiarFormulario();
                ListarOperacion();
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
            hdfIDOperacion.Value = "0";
            ddlTipoOperacion.SelectedIndex = -1;
            txtCodigo.Text = String.Empty;
            txtNombre.Text = String.Empty;
            chkEstado.Checked = true;
            upFormulario.Update();
        }

        #endregion

    }
}