using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BL.Seguridad;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Seguridad
{
	public partial class Parametro : PageBase
	{
		#region Load
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				ListarParametro();
			}
		}
		#endregion

		#region Listar 
		private void ListarParametro()
		{
			BLParametro oBLParametro = new BLParametro();
			gvLista.DataSource = oBLParametro.ParametroListar(txtBuscar.Text.Trim());
			gvLista.DataBind();
			txtBuscar.Focus();
		}
		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarParametro();
		}
		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{ 
			BLParametro oBLParametro = new BLParametro();
			BEParametro oBEParametro = oBLParametro.ParametroSeleccionar(gvLista.SelectedDataKey["IDParametro"].ToString());
			LimpiarFormulario();
			txtIDParametro.Text = oBEParametro.IDParametro.ToString();
			txtNombre.Text = oBEParametro.Descripcion; 
			txtValorDefecto.Text = oBEParametro.ValorDefecto;
			txtNombre.Focus();
			upFrm.Update();
			registrarScript("AbrirModal('ModalParametro');");
		}
		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			ListarParametro();
		}
		#endregion

		#region Registro 
		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();
			txtNombre.Focus();
			gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('ModalParametro');");
		}
		 
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			StringBuilder validaciones = new StringBuilder();
			if (txtNombre.Text.Trim().Length == 0)
				validaciones.Append("<div>Ingrese Nombre</div>");
			if (txtValorDefecto.Text.Trim().Length == 0)
				validaciones.Append("<div>Ingrese Valor por Defecto</div>");
			if (validaciones.Length > 0)
			{
				msgbox(TipoMsgBox.warning, validaciones.ToString());
				return;
			}

			BEParametro oBEParametro = new BEParametro();
			BLParametro oBLParametro = new BLParametro();
			oBEParametro.IDParametro = txtIDParametro.Text.Trim();
			oBEParametro.Descripcion = txtNombre.Text.Trim();
			oBEParametro.ValorDefecto = txtValorDefecto.Text.Trim(); 
			oBEParametro.IDUsuario = IDUsuario();
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = oBLParametro.ParametroGuardar(oBEParametro);

			if (oBERetorno.Retorno == "1")
			{
				LimpiarFormulario();
				ListarParametro();
				upLista.Update();
				msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				registrarScript("CerrarModal('ModalParametro');");
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
		private void LimpiarFormulario()
		{
			txtIDParametro.Text = "0";
			txtNombre.Text = String.Empty; 
			txtValorDefecto.Text = String.Empty;
		}
		#endregion

	}
}