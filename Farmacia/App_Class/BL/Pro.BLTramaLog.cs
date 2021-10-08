using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Proceso;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.Proceso
{
	public class BLTramaLog : BLBase
	{
		public BERetornoTran TramaLogInsertar(BETramaLog oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("pro.TramaLogInsertar");
			cmd.Parameters.Add("@IDEstructuraProceso", SqlDbType.Int, 10).Value = oBE.IDEstructuraProceso;
			cmd.Parameters.Add("@RutaArchivo", SqlDbType.VarChar, 1000).Value = oBE.RutaArchivo;
			cmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar, 1000).Value = oBE.NombreArchivo;  
			cmd.Parameters.Add("@IDTipoEjecucion", SqlDbType.VarChar, 10).Value = oBE.IDTipoEjecucion;
			cmd.Parameters.Add("@CantidadI", SqlDbType.Int, 10).Value = oBE.CantidadI;
			cmd.Parameters.Add("@CantidadR", SqlDbType.Int, 10).Value = oBE.CantidadR;
			cmd.Parameters.Add("@IDEstadoEvento", SqlDbType.VarChar, 10).Value = oBE.IDEstadoEvento;  
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
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

		public BERetornoTran TramaLogActualizar(BETramaLog oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("pro.TramaLogActualizar");
			cmd.Parameters.Add("@IDTramaLog", SqlDbType.Int, 10).Value = oBE.IDTramaLog;
			cmd.Parameters.Add("@RutaArchivo", SqlDbType.VarChar, 1000).Value = oBE.RutaArchivo;
			cmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar, 1000).Value = oBE.NombreArchivo;
			cmd.Parameters.Add("@RutaArchivoLog", SqlDbType.VarChar, 1000).Value = oBE.RutaArchivoLog;
			cmd.Parameters.Add("@NombreArchivoLog", SqlDbType.VarChar, 1000).Value = oBE.NombreArchivoLog;
			cmd.Parameters.Add("@IDTipoEjecucion", SqlDbType.VarChar, 10).Value = oBE.IDTipoEjecucion;
			cmd.Parameters.Add("@IDEstadoEvento", SqlDbType.VarChar, 10).Value = oBE.IDEstadoEvento;
			cmd.Parameters.Add("@CantidadI", SqlDbType.Int, 10).Value = oBE.CantidadI;
			cmd.Parameters.Add("@CantidadR", SqlDbType.Int, 10).Value = oBE.CantidadR; 
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
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

		public BERetornoTran TramaLogEliminar(BETramaLog oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("pro.TramaLogEliminar");
			cmd.Parameters.Add("@IDTramaLog", SqlDbType.Int, 10).Value = oBE.IDTramaLog; 
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
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
		 
		public IList TramaLogListar(BETramaLog pEntidad)
		{ 
			SqlCommand cmd = ConexionCmd("pro.TramaLogListar");
			BETramaLog oBE = new BETramaLog(); 
			cmd.Parameters.Add("@IDEstructuraProceso", SqlDbType.Int, 10).Value = pEntidad.IDEstructuraProceso;
			cmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar, 200).Value = pEntidad.NombreArchivo; 
			cmd.Parameters.Add("@FechaDesde", SqlDbType.DateTime, 50).Value = pEntidad.FechaDesde;
			cmd.Parameters.Add("@FechaHasta", SqlDbType.DateTime, 50).Value = pEntidad.FechaHasta; 
			cmd.Parameters.Add("@Estado", SqlDbType.VarChar, 50).Value = pEntidad.Estado;  
			ArrayList listaAux = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleResult);

				while (rd.Read())// 
				{
					oBE = new BETramaLog();
					oBE.IDTramaLog = rd.GetInt32(rd.GetOrdinal("IDTramaLog")); 
					oBE.NombreArchivo = rd.GetString(rd.GetOrdinal("NombreArchivo"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.CantidadI = rd.GetInt32(rd.GetOrdinal("CantidadI"));
					oBE.CantidadR = rd.GetInt32(rd.GetOrdinal("CantidadR"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.EstadoEvento = rd.GetString(rd.GetOrdinal("EstadoEvento")); 
					oBE.ArchivoLog = rd.GetBoolean(rd.GetOrdinal("ArchivoLog")); 

					listaAux.Add(oBE);
					oBE = null;
				}

				rd.Close();
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
			return listaAux;
		}
		 
		public BETramaLog Obtener_TramaLog(BETramaLog pEntidad)
		{

			SqlCommand cmd = ConexionCmd("Pro.Obtener_TramaLog");
			BETramaLog oBE = new BETramaLog();
			cmd.Parameters.Add("@IDTramaLog", SqlDbType.Int, 10).Value = pEntidad.IDTramaLog;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SingleResult);
				while (rd.Read())// 
				{
					oBE.IDTramaLog = rd.GetInt32(rd.GetOrdinal("IDTramaLog"));
					oBE.NombreArchivo = rd.GetString(rd.GetOrdinal("NombreArchivo"));
					oBE.RutaArchivo = rd.GetString(rd.GetOrdinal("RutaArchivo"));
					oBE.NombreArchivoLog = rd.GetString(rd.GetOrdinal("NombreArchivoLog"));
					oBE.RutaArchivoLog = rd.GetString(rd.GetOrdinal("RutaArchivoLog"));
				}

				rd.Close();
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


	}
}