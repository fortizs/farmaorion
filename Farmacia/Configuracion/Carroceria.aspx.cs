
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
	public partial class Carroceria : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				ListarCarroceria();
			}
		}

		#endregion

		#region Lista

		private void ListarCarroceria()
		{
			BLCarroceria oBL = new BLCarroceria();
			gvLista.DataSource = oBL.CarroceriaListar(IDEmpresa());
			gvLista.DataBind();
			upLista.Update();
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			ListarCarroceria();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarCarroceria();
		}

		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32 pIDCarroceria = Int32.Parse(gvLista.SelectedDataKey["IDCarroceria"].ToString());
			BLCarroceria oBL = new BLCarroceria();
			BECarroceria oBE = oBL.CarroceriaSeleccionar(pIDCarroceria);
			hdfIDCarroceria.Value = pIDCarroceria.ToString();
			txtNombre.Text = oBE.Nombre;
			chkEstado.Checked = oBE.Estado;
			upFormulario.Update();
			registrarScript("AbrirModal('DatosCarroceria');");
		}

		#endregion

		#region Registrar

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();
			txtNombre.Focus();
			gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('DatosCarroceria');");
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

			BECarroceria oBE = new BECarroceria();
			BLCarroceria oBL = new BLCarroceria();
			oBE.IDCarroceria = Int32.Parse(hdfIDCarroceria.Value);
			oBE.Nombre = txtNombre.Text.Trim();
			oBE.IDEmpresa = IDEmpresa();
			oBE.Estado = chkEstado.Checked;
			oBE.IDUsuario = IDUsuario();
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = oBL.CarroceriaGuardar(oBE);

			if (oBERetorno.Retorno == "1")
			{
				LimpiarFormulario();
				ListarCarroceria();
				msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				registrarScript("CerrarModal('DatosCarroceria');");
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
			hdfIDCarroceria.Value = "0";
			txtNombre.Text = String.Empty;
			chkEstado.Checked = true;
			upFormulario.Update();
		}

		#endregion

	}
}