using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Farmacia.Seguridad
{
	public partial class vUtilSubirImagen : PageBase
	{
		 
		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				hfIDPersona.Value = Request.QueryString["pIDElemento"].ToString(); 
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
							if (hfIDPersona.Value == "0") validaciones.Append("<div>Seleccione Persona</div>");
							 
							if (validaciones.Length > 0)
							{
								msgbox(TipoMsgBox.warning, validaciones.ToString());
								return;
							}

							/*Verificando Archivo*/
							String pAnio = DateTime.Now.Year.ToString();
							String pMes = DateTime.Now.Month.ToString();
							// String pMes = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.GetValue(DateTime.Now.Month - 1).ToString();
							BEColaborador oBEPer = new BLColaborador().SeleccionarColaborador(Int32.Parse(hfIDPersona.Value));
							BEEmpresa oBEEmp = new BLEmpresa().EmpresaSeleccionar(oBEPer.IDEmpresa);

							String pNombreArchivo = hfIDPersona.Value + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + "_ori" + Path.GetExtension(fuCarga.FileName);
							String pNombreArchivo_Max = hfIDPersona.Value + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + "_max" + Path.GetExtension(fuCarga.FileName);
							String pRutaServidorFisico = "\\Foto\\" + oBEEmp.Ruc + "\\" + hfIDPersona.Value + "\\";
							String pRutaServidorWeb = "\\Foto\\" + oBEEmp.Ruc + "\\" + hfIDPersona.Value + "\\";
							String pRutaServidorFisicoFinal = rutaServidor + pRutaServidorFisico;

							/*Fin Archivo*/
							 
							if (VerificarDirectorio(pRutaServidorFisicoFinal))
							{
								BEColaborador oBEx = new BEColaborador();
								oBEx.IDColaborador = Int32.Parse(hfIDPersona.Value);
								oBEx.RutaImagenFoto = pRutaServidorWeb;
								oBEx.NombreImagenFoto = pNombreArchivo_Max;
								BERetornoTran oBERetorno = new BERetornoTran();
								oBERetorno = new BLColaborador().ColaboradorImagenActualizar(oBEx);
								 
								if (oBERetorno.Retorno == "1")
								{ 
									String FilePath = Path.Combine(pRutaServidorFisicoFinal, pNombreArchivo.ToString().Trim());
									fuCarga.SaveAs(FilePath);

									//Cambiando tamaño
									System.Drawing.Bitmap bImagenMax = new System.Drawing.Bitmap(fuCarga.PostedFile.InputStream);
									System.Drawing.Image iImagenMax = ScaleImage(bImagenMax, 128, 128);
									iImagenMax.Save(pRutaServidorFisicoFinal + pNombreArchivo_Max, System.Drawing.Imaging.ImageFormat.Jpeg);
									//


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
			String[] permiteExtension = { ".jpg", ".jpeg", ".png"};
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