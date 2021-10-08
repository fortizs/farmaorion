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
    public partial class Rol : PageBase
    {
        #region Inicio
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();  
                CargarDDL(ddlBPerfil, new BLPerfil().Listar(IDUsuario()), "IDPerfil", "Nombre", true, Constantes.TODOS);
                CargarDDL(ddlPerfil, new BLPerfil().Listar(IDUsuario()), "IDPerfil", "Nombre");
                CargarDDL(ddlBEmpresa, new BLEmpresa().EmpresaListar(), "IDEmpresa", "RazonSocial", false);
                CargarDDL(ddlEmpresa, new BLEmpresa().EmpresaListar(), "IDEmpresa", "RazonSocial", false);
                ListarRol(); 
                CargarDDL(ddlModulo, new BLModulo().ModuloListarxPermiso(IDUsuario()), "IDModulo", "Nombre");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtNombre.Focus();
            gvLista.SelectedIndex = -1;
            registrarScript("AbrirModal('DatosRol');");
        }
        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
			try
			{
				StringBuilder validaciones = new StringBuilder();
				if (ddlEmpresa.SelectedValue == "0" || ddlEmpresa.SelectedValue == "-1")
					validaciones.Append("<div>Seleccione Empresa</div>");
				if (ddlPerfil.SelectedValue == "0" || ddlPerfil.SelectedValue == "-1")
					validaciones.Append("<div>Seleccione Perfil</div>");
				if (txtNombre.Text.Trim().Length == 0)
					validaciones.Append("<div>Ingrese Nombre</div>");
				if (validaciones.Length > 0)
				{
					msgbox(TipoMsgBox.warning, validaciones.ToString());
					return;
				}

				BERol oBERol = new BERol();
				BLRol oBLRol = new BLRol();
				oBERol.IDRol = Int32.Parse(txtIDRol.Text);
				oBERol.Nombre = txtNombre.Text.Trim();
				oBERol.IDEmpresa = Int32.Parse(ddlEmpresa.SelectedValue);
				oBERol.IDPerfil = Int32.Parse(ddlPerfil.SelectedValue);
				oBERol.Estado = chkEstado.Checked;
				BERetornoTran oBERetorno = new BERetornoTran();
				if (oBERol.IDRol == 0)
				{
					oBERetorno = oBLRol.Insertar(oBERol);
				}
				else
				{
					oBERetorno = oBLRol.Actualizar(oBERol);
				}

				if (oBERetorno.Retorno == "1")
				{
					LimpiarFormulario();
					ListarRol();
				
					msgbox(TipoMsgBox.confirmation, "Sistema", "Datos grabados con éxito.");
					registrarScript("CerrarModal('DatosRol');");
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
            txtIDRol.Text = "0";
            txtNombre.Text = String.Empty; 
            chkEstado.Checked = true;
        } 

        #endregion

        #region Lista

        private void ListarRol()
        {
            BERol oBEX = new BERol();
            oBEX.Buscar = txtBuscar.Text.Trim();
            oBEX.IDEmpresa = Int32.Parse(ddlBEmpresa.SelectedValue);
            oBEX.IDPerfil = Int32.Parse(ddlBPerfil.SelectedValue);
            oBEX.IDUsuario = IDUsuario(); 
            BLRol oBLRol = new BLRol();
            gvLista.DataSource = oBLRol.Listar(oBEX);
            gvLista.DataBind();
			upLista.Update();
		}

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarRol();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            ListarRol();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 pIDRol = Int32.Parse(gvLista.SelectedDataKey["IDRol"].ToString());
            BLRol oBLRol = new BLRol();
            BERol oBERol = oBLRol.Seleccionar(pIDRol);
            LimpiarFormulario();
            txtIDRol.Text = oBERol.IDRol.ToString();
            txtNombre.Text = oBERol.Nombre; 
            ddlPerfil.SelectedValue = oBERol.IDPerfil.ToString();
            chkEstado.Checked = oBERol.Estado; 
            txtNombre.Focus();
            upFormulario.Update();
			registrarScript("AbrirModal('DatosRol');");
		}

        protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ConfigPermiso")
            {
                BLRol oBLRol = new BLRol();
                BERol oBERol = oBLRol.Seleccionar(Int32.Parse(e.CommandArgument.ToString()));
                hfIDRol.Value = oBERol.IDRol.ToString();
                lblRol.Text = oBERol.Nombre;
                upPermisoModulo.Update();
                ListarRolMenu(); 
				registrarScript("AbrirModal('configPermiso')");
            }
            else if (e.CommandName == "Eliminar")
            {
                
                BERetornoTran oBERetorno = new BERetornoTran();
                oBERetorno = new BLRol().Eliminar(Int32.Parse(e.CommandArgument.ToString()));

                if (oBERetorno.Retorno == "1")
                {
                    ListarRol();
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
        protected void ddlBPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarRol();
        }

        protected void ddlBEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarRol();
        }
        #endregion

        #region Permiso RolMenu
        protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarRolMenu();
        }
        private void ListarRolMenu()
        {
            gvRolMenu.DataSource = new BLRolMenu().Listar(Int32.Parse(hfIDRol.Value), Int32.Parse(ddlModulo.SelectedValue), IDUsuario());
            gvRolMenu.DataBind();
        }
        protected void btnGrabarRolMenu_Click(object sender, EventArgs e)
        {
            try
            {
                BERolMenu oBERolMenu = new BERolMenu();
                BLRolMenu oBLRolMenu = new BLRolMenu();
                Int32 IDRol = Convert.ToInt32(hfIDRol.Value);
                Int32 Registro = 0;
                for (int i = 0; i <= this.gvRolMenu.Rows.Count - 1; i++)
                {
                    Registro = Convert.ToInt32(((Label)this.gvRolMenu.Rows[i].Cells[0].Controls[1]).Text);
                    CheckBoxList cblOperacion = (CheckBoxList)this.gvRolMenu.Rows[i].FindControl("cblOperacion");
                    oBERolMenu.Operaciones = String.Join(",", GetSelectedItems(cblOperacion));
                    oBERolMenu.IDMenu = Convert.ToInt32(((Label)this.gvRolMenu.Rows[i].Cells[1].Controls[1]).Text);
                    oBERolMenu.IDRol = IDRol;
                    oBERolMenu.IDUsuario = IDUsuario();
                    oBERolMenu.Estado = ((CheckBox)this.gvRolMenu.Rows[i].Cells[4].Controls[1]).Checked;
                    if (oBERolMenu.Estado)
                    {
                        oBLRolMenu.Insertar(oBERolMenu);
                    }
                    else if ((Registro != 0))
                    {
                        oBLRolMenu.Insertar(oBERolMenu);
                    }
                }
                ListarRolMenu();
                msgbox(TipoMsgBox.confirmation, "Permisos guardados con éxito");
            }
            catch (Exception ex)
            {
                RegistrarLogSistema("btnGrabarRolMenu_Click()", ex.ToString(), true);
            }
        }

        protected void gvRolMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            registrarScript("funModalOpeAbrir()", "jsReOpe");
        }


        public static List<string> GetSelectedItems(CheckBoxList cbl)
        {
            var result = new List<string>();
            foreach (ListItem item in cbl.Items)
                if (item.Selected)
                    result.Add(item.Value);
            return result;
        }


        protected void gvRolMenu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBoxList cblOperacion = (CheckBoxList)e.Row.FindControl("cblOperacion");
                Int32 pIDMenu = Int32.Parse(((Label)e.Row.FindControl("lblIDMenu")).Text);
                BLRolMenuOperacion oBLRolMO = new BLRolMenuOperacion();
                IList Lista = oBLRolMO.Listar(Int32.Parse(hfIDRol.Value), pIDMenu, IDUsuario());
                cblOperacion.Items.Clear();
                foreach (BERolMenuOperacion oBE in Lista)
                {
                    ListItem item = new ListItem();
                    item.Value = oBE.IDOperacion.ToString();
                    item.Text = "&nbsp;" + oBE.Nombre;
                    item.Selected = oBE.Acceso;
                    cblOperacion.Items.Add(item);
                }
            }
        }

        #endregion

    }
}