using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Controles
{
	public partial class ccBuscarCliente : System.Web.UI.UserControl
	{
		PageBase pPageBase = new PageBase();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				ListarCliente();
				CargarDatos();
			}
		}

		public void CargarDatos()
		{
			new PageBase().CargarDDL(ddlIDTipoDocumentoX, new BLTipoDocumento().TipoDocumentoListar(), "IDTipoDocumento", "Nombre", true, Constantes.SELECCIONAR);

		}

		protected void ddlIDTipoDocumentoX_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32 TipoDoc = Int32.Parse(ddlIDTipoDocumentoX.SelectedValue);

			if (TipoDoc == 1)
			{
				lblRazonSocial.Text = "Apellidos y Nombres:";
				div_NombreComercial.Visible = false;
				txtNombreComercial.Text = "";
				txtRazonSocial.Text = "";
			}
			else {

				lblRazonSocial.Text = "Razón Social:";
				div_NombreComercial.Visible = true;
				txtNombreComercial.Text = "";
				txtRazonSocial.Text = "";
			}

		}

		private void ListarCliente()
		{
			BLCliente oBLCliente = new BLCliente();
			gvClienteLista.DataSource = oBLCliente.ClienteListar(txtFiltro.Text, Int32.Parse(Session["IDEmpresa"].ToString()));
			gvClienteLista.DataBind();
		}

		protected void btnBuscarCliente_Click(object sender, EventArgs e)
		{
			ListarCliente();
		}

		protected void btnClienteListadoNuevo_Click(object sender, EventArgs e)
		{
			pnDatosClienteListado.Visible = false;
			pnDatosClienteRegistro.Visible = true;
			LimpiarFormulario();
		}

		protected void btnClienteRetornar_Click(object sender, EventArgs e)
		{
			pnDatosClienteListado.Visible = true;
			pnDatosClienteRegistro.Visible = false;
		}

		protected void btnClienteGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (ddlIDTipoDocumentoX.SelectedValue.Equals("0") || ddlIDTipoDocumentoX.SelectedValue.Equals("-1")) validacion.Append("<div>Seleccione tipo documento.</div>");
				if (ddlIDTipoDocumentoX.SelectedValue.Equals("3"))
				{
					if (txtNumeroDocumento.Text.Trim().Length != 11 || !PageBase.esDouble(txtNumeroDocumento.Text.Trim())) validacion.Append("<div>Ingrese un Ruc válido.</div>");
				}
				else {
					if (txtNumeroDocumento.Text.Length != 8 || !PageBase.esDouble(txtNumeroDocumento.Text.Trim())) validacion.Append("<div>Ingrese un número Documento válido.</div>");
				}

				if (txtNumeroDocumento.Text.Length == 0) validacion.Append("<div>Ingrese Numero Documento.</div>");
				if (txtRazonSocial.Text.Length == 0) validacion.Append("<div>Ingrese nombre o razon social.</div>");

				if (validacion.Length > 0)
				{
					pPageBase.msgbox(this.Page, PageBase.TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BECliente oBE = new BECliente();
				BLCliente oBL = new BLCliente();
				oBE.IDCliente = Int32.Parse(hdfIDCliente.Value);
				oBE.IDTipoDocumento = Int32.Parse(ddlIDTipoDocumentoX.SelectedValue);
				oBE.NumeroDocumento = txtNumeroDocumento.Text.Trim();
				oBE.RazonSocial = txtRazonSocial.Text.Trim();
				oBE.NombreComercial = txtNombreComercial.Text.Trim();
				oBE.IDUbigeo = hdfRegIDUbigeo.Value;
				oBE.Direccion = txtDireccion.Text.Trim();
				oBE.Urbanizacion = txtUrbanizacion.Text.Trim();
				oBE.Correo = txtCorreo.Text;
				oBE.Estado = true;
				oBE.FechaNacimiento = txtFechaNacimiento.Text.Trim().Length == 0 ? DateTime.Parse(Constantes.FECHA_NULA) : DateTime.Parse(txtFechaNacimiento.Text.Trim());
				oBE.Telefono = txtTelefono.Text.Trim();
				oBE.Celular = txtCelular.Text.Trim();
				oBE.Sexo = ddlSexo.SelectedValue;
				oBE.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
				oBE.IDUsuario = new PageBase().IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.ClienteGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormulario();
					ListarCliente();
					pnDatosClienteListado.Visible = true;
					pnDatosClienteRegistro.Visible = false;
					pPageBase.msgbox(this.Page, PageBase.TipoMsgBox.confirmation, "La operación se realizó con éxito");

				}
				else {
					if (oBERetorno.Retorno != "-1")
					{
						pPageBase.msgbox(this.Page, PageBase.TipoMsgBox.warning, oBERetorno.ErrorMensaje);
					}
					else
					{
						pPageBase.RegistrarLogSistema("btnClienteGuardar_Click()", oBERetorno.ErrorMensaje, true);
					}

				}
			}
			catch (Exception ex)
			{
				pPageBase.RegistrarLogSistema("btnClienteGuardar_Click()", ex.Message, true);
			} 
		}

		protected void LimpiarFormulario()
		{
			hdfIDCliente.Value = "0";
			ddlIDTipoDocumentoX.SelectedIndex = -1;
			txtNumeroDocumento.Text = String.Empty;
			txtRazonSocial.Text = String.Empty;
			txtNombreComercial.Text = String.Empty;
			hdfRegIDUbigeo.Value = "0";
			txtRegUbigeo.Text = String.Empty;
			txtDireccion.Text = String.Empty;
			txtUrbanizacion.Text = String.Empty;
			txtCorreo.Text = String.Empty; 
			ddlSexo.SelectedIndex = -1;
			txtFechaNacimiento.Text = String.Empty;
			txtEdad.Text = "0";
			txtTelefono.Text = String.Empty;
			txtCelular.Text = String.Empty;

		}

		protected void gvClienteLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvClienteLista.PageIndex = e.NewPageIndex;
			ListarCliente();
		}

		protected void gvClienteLista_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				BECliente oBE = new BLCliente().ClienteSeleccionar(Int32.Parse(gvClienteLista.SelectedDataKey["IDCliente"].ToString()));
				hdfIDCliente.Value = oBE.IDCliente.ToString();
				ddlIDTipoDocumentoX.SelectedValue = oBE.IDTipoDocumento.ToString();
				txtNumeroDocumento.Text = oBE.NumeroDocumento;
				txtRazonSocial.Text = oBE.RazonSocial;
				txtNombreComercial.Text = oBE.NombreComercial;

				ddlSexo.SelectedValue = oBE.Sexo.Trim();
				txtFechaNacimiento.Text = oBE.FechaNacimiento.ToShortDateString() == "01/01/1900" ? "" : oBE.FechaNacimiento.ToShortDateString();
				txtEdad.Text = oBE.Edad.ToString();
				txtTelefono.Text = oBE.Telefono;
				txtCelular.Text = oBE.Celular;

				hdfRegIDUbigeo.Value = oBE.IDUbigeo;
				txtRegUbigeo.Text = oBE.Ubigeo;
				txtDireccion.Text = oBE.Direccion;
				txtUrbanizacion.Text = oBE.Urbanizacion;
				txtCorreo.Text = oBE.Correo;
				pnDatosClienteListado.Visible = false;
				pnDatosClienteRegistro.Visible = true;
			}
			catch (Exception ex)
			{
				pPageBase.RegistrarLogSistema("gvClienteLista_SelectedIndexChanged()", ex.Message, true);
			} 
		}


		protected void gvClienteLista_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				LinkButton btn = (LinkButton)e.Row.FindControl("lbSeleccionar");
				String IDCliente = DataBinder.Eval(e.Row.DataItem, "IDCliente").ToString();
				String IDTipoDocumento = DataBinder.Eval(e.Row.DataItem, "IDTipoDocumento").ToString();
				String NumeroDocumento = DataBinder.Eval(e.Row.DataItem, "NumeroDocumento").ToString();
				String Nombre = DataBinder.Eval(e.Row.DataItem, "RazonSocial").ToString();
				String Direccion = DataBinder.Eval(e.Row.DataItem, "Direccion").ToString();
				btn.Attributes.Add("onclick", "SeleccionarCliente('" + IDCliente + "','" + NumeroDocumento + "','" + Nombre + "'); return false;");
			}
		}

	}
}