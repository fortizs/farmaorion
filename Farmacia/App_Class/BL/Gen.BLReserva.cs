using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL
{
    public class BLReserva : BLBase
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

		public BEReserva ReservaSeleccionar(Int32 pIDReserva)
		{
			SqlCommand cmd = ConexionCmd("gen.ReservaSeleccionar");
            BEReserva oBE = new BEReserva();
			cmd.Parameters.Add("@IDReserva", SqlDbType.Int, 10).Value = pIDReserva;
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
					oBE.CodigoQR = rd.GetString(rd.GetOrdinal("CodigoQR"));
					oBE.IDReserva = rd.GetInt32(rd.GetOrdinal("IDReserva"));
					oBE.Anulado = rd.GetBoolean(rd.GetOrdinal("Anulado"));
					oBE.MotivoAnulacion = rd.GetString(rd.GetOrdinal("MotivoAnulacion"));
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

		public IList ReservasListar(BEReserva pBE)
		{
			SqlCommand cmd = ConexionCmd("gen.ReservasListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pBE.Filtro;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pBE.IDCliente;
			cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int).Value = pBE.IDTipoComprobante;
			cmd.Parameters.Add("@IDEstado", SqlDbType.Int).Value = pBE.IDEstado;
			cmd.Parameters.Add("@IDEstadoCobranza", SqlDbType.Int).Value = pBE.IDEstadoCobranza;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pBE.FechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pBE.FechaFin;
            BEReserva oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEReserva();
					oBE.IDReserva = rd.GetInt32(rd.GetOrdinal("IDReserva"));
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
				cmd.Parameters.Add("@IDTarjetaCredito", SqlDbType.Int, 50).Value = pVentaFormaPago.IDTarjetaCredito == 0 ? (Object) DBNull.Value : pVentaFormaPago.IDTarjetaCredito;
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
		 
		public BERetornoTran ActualizarReserva(BEReserva pReserva, ArrayList pReservaDetalle, BEReservaFormaPago pReservaFormaPago, ArrayList pReservaDetalleLote)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEReserva BEReservaRetorno = new BEReserva();
			int vSucursal = pReserva.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.ReservaActualizar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{

				cmd.Parameters.Add("@IDReserva", SqlDbType.Int, 10).Value = pReserva.IDReserva;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pReserva.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pReserva.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pReserva.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pReserva.TipoCambio;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pReserva.IDTipoComprobante;
				cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 30).Value = pReserva.SerieNumero;
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pReserva.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pReserva.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pReserva.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pReserva.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pReserva.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pReserva.TotalOperacionGravada;
				cmd.Parameters.Add("@TotalOperacionExonerada", SqlDbType.Decimal).Value = pReserva.TotalOperacionExonerada;
				cmd.Parameters.Add("@TotalOperacionInafecta", SqlDbType.Decimal).Value = pReserva.TotalOperacionInafecta;
				cmd.Parameters.Add("@TotalOperacionGratuita", SqlDbType.Decimal).Value = pReserva.TotalOperacionGratuita;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pReserva.TotalIGV;
				cmd.Parameters.Add("@TotalISC", SqlDbType.Decimal).Value = pReserva.TotalISC;
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pReserva.TotalDescuentos;
				cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = pReserva.TotalOtrosTributos;
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pReserva.TotalVenta;
				cmd.Parameters.Add("@Migrado", SqlDbType.VarChar, 1).Value = pReserva.Migrado;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pReserva.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}

				//RESERVA DETALLE-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.ReservaDetalleGuardar";

				BEReservaDetalle oBEReservDetalle = new BEReservaDetalle();
				for (Int32 i = 0; i < pReservaDetalle.Count; i++)
				{
                    oBEReservDetalle = (BEReservaDetalle)pReservaDetalle[i];
					cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = vSucursal;
					cmd.Parameters.Add("@IDReservaDetalle", SqlDbType.Int, 50).Value = oBEReservDetalle.IDReservaDetalle;
					cmd.Parameters.Add("@IDReserva", SqlDbType.Int, 50).Value = pReserva.IDReserva;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEReservDetalle.IDProducto;
					cmd.Parameters.Add("@DetalleProducto", SqlDbType.VarChar, 200).Value = oBEReservDetalle.ProductoDetalle;
					cmd.Parameters.Add("@Item", SqlDbType.Int, 50).Value = oBEReservDetalle.Item;
					cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int, 50).Value = oBEReservDetalle.IDUnidadMedida;
					cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal, 50).Value = oBEReservDetalle.Cantidad;
					cmd.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = oBEReservDetalle.PrecioUnitario;
					cmd.Parameters.Add("@PrecioUnitarioConIgv", SqlDbType.Decimal).Value = oBEReservDetalle.PrecioUnitarioConIgv;
					cmd.Parameters.Add("@IDTipoPrecio", SqlDbType.VarChar, 10).Value = oBEReservDetalle.IDTipoPrecio;
					cmd.Parameters.Add("@IDTipoImpuesto", SqlDbType.VarChar, 10).Value = oBEReservDetalle.IDTipoImpuesto;
					cmd.Parameters.Add("@Igv", SqlDbType.Decimal).Value = oBEReservDetalle.Igv;
					cmd.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = oBEReservDetalle.Descuento;
					cmd.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = oBEReservDetalle.ImporteVenta; 
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEReservDetalle.IDUsuario;
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
				cmd.CommandText = "gen.ReservaFormaPagoGuardar";
				cmd.Parameters.Add("@IDReserva", SqlDbType.Int, 50).Value = pReserva.IDReserva;
				cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int, 50).Value = pReservaFormaPago.IDFormaPago;
				cmd.Parameters.Add("@IDTarjetaCredito", SqlDbType.Int, 50).Value = pReservaFormaPago.IDTarjetaCredito == 0 ? (Object)DBNull.Value : pReservaFormaPago.IDTarjetaCredito;
				cmd.Parameters.Add("@NumeroOperacion", SqlDbType.VarChar, 10).Value = pReservaFormaPago.NumeroOperacion;
				cmd.Parameters.Add("@MontoPagado", SqlDbType.Decimal).Value = pReservaFormaPago.MontoPagado;

				cmd.Parameters.Add("@Efectivo", SqlDbType.Decimal).Value = pReservaFormaPago.Efectivo;
				cmd.Parameters.Add("@Tarjeta", SqlDbType.Decimal).Value = pReservaFormaPago.Tarjeta;
				cmd.Parameters.Add("@Transferencia", SqlDbType.Decimal).Value = pReservaFormaPago.Transferencia;
				cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 500).Value = pReservaFormaPago.Referencia;

				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = pReservaFormaPago.IDUsuario;
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
				cmd.CommandText = "gen.ReservaDetalleLoteGuardar";

				BEReservaDetalleLote oBEDetLote = new BEReservaDetalleLote();
                for (Int32 i = 0; i < pReservaDetalleLote.Count; i++)
                {
                    oBEDetLote = (BEReservaDetalleLote)pReservaDetalleLote[i];
                    cmd.Parameters.Add("@IDReserva", SqlDbType.Int, 50).Value = pReserva.IDReserva;
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
         
		public BERetornoTran ReservaRapidaGuardar(BEReserva pReserva, BEReservaFormaPago pReservaFormaPago)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEReserva BEReservaRetorno = new BEReserva();
			int vSucursal = pReserva.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//RESERVA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.ReservaRapidaGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{

				cmd.Parameters.Add("@IDReserva", SqlDbType.Int, 10).Value = pReserva.IDReserva;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pReserva.IDSucursal;
				cmd.Parameters.Add("@IDCliente", SqlDbType.Int, 10).Value = pReserva.IDCliente;
				cmd.Parameters.Add("@IDMoneda", SqlDbType.Char, 10).Value = pReserva.IDMoneda;
				cmd.Parameters.Add("@TipoCambio", SqlDbType.Decimal).Value = pReserva.TipoCambio;
				cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pReserva.IDTipoComprobante;
				cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 30).Value = pReserva.SerieNumero;
				cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = pReserva.FechaVenta;
				cmd.Parameters.Add("@CalculoIGV", SqlDbType.Decimal).Value = pReserva.CalculoIGV;
				cmd.Parameters.Add("@CalculoISC", SqlDbType.Decimal).Value = pReserva.CalculoISC;
				cmd.Parameters.Add("@CalculoDetraccion", SqlDbType.Decimal).Value = pReserva.CalculoDetraccion;
				cmd.Parameters.Add("@MontoDetraccion", SqlDbType.Decimal).Value = pReserva.MontoDetraccion;
				cmd.Parameters.Add("@TotalOperacionGravada", SqlDbType.Decimal).Value = pReserva.TotalOperacionGravada;
				cmd.Parameters.Add("@TotalOperacionExonerada", SqlDbType.Decimal).Value = pReserva.TotalOperacionExonerada;
				cmd.Parameters.Add("@TotalOperacionInafecta", SqlDbType.Decimal).Value = pReserva.TotalOperacionInafecta;
				cmd.Parameters.Add("@TotalOperacionGratuita", SqlDbType.Decimal).Value = pReserva.TotalOperacionGratuita;
				cmd.Parameters.Add("@TotalIGV", SqlDbType.Decimal).Value = pReserva.TotalIGV;
				cmd.Parameters.Add("@TotalISC", SqlDbType.Decimal).Value = pReserva.TotalISC;
				cmd.Parameters.Add("@TotalDescuentos", SqlDbType.Decimal).Value = pReserva.TotalDescuentos;
				cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = pReserva.TotalOtrosTributos;
				cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = pReserva.TotalVenta;
				cmd.Parameters.Add("@Migrado", SqlDbType.VarChar, 1).Value = pReserva.Migrado;
				cmd.Parameters.Add("@Proceso", SqlDbType.VarChar, 50).Value = pReserva.Proceso; 
				cmd.Parameters.Add("@IDReservaRelacionado", SqlDbType.Int).Value = pReserva.IDReservaRelacionado;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pReserva.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@IDReservaFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDReservaFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);

				}
				else
				{
                    BEReservaRetorno.IDReserva = Int32.Parse(cmd.Parameters["@IDReservaFinal"].Value.ToString());
				}
				 
				//FORMA PAGO-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.ReservaFormaPagoGuardar";
				cmd.Parameters.Add("@IDReserva", SqlDbType.Int, 50).Value = BEReservaRetorno.IDReserva;
				cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int, 50).Value = pReservaFormaPago.IDFormaPago;
				cmd.Parameters.Add("@IDTarjetaCredito", SqlDbType.Int, 50).Value = pReservaFormaPago.IDTarjetaCredito == 0 ? (Object)DBNull.Value : pReservaFormaPago.IDTarjetaCredito;
				cmd.Parameters.Add("@NumeroOperacion", SqlDbType.VarChar, 10).Value = pReservaFormaPago.NumeroOperacion;
				cmd.Parameters.Add("@MontoPagado", SqlDbType.Decimal).Value = pReservaFormaPago.MontoPagado;

				cmd.Parameters.Add("@Efectivo", SqlDbType.Decimal).Value = pReservaFormaPago.Efectivo;
				cmd.Parameters.Add("@Tarjeta", SqlDbType.Decimal).Value = pReservaFormaPago.Tarjeta;
				cmd.Parameters.Add("@Transferencia", SqlDbType.Decimal).Value = pReservaFormaPago.Transferencia;
				cmd.Parameters.Add("@Referencia", SqlDbType.VarChar, 500).Value = pReservaFormaPago.Referencia;

				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = pReservaFormaPago.IDUsuario;
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