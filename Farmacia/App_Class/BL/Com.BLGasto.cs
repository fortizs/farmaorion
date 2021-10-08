using Farmacia.App_Class.BE.Compras;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.Compras
{
	public class BLGasto : BLBase
	{
		#region Transaccional

		public BERetornoTran GuardarGasto(BEGasto BEParam, ArrayList pVentaDetalle)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEGasto BEVentaRetorno = new BEGasto();
			int vSucursal = BEParam.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("com.GastoGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{ 
				cmd.Parameters.Add("@IDGasto", SqlDbType.Int).Value = BEParam.IDGasto;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
				cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = BEParam.IDProveedor;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 5).Value = BEParam.IDMoneda;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = BEParam.IDTipoDocumento;
				cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = BEParam.IDFormaPago;
				cmd.Parameters.Add("@TipoCompra", SqlDbType.VarChar, 1).Value = BEParam.TipoCompra;
				cmd.Parameters.Add("@CuentaCaja", SqlDbType.VarChar, 20).Value = BEParam.CuentaCaja;
				cmd.Parameters.Add("@Cuenta", SqlDbType.VarChar, 20).Value = BEParam.Cuenta;
				cmd.Parameters.Add("@Glosa", SqlDbType.VarChar, 200).Value = BEParam.Glosa;
				cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 4).Value = BEParam.Serie;
				cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 10).Value = BEParam.NumeroDocumento;
				cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = BEParam.FechaRegistro;
				cmd.Parameters.Add("@FechaCompra", SqlDbType.DateTime).Value = BEParam.FechaCompra;
				cmd.Parameters.Add("@FechaVencimiento", SqlDbType.DateTime).Value = BEParam.FechaVencimiento;
				cmd.Parameters.Add("@TotalCompra", SqlDbType.Decimal).Value = BEParam.TotalCompra;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = BEParam.TotalIGV;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
				cmd.Parameters.Add("@IDGastoFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDGastoFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);
				}
				else
				{
					BEVentaRetorno.IDGasto = Int32.Parse(cmd.Parameters["@IDGastoFinal"].Value.ToString());
				}

				//VENTA DETALLE-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "com.GastoDetalleGuardar";

				BEGastoDetalle oBEVentDetalle = new BEGastoDetalle();
				for (Int32 i = 0; i < pVentaDetalle.Count; i++)
				{
					oBEVentDetalle = (BEGastoDetalle)pVentaDetalle[i];
					cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
					cmd.Parameters.Add("@IDGastoDetalle", SqlDbType.Int).Value = oBEVentDetalle.IDGastoDetalle;
					cmd.Parameters.Add("@IDGasto", SqlDbType.Int).Value = BEVentaRetorno.IDGasto;
					cmd.Parameters.Add("@DetalleProducto", SqlDbType.VarChar, 500).Value = oBEVentDetalle.DetalleProducto;
					cmd.Parameters.Add("@Item", SqlDbType.Int).Value = oBEVentDetalle.Item;
					cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int).Value = oBEVentDetalle.IDUnidadMedida;
					cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = oBEVentDetalle.Cantidad;
					cmd.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitario;
					cmd.Parameters.Add("@AplicaIgv", SqlDbType.Bit).Value = oBEVentDetalle.AplicaIgv;
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBEVentDetalle.IDUsuario;
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

		public BERetornoTran GastoActualizar(BEGasto pVenta, ArrayList pVentaDetalle)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEGasto BEVentaRetorno = new BEGasto();
			int vSucursal = pVenta.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("com.GastoActualizar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{
				cmd.Parameters.Add("@IDGasto", SqlDbType.Int, 10).Value = pVenta.IDGasto;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pVenta.IDSucursal;
				cmd.Parameters.Add("@IDProveedor", SqlDbType.Int, 10).Value = pVenta.IDProveedor;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pVenta.IDMoneda;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pVenta.IDTipoDocumento;
				cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int, 10).Value = pVenta.IDFormaPago;
				cmd.Parameters.Add("@TipoCompra", SqlDbType.VarChar, 1).Value = pVenta.TipoCompra;
				cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 20).Value = pVenta.Serie;
				cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 20).Value = pVenta.NumeroDocumento;

				cmd.Parameters.Add("@Cuenta", SqlDbType.VarChar, 20).Value = pVenta.Cuenta;
				cmd.Parameters.Add("@CuentaCaja", SqlDbType.VarChar, 20).Value = pVenta.CuentaCaja;
				cmd.Parameters.Add("@Glosa", SqlDbType.VarChar, 200).Value = pVenta.Glosa;

				cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = pVenta.FechaRegistro;
				cmd.Parameters.Add("@FechaCompra", SqlDbType.DateTime).Value = pVenta.FechaCompra;
				cmd.Parameters.Add("@FechaVencimiento", SqlDbType.DateTime).Value = pVenta.FechaVencimiento;
				cmd.Parameters.Add("@TotalCompra", SqlDbType.Decimal).Value = pVenta.TotalCompra;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pVenta.TotalIGV;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pVenta.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);
				}

				//VENTA DETALLE-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "com.GastoDetalleGuardar";

				BEGastoDetalle oBEVentDetalle = new BEGastoDetalle();
				for (Int32 i = 0; i < pVentaDetalle.Count; i++)
				{
					oBEVentDetalle = (BEGastoDetalle)pVentaDetalle[i];
					cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = vSucursal;
					cmd.Parameters.Add("@IDGastoDetalle", SqlDbType.Int, 50).Value = oBEVentDetalle.IDGastoDetalle;
					cmd.Parameters.Add("@IDGasto", SqlDbType.Int, 50).Value = pVenta.IDGasto;
					cmd.Parameters.Add("@DetalleProducto", SqlDbType.VarChar, 200).Value = oBEVentDetalle.ProductoDetalle;
					cmd.Parameters.Add("@Item", SqlDbType.Int, 50).Value = oBEVentDetalle.Item;
					cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int, 50).Value = oBEVentDetalle.IDUnidadMedida;
					cmd.Parameters.Add("@Cantidad", SqlDbType.Int, 50).Value = oBEVentDetalle.Cantidad;
					cmd.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitario;
					cmd.Parameters.Add("@AplicaIgv", SqlDbType.Bit).Value = oBEVentDetalle.AplicaIgv;
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEVentDetalle.IDUsuario;
					cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
					cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
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

		public BERetornoTran GastoCambiarEstado(Int32 pIDGasto, Int32 pIDEstado, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.GastoCambiarEstado");
			cmd.Parameters.Add("@IDGasto", SqlDbType.Int).Value = pIDGasto;
			cmd.Parameters.Add("@IDEstado", SqlDbType.Int).Value = pIDEstado;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;

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

		public String GastoNumeroSeleccionar(String pTipoCompra)
		{
			SqlCommand cmd = ConexionCmd("com.GastoNumeroSeleccionar");
			cmd.Parameters.Add("@TipoCompra", SqlDbType.VarChar, 1).Value = pTipoCompra;

			String pNumeroCompraFormateado = "";
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					pNumeroCompraFormateado = rd.GetString(rd.GetOrdinal("NumeroCompraFormateado"));
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
			return pNumeroCompraFormateado;
		}

		public IList GastoDetalleListar(Int32 pIDGasto)
		{
			SqlCommand cmd = ConexionCmd("com.GastoDetalleListar");
			cmd.Parameters.Add("@IDGasto", SqlDbType.Int, 10).Value = pIDGasto;
			BEGastoDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEGastoDetalle();
					oBE.Item = rd.GetInt32(rd.GetOrdinal("Item"));
					oBE.ProductoDetalle = rd.GetString(rd.GetOrdinal("DetalleProducto"));
					oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
					oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
					oBE.Cantidad = rd.GetInt32(rd.GetOrdinal("Cantidad"));
					oBE.PrecioUnitario = rd.GetDecimal(rd.GetOrdinal("PrecioUnitario"));
					oBE.SubTotal = rd.GetDecimal(rd.GetOrdinal("SubTotal"));
					oBE.Total = rd.GetDecimal(rd.GetOrdinal("Total"));
					oBE.AplicaIgv = rd.GetBoolean(rd.GetOrdinal("AplicaIgv"));
					oBE.Igv = rd.GetDecimal(rd.GetOrdinal("Igv"));

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

		public BEGasto GastoSeleccionar(Int32 pIDGasto)
		{
			SqlCommand cmd = ConexionCmd("com.GastoSeleccionar");
			BEGasto oBE = new BEGasto();
			cmd.Parameters.Add("@IDGasto", SqlDbType.Int, 10).Value = pIDGasto;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDGasto = rd.GetInt32(rd.GetOrdinal("IDGasto"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
					oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
					oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
					oBE.TipoCompra = rd.GetString(rd.GetOrdinal("TipoCompra"));
					oBE.FechaRegistro = rd.GetDateTime(rd.GetOrdinal("FechaRegistro"));
					oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
					oBE.FechaCompra = rd.GetDateTime(rd.GetOrdinal("FechaCompra"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.TotalCompra = rd.GetDecimal(rd.GetOrdinal("TotalCompra"));
					oBE.TotalIGV = rd.GetDecimal(rd.GetOrdinal("TotalIGV"));
					oBE.SubTotal = rd.GetDecimal(rd.GetOrdinal("SubTotal"));
					oBE.SerieDocumento = rd.GetString(rd.GetOrdinal("SerieDocumento"));
					oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
					oBE.Glosa = rd.GetString(rd.GetOrdinal("Glosa"));
					oBE.Cuenta = rd.GetString(rd.GetOrdinal("Cuenta"));
					oBE.CuentaCaja = rd.GetString(rd.GetOrdinal("CuentaCaja"));


					oBE.ProveedorIDTipoDocumento = rd.GetInt32(rd.GetOrdinal("ProveedorIDTipoDocumento"));
					oBE.ProveedorNumeroDocumento = rd.GetString(rd.GetOrdinal("ProveedorNumeroDocumento"));
					oBE.ProveedorRazonSocial = rd.GetString(rd.GetOrdinal("ProveedorRazonSocial"));
					oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda"));
					oBE.NumeroCompra = rd.GetString(rd.GetOrdinal("NumeroCompra"));



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

		public IList GastoListar(Int32 pIDSucursal, String pFiltro, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("com.GastoListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			BEGasto oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEGasto();
					oBE.IDGasto = rd.GetInt32(rd.GetOrdinal("IDGasto"));
					oBE.NumeroCompra = rd.GetString(rd.GetOrdinal("NumeroCompra"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.RucProveedor = rd.GetString(rd.GetOrdinal("RucProveedor"));
					oBE.Proveedor = rd.GetString(rd.GetOrdinal("Proveedor"));
					oBE.IDTipoComprobanteCS = rd.GetString(rd.GetOrdinal("IDTipoComprobanteCS"));
					oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
					oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda"));
					oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
					oBE.SerieDocumento = rd.GetString(rd.GetOrdinal("SerieDocumento"));
					oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
					oBE.FechaCompra = rd.GetDateTime(rd.GetOrdinal("FechaCompra"));
					oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
					oBE.FechaDetraccion = rd.GetDateTime(rd.GetOrdinal("FechaDetraccion"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.SubTotal = rd.GetDecimal(rd.GetOrdinal("SubTotal"));
					oBE.TotalIGV = rd.GetDecimal(rd.GetOrdinal("TotalIGV"));
					oBE.TotalCompra = rd.GetDecimal(rd.GetOrdinal("TotalCompra"));
					oBE.Cuenta = rd.GetString(rd.GetOrdinal("Cuenta"));
					oBE.CuentaCaja = rd.GetString(rd.GetOrdinal("CuentaCaja"));
					oBE.Glosa = rd.GetString(rd.GetOrdinal("Glosa"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
					oBE.EstadoNombre = rd.GetString(rd.GetOrdinal("EstadoNombre"));
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

	}
}