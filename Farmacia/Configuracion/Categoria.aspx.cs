
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class Categoria : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarCategoria(); 
            }
        }

        #endregion

        #region Lista

        private void ListarCategoria()
        {
            BLCategoria oBL = new BLCategoria();
            gvLista.DataSource = oBL.CategoriaFiltroListar(txtBuscar.Text.Trim(),Int32.Parse(Session["IDEmpresa"].ToString()));
            gvLista.DataBind();
			upLista.Update();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarCategoria();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarCategoria();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
			try
			{
				Int32 pIDCategoria = Int32.Parse(gvLista.SelectedDataKey["IDCategoria"].ToString());
				BLCategoria oBL = new BLCategoria();
				BECategoria oBE = oBL.Seleccionar(pIDCategoria);
				hdfIDCategoria.Value = pIDCategoria.ToString();
				txtCodigo.Text = oBE.Codigo;
				txtNombre.Text = oBE.Nombre;
				txtCodigo.Focus();
				upFormulario.Update();
				registrarScript("AbrirModal('DatosCategoria');");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLista_RowCommand()", ex.Message, true);
			}
            
        }

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDCategoria.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLCategoria().CategoriaEliminar(Int32.Parse(hdfIDCategoria.Value));
						if (oBERetorno.Retorno == "1")
						{
							ListarCategoria();
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
			registrarScript("AbrirModal('DatosCategoria');");
		}
        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (txtCodigo.Text.Length == 0) validacion.Append("<div>Ingrese Código.</div>");
				if (txtNombre.Text.Length == 0) validacion.Append("<div>Ingrese nombre.</div>");
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BECategoria oBE = new BECategoria();
				BLCategoria oBL = new BLCategoria();
				oBE.IDCategoria = Int32.Parse(hdfIDCategoria.Value);
				oBE.Codigo = txtCodigo.Text.Trim();
				oBE.Nombre = txtNombre.Text.Trim();
				oBE.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
				oBE.Estado = true;
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				if (oBE.IDCategoria == 0)
				{
					oBERetorno = oBL.Insertar(oBE);
				}
				else
				{
					oBERetorno = oBL.Actualizar(oBE);
				}

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormulario();
					ListarCategoria();
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
					registrarScript("CerrarModal('DatosCategoria');");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else {
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
            hdfIDCategoria.Value = "0";
            txtCodigo.Text = String.Empty;
            txtNombre.Text = String.Empty;
            upFormulario.Update();
        }

		#endregion
		 
	}
}