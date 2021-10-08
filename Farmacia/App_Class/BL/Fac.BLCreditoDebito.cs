using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL 
{
    public class BLCreditoDebito : BLBase
    {
        public BERetornoTran CreditoDebitoGuardar(BECreditoDebito oBECreDeb, ArrayList pListaDetalle)
        {
            BERetornoTran BERetorno = new BERetornoTran(); 
            SqlConnection objcn = new SqlConnection(CadenaConexion());
            objcn.Open();
            SqlTransaction sqlTran = objcn.BeginTransaction();
             
            SqlCommand cmd = new SqlCommand("fac.CreditoDebitoInsertar", objcn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = DuracionComando();
            cmd.Transaction = sqlTran;
            BERetorno.Retorno = "-1";
            Int32 IDCreditoDebito = 0;
             
            try
            {
                cmd.Parameters.Add("@IDCreditoDebito", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@IDFacturaBoleta", SqlDbType.Int, 10).Value = oBECreDeb.IDFacturaBoleta;
                cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = oBECreDeb.IDTipoDocumento;
                cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int, 10).Value = oBECreDeb.IDEmpresa;
                cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = oBECreDeb.IDSucursal;
                cmd.Parameters.Add("@TotalVenta_NetoGravada", SqlDbType.Decimal).Value = oBECreDeb.TotalVenta_NetoGravada;
                cmd.Parameters.Add("@TotalVenta_NetoExonerada", SqlDbType.Decimal).Value = oBECreDeb.TotalVenta_NetoExonerada;
                cmd.Parameters.Add("@TotalVenta_NetoInafecta", SqlDbType.Decimal).Value = oBECreDeb.TotalVenta_NetoInafecta;
                cmd.Parameters.Add("@TotalVenta_NetoGratuita", SqlDbType.Decimal).Value = oBECreDeb.TotalVenta_NetoGratuita;

                cmd.Parameters.Add("@TotalIgvItems", SqlDbType.Decimal).Value = oBECreDeb.TotalIgvItems;
                cmd.Parameters.Add("@TotalIscItems", SqlDbType.Decimal).Value = oBECreDeb.TotalIscItems;
                cmd.Parameters.Add("@TotalOtrosTributos", SqlDbType.Decimal).Value = oBECreDeb.TotalOtrosTributos;
                cmd.Parameters.Add("@TotalVenta", SqlDbType.Decimal).Value = oBECreDeb.TotalVenta;

                cmd.Parameters.Add("@SerieNumeroAfectado", SqlDbType.VarChar, 20).Value = oBECreDeb.SerieNumeroAfectado;
                cmd.Parameters.Add("@TipoDocumentoAfectado", SqlDbType.VarChar, 10).Value = oBECreDeb.TipoDocumentoAfectado;
                cmd.Parameters.Add("@IDTipoMotivo", SqlDbType.VarChar, 10).Value = oBECreDeb.IDTipoMotivo;
                cmd.Parameters.Add("@MotivoDocumento", SqlDbType.VarChar, 200).Value = oBECreDeb.MotivoDocumento;

                cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBECreDeb.IDUsuario;
                cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
                BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
                if (BERetorno.Retorno != "1")
                {
                    throw new Exception(BERetorno.ErrorMensaje);
                }
                else
                {
                    IDCreditoDebito = Int32.Parse(cmd.Parameters["@IDCreditoDebito"].Value.ToString());
                    BERetorno.Retorno2 = cmd.Parameters["@IDCreditoDebito"].Value.ToString();
                }

                //COMPRA DETALLE-----------------------------------------------------------------------------               
                cmd.Parameters.Clear();
                BERetorno.Retorno = "-1";
                cmd.CommandText = "fac.CreditoDebitoDetalleInsertar";

                BEFacturaBoletaDetalle oBEDetalle = new BEFacturaBoletaDetalle();
                Int32 cont = 0;
                for (Int32 i = 0; i < pListaDetalle.Count; i++)
                {
                    cont = cont + 1;
                    oBEDetalle = (BEFacturaBoletaDetalle)pListaDetalle[i];
                    cmd.Parameters.Add("@IDCreditoDebito", SqlDbType.Int, 50).Value = IDCreditoDebito;
                    cmd.Parameters.Add("@NumeroOrdenItem", SqlDbType.Int, 50).Value = cont;
                    cmd.Parameters.Add("@IDProducto", SqlDbType.VarChar, 100).Value = oBEDetalle.IDProducto;
                    cmd.Parameters.Add("@CodigoProducto", SqlDbType.VarChar, 100).Value = oBEDetalle.CodigoProducto;
                    cmd.Parameters.Add("@DescripcionProducto", SqlDbType.VarChar, 300).Value = oBEDetalle.DescripcionProducto;
                    cmd.Parameters.Add("@Cantidad", SqlDbType.Int, 50).Value = oBEDetalle.Cantidad;
                    cmd.Parameters.Add("@UnidadMedida", SqlDbType.VarChar, 10).Value = oBEDetalle.CodigoUnidadMedida;
                    cmd.Parameters.Add("@ImporteUniSinImpuesto", SqlDbType.Decimal).Value = oBEDetalle.ImporteUniSinImpuesto;
                    cmd.Parameters.Add("@ImporteTotalSinImpuesto", SqlDbType.Decimal).Value = oBEDetalle.ImporteTotalSinImpuesto;
                    cmd.Parameters.Add("@ImporteIgv", SqlDbType.Decimal).Value = oBEDetalle.ImporteIgv;
                    cmd.Parameters.Add("@ImporteIsc", SqlDbType.Decimal).Value = 0;
                    cmd.Parameters.Add("@CodigoAfectacionIgv", SqlDbType.VarChar, 10).Value = oBEDetalle.IDTipoImpuesto;
                    cmd.Parameters.Add("@ImporteReferencial", SqlDbType.Decimal).Value = oBEDetalle.ImporteReferencial;
                    cmd.Parameters.Add("@ImporteDescuento", SqlDbType.Decimal).Value = oBEDetalle.ImporteDescuento;
                    cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEDetalle.IDUsuario;
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
                    // BERetorno.Retorno = "-1";

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
         
        public BERetornoTran CreditoDebitoArchivoActualizar(BEBase pCreditoDebito)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.CreditoDebitoArchivoActualizar");
            cmd = LlenarEstructura(pCreditoDebito, cmd, "I");
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


        public BERetornoTran CreditoDebitoAnular(Int32 pIDCreditoDebito, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.CreditoDebitoAnular");
            cmd.Parameters.Add("@IDCreditoDebito", SqlDbType.Int).Value = pIDCreditoDebito;
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

        public SqlCommand LlenarEstructura(BEBase pCreditoDebito, SqlCommand pcmd, String pTipoTransaccion)
        {
            BECreditoDebito oBE = (BECreditoDebito)pCreditoDebito;
            if (pTipoTransaccion == "I")  // Insertar
            {
                pcmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = oBE.IDCreditoDebito;
                pcmd.Parameters.Add("@RutaArchivo", SqlDbType.VarChar, 200).Value = oBE.RutaArchivo;
                pcmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar, 200).Value = oBE.NombreArchivo;
                pcmd.Parameters.Add("@Accion", SqlDbType.VarChar, 10).Value = oBE.Accion;
            }

            pcmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            pcmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            pcmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return pcmd;
        }
         
        public BECreditoDebito CreditoDebitoSeleccionar(Int32 pIDCreditoDebito)
        {
            SqlCommand cmd = ConexionCmd("fac.CreditoDebitoSeleccionar");
            BECreditoDebito oBE = new BECreditoDebito();
            cmd.Parameters.Add("@IDCreditoDebito", SqlDbType.Int).Value = pIDCreditoDebito;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {


                    oBE.IDCreditoDebito = rd.GetInt32(rd.GetOrdinal("IDCreditoDebito"));
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
					oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
					oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.CorreoAdquiriente = rd.GetString(rd.GetOrdinal("CorreoAdquiriente"));
                    oBE.NumeroDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("NumeroDocumentoAdquiriente"));
                    oBE.TipoDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("TipoDocumentoAdquiriente"));
                    oBE.RazonSocialAdquiriente = rd.GetString(rd.GetOrdinal("RazonSocialAdquiriente"));
                    oBE.DireccionAdquiriente = rd.GetString(rd.GetOrdinal("DireccionAdquiriente"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
                    oBE.TotalVenta_NetoGravada = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoGravada"));
                    oBE.TotalVenta_NetoInafecta = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoInafecta"));
                    oBE.TotalVenta_NetoExonerada = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoExonerada"));
                    oBE.TotalVenta_NetoGratuita = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoGratuita"));
                    oBE.TotalIgvItems = rd.GetDecimal(rd.GetOrdinal("TotalIgvItems"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.IDEstadoDocumento = rd.GetString(rd.GetOrdinal("IDEstadoDocumento"));
                    oBE.IDEstadoSunat = rd.GetString(rd.GetOrdinal("IDEstadoSunat"));
                    oBE.SerieNumeroAfectado = rd.GetString(rd.GetOrdinal("SerieNumeroAfectado"));
                    oBE.TipoDocumentoAfectado = rd.GetString(rd.GetOrdinal("TipoDocumentoAfectado"));
                    oBE.IDTipoMotivo = rd.GetString(rd.GetOrdinal("IDTipoMotivo"));
                    oBE.MotivoDocumento = rd.GetString(rd.GetOrdinal("MotivoDocumento"));
                    oBE.TipoDocumentoReferencia = rd.GetString(rd.GetOrdinal("TipoDocumentoReferencia"));
                    oBE.NumeroDocumentoReferencia = rd.GetString(rd.GetOrdinal("NumeroDocumentoReferencia"));
                    oBE.MontoTotalLetra = rd.GetString(rd.GetOrdinal("MontoTotalLetra"));
                    oBE.TramaXML_SinFirmar = rd.GetString(rd.GetOrdinal("TramaXML_SinFirmar"));
                    oBE.TramaXML_Firmado = rd.GetString(rd.GetOrdinal("TramaXML_Firmado"));
                    oBE.TramaZIP_CDR = rd.GetString(rd.GetOrdinal("TramaZIP_CDR"));
                    oBE.ResumenFirma = rd.GetString(rd.GetOrdinal("ResumenFirma"));
                    oBE.ValorFirmaDigital = rd.GetString(rd.GetOrdinal("ValorFirmaDigital"));
                    oBE.RutaCodigoQR = rd.GetString(rd.GetOrdinal("RutaCodigoQR"));
                    oBE.RutaDocumento = rd.GetString(rd.GetOrdinal("RutaDocumento"));
                    oBE.NombreDocumento = rd.GetString(rd.GetOrdinal("NombreDocumento"));
                    oBE.RutaArchivoZip = rd.GetString(rd.GetOrdinal("RutaArchivoZip"));
                    oBE.NombreArchivoZip = rd.GetString(rd.GetOrdinal("NombreArchivoZip"));
                    oBE.CodigoQR = rd.GetString(rd.GetOrdinal("CodigoQR"));
                    oBE.NombrePARA = rd.GetString(rd.GetOrdinal("NombrePARA"));
                    oBE.CorreoPARA = rd.GetString(rd.GetOrdinal("CorreoPARA"));
                    oBE.NombreCC = rd.GetString(rd.GetOrdinal("NombreCC"));
                    oBE.CorreoCC = rd.GetString(rd.GetOrdinal("CorreoCC"));
                    oBE.Certificado = rd.GetString(rd.GetOrdinal("Certificado"));
                    oBE.ClaveCertificado = rd.GetString(rd.GetOrdinal("ClaveCertificado"));

					oBE.HoraEmision = rd.GetString(rd.GetOrdinal("HoraEmision"));
					oBE.FechaVencimiento = rd.GetString(rd.GetOrdinal("FechaVencimiento"));
					oBE.CodigoLeyenda = rd.GetString(rd.GetOrdinal("CodigoLeyenda"));
					oBE.NroItems = rd.GetInt32(rd.GetOrdinal("NroItems"));
					oBE.SerieC = rd.GetString(rd.GetOrdinal("SerieC"));
					oBE.NumeroC = rd.GetInt32(rd.GetOrdinal("NumeroC"));

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
         
        public IList CreditoDebitoListar(Int32 pIDSucursal, String pIDTipoComprobante, String pFiltro, Int32 pIDEstadoSunat)
        {
            SqlCommand cmd = ConexionCmd("fac.CreditoDebitoListar");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.VarChar,2).Value = pIDTipoComprobante;
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar,500).Value = pFiltro;
            cmd.Parameters.Add("@IDEstadoSunat", SqlDbType.Int).Value = pIDEstadoSunat;
             
            BECreditoDebito oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BECreditoDebito(); 
                    oBE.IDCreditoDebito = rd.GetInt32(rd.GetOrdinal("IDCreditoDebito"));
                    oBE.IDTipoComprobante = rd.GetString(rd.GetOrdinal("IDTipoComprobante"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.NumeroDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("NumeroDocumentoAdquiriente"));
                    oBE.RazonSocialAdquiriente = rd.GetString(rd.GetOrdinal("RazonSocialAdquiriente"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
                    oBE.MonedaSimbolo = rd.GetString(rd.GetOrdinal("MonedaSimbolo"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.SerieNumeroAfectado = rd.GetString(rd.GetOrdinal("SerieNumeroAfectado"));
                    oBE.EstadoDocumento = rd.GetString(rd.GetOrdinal("EstadoDocumento"));
                    oBE.EstadoSunat = rd.GetString(rd.GetOrdinal("EstadoSunat"));
                    oBE.FechaEnvioSunat = rd.GetDateTime(rd.GetOrdinal("FechaEnvioSunat"));
                    oBE.MensajeSunat = rd.GetString(rd.GetOrdinal("MensajeSunat"));
                    oBE.IDEstadoDocumento = rd.GetString(rd.GetOrdinal("IDEstadoDocumento"));
                    oBE.IDEstadoSunat = rd.GetString(rd.GetOrdinal("IDEstadoSunat"));
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
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
         
        public IList CreditoDebitoDetalleListar(Int32 pIDCreditoDebito)
        {
            SqlCommand cmd = ConexionCmd("fac.CreditoDebitoDetalleListar");
            cmd.Parameters.Add("@IDCreditoDebito", SqlDbType.Int).Value = pIDCreditoDebito;

            BECreditoDebitoDetalle oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BECreditoDebitoDetalle();
                    
                    oBE.IDCreditoDebitoDetalle = rd.GetInt32(rd.GetOrdinal("IDCreditoDebitoDetalle"));
                    oBE.IDCreditoDebito = rd.GetInt32(rd.GetOrdinal("IDCreditoDebito"));
                    oBE.NumeroOrdenItem = rd.GetInt32(rd.GetOrdinal("NumeroOrdenItem"));
                    oBE.CodigoProducto = rd.GetString(rd.GetOrdinal("CodigoProducto"));
                    oBE.DescripcionProducto = rd.GetString(rd.GetOrdinal("DescripcionProducto"));
                    oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
                    oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
                    oBE.IDUnidadMedida = rd.GetString(rd.GetOrdinal("IDUnidadMedida"));
                    oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
                    oBE.ImporteUniSinImpuesto = rd.GetDecimal(rd.GetOrdinal("ImporteUniSinImpuesto"));
                    oBE.ImporteTotalSinImpuesto = rd.GetDecimal(rd.GetOrdinal("ImporteTotalSinImpuesto"));
                    oBE.ImporteTotalConImpuesto = rd.GetDecimal(rd.GetOrdinal("ImporteTotalConImpuesto"));
                    oBE.ImporteIgv = rd.GetDecimal(rd.GetOrdinal("ImporteIgv"));
                    oBE.ImporteIsc = rd.GetDecimal(rd.GetOrdinal("ImporteIsc"));
                    oBE.CodigoAfectacionIgv = rd.GetString(rd.GetOrdinal("CodigoAfectacionIgv"));
                    oBE.CodigoSistemaIsc = rd.GetString(rd.GetOrdinal("CodigoSistemaIsc"));
                    oBE.CodigoImporteReferencial = rd.GetString(rd.GetOrdinal("CodigoImporteReferencial"));
                    oBE.ImporteReferencial = rd.GetDecimal(rd.GetOrdinal("ImporteReferencial"));
                    oBE.ImporteDescuento = rd.GetDecimal(rd.GetOrdinal("ImporteDescuento"));
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
         
    }
}