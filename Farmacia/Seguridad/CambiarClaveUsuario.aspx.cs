using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BL.Seguridad;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Seguridad
{
	public partial class CambiarClaveUsuario : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                bool Val = false;
                Val = bool.Parse(Session["SegPrimerLogin"].ToString()) || bool.Parse(Session["SegClaveCaducada"].ToString());
                //No validar acceso a la página si pólica de cambio de contraseña esta activa.
                if (!Val)
                {
                    ConfigPage(false, false, false);
                }
                else
                {
                    //((Literal)Page.Master.FindControl("lBreadCrumb")).Text = "<li>Cambiar Constraseña</li>";
                    //((Literal)Page.Master.FindControl("lPageName")).Text = "Cambiar Contraseña";
                    //((Literal)Page.Master.FindControl("lPageIcono")).Text = "<i class=\"icon-key position-left\"></i>";
                }

                try
                {
                    BEUsuario oBEUsuario = (BEUsuario)Session["BEUsuario"];
                    if (oBEUsuario.IDUsuario == 1)
                    {
                        lblMensajeUsu.Text = "Opción no disponible para este usuario.";
                        MultiView1.ActiveViewIndex = 2;
                    }
                    else
                    {
                        if (oBEUsuario.UsuarioDominio.Length == 0)
                        {
                            if (Boolean.Parse(Session["SegPrimerLogin"].ToString()))
                            {
                                lblEstadoClave.Text = "Por favor actualice la contraseña";
                                dEstadoClave.Visible = true;
                            }

                            if (Boolean.Parse(Session["SegClaveCaducada"].ToString()))
                            {
                                lblEstadoClave.Text = "Contraseña caducada, Por favor actualice la contraseña";
                                dEstadoClave.Visible = true;
                            }
                            BLParametroConfig oBLSegConfig = new BLParametroConfig();
                            IList ListaSegConfig = oBLSegConfig.ListarxEmpresa(oBEUsuario.IDEmpresa);
                            //Longitud de caracteres mínima para la contraseña (Número)
                            Int32 pLongMinClave = Int32.Parse(oBLSegConfig.Parametro("CON02", ListaSegConfig));
                            if (pLongMinClave == 0)
                            {
                                //0 No válida longitud
                                revLongitudNuevaC.Enabled = false;
                            }
                            else
                            {
                                revLongitudNuevaC.Enabled = true;
                                revLongitudNuevaC.ValidationExpression = "[\\S\\s]{" + pLongMinClave.ToString() + ",50}";
                                revLongitudNuevaC.ErrorMessage = "La nueva contraseña debe tener una longitud mínima de " + pLongMinClave.ToString() + " caracteres";
                            }
                            hfIDUsuario.Value = oBEUsuario.IDUsuario.ToString();

                            //Contraseña Segura
                            revSeguridadClave.Enabled = Convert.ToBoolean(Int32.Parse(oBLSegConfig.Parametro("CON03", ListaSegConfig)));
                            //Cantidad de contraseñas sin repetir
                            hfCantClaveSinRepetir.Value = oBLSegConfig.Parametro("CON04", ListaSegConfig);
                            //Modificar la Clave solo una vez al día
                            if (Convert.ToBoolean(Int32.Parse(oBLSegConfig.Parametro("CON06", ListaSegConfig))))
                            {
                                BLEvento oBLSegEvento = new BLEvento();
                                if (oBLSegEvento.ClaveModificadaHoy(IDUsuario()))
                                {
                                    MultiView1.ActiveViewIndex = 2;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btnCambiarClave_Click(object sender, EventArgs e)
        {
            StringBuilder validaciones = new StringBuilder(); 
            if (validaciones.Length > 0)
            {
                msgbox(TipoMsgBox.warning, validaciones.ToString());
                return;
            }
             
            BLUsuario oBLUsuario = new BLUsuario();
            BERetornoTran oBERetorno = new BERetornoTran();
            oBERetorno = oBLUsuario.CambiarClave(Int32.Parse(hfIDUsuario.Value), txtClaveActual.Text, txtNuevaClave.Text, Int32.Parse(hfCantClaveSinRepetir.Value), IDUsuario());
            if (oBERetorno.Retorno == "1")
            {
                BEUsuario oBEUsuario = new BEUsuario();
                RegistrarEventoSeguridad(4, oBEUsuario.GetMD5(txtNuevaClave.Text));
                MultiView1.ActiveViewIndex = 1;
                Session["SegPrimerLogin"] = false;
                Session["SegClaveCaducada"] = false;
                lblEstadoClave.Text = "";
                dEstadoClave.Visible = false;
            }
            else if (oBERetorno.Retorno == "-2")
            {
                msgbox(TipoMsgBox.warning, "Validación", oBERetorno.ErrorMensaje);
            }
            else
            {
                RegistrarLogSistema("btnCambiarClave_Click()", oBERetorno.ErrorMensaje, true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtClaveActual.Text = "";
            txtNuevaClave.Text = "";
            txtNuevaClaveConfir.Text = "";
        }


    }
}