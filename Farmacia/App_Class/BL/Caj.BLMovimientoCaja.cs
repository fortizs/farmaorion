using Farmacia.App_Class.BE.Caja;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Caja
{
	public class BLMovimientoCaja : BLBase
	{
		#region Transaccional

		public BERetornoTran MovimientoCajaGuardar(BEMovimientoCaja BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("caj.MovimientoCajaGuardar");
			cmd.Parameters.Add("@IDMovimientoCaja", SqlDbType.Int).Value = BEParam.IDMovimientoCaja; 
			cmd.Parameters.Add("@IDOperacion", SqlDbType.Int).Value = BEParam.IDOperacion;
			cmd.Parameters.Add("@FechaMovimiento", SqlDbType.DateTime).Value = BEParam.FechaMovimiento;
			cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int).Value = BEParam.IDTipoComprobante == 0 ? (Object)DBNull.Value: BEParam.IDTipoComprobante;
			cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 4).Value = BEParam.Serie;
			cmd.Parameters.Add("@Numero", SqlDbType.VarChar, 10).Value = BEParam.Numero;
			cmd.Parameters.Add("@IDMoneda", SqlDbType.VarChar, 4).Value = BEParam.IDMoneda;
			cmd.Parameters.Add("@Monto", SqlDbType.Decimal).Value = BEParam.Monto;
			cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 2000).Value = BEParam.Observacion;
			cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = BEParam.IDFormaPago;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
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
		 
		public BERetornoTran MovimientoCajaEliminar(Int32 pIDMovimientoCaja)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("caj.MovimientoCajaEliminar"); 
			cmd.Parameters.Add("@IDMovimientoCaja", SqlDbType.Int).Value = pIDMovimientoCaja;
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

		#region NoTransaccional

		public IList MovimientoCajaListar(Int32 pIDCaja, String pFiltro, String pTipoMovimiento, Int32 pIDFormaPago, String pFechaInicio, String pFechaFin, Int32 pIDSucursal, Int32 pIDUsuario)
		{
			SqlCommand cmd = ConexionCmd("caj.MovimientoCajaListar");
			cmd.Parameters.Add("@IDCaja", SqlDbType.Int).Value = pIDCaja;
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
			cmd.Parameters.Add("@TipoMovimiento", SqlDbType.Char, 1).Value = pTipoMovimiento;
			cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = pIDFormaPago;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			 
			BEMovimientoCaja oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEMovimientoCaja();
					oBE.IDMovimientoCaja = rd.GetInt32(rd.GetOrdinal("IDMovimientoCaja"));
					oBE.CajaMecanica = rd.GetString(rd.GetOrdinal("CajaMecanica"));
					oBE.NombreTipoMovimiento = rd.GetString(rd.GetOrdinal("NombreTipoMovimiento"));
					oBE.Operacion = rd.GetString(rd.GetOrdinal("Operacion"));
					oBE.FechaMovimiento = rd.GetDateTime(rd.GetOrdinal("FechaMovimiento"));
					oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
					oBE.SiglaTipoComprobante = rd.GetString(rd.GetOrdinal("SiglaTipoComprobante"));
					oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
					oBE.MedioPago = rd.GetString(rd.GetOrdinal("MedioPago"));
					oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda"));
					oBE.Monto = rd.GetDecimal(rd.GetOrdinal("Monto"));
					oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
					oBE.UsuarioCreacion = rd.GetString(rd.GetOrdinal("UsuarioCreacion"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.TipoMovimiento = rd.GetString(rd.GetOrdinal("TipoMovimiento"));
					oBE.IDOperacion = rd.GetInt32(rd.GetOrdinal("IDOperacion"));
					 
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

		public BEMovimientoCaja MovimientoCajaSeleccionar(Int32 pIDMovimientoCaja)
		{
			SqlCommand cmd = ConexionCmd("caj.MovimientoCajaSeleccionar");
			BEMovimientoCaja oBE = new BEMovimientoCaja();
			cmd.Parameters.Add("@IDMovimientoCaja", SqlDbType.Int).Value = pIDMovimientoCaja;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDMovimientoCaja = rd.GetInt32(rd.GetOrdinal("IDMovimientoCaja"));
					oBE.IDCaja = rd.GetInt32(rd.GetOrdinal("IDCaja"));
					oBE.IDOperacion = rd.GetInt32(rd.GetOrdinal("IDOperacion"));
					oBE.TipoMovimiento = rd.GetString(rd.GetOrdinal("TipoMovimiento"));
					oBE.FechaMovimiento = rd.GetDateTime(rd.GetOrdinal("FechaMovimiento"));
					oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("IDTipoComprobante"));
					oBE.Serie = rd.GetString(rd.GetOrdinal("Serie"));
					oBE.Numero = rd.GetString(rd.GetOrdinal("Numero"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.Monto = rd.GetDecimal(rd.GetOrdinal("Monto"));
					oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
					oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
					oBE.CajaMecanica = rd.GetString(rd.GetOrdinal("CajaMecanica"));
					oBE.Cajero = rd.GetString(rd.GetOrdinal("Cajero"));
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
		 
		public BEMovimientoCaja MovimientoCajaTotalListar(Int32 pIDCaja, String pFiltro, String pTipoMovimiento, Int32 pIDFormaPago, String pFechaInicio, String pFechaFin, Int32 pIDSucursal, Int32 pIDUsuario)
		{
			SqlCommand cmd = ConexionCmd("caj.MovimientoCajaTotalListar");
			cmd.Parameters.Add("@IDCaja", SqlDbType.Int).Value = pIDCaja;
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
			cmd.Parameters.Add("@TipoMovimiento", SqlDbType.Char, 1).Value = pTipoMovimiento;
			cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = pIDFormaPago;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			BEMovimientoCaja oBE = new BEMovimientoCaja();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.TotalIngreso = rd.GetDecimal(rd.GetOrdinal("TotalIngreso"));
					oBE.TotalSalida = rd.GetDecimal(rd.GetOrdinal("TotalSalida"));
					oBE.TotalSaldo = rd.GetDecimal(rd.GetOrdinal("TotalSaldo"));
					 
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

		#endregion

	}
}