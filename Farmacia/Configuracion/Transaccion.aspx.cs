using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
	public partial class Transaccion : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				TransaccionListar();
			}
		}

		#endregion

		#region Lista

		private void TransaccionListar()
		{
			BLTransaccion oBL = new BLTransaccion();
			gvLista.DataSource = oBL.TransaccionListar(ddlBTipoMovimiento.SelectedValue);
			gvLista.DataBind();
			upLista.Update();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			TransaccionListar();
		}

		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				hdfIDTransaccion.Value = gvLista.SelectedDataKey["IDTransaccion"].ToString();
				BETransaccion oBE = new BLTransaccion().TransaccionSeleccionar(Int32.Parse(hdfIDTransaccion.Value));
				txtCodigo.Text = oBE.Codigo;
				txtNombre.Text = oBE.Nombre;
				ddlTipoMovimiento.SelectedValue = oBE.TipoMovimiento;
				txtCodigo.Focus();
				upFormulario.Update();
				registrarScript("AbrirModal('ModalTransaccion');");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardar_Click()", ex.Message, true);
			}
			
		}
		 
		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDTransaccion.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLTransaccion().TransaccionEliminar(Int32.Parse(hdfIDTransaccion.Value));
						if (oBERetorno.Retorno == "1")
						{
							TransaccionListar();
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

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			TransaccionListar();
		}

		#endregion

		#region Registrar

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();
			txtNombre.Focus();
			gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('ModalTransaccion');");
		}

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (txtCodigo.Text.Length == 0) validacion.Append("<div>Ingrese Código.</div>");
				if (ddlTipoMovimiento.SelectedValue == "0") validacion.Append("<div>Seleccione Tipo Movimiento.</div>");
				if (txtNombre.Text.Length == 0) validacion.Append("<div>Ingrese nombre.</div>");
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BETransaccion oBE = new BETransaccion();
				BLTransaccion oBL = new BLTransaccion();
				oBE.IDTransaccion = Int32.Parse(hdfIDTransaccion.Value);
				oBE.Codigo = txtCodigo.Text.Trim();
				oBE.Nombre = txtNombre.Text.Trim();
				oBE.TipoMovimiento = ddlTipoMovimiento.SelectedValue;
				oBE.IDEmpresa = IDEmpresa();
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.TransaccionGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormulario();
					TransaccionListar();
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
					registrarScript("CerrarModal('ModalTransaccion');");
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
			hdfIDTransaccion.Value = "0";
			txtCodigo.Text = String.Empty;
			txtNombre.Text = String.Empty;
			ddlTipoMovimiento.SelectedIndex = -1;
			upFormulario.Update();
		}

		#endregion

	}
}