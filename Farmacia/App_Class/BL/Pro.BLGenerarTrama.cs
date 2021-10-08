using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using Farmacia.App_Class.BE.General;
using System.Data.SqlClient;
using Farmacia.App_Class.BE.Proceso;
using System.IO;
using ExcelDataReader;

namespace Farmacia.App_Class.BL.Proceso
{
	public class BLGenerarTrama : BLBase
	{

		public BERetornoTran Crear_Temporal(int pIDEstructuraProceso, int pIDEstructuraProducto)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("Pro.Tabla_CrearTemporal"); 
			cmd.Parameters.Add("@IDEstructuraProcesos", SqlDbType.Int, 10).Value = pIDEstructuraProceso;
			cmd.Parameters.Add("@IDEstructuraProductos", SqlDbType.Int, 10).Value = pIDEstructuraProducto;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;

			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
			}
			catch (Exception ex)
			{
				BERetorno.ErrorMensaje = ex.Message.ToString();
				BERetorno.Retorno = "-1";
			}
			finally
			{
				if (cmd.Connection.State == ConnectionState.Open)
				{
					cmd.Connection.Close();
				}
			}
			return BERetorno;
		}
		 
		public Dictionary<String, Object> ObtenerHojaExcel(BEParametrosConsultas pFiltros)
		{
			Dictionary<String, Object> DIResul = new Dictionary<String, Object>();

			BEParametrosConsultas oBE = new BEParametrosConsultas();
			string conStr = Convert.ToString(CadenaConexionE(pFiltros.Dato1));
			conStr = String.Format(conStr, pFiltros.Dato2, "No");
			OleDbConnection connExcel = new OleDbConnection(conStr);
			ArrayList lista = new ArrayList();
			try
			{
				connExcel.Open();
				DataTable dt = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				if (dt == null)
				{
					return null;
				}

				String[] excelSheets = new String[dt.Rows.Count];
				int i = 0;
				foreach (DataRow row in dt.Rows)
				{
					excelSheets[i] = row["TABLE_NAME"].ToString();
					lista.Add(excelSheets);
					i++;
				}
				oBE = null;
				dt.Clear();
				DIResul.Add("Codigo", "1");
				DIResul.Add("Mensaje", "Ok");
				DIResul.Add("Lista", lista);

			}
			catch (Exception ex)
			{

				DIResul.Add("Codigo", "-1");
				DIResul.Add("Mensaje", ex.Message.ToString());
				DIResul.Add("Lista", DBNull.Value);

				//BEResul.ErrorMensaje =  ex.Message.ToString() ;
				//BEResul.Retorno = "-1";
				//DIResul.Add(lista, BEResul);
			}
			finally
			{
				if ((connExcel.State == ConnectionState.Open))
				{
					connExcel.Close();
					connExcel.Dispose();
				}
			}
			return DIResul;
		}
		 
		public BERetornoTran Ejecutar_TramaImport(BEGenerarTrama pEntidad)
		{

			SqlCommand cmd = ConexionCmd("Pro.Ejecutar_TramaImportCore");
			BERetornoTran BERetorno = new BERetornoTran();
			string Linea = string.Empty;

			cmd.Parameters.Add("@IDEstructuraProducto", SqlDbType.Int).Value = pEntidad.IDEstructuraProducto;
			cmd.Parameters.Add("@IDTipoEjecucion", SqlDbType.VarChar, 10).Value = pEntidad.IDTipoEjecucion;
			cmd.Parameters.Add("@IDTramaLog", SqlDbType.Int, 10).Value = pEntidad.IDTramaLog;
			cmd.Parameters.Add("@Adicional1", SqlDbType.Char, 50).Value = pEntidad.Adicional1;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 10).Value = pEntidad.IDUsuario;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
			 
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if ((cmd.Connection.State == ConnectionState.Open))
				{
					cmd.Connection.Close();
				}
			}
			return BERetorno;
		}
		 
		public BEGenerarTrama Listar_LogObservaciones(Int32 pIDTramaLog)
		{

			SqlCommand cmd = ConexionCmd("Pro.TramaLog_ObservacionesCore");

			BEGenerarTrama oBE = new BEGenerarTrama();
			DataSet ds = new DataSet();
			cmd.Parameters.Add("@IDTramaLog", SqlDbType.Int).Value = pIDTramaLog;
			cmd.Parameters.Add("@NumeroRegistros", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@NumeroRechazos", SqlDbType.Int, 10).Direction = ParameterDirection.Output;

			try
			{

				SqlDataAdapter oDA = new SqlDataAdapter();
				oDA.SelectCommand = cmd;
				oDA.Fill(ds);
				oBE.NumeroRegistros = Int32.Parse(cmd.Parameters["@NumeroRegistros"].Value.ToString());
				oBE.NumeroRechazos = Int32.Parse(cmd.Parameters["@NumeroRechazos"].Value.ToString());

				oBE.Tramadt = ds.Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if ((cmd.Connection.State == ConnectionState.Open))
				{
					cmd.Connection.Close();
				}
			}
			return oBE;
		}
		 
		public BEGenerarTrama Listar_LogCargados(Int32 pIDTramaLog)
		{

			SqlCommand cmd = ConexionCmd("Pro.TramaLog_Cargados");

			BEGenerarTrama oBE = new BEGenerarTrama();
			DataSet ds = new DataSet();
			cmd.Parameters.Add("@IDTramaLog", SqlDbType.Int).Value = pIDTramaLog;
			cmd.Parameters.Add("@NumeroRegistros", SqlDbType.Int, 10).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@NumeroRechazos", SqlDbType.Int, 10).Direction = ParameterDirection.Output;

			try
			{

				SqlDataAdapter oDA = new SqlDataAdapter();
				oDA.SelectCommand = cmd;
				oDA.Fill(ds);
				oBE.NumeroRegistros = Int32.Parse(cmd.Parameters["@NumeroRegistros"].Value.ToString());
				oBE.NumeroRechazos = Int32.Parse(cmd.Parameters["@NumeroRechazos"].Value.ToString());

				oBE.Tramadt = ds.Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if ((cmd.Connection.State == ConnectionState.Open))
				{
					cmd.Connection.Close();
				}
			}
			return oBE;
		}
		 
		public BERetornoTran Insertar_Trama_General(DataTable dtTrama, string Archivo)
		{

			BERetornoTran Resul = new BERetornoTran();
			Int32 filasAfectadas = -1;

			try
			{
				SqlConnection objcn = new SqlConnection(CadenaConexionTemporal());
				objcn.Open();

				try
				{
					using (SqlBulkCopy copy = new SqlBulkCopy(objcn, SqlBulkCopyOptions.TableLock, null))
					{

						for (int i = 0; i < dtTrama.Columns.Count; i++)
						{
							//copy.ColumnMappings.Add(dtTrama.Columns[i].Caption.Trim(), dtTrama.Columns[i].Caption);
							copy.ColumnMappings.Add(i, i);
							//copy.ColumnMappings.Add("Producto", "Producto");
						}
						copy.DestinationTableName = "dbo." + Archivo;
						copy.BulkCopyTimeout = 60;
						copy.WriteToServer(dtTrama);
						filasAfectadas = 1;

						Resul.ErrorMensaje = "Ok";
						Resul.Retorno = filasAfectadas.ToString();
					}
				}
				catch (Exception ex)
				{
					Resul.ErrorMensaje = ex.Message;
					Resul.Retorno = "-1";
				}
				finally
				{
					if (objcn.State == ConnectionState.Open)
					{
						objcn.Close();
					}
				}
			}
			catch (Exception ex)
			{
				Resul.ErrorMensaje = ex.Message;
				Resul.Retorno = "-1";
			}
			return Resul;
		}
		 
		public Dictionary<String, Object> ObtenerNombreHojaExcel(BEParametrosConsultas pFiltros)
		{
			Dictionary<String, Object> DIResul = new Dictionary<String, Object>();
			ArrayList lista = new ArrayList();
			try
			{
				FileStream stream;
				stream = File.Open(pFiltros.Dato2, FileMode.Open, FileAccess.Read);

				IExcelDataReader excelReader;

				if (Path.GetExtension(pFiltros.Dato2).ToUpper().Equals(".XLS"))
				{
					excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
				}
				else {
					excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
				}

				DataSet result = excelReader.AsDataSet();

				//Ejemplos de acceso a datos
				Int32 i = result.Tables.Count;
				for (i = 0; i < result.Tables.Count; i++)
				{
					lista.Add(result.Tables[i].TableName);
				}

				excelReader.Close();
				excelReader.Dispose();
				excelReader = null;
				stream.Close();
				stream.Dispose();
				stream = null;

				DIResul.Add("Codigo", "1");
				DIResul.Add("Mensaje", "Ok");
				DIResul.Add("Lista", lista);

			}
			catch (Exception ex)
			{

				DIResul.Add("Codigo", "-1");
				DIResul.Add("Mensaje", ex.Message.ToString());
				DIResul.Add("Lista", DBNull.Value);


			}

			return DIResul;
		}


		public BERetornoTran Insertar_ConfiguracionGenerarTramaEnvio(int IDTramaLog, int IDProducto, DateTime FechaProceso, int IDConfiguracion, String RutaArchivo, String NombreArchivo)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("ope.ConfiguracionGeneraTramaEnvio_Pendiente");

			cmd.Parameters.Add("@IDTramaLog", SqlDbType.Int, 10).Value = IDTramaLog;
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 10).Value = IDProducto;
			cmd.Parameters.Add("@FechaProceso", SqlDbType.DateTime, 20).Value = FechaProceso;
			cmd.Parameters.Add("@IDConfiguracion", SqlDbType.Int, 10).Value = IDConfiguracion;
			cmd.Parameters.Add("@RutaArchivo", SqlDbType.VarChar, 500).Value = RutaArchivo;
			cmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar, 500).Value = NombreArchivo;

			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;

			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
			}
			catch (Exception ex)
			{
				BERetorno.ErrorMensaje = ex.ToString();
			}
			finally
			{
				if (cmd.Connection.State == ConnectionState.Open)
				{
					cmd.Connection.Close();
				}
			}
			return BERetorno;
		}




	}
}