using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Compras;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.Compras;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.Compras
{
	public class BLCompras : BLBase
	{
		#region Transaccional

		public BERetornoTran GuardarCompras(BECompras BEParam, ArrayList pVentaDetalle)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BECompras BEVentaRetorno = new BECompras();
			int vSucursal = BEParam.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.ComprasGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{
				cmd.Parameters.Add("@IDCompras", SqlDbType.Int).Value = BEParam.IDCompras;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
				cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = BEParam.IDProveedor;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 5).Value = BEParam.IDMoneda;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = BEParam.IDTipoDocumento;
				cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = BEParam.IDFormaPago;
                cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = BEParam.IDAlmacen;
                cmd.Parameters.Add("@TipoCompra", SqlDbType.VarChar, 1).Value = BEParam.TipoCompra;
                cmd.Parameters.Add("@CuentaCaja", SqlDbType.VarChar, 20).Value = BEParam.CuentaCaja.Trim().Length == 0 ? "" : BEParam.CuentaCaja.Trim();
                cmd.Parameters.Add("@Cuenta", SqlDbType.VarChar, 20).Value = BEParam.Cuenta.Trim().Length == 0 ? "" : BEParam.Cuenta.Trim();
                cmd.Parameters.Add("@Glosa", SqlDbType.VarChar, 200).Value = BEParam.Glosa.Trim().Length == 0 ? "" : BEParam.Glosa.Trim();
				cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 4).Value = BEParam.Serie;
				cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 10).Value = BEParam.NumeroDocumento;
				cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = BEParam.FechaRegistro;
				cmd.Parameters.Add("@FechaCompra", SqlDbType.DateTime).Value = BEParam.FechaCompra;
				cmd.Parameters.Add("@FechaVencimiento", SqlDbType.DateTime).Value = BEParam.FechaVencimiento;
				cmd.Parameters.Add("@TotalCompra", SqlDbType.Decimal).Value = BEParam.TotalCompra;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = BEParam.TotalIGV;
				cmd.Parameters.Add("@TipoDocumentoReferencia", SqlDbType.VarChar, 20).Value = BEParam.TipoDocumentoReferencia;
				cmd.Parameters.Add("@SerieNumeroDocumentoReferencia", SqlDbType.VarChar, 20).Value = BEParam.SerieNumeroDocumentoReferencia;
				cmd.Parameters.Add("@FechaEmisionDocumentoReferencia", SqlDbType.Date).Value = BEParam.FechaEmisionDocumentoReferencia;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
				cmd.Parameters.Add("@IDComprasFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;

				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDComprasFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);
				}
				else
				{
					BEVentaRetorno.IDCompras = Int32.Parse(cmd.Parameters["@IDComprasFinal"].Value.ToString());
				}

				//VENTA DETALLE-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.ComprasDetalleGuardar";

				BEComprasDetalle oBEVentDetalle = new BEComprasDetalle();
				for (Int32 i = 0; i < pVentaDetalle.Count; i++)
				{
					oBEVentDetalle = (BEComprasDetalle)pVentaDetalle[i];
					cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
                    cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = BEParam.IDProveedor;
                    cmd.Parameters.Add("@IDComprasDetalle", SqlDbType.Int).Value = oBEVentDetalle.IDComprasDetalle;
					cmd.Parameters.Add("@IDCompras", SqlDbType.Int).Value = BEVentaRetorno.IDCompras;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = oBEVentDetalle.IDProducto;
					cmd.Parameters.Add("@DetalleProducto", SqlDbType.VarChar, 200).Value = oBEVentDetalle.DetalleProducto;
					cmd.Parameters.Add("@Item", SqlDbType.Int).Value = oBEVentDetalle.Item;
					cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int).Value = oBEVentDetalle.IDUnidadMedida;
					cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = oBEVentDetalle.Cantidad;
					cmd.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitario;
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

		public BERetornoTran ComprasActualizar(BECompras BEParam, ArrayList pVentaDetalle)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BECompras BEVentaRetorno = new BECompras();
			int vSucursal = BEParam.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.ComprasActualizar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{
				cmd.Parameters.Add("@IDCompras", SqlDbType.Int).Value = BEParam.IDCompras;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
				cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = BEParam.IDProveedor;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 5).Value = BEParam.IDMoneda;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = BEParam.IDTipoDocumento;
				cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = BEParam.IDFormaPago;
                cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = BEParam.IDAlmacen;
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
				cmd.Parameters.Add("@TipoDocumentoReferencia", SqlDbType.VarChar, 2).Value = BEParam.TipoDocumentoReferencia;
				cmd.Parameters.Add("@SerieNumeroDocumentoReferencia", SqlDbType.VarChar, 20).Value = BEParam.SerieNumeroDocumentoReferencia;
				cmd.Parameters.Add("@FechaEmisionDocumentoReferencia", SqlDbType.Date).Value = BEParam.FechaEmisionDocumentoReferencia;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
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
				cmd.CommandText = "gen.ComprasDetalleGuardar";

				BEComprasDetalle oBEVentDetalle = new BEComprasDetalle();
				for (Int32 i = 0; i < pVentaDetalle.Count; i++)
				{
					oBEVentDetalle = (BEComprasDetalle)pVentaDetalle[i];
					cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
                    cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = BEParam.IDProveedor;
                    cmd.Parameters.Add("@IDComprasDetalle", SqlDbType.Int).Value = oBEVentDetalle.IDComprasDetalle;
					cmd.Parameters.Add("@IDCompras", SqlDbType.Int).Value = BEParam.IDCompras;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = oBEVentDetalle.IDProducto;
					cmd.Parameters.Add("@DetalleProducto", SqlDbType.VarChar, 200).Value = oBEVentDetalle.DetalleProducto;
					cmd.Parameters.Add("@Item", SqlDbType.Int).Value = oBEVentDetalle.Item;
					cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int).Value = oBEVentDetalle.IDUnidadMedida;
					cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = oBEVentDetalle.Cantidad;
					cmd.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitario;
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
		 
		public BERetornoTran ComprasPagoGuardar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ComprasPagoGuardar");
			cmd = LlenarEstructura(pEntidad, cmd, "I");
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

		public BERetornoTran ComprasCambiarEstado(Int32 pIDCompra, Int32 pIDEstado, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ComprasCambiarEstado");
			cmd.Parameters.Add("@IDCompras", SqlDbType.Int).Value = pIDCompra;
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

		public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand cmd, String pTipoTransaccion)
		{
			BECompras oBE = (BECompras)pEntidad;
			cmd.Parameters.Add("@IDComprasPago", SqlDbType.Int).Value = oBE.IDComprasPago;
			cmd.Parameters.Add("@IDCompra", SqlDbType.Int).Value = oBE.IDCompras;
			cmd.Parameters.Add("@IDMedioPago", SqlDbType.Int).Value = oBE.IDMedioPago;
			if (oBE.IDBanco == 0)
			{
				cmd.Parameters.Add("@IDBanco", SqlDbType.Int).Value = DBNull.Value;
			}
			else {
				cmd.Parameters.Add("@IDBanco", SqlDbType.Int).Value = oBE.IDBanco;
			}
			cmd.Parameters.Add("@MontoPagado", SqlDbType.Decimal).Value = oBE.MontoPagado;
			cmd.Parameters.Add("@CuentaBancaria", SqlDbType.VarChar, 100).Value = oBE.CuentaBancaria;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			return cmd;
		}


        public BERetornoTran ComprasValidarCaja(Int32 pIDUsuario, Int32 pIDSucursal)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.ComprasValidarCaja");
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 10).Value = pIDUsuario;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pIDSucursal;
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

        public String ComprasNumeroSeleccionar(String pTipoCompra)
		{
			SqlCommand cmd = ConexionCmd("gen.ComprasNumeroSeleccionar");
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

		public IList ComprasDetalleListar(Int32 pIDCompras)
		{
			SqlCommand cmd = ConexionCmd("gen.ComprasDetalleListar");
			cmd.Parameters.Add("@IDCompra", SqlDbType.Int, 10).Value = pIDCompras;
			BEComprasDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEComprasDetalle();
                    oBE.Item = rd.GetInt32(rd.GetOrdinal("Item"));
                    oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
                    oBE.CodigoProducto = rd.GetString(rd.GetOrdinal("CodigoProducto"));
                    oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
                    oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
                    oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
                    oBE.Cantidad = rd.GetInt32(rd.GetOrdinal("Cantidad"));
                    oBE.DetalleProducto = rd.GetString(rd.GetOrdinal("DetalleProducto"));
                    oBE.PrecioUnitario = rd.GetDecimal(rd.GetOrdinal("PrecioUnitario"));
                    oBE.ControlaLote = rd.GetBoolean(rd.GetOrdinal("ControlaLote"));
                    oBE.Factor = rd.GetInt32(rd.GetOrdinal("Factor"));
                    oBE.CantidadVenta = oBE.Factor * oBE.Cantidad;

                    oBE.UnidadMedidaVenta = rd.GetString(rd.GetOrdinal("UnidadMedidaVenta"));
                    oBE.SubTotal = rd.GetDecimal(rd.GetOrdinal("SubTotal"));
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

		public BECompras ComprasSeleccionar(Int32 pIDCompras)
		{
			SqlCommand cmd = ConexionCmd("gen.ComprasSeleccionar");
			BECompras oBE = new BECompras();
			cmd.Parameters.Add("@IDCompras", SqlDbType.Int, 10).Value = pIDCompras;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDCompras = rd.GetInt32(rd.GetOrdinal("IDCompras"));
					oBE.NumeroCompra = rd.GetString(rd.GetOrdinal("NumeroCompra"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
                    oBE.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacen"));
                    oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
					oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
					oBE.TipoCompra = rd.GetString(rd.GetOrdinal("TipoCompra"));
					oBE.FechaRegistro = rd.GetDateTime(rd.GetOrdinal("FechaRegistro"));
					oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
					oBE.FechaCompra = rd.GetDateTime(rd.GetOrdinal("FechaCompra"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda"));
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
					oBE.IDTipoComprobanteCS = rd.GetString(rd.GetOrdinal("IDTipoComprobanteCS"));
					oBE.TipoDocumentoReferencia = rd.GetString(rd.GetOrdinal("TipoDocumentoReferencia"));
					oBE.SerieNumeroDocumentoReferencia = rd.GetString(rd.GetOrdinal("SerieNumeroDocumentoReferencia"));
					oBE.FechaEmisionDocumentoReferencia = rd.GetDateTime(rd.GetOrdinal("FechaEmisionDocumentoReferencia"));
                    oBE.TipoComprobanteCompra = rd.GetInt32(rd.GetOrdinal("TipoComprobanteCompra"));

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


        public IList ComprasNotaCreditoListar(Int32 pIDSucursal, Int32 pIDAlmacen, String pFiltro, String pFechaInicio, String pFechaFin, Int32 pIDEstadoCompra, Int32 pIDEstadoAlmacen, String pAccion)
        {
            SqlCommand cmd = ConexionCmd("gen.ComprasNotaCreditoListar");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = pIDAlmacen;
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
            cmd.Parameters.Add("@IDEstadoCompra", SqlDbType.Int).Value = pIDEstadoCompra;
            cmd.Parameters.Add("@IDEstadoAlmacen", SqlDbType.Int).Value = pIDEstadoAlmacen;
            cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = pAccion;

            BECompras oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BECompras();
                    oBE.IDCompras = rd.GetInt32(rd.GetOrdinal("IDCompras"));
                    oBE.NumeroCompra = rd.GetString(rd.GetOrdinal("NumeroCompra"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
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
                    oBE.FechaRegistro = rd.GetDateTime(rd.GetOrdinal("FechaRegistro"));
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
                    oBE.EstadoAlmacen = rd.GetString(rd.GetOrdinal("EstadoAlmacen"));
                    oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
                    oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));
                    oBE.Sigla = rd.GetString(rd.GetOrdinal("Sigla"));
                    oBE.IDEstadoPago = rd.GetInt32(rd.GetOrdinal("IDEstadoPago"));
                    oBE.EstadoPago = rd.GetString(rd.GetOrdinal("EstadoPago"));
                    oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("TipoComprobanteCompra"));


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

        //filtrar compras por tipo documento 1= factura ; 2=boleta
        public IList ComprasListarxDocumento(Int32 pIDSucursal, Int32 pIDAlmacen, String pFiltro, String pFechaInicio, String pFechaFin, Int32 pIDEstadoCompra, Int32 pIDEstadoAlmacen, String pAccion)
        {
            SqlCommand cmd = ConexionCmd("gen.ComprasListarNotaCredito");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = pIDAlmacen;
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
            cmd.Parameters.Add("@IDEstadoCompra", SqlDbType.Int).Value = pIDEstadoCompra;
            cmd.Parameters.Add("@IDEstadoAlmacen", SqlDbType.Int).Value = pIDEstadoAlmacen;
            cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = pAccion;

            BECompras oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BECompras();
                    oBE.IDCompras = rd.GetInt32(rd.GetOrdinal("IDCompras"));
                    oBE.NumeroCompra = rd.GetString(rd.GetOrdinal("NumeroCompra"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
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
                    oBE.FechaRegistro = rd.GetDateTime(rd.GetOrdinal("FechaRegistro"));
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
                    oBE.EstadoAlmacen = rd.GetString(rd.GetOrdinal("EstadoAlmacen"));
                    oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
                    oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));
                    oBE.Sigla = rd.GetString(rd.GetOrdinal("Sigla"));
                    oBE.IDEstadoPago = rd.GetInt32(rd.GetOrdinal("IDEstadoPago"));
                    oBE.EstadoPago = rd.GetString(rd.GetOrdinal("EstadoPago"));
                    oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("TipoComprobanteCompra"));


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

        public IList ComprasListar(Int32 pIDSucursal, Int32 pIDAlmacen, String pFiltro, String pFechaInicio, String pFechaFin, Int32 pIDEstadoCompra, Int32 pIDEstadoAlmacen, String pAccion)
		{
			SqlCommand cmd = ConexionCmd("gen.ComprasListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = pIDAlmacen;
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			cmd.Parameters.Add("@IDEstadoCompra", SqlDbType.Int).Value = pIDEstadoCompra;
			cmd.Parameters.Add("@IDEstadoAlmacen", SqlDbType.Int).Value = pIDEstadoAlmacen;
			cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = pAccion;
			 
			BECompras oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECompras(); 
					oBE.IDCompras = rd.GetInt32(rd.GetOrdinal("IDCompras"));
					oBE.NumeroCompra = rd.GetString(rd.GetOrdinal("NumeroCompra"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
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
					oBE.FechaRegistro = rd.GetDateTime(rd.GetOrdinal("FechaRegistro"));
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
					oBE.EstadoAlmacen = rd.GetString(rd.GetOrdinal("EstadoAlmacen"));
					oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
					oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));
                    oBE.Sigla = rd.GetString(rd.GetOrdinal("Sigla"));
                    oBE.IDEstadoPago = rd.GetInt32(rd.GetOrdinal("IDEstadoPago"));
                    oBE.EstadoPago = rd.GetString(rd.GetOrdinal("EstadoPago"));
                    oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("TipoComprobanteCompra"));
					oBE.IDEstadoAlmacen = rd.GetInt32(rd.GetOrdinal("IDEstadoAlmacen"));

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

		public IList ProveedorPendientePagoListar(String pFiltro, Int32 pIDFormaPago, Boolean pPagada)
		{
			SqlCommand cmd = ConexionCmd("gen.ProveedorPendientePagoListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
			cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = pIDFormaPago;
			cmd.Parameters.Add("@Pagada", SqlDbType.Bit).Value = pPagada;

			BEProveedor oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProveedor();
					oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
					oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
					oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
					oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
					oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
					oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));
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

		public IList ComprasxProveedorListar(Int32 pIDProveedor)
		{
			SqlCommand cmd = ConexionCmd("gen.ComprasxProveedorListar");
			cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = pIDProveedor;

			BECompras oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECompras();
					oBE.IDCompras = rd.GetInt32(rd.GetOrdinal("IDCompras"));
					oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
					oBE.FechaCompra = rd.GetDateTime(rd.GetOrdinal("FechaCompra"));
					oBE.TotalCompra = rd.GetDecimal(rd.GetOrdinal("TotalCompra"));
					oBE.DeudaPendiente = rd.GetDecimal(rd.GetOrdinal("DeudaPendiente"));
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

		public IList PagoProveedorxCompraListar(Int32 pIDCompra)
		{
			SqlCommand cmd = ConexionCmd("gen.PagoProveedorxCompraListar");
			cmd.Parameters.Add("@IDCompra", SqlDbType.Int).Value = pIDCompra;

			BECompras oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECompras();
					oBE.IDComprasPago = rd.GetInt32(rd.GetOrdinal("IDComprasPago"));
					oBE.MedioPago = rd.GetString(rd.GetOrdinal("MedioPago"));
					oBE.Banco = rd.GetString(rd.GetOrdinal("Banco"));
					oBE.CuentaBancaria = rd.GetString(rd.GetOrdinal("CuentaBancaria"));
					oBE.MontoPagado = rd.GetDecimal(rd.GetOrdinal("MontoPagado"));
					oBE.FechaPago = rd.GetDateTime(rd.GetOrdinal("FechaPago"));
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

		public Decimal ReporteSumaTotalComprasxProveedorListar(Int32 pIDSucursal, Int32 pIDProveedor, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("com.ReporteSumaTotalComprasxProveedorListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = pIDProveedor;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;

			Decimal TotalCompra = 0;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					TotalCompra = rd.GetDecimal(rd.GetOrdinal("TotalComprado"));
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
			return TotalCompra;
		}

		public IList ReporteComprasxProveedorListar(Int32 pIDSucursal, Int32 pIDProveedor, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("com.ReporteComprasxProveedorListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = pIDProveedor;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;

			BECompras oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECompras();
                    oBE.IDCompras = rd.GetInt32(rd.GetOrdinal("IDCompras"));
                    oBE.NumeroCompra = rd.GetString(rd.GetOrdinal("NumeroCompra"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial")); 
                    oBE.FechaCompra = rd.GetDateTime(rd.GetOrdinal("FechaCompra"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
                    oBE.TotalCompra = rd.GetDecimal(rd.GetOrdinal("TotalCompra"));

					oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));
					oBE.EstadoCompra = rd.GetString(rd.GetOrdinal("EstadoCompra"));
					oBE.EstadoPago = rd.GetString(rd.GetOrdinal("EstadoPago"));
					  
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

		public IList ReporteComprasxProveedorDetalleListar(Int32 pIDCompras)
		{
			SqlCommand cmd = ConexionCmd("com.ReporteComprasxProveedorDetalleListar");
			cmd.Parameters.Add("@IDCompras", SqlDbType.Int).Value = pIDCompras;

			BEComprasDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEComprasDetalle();
					oBE.IDComprasDetalle = rd.GetInt32(rd.GetOrdinal("IDComprasDetalle"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.Item = rd.GetInt32(rd.GetOrdinal("Item"));
					oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
					oBE.Cantidad = rd.GetInt32(rd.GetOrdinal("Cantidad"));
					oBE.PrecioUnitario = rd.GetDecimal(rd.GetOrdinal("PrecioUnitario"));
					oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
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

		public IList ReporteRegistroCompras(Int32 pIDSucursal, String pPeriodo)
		{
			SqlCommand cmd = ConexionCmd("com.ReporteRegistroCompras");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@Periodo", SqlDbType.VarChar, 100).Value = pPeriodo;

            BECompras oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECompras();
					oBE.Periodo = rd.GetString(rd.GetOrdinal("Periodo"));
					oBE.FechaRegistro = rd.GetDateTime(rd.GetOrdinal("FechaRegistro"));
                    oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
                    oBE.CPTipoDocumento = rd.GetString(rd.GetOrdinal("CPTipoComprobante"));
					oBE.CPSerieDocumento = rd.GetString(rd.GetOrdinal("CPSerieComprobante"));
					oBE.CPNumeroDocumento = rd.GetString(rd.GetOrdinal("CPNumeroComprobante"));
					oBE.PROVTipoDocumento = rd.GetInt32(rd.GetOrdinal("PROVTipoDocumento"));
					oBE.PROVNumeroDocumento = rd.GetString(rd.GetOrdinal("PROVNumeroDocumento"));
					oBE.PROVRazonSocial = rd.GetString(rd.GetOrdinal("PROVRazonSocial"));
					oBE.BaseImponible = rd.GetDecimal(rd.GetOrdinal("BaseImponible"));
					oBE.Igv = rd.GetDecimal(rd.GetOrdinal("TotalIGV"));
					oBE.ImporteNoGravado = rd.GetInt32(rd.GetOrdinal("ImporteNoGravado"));
					oBE.ImporteTotal = rd.GetDecimal(rd.GetOrdinal("TotalCompra"));
					oBE.FechaEmisionReferencia = rd.GetDateTime(rd.GetOrdinal("FechaEmisionReferencia"));
					oBE.TipoDocumentoReferencia = rd.GetString(rd.GetOrdinal("TipoDocumentoReferencia"));
					oBE.SerieReferencia = rd.GetString(rd.GetOrdinal("SerieReferencia"));
					oBE.NumeroComprobanteReferencia = rd.GetString(rd.GetOrdinal("NumeroComprobanteReferencia"));
					//oBE.FechaEmisionRetencion = rd.GetString(rd.GetOrdinal("FechaEmisionRetencion"));
					//oBE.NumeroComprobanteRetencion = rd.GetString(rd.GetOrdinal("NumeroComprobanteRetencion"));
					//oBE.Cuenta = rd.GetString(rd.GetOrdinal("Cuenta"));
					oBE.EstadoPago = rd.GetString(rd.GetOrdinal("EstadoPago"));
                    oBE.Sigla = rd.GetString(rd.GetOrdinal("Sigla"));
                    oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));

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

        public IList CompraPendientePagoXProveedorListar(Int32 pIDProveedor, String pTipoFiltro, String pFechaInicio, String pFechaFin, String pFiltro, Int32 pFormaPago)
        {
            SqlCommand cmd = ConexionCmd("com.CompraPendientePagoXProveedorListar");
            cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = pIDProveedor;
            cmd.Parameters.Add("@TipoFiltro", SqlDbType.VarChar, 10).Value = pTipoFiltro;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
            cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = pFormaPago;
            //cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pFormaPago;

            BECompras oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BECompras();
                    oBE.IDCompras = rd.GetInt32(rd.GetOrdinal("IDCompras"));
                    oBE.NumeroCompraFormato = rd.GetString(rd.GetOrdinal("NumeroCompraFormato"));
                    oBE.NumeroCompraCuentas = rd.GetInt32(rd.GetOrdinal("NumeroCompra"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.ProveedorNumeroDocumento = rd.GetString(rd.GetOrdinal("ProveedorNumeroDocumento"));
                    oBE.Proveedor = rd.GetString(rd.GetOrdinal("Proveedor"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
                    oBE.TipoComprobanteSigla = rd.GetString(rd.GetOrdinal("TipoComprobanteSigla"));
                    oBE.Serie = rd.GetString(rd.GetOrdinal("Serie"));
                    oBE.NumeroComprobante = rd.GetString(rd.GetOrdinal("NumeroComprobante"));
                    oBE.FechaEmision = rd.GetDateTime(rd.GetOrdinal("FechaEmision"));
                    oBE.FechaRegistro = rd.GetDateTime(rd.GetOrdinal("FechaRegistro"));
                    oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
                    oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda"));
                    oBE.MonedaSimbolo = rd.GetString(rd.GetOrdinal("MonedaSimbolo"));
                    oBE.TotalCompra = rd.GetDecimal(rd.GetOrdinal("TotalCompra"));
                    oBE.TotalIGV = rd.GetDecimal(rd.GetOrdinal("TotalIGV"));
                    oBE.SubTotal = rd.GetDecimal(rd.GetOrdinal("SubTotal"));
                    oBE.EstadoAlmacen = rd.GetString(rd.GetOrdinal("EstadoAlmacen"));
                    oBE.EstadoCompra = rd.GetString(rd.GetOrdinal("EstadoCompra"));
                    oBE.EstadoPago = rd.GetString(rd.GetOrdinal("EstadoPago"));
                    oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));
                    oBE.IDEstadoAlmacen = rd.GetInt32(rd.GetOrdinal("IDEstadoAlmacen"));
                    oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
                    oBE.IDEstadoPago = rd.GetInt32(rd.GetOrdinal("IDEstadoPago"));
                    oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("IDTipoComprobante"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
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


        #endregion

    }
}