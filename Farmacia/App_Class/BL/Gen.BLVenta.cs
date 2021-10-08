
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL
{
	public class BLVenta : BLBase
	{
		public BERetornoTran GuardarVenta(BEVenta pVenta, ArrayList pVentaDetalle, ArrayList pVentaFormaPago, ArrayList pVentaDetalleLote)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEVenta BEVentaRetorno = new BEVenta();
			int vSucursal = pVenta.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.VentaGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{

				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 10).Value = pVenta.IDVenta;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pVenta.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pVenta.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pVenta.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pVenta.TipoCambio;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pVenta.IDTipoComprobante;
				cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 30).Value = pVenta.SerieNumero;
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pVenta.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pVenta.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pVenta.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pVenta.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pVenta.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pVenta.TotalOperacionGravada;
				cmd.Parameters.Add("@TotalOperacionExonerada", SqlDbType.Decimal).Value = pVenta.TotalOperacionExonerada;
				cmd.Parameters.Add("@TotalOperacionInafecta", SqlDbType.Decimal).Value = pVenta.TotalOperacionInafecta;
				cmd.Parameters.Add("@TotalOperacionGratuita", SqlDbType.Decimal).Value = pVenta.TotalOperacionGratuita;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pVenta.TotalIGV;
				cmd.Parameters.Add("@TotalISC", SqlDbType.Decimal).Value = pVenta.TotalISC;
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pVenta.TotalDescuentos;
				cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = pVenta.TotalOtrosTributos;
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pVenta.TotalVenta;
				cmd.Parameters.Add("@Migrado", SqlDbType.VarChar, 1).Value = pVenta.Migrado;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pVenta.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@IDVentaFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDVentaFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}
				else
				{
					BEVentaRetorno.IDVenta = Int32.Parse(cmd.Parameters["@IDVentaFinal"].Value.ToString());
				}

				//VENTA DETALLE-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaDetalleGuardar";

				BEVentaDetalle oBEVentDetalle = new BEVentaDetalle();
				for (Int32 i = 0; i < pVentaDetalle.Count; i++)
				{
					oBEVentDetalle = (BEVentaDetalle)pVentaDetalle[i];
					cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = vSucursal;
					cmd.Parameters.Add("@IDVentaDetalle", SqlDbType.Int, 50).Value = oBEVentDetalle.IDVentaDetalle;
					cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEVentDetalle.IDProducto;
					cmd.Parameters.Add("@DetalleProducto", SqlDbType.VarChar, 200).Value = oBEVentDetalle.Nombre;
					cmd.Parameters.Add("@Item", SqlDbType.Int, 50).Value = oBEVentDetalle.Item;
					cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int, 50).Value = oBEVentDetalle.IDUnidadMedida;
					cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal, 50).Value = oBEVentDetalle.Cantidad;
					cmd.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitario;
					cmd.Parameters.Add("@PrecioUnitarioConIgv", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitarioConIgv;
					cmd.Parameters.Add("@IDTipoPrecio", SqlDbType.VarChar, 10).Value = oBEVentDetalle.IDTipoPrecio;
					cmd.Parameters.Add("@IDTipoImpuesto", SqlDbType.VarChar, 10).Value = oBEVentDetalle.IDTipoImpuesto;
					cmd.Parameters.Add("@Igv", SqlDbType.Decimal).Value = oBEVentDetalle.Igv;
					cmd.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = oBEVentDetalle.Descuento;
					cmd.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = oBEVentDetalle.SubTotal;
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

				//FORMA PAGO-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaFormaPagoGuardar";

				BEVentaFormaPago oBEFormaPago = new BEVentaFormaPago();
				for (Int32 i = 0; i < pVentaFormaPago.Count; i++)
				{
					oBEFormaPago = (BEVentaFormaPago)pVentaFormaPago[i];
					cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta;
					cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int, 50).Value = oBEFormaPago.IDFormaPago;
					cmd.Parameters.Add("@IDTarjetaCredito", SqlDbType.Int, 50).Value = oBEFormaPago.IDTarjetaCredito;
					cmd.Parameters.Add("@NumeroOperacion", SqlDbType.VarChar, 10).Value = oBEFormaPago.NumeroOperacion;
					cmd.Parameters.Add("@MontoPagado", SqlDbType.Decimal).Value = oBEFormaPago.MontoPagado;
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEFormaPago.IDUsuario;
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


				//LOTE-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaDetalleLoteGuardar";

				BEVentaDetalleLote oBEDetLote = new BEVentaDetalleLote();
				for (Int32 i = 0; i < pVentaDetalleLote.Count; i++)
				{
					oBEDetLote = (BEVentaDetalleLote)pVentaDetalleLote[i];
					cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEDetLote.IDProducto;
					cmd.Parameters.Add("@IDLote", SqlDbType.Int, 50).Value = oBEDetLote.IDLote;
					cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = oBEDetLote.IDSucursal;
					cmd.Parameters.Add("@CantidadLote", SqlDbType.Int).Value = oBEDetLote.CantidadLote;
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEDetLote.IDUsuario;
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

		public BERetornoTran ActualizarVenta(BEVenta pVenta, ArrayList pVentaDetalle, ArrayList pVentaFormaPago)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEVenta BEVentaRetorno = new BEVenta();
			int vSucursal = pVenta.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.VentaActualizar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{

				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 10).Value = pVenta.IDVenta;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pVenta.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pVenta.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pVenta.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pVenta.TipoCambio;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pVenta.IDTipoComprobante;
				cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 30).Value = pVenta.SerieNumero;
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pVenta.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pVenta.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pVenta.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pVenta.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pVenta.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pVenta.TotalOperacionGravada;
				cmd.Parameters.Add("@TotalOperacionExonerada", SqlDbType.Decimal).Value = pVenta.TotalOperacionExonerada;
				cmd.Parameters.Add("@TotalOperacionInafecta", SqlDbType.Decimal).Value = pVenta.TotalOperacionInafecta;
				cmd.Parameters.Add("@TotalOperacionGratuita", SqlDbType.Decimal).Value = pVenta.TotalOperacionGratuita;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pVenta.TotalIGV;
				cmd.Parameters.Add("@TotalISC", SqlDbType.Decimal).Value = pVenta.TotalISC;
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pVenta.TotalDescuentos;
				cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = pVenta.TotalOtrosTributos;
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pVenta.TotalVenta;
				cmd.Parameters.Add("@Migrado", SqlDbType.VarChar, 1).Value = pVenta.Migrado;
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
				cmd.CommandText = "gen.VentaDetalleGuardar";

				BEVentaDetalle oBEVentDetalle = new BEVentaDetalle();
				for (Int32 i = 0; i < pVentaDetalle.Count; i++)
				{
					oBEVentDetalle = (BEVentaDetalle)pVentaDetalle[i];
					cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = vSucursal;
					cmd.Parameters.Add("@IDVentaDetalle", SqlDbType.Int, 50).Value = oBEVentDetalle.IDVentaDetalle;
					cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = pVenta.IDVenta;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEVentDetalle.IDProducto;
					cmd.Parameters.Add("@DetalleProducto", SqlDbType.VarChar, 200).Value = oBEVentDetalle.ProductoDetalle;
					cmd.Parameters.Add("@Item", SqlDbType.Int, 50).Value = oBEVentDetalle.Item;
					cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int, 50).Value = oBEVentDetalle.IDUnidadMedida;
					cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal, 50).Value = oBEVentDetalle.Cantidad;
					cmd.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitario;
					cmd.Parameters.Add("@PrecioUnitarioConIgv", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitarioConIgv;
					cmd.Parameters.Add("@IDTipoPrecio", SqlDbType.VarChar, 10).Value = oBEVentDetalle.IDTipoPrecio;
					cmd.Parameters.Add("@IDTipoImpuesto", SqlDbType.VarChar, 10).Value = oBEVentDetalle.IDTipoImpuesto;
					cmd.Parameters.Add("@Igv", SqlDbType.Decimal).Value = oBEVentDetalle.Igv;
					cmd.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = oBEVentDetalle.Descuento;
					cmd.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = oBEVentDetalle.ImporteVenta;

					cmd.Parameters.Add("@ControlaLote", SqlDbType.Bit).Value = oBEVentDetalle.ControlaLote;
					cmd.Parameters.Add("@IDLote", SqlDbType.Int, 50).Value = oBEVentDetalle.IDLote;
					cmd.Parameters.Add("@CantidadLote", SqlDbType.Int, 50).Value = oBEVentDetalle.CantidadLote;

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

				//FORMA PAGO-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaFormaPagoGuardar";

				BEVentaFormaPago oBEFormaPago = new BEVentaFormaPago();
				for (Int32 i = 0; i < pVentaFormaPago.Count; i++)
				{
					oBEFormaPago = (BEVentaFormaPago)pVentaFormaPago[i];
					cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = pVenta.IDVenta;
					cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int, 50).Value = oBEFormaPago.IDFormaPago;
					cmd.Parameters.Add("@IDTarjetaCredito", SqlDbType.Int, 50).Value = oBEFormaPago.IDTarjetaCredito;
					cmd.Parameters.Add("@NumeroOperacion", SqlDbType.VarChar, 10).Value = oBEFormaPago.NumeroOperacion;
					cmd.Parameters.Add("@MontoPagado", SqlDbType.Decimal).Value = oBEFormaPago.MontoPagado;
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEFormaPago.IDUsuario;
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

		public BERetornoTran VentaAnular(Int32 pIDVenta, String pMotivoAnulacion, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaAnular");
			cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 10).Value = pIDVenta;
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

		public BERetornoTran VentaContratoFinalGuardar(BEVenta oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaContratoFinalGuardar");
			cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 10).Value = oBE.IDVenta;
			cmd.Parameters.Add("@IDContrato", SqlDbType.Int, 10).Value = oBE.IDContrato;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = oBE.IDCliente;
			cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int, 10).Value = oBE.IDTipoComprobante;
			cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = oBE.FechaVenta;
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int, 10).Value = oBE.IDEmpresa;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = oBE.IDSucursal;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 10).Value = oBE.IDUsuario;
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

		public BEVenta VentaSeleccionar(Int32 pIDVenta)
		{
			SqlCommand cmd = ConexionCmd("gen.VentaSeleccionar");
			BEVenta oBE = new BEVenta();
			cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 10).Value = pIDVenta;
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
					oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
					oBE.Anulado = rd.GetBoolean(rd.GetOrdinal("Anulado"));
					oBE.MotivoAnulacion = rd.GetString(rd.GetOrdinal("MotivoAnulacion"));
					oBE.ImpresionVenta = rd.GetString(rd.GetOrdinal("ImpresionVenta"));
					oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));

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

		public IList VentasxClienteListar(Int32 pIDCliente)
		{
			SqlCommand cmd = ConexionCmd("gen.VentasxClienteListar");
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pIDCliente;
			BEVenta oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEVenta();
					oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
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

		public IList VentasListar(BEVenta pBE)
		{
			SqlCommand cmd = ConexionCmd("gen.VentasListar");
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
			BEVenta oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEVenta();
					oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
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
					oBE.IDUsuarioReg = rd.GetInt32(rd.GetOrdinal("IDUsuarioReg"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.NumeroDocumentoCliente = rd.GetString(rd.GetOrdinal("NumeroDocumentoCliente"));
					oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
					oBE.Usuario = rd.GetString(rd.GetOrdinal("Usuario"));
					oBE.FechaReg = rd.GetDateTime(rd.GetOrdinal("FechaReg"));
					oBE.EstadoSunat = rd.GetString(rd.GetOrdinal("EstadoSunat"));
					oBE.EstadoCobranza = rd.GetString(rd.GetOrdinal("EstadoCobranza"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
					oBE.Anulado = rd.GetBoolean(rd.GetOrdinal("Anulado"));
					oBE.MotivoAnulacion = rd.GetString(rd.GetOrdinal("MotivoAnulacion"));
					oBE.SerieNumeroAfectado = rd.GetString(rd.GetOrdinal("SerieNumeroAfectado"));


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
		 
		public IList VentasMigrarListar(BEVenta pBE)
		{
			SqlCommand cmd = ConexionCmd("gen.VentasMigrarListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pBE.Filtro;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal; 
			cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int).Value = pBE.IDTipoComprobante;
			cmd.Parameters.Add("@IDEstado", SqlDbType.Int).Value = pBE.IDEstado; 
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pBE.FechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pBE.FechaFin; 
			BEVenta oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEVenta();
					oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
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
					oBE.IDUsuarioReg = rd.GetInt32(rd.GetOrdinal("IDUsuarioReg"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.NumeroDocumentoCliente = rd.GetString(rd.GetOrdinal("NumeroDocumentoCliente"));
					oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
					oBE.Usuario = rd.GetString(rd.GetOrdinal("Usuario"));
					oBE.FechaReg = rd.GetDateTime(rd.GetOrdinal("FechaReg"));
					oBE.EstadoSunat = rd.GetString(rd.GetOrdinal("EstadoSunat"));
					oBE.EstadoCobranza = rd.GetString(rd.GetOrdinal("EstadoCobranza"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
					oBE.Anulado = rd.GetBoolean(rd.GetOrdinal("Anulado"));
					oBE.MotivoAnulacion = rd.GetString(rd.GetOrdinal("MotivoAnulacion"));
					oBE.SerieNumeroAfectado = rd.GetString(rd.GetOrdinal("SerieNumeroAfectado"));


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



		public BERetornoTran GuardarVentaV2(BEVenta pVenta, ArrayList pVentaDetalle, BEVentaFormaPago pVentaFormaPago, ArrayList pVentaDetalleLote)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEVenta BEVentaRetorno = new BEVenta();
			int vSucursal = pVenta.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.VentaGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{

				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 10).Value = pVenta.IDVenta;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pVenta.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pVenta.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pVenta.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pVenta.TipoCambio;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pVenta.IDTipoComprobante;
				cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 30).Value = pVenta.SerieNumero;
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pVenta.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pVenta.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pVenta.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pVenta.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pVenta.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pVenta.TotalOperacionGravada;
				cmd.Parameters.Add("@TotalOperacionExonerada", SqlDbType.Decimal).Value = pVenta.TotalOperacionExonerada;
				cmd.Parameters.Add("@TotalOperacionInafecta", SqlDbType.Decimal).Value = pVenta.TotalOperacionInafecta;
				cmd.Parameters.Add("@TotalOperacionGratuita", SqlDbType.Decimal).Value = pVenta.TotalOperacionGratuita;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pVenta.TotalIGV;
				cmd.Parameters.Add("@TotalISC", SqlDbType.Decimal).Value = pVenta.TotalISC;
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pVenta.TotalDescuentos;
				cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = pVenta.TotalOtrosTributos;
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pVenta.TotalVenta;
				cmd.Parameters.Add("@Migrado", SqlDbType.VarChar, 1).Value = pVenta.Migrado;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pVenta.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@IDVentaFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDVentaFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}
				else
				{
					BEVentaRetorno.IDVenta = Int32.Parse(cmd.Parameters["@IDVentaFinal"].Value.ToString());
				}

				//VENTA DETALLE-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaDetalleGuardar";

				BEVentaDetalle oBEVentDetalle = new BEVentaDetalle();
				for (Int32 i = 0; i < pVentaDetalle.Count; i++)
				{
					oBEVentDetalle = (BEVentaDetalle)pVentaDetalle[i];
					cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = vSucursal;
					cmd.Parameters.Add("@IDVentaDetalle", SqlDbType.Int, 50).Value = oBEVentDetalle.IDVentaDetalle;
					cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEVentDetalle.IDProducto;
					cmd.Parameters.Add("@DetalleProducto", SqlDbType.VarChar, 200).Value = oBEVentDetalle.Nombre;
					cmd.Parameters.Add("@Item", SqlDbType.Int, 50).Value = oBEVentDetalle.Item;
					cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int, 50).Value = oBEVentDetalle.IDUnidadMedida;
					cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal, 50).Value = oBEVentDetalle.Cantidad;
					cmd.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitario;
					cmd.Parameters.Add("@PrecioUnitarioConIgv", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitarioConIgv;
					cmd.Parameters.Add("@IDTipoPrecio", SqlDbType.VarChar, 10).Value = oBEVentDetalle.IDTipoPrecio;
					cmd.Parameters.Add("@IDTipoImpuesto", SqlDbType.VarChar, 10).Value = oBEVentDetalle.IDTipoImpuesto;
					cmd.Parameters.Add("@Igv", SqlDbType.Decimal).Value = oBEVentDetalle.Igv;
					cmd.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = oBEVentDetalle.Descuento;
					cmd.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = oBEVentDetalle.SubTotal;
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

				//FORMA PAGO-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaFormaPagoGuardar";
				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta;
				cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int, 50).Value = pVentaFormaPago.IDFormaPago;
				cmd.Parameters.Add("@IDTarjetaCredito", SqlDbType.Int, 50).Value = pVentaFormaPago.IDTarjetaCredito == 0 ? (Object)DBNull.Value : pVentaFormaPago.IDTarjetaCredito;
				cmd.Parameters.Add("@NumeroOperacion", SqlDbType.VarChar, 10).Value = pVentaFormaPago.NumeroOperacion;
				cmd.Parameters.Add("@MontoPagado", SqlDbType.Decimal).Value = pVentaFormaPago.MontoPagado;

				cmd.Parameters.Add("@Efectivo", SqlDbType.Decimal).Value = pVentaFormaPago.Efectivo;
				cmd.Parameters.Add("@Tarjeta", SqlDbType.Decimal).Value = pVentaFormaPago.Tarjeta;
				cmd.Parameters.Add("@Transferencia", SqlDbType.Decimal).Value = pVentaFormaPago.Transferencia;
				cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 500).Value = pVentaFormaPago.Referencia;

				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = pVentaFormaPago.IDUsuario;
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



				//LOTE-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaDetalleLoteGuardar";

				BEVentaDetalleLote oBEDetLote = new BEVentaDetalleLote();
				for (Int32 i = 0; i < pVentaDetalleLote.Count; i++)
				{
					oBEDetLote = (BEVentaDetalleLote)pVentaDetalleLote[i];
					cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEDetLote.IDProducto;
					cmd.Parameters.Add("@IDLote", SqlDbType.Int, 50).Value = oBEDetLote.IDLote;
					cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = oBEDetLote.IDSucursal;
					cmd.Parameters.Add("@CantidadLote", SqlDbType.Int).Value = oBEDetLote.CantidadLote;
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEDetLote.IDUsuario;
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

		public BERetornoTran ActualizarVentaV2(BEVenta pVenta, ArrayList pVentaDetalle, BEVentaFormaPago pVentaFormaPago, ArrayList pVentaDetalleLote)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEVenta BEVentaRetorno = new BEVenta();
			int vSucursal = pVenta.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.VentaActualizar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{

				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 10).Value = pVenta.IDVenta;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pVenta.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pVenta.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pVenta.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pVenta.TipoCambio;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pVenta.IDTipoComprobante;
				cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 30).Value = pVenta.SerieNumero;
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pVenta.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pVenta.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pVenta.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pVenta.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pVenta.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pVenta.TotalOperacionGravada;
				cmd.Parameters.Add("@TotalOperacionExonerada", SqlDbType.Decimal).Value = pVenta.TotalOperacionExonerada;
				cmd.Parameters.Add("@TotalOperacionInafecta", SqlDbType.Decimal).Value = pVenta.TotalOperacionInafecta;
				cmd.Parameters.Add("@TotalOperacionGratuita", SqlDbType.Decimal).Value = pVenta.TotalOperacionGratuita;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pVenta.TotalIGV;
				cmd.Parameters.Add("@TotalISC", SqlDbType.Decimal).Value = pVenta.TotalISC;
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pVenta.TotalDescuentos;
				cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = pVenta.TotalOtrosTributos;
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pVenta.TotalVenta;
				cmd.Parameters.Add("@Migrado", SqlDbType.VarChar, 1).Value = pVenta.Migrado;
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
				cmd.CommandText = "gen.VentaDetalleGuardar";

				BEVentaDetalle oBEVentDetalle = new BEVentaDetalle();
				for (Int32 i = 0; i < pVentaDetalle.Count; i++)
				{
					oBEVentDetalle = (BEVentaDetalle)pVentaDetalle[i];
					cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = vSucursal;
					cmd.Parameters.Add("@IDVentaDetalle", SqlDbType.Int, 50).Value = oBEVentDetalle.IDVentaDetalle;
					cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = pVenta.IDVenta;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEVentDetalle.IDProducto;
					cmd.Parameters.Add("@DetalleProducto", SqlDbType.VarChar, 200).Value = oBEVentDetalle.ProductoDetalle;
					cmd.Parameters.Add("@Item", SqlDbType.Int, 50).Value = oBEVentDetalle.Item;
					cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int, 50).Value = oBEVentDetalle.IDUnidadMedida;
					cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal, 50).Value = oBEVentDetalle.Cantidad;
					cmd.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitario;
					cmd.Parameters.Add("@PrecioUnitarioConIgv", SqlDbType.Decimal).Value = oBEVentDetalle.PrecioUnitarioConIgv;
					cmd.Parameters.Add("@IDTipoPrecio", SqlDbType.VarChar, 10).Value = oBEVentDetalle.IDTipoPrecio;
					cmd.Parameters.Add("@IDTipoImpuesto", SqlDbType.VarChar, 10).Value = oBEVentDetalle.IDTipoImpuesto;
					cmd.Parameters.Add("@Igv", SqlDbType.Decimal).Value = oBEVentDetalle.Igv;
					cmd.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = oBEVentDetalle.Descuento;
					cmd.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = oBEVentDetalle.ImporteVenta;
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

				//FORMA PAGO-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaFormaPagoGuardar";
				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = pVenta.IDVenta;
				cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int, 50).Value = pVentaFormaPago.IDFormaPago;
				cmd.Parameters.Add("@IDTarjetaCredito", SqlDbType.Int, 50).Value = pVentaFormaPago.IDTarjetaCredito == 0 ? (Object)DBNull.Value : pVentaFormaPago.IDTarjetaCredito;
				cmd.Parameters.Add("@NumeroOperacion", SqlDbType.VarChar, 10).Value = pVentaFormaPago.NumeroOperacion;
				cmd.Parameters.Add("@MontoPagado", SqlDbType.Decimal).Value = pVentaFormaPago.MontoPagado;

				cmd.Parameters.Add("@Efectivo", SqlDbType.Decimal).Value = pVentaFormaPago.Efectivo;
				cmd.Parameters.Add("@Tarjeta", SqlDbType.Decimal).Value = pVentaFormaPago.Tarjeta;
				cmd.Parameters.Add("@Transferencia", SqlDbType.Decimal).Value = pVentaFormaPago.Transferencia;
				cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 500).Value = pVentaFormaPago.Referencia;

				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = pVentaFormaPago.IDUsuario;
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



				//LOTE-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaDetalleLoteGuardar";

				BEVentaDetalleLote oBEDetLote = new BEVentaDetalleLote();
				for (Int32 i = 0; i < pVentaDetalleLote.Count; i++)
				{
					oBEDetLote = (BEVentaDetalleLote)pVentaDetalleLote[i];
					cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = pVenta.IDVenta;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEDetLote.IDProducto;
					cmd.Parameters.Add("@IDLote", SqlDbType.Int, 50).Value = oBEDetLote.IDLote;
					cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = oBEDetLote.IDSucursal;
					cmd.Parameters.Add("@CantidadLote", SqlDbType.Int).Value = oBEDetLote.CantidadLote;
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEDetLote.IDUsuario;
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

		public BERetornoTran VentaRapidaGuardar(BEVenta pVenta, BEVentaFormaPago pVentaFormaPago, BEVentaRecetaMedica pVentaRecetaMedica)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEVenta BEVentaRetorno = new BEVenta();
			int vSucursal = pVenta.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.VentaRapidaGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{

				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 10).Value = pVenta.IDVenta;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pVenta.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pVenta.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pVenta.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pVenta.TipoCambio;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pVenta.IDTipoComprobante;
				cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 30).Value = pVenta.SerieNumero;
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pVenta.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pVenta.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pVenta.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pVenta.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pVenta.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pVenta.TotalOperacionGravada;
				cmd.Parameters.Add("@TotalOperacionExonerada", SqlDbType.Decimal).Value = pVenta.TotalOperacionExonerada;
				cmd.Parameters.Add("@TotalOperacionInafecta", SqlDbType.Decimal).Value = pVenta.TotalOperacionInafecta;
				cmd.Parameters.Add("@TotalOperacionGratuita", SqlDbType.Decimal).Value = pVenta.TotalOperacionGratuita;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pVenta.TotalIGV;
				cmd.Parameters.Add("@TotalISC", SqlDbType.Decimal).Value = pVenta.TotalISC;
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pVenta.TotalDescuentos;
				cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = pVenta.TotalOtrosTributos;
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pVenta.TotalVenta;
				cmd.Parameters.Add("@Migrado", SqlDbType.VarChar, 1).Value = pVenta.Migrado;
				cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 50).Value = pVenta.Proceso;
				cmd.Parameters.Add("@IDVentaRelacionado", SqlDbType.Int).Value = pVenta.IDVentaRelacionado == 0 ? (Object)DBNull.Value : pVenta.IDVentaRelacionado;
				cmd.Parameters.Add("@IDTipoMotivo", SqlDbType.Int).Value = pVenta.IDTipoMotivo == 0 ? (Object)DBNull.Value : pVenta.IDTipoMotivo;
				cmd.Parameters.Add("@MotivoNota", SqlDbType.VarChar, 500).Value = pVenta.MotivoNota;
				cmd.Parameters.Add("@IDMedioPago", SqlDbType.Int).Value = pVenta.IDMedioPago == 0 ? (Object)DBNull.Value : pVenta.IDMedioPago;
				cmd.Parameters.Add("@Vuelto", SqlDbType.Decimal).Value = pVenta.Vuelto;
				cmd.Parameters.Add("@Observacion", SqlDbType.VarChar).Value = pVenta.Observacion;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pVenta.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@IDVentaFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDVentaFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}
				else
				{
					BEVentaRetorno.IDVenta = Int32.Parse(cmd.Parameters["@IDVentaFinal"].Value.ToString());
				}

				//ACTUALIZAR MOVIMIENTO ALMACEN----------------------------------------------------------				
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaActualizarMovimientoGuardar";
				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = pVenta.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;

				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);

				if (BERetorno.Retorno == "-1")
				{
					throw new Exception(BERetorno.ErrorMensaje);
				}


				//FORMA PAGO-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaFormaPagoGuardar";
				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta; 
				cmd.Parameters.Add("@Efectivo", SqlDbType.Decimal).Value = pVentaFormaPago.Efectivo;
				cmd.Parameters.Add("@Tarjeta", SqlDbType.Decimal).Value = pVentaFormaPago.Tarjeta;
				cmd.Parameters.Add("@Transferencia", SqlDbType.Decimal).Value = pVentaFormaPago.Transferencia;
				cmd.Parameters.Add("@Credito", SqlDbType.Decimal).Value = pVentaFormaPago.Credito;
				cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 500).Value = pVentaFormaPago.Referencia;
				cmd.Parameters.Add("@DiaCredito", SqlDbType.Int).Value = pVentaFormaPago.DiaCredito;
				cmd.Parameters.Add("@FechaVencimiento", SqlDbType.DateTime).Value = pVentaFormaPago.FechaVencimiento.ToShortDateString() == "01/01/1900" ? (Object) DBNull.Value: pVentaFormaPago.FechaVencimiento; 
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = pVentaFormaPago.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				 
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);

				if (BERetorno.Retorno == "-1")
				{
					throw new Exception(BERetorno.ErrorMensaje);
				}
				//RECETA MEDICA-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				if (pVentaRecetaMedica.IDVentaRecetaMedica == 0)
				{ 
					BERetorno.Retorno = "-1";
					cmd.CommandText = "gen.VentaRecetaMedicaGuardar";
					cmd.Parameters.Add("@IDVentaRecetaMedica", SqlDbType.Int).Value = pVentaRecetaMedica.IDVentaRecetaMedica;
					cmd.Parameters.Add("@IDVenta", SqlDbType.Int).Value = BEVentaRetorno.IDVenta;
					cmd.Parameters.Add("@FolioReceta", SqlDbType.VarChar, 20).Value = pVentaRecetaMedica.FolioReceta;
					cmd.Parameters.Add("@RecetaRetenida", SqlDbType.Bit).Value = pVentaRecetaMedica.RecetaRetenida;
					cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 20).Value = pVentaRecetaMedica.NumeroDocumento;
					cmd.Parameters.Add("@NombresCompleto", SqlDbType.VarChar, 200).Value = pVentaRecetaMedica.NombresCompleto;
					cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 800).Value = pVentaRecetaMedica.Direccion;
					cmd.Parameters.Add("@CMP", SqlDbType.VarChar, 10).Value = pVentaRecetaMedica.CMP;
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pVentaRecetaMedica.IDUsuario;
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


				//POST VENTA-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.PostVentaGuardar";
				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta; 
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = pVenta.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;

				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);

				if (BERetorno.Retorno == "-1")
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

		public BERetornoTran VentaValidarCaja(Int32 pIDUsuario, Int32 pIDSucursal)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaValidarCaja");
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

		public BERetornoTran VentasMigrarInsertar(Int32 pIDTipoDocumento, String pFechaInicio, String pFechaFin, Int32 pIDUsuario, Int32 pIDEmpresa, Int32 pIDSucursal)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentasMigrarInsertar");
			cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = pIDTipoDocumento;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
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
		 
		public BERetornoTran VentaRecetaAplica(Int32 pIDUsuario,String pProceso)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.VentaRecetaAplica");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 10).Value = pIDUsuario;
			cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 100).Value = pProceso;
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
		 
		public BERetornoTran CreditoDebitoMigrarInsertar(Int32 pIDTipoDocumento, String pFechaInicio, String pFechaFin, Int32 pIDUsuario, Int32 pIDEmpresa, Int32 pIDSucursal)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CreditoDebitoMigrarInsertar");
			cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = pIDTipoDocumento;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
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


		public BERetornoTran VentaPedidoGuardar(BEVenta pVenta, BEVentaFormaPago pVentaFormaPago)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEVenta BEVentaRetorno = new BEVenta();
			int vSucursal = pVenta.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.VentaPedidoGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{ 
				cmd.Parameters.Add("@IDPedido", SqlDbType.Int, 10).Value = pVenta.IDPedido;
				cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int, 10).Value = pVenta.IDEmpresa;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pVenta.IDSucursal; 
				cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int, 10).Value = pVenta.IDTipoComprobante; 
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pVenta.FechaVenta; 
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pVenta.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@IDVentaFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDVentaFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}
				else
				{
					BEVentaRetorno.IDVenta = Int32.Parse(cmd.Parameters["@IDVentaFinal"].Value.ToString());
				}

				//FORMA PAGO-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.VentaFormaPagoGuardar";
				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta;
				cmd.Parameters.Add("@Efectivo", SqlDbType.Decimal).Value = pVentaFormaPago.Efectivo;
				cmd.Parameters.Add("@Tarjeta", SqlDbType.Decimal).Value = pVentaFormaPago.Tarjeta;
				cmd.Parameters.Add("@Transferencia", SqlDbType.Decimal).Value = pVentaFormaPago.Transferencia;
				cmd.Parameters.Add("@Credito", SqlDbType.Decimal).Value = pVentaFormaPago.Credito;
				cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 500).Value = pVentaFormaPago.Referencia;
				cmd.Parameters.Add("@DiaCredito", SqlDbType.Int).Value = pVentaFormaPago.DiaCredito;
				cmd.Parameters.Add("@FechaVencimiento", SqlDbType.DateTime).Value = pVentaFormaPago.FechaVencimiento.ToShortDateString() == "01/01/1900" ? (Object)DBNull.Value : pVentaFormaPago.FechaVencimiento;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = pVentaFormaPago.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;

				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);

				if (BERetorno.Retorno == "-1")
				{
					throw new Exception(BERetorno.ErrorMensaje);
				}
		 
				//POST VENTA-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.PostVentaGuardar";
				cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 50).Value = BEVentaRetorno.IDVenta;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = pVenta.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;

				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);

				if (BERetorno.Retorno == "-1")
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

	}
}