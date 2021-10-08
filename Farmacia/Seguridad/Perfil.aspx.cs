
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BL.Seguridad;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Seguridad
{
	public partial class Perfil : PageBase
	{
		#region Load
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				ListarPerfil();
			}
		}
		#endregion

		#region Listar 
		private void ListarPerfil()
		{
			BLPerfil oBLPerfil = new BLPerfil();
			gvLista.DataSource = oBLPerfil.Listar(txtBuscar.Text.Trim());
			gvLista.DataBind();
			txtBuscar.Focus();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarPerfil();
		}

		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32 pIDPerfil = Int32.Parse(gvLista.SelectedDataKey["IDPerfil"].ToString());
			BLPerfil oBLPerfil = new BLPerfil();
			BEPerfil oBEPerfil = oBLPerfil.Seleccionar(pIDPerfil);
			LimpiarFormulario();
			txtIDPerfil.Text = oBEPerfil.IDPerfil.ToString();
			txtNombre.Text = oBEPerfil.Nombre;
			chkEstado.Checked = oBEPerfil.Estado;
			txtNombre.Focus();
			upFrm.Update();
			registrarScript("AbrirModal('DatosPerfil');");
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			ListarPerfil();
		}

		#endregion

		#region Registro 

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();
			txtNombre.Focus();
			gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('DatosPerfil');");
		}
		 
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			StringBuilder validaciones = new StringBuilder();
			if (txtNombre.Text.Trim().Length == 0)
				validaciones.Append("<div>Ingrese Nombre</div>");
			if (validaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, validaciones.ToString());
				return;
			}

			BEPerfil oBEPerfil = new BEPerfil();
			BLPerfil oBLPerfil = new BLPerfil();
			oBEPerfil.IDPerfil = Int32.Parse(txtIDPerfil.Text);
			oBEPerfil.Nombre = txtNombre.Text.Trim();
			oBEPerfil.Estado = chkEstado.Checked;
			BERetornoTran oBERetorno = new BERetornoTran();
			if (oBEPerfil.IDPerfil == 0)
			{
				oBERetorno = oBLPerfil.Insertar(oBEPerfil);
			}
			else
			{
				oBERetorno = oBLPerfil.Actualizar(oBEPerfil);
			}

			if (oBERetorno.Retorno != "-1")
			{
				LimpiarFormulario();
				ListarPerfil();
				upLista.Update();
				msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				registrarScript("ModalRegistroCerrar();");
			}
			else
			{
				RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
			}
		}

		private void LimpiarFormulario()
		{
			txtIDPerfil.Text = "0";
			txtNombre.Text = String.Empty;
			chkEstado.Checked = true;
		}

		#endregion

	}
}