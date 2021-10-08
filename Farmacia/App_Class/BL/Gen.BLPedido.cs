using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLPedido : BLBase
	{ 
		public BERetornoTran PedidoAnular(Int32 pIDPedido, String pMotivoAnulacion, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoAnular");
			cmd.Parameters.Add("@IDPedido", SqlDbType.Int, 10).Value = pIDPedido;
			cmd.Parameters.Add("@MotivoAnulacion", SqlDbType.VarChar, 500).Value = pMotivoAnulacion;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 10).Value = pIDUsuario;
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
 
		public BEPedido PedidoSeleccionar(Int32 pIDPedido)
		{
			SqlCommand cmd = ConexionCmd("gen.PedidoSeleccionar");
			BEPedido oBE = new BEPedido();
			cmd.Parameters.Add("@IDPedido", SqlDbType.Int, 10).Value = pIDPedido;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.RucEmisor = rd.GetString(rd.GetOrdinal("RucEmisor"));
					oBE.RazonSocialEmisor = rd.GetString(rd.GetOrdinal("RazonSocialEmisor"));
					oBE.UbigeoEmisor = rd.GetString(rd.GetOrdinal("UbigeoEmisor"));
					oBE.DireccionEmisor = rd.GetString(rd.GetOrdinal("DireccionEmisor"));
					oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda"));
					oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("IDTipoComprobante"));
					oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
					oBE.CalculoIGV = rd.GetDecimal(rd.GetOrdinal("CalculoIGV"));
					oBE.CalculoISC = rd.GetDecimal(rd.GetOrdinal("CalculoISC"));
					oBE.CalculoDetraccion = rd.GetDecimal(rd.GetOrdinal("CalculoDetraccion"));
					oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
					oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
					oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
					oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
					oBE.DireccionCliente = rd.GetString(rd.GetOrdinal("DireccionCliente"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.TipoCambio = rd.GetDecimal(rd.GetOrdinal("TipoCambio"));
					oBE.FechaVenta = rd.GetDateTime(rd.GetOrdinal("FechaVenta"));
					oBE.TotalOperacionExonerada = rd.GetDecimal(rd.GetOrdinal("TotalOperacionExonerada"));
					oBE.TotalOperacionInafecta = rd.GetDecimal(rd.GetOrdinal("TotalOperacionInafecta"));
					oBE.TotalOperacionGratuita = rd.GetDecimal(rd.GetOrdinal("TotalOperacionGratuita"));
					oBE.TotalOperacionGravada = rd.GetDecimal(rd.GetOrdinal("TotalOperacionGravada"));
					oBE.TotalIGV = rd.GetDecimal(rd.GetOrdinal("TotalIGV"));
					oBE.TotalISC = rd.GetDecimal(rd.GetOrdinal("TotalISC"));
					oBE.TotalDescuentos = rd.GetDecimal(rd.GetOrdinal("TotalDescuentos"));
					oBE.TotalOtrosTributos = rd.GetDecimal(rd.GetOrdinal("TotalOtrosTributos"));
					oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
					oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
					oBE.Cajero = rd.GetString(rd.GetOrdinal("Cajero"));
					oBE.MontoLetras = rd.GetString(rd.GetOrdinal("MontoLetras"));
					//oBE.CodigoQR = rd.GetString(rd.GetOrdinal("CodigoQR"));
					oBE.IDPedido = rd.GetInt32(rd.GetOrdinal("IDPedido"));
					oBE.Anulado = rd.GetBoolean(rd.GetOrdinal("Anulado"));
					oBE.MotivoAnulacion = rd.GetString(rd.GetOrdinal("MotivoAnulacion"));
					oBE.ImpresionVenta = rd.GetString(rd.GetOrdinal("ImpresionVenta"));


					oBE.NumeroPlaca = rd.GetString(rd.GetOrdinal("NumeroPlaca"));
					oBE.IDModeloVersion = rd.GetInt32(rd.GetOrdinal("IDModeloVersion"));
					oBE.Color = rd.GetString(rd.GetOrdinal("Color"));
					oBE.Motor = rd.GetString(rd.GetOrdinal("Motor"));
					oBE.Combustible = rd.GetString(rd.GetOrdinal("Combustible"));
					oBE.SerieChasis = rd.GetString(rd.GetOrdinal("SerieChasis"));
					oBE.AnioFabricacion = rd.GetInt32(rd.GetOrdinal("AnioFabricacion"));
					oBE.AnioModelo = rd.GetInt32(rd.GetOrdinal("AnioModelo"));
					oBE.NumeroAsiento = rd.GetString(rd.GetOrdinal("NumeroAsiento"));
					oBE.Kilometraje = rd.GetString(rd.GetOrdinal("Kilometraje"));

					oBE.IDSalon = rd.GetInt32(rd.GetOrdinal("IDSalon"));
					oBE.HoraInicio = rd.GetString(rd.GetOrdinal("HoraInicio"));
					oBE.HoraFin = rd.GetString(rd.GetOrdinal("HoraFin"));
					oBE.NumeroInvitado = rd.GetInt32(rd.GetOrdinal("NumeroInvitado"));
					 
					oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));

					oBE.NumeroMesa = rd.GetInt32(rd.GetOrdinal("NumeroMesa")); 
					oBE.UsuarioCreacion = rd.GetString(rd.GetOrdinal("UsuarioCreacion"));
				 



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

		public IList PedidosxClienteListar(Int32 pIDCliente)
		{
			SqlCommand cmd = ConexionCmd("gen.PedidosxClienteListar");
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pIDCliente;
			BEPedido oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEPedido();
					oBE.IDPedido = rd.GetInt32(rd.GetOrdinal("IDPedido"));
					oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
					oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
					oBE.FechaVenta = rd.GetDateTime(rd.GetOrdinal("FechaVenta"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
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

		public IList PedidoListar(BEPedido pBE)
		{
			SqlCommand cmd = ConexionCmd("gen.PedidoListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pBE.Filtro;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pBE.IDCliente;
			cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int).Value = pBE.IDTipoComprobante;
			cmd.Parameters.Add("@IDEstado", SqlDbType.Int).Value = pBE.IDEstado;
			cmd.Parameters.Add("@IDEstadoCobranza", SqlDbType.Int).Value = pBE.IDEstadoCobranza;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pBE.FechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pBE.FechaFin;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pBE.IDUsuario;
			cmd.Parameters.Add("@Accion", SqlDbType.VarChar, 100).Value = pBE.Accion;
			BEPedido oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEPedido();
					oBE.IDPedido = rd.GetInt32(rd.GetOrdinal("IDPedido"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
					oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("IDTipoComprobante"));
					oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
					oBE.TipoComprobanteCodigoSunat = rd.GetString(rd.GetOrdinal("CodigoSunatTipoComprobante"));
					oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
					oBE.FechaVenta = rd.GetDateTime(rd.GetOrdinal("FechaVenta"));
					oBE.TotalOperacionGravada = rd.GetDecimal(rd.GetOrdinal("TotalOperacionGravada"));
					oBE.TotalIGV = rd.GetDecimal(rd.GetOrdinal("TotalIGV"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.Simbolo = rd.GetString(rd.GetOrdinal("Simbolo"));
					oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
					oBE.Migrado = rd.GetString(rd.GetOrdinal("Migrado"));
					oBE.IDUsuarioCreacion = rd.GetInt32(rd.GetOrdinal("IDUsuarioCreacion"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.NumeroDocumentoCliente = rd.GetString(rd.GetOrdinal("NumeroDocumentoCliente"));
					oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
					oBE.Usuario = rd.GetString(rd.GetOrdinal("Usuario"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.EstadoSunat = rd.GetString(rd.GetOrdinal("EstadoSunat"));
					oBE.EstadoCobranza = rd.GetString(rd.GetOrdinal("EstadoCobranza"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
					oBE.Anulado = rd.GetBoolean(rd.GetOrdinal("Anulado"));
					oBE.MotivoAnulacion = rd.GetString(rd.GetOrdinal("MotivoAnulacion"));
					oBE.SerieNumeroAfectado = rd.GetString(rd.GetOrdinal("SerieNumeroAfectado"));

					oBE.NumeroPedidoFormato = rd.GetString(rd.GetOrdinal("NumeroPedidoFormato"));
					oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
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
		 
		public BERetornoTran PedidoGuardar(BEPedido pPedido)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEPedido BEPedidoRetorno = new BEPedido();
			int vSucursal = pPedido.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//Pedido------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.PedidoGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{

				cmd.Parameters.Add("@IDPedido", SqlDbType.Int, 10).Value = pPedido.IDPedido;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pPedido.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pPedido.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pPedido.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pPedido.TipoCambio;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pPedido.IDTipoComprobante;
				cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 30).Value = pPedido.SerieNumero;
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pPedido.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pPedido.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pPedido.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pPedido.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pPedido.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pPedido.TotalOperacionGravada;
				cmd.Parameters.Add("@TotalOperacionExonerada", SqlDbType.Decimal).Value = pPedido.TotalOperacionExonerada;
				cmd.Parameters.Add("@TotalOperacionInafecta", SqlDbType.Decimal).Value = pPedido.TotalOperacionInafecta;
				cmd.Parameters.Add("@TotalOperacionGratuita", SqlDbType.Decimal).Value = pPedido.TotalOperacionGratuita;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pPedido.TotalIGV;
				cmd.Parameters.Add("@TotalISC", SqlDbType.Decimal).Value = pPedido.TotalISC;
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pPedido.TotalDescuentos;
				cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = pPedido.TotalOtrosTributos;
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pPedido.TotalVenta;
				cmd.Parameters.Add("@Migrado", SqlDbType.VarChar, 1).Value = pPedido.Migrado;
				cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 50).Value = pPedido.Proceso;
				cmd.Parameters.Add("@IDVentaRelacionado", SqlDbType.Int).Value = pPedido.IDVentaRelacionado == 0 ? (Object)DBNull.Value : pPedido.IDVentaRelacionado;
				cmd.Parameters.Add("@IDTipoMotivo", SqlDbType.Int).Value = pPedido.IDTipoMotivo == 0 ? (Object)DBNull.Value : pPedido.IDTipoMotivo;
				cmd.Parameters.Add("@MotivoNota", SqlDbType.VarChar, 500).Value = pPedido.MotivoNota;
				cmd.Parameters.Add("@IDMedioPago", SqlDbType.Int).Value = pPedido.IDMedioPago == 0 ? (Object)DBNull.Value : pPedido.IDMedioPago;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pPedido.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@IDPedidoFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDPedidoFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}
				else
				{
					BEPedidoRetorno.IDPedido = Int32.Parse(cmd.Parameters["@IDPedidoFinal"].Value.ToString());
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

		public BERetornoTran PedidoAutomotrizGuardar(BEPedido pPedido)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEPedido BEPedidoRetorno = new BEPedido();
			int vSucursal = pPedido.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//Pedido------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.PedidoAutomotrizGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{

				cmd.Parameters.Add("@IDPedido", SqlDbType.Int, 10).Value = pPedido.IDPedido;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pPedido.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pPedido.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pPedido.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pPedido.TipoCambio;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pPedido.IDTipoComprobante;
				cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 30).Value = pPedido.SerieNumero;
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pPedido.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pPedido.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pPedido.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pPedido.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pPedido.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pPedido.TotalOperacionGravada;
				cmd.Parameters.Add("@TotalOperacionExonerada", SqlDbType.Decimal).Value = pPedido.TotalOperacionExonerada;
				cmd.Parameters.Add("@TotalOperacionInafecta", SqlDbType.Decimal).Value = pPedido.TotalOperacionInafecta;
				cmd.Parameters.Add("@TotalOperacionGratuita", SqlDbType.Decimal).Value = pPedido.TotalOperacionGratuita;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pPedido.TotalIGV;
				cmd.Parameters.Add("@TotalISC", SqlDbType.Decimal).Value = pPedido.TotalISC;
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pPedido.TotalDescuentos;
				cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = pPedido.TotalOtrosTributos;
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pPedido.TotalVenta;
				cmd.Parameters.Add("@Migrado", SqlDbType.VarChar, 1).Value = pPedido.Migrado;
				cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 50).Value = pPedido.Proceso;
				cmd.Parameters.Add("@IDVentaRelacionado", SqlDbType.Int).Value = pPedido.IDVentaRelacionado == 0 ? (Object)DBNull.Value : pPedido.IDVentaRelacionado;
				cmd.Parameters.Add("@IDTipoMotivo", SqlDbType.Int).Value = pPedido.IDTipoMotivo == 0 ? (Object)DBNull.Value : pPedido.IDTipoMotivo;
				cmd.Parameters.Add("@MotivoNota", SqlDbType.VarChar, 500).Value = pPedido.MotivoNota;
				cmd.Parameters.Add("@IDMedioPago", SqlDbType.Int).Value = pPedido.IDMedioPago == 0 ? (Object)DBNull.Value : pPedido.IDMedioPago;

				cmd.Parameters.Add("@NumeroPlaca", SqlDbType.VarChar, 100).Value = pPedido.NumeroPlaca;
				cmd.Parameters.Add("@IDModeloVersion", SqlDbType.Int).Value = pPedido.IDModeloVersion == 0 ? (Object)DBNull.Value : pPedido.IDModeloVersion;
				cmd.Parameters.Add("@Color", SqlDbType.VarChar, 100).Value = pPedido.Color;
				cmd.Parameters.Add("@Motor", SqlDbType.VarChar, 100).Value = pPedido.Motor;
				cmd.Parameters.Add("@Combustible", SqlDbType.VarChar, 100).Value = pPedido.Combustible;
				cmd.Parameters.Add("@SerieChasis", SqlDbType.VarChar, 100).Value = pPedido.SerieChasis;
				cmd.Parameters.Add("@AnioFabricacion", SqlDbType.Int).Value = pPedido.AnioFabricacion == 0 ? (Object)DBNull.Value : pPedido.AnioFabricacion;
				cmd.Parameters.Add("@AnioModelo", SqlDbType.Int).Value = pPedido.AnioModelo == 0 ? (Object)DBNull.Value : pPedido.AnioModelo;
				cmd.Parameters.Add("@NumeroAsiento", SqlDbType.VarChar, 100).Value = pPedido.NumeroAsiento;
				cmd.Parameters.Add("@Kilometraje", SqlDbType.VarChar, 100).Value = pPedido.Kilometraje;

				cmd.Parameters.Add("@Observacion", SqlDbType.VarChar).Value = pPedido.Observacion; 
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pPedido.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@IDPedidoFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDPedidoFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}
				else
				{
					BEPedidoRetorno.IDPedido = Int32.Parse(cmd.Parameters["@IDPedidoFinal"].Value.ToString());
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
		 
		public BERetornoTran PedidoValidarCaja(Int32 pIDUsuario, Int32 pIDSucursal)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PedidoValidarCaja");
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
		 
		public BERetornoTran PedidoEventoGuardar(BEPedido pPedido)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEPedido BEPedidoRetorno = new BEPedido();
			int vSucursal = pPedido.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//Pedido------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.PedidoEventoGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{

				cmd.Parameters.Add("@IDPedido", SqlDbType.Int, 10).Value = pPedido.IDPedido;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pPedido.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pPedido.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pPedido.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pPedido.TipoCambio;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pPedido.IDTipoComprobante;
				cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 30).Value = pPedido.SerieNumero;
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pPedido.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pPedido.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pPedido.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pPedido.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pPedido.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pPedido.TotalOperacionGravada;
				cmd.Parameters.Add("@TotalOperacionExonerada", SqlDbType.Decimal).Value = pPedido.TotalOperacionExonerada;
				cmd.Parameters.Add("@TotalOperacionInafecta", SqlDbType.Decimal).Value = pPedido.TotalOperacionInafecta;
				cmd.Parameters.Add("@TotalOperacionGratuita", SqlDbType.Decimal).Value = pPedido.TotalOperacionGratuita;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pPedido.TotalIGV;
				cmd.Parameters.Add("@TotalISC", SqlDbType.Decimal).Value = pPedido.TotalISC;
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pPedido.TotalDescuentos;
				cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = pPedido.TotalOtrosTributos;
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pPedido.TotalVenta;
				cmd.Parameters.Add("@Migrado", SqlDbType.VarChar, 1).Value = pPedido.Migrado;
				cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 50).Value = pPedido.Proceso;
				cmd.Parameters.Add("@IDVentaRelacionado", SqlDbType.Int).Value = pPedido.IDVentaRelacionado == 0 ? (Object)DBNull.Value : pPedido.IDVentaRelacionado;
				cmd.Parameters.Add("@IDTipoMotivo", SqlDbType.Int).Value = pPedido.IDTipoMotivo == 0 ? (Object)DBNull.Value : pPedido.IDTipoMotivo;
				cmd.Parameters.Add("@MotivoNota", SqlDbType.VarChar, 500).Value = pPedido.MotivoNota;
				cmd.Parameters.Add("@IDMedioPago", SqlDbType.Int).Value = pPedido.IDMedioPago == 0 ? (Object)DBNull.Value : pPedido.IDMedioPago;

				cmd.Parameters.Add("@IDSalon", SqlDbType.Int).Value = pPedido.IDSalon; 
				cmd.Parameters.Add("@HoraInicio", SqlDbType.VarChar, 5).Value = pPedido.HoraInicio;
				cmd.Parameters.Add("@HoraFin", SqlDbType.VarChar, 5).Value = pPedido.HoraFin;
				cmd.Parameters.Add("@NumeroInvitado", SqlDbType.Int).Value = pPedido.NumeroInvitado;
				cmd.Parameters.Add("@Observacion", SqlDbType.VarChar).Value = pPedido.Observacion; 
				 


				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pPedido.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@IDPedidoFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDPedidoFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}
				else
				{
					BEPedidoRetorno.IDPedido = Int32.Parse(cmd.Parameters["@IDPedidoFinal"].Value.ToString());
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
		 
		public BERetornoTran PedidoEventoActualizar(BEPedido pPedido)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEPedido BEPedidoRetorno = new BEPedido();
			int vSucursal = pPedido.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//Pedido------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.PedidoEventoActualizar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{ 
				cmd.Parameters.Add("@IDPedido", SqlDbType.Int, 10).Value = pPedido.IDPedido;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pPedido.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pPedido.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pPedido.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pPedido.TipoCambio;  
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pPedido.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pPedido.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pPedido.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pPedido.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pPedido.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pPedido.TotalOperacionGravada; 
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pPedido.TotalIGV; 
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pPedido.TotalDescuentos; 
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pPedido.TotalVenta;  
				cmd.Parameters.Add("@IDSalon", SqlDbType.Int).Value = pPedido.IDSalon;
				cmd.Parameters.Add("@HoraInicio", SqlDbType.VarChar, 5).Value = pPedido.HoraInicio;
				cmd.Parameters.Add("@HoraFin", SqlDbType.VarChar, 5).Value = pPedido.HoraFin;
				cmd.Parameters.Add("@NumeroInvitado", SqlDbType.Int).Value = pPedido.NumeroInvitado;
				cmd.Parameters.Add("@Observacion", SqlDbType.VarChar).Value = pPedido.Observacion; 
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pPedido.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue; 
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value); 
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

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
		 
		public BERetornoTran PedidoRestauranteGuardar(BEPedido pPedido)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEPedido BEPedidoRetorno = new BEPedido();
			int vSucursal = pPedido.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//Pedido------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.PedidoRestauranteGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{ 
				cmd.Parameters.Add("@IDPedido", SqlDbType.Int, 10).Value = pPedido.IDPedido;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pPedido.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pPedido.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pPedido.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pPedido.TipoCambio; 
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pPedido.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pPedido.CalculoIGV; 
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pPedido.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pPedido.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pPedido.TotalOperacionGravada;
			    cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pPedido.TotalIGV; 
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pPedido.TotalDescuentos; 
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pPedido.TotalVenta; 
				cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 50).Value = pPedido.Proceso; 
			    cmd.Parameters.Add("@IDMesa", SqlDbType.Int).Value = pPedido.IDMesa;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pPedido.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@IDPedidoFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDPedidoFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}
				else
				{
					BEPedidoRetorno.IDPedido = Int32.Parse(cmd.Parameters["@IDPedidoFinal"].Value.ToString());
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

	}
}