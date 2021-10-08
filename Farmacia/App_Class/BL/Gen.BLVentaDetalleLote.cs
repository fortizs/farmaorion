using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL
{
	public class BLVentaDetalleLote : BLBase
	{
		#region NoTransaccional

		public IList VentaDetalleLoteTempListar(Int32 pIDVentaDetalleTemp)
		{
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleLoteTempListar");
			cmd.Parameters.Add("@IDVentaDetalleTemp", SqlDbType.Int, 10).Value = pIDVentaDetalleTemp; 
			BEVentaDetalleLote oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEVentaDetalleLote();
					oBE.IDVentaDetalleLoteTemp = rd.GetInt32(rd.GetOrdinal("IDVentaDetalleLoteTemp"));
					oBE.IDVentaDetalleTemp = rd.GetInt32(rd.GetOrdinal("IDVentaDetalleTemp"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDLote = rd.GetInt32(rd.GetOrdinal("IDLote"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
					oBE.IDUsuarioCreacion = rd.GetInt32(rd.GetOrdinal("IDUsuarioCreacion"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.Lote = rd.GetString(rd.GetOrdinal("Lote"));
					oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
					oBE.FechaFabricacion = rd.GetDateTime(rd.GetOrdinal("FechaFabricacion"));
					oBE.StockActualLote = rd.GetDecimal(rd.GetOrdinal("StockActualLote"));

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
		 
		public IList VentaDetalleLoteListar(Int32 pIDVentaDetalle)
		{
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleLoteListar");
			cmd.Parameters.Add("@IDVentaDetalle", SqlDbType.Int, 10).Value = pIDVentaDetalle;
			BEVentaDetalleLote oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEVentaDetalleLote();
					oBE.IDVentaDetalleLote = rd.GetInt32(rd.GetOrdinal("IDVentaDetalleLote"));
					oBE.IDVentaDetalle = rd.GetInt32(rd.GetOrdinal("IDVentaDetalle"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDLote = rd.GetInt32(rd.GetOrdinal("IDLote"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
					oBE.IDUsuarioCreacion = rd.GetInt32(rd.GetOrdinal("IDUsuarioCreacion"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.Lote = rd.GetString(rd.GetOrdinal("Lote"));
					oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
					oBE.FechaFabricacion = rd.GetDateTime(rd.GetOrdinal("FechaFabricacion"));
					oBE.StockActualLote = rd.GetDecimal(rd.GetOrdinal("StockActualLote"));
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

		public BERetornoTran VentaDetalleLoteTempGuardar(BEVentaDetalleLote BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran(); 
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleLoteTempGuardar");
			 
		    cmd.Parameters.Add("@IDVentaDetalleLoteTemp", SqlDbType.Int).Value = BEParam.IDVentaDetalleLoteTemp;
			cmd.Parameters.Add("@IDVentaDetalleTemp", SqlDbType.Int).Value = BEParam.IDVentaDetalleTemp;
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
			cmd.Parameters.Add("@IDLote", SqlDbType.Int).Value = BEParam.IDLote;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = BEParam.Cantidad;
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
		 
		#endregion

	}
}