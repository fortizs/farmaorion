using System;
using System.Linq;
using SimpleCrypto;
using Farmacia.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Farmacia.App_Class;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Farmacia
{
    public partial class RecuperarClave : PageBase
    {
        private DataFarmaciaDataContext db = new DataFarmaciaDataContext();
         
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                
            }
        }
         
        public string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString().ToUpper();
        } 
        protected void lnkEnviar_Click(object sender, EventArgs e)
        {  
            String pUsuario = txtUser.Text.Trim(); 
            var persona = db.Usuario.Where(x => x.Usuario1 == pUsuario).FirstOrDefault(); 
            if (persona != null)
            {
                //Libreria para crear contraseñas aleaotoria
                ICryptoService cryptoService = new PBKDF2(); 
                string contraseniaNueva =RandomPassword.Generate(10, PasswordGroup.Lowercase, PasswordGroup.Uppercase, PasswordGroup.Numeric, PasswordGroup.Special);
                //encripta la contraseña con el algoritmo MD5
                string contraseniaEncriptada = GetMD5(contraseniaNueva);

                MailAddress Para = new MailAddress(persona.EmailUser, "Usuario Farma Orión");
                String Asunto = "Recuperación de Contraseña";
                String BodyMessage = "<html><body>";
                BodyMessage += "<table width='100%' bgcolor='#f5f5f5' cellpadding='0' cellspacing='0' border='0'>";
                BodyMessage += "<p style='font-size:15px;color:blue;'> Estimado(a) Colaborador, su nueva contraseña para ingresar al sistema es:"+ " " + contraseniaNueva + "</p>";
                BodyMessage += "</table>";
                BodyMessage += "</body></html>";
                try
                {       
                        txtUser.Text = "";                       
                        persona.Clave = contraseniaEncriptada;
                        persona.CambiarClave = true;
                        db.SubmitChanges();
                        String respuesta = EnviarMailServer(Para, Asunto, BodyMessage, contraseniaNueva); 
                        if(respuesta == "OK")
                        {
                        msgbox(TipoMsgBox.confirmation, "Sistema", "La nueva contraseña se ha enviado. Revise su bandeja de entrada.");
                       
                        }
                        else
                        {
                        msgbox(TipoMsgBox.confirmation, "Sistema", "La nueva contraseña se ha cambiado pero no se envío al correo.");
                        }
                        upResetPassword.Update();
                       
                }
                catch (Exception ex)
                { 
                    throw ex;
                } 
            }
            else
            {
                msgbox(TipoMsgBox.error, "Sistema", "Usuario no existe.");
            }
        }
        
        //solo para correos gmail
        public void EnviarCorreo(String EnviarA,String ClaveNueva)
        {
            String correoAdmin = ConfigurationManager.AppSettings["correoElectronico"].ToString();
            String contraAdmin = ConfigurationManager.AppSettings["contraseniaCorreo"].ToString();
            String RutaImagenOpcional = "Recursos/img/LOGO-FARMAORION.png";
            String Asunto = "Recuperacion de Contraseña Farma Orion"; 
            String Body = "La clave nueva es: " + " " + ClaveNueva + ""; 
            //Configurar
            var smtp = new SmtpClient();
            {
                smtp.Host = "mail.consultoriamineradelperu.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(correoAdmin, contraAdmin);
                smtp.Timeout = 20000;
                
            }

            try
            {
                smtp.Send(correoAdmin, EnviarA, Asunto, Body);
            }
            catch (Exception ex)
            {

                msgbox(TipoMsgBox.confirmation, "Sistema", "No se pudo enviar el correo.");
            }
        }
         
        //para correos con dominio.com
        public String EnviarMailServer(MailAddress Para,String Asunto, String BodyMessage, String ClaveNueva)
        {
            String Resultado = "OK";
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();

            m.From = new MailAddress(Constantes.EmailServer);
            m.To.Add(Para);
            m.Subject = Asunto; 
            AlternateView imgview = AlternateView.CreateAlternateViewFromString(BodyMessage, null, MediaTypeNames.Text.Html); 
            LinkedResource lr = new LinkedResource(Server.MapPath("~/Recursos/assets/img/LOGO-FARMAORION.png")); 
            lr.ContentId = "imgpath";
            imgview.LinkedResources.Add(lr);
            m.AlternateViews.Add(imgview);
            m.Body = lr.ContentId;
            m.CC.Add(Constantes.EmailServer);
            m.IsBodyHtml = true;
            sc.Host = "mail.consultoriamineradelperu.com";
            string str1 = "gmail.com";
            string str2 = Constantes.EmailServer;
            if (str2.Contains(str1))
            {
                try
                {
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential(Constantes.EmailServer, Constantes.EmailPass);
                    sc.EnableSsl = true;
                    sc.Send(m);
                }
                catch (Exception ex)
                {
                    Resultado = ex.StackTrace;
                }
            }
            else
            {
                try
                {
                    sc.Port = 25;
                    sc.Credentials = new System.Net.NetworkCredential(Constantes.EmailServer, Constantes.EmailPass);
                    sc.EnableSsl = false;
                    sc.Send(m);
                }
                catch (Exception ex)
                {
                    Resultado = ex.StackTrace;
                }
            }
            return Resultado;
        }



    }
}