using Farmacia.App_Class.BE;
using System; 
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL
{
    public class BLMedioPago : BLBase
    {

        public IList MedioPagoListar(String pFiltro)
        {
            SqlCommand cmd = ConexionCmd("gen.MedioPagoListar");
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
            BEMedioPago oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEMedioPago();
                    oBE.IDMedioPago = rd.GetInt32(rd.GetOrdinal("IDMedioPago"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
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

        public BEMedioPago MedioPagoSeleccionar(Int32 pID)
        {
            SqlCommand cmd = ConexionCmd("gen.MedioPagoSeleccionar");
            BEMedioPago oBE = new BEMedioPago();
            cmd.Parameters.Add("@IDMedioPago", SqlDbType.Int).Value = pID;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDMedioPago = rd.GetInt32(rd.GetOrdinal("IDMedioPago"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo")); 
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado")); 
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
    }
}