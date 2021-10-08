using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL
{
	public class BLReservaDetalle : BLBase
	{
		#region NoTransaccional
		 
		public IList ReservaDetalleListar(Int32 pIDReserva)
		{
			SqlCommand cmd = ConexionCmd("gen.ReservaDetalleListar");
			cmd.Parameters.Add("@IDReserva", SqlDbType.Int, 10).Value = pIDReserva;
			BEReservaDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEReservaDetalle();
					oBE.IDReservaDetalle = rd.GetInt32(rd.GetOrdinal("IDReservaDetalle"));
					oBE.IDReserva = rd.GetInt32(rd.GetOrdinal("IDReserva"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
					oBE.Item = rd.GetInt32(rd.GetOrdinal("Item"));
					oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
					oBE.ValorUnitario = rd.GetDecimal(rd.GetOrdinal("ValorUnitario"));
					oBE.Igv = rd.GetDecimal(rd.GetOrdinal("Igv"));
					oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
					oBE.Descuento = rd.GetDecimal(rd.GetOrdinal("Descuento"));
					oBE.SubTotal = rd.GetDecimal(rd.GetOrdinal("SubTotal"));
					oBE.IDTipoPrecio = rd.GetString(rd.GetOrdinal("IDTipoPrecio"));
					oBE.IDTipoImpuesto = rd.GetString(rd.GetOrdinal("IDTipoImpuesto"));
					oBE.TipoImpuesto = rd.GetString(rd.GetOrdinal("TipoImpuesto"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
					oBE.UnidadMedidaVenta = rd.GetString(rd.GetOrdinal("UnidadMedidaVenta"));
					oBE.ControlaLote = rd.GetBoolean(rd.GetOrdinal("ControlaLote"));
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
		 
		public IList ReservaDetalleTempListar(Int32 pIDUsuario, String pToken)
		{
			SqlCommand cmd = ConexionCmd("gen.ReservaDetalleTempListar");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 10).Value = pIDUsuario;
			cmd.Parameters.Add("@Token", SqlDbType.VarChar).Value = pToken;
			BEReservaDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEReservaDetalle();
					oBE.IDReservaDetalleTemp = rd.GetInt32(rd.GetOrdinal("IDReservaDetalleTemp"));
					oBE.IDUsuario = rd.GetInt32(rd.GetOrdinal("IDUsuario"));
					oBE.Token = rd.GetString(rd.GetOrdinal("Token"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDUnidadMedidaVenta = rd.GetInt32(rd.GetOrdinal("IDUnidadMedidaVenta"));
					oBE.Item = rd.GetInt32(rd.GetOrdinal("Item"));
					oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
					oBE.ValorUnitario = rd.GetDecimal(rd.GetOrdinal("ValorUnitario"));
					oBE.Igv = rd.GetDecimal(rd.GetOrdinal("Igv"));
					oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
					oBE.Descuento = rd.GetDecimal(rd.GetOrdinal("Descuento"));
					oBE.SubTotal = rd.GetDecimal(rd.GetOrdinal("SubTotal"));
					oBE.IDTipoPrecio = rd.GetString(rd.GetOrdinal("IDTipoPrecio"));
					oBE.IDTipoImpuesto = rd.GetString(rd.GetOrdinal("IDTipoImpuesto"));
					oBE.EsVentaEspera = rd.GetBoolean(rd.GetOrdinal("EsVentaEspera"));
					oBE.IDUsuarioCreacion = rd.GetInt32(rd.GetOrdinal("IDUsuarioCreacion"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.TipoImpuesto = rd.GetString(rd.GetOrdinal("TipoImpuesto"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
					oBE.UnidadMedidaVenta = rd.GetString(rd.GetOrdinal("UnidadMedidaVenta"));
					oBE.ControlaLote = rd.GetBoolean(rd.GetOrdinal("ControlaLote"));

					
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

		public BERetornoTran ReservaDetalleTempGuardar(BEReservaDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ReservaDetalleTempGuardar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = BEParam.Token;
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
			cmd.Parameters.Add("@IDUnidadMedidaVenta", SqlDbType.Int).Value = BEParam.IDUnidadMedidaVenta;
			cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = BEParam.Cantidad;
			cmd.Parameters.Add("@ValorUnitario", SqlDbType.Decimal).Value = BEParam.ValorUnitario;
			cmd.Parameters.Add("@Igv", SqlDbType.Decimal).Value = BEParam.Igv;
			cmd.Parameters.Add("@PrecioVenta", SqlDbType.Decimal).Value = BEParam.PrecioVenta;
			cmd.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = BEParam.Descuento;
			cmd.Parameters.Add("@IDTipoPrecio", SqlDbType.VarChar, 2).Value = BEParam.IDTipoPrecio;
			cmd.Parameters.Add("@IDTipoImpuesto", SqlDbType.VarChar, 2).Value = BEParam.IDTipoImpuesto;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
			cmd.Parameters.Add("@IDReservaDetalleTempFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDReservaDetalleTempFinal"].Value);
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
		 
		public BERetornoTran ReservaDetalleTempLoteGuardar(BEReservaDetalle BEParam, ArrayList pReservaDetalleLote)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEReserva BEReservaRetorno = new BEReserva(); 
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction(); 
			SqlCommand cmd = new SqlCommand("gen.ReservaDetalleTempGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			Int32 pIDReservaDetalleTempFinal = 0;
			try
			{
				 
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
				cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = BEParam.Token;
				cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
				cmd.Parameters.Add("@IDUnidadMedidaVenta", SqlDbType.Int).Value = BEParam.IDUnidadMedidaVenta; 
				cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = BEParam.Cantidad;
				cmd.Parameters.Add("@ValorUnitario", SqlDbType.Decimal).Value = BEParam.ValorUnitario;
				cmd.Parameters.Add("@Igv", SqlDbType.Decimal).Value = BEParam.Igv;
				cmd.Parameters.Add("@PrecioVenta", SqlDbType.Decimal).Value = BEParam.PrecioVenta;
				cmd.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = BEParam.Descuento;
				cmd.Parameters.Add("@IDTipoPrecio", SqlDbType.VarChar, 2).Value = BEParam.IDTipoPrecio;
				cmd.Parameters.Add("@IDTipoImpuesto", SqlDbType.VarChar, 2).Value = BEParam.IDTipoImpuesto;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
				cmd.Parameters.Add("@IDReservaDetalleTempFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value); 
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}
				else
				{
                    pIDReservaDetalleTempFinal = Int32.Parse(cmd.Parameters["@IDReservaDetalleTempFinal"].Value.ToString());
				}

				//VENTA DETALLE LOTE TEMP-----------------------------------------------------------------------------       
				     
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.ReservaDetalleLoteTempGuardar";

                BEReservaDetalleLote oBEDetLote = new BEReservaDetalleLote();
				for (Int32 i = 0; i < pReservaDetalleLote.Count; i++)
				{
					oBEDetLote = (BEReservaDetalleLote)pReservaDetalleLote[i];
					cmd.Parameters.Add("@IDReservaDetalleLoteTemp", SqlDbType.Int).Value = oBEDetLote.IDReservaDetalleLoteTemp;
					cmd.Parameters.Add("@IDReservaDetalleTemp", SqlDbType.Int).Value = pIDReservaDetalleTempFinal;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = oBEDetLote.IDProducto;
					cmd.Parameters.Add("@IDLote", SqlDbType.Int).Value = oBEDetLote.IDLote;
					cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = oBEDetLote.IDSucursal;
					cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = oBEDetLote.CantidadLote;
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBEDetLote.IDUsuario;
					cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
					cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;

					cmd.ExecuteNonQuery();
					BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
					BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);

					if (BERetorno.Retorno == "-1")
					{
						throw new Exception(BERetorno.ErrorMensaje);
					}
					cmd.Parameters.Clear();
				}


				sqlTran.Commit();
			}
			catch (Exception ex)
			{
				sqlTran.Rollback();
				if (BERetorno.Retorno == "-1")
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

		public BERetornoTran ReservaDetalleTempEliminar(Int32 pIDReservaDetalleTemp)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ReservaDetalleTempEliminar");
			cmd.Parameters.Add("@IDReservaDetalleTemp", SqlDbType.Int).Value = pIDReservaDetalleTemp; 
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

		public BERetornoTran ReservaDetalleTempActualizar(BEReservaDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ReservaDetalleTempActualizar");
			cmd.Parameters.Add("@IDReservaDetalleTemp", SqlDbType.Int).Value = BEParam.IDReservaDetalleTemp;
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

		public BERetornoTran VentaDetalleTempNotaCreditoGuardar(Int32 pIDVenta, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleTempNotaCreditoGuardar");
			cmd.Parameters.Add("@IDVenta", SqlDbType.Int).Value = pIDVenta; 
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