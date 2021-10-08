using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.General
{
	public class BLVentaPedido : BLBase
	{
		public IList VentaPedidoListar(Int32 pIDTipoComprobante, Int32 pIDSucursal, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("gen.VentaPedidoListar");
			cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int).Value = pIDTipoComprobante;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			BEVentaPedido oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEVentaPedido();
					oBE.IDPedido = rd.GetInt32(rd.GetOrdinal("IDPedido"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
					oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
					oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
					oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
					oBE.FechaVenta = rd.GetDateTime(rd.GetOrdinal("FechaVenta"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
					oBE.Usuario = rd.GetString(rd.GetOrdinal("Usuario"));
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


		public IList VentaPendienteListar(Int32 pIDSucursal, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("gen.VentaPendienteListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			BEVenta oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEVenta();
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
					oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
					oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
					oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
					oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
					oBE.FechaVenta = rd.GetDateTime(rd.GetOrdinal("FechaVenta"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
					oBE.TotalIGV = rd.GetDecimal(rd.GetOrdinal("TotalIGV"));
					oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
					oBE.IDEstadoAlmacen = rd.GetInt32(rd.GetOrdinal("IDEstadoAlmacen"));
					oBE.Usuario = rd.GetString(rd.GetOrdinal("Usuario"));
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
