using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Inventario;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
	public partial class Almacen : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
                CargarDDL(ddlIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", false);
                AlmacenListar();
			}
		}

		#endregion

		#region Lista

		private void AlmacenListar()
		{
			BLAlmacen oBL = new BLAlmacen();
			gvLista.DataSource = oBL.AlmacenListar(0);
			gvLista.DataBind();
			upLista.Update();
		}

		protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvLista.PageIndex = e.NewPageIndex;
			gvLista.SelectedIndex = -1;
			AlmacenListar();
		}

		protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			hdfIDAlmacen.Value = gvLista.SelectedDataKey["IDAlmacen"].ToString(); 
			BEAlmacen oBE = new BLAlmacen().AlmacenSeleccionar(Int32.Parse(hdfIDAlmacen.Value));
            ddlIDSucursal.SelectedValue = oBE.IDSucursal.ToString();
			txtCodigo.Text = oBE.Codigo;
			txtNombre.Text = oBE.Nombre;
			hdfIDUbigeo.Value = oBE.IDUbigeo;
			txtUbigeo.Text = oBE.Ubigeo;
			txtDireccion.Text = oBE.Direccion;
			txtTelefono.Text = oBE.Telefono;
			txtCelular.Text = oBE.Celular;
			txtNumeroNotaIngreso.Text = oBE.NumeroNotaIngreso.ToString();
			txtNumeroNotaSalida.Text = oBE.NumeroNotaSalida.ToString();
			txtCodigo.Focus();
			upFormulario.Update();
			registrarScript("AbrirModal('ModalAlmacen');");
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDAlmacen.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLAlmacen().AlmacenEliminar(Int32.Parse(hdfIDAlmacen.Value));

						if (oBERetorno.Retorno == "1")
						{
							AlmacenListar();
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
			AlmacenListar();
		}

		#endregion

		#region Registrar

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			LimpiarFormulario();
			txtNombre.Focus();
			gvLista.SelectedIndex = -1;
			registrarScript("AbrirModal('ModalAlmacen');");
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

			BEAlmacen oBE = new BEAlmacen();
			BLAlmacen oBL = new BLAlmacen();
			oBE.IDAlmacen = Int32.Parse(hdfIDAlmacen.Value);
            oBE.IDSucursal = Int32.Parse(ddlIDSucursal.SelectedValue);
			oBE.Codigo = txtCodigo.Text.Trim();
			oBE.Nombre = txtNombre.Text.Trim();
			oBE.IDUbigeo = hdfIDUbigeo.Value;
			oBE.Direccion = txtDireccion.Text;
			oBE.Telefono = txtTelefono.Text;
			oBE.Celular = txtCelular.Text;
			oBE.Estado = true; 
			oBE.IDEmpresa = IDEmpresa();
			oBE.IDUsuario = IDUsuario();
			BERetornoTran oBERetorno = new BERetornoTran();
			oBERetorno = oBL.AlmacenGuardar(oBE);

			if (oBERetorno.Retorno == "1")
			{
				LimpiarFormulario();
				AlmacenListar(); 
				msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
				registrarScript("CerrarModal('ModalAlmacen');");
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
			hdfIDAlmacen.Value = "0";
            ddlIDSucursal.SelectedIndex = -1;
			txtCodigo.Text = String.Empty;
			txtNombre.Text = String.Empty;
			hdfIDUbigeo.Value = "0";
			txtUbigeo.Text = String.Empty;
			txtDireccion.Text = String.Empty;
			txtTelefono.Text = String.Empty;
			txtCelular.Text = String.Empty;
			txtNumeroNotaIngreso.Text = "0";
			txtNumeroNotaSalida.Text = "0";
			upFormulario.Update();
		}

		#endregion
		 
	}
}