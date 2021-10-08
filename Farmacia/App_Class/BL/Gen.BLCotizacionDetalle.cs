using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLCotizacionDetalle : BLBase
	{
		#region NoTransaccional

		public IList CotizacionDetalleListar(Int32 pIDCotizacion)
		{
			SqlCommand cmd = ConexionCmd("gen.CotizacionDetalleListar");
			cmd.Parameters.Add("@IDCotizacion", SqlDbType.Int, 10).Value = pIDCotizacion;
			BECotizacionDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECotizacionDetalle();
					oBE.IDCotizacionDetalle = rd.GetInt32(rd.GetOrdinal("IDCotizacionDetalle"));
					oBE.IDCotizacion = rd.GetInt32(rd.GetOrdinal("IDCotizacion"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
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
					oBE.TipoImpuesto = rd.GetString(rd.GetOrdinal("TipoImpuesto"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
					oBE.UnidadMedidaVenta = rd.GetString(rd.GetOrdinal("UnidadMedidaVenta"));
					oBE.ControlaLote = rd.GetBoolean(rd.GetOrdinal("ControlaLote"));
					oBE.PorcentajeDescuento = rd.GetDecimal(rd.GetOrdinal("PorcentajeDescuento"));
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


		public IList CotizacionDetalleTempListar(Int32 pIDUsuario, String pProceso)
		{
			SqlCommand cmd = ConexionCmd("gen.CotizacionDetalleTempListar");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 10).Value = pIDUsuario;
			cmd.Parameters.Add("@Proceso", SqlDbType.VarChar).Value = pProceso;
			BECotizacionDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECotizacionDetalle();
					oBE.IDCotizacionDetalleTemp = rd.GetInt32(rd.GetOrdinal("IDCotizacionDetalleTemp"));
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
		public BERetornoTran CotizacionDetalleTempGuardar(BECotizacionDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionDetalleTempGuardar");
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
			cmd.Parameters.Add("@IDCotizacionDetalleTempFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDCotizacionDetalleTempFinal"].Value);
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
		 
		public BERetornoTran CotizacionDetalleTempxUsuarioEliminar(Int32 pIDUsuario, String pProceso)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionDetalleTempxUsuarioEliminar");
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
		 
		public BERetornoTran CotizacionDetalleTempActualizar(BECotizacionDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionDetalleTempActualizar");
			cmd.Parameters.Add("@IDCotizacionDetalleTemp", SqlDbType.Int).Value = BEParam.IDCotizacionDetalleTemp;
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
		 
		public BERetornoTran CotizacionDetalleTempEliminar(Int32 pIDCotizacionDetalleTemp)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionDetalleTempEliminar");
			cmd.Parameters.Add("@IDCotizacionDetalleTemp", SqlDbType.Int).Value = pIDCotizacionDetalleTemp;
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
		 
		public BERetornoTran CotizacionDetalleTempDescuentoItem(BECotizacionDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionDetalleTempDescuentoItem");
			cmd.Parameters.Add("@IDCotizacionDetalleTemp", SqlDbType.Int).Value = BEParam.IDCotizacionDetalleTemp;
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

		public BERetornoTran VentaDetalleTempDescuentoGeneral2(BEVentaDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleTempDescuentoGeneral");
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
		 
		public BERetornoTran CotizacionDetalleActualizar(BECotizacionDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionDetalleActualizar");
			cmd.Parameters.Add("@IDCotizacionDetalle", SqlDbType.Int).Value = BEParam.IDCotizacionDetalle;
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

		public BERetornoTran CotizacionDetalleGuardar(BECotizacionDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionDetalleGuardar");
			cmd.Parameters.Add("@IDCotizacion", SqlDbType.Int).Value = BEParam.IDCotizacion;
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
		 
		public BERetornoTran CotizacionDetalleEliminar(Int32 pIDCotizacionDetalle)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CotizacionDetalleEliminar");
			cmd.Parameters.Add("@IDCotizacionDetalle", SqlDbType.Int).Value = pIDCotizacionDetalle;
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