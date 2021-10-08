using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
	public partial class Banco : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				ListarBanco();
			}
		}

		#endregion

		#region Lista

		private void ListarBanco()
		{
			BLBanco oBL = new BLBanco();
			gvLista.DataSource = oBL.BancoListar(txtBuscar.Text.Trim());
			gvLista.DataBind();
			upLista.Update();
		}
		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			ListarBanco();
		}
		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarBanco();
		}
		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32 pIDBanco = Int32.Parse(gvLista.SelectedDataKey["IDBanco"].ToString());
			BLBanco oBL = new BLBanco();
			BEBanco oBE = oBL.BancoSeleccionar(pIDBanco);
			hdfIDBanco.Value = pIDBanco.ToString();
			txtCodigo.Text = oBE.Codigo;
			txtNombre.Text = oBE.Nombre;
			chkEstado.Checked = oBE.Estado;
			txtCodigo.Focus();
			upFormulario.Update();
			registrarScript("funModalAbrir();");
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDBanco.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLBanco().BancoEliminar(Int32.Parse(hdfIDBanco.Value));
						if (oBERetorno.Retorno == "1")
						{
							ListarBanco();
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

			BEBanco oBE = new BEBanco();
			BLBanco oBL = new BLBanco();
			oBE.IDBanco = Int32.Parse(hdfIDBanco.Value);
			oBE.Codigo = txtCodigo.Text.Trim();
			oBE.Nombre = txtNombre.Text.Trim();
			oBE.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
			oBE.Estado = chkEstado.Checked;
			oBE.IDUsuario = IDUsuario();
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = oBL.BancoGuardar(oBE);

			if (oBERetorno.Retorno == "1")
			{
				LimpiarFormulario();
				ListarBanco();
				msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				registrarScript("funModalCerrar();");
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
			hdfIDBanco.Value = "0";
			txtCodigo.Text = String.Empty;
			txtNombre.Text = String.Empty;
			chkEstado.Checked = true;
			upFormulario.Update();
		}

		#endregion

	}
}