 
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLTarjetaCredito : BLBase
    { 
        public IList TarjetaCreditoListar()
        {
            SqlCommand cmd = ConexionCmd("gen.TarjetaCreditoListar");
            BETarjetaCredito oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BETarjetaCredito();
                    oBE.IDTarjetaCredito = rd.GetInt32(rd.GetOrdinal("IDTarjetaCredito"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
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
