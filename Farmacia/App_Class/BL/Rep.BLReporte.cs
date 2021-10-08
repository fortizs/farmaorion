using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Reportes;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLReporte : BLBase 
	{

		public IList ReporteDetalleIngresos(Int32 pIDSucursal, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("caj.CajaDetalleIngresos");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;

			BEReporte oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEReporte();
					oBE.Codigo = rd.GetInt32(rd.GetOrdinal("Codigo"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
					oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
					oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
					oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
					oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
					oBE.FechaSeparacion = rd.GetString(rd.GetOrdinal("FechaSeparacion"));
					oBE.TipoOperacion = rd.GetString(rd.GetOrdinal("TipoOperacion"));
					oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
					oBE.TotalEfectivo = rd.GetDecimal(rd.GetOrdinal("Efectivo"));
					oBE.TotalCredito = rd.GetDecimal(rd.GetOrdinal("Credito"));
					oBE.TotalTransferencia = rd.GetDecimal(rd.GetOrdinal("Transferencia"));
					oBE.TotalCheque = rd.GetDecimal(rd.GetOrdinal("Cheque"));
					oBE.TotalVisa = rd.GetDecimal(rd.GetOrdinal("TarjetaVisa"));
					oBE.TotalMastercard = rd.GetDecimal(rd.GetOrdinal("TarjetaMastercard"));
					oBE.TotalAExpress = rd.GetDecimal(rd.GetOrdinal("TarjetaAExpress"));
					oBE.Anulado = rd.GetString(rd.GetOrdinal("Anulado"));
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

        public IList ReporteStockProductoxSucursalListar(Int32 pIDCategoria, Int32 pIDMarca, Int32 pIDSucursal, String pFiltro, String pTipoReporte="0")
        {
            SqlCommand cmd = ConexionCmd("gen.ReporteStockProductoxSucursalListar");
			cmd.Parameters.Add("@IDCategoria", SqlDbType.Int).Value = pIDCategoria;
			cmd.Parameters.Add("@IDMarca", SqlDbType.Int).Value = pIDMarca;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
            cmd.Parameters.Add("@TipoReporte", SqlDbType.VarChar, 1).Value = pTipoReporte;

            BEProducto oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEProducto();
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.CodigoBarra = rd.GetString(rd.GetOrdinal("CodigoBarra"));
					oBE.CodigoAlterna = rd.GetString(rd.GetOrdinal("CodigoAlterna"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Localizacion = rd.GetString(rd.GetOrdinal("Localizacion"));
					oBE.UnidadMedidaCompra = rd.GetString(rd.GetOrdinal("UnidadMedidaCompra"));
					oBE.UnidadMedidaVenta = rd.GetString(rd.GetOrdinal("UnidadMedidaVenta"));
					oBE.Factor = rd.GetInt32(rd.GetOrdinal("Factor"));
					oBE.ControlaLote = rd.GetBoolean(rd.GetOrdinal("ControlaLote"));
					oBE.VentaConReceta = rd.GetBoolean(rd.GetOrdinal("VentaConReceta"));
					oBE.Laboratorio = rd.GetString(rd.GetOrdinal("Laboratorio"));
					oBE.Categoria = rd.GetString(rd.GetOrdinal("Categoria"));
					oBE.StockMinimo = rd.GetDecimal(rd.GetOrdinal("StockMinimo"));
					oBE.StockActual = rd.GetDecimal(rd.GetOrdinal("StockActual"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.PrecioCosto = rd.GetDecimal(rd.GetOrdinal("PrecioCosto"));
					oBE.PrecioCostoTotalSinIgv = rd.GetDecimal(rd.GetOrdinal("PrecioCostoTotalSinIgv"));
					oBE.PrecioCostoUnidadSinIgv = rd.GetDecimal(rd.GetOrdinal("PrecioCostoUnidadSinIgv"));
					oBE.PrecioCostoUnidadConIgv = rd.GetDecimal(rd.GetOrdinal("PrecioCostoUnidadConIgv"));
					oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
					oBE.MargenUtilidad = rd.GetDecimal(rd.GetOrdinal("MargenUtilidad"));
					oBE.Valorizado = rd.GetDecimal(rd.GetOrdinal("Valorizado"));


					
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
         
        public IList VentasResumidasPorSucursal(Int32 pIDSucursal, Int32 pIDColaborador, Int32 pIDRespPedido, String pFechaInicio, String pFechaFin, Int32 pIDEstadoVenta, Int32 pIDEstadoCobranza, String pFiltro)
        {
            SqlCommand cmd = ConexionCmd("gen.RepVentasResumidasPorSucursalListar");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = pIDColaborador;
			cmd.Parameters.Add("@IDRespPedido", SqlDbType.Int).Value = pIDRespPedido; 
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			cmd.Parameters.Add("@IDEstadoVenta", SqlDbType.Int).Value = pIDEstadoVenta;
			cmd.Parameters.Add("@IDEstadoCobranza", SqlDbType.Int).Value = pIDEstadoCobranza; 
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;

            BEReporte oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEReporte();
                    oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.FechaVenta = rd.GetDateTime(rd.GetOrdinal("FechaVenta"));
                    oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda"));
                    oBE.Simbolo = rd.GetString(rd.GetOrdinal("Simbolo"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
					oBE.Utilidad = rd.GetDecimal(rd.GetOrdinal("Utilidad"));
					oBE.Cajero = rd.GetString(rd.GetOrdinal("Cajero"));
                    oBE.FechaReg = rd.GetDateTime(rd.GetOrdinal("FechaReg"));
					oBE.EstadoVenta = rd.GetString(rd.GetOrdinal("EstadoVenta"));
					oBE.EstadoCobranza = rd.GetString(rd.GetOrdinal("EstadoCobranza"));
					oBE.Pedido = rd.GetString(rd.GetOrdinal("Pedido"));

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

        public IList RepVentasDetalleProductoListar(Int32 pIDSucursal, Int32 pIDCliente, Int32 pIDColaborador, String pFechaInicio, String pFechaFin)
        {
            SqlCommand cmd = ConexionCmd("gen.RepVentasDetalleProductoListar");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pIDCliente;
            cmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = pIDColaborador;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
          
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    BEReporte oBE = new BEReporte();

                    oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
                    oBE.FechaVenta = rd.GetDateTime(rd.GetOrdinal("FechaVenta"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
                    oBE.NombreProducto = rd.GetString(rd.GetOrdinal("NombreProducto"));
                    oBE.PrecioVentaReporte = rd.GetDecimal(rd.GetOrdinal("PrecioVentaReporte"));
                    oBE.Cantidad = rd.GetInt32(rd.GetOrdinal("Cantidad"));
                    oBE.ItemDetalle = rd.GetInt32(rd.GetOrdinal("ItemDetalle"));
                    oBE.IGVDetalle = rd.GetDecimal(rd.GetOrdinal("IGVDetalle"));
                    oBE.ImporteTotal = rd.GetDecimal(rd.GetOrdinal("ImporteTotal"));
                    oBE.ValorUnitario = rd.GetDecimal(rd.GetOrdinal("ValorUnitario"));
                    oBE.Colaborador = rd.GetString(rd.GetOrdinal("Colaborador"));
                    oBE.PorcentajeDescuento = rd.GetDecimal(rd.GetOrdinal("PorcentajeDescuento"));
                    oBE.Descuento = rd.GetDecimal(rd.GetOrdinal("Descuento"));
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

        //REPORTE DETALLE VENTAS
        public IList RepVentasDetalle(Int32 pIDSucursal, Int32 pIDCliente, String pFechaInicio, String pFechaFin)
        {
            SqlCommand cmd = ConexionCmd("gen.ReporteDetalleVentaListarCore");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pIDCliente; 
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;

            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    BEReporte oBE = new BEReporte();

                    oBE.IDVentaDetalle = rd.GetInt32(rd.GetOrdinal("IDVentaDetalle"));
                    oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
                    oBE.NombreSucursal = rd.GetString(rd.GetOrdinal("NombreSucursal"));
                    oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
                    oBE.NumeroDocumentoCliente = rd.GetString(rd.GetOrdinal("NumeroDocumentoCliente"));
                    oBE.NombreCliente = rd.GetString(rd.GetOrdinal("NombreCliente"));
                    oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
                    oBE.IDTipoComprobante2 = rd.GetInt32(rd.GetOrdinal("IDTipoComprobante"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.FechaVenta = rd.GetDateTime(rd.GetOrdinal("FechaVenta"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.Cajero = rd.GetString(rd.GetOrdinal("Cajero"));
                    oBE.Item = rd.GetInt32(rd.GetOrdinal("Item"));
                    oBE.ValorUnitario = rd.GetDecimal(rd.GetOrdinal("ValorUnitario"));
                    oBE.Igv = rd.GetDecimal(rd.GetOrdinal("Igv"));
                    oBE.PorcentajeDescuento = rd.GetDecimal(rd.GetOrdinal("PorcentajeDescuento"));
                    oBE.Descuento = rd.GetDecimal(rd.GetOrdinal("Descuento"));
                    oBE.SubTotal = rd.GetDecimal(rd.GetOrdinal("SubTotal"));
                    oBE.ImporteTotal = rd.GetDecimal(rd.GetOrdinal("ImporteTotal"));
                    oBE.Cantidad = rd.GetInt32(rd.GetOrdinal("Cantidad"));
                    oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
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

        public IList ReporteRegistroLibroVentas(Int32 pIDSucursal, String pPeriodo)
        {
            SqlCommand cmd = ConexionCmd("gen.RepRegistroLibroVentas");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@Periodo", SqlDbType.VarChar, 100).Value = pPeriodo;

            BEReporte oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEReporte();
                    oBE.Periodo = rd.GetString(rd.GetOrdinal("Periodo"));
                    oBE.Venta = rd.GetString(rd.GetOrdinal("IDVenta"));
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.NombreCliente = rd.GetString(rd.GetOrdinal("NombreCliente"));
                    oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
                    oBE.CalculoIGV = rd.GetDecimal(rd.GetOrdinal("CalculoIGV"));
                    oBE.CalculoISC = rd.GetDecimal(rd.GetOrdinal("CalculoISC"));
                    oBE.CalculoDetraccion = rd.GetDecimal(rd.GetOrdinal("CalculoDetraccion"));
                    oBE.TotalOperacionGravada = rd.GetDecimal(rd.GetOrdinal("TotalOperacionGravada"));
                    oBE.TotalIgv = rd.GetDecimal(rd.GetOrdinal("TotalIgv"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.Migrado = rd.GetString(rd.GetOrdinal("Migrado"));
                    oBE.Anulado = rd.GetString(rd.GetOrdinal("Anulado"));
                    oBE.Cobrado = rd.GetString(rd.GetOrdinal("Cobrado"));
                    oBE.TipoCambio = rd.GetDecimal(rd.GetOrdinal("TipoCambio"));
                    oBE.MotivoAnulacion = rd.GetString(rd.GetOrdinal("MotivoAnulacion"));
                    oBE.FechaAnulado = rd.GetDateTime(rd.GetOrdinal("FechaAnulado"));
                    oBE.EstadoVenta = rd.GetString(rd.GetOrdinal("EstadoVenta"));
                    oBE.EstadoCobranza = rd.GetString(rd.GetOrdinal("EstadoCobranza"));
                    oBE.FechaVenta = rd.GetDateTime(rd.GetOrdinal("FechaVenta")); 
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

        public IList ReporteClientesTop10Ventas(Int32 pIDSucursal, Int32 pIDClientes, String pFechaInicio, String pFechaFin)
        {
            SqlCommand cmd = ConexionCmd("gen.ReporteClientesTop10Ventas");
          
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal; 
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
            cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pIDClientes;
            BEReporte oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEReporte();
                    oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
                    oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.VentasTotal = rd.GetDecimal(rd.GetOrdinal("VentasTotal"));
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

        public IList ReporteVendedoresTopPorSucursal(Int32 pIDSucursal, String pFechaInicio, String pFechaFin)
        {
            SqlCommand cmd = ConexionCmd("gen.ReporteVendedoresTopPorSucursal");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;

            BEReporte oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEReporte();
                    oBE.IDColaborador = rd.GetInt32(rd.GetOrdinal("IDColaborador"));
                    oBE.Colaborador = rd.GetString(rd.GetOrdinal("Colaborador"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));

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
         
        public IList ReporteCajaResumenListar(Int32 pIDSucursal, Int32 pIDCaja, Int32 pIDColaborador, String pFechaInicio, String pFechaFin)
        {
            SqlCommand cmd = ConexionCmd("gen.ReporteCajaResumenListar"); 
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@IDCaja", SqlDbType.Int).Value = pIDCaja;
            cmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = pIDColaborador;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
            BEReporte oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEReporte();
                    oBE.IDCaja = rd.GetInt32(rd.GetOrdinal("IDCaja"));
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.FechaCaja = rd.GetDateTime(rd.GetOrdinal("FechaCaja"));
                    oBE.FechaApertura = rd.GetDateTime(rd.GetOrdinal("FechaApertura"));
                    oBE.FechaCierre = rd.GetDateTime(rd.GetOrdinal("FechaCierre"));
                    oBE.FechaReAperturaCaja = rd.GetDateTime(rd.GetOrdinal("FechaReAperturaCaja"));
                    oBE.UsuarioApertura = rd.GetString(rd.GetOrdinal("UsuarioApertura"));
                    oBE.UsuarioCierre = rd.GetString(rd.GetOrdinal("UsuarioCierre"));
                    oBE.UsuarioReaApertura = rd.GetString(rd.GetOrdinal("UsuarioReaApertura"));
                    oBE.MontoApertura = rd.GetDecimal(rd.GetOrdinal("MontoApertura"));
                    oBE.Contado = rd.GetDecimal(rd.GetOrdinal("Contado"));
                    oBE.Calculado = rd.GetDecimal(rd.GetOrdinal("Calculado"));
                    oBE.Diferencia = rd.GetDecimal(rd.GetOrdinal("Diferencia"));
					oBE.Retiro = rd.GetDecimal(rd.GetOrdinal("Retiro"));
					oBE.NombreEstado = rd.GetString(rd.GetOrdinal("NombreEstado"));
                    oBE.CajaMecanica = rd.GetString(rd.GetOrdinal("CajaMecanica"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.Colaborador = rd.GetString(rd.GetOrdinal("Colaborador"));
                    oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
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

        public IList ReporteMovimientoCajaListar(Int32 pIDSucursal, String pTipoMovimiento,Int32 pIDCaja , String pFechaInicio, String pFechaFin)
        {
            SqlCommand cmd = ConexionCmd("gen.ReporteMovimientoCajaListar"); 
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@TipoMovimiento", SqlDbType.Char, 1).Value = pTipoMovimiento;
            cmd.Parameters.Add("@IDCaja", SqlDbType.Int).Value = pIDCaja;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
            BEReporte oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEReporte();
                    oBE.IDMovimientoCaja = rd.GetInt32(rd.GetOrdinal("IDMovimientoCaja"));
                    oBE.CajaMecanica = rd.GetString(rd.GetOrdinal("CajaMecanica"));
                    oBE.NombreTipoMovimiento = rd.GetString(rd.GetOrdinal("NombreTipoMovimiento"));
                    oBE.FechaMovimiento = rd.GetDateTime(rd.GetOrdinal("FechaMovimiento"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
                    oBE.SiglaTipoComprobante = rd.GetString(rd.GetOrdinal("SiglaTipoComprobante"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.Monto = rd.GetDecimal(rd.GetOrdinal("Monto"));
                    oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
                    oBE.UsuarioCreacion = rd.GetString(rd.GetOrdinal("UsuarioCreacion"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.Operacion = rd.GetString(rd.GetOrdinal("Operacion"));
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

    }
}