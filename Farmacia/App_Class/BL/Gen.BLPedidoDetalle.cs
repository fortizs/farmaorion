using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLPedidoDetalle : BLBase
	{
		#region NoTransaccional

		public IList PedidoDetalleListar(Int32 pIDPedido)
		{
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleListar");
			cmd.Parameters.Add("@IDPedido", SqlDbType.Int, 10).Value = pIDPedido;
			BEPedidoDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEPedidoDetalle();
					oBE.IDPedidoDetalle = rd.GetInt32(rd.GetOrdinal("IDPedidoDetalle"));
					oBE.IDPedido = rd.GetInt32(rd.GetOrdinal("IDPedido"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
					oBE.Item = rd.GetInt32(rd.GetOrdinal("Item"));
					oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
					oBE.ValorUnitario = rd.GetDecimal(rd.GetOrdinal("ValorUnitario"));
					oBE.Igv = rd.GetDecimal(rd.GetOrdinal("Igv"));
					oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
					oBE.PorcentajeDescuento = rd.GetDecimal(rd.GetOrdinal("PorcentajeDescuento"));
					oBE.Descuento = rd.GetDecimal(rd.GetOrdinal("Descuento"));
					oBE.SubTotal = rd.GetDecimal(rd.GetOrdinal("SubTotal"));
					oBE.ImporteTotal = rd.GetDecimal(rd.GetOrdinal("ImporteTotal"));
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

		public IList PedidoDetalleTempListar(Int32 pIDUsuario, String pProceso)
		{
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleTempListar");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 10).Value = pIDUsuario;
			cmd.Parameters.Add("@Proceso", SqlDbType.VarChar).Value = pProceso;
			BEPedidoDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEPedidoDetalle();
					oBE.IDPedidoDetalleTemp = rd.GetInt32(rd.GetOrdinal("IDPedidoDetalleTemp"));
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
					oBE.ImporteTotal = rd.GetDecimal(rd.GetOrdinal("ImporteTotal"));
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

					oBE.PorcentajeDescuento = rd.GetDecimal(rd.GetOrdinal("PorcentajeDescuento"));

					oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
					oBE.PrincipioActivo = rd.GetString(rd.GetOrdinal("PrincipioActivo"));
					oBE.VentaConReceta = rd.GetBoolean(rd.GetOrdinal("VentaConReceta"));
					oBE.PrecioVentaSinDescuento = rd.GetDecimal(rd.GetOrdinal("PrecioVentaSinDescuento"));



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

		public BERetornoTran PedidoDetalleTempGuardar(BEPedidoDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleTempGuardar");
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
			cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 100).Value = BEParam.Proceso;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
			cmd.Parameters.Add("@IDPedidoDetalleTempFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDPedidoDetalleTempFinal"].Value);
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

		public BERetornoTran PedidoDetalleTempLoteGuardar(BEPedidoDetalle BEParam, ArrayList pPedidoDetalleLote)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEPedido BEPedidoRetorno = new BEPedido();
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();
			SqlCommand cmd = new SqlCommand("gen.PedidoDetalleTempGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			Int32 pIDPedidoDetalleTempFinal = 0;
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
				cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 20).Value = BEParam.Proceso;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
				cmd.Parameters.Add("@IDPedidoDetalleTempFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
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
					pIDPedidoDetalleTempFinal = Int32.Parse(cmd.Parameters["@IDPedidoDetalleTempFinal"].Value.ToString());
				}

				//Pedido DETALLE LOTE TEMP-----------------------------------------------------------------------------       

				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.PedidoDetalleLoteTempGuardar";

				BEPedidoDetalleLote oBEDetLote = new BEPedidoDetalleLote();
				for (Int32 i = 0; i < pPedidoDetalleLote.Count; i++)
				{
					oBEDetLote = (BEPedidoDetalleLote)pPedidoDetalleLote[i];
					cmd.Parameters.Add("@IDPedidoDetalleLoteTemp", SqlDbType.Int).Value = oBEDetLote.IDPedidoDetalleLoteTemp;
					cmd.Parameters.Add("@IDPedidoDetalleTemp", SqlDbType.Int).Value = pIDPedidoDetalleTempFinal;
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

		public BERetornoTran PedidoDetalleTempEliminar(Int32 pIDPedidoDetalleTemp)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleTempEliminar");
			cmd.Parameters.Add("@IDPedidoDetalleTemp", SqlDbType.Int).Value = pIDPedidoDetalleTemp;
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

		public BERetornoTran PedidoDetalleTempActualizar(BEPedidoDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleTempActualizar");
			cmd.Parameters.Add("@IDPedidoDetalleTemp", SqlDbType.Int).Value = BEParam.IDPedidoDetalleTemp;
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

		public BERetornoTran PedidoDetalleActualizar(BEPedidoDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleActualizar");
			cmd.Parameters.Add("@IDPedidoDetalle", SqlDbType.Int).Value = BEParam.IDPedidoDetalle;
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
		  
		public BERetornoTran PedidoDetalleTempDescuentoItem(BEPedidoDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleTempDescuentoItem");
			cmd.Parameters.Add("@IDPedidoDetalleTemp", SqlDbType.Int).Value = BEParam.IDPedidoDetalleTemp;
			cmd.Parameters.Add("@PorcentajeDescuento", SqlDbType.Decimal).Value = BEParam.PorcentajeDescuento;
			cmd.Parameters.Add("@PrecioConDescuento", SqlDbType.Decimal).Value = BEParam.PrecioConDescuento;
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

		public BERetornoTran PedidoDetalleTempDescuentoGeneral(BEPedidoDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleTempDescuentoGeneral");
			cmd.Parameters.Add("@PorcentajeDescuento", SqlDbType.Decimal).Value = BEParam.PorcentajeDescuento;
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

		public BERetornoTran PedidoDetalleTempNotaCreditoGuardar(Int32 pIDPedido, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleTempNotaCreditoGuardar");
			cmd.Parameters.Add("@IDPedido", SqlDbType.Int).Value = pIDPedido;
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

		public BERetornoTran PedidoDetalleTempxUsuarioEliminar(Int32 pIDUsuario, String pProceso)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleTempxUsuarioEliminar");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 100).Value = pProceso;
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
		 
		public BERetornoTran PedidoDetalleEliminar(Int32 pIDPedidoDetalle)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleEliminar");
			cmd.Parameters.Add("@IDPedidoDetalle", SqlDbType.Int).Value = pIDPedidoDetalle;
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
		  
		public BERetornoTran PedidoDetalleGuardar(BEPedidoDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoDetalleGuardar");
			cmd.Parameters.Add("@IDPedido", SqlDbType.Int).Value = BEParam.IDPedido;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal; 
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
			cmd.Parameters.Add("@IDPedidoDetalleFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDPedidoDetalleFinal"].Value);
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