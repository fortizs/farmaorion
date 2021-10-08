
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
	public partial class Proveedor : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				ListarProveedor();
				CargarDDL(ddlIDTipoDocumento, new BLTipoDocumento().TipoDocumentoListar(), "IDTipoDocumento", "Nombre");
				CargarDDL(ddlIDCategoria, new BLCategoria().Listar(IDEmpresa()), "IDCategoria", "Nombre", true, Constantes.SELECCIONAR);
			}
		}

		#endregion

		#region Lista

		private void ListarProveedor()
		{
			BLProveedor oBLProveedor = new BLProveedor();
			gvLista.DataSource = oBLProveedor.ProveedorFiltroListar(Int32.Parse(Session["IDEmpresa"].ToString()), txtBuscar.Text.Trim());
			gvLista.DataBind();
			upLista.Update();
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			ListarProveedor();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			ListarProveedor();
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDProveedor.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();
				switch (e.CommandName)
				{
					case "Categoria":
						LimpiarProveedorCategoria();
						ListarProveedorCategoria();
						registrarScript("AbrirModal('DatosProveedorCategoria');");
						break;
					case "Eliminar":
						oBERetorno = new BLProveedor().ProveedorEliminar(Int32.Parse(hdfIDProveedor.Value));
						if (oBERetorno.Retorno == "1")
						{
							ListarProveedor();
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

		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				Int32 pIDProveedor = Int32.Parse(gvLista.SelectedDataKey["IDProveedor"].ToString());
				BLProveedor oBL = new BLProveedor();
				BEProveedor oBE = oBL.ProveedorSeleccionar(pIDProveedor);
				hdfIDProveedor.Value = pIDProveedor.ToString();
				ddlIDTipoDocumento.SelectedValue = oBE.IDTipoDocumento.ToString();
				txtNumeroDocumento.Text = oBE.NumeroDocumento;
				txtRazonSocial.Text = oBE.RazonSocial;
				txtNombreComercial.Text = oBE.NombreComercial;
				hdfRegIDUbigeo.Value = oBE.IDUbigeo;
				txtRegUbigeo.Text = oBE.Ubigeo;
				txtDireccion.Text = oBE.Direccion.ToString();
				txtUrbanizacion.Text = oBE.Urbanizacion.ToString();
				txtCorreo.Text = oBE.Correo.ToString();
				txtCelular.Text = oBE.Celular; 
				txtNumeroDocumento.Focus();
				upFormulario.Update();
				registrarScript("AbrirModal('DatosProveedor');");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLista_SelectedIndexChanged()", ex.Message, true);
			}
		}

		#endregion

		#region Registrar

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();
			txtNumeroDocumento.Focus();
			gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('DatosProveedor');");
		}

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (txtNumeroDocumento.Text.Length == 0) validacion.Append("<div>Ingrese Numero Documento.</div>");
				if (txtRazonSocial.Text.Length == 0) validacion.Append("<div>Ingrese nombre o razon social.</div>");
				if (txtNombreComercial.Text.Length == 0) validacion.Append("<div>Ingrese Nombre Comercial</div>");

				if (ddlIDTipoDocumento.SelectedValue.Equals("3"))
				{
					if (txtNumeroDocumento.Text.Trim().Length != 11 || !esDouble(txtNumeroDocumento.Text.Trim())) validacion.Append("<div>Ingrese un Ruc válido.</div>");
				}
				else {
					if (txtNumeroDocumento.Text.Length != 8 || !esDouble(txtNumeroDocumento.Text.Trim())) validacion.Append("<div>Ingrese un número Documento válido.</div>");
				}

				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEProveedor oBE = new BEProveedor();
				BLProveedor oBL = new BLProveedor();
				oBE.IDProveedor = Int32.Parse(hdfIDProveedor.Value);
				oBE.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
				oBE.IDTipoDocumento = Int32.Parse(ddlIDTipoDocumento.SelectedValue);
				oBE.NumeroDocumento = txtNumeroDocumento.Text.Trim();
				oBE.RazonSocial = txtRazonSocial.Text.Trim();
				oBE.NombreComercial = txtNombreComercial.Text.Trim();
				oBE.IDUbigeo = hdfRegIDUbigeo.Value;
				oBE.Direccion = txtDireccion.Text;
				oBE.Urbanizacion = txtUrbanizacion.Text;
				oBE.Correo = txtCorreo.Text;
				oBE.Celular = txtCelular.Text;
				oBE.Estado = true;
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				if (oBE.IDProveedor == 0)
				{
					oBERetorno = oBL.ProveedorGuardar(oBE);
				}
				else
				{
					oBERetorno = oBL.ProveedorActualizar(oBE);
				}

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormulario();
					ListarProveedor();
					upLista.Update();
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
					registrarScript("CerrarModal('DatosProveedor');");
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
			hdfIDProveedor.Value = "0";
			hdfRegIDUbigeo.Value = "";
			txtRegUbigeo.Text = "";
			txtNumeroDocumento.Text = String.Empty;
			txtRazonSocial.Text = String.Empty;
			txtNombreComercial.Text = String.Empty;
			txtDireccion.Text = String.Empty;
			txtUrbanizacion.Text = String.Empty;
			txtCorreo.Text = String.Empty;
			txtCelular.Text = String.Empty;
			ddlIDTipoDocumento.SelectedIndex = -1;
			upFormulario.Update();
		}

		#endregion

		#region Categoria

		private void ListarProveedorCategoria()
		{
			BLProveedorCategoria oBL = new BLProveedorCategoria();
			gvProveedorCategoriaListar.DataSource = oBL.ProveedorCategoriaListar(Int32.Parse(hdfIDProveedor.Value));
			gvProveedorCategoriaListar.DataBind();
		}

		protected void gvProveedorCategoriaListar_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvProveedorCategoriaListar.PageIndex = e.NewPageIndex;
			gvProveedorCategoriaListar.SelectedIndex = -1;
			ListarProveedorCategoria();
		}

		protected void btnAgregarProveedorCategoria_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (hdfIDProveedor.Value.Length == 0) validacion.Append("<div>Seleccione Proveedor.</div>");
				if (ddlIDCategoria.SelectedValue == "0" || ddlIDCategoria.SelectedValue == "-1") validacion.Append("<div>Seleccione Categoria.</div>");

				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEProveedorCategoria oBE = new BEProveedorCategoria();
				BLProveedorCategoria oBL = new BLProveedorCategoria();
				oBE.IDProveedorCategoria = Int32.Parse(hdfIDProveedorCategoria.Value);
				oBE.IDProveedor = Int32.Parse(hdfIDProveedor.Value);
				oBE.IDCategoria = Int32.Parse(ddlIDCategoria.SelectedValue);
				oBE.Estado = true;
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				if (oBE.IDProveedorCategoria == 0)
				{
					oBERetorno = oBL.ProveedorCategoriaGuardar(oBE);
				}

				if (oBERetorno.Retorno == "1")
				{
					ddlIDCategoria.SelectedIndex = -1;
					ListarProveedor();
					ListarProveedorCategoria();
					upLista.Update();
					msgbox(TipoMsgBox.confirmation, "Sistema", "La operación se realizó con éxito.");
				}
				else {
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnAgregarProveedorCategoria_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnAgregarProveedorCategoria_Click()", ex.Message, true);
			} 
		}

		protected void LimpiarProveedorCategoria()
		{
			hdfIDProveedorCategoria.Value = "0";
			ddlIDCategoria.SelectedIndex = -1;
			upProveedorCategoria.Update();
		}

		#endregion


	}
}