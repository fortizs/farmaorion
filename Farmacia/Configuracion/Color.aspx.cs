
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class Color : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarColor(); 
            }
        }

        #endregion

        #region Lista

        private void ListarColor()
        {
            BLColor oBL = new BLColor();
            gvLista.DataSource = oBL.ColorListar(IDEmpresa());
            gvLista.DataBind();
			upLista.Update(); 
		}

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarColor();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarColor();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 pIDColor = Int32.Parse(gvLista.SelectedDataKey["IDColor"].ToString());
            BLColor oBL = new BLColor();
            BEColor oBE = oBL.ColorSeleccionar(pIDColor);
            hdfIDColor.Value = pIDColor.ToString();  
            txtNombre.Text = oBE.Nombre;
			chkEstado.Checked = oBE.Estado;
            upFormulario.Update();
            registrarScript("AbrirModal('DatosColor');");
        }

        #endregion

        #region Registrar

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
            gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('DatosColor');");
		}
         
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            StringBuilder validacion = new StringBuilder(); 
            if (txtNombre.Text.Length == 0) validacion.Append("<div>Ingrese nombre.</div>"); 
            if (validacion.Length > 0)
            {
                msgbox(TipoMsgBox.warning, validacion.ToString());
                return;
            }
             
            BEColor oBE = new BEColor();
            BLColor oBL = new BLColor();
            oBE.IDColor = Int32.Parse(hdfIDColor.Value); 
            oBE.Nombre = txtNombre.Text.Trim();
            oBE.IDEmpresa = IDEmpresa();
            oBE.Estado = chkEstado.Checked;
            oBE.IDUsuario = IDUsuario();
            BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = oBL.ColorGuardar(oBE); 

			if (oBERetorno.Retorno == "1")
            {
                LimpiarFormulario();
                ListarColor(); 
                msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				registrarScript("CerrarModal('DatosColor');");
			}
            else
            {
				if (oBERetorno.Retorno != "-1")
				{ 
					msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje); 
				}
				else
				{
					RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
				} 
            }
        }

        private void LimpiarFormulario()
        {
            hdfIDColor.Value = "0"; 
            txtNombre.Text = String.Empty;
			chkEstado.Checked = true;
            upFormulario.Update();
        }

        #endregion

    }
}