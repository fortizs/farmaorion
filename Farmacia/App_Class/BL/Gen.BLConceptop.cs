using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.General
{
    public class BLConcepto : BLBase
    {
        public IList ConceptoListar(String pTipoConcepto, String pFiltro)
        {
            SqlCommand cmd = ConexionCmd("gen.ConceptoListar");
            cmd.Parameters.Add("@TipoConcepto", SqlDbType.VarChar, 100).Value = pTipoConcepto;
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
            BEConcepto oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEConcepto();
                    oBE.IDConcepto = rd.GetInt32(rd.GetOrdinal("IDConcepto"));
                    oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
                    oBE.TipoConcepto = rd.GetString(rd.GetOrdinal("TipoConcepto"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.CuentaContable = rd.GetString(rd.GetOrdinal("CuentaContable"));
                    oBE.CuentaPagoDiferido = rd.GetString(rd.GetOrdinal("CuentaPagoDiferido"));
                    oBE.IDAspecto = rd.GetString(rd.GetOrdinal("IDAspecto"));
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