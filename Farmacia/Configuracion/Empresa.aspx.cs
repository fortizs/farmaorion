
using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Farmacia.Configuracion
{
	public partial class Empresa : PageBase
    {
		#region Load
		String RutaArchivos = ConfigurationManager.AppSettings["RutaArchivosWeb"];

		protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage(); 
                CargaInicial();
				EmpresaSeleccionar(IDEmpresa());
				ListarSucursal();
				ListarDocumentoSerie();
                ListarArchivos();

            }
        }

        private void CargaInicial()
        { 
		    CargarDDL(ddlDSIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS);
			CargarDDL(ddlIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true);
            CargarDDL(ddlIDTipoComprobante, new BLTipoComprobante().TipoComprobanteListar("0","VEN"), "IDTipoComprobante", "Nombre", false);
			hdfIDEmpresa.Value = IDEmpresa().ToString();
		}
         
        private void EmpresaSeleccionar(Int32 pIDEmpresa)
        {
			try
			{
				BEEmpresa oBE = new BLEmpresa().EmpresaSeleccionar(pIDEmpresa);
				txtRegNumeroDocumento.Text = oBE.Ruc;
				txtRegRazonSocial.Text = oBE.RazonSocial;
				txtRegNombreComercial.Text = oBE.NombreComercial;
				txtRegCorreo.Text = oBE.Correo;
				txtRegClaveCorreoEmisor.Text = oBE.ClaveCorreo;
				hdfRegIDUbigeo.Value = oBE.IDUbigeo;
				txtRegUbigeo.Text = oBE.Ubigeo;
				txtRegUrbanizacion.Text = oBE.Urbanizacion;
				txtRegDireccion.Text = oBE.Direccion;
				ddlIDTema.SelectedValue = oBE.IDTema.ToString();
				txtRegClaveCertificado.Text = oBE.ClaveCertificado;
				txtRegUsuarioSol.Text = oBE.UsuarioSol;
				txtRegClaveSol.Text = oBE.ClaveSol;
				ddlIDEmailSMTP.SelectedValue = oBE.IDEmailSMTP.ToString();
				ddlIDSunat.SelectedValue = oBE.IDSunat.ToString();
				ddlSalidaAlmacen.SelectedValue = oBE.SalidaAlmacen;
				ddlIngresoAlmacen.SelectedValue = oBE.IngresoAlmacen;
				ddlImpresionVenta.SelectedValue = oBE.ImpresionVenta;
				txtCodigoEstablecimiento.Text = oBE.CodigoEstablecimiento;
				txtTerminoCondicion.Text = oBE.TerminoCondicion.Trim();
				upRegistroEmpresa.Update();
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("EmpresaSeleccionar()", ex.Message, true);
			} 
        }
		 
        #endregion

        #region Registro 

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder validacion = new StringBuilder();
                if (txtRegNumeroDocumento.Text.Length == 0) validacion.Append("<div>Ingrese el número de documento.</div>");
                if (txtRegRazonSocial.Text.Length == 0) validacion.Append("<div>Ingrese la razon social.</div>");
                if (txtRegNombreComercial.Text.Length == 0) validacion.Append("<div>Ingrese el nombre comercial.</div>");
                if (txtRegCorreo.Text.Length == 0) validacion.Append("<div>Ingrese el Correo.</div>");
                if (hdfRegIDUbigeo.Value.Length == 0) validacion.Append("<div>Seleccione una Ubicación.</div>");
                if (txtRegUrbanizacion.Text.Length == 0) validacion.Append("<div>Ingrese la urbanización.</div>");
                if (txtRegDireccion.Text.Length == 0) validacion.Append("<div>Ingrese la Dirección.</div>");
                if (validacion.Length > 0)
                {
                    msgbox(TipoMsgBox.warning, validacion.ToString());
                    return;
                }

                BEEmpresa oBE = new BEEmpresa();
                oBE.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
                oBE.IDTipoDocumento = 3;
                oBE.IDTema = 1;
                oBE.IDEmailSMTP = Int32.Parse(ddlIDEmailSMTP.SelectedValue);
                oBE.IDSunat = Int32.Parse(ddlIDSunat.SelectedValue);
                oBE.Ruc = txtRegNumeroDocumento.Text;
                oBE.RazonSocial = txtRegRazonSocial.Text;
                oBE.NombreComercial = txtRegNombreComercial.Text;
                oBE.IDUbigeo = hdfRegIDUbigeo.Value;
                oBE.Direccion = txtRegDireccion.Text;
                oBE.Urbanizacion = txtRegUrbanizacion.Text;
                oBE.ClaveCertificado = txtRegClaveCertificado.Text;
                oBE.UsuarioSol = txtRegUsuarioSol.Text;
                oBE.ClaveSol = txtRegClaveSol.Text;
                oBE.Correo = txtRegCorreo.Text;
                oBE.ClaveCorreo = txtRegClaveCorreoEmisor.Text;
                oBE.IDUsuario = IDUsuario();
				oBE.CodigoEstablecimiento = txtCodigoEstablecimiento.Text;

				BLEmpresa oBL = new BLEmpresa();
                BERetornoTran oBERetorno = new BERetornoTran();
                oBERetorno = oBL.EmpresaGuardar(oBE);

                if (oBERetorno.Retorno == "1")
                { 
                    EmpresaSeleccionar(Int32.Parse(Session["IDEmpresa"].ToString())); 
                    upDetalle.Update();
                    ListarSucursal(); 
                    ListarDocumentoSerie();
					msgbox(TipoMsgBox.confirmation, "Facturacion", "Datos de la Empresa guardados con éxito");
				}
                else 
                {
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
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
		 
		protected void btnResetearEmpresa_Click(object sender, EventArgs e)
		{
			try
			{
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLEmpresa().DataTablasEliminar(IDEmpresa());
				if (oBERetorno.Retorno == "1")
				{
					msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnResetearEmpresa_Click()", oBERetorno.ErrorMensaje, true);
					}

				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnResetearEmpresa_Click()", ex.Message, true);
			}

		}
		 
		#endregion

		#region Sucursal

		private void ListarSucursal()
        {
            BLSucursal oBL = new BLSucursal();
            gvListarSucursal.DataSource = oBL.SucursalxEmpresaListar(Int32.Parse(Session["IDEmpresa"].ToString()));
            gvListarSucursal.DataBind();
            upSucursal.Update();
        }

        protected void gvListarSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
			try
			{
				Int32 pIDSucursal = Int32.Parse(gvListarSucursal.SelectedDataKey["IDSucursal"].ToString());
				BESucursal oBE = new BLSucursal().SucursalSeleccionar(pIDSucursal);
				hdfIDSucursal.Value = pIDSucursal.ToString();
				txtSuNombre.Text = oBE.Nombre;
				txtSuTelefono.Text = oBE.Telefono;
				txtSuCelular.Text = oBE.Celular;
				txtSuEmail.Text = oBE.Email;
				hdfSuIDUbigeo.Value = oBE.IDUbigeo;
				txtSuUbigeo.Text = oBE.Ubigeo;
				txtSuDireccion.Text = oBE.Direccion;
				upRegistroSucursal.Update();
				registrarScript("AbrirModal('DatosSucursal');");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvListarSucursal_RowCommand()", ex.Message, true);
			}

           
        }

		protected void gvListarSucursal_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDSucursal.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLSucursal().SucursalEliminar(Int32.Parse(hdfIDSucursal.Value));
						if (oBERetorno.Retorno == "1")
						{
							ListarSucursal();
							msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
						}
						else
						{
							if (oBERetorno.Retorno != "-1")
							{
								msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
							}
							else {
								RegistrarLogSistema("gvListarSucursal_RowCommand()", oBERetorno.ErrorMensaje, true);
							}
						}
						break;
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvListarSucursal_RowCommand()", ex.Message, true);
			}
		}

		protected void btnNuevoSucursal_Click(object sender, EventArgs e)
        {
            LimpiarFormularioSucursal();
            txtSuNombre.Focus();
            registrarScript("AbrirModal('DatosSucursal');");
        }
         
        protected void btnGuardarSucursal_Click(object sender, EventArgs e)
        {
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (txtSuNombre.Text.Length == 0) validacion.Append("<div>Ingrese nombre.</div>");
				if (txtSuDireccion.Text.Length == 0) validacion.Append("<div>Ingrese Dirección.</div>");
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BESucursal oBE = new BESucursal();
				BLSucursal oBL = new BLSucursal();
				oBE.IDSucursal = Int32.Parse(hdfIDSucursal.Value);
				oBE.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
				oBE.Nombre = txtSuNombre.Text.Trim();
				oBE.Telefono = txtSuTelefono.Text.Trim();
				oBE.Celular = txtSuCelular.Text.Trim();
				oBE.Email = txtSuEmail.Text.Trim();
				oBE.IDUbigeo = hdfSuIDUbigeo.Value;
				oBE.Direccion = txtSuDireccion.Text.Trim();
				oBE.Estado = true;
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.SucursalGuardar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormularioSucursal();
					ListarSucursal();
					CargarDDL(ddlIDSucursal, new BLSucursal().SucursalxEmpresaListar(Int32.Parse(Session["IDEmpresa"].ToString())), "IDSucursal", "Sucursal", true);
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
					registrarScript("CerrarModal('DatosSucursal');");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarSucursal_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarSucursal_Click()", ex.Message, true);
			} 
        }

        private void LimpiarFormularioSucursal()
        {
            hdfIDSucursal.Value = "0";
            txtSuNombre.Text = String.Empty;
            txtSuTelefono.Text = String.Empty;
            txtSuCelular.Text = String.Empty;
            txtSuEmail.Text = String.Empty;
            hdfSuIDUbigeo.Value = String.Empty;
            txtSuUbigeo.Text = String.Empty;
            txtSuDireccion.Text = String.Empty;
            upRegistroSucursal.Update();
        }
		 
		#endregion

		#region SerieDocumento

		private void ListarDocumentoSerie()
		{
			BLDocumentoSerie oBL = new BLDocumentoSerie();
			gvDocumentoSerieLista.DataSource = oBL.DocumentoSeriexSucursalListar(Int32.Parse(ddlDSIDSucursal.SelectedValue));
			gvDocumentoSerieLista.DataBind();
			upDocumentoSerie.Update();
		}
		 
		protected void gvDocumentoSerieLista_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
		{
			gvDocumentoSerieLista.PageIndex = e.NewPageIndex;
			gvDocumentoSerieLista.SelectedIndex = -1;
			ListarDocumentoSerie();
		}

		protected void gvDocumentoSerieLista_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			try
			{
				hfIDDocumentoSerie.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLDocumentoSerie().DocumentoSerieEliminar(Int32.Parse(hfIDDocumentoSerie.Value));
						if (oBERetorno.Retorno == "1")
						{
							ListarDocumentoSerie();
							msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
						}
						else
						{
							if (oBERetorno.Retorno != "-1")
							{
								msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
							}
							else {
								RegistrarLogSistema("gvDocumentoSerieLista_RowCommand()", oBERetorno.ErrorMensaje, true);
							}
						}
						break;
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvDocumentoSerieLista_RowCommand()", ex.Message, true);
			}
		}
		 
		protected void ddlDSIDSucursal_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListarDocumentoSerie();
		}

        protected void gvDocumentoSerieLista_SelectedIndexChanged(object sender, EventArgs e)
        {
			try
			{
				Int32 pIDDocumentoSerie = Int32.Parse(gvDocumentoSerieLista.SelectedDataKey["IDDocumentoSerie"].ToString());
				BEDocumentoSerie oBE = new BLDocumentoSerie().DocumentoSerieSeleccionar(pIDDocumentoSerie);
				hfIDDocumentoSerie.Value = pIDDocumentoSerie.ToString();
				ddlIDSucursal.SelectedValue = oBE.IDSucursal.ToString();
				ddlIDTipoComprobante.SelectedValue = oBE.IDTipoComprobante.ToString();
				txtSerie.Text = oBE.Serie;
				txtNumero.Text = oBE.Numero.ToString();
				ddlIDSucursal.Enabled = false;
				ddlIDTipoComprobante.Enabled = false;
				upRegistroDocumentoSerie.Update();
				registrarScript("AbrirModal('DatosDocumentoSerie');");
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvDocumentoSerieLista_SelectedIndexChanged()", ex.Message, true);
			} 
        }

        protected void btnNuevoDocumentoSerie_Click(object sender, EventArgs e)
        {
            LimpiarFormularioDocumentoSerie();
            txtSerie.Focus();
            registrarScript("AbrirModal('DatosDocumentoSerie');");
        }
        
        protected void btnGuardarDocumentoSerie_Click(object sender, EventArgs e)
        {
			try
			{
				StringBuilder validacion = new StringBuilder();
				if (txtSerie.Text.Length == 0) validacion.Append("<div>Ingrese Serie.</div>");
				if (txtNumero.Text.Length == 0) validacion.Append("<div>Ingrese Número.</div>");
				if (validacion.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validacion.ToString());
					return;
				}

				BEDocumentoSerie oBE = new BEDocumentoSerie();
				BLDocumentoSerie oBL = new BLDocumentoSerie();
				oBE.IDDocumentoSerie = Int32.Parse(hfIDDocumentoSerie.Value);
				oBE.IDSucursal = Int32.Parse(ddlIDSucursal.SelectedValue);
				oBE.IDTipoComprobante = Int32.Parse(ddlIDTipoComprobante.SelectedValue);
				oBE.Serie = txtSerie.Text.Trim();
				oBE.Numero = Int32.Parse(txtNumero.Text.Trim());
				oBE.Estado = true;
				oBE.IDUsuario = IDUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();

				if (Int32.Parse(hfIDDocumentoSerie.Value) == 0)
				{
					oBERetorno = oBL.DocumentoSerieInsertar(oBE);
				}
				else {
					oBERetorno = oBL.DocumentoSerieActualizar(oBE);
				}

				if (oBERetorno.Retorno == "1")
				{
					ListarDocumentoSerie();
					LimpiarFormularioDocumentoSerie();
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
					registrarScript("CerrarModal('DatosDocumentoSerie');");
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarDocumentoSerie_Click()", oBERetorno.ErrorMensaje, true);
					}
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarDocumentoSerie_Click()", ex.Message, true);
			} 
        }

        private void LimpiarFormularioDocumentoSerie()
        {
            hfIDDocumentoSerie.Value = "0";
            ddlIDSucursal.SelectedIndex = -1;
            ddlIDTipoComprobante.SelectedIndex = -1;
            txtSerie.Text = String.Empty;
            txtNumero.Text = String.Empty;
            ddlIDSucursal.Enabled = true;
            ddlIDTipoComprobante.Enabled = true;
            upRegistroDocumentoSerie.Update();
        }

		#endregion

		#region Configuraciones

		protected void btnGuardarConfig_Click(object sender, EventArgs e)
		{
			try
			{
				BEEmpresa oBE = new BEEmpresa();
				oBE.IDEmpresa = IDEmpresa();
				oBE.SalidaAlmacen = ddlSalidaAlmacen.SelectedValue;
				oBE.IngresoAlmacen = ddlIngresoAlmacen.SelectedValue;
				oBE.ImpresionVenta = ddlImpresionVenta.SelectedValue;
				oBE.IDUsuario = IDUsuario();
				BLEmpresa oBL = new BLEmpresa();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.EmpresaConfigGuardar(oBE);
				if (oBERetorno.Retorno == "1")
				{
					EmpresaSeleccionar(IDEmpresa());
					upEmpresaConfig.Update();
					msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
				}
				else 
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarConfig_Click()", oBERetorno.ErrorMensaje, true);
					} 
				} 
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarConfig_Click()", ex.Message, true);
			}
		}

		#endregion

		#region Archivos

		private void ListarArchivos()
		{
			BLEmpresa oBL = new BLEmpresa();
			gvEmpresaArchivos.DataSource = oBL.EmpresaArchivoListar(IDEmpresa());
			gvEmpresaArchivos.DataBind();
			upEmpresaArchivos.Update();
		}
		 
		protected void gvEmpresaArchivos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			try
			{
				hdfIDEmpresaArchivo.Value = e.CommandArgument.ToString();
				BERetornoTran oBERetorno = new BERetornoTran();

				switch (e.CommandName)
				{
					case "Eliminar":
						oBERetorno = new BLEmpresa().ArchivoEmpresaEliminar(Int32.Parse(hdfIDEmpresaArchivo.Value));
						if (oBERetorno.Retorno == "1")
						{
							ListarArchivos();
							msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
						}
						else
						{
							if (oBERetorno.Retorno != "-1")
							{
								msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
							}
							else {
								RegistrarLogSistema("gvEmpresaArchivos_RowCommand()", oBERetorno.ErrorMensaje, true);
							}
						}
						break;

					case "Descargar":
						String[] cmdArgumentos = e.CommandArgument.ToString().Split(new Char[] { ';' });
						String RutaArchivo = cmdArgumentos[0].ToString();
						String NombreArchivo = cmdArgumentos[1].ToString();
						String Archivo = RutaArchivos + RutaArchivo + NombreArchivo;
						if (System.IO.File.Exists(Archivo))
						{
							HttpContext.Current.Response.Clear();
							HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
							HttpContext.Current.Response.ContentType = "text/plain";
							HttpContext.Current.Response.WriteFile(Archivo);
							HttpContext.Current.Response.Flush();
							HttpContext.Current.Response.End();
						}
						else {
							msgbox(TipoMsgBox.error, "No se encontró el Archivo en el Servidor");
						}
						break;

				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("gvEmpresaArchivos_RowCommand()", ex.Message, true);
			}
		}

		protected void btnNuevoArchivo_Click(object sender, EventArgs e)
		{
			registrarScript("ModalImagen();");
		}

		protected void btnBuscarEmpresaArchivo_Click(object sender, EventArgs e)
		{
			ListarArchivos();
		}

		#endregion

		#region Cotizacion

		protected void btnGuardarTerminoCondicion_Click(object sender, EventArgs e)
		{
			try
			{
				BEEmpresa oBE = new BEEmpresa();
				oBE.IDEmpresa = IDEmpresa();
				oBE.TerminoCondicion = txtTerminoCondicion.Text;
				oBE.IDUsuario = IDUsuario();
				BLEmpresa oBL = new BLEmpresa();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBL.EmpresaTerminoActualizar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					EmpresaSeleccionar(IDEmpresa());
					msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
					else {
						RegistrarLogSistema("btnGuardarTerminoCondicion_Click()", oBERetorno.ErrorMensaje, true);
					}
				}

			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnGuardarTerminoCondicion_Click()", ex.Message, true);
			}
		}

		#endregion

	}
}