using Farmacia.App_Class;
using Farmacia.App_Class.BE.Caja;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.Caja;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.CajaBanco
{
	public partial class CajaMecanica : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
				ListarCaja();
			}
		}

		private void CargaInicial()
		{ 
		    CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
			CargarDDL(ddlIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.SELECCIONAR); 
		}
		 
		#endregion

		#region Lista

		private void ListarCaja()
		{
			BLCajaMecanica oBL = new BLCajaMecanica();
			gvLista.DataSource = oBL.CajaMecanicaListar(txtBuscar.Text.Trim(), Int32.Parse(ddlBIDSucursal.SelectedValue));
			gvLista.DataBind();
		}
		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			ListarCaja();
		}
		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarCaja();
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			hdfIDCajaMecanica.Value = e.CommandArgument.ToString(); 
			upLista.Update();
			BERetornoTran oBERetorno = new BERetornoTran();
			switch (e.CommandName)
			{
				case "Editar": 
					BECajaMecanica oBE = new BLCajaMecanica().CajaMecanicaSeleccionar(Int32.Parse(hdfIDCajaMecanica.Value));
					ddlIDSucursal.SelectedValue = oBE.IDSucursal.ToString();
					txtCodigo.Text = oBE.Codigo;
					txtNombre.Text = oBE.Nombre; 
					chkEstado.Checked = oBE.Estado;
					txtCodigo.Focus();
					upFormulario.Update();
					registrarScript("AbrirModal('ModalCajaMecanica');");
					break;
				case "Eliminar":
					oBERetorno = new BLCajaMecanica().CajaMecanicaEliminar(Int32.Parse(hdfIDCajaMecanica.Value));
					if (oBERetorno.Retorno == "1")
					{
						ListarCaja();
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

		#endregion

		#region Registrar

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();
			txtNombre.Focus();
			gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('ModalCajaMecanica');");
		}
		 
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			StringBuilder validacion = new StringBuilder();
			if (ddlIDSucursal.SelectedValue == "0") validacion.Append("<div>Seleccione Sucursal.</div>");
			if (txtCodigo.Text.Length == 0) validacion.Append("<div>Ingrese Código.</div>");
			if (txtNombre.Text.Length == 0) validacion.Append("<div>Ingrese nombre.</div>"); 
			if (validacion.Length > 0)
			{
				msgbox(TipoMsgBox.warning, validacion.ToString());
				return;
			}

			BECajaMecanica oBE = new BECajaMecanica(); 
			oBE.IDCajaMecanica = Int32.Parse(hdfIDCajaMecanica.Value);
			oBE.IDSucursal = Int32.Parse(ddlIDSucursal.SelectedValue);
			oBE.Codigo = txtCodigo.Text.Trim();
			oBE.Nombre = txtNombre.Text.Trim();
			oBE.Responsable = "";
			oBE.Estado = chkEstado.Checked;
			oBE.IDUsuario = IDUsuario();
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = new BLCajaMecanica().CajaMecanicaGuardar(oBE);
			  
			if (oBERetorno.Retorno != "-1")
			{
				LimpiarFormulario();
				ListarCaja();
				upLista.Update();
				msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				registrarScript("CerrarModal('ModalCajaMecanica');");
			}
			else
			{
				RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
			}
		}
		private void LimpiarFormulario()
		{
			hdfIDCajaMecanica.Value = "0";
			ddlIDSucursal.SelectedIndex = -1;
			txtCodigo.Text = String.Empty;
			txtNombre.Text = String.Empty; 
			chkEstado.Checked = true;
			upFormulario.Update();
		}

		#endregion
		 
	}
}