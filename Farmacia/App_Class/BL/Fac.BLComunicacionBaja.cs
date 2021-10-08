using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Facturacion
{
    public class BLComunicacionBaja : BLBase
    { 
        public IList ComunicacionBajaListar(Int32 pIDEmpresa, Int32 pIDEstadoSunat, String pFiltro)
        {
            SqlCommand cmd = ConexionCmd("fac.ComunicacionBajaListar");
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            cmd.Parameters.Add("@IDEstadoSunat", SqlDbType.Int).Value = pIDEstadoSunat;
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar,200).Value = pFiltro;
             
            BEComunicacionBaja oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEComunicacionBaja();
                    oBE.IDComunicacionBaja = rd.GetInt32(rd.GetOrdinal("IDComunicacionBaja"));
                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));
                    oBE.IDResumen = rd.GetString(rd.GetOrdinal("IDResumen"));
                    oBE.FechaEmisionComprobante = rd.GetString(rd.GetOrdinal("FechaEmisionComprobante"));
                    oBE.FechaGeneracionResumen = rd.GetString(rd.GetOrdinal("FechaGeneracionResumen"));
                    oBE.TicketSunat = rd.GetString(rd.GetOrdinal("TicketSunat"));
                    oBE.IDEstadoDocumento = rd.GetString(rd.GetOrdinal("IDEstadoDocumento"));
                    oBE.IDEstadoSunat = rd.GetString(rd.GetOrdinal("IDEstadoSunat"));
                    oBE.IDTipoComprobante = rd.GetString(rd.GetOrdinal("IDTipoComprobante"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
                    oBE.EstadoDocumento = rd.GetString(rd.GetOrdinal("EstadoDocumento"));
                    oBE.EstadoSunat = rd.GetString(rd.GetOrdinal("EstadoSunat"));
                    oBE.FechaEnvioSunat = rd.GetDateTime(rd.GetOrdinal("FechaEnvioSunat"));
                    oBE.MensajeSunat = rd.GetString(rd.GetOrdinal("MensajeSunat")); 
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

        public BEComunicacionBaja ComunicacionBajaSeleccionar(Int32 pIDComunicacionBaja)
        {
            SqlCommand cmd = ConexionCmd("fac.ComunicacionBajaSeleccionar");
            BEComunicacionBaja oBE = new BEComunicacionBaja();
            cmd.Parameters.Add("@IDComunicacionBaja", SqlDbType.Int).Value = pIDComunicacionBaja;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDComunicacionBaja = rd.GetInt32(rd.GetOrdinal("IDComunicacionBaja"));
                    oBE.IDResumen = rd.GetString(rd.GetOrdinal("IDResumen"));
                    oBE.Correlativo = rd.GetInt32(rd.GetOrdinal("Correlativo"));
                    oBE.RazonSocialEmisor = rd.GetString(rd.GetOrdinal("RazonSocialEmisor"));
                    oBE.TipoDocumentoEmisor = rd.GetInt32(rd.GetOrdinal("TipoDocumentoEmisor"));
                    oBE.RucEmisor = rd.GetString(rd.GetOrdinal("RucEmisor"));
                    oBE.FechaEmisionComprobante = rd.GetString(rd.GetOrdinal("FechaEmisionComprobante"));
                    oBE.FechaGeneracionResumen = rd.GetString(rd.GetOrdinal("FechaGeneracionResumen"));
                    oBE.IDTipoComprobante = rd.GetString(rd.GetOrdinal("IDTipoComprobante"));
                    oBE.IDEstadoDocumento = rd.GetString(rd.GetOrdinal("IDEstadoDocumento"));
                    oBE.IDEstadoSunat = rd.GetString(rd.GetOrdinal("IDEstadoSunat"));
                    oBE.TicketSunat = rd.GetString(rd.GetOrdinal("TicketSunat"));
                    oBE.TramaXML_Firmado = rd.GetString(rd.GetOrdinal("TramaXML_Firmado"));
                    oBE.TramaXML_SinFirmar = rd.GetString(rd.GetOrdinal("TramaXML_SinFirmar"));
                    oBE.TramaZIP_CDR = rd.GetString(rd.GetOrdinal("TramaZIP_CDR"));
                    oBE.CodigoRespuestaSunat = rd.GetString(rd.GetOrdinal("CodigoRespuestaSunat"));
                    oBE.MensajeSunat = rd.GetString(rd.GetOrdinal("MensajeSunat"));
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
        
        public BERetornoTran ComunicacionBajaActualizar(BEComunicacionBaja oBE)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("fac.ComunicacionBajaActualizar");
            cmd.Parameters.Add("@IDComunicacionBaja", SqlDbType.Int).Value = oBE.IDComunicacionBaja;
            cmd.Parameters.Add("@TramaXML_SinFirmar", SqlDbType.VarChar).Value = oBE.TramaXML_SinFirmar;
            cmd.Parameters.Add("@TramaXML_Firmado", SqlDbType.VarChar).Value = oBE.TramaXML_Firmado;
            cmd.Parameters.Add("@TramaZIP_CDR", SqlDbType.VarChar).Value = oBE.TramaZIP_CDR;
            cmd.Parameters.Add("@TicketSunat", SqlDbType.VarChar, 100).Value = oBE.TicketSunat; 
            cmd.Parameters.Add("@CodigoRespuestaSunat", SqlDbType.VarChar,10).Value = oBE.CodigoRespuestaSunat;
            cmd.Parameters.Add("@MensajeSunat", SqlDbType.VarChar,5000).Value = oBE.MensajeSunat;
            cmd.Parameters.Add("@IDEstadoDocumento", SqlDbType.VarChar, 10).Value = oBE.IDEstadoDocumento;
            cmd.Parameters.Add("@IDEstadoSunat", SqlDbType.VarChar, 10).Value = oBE.IDEstadoSunat; 
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
