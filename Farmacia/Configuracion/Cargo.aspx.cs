
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class Cargo : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarCargo(); 
            }
        }

        #endregion

        #region Lista

        private void ListarCargo()
        {  
			BLCargo oBL = new BLCargo(); 
			gvLista.DataSource = oBL.CargoFiltroListar(txtBuscar.Text.Trim());
            gvLista.DataBind();
			upLista.Update();

		}

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarCargo();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarCargo();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
			try
			{
				Int32 pIDCargo = Int32.Parse(gvLista.SelectedDataKey["IDCargo"].ToString());
				BLCargo oBL = new BLCargo();
				BECargo oBE = oBL.Seleccionar(pIDCargo);
				hdfIDCargo.Value = pIDCargo.ToString();
				txtNombre.Text = oBE.Nombre;
				upFormulario.Update();
				registrarScript("AbrirModal('DatosCargo');");
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
				hdfIDCargo.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLCargo().CargoEliminar(Int32.Parse(hdfIDCargo.Value));
						if (oBERetorno.Retorno == "1")
						{
							ListarCargo();
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
			registrarScript("AbrirModal('DatosCargo');");
		}
        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (txtNombre.Text.Length == 0) validacion.Append("<div>Ingrese Nombre.</div>");

				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}


				BECargo oBE = new BECargo();
				BLCargo oBL = new BLCargo();
				oBE.IDCargo = Int32.Parse(hdfIDCargo.Value);
				oBE.Nombre = txtNombre.Text.Trim();
				oBE.Estado = true;
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				if (oBE.IDCargo == 0)
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
					ListarCargo();
					upLista.Update();
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
					registrarScript("CerrarModal('DatosCargo');");
				}
				else
				{
					RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardar_Click()", ex.Message, true);
			} 
        }

        private void LimpiarFormulario()
        {
            hdfIDCargo.Value = "0"; 
            txtNombre.Text = String.Empty; 
        }

		#endregion
		 
	}
}