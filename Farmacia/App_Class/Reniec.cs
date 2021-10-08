using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace Farmacia.App_Class
{

	public class Reniec
	{
		#region Variables
		private string _Nombre;
		private string _Distrito;
		private string _Dni;
		private string _Provincia;
		private string _Departamento;
		private CookieContainer myCookie;
		private Resul state;
		#endregion

		public Reniec()
		{
			//try
			//{
			myCookie = null;
			myCookie = new CookieContainer();
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
			//}
			//catch (Exception ex)
			//{
			//    throw ex;
			//}
		}
		public enum Resul
		{
			Ok = 0,
			NoResul = 1,
			ErrorCapcha = 2,
			Error = 3,
		}
		public string Dni
		{
			get { return _Dni; }
		}
		public string Nombre
		{
			get { return _Nombre; }
		}
		public string Distrido
		{
			get { return _Distrito; }
		}

		public string Provincia
		{
			get { return _Provincia; }
		}
		public string Departamento
		{
			get { return _Departamento; }
		}

		public Resul GetResul
		{
			get { return state; }
		}
		private Boolean ValidarCertificado(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		public void GetInfo(string numRuc)
		{
			try
			{


				//A este link le pasamos los datos , RUC y valor del captcha
				//string myUrl = String.Format("http://clientes.reniec.gob.pe/padronElectoral2012/consulta.htm?hTipo=2&hDni={0}", numRuc);
				string myUrl = String.Format("http://aplicaciones007.jne.gob.pe/srop_publico/Consulta/Afiliado/GetNombresCiudadano?DNI={0}", numRuc);
				//string myUrl = String.Format("http://aplicaciones007.jne.gob.pe/srop_publico/Consulta/Afiliado/GetNombresCiudadano?DNI={0}", numRuc);


				HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
				myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
				myWebRequest.CookieContainer = myCookie;
				myWebRequest.Credentials = CredentialCache.DefaultCredentials;
				myWebRequest.Proxy = null;
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
				Stream myStream = myHttpWebResponse.GetResponseStream();
				StreamReader myStreamReader = new StreamReader(myStream);
				//Leemos los datos
				string xDat = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
				string[] _split = xDat.Split(new char[] { '<', '>', '\n', '\r' });
				List<String> _result = new List<String>();
				//quitamos todos los caracteres nul
				for (int i = 0; i < _split.Length; i++)
				{
					if (!string.IsNullOrEmpty(_split[i].Trim()))
						_result.Add(_split[i].Trim());
				}

				if (_result.Count == 80)
					state = Resul.NoResul;
				if (_result.Count >= 100)
					state = Resul.Ok;
				switch (state)
				{
					case Resul.Ok:
						StateOK(xDat, numRuc);
						break;
					case Resul.NoResul:
						break;
					default:
						break;
				}
				myHttpWebResponse.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void StateOK(string xDat, string numRuc)
		{
			//declarar Variables
			string xDni = string.Empty;
			string xNombre = string.Empty;
			string xProvincia = string.Empty;
			string xDistrito = string.Empty;
			string xDepartamento = string.Empty;
			string[] tabla;
			//reemplazar o quitar caracteres
			xDat = xDat.Replace("     ", " ");
			xDat = xDat.Replace("    ", " ");
			xDat = xDat.Replace("   ", " ");
			xDat = xDat.Replace("  ", " ");
			xDat = xDat.Replace("( ", "(");
			xDat = xDat.Replace(" )", ")");
			//convertir a tabla en un arreglo de string como se ve declarado arriba
			//tabla = Regex.Split(xDat, "class");
			////Depende el numero de ruc 1 natural  o 2 empresa               


			////reemplazar o quitar caracteres
			//tabla[9] = tabla[9].Replace("=\"txtCuerpo\">", "");
			//tabla[9] = tabla[9].Replace("</td>\r\n </tr>\r\n <tr>\r\n", "");
			//tabla[9] = tabla[9].Replace("<td", "");

			//tabla[15] = tabla[15].Replace("=\"txtCuerpo\">", "");
			//tabla[15] = tabla[15].Replace("</td>\r\n </tr>\r\n <tr>\r\n", "");
			//tabla[15] = tabla[15].Replace("<td", "");

			//tabla[17] = tabla[17].Replace("=\"txtCuerpo\">", "");
			//tabla[17] = tabla[17].Replace("</td>\r\n </tr>\r\n <tr>\r\n", "");
			//tabla[17] = tabla[17].Replace("<td", "");

			//tabla[19] = tabla[19].Replace("=\"txtCuerpo\">", "");
			//tabla[19] = tabla[19].Replace("</td>\r\n </tr>\r\n <tr>\r\n", "");
			//tabla[19] = tabla[19].Replace("<td", "");
			//tabla[19] = tabla[19].Replace("</td>", "");
			//tabla[19] = tabla[19].Replace("</tr>", "");
			//tabla[19] = tabla[19].Replace("</table>", "");
			//tabla[19] = tabla[19].Replace("</td>", "");
			//tabla[19] = tabla[19].Replace("</tr>", "");
			//tabla[19] = tabla[19].Replace("<tr", "");


			//xDni = numRuc;
			//xNombre = (string)tabla[9];     //nombre
			//xDistrito = (string)tabla[15];      // distrito
			//xProvincia = (string)tabla[17];     //provincia
			//xDepartamento = (string)tabla[19];      //departamento

			tabla = Regex.Split(xDat, "<bod");
			//Depende el numero de ruc 1 natural  o 2 empresa               


			//reemplazar o quitar caracteres
			//tabla[1] = tabla[1].Replace("y>", "");

			tabla[0] = tabla[0].Replace("|", " ");
			tabla[0] = tabla[0].Replace("|", ", ");




			xDni = numRuc;
			xNombre = (string)tabla[0];     //nombre
											//   xDistrito = (string)tabla[15];      // distrito
											//  xProvincia = (string)tabla[17];     //provincia
											//xDepartamento = (string)tabla[19];      //departamento

			//los resultados
			_Dni = xDni;
			_Nombre = xNombre;
			//	_Distrito = xDistrito;
			//	_Provincia = xProvincia;
			//	_Departamento = xDepartamento;

		}
	}

}
