
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class Procedencia : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarProcedencia(); 
            }
        }

        #endregion

        #region Lista

        private void ListarProcedencia()
        {
            BLProcedencia oBL = new BLProcedencia();
            gvLista.DataSource = oBL.ProcedenciaFiltroListar(txtBuscar.Text.Trim());
            gvLista.DataBind();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarProcedencia();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarProcedencia();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 pIDProcedencia = Int32.Parse(gvLista.SelectedDataKey["IDProcedencia"].ToString());
            BLProcedencia oBL = new BLProcedencia();
            BEProcedencia oBE = oBL.Seleccionar(pIDProcedencia);
            hdfIDProcedencia.Value = pIDProcedencia.ToString(); 
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
             
            BEProcedencia oBE = new BEProcedencia();
            BLProcedencia oBL = new BLProcedencia();
            oBE.IDProcedencia = Int32.Parse(hdfIDProcedencia.Value);
            oBE.Codigo = txtCodigo.Text.Trim();
            oBE.Nombre = txtNombre.Text.Trim(); 
            oBE.Estado = true;
            oBE.IDUsuario = IDUsuario();
            BERetornoTran oBERetorno = new BERetornoTran();
            if (oBE.IDProcedencia == 0)
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
                ListarProcedencia();
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
            hdfIDProcedencia.Value = "0";
            txtCodigo.Text = String.Empty;
            txtNombre.Text = String.Empty;
            upFormulario.Update();
        }

        #endregion

    }
}