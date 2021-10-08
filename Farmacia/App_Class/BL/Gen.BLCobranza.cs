using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLCobranza : BLBase
	{
		#region No Transaccional

		public IList CobranzaListar(Int32 pIDVenta)
		{
			SqlCommand cmd = ConexionCmd("gen.CobranzaListar"); 
			cmd.Parameters.Add("@IDVenta", SqlDbType.Int).Value = pIDVenta;
			BECobranza oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECobranza();
					oBE.IDCobranza = rd.GetInt32(rd.GetOrdinal("IDCobranza"));
					oBE.IDMedioPago = rd.GetInt32(rd.GetOrdinal("IDMedioPago"));
					oBE.IDBanco = rd.GetInt32(rd.GetOrdinal("IDBanco"));
					oBE.MedioPago = rd.GetString(rd.GetOrdinal("MedioPago"));
					oBE.Banco = rd.GetString(rd.GetOrdinal("Banco"));
					oBE.CuentaBancaria = rd.GetString(rd.GetOrdinal("CuentaBancaria"));
					oBE.MontoCobrado = rd.GetDecimal(rd.GetOrdinal("MontoCobrado"));
					oBE.FechaCobro = rd.GetDateTime(rd.GetOrdinal("FechaCobro"));
					oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
					oBE.NumeroCobranzaFormato = rd.GetString(rd.GetOrdinal("NumeroCobranzaFormato"));
					oBE.UsuarioCreacion = rd.GetString(rd.GetOrdinal("UsuarioCreacion"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
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

		public IList ReporteCobranzaListar(Int32 pIDSucursal, Int32 pIDMedioPago, Int32 pIDCliente, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("gen.ReporteCobranzaListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@IDMedioPago", SqlDbType.Int).Value = pIDMedioPago;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pIDCliente;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			BECobranza oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECobranza();
					oBE.IDCobranza = rd.GetInt32(rd.GetOrdinal("IDCobranza"));
					oBE.IDMedioPago = rd.GetInt32(rd.GetOrdinal("IDMedioPago"));
					oBE.IDBanco = rd.GetInt32(rd.GetOrdinal("IDBanco"));
					oBE.MedioPago = rd.GetString(rd.GetOrdinal("MedioPago"));
					oBE.Banco = rd.GetString(rd.GetOrdinal("Banco"));
					oBE.CuentaBancaria = rd.GetString(rd.GetOrdinal("CuentaBancaria"));
					oBE.MontoCobrado = rd.GetDecimal(rd.GetOrdinal("MontoCobrado"));
					oBE.FechaCobro = rd.GetDateTime(rd.GetOrdinal("FechaCobro"));
					oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
					oBE.NumeroCobranzaFormato = rd.GetString(rd.GetOrdinal("NumeroCobranzaFormato"));
					oBE.UsuarioCreacion = rd.GetString(rd.GetOrdinal("UsuarioCreacion"));
                    oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
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

		#endregion

		#region Transaccional

		public BERetornoTran CobranzaGuardar(BECobranza BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CobranzaGuardar");
			cmd.Parameters.Add("@IDCobranza", SqlDbType.Int).Value = BEParam.IDCobranza;
			cmd.Parameters.Add("@IDVenta", SqlDbType.Int).Value = BEParam.IDVenta;
			cmd.Parameters.Add("@IDMedioPago", SqlDbType.Int).Value = BEParam.IDMedioPago;
			cmd.Parameters.Add("@IDBanco", SqlDbType.Int).Value = BEParam.IDBanco;
			cmd.Parameters.Add("@MontoCobrado", SqlDbType.Decimal).Value = BEParam.MontoCobrado;
			cmd.Parameters.Add("@CuentaBancaria", SqlDbType.VarChar, 100).Value = BEParam.CuentaBancaria;
			cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 2000).Value = BEParam.Observacion;
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
		 
		public BERetornoTran CobranzaEliminar(Int32 pIDCobranza, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CobranzaEliminar");
			cmd.Parameters.Add("@IDCobranza", SqlDbType.Int).Value = pIDCobranza;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
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
		 
		#endregion
	}
}