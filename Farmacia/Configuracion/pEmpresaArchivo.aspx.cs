using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Code;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Farmacia.Configuracion
{
	public partial class pEmpresaArchivo : PageBase
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				hfIDEmpresa.Value = Request.QueryString["pIDElemento"].ToString();
			}
		}

		protected void btnProcesar_Click(object sender, EventArgs e)
		{
			try
			{
				/*********CARGA DE ARCHIVO ********/
				String rutaServidor = ConfigurationManager.AppSettings["RutaArchivos"];
				/******************************/

				if (fuCarga.HasFile)
				{
					if (validarTipoArchivo(fuCarga.PostedFile))
					{
						if (validarTamanoArchivo(fuCarga.PostedFile))
						{
							StringBuilder validaciones = new StringBuilder();
							if (hfIDEmpresa.Value == "0") validaciones.Append("<div>Seleccione Empresa</div>");

							/*VALIDAMOS EL MIME TYPE*/

							HttpPostedFile file = fuCarga.PostedFile;
							String pArchivoExtension = Path.GetExtension(fuCarga.FileName).ToLower();

							//var contentType_Ori = MimeTypes.GetContentType(file.FileName);
						    var contentType_Ver = MimeTypes.GetContentType1(file);

							if (contentType_Ver.Equals("application/octet-stream"))
							{
								validaciones.Append("<div>Archivo no permitido la extensión no es la misma que el mime type del archivo (extensión " + pArchivoExtension + ").</div>");

							}

							/*FIN*/

							if (validaciones.Length > 0)
							{
								msgbox(TipoMsgBox.warning, validaciones.ToString());
								return;
							}

							/*Verificando Archivo*/
							String pAnio = DateTime.Now.Year.ToString();
							String pMes = DateTime.Now.Month.ToString();
							// String pMes = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.GetValue(DateTime.Now.Month - 1).ToString();
							BEEmpresa oBEEmp = new BLEmpresa().EmpresaSeleccionar(Int32.Parse(hfIDEmpresa.Value));

							String pNombreArchivo = hfIDEmpresa.Value + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + Path.GetExtension(fuCarga.FileName);
							String pRutaServidorFisico = oBEEmp.Ruc + "\\" + (ddlTipoArchivo.SelectedValue == "L" ? "Logo" : "Certificado") + "\\";
							String pRutaServidorWeb = oBEEmp.Ruc + "\\" + (ddlTipoArchivo.SelectedValue == "L" ? "Logo" : "Certificado") + "\\";
							String pRutaServidorFisicoFinal = rutaServidor + pRutaServidorFisico;

							/*Fin Archivo*/

							

							if (VerificarDirectorio(pRutaServidorFisicoFinal))
							{
								BEEmpresa oBELogo = new BEEmpresa();
								oBELogo.IDArchivoEmpresa = 0;
								oBELogo.IDEmpresa = IDEmpresa();
								oBELogo.TipoArchivo = ddlTipoArchivo.SelectedValue;
								oBELogo.RutaArchivo = pRutaServidorWeb;
								oBELogo.NombreArchivo = pNombreArchivo;
								oBELogo.IDUsuario = IDUsuario();

								BERetornoTran oBERetorno = new BERetornoTran();
								oBERetorno = new BLEmpresa().EmpresaArchivoGuardar(oBELogo);

								if (oBERetorno.Retorno == "1")
								{
									String FilePath = Path.Combine(pRutaServidorFisicoFinal, pNombreArchivo.ToString().Trim());
									fuCarga.SaveAs(FilePath);
									registrarScript("CerrarModalImagen();");
								}
								else
								{
									if (oBERetorno.Retorno != "-1")
									{
										msgbox(TipoMsgBox.warning, "Sistema", oBERetorno.ErrorMensaje);
									}
									else
									{
										RegistrarLogSistema("lnkGuardar_Click()", oBERetorno.ErrorMensaje, true);
									}

								}
							}
						}
						else
						{
							msgbox(TipoMsgBox.warning, "Facturacion", "El tamaño del archivo excede lo permitido.");
						}
					}
					else
					{
						msgbox(TipoMsgBox.warning, "Facturacion", "El formato del archivo no está permitido");
					}
				}
				else
				{
					msgbox(TipoMsgBox.warning, "Facturacion", "No se pudo guardar el archivo.");
				}

			}
			catch (Exception ex)
			{
				RegistrarLogSistema("btnAgregarArchivo_Click()", ex.ToString(), true);
			}
		}

		private static System.Drawing.Image ScaleImage(System.Drawing.Image image, Int32 maxAlto, Int32 maxAncho)
		{
			var newImage = new System.Drawing.Bitmap(maxAlto, maxAncho);
			using (var gr = System.Drawing.Graphics.FromImage(newImage))
			{
				gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
				gr.DrawImage(image, new System.Drawing.Rectangle(0, 0, maxAncho, maxAlto));
			}
			return newImage;
		}

		public Boolean validarTipoArchivo(HttpPostedFile archivoCargado)
		{
			Boolean pEstado = false;
			String Extension = Path.GetExtension(archivoCargado.FileName).ToLower();
			String[] permiteExtension = { ".jpg", ".jpeg", ".png", ".pfx", ".p12", ".txt", ".pdf" };
			for (int i = 0; i < permiteExtension.Length; i++)
			{
				if (Extension.ToLower() == permiteExtension[i])
				{
					pEstado = true;
				}
			}
			return pEstado;
		}

		public Boolean validarTamanoArchivo(HttpPostedFile archivoCargado)
		{
			Boolean pEstado = false;
			Double Tamano = Int32.Parse(hfMaximoArchivoByte.Value);
			Double TamanoArchivo = archivoCargado.ContentLength;
			if (TamanoArchivo <= Tamano)
			{
				pEstado = true;
			}
			return pEstado;
		}

		public Boolean VerificarDirectorio(String carpeta)
		{
			Boolean pEstado = false;
			if (!(Directory.Exists(carpeta)))
			{
				Directory.CreateDirectory(carpeta);
				pEstado = false;
			}
			if (Directory.Exists(carpeta))
			{
				pEstado = true;
			}
			else
			{
				pEstado = false;
			}
			return pEstado;
		}

	}
}