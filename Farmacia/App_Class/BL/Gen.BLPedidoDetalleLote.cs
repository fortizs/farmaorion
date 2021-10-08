using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLPedidoDetalleLote : BLBase
	{
		#region NoTransaccional

		public IList PedidoDetalleLoteTempListar(Int32 pIDPedidoDetalleTemp)
		{
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleLoteTempListar");
			cmd.Parameters.Add("@IDPedidoDetalleTemp", SqlDbType.Int, 10).Value = pIDPedidoDetalleTemp;
			BEPedidoDetalleLote oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEPedidoDetalleLote();
					oBE.IDPedidoDetalleLoteTemp = rd.GetInt32(rd.GetOrdinal("IDPedidoDetalleLoteTemp"));
					oBE.IDPedidoDetalleTemp = rd.GetInt32(rd.GetOrdinal("IDPedidoDetalleTemp"));
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

		public IList PedidoDetalleLoteListar(Int32 pIDPedidoDetalle)
		{
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleLoteListar");
			cmd.Parameters.Add("@IDPedidoDetalle", SqlDbType.Int, 10).Value = pIDPedidoDetalle;
			BEPedidoDetalleLote oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEPedidoDetalleLote();
					oBE.IDPedidoDetalleLote = rd.GetInt32(rd.GetOrdinal("IDPedidoDetalleLote"));
					oBE.IDPedidoDetalle = rd.GetInt32(rd.GetOrdinal("IDPedidoDetalle"));
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

		public BERetornoTran PedidoDetalleLoteTempGuardar(BEPedidoDetalleLote BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleLoteTempGuardar");

			cmd.Parameters.Add("@IDPedidoDetalleLoteTemp", SqlDbType.Int).Value = BEParam.IDPedidoDetalleLoteTemp;
			cmd.Parameters.Add("@IDPedidoDetalleTemp", SqlDbType.Int).Value = BEParam.IDPedidoDetalleTemp;
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