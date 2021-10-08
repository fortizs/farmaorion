
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
	public partial class TipoCombustible : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				ListarTipoCombustible();
			}
		}

		#endregion

		#region Lista

		private void ListarTipoCombustible()
		{
			BLTipoCombustible oBL = new BLTipoCombustible();
			gvLista.DataSource = oBL.TipoCombustibleListar(IDEmpresa());
			gvLista.DataBind();
			upLista.Update();
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			ListarTipoCombustible();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarTipoCombustible();
		}

		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32 pIDTipoCombustible = Int32.Parse(gvLista.SelectedDataKey["IDTipoCombustible"].ToString());
			BLTipoCombustible oBL = new BLTipoCombustible();
			BETipoCombustible oBE = oBL.TipoCombustibleSeleccionar(pIDTipoCombustible);
			hdfIDTipoCombustible.Value = pIDTipoCombustible.ToString();
			txtNombre.Text = oBE.Nombre;
			chkEstado.Checked = oBE.Estado;
			upFormulario.Update();
			registrarScript("AbrirModal('DatosTipoCombustible');");
		}

		#endregion

		#region Registrar

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();
			txtNombre.Focus();
			gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('DatosTipoCombustible');");
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

			BETipoCombustible oBE = new BETipoCombustible();
			BLTipoCombustible oBL = new BLTipoCombustible();
			oBE.IDTipoCombustible = Int32.Parse(hdfIDTipoCombustible.Value);
			oBE.Nombre = txtNombre.Text.Trim();
			oBE.IDEmpresa = IDEmpresa();
			oBE.Estado = chkEstado.Checked;
			oBE.IDUsuario = IDUsuario();
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = oBL.TipoCombustibleGuardar(oBE);

			if (oBERetorno.Retorno == "1")
			{
				LimpiarFormulario();
				ListarTipoCombustible();
				msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				registrarScript("CerrarModal('DatosTipoCombustible');");
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
			hdfIDTipoCombustible.Value = "0";
			txtNombre.Text = String.Empty;
			chkEstado.Checked = true;
			upFormulario.Update();
		}

		#endregion

	}
}