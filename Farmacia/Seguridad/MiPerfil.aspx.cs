using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Seguridad
{
	public partial class MiPerfil : PageBase
	{
		#region Inicio
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				CargaInicial();
				PersonaSeleccionar();
				hfIDColaborador.Value = IDColaborador().ToString();
				PersonaImagen();
			}
		}

		private void CargaInicial()
		{ 
			PersonaImagen();
		}

		protected void PersonaSeleccionar()
		{
			BEColaborador oBE = new BLColaborador().SeleccionarColaborador(IDColaborador()); 
			txtNumeroDocumento.Text = oBE.Dni;
			txtNombresCompleto.Text = oBE.Colaborador;
			ddlSexo.SelectedValue = oBE.Sexo;  
			txtTelefono.Text = oBE.Telefono;
			txtCelular.Text = oBE.Celular;
			txtEmail.Text = oBE.Email; 
			upUsuarioRegistrar.Update();
		}

		protected void lnkGuardarUsuario_Click(object sender, EventArgs e)
		{
			try
			{
				  
				BEColaborador oBE = new BEColaborador();
				oBE.IDColaborador = IDColaborador();
				oBE.IDUsuario = IDUsuario();
				oBE.IDEstadoCivil = 0;
				oBE.FechaNacimiento = DateTime.Parse(Constantes.FECHA_NULA);
				oBE.Telefono = txtTelefono.Text;
				oBE.Celular = txtCelular.Text;
				oBE.Email = txtEmail.Text;
                oBE.Sexo = ddlSexo.SelectedValue;
				oBE.Clave = "";
				oBE.IDUbigeo = "0";
				oBE.Direccion = "";
				BERetornoTran oBERetorno = new BERetornoTran();
				oBERetorno = new BLColaborador().UsuarioColaboradorActualizar(oBE);

				if (oBERetorno.Retorno == "1")
				{
					PersonaSeleccionar();
					msgbox(TipoMsgBox.confirmation, "Sistema", Constantes.MENSAJE_EXITO);
				}
				else
				{
					if (oBERetorno.Retorno != "-1")
					{
						msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
					}
					else
					{
						RegistrarLogSistema("lnkGuardarUsuario_Click()", oBERetorno.ErrorMensaje, true);
					} 
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("lnkGuardarUsuario_Click()", ex.ToString(), true);
			}
		}

		private void PersonaImagen()
		{
			repRegistroImagen.DataSource = new BLColaborador().ColaboradorListarSeleccionar(IDColaborador());
			repRegistroImagen.DataBind();
			upUsuarioRegistrar.Update(); 
		}

		protected void lnkRegistroImagenQuitar_Command(object sender, CommandEventArgs e)
		{
			try
			{
				LinkButton lnkRegistroImagenQuitar = (LinkButton)sender;
				RepeaterItem rpItem = (RepeaterItem)lnkRegistroImagenQuitar.NamingContainer;
				if (rpItem != null)
				{
					BERetornoTran oBERetorno = new BERetornoTran();
				    oBERetorno = new BLColaborador().ColaboradorImagenEliminar(Int32.Parse(e.CommandArgument.ToString()));
					if (oBERetorno.Retorno == "1")
					{
						PersonaImagen();
						msgbox(TipoMsgBox.confirmation, "Sistema", Constantes.MENSAJE_EXITO);
					}
					else
					{
						if (oBERetorno.Retorno != "-1")
						{
							msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
						}
						else
						{
							RegistrarLogSistema("lnkRegistroImagenQuitar_Command()", oBERetorno.ErrorMensaje, true);
						}
					} 
				}
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("lnkRegistroImagenQuitar_Command()", ex.ToString(), true);
			}
		}

		protected void btnRegistroImagenListar_Click(object sender, EventArgs e)
		{
			PersonaImagen();
		}

		#endregion


	}
}