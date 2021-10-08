using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL
{
	public class BLReservaDetalleLote : BLBase
	{
		#region NoTransaccional

		public IList ReservaDetalleLoteTempListar(Int32 pIDReservaDetalleTemp)
		{
			SqlCommand cmd = ConexionCmd("gen.ReservaDetalleLoteTempListar");
			cmd.Parameters.Add("@IDReservaDetalleTemp", SqlDbType.Int, 10).Value = pIDReservaDetalleTemp; 
			BEReservaDetalleLote oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEReservaDetalleLote();
					oBE.IDReservaDetalleLoteTemp = rd.GetInt32(rd.GetOrdinal("IDReservaDetalleLoteTemp"));
					oBE.IDReservaDetalleTemp = rd.GetInt32(rd.GetOrdinal("IDReservaDetalleTemp"));
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
		 
		public IList ReservaDetalleLoteListar(Int32 pIDReservaDetalle)
		{
			SqlCommand cmd = ConexionCmd("gen.ReservaDetalleLoteListar");
			cmd.Parameters.Add("@IDReservaDetalle", SqlDbType.Int, 10).Value = pIDReservaDetalle;
			BEReservaDetalleLote oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEReservaDetalleLote();
					oBE.IDReservaDetalleLote = rd.GetInt32(rd.GetOrdinal("IDReservaDetalleLote"));
					oBE.IDReservaDetalle = rd.GetInt32(rd.GetOrdinal("IDReservaDetalle"));
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

		public BERetornoTran ReservaDetalleLoteTempGuardar(BEReservaDetalleLote BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran(); 
			SqlCommand cmd = ConexionCmd("gen.ReservaDetalleLoteTempGuardar");
			 
		    cmd.Parameters.Add("@IDReservaDetalleLoteTemp", SqlDbType.Int).Value = BEParam.IDReservaDetalleLoteTemp;
			cmd.Parameters.Add("@IDReservaDetalleTemp", SqlDbType.Int).Value = BEParam.IDReservaDetalleTemp;
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