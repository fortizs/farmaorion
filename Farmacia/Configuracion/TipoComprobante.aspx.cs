
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.Contabilidad;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class TipoComprobante : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                 ListarTipoComprobante();
            }
        }

        #endregion

        #region Lista

        private void ListarTipoComprobante()
        {
            BLTipoComprobante oBL = new BLTipoComprobante();
            gvLista.DataSource = oBL.TipoComprobanteListar("0","VEN");
            gvLista.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarTipoComprobante();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarTipoComprobante();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
			try
			{
				Int32 pIDTipoComprobante = Int32.Parse(gvLista.SelectedDataKey["IDTipoComprobante"].ToString());
				BLTipoComprobante oBL = new BLTipoComprobante();
				BETipoComprobante oBE = oBL.TipoComprobanteSeleccionar(pIDTipoComprobante);
				hdfIDTipoComprobante.Value = pIDTipoComprobante.ToString();
				txtSigla.Text = oBE.Sigla;
				txtNombre.Text = oBE.Nombre;
				txtCodigoSunat.Text = oBE.CodigoSunat.ToString();
				chkEstado.Checked = oBE.Estado;
				txtNombre.Focus();
				upFormulario.Update();
				registrarScript("AbrirModal('DatosTipoComprobante');");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLista_SelectedIndexChanged()", ex.Message, true);
			}
           
        }

        #endregion

        #region Registrar

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
            gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('DatosTipoComprobante');");
		}
         
        private void LimpiarFormulario()
        {
            hdfIDTipoComprobante.Value = "0";
            txtSigla.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtCodigoSunat.Text = String.Empty;
            txtIndDoc.Text = String.Empty; 
            chkEstado.Checked = true;
            upFormulario.Update();
        }

        #endregion

        protected void btnGuardarComprobante_Click(object sender, EventArgs e)
        {
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (txtSigla.Text.Length == 0) validacion.Append("<div>Ingrese Sigla.</div>");
				if (txtNombre.Text.Length == 0) validacion.Append("<div>Ingrese nombre.</div>");

				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}
				BETipoComprobante oBE = new BETipoComprobante();
				BLTipoComprobante oBL = new BLTipoComprobante();
				oBE.IDTipoComprobante = Int32.Parse(hdfIDTipoComprobante.Value);
				oBE.Sigla = txtSigla.Text.Trim();
				oBE.Nombre = txtNombre.Text.Trim();
				oBE.CodigoSunat = txtCodigoSunat.Text.Trim();
				oBE.IndDoc = txtIndDoc.Text;
				oBE.IDTipoComprobanteContabilidad = 0;
				oBE.Estado = chkEstado.Checked;
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				if (oBE.IDTipoComprobante == 0)
				{
					oBERetorno = oBL.TipoComprobanteGuardar(oBE);
				}
				else
				{
					oBERetorno = oBL.TipoComprobanteActualizar(oBE);
				}

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormulario();
					ListarTipoComprobante();
					upLista.Update();
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
					registrarScript("CerrarModal('DatosTipoComprobante');");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarComprobante_Click()", oBERetorno.ErrorMensaje, true);
					} 
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarComprobante_Click()", ex.Message, true);
			}
           
        }

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
            hdfIDTipoComprobante.Value = e.CommandArgument.ToString();
            BERetornoTran oBERetorno = new BERetornoTran();
            switch (e.CommandName)
            { 
                case "Eliminar":
                    oBERetorno = new BLTipoComprobante().TipoComprobanteEliminar(Int32.Parse(hdfIDTipoComprobante.Value), IDUsuario());
                    if (oBERetorno.Retorno == "1")
                    {
                        ListarTipoComprobante();
                        msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
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
	}
}