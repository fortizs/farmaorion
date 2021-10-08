using Farmacia.App_Class.BE;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Facturacion
{
    public class BLComunicacionBajaDetalle : BLBase
    {
        public IList ComunicacionBajaDetalleListar(Int32 pIDComunicacionBaja)
        {
            SqlCommand cmd = ConexionCmd("fac.ComunicacionBajaDetalleListar");
            cmd.Parameters.Add("@IDComunicacionBaja", SqlDbType.Int).Value = pIDComunicacionBaja; 
            BEComunicacionBajaDetalle oBE;
            ArrayList lista = new ArrayList();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEComunicacionBajaDetalle();
                    oBE.IDComunicacionBajaDetalle = rd.GetInt32(rd.GetOrdinal("IDComunicacionBajaDetalle"));
                    oBE.IDComunicacionBaja = rd.GetInt32(rd.GetOrdinal("IDComunicacionBaja"));
                    oBE.IDResumen = rd.GetString(rd.GetOrdinal("IDResumen"));
                    oBE.NumeroItem = rd.GetInt32(rd.GetOrdinal("NumeroItem"));
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.SerieDocumentoBaja = rd.GetString(rd.GetOrdinal("SerieDocumentoBaja"));
                    oBE.NumeroDocumentoBaja = rd.GetString(rd.GetOrdinal("NumeroDocumentoBaja"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.MotivoBaja = rd.GetString(rd.GetOrdinal("MotivoBaja"));
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
