using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL
{
	public class BLVentaDetalle : BLBase
	{
		#region NoTransaccional
		 
		public IList VentaDetalleListar(Int32 pIDVenta)
		{
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleListar");
			cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 10).Value = pIDVenta;
			BEVentaDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEVentaDetalle();
					oBE.IDVentaDetalle = rd.GetInt32(rd.GetOrdinal("IDVentaDetalle"));
					oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
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

					oBE.Carroceria = rd.GetString(rd.GetOrdinal("Carroceria"));
					oBE.Chasis = rd.GetString(rd.GetOrdinal("Chasis"));
					oBE.NumeroLote = rd.GetString(rd.GetOrdinal("NumeroLote"));
					oBE.Motor = rd.GetString(rd.GetOrdinal("Motor"));
					oBE.NumeroDUA = rd.GetString(rd.GetOrdinal("NumeroDUA"));
					oBE.FechaDUA = rd.GetDateTime(rd.GetOrdinal("FechaDUA"));
					oBE.AnioModelo = rd.GetInt32(rd.GetOrdinal("AnioModelo"));
					oBE.TipoCombustible = rd.GetString(rd.GetOrdinal("TipoCombustible"));
					oBE.Color = rd.GetString(rd.GetOrdinal("Color"));
					oBE.Modelo = rd.GetString(rd.GetOrdinal("Modelo"));
					oBE.MarcaVehiculo = rd.GetString(rd.GetOrdinal("MarcaVehiculo"));
					oBE.ModeloVersion = rd.GetString(rd.GetOrdinal("ModeloVersion"));

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
		 
		public IList VentaDetalleTempListar(Int32 pIDUsuario, String pProceso)
		{
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleTempListar");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 10).Value = pIDUsuario;
			cmd.Parameters.Add("@Proceso", SqlDbType.VarChar).Value = pProceso;
			BEVentaDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEVentaDetalle();
					oBE.IDVentaDetalleTemp = rd.GetInt32(rd.GetOrdinal("IDVentaDetalleTemp"));
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

					//oBE.Carroceria = rd.GetString(rd.GetOrdinal("Carroceria"));
					//oBE.Chasis = rd.GetString(rd.GetOrdinal("Chasis"));
					//oBE.NumeroLote = rd.GetString(rd.GetOrdinal("NumeroLote"));
					//oBE.Motor = rd.GetString(rd.GetOrdinal("Motor"));
					//oBE.NumeroDUA = rd.GetString(rd.GetOrdinal("NumeroDUA"));
					//oBE.FechaDUA = rd.GetDateTime(rd.GetOrdinal("FechaDUA"));
					//oBE.AnioModelo = rd.GetInt32(rd.GetOrdinal("AnioModelo"));
					//oBE.TipoCombustible = rd.GetString(rd.GetOrdinal("TipoCombustible"));
					//oBE.Color = rd.GetString(rd.GetOrdinal("Color"));
					//oBE.Modelo = rd.GetString(rd.GetOrdinal("Modelo"));
					//oBE.MarcaVehiculo = rd.GetString(rd.GetOrdinal("MarcaVehiculo"));
					//oBE.ModeloVersion = rd.GetString(rd.GetOrdinal("ModeloVersion"));



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

		public BERetornoTran VentaDetalleTempGuardar(BEVentaDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleTempGuardar");
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
			cmd.Parameters.Add("@IDVentaDetalleTempFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDVentaDetalleTempFinal"].Value);
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
		 
		public BERetornoTran VentaDetalleTempLoteGuardar(BEVentaDetalle BEParam, ArrayList pVentaDetalleLote)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEVenta BEVentaRetorno = new BEVenta(); 
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction(); 
			SqlCommand cmd = new SqlCommand("gen.VentaDetalleTempGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			Int32 pIDVentaDetalleTempFinal = 0;
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
				cmd.Parameters.Add("@IDVentaDetalleTempFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
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
					pIDVentaDetalleTempFinal = Int32.Parse(cmd.Parameters["@IDVentaDetalleTempFinal"].Value.ToString());
				}

				//VENTA DETALLE LOTE TEMP-----------------------------------------------------------------------------       
				     
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaDetalleLoteTempGuardar";

				BEVentaDetalleLote oBEDetLote = new BEVentaDetalleLote();
				for (Int32 i = 0; i < pVentaDetalleLote.Count; i++)
				{
					oBEDetLote = (BEVentaDetalleLote)pVentaDetalleLote[i];
					cmd.Parameters.Add("@IDVentaDetalleLoteTemp", SqlDbType.Int).Value = oBEDetLote.IDVentaDetalleLoteTemp;
					cmd.Parameters.Add("@IDVentaDetalleTemp", SqlDbType.Int).Value = pIDVentaDetalleTempFinal;
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

		public BERetornoTran VentaDetalleTempEliminar(Int32 pIDVentaDetalleTemp)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleTempEliminar");
			cmd.Parameters.Add("@IDVentaDetalleTemp", SqlDbType.Int).Value = pIDVentaDetalleTemp; 
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

		public BERetornoTran VentaDetalleTempActualizar(BEVentaDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleTempActualizar");
			cmd.Parameters.Add("@IDVentaDetalleTemp", SqlDbType.Int).Value = BEParam.IDVentaDetalleTemp;
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

		public BERetornoTran VentaDetalleTempDescuentoItem(BEVentaDetalle BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleTempDescuentoItem");
			cmd.Parameters.Add("@IDVentaDetalleTemp", SqlDbType.Int).Value = BEParam.IDVentaDetalleTemp;
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

		public BERetornoTran VentaDetalleTempDescuentoGeneral(BEVentaDetalle BEParam)
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
		 
		public BERetornoTran VentaDetalleTempxUsuarioEliminar(Int32 pIDUsuario, String pProceso)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaDetalleTempxUsuarioEliminar");
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
		 
		#endregion

	}
}