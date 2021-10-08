using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLCotizacion : BLBase
	{
		public IList CotizacionListar(BECotizacion BEParam)
		{
			SqlCommand cmd = ConexionCmd("gen.CotizacionListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 50).Value = BEParam.Filtro;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = BEParam.IDCliente;
			cmd.Parameters.Add("@IDEstado", SqlDbType.Int).Value = BEParam.IDEstado;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = BEParam.FechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = BEParam.FechaFin;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;

			BECotizacion oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECotizacion();
					oBE.IDCotizacion = rd.GetInt32(rd.GetOrdinal("IDCotizacion"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.Anio = rd.GetInt32(rd.GetOrdinal("Anio"));
					oBE.NumeroCotizacion = rd.GetInt32(rd.GetOrdinal("NumeroCotizacion"));
					oBE.FechaCotizacion = rd.GetDateTime(rd.GetOrdinal("FechaCotizacion"));
					oBE.CalculoIGV = rd.GetDecimal(rd.GetOrdinal("CalculoIGV"));
					oBE.TotalOperacionGravada = rd.GetDecimal(rd.GetOrdinal("TotalOperacionGravada"));
					oBE.TotalIGV = rd.GetDecimal(rd.GetOrdinal("TotalIGV"));
					oBE.TotalDescuentos = rd.GetDecimal(rd.GetOrdinal("TotalDescuentos"));
					oBE.TotalCotizacion = rd.GetDecimal(rd.GetOrdinal("TotalCotizacion"));
					oBE.TipoCambio = rd.GetDecimal(rd.GetOrdinal("TipoCambio"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
					oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.ClienteNumeroDocumento = rd.GetString(rd.GetOrdinal("ClienteNumeroDocumento"));
					oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
					oBE.EstadoNombre = rd.GetString(rd.GetOrdinal("EstadoNombre"));
					oBE.MotivoAnulacion = rd.GetString(rd.GetOrdinal("MotivoAnulacion"));
					oBE.NumeroCotizacionFormato = rd.GetString(rd.GetOrdinal("NumeroCotizacionFormato"));
					lista.Add(oBE);
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
			return lista;
		}
		 
		public BECotizacion CotizacionSeleccionar(Int32 pIDCotizacion)
		{
			SqlCommand cmd = ConexionCmd("gen.CotizacionSeleccionar");
			BECotizacion oBE = new BECotizacion();
			cmd.Parameters.Add("@IDCotizacion", SqlDbType.Int).Value = pIDCotizacion;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDCotizacion = rd.GetInt32(rd.GetOrdinal("IDCotizacion"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.Anio = rd.GetInt32(rd.GetOrdinal("Anio"));
					oBE.NumeroCotizacion = rd.GetInt32(rd.GetOrdinal("NumeroCotizacion"));
					oBE.FechaCotizacion = rd.GetDateTime(rd.GetOrdinal("FechaCotizacion"));
					oBE.CalculoIGV = rd.GetDecimal(rd.GetOrdinal("CalculoIGV"));
					oBE.TotalOperacionGravada = rd.GetDecimal(rd.GetOrdinal("TotalOperacionGravada"));
					oBE.TotalIGV = rd.GetDecimal(rd.GetOrdinal("TotalIGV"));
					oBE.TotalDescuentos = rd.GetDecimal(rd.GetOrdinal("TotalDescuentos"));
					oBE.TotalCotizacion = rd.GetDecimal(rd.GetOrdinal("TotalCotizacion"));
					oBE.TipoCambio = rd.GetDecimal(rd.GetOrdinal("TipoCambio"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
					oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.ClienteNumeroDocumento = rd.GetString(rd.GetOrdinal("ClienteNumeroDocumento"));
					oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
					oBE.EstadoNombre = rd.GetString(rd.GetOrdinal("EstadoNombre"));
					oBE.MotivoAnulacion = rd.GetString(rd.GetOrdinal("MotivoAnulacion"));
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
		 
		public BERetornoTran CotizacionGuardar(BECotizacion BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionGuardar");
			cmd.Parameters.Add("@IDCotizacion", SqlDbType.Int).Value = BEParam.IDCotizacion;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = BEParam.IDCliente;
			cmd.Parameters.Add("@IDMoneda", SqlDbType.VarChar, 5).Value = BEParam.IDMoneda;
			cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = BEParam.TipoCambio; 
			cmd.Parameters.Add("@FechaCotizacion", SqlDbType.DateTime).Value = BEParam.FechaCotizacion;
			cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = BEParam.CalculoIGV;
			cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = BEParam.TotalOperacionGravada;
			cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = BEParam.TotalIGV;
			cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = BEParam.TotalDescuentos;
			cmd.Parameters.Add("@TotalCotizacion", SqlDbType.Decimal).Value = BEParam.TotalCotizacion;
			cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 50).Value = BEParam.Proceso;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario; 
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
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
		 
		public BERetornoTran CotizacionAnular(Int32 pIDCotizacion, String pMotivoAnulacion, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionAnular");
			cmd.Parameters.Add("@IDCotizacion", SqlDbType.Int, 10).Value = pIDCotizacion;
			cmd.Parameters.Add("@MotivoAnulacion", SqlDbType.VarChar, 500).Value = pMotivoAnulacion;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 10).Value = pIDUsuario;
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
		 
		public BERetornoTran CotizacionActualizar(BECotizacion BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionActualizar");
			cmd.Parameters.Add("@IDCotizacion", SqlDbType.Int).Value = BEParam.IDCotizacion;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = BEParam.IDCliente;
			cmd.Parameters.Add("@IDMoneda", SqlDbType.VarChar, 5).Value = BEParam.IDMoneda;
			cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = BEParam.TipoCambio;
			cmd.Parameters.Add("@FechaCotizacion", SqlDbType.DateTime).Value = BEParam.FechaCotizacion;
			cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = BEParam.CalculoIGV;
			cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = BEParam.TotalOperacionGravada;
			cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = BEParam.TotalIGV;
			cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = BEParam.TotalDescuentos;
			cmd.Parameters.Add("@TotalCotizacion", SqlDbType.Decimal).Value = BEParam.TotalCotizacion; 
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			 
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
		 
		public BERetornoTran CotizacionClonar(BECotizacion BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionClonar");
			cmd.Parameters.Add("@IDCotizacion", SqlDbType.Int).Value = BEParam.IDCotizacion; 
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;

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