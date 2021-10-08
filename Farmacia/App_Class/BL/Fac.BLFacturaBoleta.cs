using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Facturacion
{
    public class BLFacturaBoleta : BLBase
    {
        public BERetornoTran FacturaBoletaTramaActualizar(Int32 pIDFacturaBoleta, String pTramaXML, String pResumenFirma, String pValorFirmaDigital, String pTipo, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.FacturaBoletaTramaActualizar");
            cmd.Parameters.Add("@IDFacturaBoleta", SqlDbType.Int).Value = pIDFacturaBoleta;
            cmd.Parameters.Add("@TramaXML", SqlDbType.VarChar).Value = pTramaXML;
            cmd.Parameters.Add("@ResumenFirma", SqlDbType.VarChar).Value = pResumenFirma;
            cmd.Parameters.Add("@ValorFirmaDigital", SqlDbType.VarChar).Value = pValorFirmaDigital;
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 10).Value = pTipo;
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

        public BERetornoTran FacturaBoletaAnular(Int32 pIDFacturaBoleta, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.FacturaBoletaAnular");
            cmd.Parameters.Add("@IDFacturaBoleta", SqlDbType.Int).Value = pIDFacturaBoleta; 
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
          
        public BERetornoTran FacturaBoletaArchivoActualizar(BEBase pFacturaBoleta)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.FacturaBoletaArchivoActualizar");
            cmd = LlenarEstructura(pFacturaBoleta, cmd, "I");
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

        public SqlCommand LlenarEstructura(BEBase pFacturaBoleta, SqlCommand pcmd, String pTipoTransaccion)
        {
            BEFacturaBoleta oBE = (BEFacturaBoleta)pFacturaBoleta;
            if (pTipoTransaccion == "I")  // Insertar
            {
                pcmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = oBE.IDFacturaBoleta;
                pcmd.Parameters.Add("@RutaArchivo", SqlDbType.VarChar,200).Value = oBE.RutaArchivo;
                pcmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar, 200).Value = oBE.NombreArchivo;
                pcmd.Parameters.Add("@Accion", SqlDbType.VarChar, 10).Value = oBE.Accion;
            }
          
            pcmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            pcmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            pcmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return pcmd;
        }

        public IList FacturaBoletaListar(Int32 pIDSucursal, String pIDTipoComprobante, String pFiltro, Int32 pIDEstadoSunat)
        {
            SqlCommand cmd = ConexionCmd("fac.FacturaBoletaListar");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.VarChar, 10).Value = pIDTipoComprobante;
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 300).Value = pFiltro;
            cmd.Parameters.Add("@IDEstadoSunat", SqlDbType.Int).Value = pIDEstadoSunat; 
             
            BEFacturaBoleta oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEFacturaBoleta(); 
                    oBE.IDFacturaBoleta = rd.GetInt32(rd.GetOrdinal("IDFacturaBoleta"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.RazonSocialAdquiriente = rd.GetString(rd.GetOrdinal("RazonSocialAdquiriente"));
                    oBE.NumeroDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("NumeroDocumentoAdquiriente"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
                    oBE.MonedaSimbolo = rd.GetString(rd.GetOrdinal("MonedaSimbolo"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.IDEstadoDocumento = rd.GetString(rd.GetOrdinal("IDEstadoDocumento"));
                    oBE.IDEstadoSunat = rd.GetString(rd.GetOrdinal("IDEstadoSunat"));
                    oBE.EstadoDocumento = rd.GetString(rd.GetOrdinal("EstadoDocumento"));
                    oBE.EstadoSunat = rd.GetString(rd.GetOrdinal("EstadoSunat"));
                    oBE.FechaEnvioSunat = rd.GetDateTime(rd.GetOrdinal("FechaEnvioSunat"));
                    oBE.MensajeSunat = rd.GetString(rd.GetOrdinal("MensajeSunat"));
                    oBE.IDTipoComprobante = rd.GetString(rd.GetOrdinal("IDTipoComprobante"));
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
         
        public IList FacturaBoletaDetalleListar(Int32 pIDFacturaBoleta)
        {
            SqlCommand cmd = ConexionCmd("fac.FacturaBoletaDetalleListar");
            cmd.Parameters.Add("@IDFacturaBoleta", SqlDbType.Int).Value = pIDFacturaBoleta;
            BEFacturaBoletaDetalle oBE;
            ArrayList lista = new ArrayList();
            try
            { 
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEFacturaBoletaDetalle(); 
                    oBE.IDFacturaBoletaDetalle = rd.GetInt32(rd.GetOrdinal("IDFacturaBoletaDetalle"));
                    oBE.IDFacturaBoleta = rd.GetInt32(rd.GetOrdinal("IDFacturaBoleta"));
                    oBE.NumeroOrdenItem = rd.GetInt32(rd.GetOrdinal("NumeroOrdenItem"));
                    oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
                    oBE.CodigoProducto = rd.GetString(rd.GetOrdinal("CodigoProducto"));
                    oBE.DescripcionProducto = rd.GetString(rd.GetOrdinal("DescripcionProducto"));
                    oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
                    oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
                    oBE.IDUnidadMedida = rd.GetString(rd.GetOrdinal("IDUnidadMedida"));
                    oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
                    oBE.UnidadMedidaSunat = rd.GetString(rd.GetOrdinal("UnidadMedidaSunat"));
                    oBE.ImporteTotalSinImpuesto = rd.GetDecimal(rd.GetOrdinal("ImporteTotalSinImpuesto"));
                    oBE.ImporteTotalConImpuesto = rd.GetDecimal(rd.GetOrdinal("ImporteTotalConImpuesto")); 
                    oBE.ImporteUniSinImpuesto = rd.GetDecimal(rd.GetOrdinal("ImporteUniSinImpuesto"));
                    oBE.ImporteUniConImpuesto = rd.GetDecimal(rd.GetOrdinal("ImporteUniConImpuesto"));
                    oBE.ImporteIgv = rd.GetDecimal(rd.GetOrdinal("ImporteIgv"));
                    oBE.ImporteDescuento = rd.GetDecimal(rd.GetOrdinal("ImporteDescuento"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
                    oBE.TipoPrecio = rd.GetString(rd.GetOrdinal("TipoPrecio"));
                    oBE.IDTipoImpuesto = rd.GetString(rd.GetOrdinal("IDTipoImpuesto"));
                    oBE.TipoImpuesto = rd.GetString(rd.GetOrdinal("TipoImpuesto"));

                    
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
         
        public IList FacturaBoletaEnviadoSunatListar(String pFiltro, String pIDTipoComprobante, Int32 pIDSucursal)
        {
            SqlCommand cmd = ConexionCmd("fac.FacturaBoletaEnviadoSunatListar");
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar,100).Value = pFiltro;
            cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.VarChar, 2).Value = pIDTipoComprobante;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            BEFacturaBoleta oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEFacturaBoleta(); 
                    oBE.IDFacturaBoleta = rd.GetInt32(rd.GetOrdinal("IDFacturaBoleta")); 
                    oBE.IDTipoComprobante = rd.GetString(rd.GetOrdinal("IDTipoComprobante"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.NumeroDocumentoCliente = rd.GetString(rd.GetOrdinal("NumeroDocumentoCliente"));
                    oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
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
         
        public BEFacturaBoleta InformacionCorreoSeleccionar(Int32 pIDDocumento, String pTipo)
        {
            SqlCommand cmd = ConexionCmd("fac.InformacionCorreoSeleccionar");
            BEFacturaBoleta oBE = new BEFacturaBoleta();
            cmd.Parameters.Add("@IDDocumento", SqlDbType.Int).Value = pIDDocumento;
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 50).Value = pTipo;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.NumeroDocumentoEmisor = rd.GetString(rd.GetOrdinal("NumeroDocumentoEmisor"));
                    oBE.RazonSocialEmisor = rd.GetString(rd.GetOrdinal("RazonSocialEmisor"));
                    oBE.CorreoEmisor = rd.GetString(rd.GetOrdinal("CorreoEmisor"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.NumeroDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("NumeroDocumentoAdquiriente"));
                    oBE.RazonSocialAdquiriente = rd.GetString(rd.GetOrdinal("RazonSocialAdquiriente"));
                    oBE.CorreoAdquiriente = rd.GetString(rd.GetOrdinal("CorreoAdquiriente"));

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

        public BEFacturaBoleta FacturaBoletaSeleccionar(Int32 pIDFacturaBoleta)
        {
            SqlCommand cmd = ConexionCmd("fac.FacturaBoletaSeleccionar");
            BEFacturaBoleta oBE = new BEFacturaBoleta();
            cmd.Parameters.Add("@IDFacturaBoleta", SqlDbType.Int).Value = pIDFacturaBoleta;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                { 
                    oBE.IDFacturaBoleta = rd.GetInt32(rd.GetOrdinal("IDFacturaBoleta"));
                    oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
                    oBE.IDEmisor = rd.GetInt32(rd.GetOrdinal("IDEmisor"));
                    oBE.CorreoEmisor = rd.GetString(rd.GetOrdinal("CorreoEmisor"));
                    oBE.NumeroDocumentoEmisor = rd.GetString(rd.GetOrdinal("NumeroDocumentoEmisor"));
                    oBE.TipoDocumentoEmisor = rd.GetInt32(rd.GetOrdinal("TipoDocumentoEmisor"));
                    oBE.RazonSocialEmisor = rd.GetString(rd.GetOrdinal("RazonSocialEmisor"));
                    oBE.NombreComercialEmisor = rd.GetString(rd.GetOrdinal("NombreComercialEmisor"));
                    oBE.DireccionEmisor = rd.GetString(rd.GetOrdinal("DireccionEmisor"));
                    oBE.UrbanizacionEmisor = rd.GetString(rd.GetOrdinal("UrbanizacionEmisor"));
                    oBE.ProvinciaEmisor = rd.GetString(rd.GetOrdinal("ProvinciaEmisor"));
                    oBE.DepartamentoEmisor = rd.GetString(rd.GetOrdinal("DepartamentoEmisor"));
                    oBE.DistritoEmisor = rd.GetString(rd.GetOrdinal("DistritoEmisor"));
                    oBE.PaisEmisor = rd.GetString(rd.GetOrdinal("PaisEmisor"));
                    oBE.UbigeoEmisor = rd.GetString(rd.GetOrdinal("UbigeoEmisor"));
                    oBE.Certificado = rd.GetString(rd.GetOrdinal("Certificado"));
                    oBE.ClaveCertificado = rd.GetString(rd.GetOrdinal("ClaveCertificado"));
					oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
					oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.CorreoAdquiriente = rd.GetString(rd.GetOrdinal("CorreoAdquiriente"));
                    oBE.NumeroDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("NumeroDocumentoAdquiriente"));
                    oBE.TipoDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("TipoDocumentoAdquiriente"));
                    oBE.RazonSocialAdquiriente = rd.GetString(rd.GetOrdinal("RazonSocialAdquiriente"));
                    oBE.DireccionAdquiriente = rd.GetString(rd.GetOrdinal("DireccionAdquiriente"));
                    oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
                    oBE.TotalVenta_NetoGravada = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoGravada"));
                    oBE.TotalVenta_NetoInafecta = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoInafecta"));
                    oBE.TotalVenta_NetoExonerada = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoExonerada"));
                    oBE.TotalVenta_NetoGratuita = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoGratuita"));
                    oBE.TotalIgvItems = rd.GetDecimal(rd.GetOrdinal("TotalIgvItems"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.CalculoDetraccion = rd.GetDecimal(rd.GetOrdinal("CalculoDetraccion"));
                    oBE.MontoDetraccion = rd.GetDecimal(rd.GetOrdinal("MontoDetraccion"));
                    oBE.IDEstadoDocumento = rd.GetString(rd.GetOrdinal("IDEstadoDocumento"));
                    oBE.IDEstadoSunat = rd.GetString(rd.GetOrdinal("IDEstadoSunat"));
                    oBE.MontoAnticipo = rd.GetInt32(rd.GetOrdinal("MontoAnticipo"));
                    oBE.DocAnticipo = rd.GetString(rd.GetOrdinal("DocAnticipo"));
                    oBE.TipoDocAnticipo = rd.GetString(rd.GetOrdinal("TipoDocAnticipo"));
                    oBE.TipoOperacion = rd.GetString(rd.GetOrdinal("TipoOperacion"));
                    oBE.MontoTotalLetra = rd.GetString(rd.GetOrdinal("MontoTotalLetra"));
                    oBE.TramaXML_SinFirmar = rd.GetString(rd.GetOrdinal("TramaXML_SinFirmar"));
                    oBE.TramaXML_Firmado = rd.GetString(rd.GetOrdinal("TramaXML_Firmado"));
                    oBE.TramaZIP_CDR = rd.GetString(rd.GetOrdinal("TramaZIP_CDR"));
                    oBE.ResumenFirma = rd.GetString(rd.GetOrdinal("ResumenFirma"));
                    oBE.ValorFirmaDigital = rd.GetString(rd.GetOrdinal("ValorFirmaDigital"));
                    oBE.RutaDocumento = rd.GetString(rd.GetOrdinal("RutaDocumento"));
                    oBE.NombreDocumento = rd.GetString(rd.GetOrdinal("NombreDocumento"));
                    oBE.RutaArchivoZip = rd.GetString(rd.GetOrdinal("RutaArchivoZip"));
                    oBE.NombreArchivoZip = rd.GetString(rd.GetOrdinal("NombreArchivoZip"));
                    oBE.CodigoQR = rd.GetString(rd.GetOrdinal("CodigoQR"));
                    oBE.NombrePARA = rd.GetString(rd.GetOrdinal("NombrePARA"));
                    oBE.CorreoPARA = rd.GetString(rd.GetOrdinal("CorreoPARA"));
                    oBE.NombreCC = rd.GetString(rd.GetOrdinal("NombreCC"));
                    oBE.CorreoCC = rd.GetString(rd.GetOrdinal("CorreoCC"));

					oBE.HoraEmision = rd.GetString(rd.GetOrdinal("HoraEmision"));
					oBE.FechaVencimiento = rd.GetString(rd.GetOrdinal("FechaVencimiento"));
					oBE.CodigoLeyenda = rd.GetString(rd.GetOrdinal("CodigoLeyenda")); 
					oBE.NroItems = rd.GetInt32(rd.GetOrdinal("NroItems"));
					oBE.SerieC = rd.GetString(rd.GetOrdinal("SerieC"));
					oBE.NumeroC = rd.GetInt32(rd.GetOrdinal("NumeroC"));
					oBE.TotalDescuentoItems = rd.GetDecimal(rd.GetOrdinal("TotalDescuentoItems"));

					


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

        public IList ReporteRegistroVentas(Int32 pIDSucursal, String pPeriodo)
        {
            SqlCommand cmd = ConexionCmd("gen.ReporteRegistroVentas");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@Periodo", SqlDbType.VarChar, 100).Value = pPeriodo; 
            
            BEFacturaBoleta oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEFacturaBoleta(); 
                    oBE.Codigo = rd.GetInt32(rd.GetOrdinal("Codigo"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.Serie = rd.GetString(rd.GetOrdinal("Serie"));
                    oBE.Numero = rd.GetString(rd.GetOrdinal("Numero"));
                    oBE.TipoDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("TipoDocumentoAdquiriente"));
                    oBE.NumeroDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("NumeroDocumentoAdquiriente"));
                    oBE.RazonSocialAdquiriente = rd.GetString(rd.GetOrdinal("RazonSocialAdquiriente"));
                    oBE.OtrosIngresos = rd.GetString(rd.GetOrdinal("OtrosIngresos"));
                    oBE.TotalVenta_NetoGravada = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoGravada"));
                    oBE.TotalIgvItems = rd.GetDecimal(rd.GetOrdinal("TotalIgvItems"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));

                    oBE.FechaEmisionAfectado = rd.GetString(rd.GetOrdinal("FechaEmisionAfectado"));
                    oBE.TipoDocumentoAfectado = rd.GetString(rd.GetOrdinal("TipoDocumentoAfectado"));
                    oBE.SerieAfectado = rd.GetString(rd.GetOrdinal("SerieAfectado"));
                    oBE.NumeroAfectado = rd.GetString(rd.GetOrdinal("NumeroAfectado"));
                     
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