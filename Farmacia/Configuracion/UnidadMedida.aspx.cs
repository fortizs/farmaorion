
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class UnidadMedida : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarUnidadMedida(); 
            }
        } 
        #endregion

        #region Lista

        private void ListarUnidadMedida()
        {
            BLUnidadMedida oBL = new BLUnidadMedida();
            gvLista.DataSource = oBL.UnidadMedidaListar(txtBuscar.Text.Trim(), IDEmpresa());
            gvLista.DataBind();
			upLista.Update();

		}

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarUnidadMedida();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarUnidadMedida();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
			try
			{
				hdfIDUnidadMedida.Value = gvLista.SelectedDataKey["IDUnidadMedida"].ToString();
				BLUnidadMedida oBL = new BLUnidadMedida();
				BEUnidadMedida oBE = oBL.UnidadMedidaSeleccionar(Int32.Parse(hdfIDUnidadMedida.Value));
				txtCodigo.Text = oBE.Codigo;
				txtNombreCorto.Text = oBE.NombreCorto;
				txtNombre.Text = oBE.Nombre;
				txtCodigoSunat.Text = oBE.CodigoSunat;
				txtCodigo.Focus();
				upFormulario.Update();
				registrarScript("AbrirModal('DatosUnidad');");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLista_SelectedIndexChanged()", ex.Message, true);
			} 
        }

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDUnidadMedida.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLUnidadMedida().UnidadMedidaEliminar(Int32.Parse(hdfIDUnidadMedida.Value));
						if (oBERetorno.Retorno == "1")
						{
							ListarUnidadMedida();
							msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
						}
						else
						{
							if (oBERetorno.Retorno != "-1")
							{
								msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
							}
							else {
								RegistrarLogSistema("gvLista_RowCommand()", oBERetorno.ErrorMensaje, true);
							}
						}
						break;
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLista_RowCommand()", ex.Message, true);
			}
		}

		#endregion

		#region Registrar

		protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
            gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('DatosUnidad');");
		}
        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (txtCodigo.Text.Length == 0) validacion.Append("<div>Ingrese Código.</div>");
				if (txtNombreCorto.Text.Length == 0) validacion.Append("<div>Ingrese Nombre Corto.</div>");
				if (txtNombreCorto.Text.Length > 5) validacion.Append("<div>Nombre corto debe tener máximo 5 caracteres.</div>");
				if (txtNombre.Text.Length == 0) validacion.Append("<div>Ingrese nombre.</div>");
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEUnidadMedida oBE = new BEUnidadMedida();
				BLUnidadMedida oBL = new BLUnidadMedida();
				oBE.IDUnidadMedida = Int32.Parse(hdfIDUnidadMedida.Value);
				oBE.Codigo = txtCodigo.Text.Trim();
				oBE.NombreCorto = txtNombreCorto.Text.Trim();
				oBE.Nombre = txtNombre.Text.Trim();
				oBE.CodigoSunat = txtCodigoSunat.Text.Trim();
				oBE.Estado = true;
				oBE.IDEmpresa = IDEmpresa();
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.UnidadMedidaGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormulario();
					ListarUnidadMedida();
					msgbox(TipoMsgBox.confirmation, "Sistema", Constantes.MENSAJE_EXITO);
					registrarScript("CerrarModal('DatosUnidad');");
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
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardar_Click()", ex.Message, true);
			} 
        }

        private void LimpiarFormulario()
        {
            hdfIDUnidadMedida.Value = "0";
            txtCodigo.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtNombreCorto.Text = String.Empty;
			txtCodigoSunat.Text = String.Empty;
			upFormulario.Update();
        }

		#endregion
		 
	}
}