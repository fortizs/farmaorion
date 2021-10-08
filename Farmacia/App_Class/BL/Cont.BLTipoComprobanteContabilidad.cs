
using Farmacia.App_Class.BE.Contabilidad; 
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Contabilidad
{
    public class BLTipoComprobanteContabilidad : BLBase
    {

        public IList TipoComprobanteContabilidadListar(String pFiltro)
        {
            SqlCommand cmd = ConexionCmd("cont.TipoComprobanteContabilidadListar");
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
            BETipoComprobanteContabilidad oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BETipoComprobanteContabilidad();
                    oBE.IDTipoComprobanteContabilidad = rd.GetInt32(rd.GetOrdinal("IDTipoComprobanteContabilidad"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.CodigoSunat = rd.GetString(rd.GetOrdinal("CodigoSunat"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
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
