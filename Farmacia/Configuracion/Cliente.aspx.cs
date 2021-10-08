
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Configuracion
{
    public partial class Cliente : PageBase
    { 
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                ListarCliente(); 
                CargarDDL(ddlIDTipoDocumento, new BLTipoDocumento().TipoDocumentoListar(), "IDTipoDocumento", "Nombre", true, Constantes.SELECCIONAR);
                ddlIDTipoDocumento.SelectedValue = "3"; 
            }
        }
         
        #endregion

        #region Lista
     
        protected void ddlIDTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIDTipoDocumento.SelectedValue.Equals("1"))
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
            gvLista.DataSource = oBLCliente.ClienteListar(txtBuscar.Text.Trim(), Int32.Parse(Session["IDEmpresa"].ToString()));
            gvLista.DataBind();
			upLista.Update();
		}

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarCliente();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
			try
			{
				Int32 pIDCliente = Int32.Parse(gvLista.SelectedDataKey["IDCliente"].ToString());
				BLCliente oBL = new BLCliente();
				BECliente oBE = oBL.ClienteSeleccionar(pIDCliente);
				hdfIDCliente.Value = pIDCliente.ToString();
				ddlIDTipoDocumento.SelectedValue = oBE.IDTipoDocumento.ToString();
				txtNumeroDocumento.Text = oBE.NumeroDocumento;
				txtRazonSocial.Text = oBE.RazonSocial;
				txtNombreComercial.Text = oBE.NombreComercial;
				hdfRegIDUbigeo.Value = oBE.IDUbigeo;
				txtRegUbigeo.Text = oBE.Ubigeo;

				txtDireccion.Text = oBE.Direccion.ToString();
				txtUrbanizacion.Text = oBE.Urbanizacion.ToString();
				txtCorreo.Text = oBE.Correo.ToString();

				txtLimiteCredito.Text = oBE.LimiteCredito.ToString("N");
				txtDiasCredito.Text = oBE.DiasCredito.ToString();

				txtFechaNacimiento.Text = oBE.FechaNacimiento.ToShortDateString() == "01/01/1900" ? "" : oBE.FechaNacimiento.ToShortDateString();
				txtEdad.Text = oBE.Edad.ToString();
				txtTelefono.Text = oBE.Telefono;
				txtCelular.Text = oBE.Celular;

				txtNumeroDocumento.Focus();
				upFormulario.Update();
				registrarScript("AbrirModal('DatosCliente');");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvLista_SelectedIndexChanged()", ex.Message, true);
			} 
		}

		protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDCliente.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLCliente().ClienteEliminar(Int32.Parse(hdfIDCliente.Value));
						if (oBERetorno.Retorno == "1")
						{
							ListarCliente();
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
			ListarCliente();
		}

		#endregion

		#region Registrar

		protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNumeroDocumento.Focus();
            gvLista.SelectedIndex = -1;
            registrarScript("AbrirModal('DatosCliente');");
        }
       
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
			try
			{

				StringBuilder validacion = new StringBuilder();
				if (txtNumeroDocumento.Text.Length == 0) validacion.Append("<div>Ingrese Numero Documento.</div>");
				if (txtRazonSocial.Text.Length == 0) validacion.Append("<div>Ingrese nombre o razon social.</div>");

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

				BECliente oBE = new BECliente();
				BLCliente oBL = new BLCliente();
				oBE.IDCliente = Int32.Parse(hdfIDCliente.Value);
				oBE.IDTipoDocumento = Int32.Parse(ddlIDTipoDocumento.SelectedValue);

				oBE.NumeroDocumento = txtNumeroDocumento.Text.Trim();
				oBE.RazonSocial = txtRazonSocial.Text.Trim();
				oBE.NombreComercial = txtNombreComercial.Text.Trim();
				oBE.IDUbigeo = hdfRegIDUbigeo.Value;
				oBE.Direccion = txtDireccion.Text;
				oBE.Urbanizacion = txtUrbanizacion.Text;
				oBE.Correo = txtCorreo.Text;
				oBE.LimiteCredito = Decimal.Parse(txtLimiteCredito.Text);
				oBE.DiasCredito = Int32.Parse(txtDiasCredito.Text);
				oBE.Estado = true;
				oBE.IDEmpresa = IDEmpresa();
				oBE.IDUsuario = IDUsuario();

				oBE.FechaNacimiento = txtFechaNacimiento.Text.Trim().Length == 0 ? DateTime.Parse(Constantes.FECHA_NULA) : DateTime.Parse(txtFechaNacimiento.Text.Trim());
				oBE.Telefono = txtTelefono.Text.Trim();
				oBE.Celular = txtCelular.Text.Trim();
				oBE.Sexo = ddlSexo.SelectedValue;


				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.ClienteGuardar(oBE); 

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormulario();
					ListarCliente();  
					msgbox(TipoMsgBox.confirmation, "Sistema", Constantes.MENSAJE_EXITO);
					registrarScript("CerrarModal('DatosCliente');");
				}
				else {
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
            hdfIDCliente.Value = "0";
            hdfRegIDUbigeo.Value = "";
            txtRegUbigeo.Text = "";
            txtNumeroDocumento.Text = String.Empty;
            txtRazonSocial.Text = String.Empty;
            txtNombreComercial.Text = String.Empty;
            txtDireccion.Text = String.Empty;
            txtUrbanizacion.Text = String.Empty;
            txtCorreo.Text = String.Empty;
            ddlIDTipoDocumento.SelectedIndex = -1;
			txtDiasCredito.Text = "0";
			txtLimiteCredito.Text = "0.00";

			txtFechaNacimiento.Text = String.Empty;
			txtTelefono.Text = String.Empty;
			txtCelular.Text = String.Empty;
			ddlSexo.SelectedIndex = -1;
			 

			upFormulario.Update();
        }

		#endregion
		 
	}
}