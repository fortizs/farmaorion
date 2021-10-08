using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.Inventario
{
	public class BLKardex : BLBase
	{
		public IList KardexBuscar(Int32 pIDSucursal, String pIDProducto, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("gen.KardexBuscar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pIDSucursal;
			cmd.Parameters.Add("@IDProducto", SqlDbType.VarChar, 10).Value = pIDProducto;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			BEMovimientoDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEMovimientoDetalle();
					oBE.IDMovimientoDetalle = rd.GetInt32(rd.GetOrdinal("IDMovimientoDetalle"));
					oBE.FechaMovimiento = rd.GetDateTime(rd.GetOrdinal("FechaMovimiento"));
					oBE.DocumentoReferencia = rd.GetString(rd.GetOrdinal("DocumentoReferencia")); 
					oBE.Transaccion = rd.GetString(rd.GetOrdinal("Transaccion"));
					 
					oBE.EntradaCantidad = rd.GetDecimal(rd.GetOrdinal("EntradaCantidad"));
					oBE.EntradaValorUnidad = rd.GetDecimal(rd.GetOrdinal("EntradaValorUnidad"));
					oBE.EntradaValorTotal = rd.GetDecimal(rd.GetOrdinal("EntradaValorTotal"));

					oBE.SalidaCantidad = rd.GetDecimal(rd.GetOrdinal("SalidaCantidad"));
					oBE.SalidaValorUnidad = rd.GetDecimal(rd.GetOrdinal("SalidaValorUnidad"));
					oBE.SalidaValorTotal = rd.GetDecimal(rd.GetOrdinal("SalidaValorTotal"));

					oBE.SaldoCantidad = rd.GetDecimal(rd.GetOrdinal("SaldoCantidad"));
					oBE.SaldoValorUnidad = rd.GetDecimal(rd.GetOrdinal("SaldoValorUnidad"));
					oBE.SaldoValorTotal = rd.GetDecimal(rd.GetOrdinal("SaldoValorTotal"));

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

		public IList KardexAlmacenListar(Int32 pIDSucursal, Int32 pIDAlmacen, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("inv.KardexAlmacenListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = pIDAlmacen;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			BEKardexDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEKardexDetalle();
					oBE.CodigoProducto = rd.GetString(rd.GetOrdinal("CodigoProducto"));
					oBE.NombreProducto = rd.GetString(rd.GetOrdinal("NombreProducto"));
					oBE.Entrada = rd.GetDecimal(rd.GetOrdinal("Entrada"));
					oBE.Salida = rd.GetDecimal(rd.GetOrdinal("Salida"));
					oBE.Saldo = rd.GetDecimal(rd.GetOrdinal("Saldo"));
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


	}
}