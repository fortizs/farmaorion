
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Farmacia
{
	public class EmailSender : System.Web.UI.Page
	{
		public void SendEmail(SmtpConfig SmtpConfig, String subject, String body, MailAddress from, MailAddress to, IEnumerable<string> bcc = null, IEnumerable<string> cc = null, AttachmentCollection Attachments = null)
		{
			var message = new MailMessage();
			message.From = from;
			message.To.Add(to);
			if (null != bcc)
			{
				foreach (var address in bcc.Where(bccValue => !String.IsNullOrWhiteSpace(bccValue)))
				{
					message.Bcc.Add(address.Trim());
				}
			}
			if (null != cc)
			{
				foreach (var address in cc.Where(ccValue => !String.IsNullOrWhiteSpace(ccValue)))
				{
					message.CC.Add(address.Trim());
				}
			}
			message.Subject = subject;
			message.Body = body;
			message.IsBodyHtml = true;

			//FileStream fs = new FileStream("E:\\TestFolder\\test.pdf", FileMode.Open, FileAccess.Read); 
			//Attachment a = new Attachment(fs, "test.pdf", MediaTypeNames.Application.Octet);
			if (null != Attachments)
			{
				foreach (Attachment att in Attachments)
				{
					message.Attachments.Add(att);
				}
			}

			using (var smtpClient = new SmtpClient())
			{
				smtpClient.UseDefaultCredentials = SmtpConfig.DefaultCredentials;
				smtpClient.Host = SmtpConfig.Host;
				smtpClient.Port = SmtpConfig.Port;
				smtpClient.EnableSsl = SmtpConfig.EnableSsl;
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				if (SmtpConfig.DefaultCredentials)
					smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
				else
					smtpClient.Credentials = new NetworkCredential(SmtpConfig.UserName, SmtpConfig.Password);

				smtpClient.Send(message);
			}
		}
	}

	public class SmtpConfig : System.Web.UI.Page
	{
		public SmtpConfig()
		{
			BEEmpresa oBEEmisor = new BLEmpresa().EmpresaSeleccionar(Int32.Parse(Session["IDEmpresa"].ToString()));

			_Host = oBEEmisor.Host;
			_DefaultCredentials = oBEEmisor.DefaultCredencial;
			_Port = int.Parse(oBEEmisor.Puerto.ToString());
			_UserName = oBEEmisor.Correo;
			_Password = oBEEmisor.ClaveCorreo;
			_EnableSsl = oBEEmisor.HabilitarSSL;
		}
		private string _Host;
		private bool _DefaultCredentials;
		private int _Port;
		private string _UserName;
		private string _Password;
		private bool _EnableSsl;

		public string Host
		{
			get { return _Host; }
			set { _Host = value; }
		}
		public bool DefaultCredentials
		{
			get { return _DefaultCredentials; }
			set { _DefaultCredentials = value; }
		}
		public int Port
		{
			get { return _Port; }
			set { _Port = value; }
		}
		public string UserName
		{
			get { return _UserName; }
			set { _UserName = value; }
		}
		public string Password
		{
			get { return _Password; }
			set { _Password = value; }
		}
		public bool EnableSsl
		{
			get { return _EnableSsl; }
			set { _EnableSsl = value; }
		}
	}
}