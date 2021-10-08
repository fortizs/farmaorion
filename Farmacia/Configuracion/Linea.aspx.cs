
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class Linea : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarLinea(); 
            }
        }

        #endregion

        #region Lista

        private void ListarLinea()
        {
            BLLinea oBL = new BLLinea();
            gvLista.DataSource = oBL.LineaFiltroListar(txtBuscar.Text.Trim(), Int32.Parse(Session["IDEmpresa"].ToString()));
            gvLista.DataBind();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarLinea();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarLinea();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 pIDLinea = Int32.Parse(gvLista.SelectedDataKey["IDLinea"].ToString());
            BLLinea oBL = new BLLinea();
            BELinea oBE = oBL.Seleccionar(pIDLinea);
            hdfIDLinea.Value = pIDLinea.ToString(); 
            txtCodigo.Text = oBE.Codigo;
            txtNombre.Text = oBE.Nombre;
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
            if (validacion.Length > 0)
            {
                msgbox(TipoMsgBox.warning, validacion.ToString());
                return;
            }
             
            BELinea oBE = new BELinea();
            BLLinea oBL = new BLLinea();
            oBE.IDLinea = Int32.Parse(hdfIDLinea.Value);
            oBE.Codigo = txtCodigo.Text.Trim();
            oBE.Nombre = txtNombre.Text.Trim();
            oBE.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
            oBE.Estado = true;
            oBE.IDUsuario = IDUsuario();
            BERetornoTran oBERetorno = new BERetornoTran();
            if (oBE.IDLinea == 0)
            {
                oBERetorno = oBL.Insertar(oBE);
            }
            else
            {
                oBERetorno = oBL.Actualizar(oBE);
            }

            if (oBERetorno.Retorno != "-1")
            {
                LimpiarFormulario();
                ListarLinea();
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
            hdfIDLinea.Value = "0";
            txtCodigo.Text = String.Empty;
            txtNombre.Text = String.Empty;
            upFormulario.Update();
        }

        #endregion

    }
}