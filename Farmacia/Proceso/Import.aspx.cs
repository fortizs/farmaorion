using Farmacia.App_Class;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Proceso;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Proceso;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.Proceso
{
	public partial class Import : PageBase
	{
		String RutaArchivos = ConfigurationManager.AppSettings["RutaArchivos"] + "Proceso/Import/";

		protected void Page_Load(object sender, EventArgs e)
		{
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
			{
				ConfigPage();
				ListarEstructura();
			}
		}

		#region ListarEstructura

		private void ListarEstructura()
		{
			BLEstructuraProceso oBLEstructura = new BLEstructuraProceso();
			gvEstructura.DataSource = oBLEstructura.EstructuraProcesoListar(txtFiltro.Text.Trim());
			gvEstructura.DataBind();
		}

		protected void gvEstructura_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvEstructura.PageIndex = e.NewPageIndex;
			gvEstructura.SelectedIndex = -1;
			ListarEstructura();
		}

		protected void gvEstructura_SelectedIndexChanged(object sender, EventArgs e)
		{
			pnFiltroExport.Visible = true; 
			hfIDEstructuraProcesos.Value = gvEstructura.SelectedDataKey["IDEstructuraProceso"].ToString(); 
			hfNombreHoja.Value = gvEstructura.SelectedDataKey["NombreHoja"].ToString(); 
			txtEstructuraSel.Text = gvEstructura.SelectedRow.Cells[1].Text; 
			hfExtensionArchivo.Value = gvEstructura.SelectedRow.Cells[2].Text; 
			cListaArchivosGenerados.Visible = true;
			TramaLogListar();
		}

		protected void lbBuscar_Click(object sender, EventArgs e)
		{
			ListarEstructura();
		}

		#endregion

		#region Procesar
		 
		protected void btnProcesar_Click(object sender, EventArgs e)
		{            
			BEEstructuraProceso oBE = new BEEstructuraProceso(); 
			oBE = new BLEstructuraProceso().EstructuraProcesoSeleccionar(Int32.Parse(hfIDEstructuraProcesos.Value));
			string Extension = Path.GetExtension(fuCarga.FileName);
			hfNombreHoja.Value = oBE.NombreHoja;
			upFiltro.Update();

			if (hfExtensionArchivo.Value == ".xls")
			{
				CargaExcel();//PROCESA LA TRAMA
			}
			else {
				msgbox(TipoMsgBox.warning, "Tipo de Archivo no configurado para Procesar.");
			}
		}
		 
		private void CargaExcel()
		{
			string carpeta = Request.Path;
			string FilePath;

			string NombreArchivo = String.Empty;
			BERetornoTran oBERetorno = new BERetornoTran();
			BEGenerarTrama oBEGenTrama = new BEGenerarTrama();
			BLGenerarTrama oBLGenTrama = new BLGenerarTrama();
			BETramaLog oBETramaLog = new BETramaLog();
			BLTramaLog oBLTramaLog = new BLTramaLog();

			String FechaActual = DateTime.Now.ToString("ddMMyyyyhhmmss", CultureInfo.InvariantCulture);
			try
			{
				foreach (HttpPostedFile SubirFile in fuCarga.PostedFiles)
				{

					//NombreArchivo = Path.GetFileName(SubirFile.FileName);
					NombreArchivo = Path.GetFileNameWithoutExtension(SubirFile.FileName);

					if (NombreArchivo.Length == 0) { msgbox(TipoMsgBox.information, "Seleccione archivo a procesar."); return; }

					string Extension = Path.GetExtension(SubirFile.FileName);
					NombreArchivo = NombreArchivo + FechaActual + Extension;

					RutaArchivos = ConfigurationManager.AppSettings["RutaArchivos"];

					if (hfExtensionArchivo.Value != ".xls" && hfExtensionArchivo.Value != ".xlsx") { msgbox(TipoMsgBox.information, "Formato de archivo no válido."); return; }
					if (!System.IO.Directory.Exists(RutaArchivos))
					{
						Directory.CreateDirectory(RutaArchivos);
					}
					FilePath = Path.Combine(RutaArchivos, NombreArchivo.ToString().Trim());
					SubirFile.SaveAs(FilePath);

					BEParametrosConsultas oParam = new BEParametrosConsultas();
					Int32 NroLineas;
					oParam.Dato1 = Path.GetExtension(FilePath);
					oParam.Dato2 = FilePath;

					ArrayList pA = new ArrayList();
					Dictionary<String, Object> ResulArchivo = new Dictionary<String, Object>();

					ResulArchivo = oBLGenTrama.ObtenerHojaExcel(oParam);

					String codResultado = ResulArchivo["Codigo"].ToString();

					// 1: Correcto: Obtuvo Lista
					//-1: Error   :  En abrir Excel
					if (codResultado.Equals("1"))// SE OBTUVO LISTA 
					{
						pA = (ArrayList)ResulArchivo["Lista"];

						NroLineas = pA.Count;
						Boolean HojaEncontrada = false;
						Int32 Hojas = 0;
						foreach (string[] item in pA)
						{
							if (item[Hojas].ToString() == hfNombreHoja.Value.ToString() + "$")
							{ HojaEncontrada = true; }
							Hojas = Hojas + 1;
						}

						if (HojaEncontrada)
						{
							oBERetorno = oBLGenTrama.Crear_Temporal(Int32.Parse(hfIDEstructuraProcesos.Value), Int32.Parse(hfIDEstructuraProcesos.Value));

							if (oBERetorno.Retorno == "1")
							{
								oBERetorno = ImportarExcel(FilePath, Extension, "SI", hfNombreHoja.Value.ToString() + "$", hfNombreHoja.Value.ToString());

								if (oBERetorno.Retorno == "1")
								{
									//Inserta Archivo a Procesar
									DataTable dtObservados;
									DataTable dtCargados;
									oBETramaLog.IDEstructuraProceso = Int32.Parse(hfIDEstructuraProcesos.Value);
									oBETramaLog.IDTipoEjecucion = "MN"; // MN=Manual   AT=Automática (Servicio)
									oBETramaLog.RutaArchivo = Path.Combine(RutaArchivos, NombreArchivo);//Ruta Relativa
									oBETramaLog.NombreArchivo = NombreArchivo;
									oBETramaLog.CantidadI = 0; //Generados
									oBETramaLog.CantidadR = 0; //Rechazados
									oBETramaLog.IDEstadoEvento = "6"; //En PROCESO
									oBETramaLog.IDUsuario = IDUsuario();
									oBERetorno = oBLTramaLog.TramaLogInsertar(oBETramaLog);

									oBEGenTrama.IDEstructuraProducto = Int32.Parse(hfIDEstructuraProcesos.Value);
									oBEGenTrama.IDTipoEjecucion = "MN"; // MN=Manual   AT=Automática (Servicio)
									oBEGenTrama.IDTramaLog = Int32.Parse(oBERetorno.Retorno);
									oBEGenTrama.IDUsuario = IDUsuario();

									oBERetorno = oBLGenTrama.Ejecutar_TramaImport(oBEGenTrama);

									if (oBERetorno.Retorno == "1")
									{
										oBETramaLog.IDTramaLog = oBEGenTrama.IDTramaLog;
										oBEGenTrama = oBLGenTrama.Listar_LogObservaciones(oBEGenTrama.IDTramaLog);
										dtObservados = oBEGenTrama.Tramadt;

										oBEGenTrama = oBLGenTrama.Listar_LogCargados(oBETramaLog.IDTramaLog);
										dtCargados = oBEGenTrama.Tramadt;
										//Archivo del Log

										if (dtObservados.Rows.Count > 0 || dtCargados.Rows.Count > 0)
										{
											String NombreArchivolog = String.Empty;
											NombreArchivolog = NombreArchivo.Split('.')[0].ToString();
											NombreArchivolog = NombreArchivolog + "_log_" + DateTime.Now.ToString("yyyyMMddmmss") + ".xls";
											IWorkbook WorkBook;
											WorkBook = new HSSFWorkbook();
											HelperExcel.GenerarHoja(ref WorkBook, dtObservados, "Observados");
											HelperExcel.GenerarHoja(ref WorkBook, dtCargados, "Cargados");
											FileStream xfile = new FileStream(Path.Combine(RutaArchivos, NombreArchivolog), FileMode.Create, System.IO.FileAccess.Write);
											oBETramaLog.RutaArchivoLog = Path.Combine(RutaArchivos, NombreArchivolog);
											oBETramaLog.NombreArchivoLog = NombreArchivolog;
											WorkBook.Write(xfile);
											//xfile.Flush(); 
											xfile.Close();
										}
										oBETramaLog.CantidadI = oBEGenTrama.NumeroRegistros - oBEGenTrama.NumeroRechazos; //Generados
										oBETramaLog.CantidadR = oBEGenTrama.NumeroRechazos;
										oBETramaLog.IDEstadoEvento = "10"; //PROCESADO
										oBERetorno = oBLTramaLog.TramaLogActualizar(oBETramaLog);
										msgbox(TipoMsgBox.confirmation, "Archivo Cargado con éxito");
										TramaLogListar();
									}
									else
									{
										msgbox(TipoMsgBox.warning, "Mensaje", oBERetorno.ErrorMensaje);
									}

								}
								else
								{
									msgbox(TipoMsgBox.warning, "Mensaje", oBERetorno.ErrorMensaje);
								}
							}
							else {
								msgbox(TipoMsgBox.warning, "Mensaje", oBERetorno.ErrorMensaje);
							}
						}
						else
						{
							msgbox(TipoMsgBox.warning, "Hoja no encontrada para Procesar. Nombre Hoja: " + hfNombreHoja.Value.ToString());
						}
					}
					else
					{
						msgbox(TipoMsgBox.warning, ResulArchivo["Mensaje"].ToString());
					}
				}
			}
			catch (Exception ex)
			{
				msgbox(TipoMsgBox.error, "Error de Sistema", ex.ToString());
				registrarScript("Cargando(false);");
			}
			registrarScript("Cargando(false);");
		}

		private BERetornoTran ImportarExcel(string RutaArchivo, string Extension, string isHDR, string NombreHoja, String NombreTabla)
		{
			string conStr = "";
			BERetornoTran BEResul = new BERetornoTran();
			try
			{

				BLExtension oBLExtension = new BLExtension();
				BEExtension oBEExtension = new BEExtension();
				oBEExtension = oBLExtension.Extension(Extension);
				conStr = oBEExtension.Extension;

				conStr = String.Format(conStr, RutaArchivo, isHDR);
				//conStr ="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:/Archivos/Tramas/Configuracion/EstandardPrueba.xlsx; Extended Properties='Excel 8.0;HDR=No'";
				OleDbConnection connExcel = new OleDbConnection(conStr);
				OleDbCommand cmdExcel = new OleDbCommand();
				OleDbDataAdapter oda = new OleDbDataAdapter();
				DataTable dt = new DataTable();
				cmdExcel.Connection = connExcel;

				//leer la data de la hoja cargada
				connExcel.Open();
				cmdExcel.CommandText = "SELECT * From [" + NombreHoja + "]";
				oda.SelectCommand = cmdExcel;
				oda.Fill(dt);
				connExcel.Close();

				BLGenerarTrama blTrama = new BLGenerarTrama();
				BEResul = blTrama.Insertar_Trama_General(dt, NombreTabla);/*Nombre Hoja es igual nombre Tabla*/

			}
			catch (Exception ex)
			{
				BEResul.ErrorMensaje = ex.Message.ToString();
				BEResul.Retorno = "-1";
			}

			return BEResul;
		}

		#endregion

		#region TramaLog


		private void TramaLogListar()
		{
			BETramaLog oBETramaLog = new BETramaLog();
			oBETramaLog.IDEstructuraProceso = Int32.Parse(hfIDEstructuraProcesos.Value);
			oBETramaLog.NombreArchivo = txtArchivo.Text.Trim();
			oBETramaLog.FechaDesde = DateTime.Parse(txtFInicio.Text.Trim() == "" ? Constantes.FECHA_NULA : txtFInicio.Text.Trim());
			oBETramaLog.FechaHasta = DateTime.Parse(txtFFin.Text.Trim() == "" ? Constantes.FECHA_NULA : txtFFin.Text.Trim());
			oBETramaLog.Estado = (ddlEstadoTramaLog.SelectedValue == "1" ? true : false);
			gvTramaLog.DataSource = new BLTramaLog().TramaLogListar(oBETramaLog);
			gvTramaLog.DataBind();
		}

		protected void gvTramaLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvTramaLog.PageIndex = e.NewPageIndex;
			gvTramaLog.SelectedIndex = -1;
			TramaLogListar();
		}

		protected void gvTramaLog_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			hfidTramaLog.Value = e.CommandArgument.ToString();
			GridViewRow row = gvTramaLog.SelectedRow;

			if (e.CommandName == "Anular")
			{
				BLTramaLog oBLTramaLog = new BLTramaLog();
				BETramaLog oBETramaLog = new BETramaLog();
				BERetornoTran oBERetorno = new BERetornoTran();
				oBETramaLog.IDTramaLog = Int32.Parse(hfidTramaLog.Value);
				oBETramaLog.IDUsuario = IDUsuario();
				oBERetorno = oBLTramaLog.TramaLogEliminar(oBETramaLog);
				if (oBERetorno.Retorno != "-1")
				{
					msgbox(TipoMsgBox.confirmation, "Archivo Eliminado Correctamente");
					TramaLogListar();
					upTramaLog.Update();
				}
				else
				{
					msgbox(TipoMsgBox.error, oBERetorno.ErrorMensaje);
				}
			}

			if (e.CommandName == "ResumenLog")
			{
				BLTramaLog oBLTramaLog = new BLTramaLog();
				BETramaLog oBETramaLog = new BETramaLog();
				oBETramaLog.IDTramaLog = Int32.Parse(hfidTramaLog.Value);

				oBETramaLog = oBLTramaLog.Obtener_TramaLog(oBETramaLog);

				String NombreArchivo = oBETramaLog.NombreArchivoLog;
				String RutaArchivo = oBETramaLog.RutaArchivoLog;

				if (NombreArchivo.Trim().ToString().Length > 0)
				{
					HttpContext.Current.Response.Clear();
					HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
					HttpContext.Current.Response.ContentType = "text/plain";
					HttpContext.Current.Response.WriteFile(Path.Combine(RutaArchivos, RutaArchivo));
					HttpContext.Current.Response.Flush();
					HttpContext.Current.Response.End();
				}
				else
				{
					msgbox(TipoMsgBox.warning, "Archivo Log no válido.");
				}
			}

			if (e.CommandName == "ArchivoCarga")
			{

				BLTramaLog oBLTramaLog = new BLTramaLog();
				BETramaLog oBETramaLog = new BETramaLog();
				oBETramaLog.IDTramaLog = Int32.Parse(hfidTramaLog.Value);

				oBETramaLog = oBLTramaLog.Obtener_TramaLog(oBETramaLog);

				String NombreArchivo = oBETramaLog.NombreArchivo;
				String RutaArchivo = oBETramaLog.RutaArchivo;

				if (NombreArchivo.Trim().ToString().Length > 0)
				{
					HttpContext.Current.Response.Clear();
					HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + NombreArchivo);
					HttpContext.Current.Response.ContentType = "text/plain";
					HttpContext.Current.Response.WriteFile(Path.Combine(RutaArchivos, RutaArchivo));
					HttpContext.Current.Response.Flush();
					HttpContext.Current.Response.End();
				}
				else
				{
					msgbox(TipoMsgBox.warning, "Archivo Cargado no válido.");
				}
			}
		}

		protected void gvTramaLog_SelectedIndexChanged(object sender, EventArgs e)
		{
			String NombreArchivo = ((Label)gvTramaLog.SelectedRow.Cells[1].FindControl("lblNombreArchivo")).Text;
			String RutaArchivo = ((Label)gvTramaLog.SelectedRow.Cells[2].FindControl("lblRutaArchivo")).Text;
		}

		protected void lnkBuscar_Click(object sender, EventArgs e)
		{
			TramaLogListar();
		}
		 
		#endregion

	}
}