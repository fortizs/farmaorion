using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL
{
    public class BLFacturacion : BLBase
    {
        public BERetornoTran VentasMigrarInsertar(Int32 pIDTipoDocumento, String pFechaInicio, String pFechaFin, Int32 pIDUsuario, Int32 pIDEmpresa, Int32 pIDSucursal)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.VentasMigrarInsertar");
            cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = pIDTipoDocumento;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar,10).Value = pFechaInicio;
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

        public BERetornoTran VentasMigrarInsertarPorID(Int32 pIDVenta,  Int32 pIDUsuario, Int32 pIDEmpresa)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.VentasMigrarInsertarPorID");
            cmd.Parameters.Add("@IDVenta", SqlDbType.Int).Value = pIDVenta;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
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
        
        public IList VentasMigrarSunatListar(Int32 pIDEmpresa, String pIDTipoDocumento, String pFechaInicio, String pFechaFin)
        {
            SqlCommand cmd = ConexionCmd("fac.VentasMigrarSunatListar");
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.VarChar,50).Value = pIDTipoDocumento;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
            BEFacturacion oBE;
            ArrayList lista = new ArrayList();
            try
            {
           
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEFacturacion();
                    oBE.Codigo = rd.GetInt32(rd.GetOrdinal("Codigo"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
                    oBE.NumeroDocumentoCliente = rd.GetString(rd.GetOrdinal("NumeroDocumentoCliente"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.EstadoDocumento = rd.GetString(rd.GetOrdinal("EstadoDocumento"));
                    oBE.EstadoSunat = rd.GetString(rd.GetOrdinal("EstadoSunat"));
                    oBE.FechaEnvioSunat = rd.GetDateTime(rd.GetOrdinal("FechaEnvioSunat"));
                    oBE.MensajeSunat = rd.GetString(rd.GetOrdinal("MensajeSunat"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
                    oBE.IDEstadoDocumento = rd.GetString(rd.GetOrdinal("IDEstadoDocumento"));
                    oBE.IDEstadoSunat = rd.GetString(rd.GetOrdinal("IDEstadoSunat"));
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

        public IList VentasCrearResumenListar(Int32 pIDEmpresa, String pIDTipoDocumento, String pFechaInicio)
        {
            SqlCommand cmd = ConexionCmd("fac.VentasCrearResumenListar");
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.VarChar, 50).Value = pIDTipoDocumento;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
//            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
            BEFacturacion oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEFacturacion();
                    oBE.Codigo = rd.GetInt32(rd.GetOrdinal("Codigo"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
                    oBE.NumeroDocumentoCliente = rd.GetString(rd.GetOrdinal("NumeroDocumentoCliente"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.EstadoDocumento = rd.GetString(rd.GetOrdinal("EstadoDocumento"));
                    oBE.EstadoSunat = rd.GetString(rd.GetOrdinal("EstadoSunat"));
                    oBE.FechaEnvioSunat = rd.GetDateTime(rd.GetOrdinal("FechaEnvioSunat"));
                    oBE.MensajeSunat = rd.GetString(rd.GetOrdinal("MensajeSunat"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
                    oBE.IDEstadoDocumento = rd.GetString(rd.GetOrdinal("IDEstadoDocumento"));
                    oBE.IDEstadoSunat = rd.GetString(rd.GetOrdinal("IDEstadoSunat"));
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

        public IList VentasComunicacionBajaListar(Int32 pIDEmpresa, String pIDTipoDocumento, String pFechaInicio)
        {
            SqlCommand cmd = ConexionCmd("fac.VentasComunicacionBajaListar");
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.VarChar, 50).Value = pIDTipoDocumento;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            //            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
            BEFacturacion oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEFacturacion();
                    oBE.Codigo = rd.GetInt32(rd.GetOrdinal("Codigo"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
                    oBE.NumeroDocumentoCliente = rd.GetString(rd.GetOrdinal("NumeroDocumentoCliente"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.EstadoDocumento = rd.GetString(rd.GetOrdinal("EstadoDocumento"));
                    oBE.EstadoSunat = rd.GetString(rd.GetOrdinal("EstadoSunat"));
                    oBE.FechaEnvioSunat = rd.GetDateTime(rd.GetOrdinal("FechaEnvioSunat"));
                    oBE.MensajeSunat = rd.GetString(rd.GetOrdinal("MensajeSunat"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
                    oBE.IDEstadoDocumento = rd.GetString(rd.GetOrdinal("IDEstadoDocumento"));
                    oBE.IDEstadoSunat = rd.GetString(rd.GetOrdinal("IDEstadoSunat"));
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
        
        public BERetornoTran ActualizarFacturaTramaFirmado(Int32 pCodigo, String pTramaXML, String pResumenFirma,String pValorFirmaDigital, String pTipo, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.ActualizarFacturaTramaFirmado");
            cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = pCodigo;
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

        public BERetornoTran ActualizarDocumentoEnviadoSunat(Int32 pCodigo, String pTramaXML, String pMensajeSunat, String pTipo, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.ActualizarDocumentoEnviadoSunat");
            cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = pCodigo;
            cmd.Parameters.Add("@TramaXML", SqlDbType.VarChar).Value = pTramaXML;
            cmd.Parameters.Add("@MensajeSunat", SqlDbType.VarChar,200).Value = pMensajeSunat;
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

        public BERetornoTran DarBajaDocumentoElectronico(Int32 pIDFacturaBoleta, String pMotivo, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.DarBajaDocumentoElectronico");
            cmd.Parameters.Add("@IDFacturaBoleta", SqlDbType.Int).Value = pIDFacturaBoleta;
            cmd.Parameters.Add("@Motivo", SqlDbType.VarChar, 8000).Value = pMotivo; 
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
            
        public IList FacturaBoletasMigradasSunatListar(String pSerieNumero, String pFechaInicio, String pFechaFin)
        {
            SqlCommand cmd = ConexionCmd("fac.FacturaBoletasMigradasSunatListar"); 
            cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 50).Value = pSerieNumero;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
  
            BEFacturacion oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEFacturacion();
                    oBE.Codigo = rd.GetInt32(rd.GetOrdinal("Codigo"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
                    oBE.NumeroDocumentoCliente = rd.GetString(rd.GetOrdinal("NumeroDocumentoCliente"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.EstadoDocumento = rd.GetString(rd.GetOrdinal("EstadoDocumento"));
                    oBE.EstadoSunat = rd.GetString(rd.GetOrdinal("EstadoSunat"));
                    oBE.FechaEnvioSunat = rd.GetDateTime(rd.GetOrdinal("FechaEnvioSunat"));
                    oBE.MensajeSunat = rd.GetString(rd.GetOrdinal("MensajeSunat"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));

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
          
        public IList ComunicacionBajaDetalleListar(Int32 pIDComunicacionBaja)
        {
            SqlCommand cmd = ConexionCmd("fac.ComunicacionBajaDetalleListar");
            cmd.Parameters.Add("@IDComunicacionBaja", SqlDbType.Int).Value = pIDComunicacionBaja;
            BEFacturacion oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEFacturacion();
                    oBE.IDComunicacionBajaDetalle = rd.GetInt32(rd.GetOrdinal("IDComunicacionBajaDetalle"));
                    oBE.IDComunicacionBaja = rd.GetInt32(rd.GetOrdinal("IDComunicacionBaja"));
                    oBE.IDResumen = rd.GetString(rd.GetOrdinal("IDResumen"));
                    oBE.NumeroItem = rd.GetInt32(rd.GetOrdinal("NumeroItem")); 
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.SerieDocumentoBaja = rd.GetString(rd.GetOrdinal("SerieDocumentoBaja"));
                    oBE.NumeroDocumentoBaja = rd.GetString(rd.GetOrdinal("NumeroDocumentoBaja"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.MotivoBaja = rd.GetString(rd.GetOrdinal("MotivoBaja")); ;
                     
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

        public BERetornoTran CrearResumenBajaMasivo(BEFacturacion pFact, ArrayList pDocumento)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            BEFacturacion BECompraRetorno = new BEFacturacion();

            SqlConnection objcn = new SqlConnection(CadenaConexion());
            objcn.Open();
            SqlTransaction sqlTran = objcn.BeginTransaction();

            //FACTURA------------------------------------------------------------------------------------------------
             
            SqlCommand cmd = new SqlCommand("fac.ComunicacionBajaGuardar", objcn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = DuracionComando();
            cmd.Transaction = sqlTran;
            BERetorno.Retorno = "-1";
            try
            {
                cmd.Parameters.Add("@IDEmpresa", SqlDbType.VarChar, 10).Value = pFact.IDEmpresa;
                cmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar, 10).Value = pFact.TipoDocumento;
                cmd.Parameters.Add("@FechaEmision", SqlDbType.VarChar, 10).Value = pFact.FechaEmision;
                cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pFact.IDUsuario; 
                cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add("@IDComunicacionBajaFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
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
                    BECompraRetorno.IDComunicacionBajaFinal = Int32.Parse(cmd.Parameters["@IDComunicacionBajaFinal"].Value.ToString());
                }

                //COMPRA DETALLE-----------------------------------------------------------------------------               
                cmd.Parameters.Clear();
                BERetorno.Retorno = "-1";
                cmd.CommandText = "fac.ComunicacionBajaDetalleGuardar";
  
                BEFacturacion oBEDocumentoDet = new BEFacturacion();
                for (Int32 i = 0; i < pDocumento.Count; i++)
                {

                    oBEDocumentoDet = (BEFacturacion)pDocumento[i]; 
                    cmd.Parameters.Add("IDComunicacionBaja", SqlDbType.Int, 50).Value = BECompraRetorno.IDComunicacionBajaFinal;
                    cmd.Parameters.Add("@IDFacturaBoleta", SqlDbType.Int, 50).Value = oBEDocumentoDet.Codigo;
                    cmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar, 10).Value = pFact.TipoDocumento;
                    cmd.Parameters.Add("@Motivo", SqlDbType.VarChar, 200).Value = oBEDocumentoDet.MotivoBaja;
                    cmd.Parameters.Add("@Item", SqlDbType.Int,10).Value = i + 1; 
                    cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEDocumentoDet.IDUsuario;
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

        public BERetornoTran ActualizarErrorXML(Int32 pCodigo, String pMensajeError, String pTipo, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.ActualizarErrorXML");
            cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = pCodigo;
            cmd.Parameters.Add("@MensajeError", SqlDbType.VarChar).Value = pMensajeError;
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
		 
        public BERetornoTran ActualizarCodigoQR(Int32 pCodigo, String pCodigoQR, String pTipo, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.ActualizarCodigoQR");
            cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = pCodigo;
            cmd.Parameters.Add("@CodigoQR", SqlDbType.VarChar).Value = pCodigoQR;
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
		 
        public BERetornoTran ActualizarUrlDocumento(Int32 pCodigo, String pUrl, String pNombre, String pTipo, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.ActualizarUrlDocumento");
            cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = pCodigo;
            cmd.Parameters.Add("@UrlDocumento", SqlDbType.VarChar).Value = pUrl;
            cmd.Parameters.Add("@NombreDocumento", SqlDbType.VarChar).Value = pNombre;
            cmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar, 10).Value = pTipo;
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
		 
        public BERetornoTran ObtenerUrlDocumento(Int32 pCodigo, String pTipo)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.ObtenerUrlDocumento");
            cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = pCodigo;
            cmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar, 10).Value = pTipo;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@Retorno2", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
                BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
                BERetorno.Retorno2  = Convert.ToString(cmd.Parameters["@Retorno2"].Value);
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

        public BEBase ReporteFacturaBoletaSeleccionar(Int32 pIDFacturaBoleta)
        {
            SqlCommand cmd = ConexionCmd("fac.ReporteFacturaBoletaSeleccionar");
            BEFacturaBoleta oBE = new BEFacturaBoleta();
            cmd.Parameters.Add("@IDFacturaBoleta", SqlDbType.Int).Value = pIDFacturaBoleta;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                { 
                    oBE.NumeroDocumentoEmisor = rd.GetString(rd.GetOrdinal("NumeroDocumentoEmisor"));
                    oBE.RazonSocialEmisor = rd.GetString(rd.GetOrdinal("RazonSocialEmisor"));
                    oBE.DireccionEmisor = rd.GetString(rd.GetOrdinal("DireccionEmisor"));
                    oBE.UbigeoEmisor = rd.GetString(rd.GetOrdinal("UbigeoEmisor"));
                    oBE.TipoDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("TipoDocumentoAdquiriente"));
                    oBE.NumeroDocumentoAdquiriente = rd.GetString(rd.GetOrdinal("NumeroDocumentoAdquiriente"));
                    oBE.RazonSocialAdquiriente = rd.GetString(rd.GetOrdinal("RazonSocialAdquiriente"));
                    oBE.DireccionAdquiriente = rd.GetString(rd.GetOrdinal("DireccionAdquiriente"));
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.FechaEmision = rd.GetString(rd.GetOrdinal("FechaEmision"));
                    oBE.TipoMoneda = rd.GetString(rd.GetOrdinal("TipoMoneda"));
                    oBE.TotalVenta_NetoInafecta = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoInafecta"));
                    oBE.TotalVenta_NetoExonerada  = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoExonerada"));
                    oBE.TotalVenta_NetoGravada = rd.GetDecimal(rd.GetOrdinal("TotalVenta_NetoGravada"));
                    oBE.TotalIgvItems = rd.GetDecimal(rd.GetOrdinal("TotalIgvItems"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.TextoLeyenda_1 = rd.GetString(rd.GetOrdinal("TextoLeyenda_1"));
                    oBE.Resumen = rd.GetString(rd.GetOrdinal("Resumen"));
                    oBE.CodigoQR = rd.GetString(rd.GetOrdinal("CodigoQR"));
                    oBE.UrlLogo = rd.GetString(rd.GetOrdinal("UrlLogo"));
                    oBE.CuentaBancaria = rd.GetString(rd.GetOrdinal("CuentaBancaria"));
                    oBE.CuentaDetraccion = rd.GetString(rd.GetOrdinal("CuentaDetraccion"));
                    oBE.CalculoDetraccion  = rd.GetDecimal(rd.GetOrdinal("CalculoDetraccion"));
                    oBE.MontoDetraccion = rd.GetDecimal(rd.GetOrdinal("MontoDetraccion"));
                    oBE.MontoAnticipo = rd.GetDecimal(rd.GetOrdinal("MontoAnticipo"));
                    oBE.DocAnticipo  = rd.GetString(rd.GetOrdinal("DocAnticipo"));
                    oBE.FechaAnticipo = rd.GetString(rd.GetOrdinal("FechaAnticipo"));
                    oBE.MonedaNombre = rd.GetString(rd.GetOrdinal("MonedaNombre"));
                    oBE.MonedaNombreCorto= rd.GetString(rd.GetOrdinal("MonedaNombreCorto"));
                    oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));
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

        public IList ReporteFacturaBoletaDetalle(Int32 pIDFacturaBoleta)
        {
            SqlCommand cmd = ConexionCmd("fac.ReporteFacturaBoletaDetalle");
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
                    oBE.NumeroOrdenItem = rd.GetInt32(rd.GetOrdinal("NumeroOrdenItem"));
                    oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
                    oBE.DescripcionProducto = rd.GetString(rd.GetOrdinal("DescripcionProducto"));
                    oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
                    oBE.ImporteUniSinImpuesto = rd.GetDecimal(rd.GetOrdinal("ImporteUniSinImpuesto"));
                    oBE.ImporteUniConImpuesto = rd.GetDecimal(rd.GetOrdinal("ImporteUniConImpuesto"));
                    oBE.ImporteIgv = rd.GetDecimal(rd.GetOrdinal("ImporteIgv"));
                    oBE.ImporteTotalSinImpuesto = rd.GetDecimal(rd.GetOrdinal("ImporteTotalSinImpuesto"));


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
		 
		public BERetornoTran ComprobanteCDRActualizar(BEFacturacion oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("fac.ComprobanteCDRActualizar");
			cmd.Parameters.Add("@IDComprobante", SqlDbType.Int).Value = oBE.IDComprobante;
			cmd.Parameters.Add("@TramaZipCdr", SqlDbType.VarChar).Value = oBE.TramaZipCdr;
			cmd.Parameters.Add("@MensajeRespuesta", SqlDbType.VarChar).Value = oBE.MensajeRespuesta;
			cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 10).Value = oBE.Tipo;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
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
		 
	}
}
