using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Seguridad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Seguridad
{
	public partial class Usuario : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
			}
		}
		#endregion

		#region Funciones
		private void CargaInicial()
		{ 
			CargarDDL(ddlColEmpresa, new BLEmpresa().EmpresaListar(), "IDEmpresa", "RazonSocial", true); 
			CargarDDL(ddlColSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true);
			CargarDDL(ddlBIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", true, Constantes.TODOS); 
			CargarDDL(ddlColCargo, new BLCombo().LLenarCombo("CAR"), "Codigo", "Nombre", true);
			ddlColEmpresa.SelectedValue = IDEmpresa().ToString();
			UsuarioListar();
		}


		protected void ddlColEmpresa_SelectedIndexChanged(object sender, EventArgs e)
		{
			CargarDDL(ddlColSucursal, new BLSucursal().SucursalxEmpresaListar(Int32.Parse(ddlColEmpresa.SelectedValue)), "IDSucursal", "Sucursal", true);
		}


		private void CargoListar()
		{
			CargarDDL(ddlColCargo, new BLCombo().LLenarCombo("CAR"), "Codigo", "Nombre", true);
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			UsuarioListar();
		}

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			hfIDUsuario.Value = "0";
			hfIDColaborador.Value = "0";
			LimpiarFormulario();
			CargarRolesDisponibles("");
			if (Boolean.Parse(Session["EmpresaEsPrincipal"].ToString()))
			{
				ddlColEmpresa.SelectedValue = Session["IDEmpresa"].ToString();
				ddlColEmpresa.Enabled = true;
			}
			else {
				ddlColEmpresa.SelectedValue = Session["IDEmpresa"].ToString();
				ddlColEmpresa.Enabled = false;
			}
			upRegistro.Update();
			registrarScript("UsuarioAbrir();");
		}

		private void CargarUsuarioRol()
		{
			if (chkUsuEstado.Checked)
			{
				CargarRolesDisponibles("");
				ArrayList ListaRolesAsignados = (ArrayList)new BLUsuarioRol().ListarPerfilRolAsignados(0, Int32.Parse(hfIDUsuario.Value));
				if (ListaRolesAsignados.Count > 0)
				{
					Session["ListaRolesAsignados"] = ListaRolesAsignados;
					LlenarRolesAsignados(ListaRolesAsignados);
					String IDRoles = String.Empty;
					if (ListaRolesAsignados.Count > 0)
					{
						foreach (BEUsuarioRol pBE in ListaRolesAsignados)
						{
							IDRoles += pBE.IDRol.ToString() + ",";
						}
					}
					CargarRolesDisponibles(IDRoles.TrimEnd(','));
				}
				else
				{
					LlenarRolesAsignados(null);
				}
			}
			else
			{
				gvRolesDisponibles.DataSource = null;
				gvRolesDisponibles.DataBind();
				gvRolesAsignados.DataSource = null;
				gvRolesAsignados.DataBind();
			}
		}

		private void CargarRolesDisponibles(String pListaExcluir)
		{
			BLUsuarioRol pBL = new BLUsuarioRol();
			gvRolesDisponibles.DataSource = pBL.ListarPerfilRolDisponibles(IDUsuario(), pListaExcluir);
			gvRolesDisponibles.DataBind();
		}

		public void LlenarRolesAsignados(ArrayList DT)
		{
			gvRolesAsignados.DataSource = DT;
			gvRolesAsignados.DataBind();
		}

		private void LimpiarFormulario()
		{
			chkUsuEstado.Checked = true;
			chkUsuBloqueado.Checked = false;
			txtUsuCodigo.Text = String.Empty;
			txtUsuNombres.Text = String.Empty;
			txtUsuApellidoPaterno.Text = String.Empty;
			txtUsuApellidoMaterno.Text = String.Empty;
			txtUsuEmail.Text = String.Empty;
			txtUsuNroDocumento.Text = String.Empty;
			txtUsuCodigo.Enabled = true;
			CargoListar();
			ddlColEmpresa.Enabled = true;
			ddlColEmpresa.SelectedIndex = -1;
			ddlColSucursal.Enabled = true;
			ddlColSucursal.SelectedIndex = -1;
			ddlColCargo.Enabled = true;
			ddlColCargo.SelectedIndex = -1;
			Session["ListaRolesAsignados"] = null;
			gvRolesDisponibles.DataSource = new List<BEUsuarioRol>();
			gvRolesDisponibles.DataBind();
			gvRolesAsignados.DataSource = new List<BEUsuarioRol>();
			gvRolesAsignados.DataBind();

		}
		 
		#endregion

		#region Lista

		private void UsuarioListar()
		{
			BLUsuario pBL = new BLUsuario();
			gvUsuarios.DataSource = pBL.UsuarioListar(txtBuscar.Text.Trim(), Int32.Parse(ddlBIDSucursal.SelectedValue));
			gvUsuarios.DataBind();
			gvUsuarios.SelectedIndex = -1;
		}

		protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvUsuarios.PageIndex = e.NewPageIndex;
			UsuarioListar();
			upLista.Update();
		}

		protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
		{
            if (e.CommandName == "Seleccionar")
            {
                LimpiarFormulario();
                String[] cmdArgumentos = e.CommandArgument.ToString().Split(new Char[] { ',' });
                hfIDUsuario.Value = cmdArgumentos[0].ToString();
                hfIDColaborador.Value = cmdArgumentos[1].ToString();
                CargarUsuarioSeleccionar();
                upRegistro.Update();
            }
            else if (e.CommandName == "ReiniciarClave")
            {
                BEUsuario oBEUsuario = new BLUsuario().Seleccionar(Int32.Parse(e.CommandArgument.ToString()), 0);
                hfIDUsuarioClave.Value = oBEUsuario.IDUsuario.ToString();
                txtUsuarioClave.Text = oBEUsuario.Usuario;
                txtNumeroDocumentoClave.Text = oBEUsuario.NumeroDocumento;
                txtNombreCompletoClave.Text = oBEUsuario.NombreCompleto;
                txtNuevaClave.Text = "";
                registrarScript("CambiarClaveAbrir()", "jsReiniciarClave");
                upCambiarClave.Update();
            }
            else if (e.CommandName == "Eliminar")
            {
                String[] cmdArgumentos = e.CommandArgument.ToString().Split(new Char[] { ',' });
                hfIDUsuario.Value = cmdArgumentos[0].ToString();
                hfIDColaborador.Value = cmdArgumentos[1].ToString();

                BEUsuario oBEUsuario = new BEUsuario();
                oBEUsuario.IDUsuario = Int32.Parse(hfIDUsuario.Value);
                oBEUsuario.IDColaborador = Int32.Parse(hfIDColaborador.Value);
                oBEUsuario.IDUsuarioAuditoria = IDUsuario();
            
                BERetornoTran oBERetorno = new BERetornoTran();

                oBERetorno = new BLUsuario().UsuarioEliminar(oBEUsuario);
                if (oBERetorno.Retorno == "1")
                {
                    UsuarioListar();
                    msgbox(TipoMsgBox.confirmation, "El proceso se realizó con éxito");
                }
                else
                {
                    if (oBERetorno.Retorno != "-1")
                    {
                        msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
                        RegistrarLogSistema("gvUsuarios_RowCommand()", oBERetorno.ErrorMensaje, true);
                    }
                    else
                    {
                        RegistrarLogSistema("gvUsuarios_RowCommand()", oBERetorno.ErrorMensaje, true);
                    }
                }                
            }
        }

		private void CargarUsuarioSeleccionar()
		{
			LimpiarFormulario();
			chkUsuEstado.Checked = false;
			BEUsuario oBEUsuario = new BLUsuario().Seleccionar(Int32.Parse(hfIDUsuario.Value), Int32.Parse(hfIDColaborador.Value));
			if (oBEUsuario != null)
			{
				hfIDUsuario.Value = oBEUsuario.IDUsuario.ToString();
				hfIDColaborador.Value = oBEUsuario.IDColaborador.ToString();
				ddlColEmpresa.Enabled = false;
				ddlColEmpresa.SelectedValue = oBEUsuario.IDEmpresa.ToString();
				CargoListar();
				txtUsuCodigo.Enabled = false;
				txtUsuCodigo.Text = oBEUsuario.Usuario;
				txtUsuNombres.Text = oBEUsuario.Nombres;
				txtUsuApellidoPaterno.Text = oBEUsuario.ApellidoPaterno;
				txtUsuApellidoMaterno.Text = oBEUsuario.ApellidoMaterno;
				txtUsuNroDocumento.Text = oBEUsuario.NumeroDocumento;

				if (oBEUsuario.IDUsuario != 0)
				{
					chkUsuEstado.Checked = oBEUsuario.EstadoUsuario;
					chkUsuBloqueado.Checked = oBEUsuario.Bloqueado;
					chkUsuReiniciarClave.Checked = false;
					txtUsuEmail.Text = oBEUsuario.Email;
				}

				if (oBEUsuario.IDColaborador != 0)
				{
					ddlColSucursal.SelectedValue = oBEUsuario.IDSucursal.ToString();
					ddlColCargo.SelectedValue = oBEUsuario.IDCargo.ToString();
				}

				chkUsuBloqueado.Checked = oBEUsuario.Bloqueado;
				chkUsuReiniciarClave.Checked = oBEUsuario.CambiarClave;

			}
			CargarUsuarioRol();
			upRegistro.Update();
			registrarScript("UsuarioAbrir();");
		}


		#endregion

		#region Roles
		protected void btnRolAgregar_Click(object sender, EventArgs e)
		{
			ArrayList ListaIndex = new ArrayList();
			foreach (GridViewRow row in gvRolesDisponibles.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					CheckBox chkRow = (CheckBox)row.Cells[0].FindControl("chkRDSel");
					if (chkRow.Checked)
					{
						BEUsuarioRol pBE = new BEUsuarioRol();
						pBE.Index = Int32.Parse(((Label)row.Cells[0].FindControl("lblIndex")).Text);
						pBE.IDRol = Int32.Parse(((Label)row.Cells[0].FindControl("lblIDRol")).Text);
						pBE.Perfil = ((Label)row.Cells[0].FindControl("lblPerfil")).Text;
						pBE.Rol = ((Label)row.Cells[0].FindControl("lblRol")).Text;
						ListaIndex.Add(pBE);
					}
				}
			}
			if (ListaIndex.Count > 0)
			{
				ArrayList ListaRolesAsignados = new ArrayList();
				ListaRolesAsignados = (ArrayList)(Session["ListaRolesAsignados"]);
				String IDRoles = String.Empty;
				//ListaIndex.Reverse();
				foreach (BEUsuarioRol pBE in ListaIndex)
				{
					if (ListaRolesAsignados == null) ListaRolesAsignados = new ArrayList();
					ListaRolesAsignados.Add(pBE);
				}
				LlenarRolesAsignados(ListaRolesAsignados);
				Session["ListaRolesAsignados"] = ListaRolesAsignados;
				if (ListaRolesAsignados.Count > 0)
				{
					foreach (BEUsuarioRol pBE in ListaRolesAsignados)
					{
						IDRoles += pBE.IDRol.ToString() + ",";
					}
				}
				CargarRolesDisponibles(IDRoles.TrimEnd(','));
				gvRolesAsignados.SelectedIndex = -1;
				upTabUsuario.Update();
			}
			else
			{
				msgbox(TipoMsgBox.warning, "Seleccione al menos un Rol a asignar.");
			}
		}

		protected void btnRolEliminar_Click(object sender, EventArgs e)
		{
			ArrayList ListaIndex = new ArrayList();
			foreach (GridViewRow row in gvRolesAsignados.Rows)
			{
				if (row.RowType == DataControlRowType.DataRow)
				{
					CheckBox chkRow = (CheckBox)row.Cells[0].FindControl("chkRASel");
					if (chkRow.Checked)
					{
						BEUsuarioRol pBE = new BEUsuarioRol();
						pBE.Index = Int32.Parse(((Label)row.Cells[0].FindControl("lblIndex")).Text);
						pBE.IDRol = Int32.Parse(((Label)row.Cells[0].FindControl("lblIDRol")).Text);
						pBE.Perfil = ((Label)row.Cells[0].FindControl("lblPerfil")).Text;
						pBE.Rol = ((Label)row.Cells[0].FindControl("lblRol")).Text;
						ListaIndex.Add(pBE);
					}
				}
			}
			if (ListaIndex.Count > 0)
			{
				ArrayList ListaRolesAsignados = new ArrayList();
				ListaRolesAsignados = (ArrayList)(Session["ListaRolesAsignados"]);
				String IDRoles = String.Empty;
				ListaIndex.Reverse();
				foreach (BEUsuarioRol pBE in ListaIndex)
				{
					ListaRolesAsignados.RemoveAt(pBE.Index);
				}
				LlenarRolesAsignados(ListaRolesAsignados);
				Session["ListaRolesAsignados"] = ListaRolesAsignados;
				if (ListaRolesAsignados.Count > 0)
				{
					foreach (BEUsuarioRol pBE in ListaRolesAsignados)
					{
						IDRoles += pBE.IDRol.ToString() + ",";
					}
				}
				CargarRolesDisponibles(IDRoles.TrimEnd(','));
				gvRolesAsignados.SelectedIndex = -1;
				upTabUsuario.Update();
			}
			else
			{
				msgbox(TipoMsgBox.warning, "Seleccione al menos un Rol a quitar.");
			}
		}
		#endregion

		#region Guardar
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			if (chkUsuEstado.Checked != false)
			{
				BEUsuario oBEUsuario = new BEUsuario();
				BLUsuario oBLUsuario = new BLUsuario();

				StringBuilder validaciones = new StringBuilder();
				if (txtUsuCodigo.Text.Trim().Length == 0)
					validaciones.Append("<div>Ingrese Usuario</div>");
				if (txtUsuNroDocumento.Text.Trim().Length == 0)
					validaciones.Append("<div>Ingrese Nro. Documento</div>");
				if (txtUsuEmail.Text.Trim().Length > 0 && !esEmail(txtUsuEmail.Text.Trim()))
					validaciones.Append("<div>Ingrese E-mail válido</div>");
				if (ddlColEmpresa.SelectedValue == "0")
					validaciones.Append("<div>Seleccione Empresa</div>");
				if (ddlColSucursal.SelectedValue == "0")
					validaciones.Append("<div>Seleccione Sucursal</div>");
				if (ddlColCargo.SelectedValue == "0")
					validaciones.Append("<div>Seleccione Cargo</div>");
				if (validaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validaciones.ToString());
					return;
				}

				if (txtUsuCodigo.Enabled)
				{
					String ValUsuario = oBLUsuario.ValidarNombreUsuario(txtUsuCodigo.Text.Trim());
					if (ValUsuario == "/Error/")
					{
						msgbox(TipoMsgBox.warning, "Ocurrió un error al validar el Usuario");
						return;
					}
					else if (ValUsuario != "")
					{
						msgbox(TipoMsgBox.warning, "Usuario no disponible");
						return;
					}
				}

				//Usuario
				oBEUsuario.Nombres = txtUsuNombres.Text.Trim();
				oBEUsuario.ApellidoPaterno = txtUsuApellidoPaterno.Text.Trim();
				oBEUsuario.ApellidoMaterno = txtUsuApellidoMaterno.Text.Trim();
				oBEUsuario.NumeroDocumento = txtUsuNroDocumento.Text.Trim();
				oBEUsuario.IDUsuario = Int32.Parse(hfIDUsuario.Value);
				oBEUsuario.Usuario = txtUsuCodigo.Text.Trim();
				oBEUsuario.Email = txtUsuEmail.Text.Trim();
				oBEUsuario.EstadoUsuario = chkUsuEstado.Checked;
				IList ListaRol = new ArrayList();
				BEUsuarioRol oBEUsuarioRol;
				foreach (GridViewRow row in gvRolesAsignados.Rows)
				{
					if (row.RowType == DataControlRowType.DataRow)
					{
						oBEUsuarioRol = new BEUsuarioRol();
						oBEUsuarioRol.IDRol = Int32.Parse(((Label)row.Cells[0].FindControl("lblIDRol")).Text);
						ListaRol.Add(oBEUsuarioRol);
						oBEUsuarioRol = null;
					}
				}
				oBEUsuario.ListaRol = ListaRol;
				oBEUsuario.IDUsuarioAuditoria = IDUsuario();
				oBEUsuario.CodColaborador = txtUsuCodigo.Text.Trim();
				oBEUsuario.IDColaborador = Int32.Parse(hfIDColaborador.Value);
				oBEUsuario.IDSucursal = Int32.Parse(ddlColSucursal.SelectedValue);
				oBEUsuario.IDCargo = Int32.Parse(ddlColCargo.SelectedValue);
				oBEUsuario.EstadoColaborador = true;
				oBEUsuario.Bloqueado = chkUsuBloqueado.Checked;
				oBEUsuario.CambiarClave = chkUsuReiniciarClave.Checked;

				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBLUsuario.Insertar(oBEUsuario);
				if (oBERetorno.Retorno != "-1")
				{
					if (!chkUsuBloqueado.Checked)
					{
						oBLUsuario.Desbloquear(oBEUsuario.IDUsuario, IDUsuario(), chkUsuReiniciarClave.Checked);
					}

					UsuarioListar();
					hfIDUsuario.Value = oBEUsuario.IDUsuario.ToString();
					hfIDColaborador.Value = oBEUsuario.IDColaborador.ToString();
					CargarUsuarioSeleccionar();
					msgbox(TipoMsgBox.confirmation, "Usuario guardado con éxito.");
					registrarScript("UsuarioCerrar();");
					upLista.Update();
				}
				else
				{
					RegistrarLogSistema("btnGuardar_Click()", oBERetorno.ErrorMensaje, true);
				}
			}
			else
			{
				msgbox(TipoMsgBox.warning, "Seleccione Usuario");
			}
		}
		#endregion

		#region Cambiar Clave
		protected void btnCambiarClave_Click(object sender, EventArgs e)
		{
			if (txtNuevaClave.Text.Trim().Length > 0)
			{
				BLUsuario oBLUsuario = new BLUsuario();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = oBLUsuario.CambiarClave(Int32.Parse(hfIDUsuarioClave.Value), txtNuevaClave.Text, IDUsuario());
				if (oBERetorno.Retorno != "-1")
				{
					msgbox(TipoMsgBox.confirmation, "Contraseña Actualizada con éxito");
					registrarScript("CambiarClaveCerrar();");
				}
				else
				{
					RegistrarLogSistema("btnCambiarClave_Click()", oBERetorno.ErrorMensaje, true);
				}
			}
			else
			{
				msgbox(TipoMsgBox.warning, "Ingrese una contraseña");
			}
		}
		#endregion

	}
}