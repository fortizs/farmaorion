using Farmacia.App_Class.BE; 
using System; 
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLWebService : BLBase
    { 

        public List<BEWebService> ProductoSucursalListar(Int32 pIDSucursal, String pBuscar)
        {
            SqlCommand cmd = ConexionCmd("gen.ProductoSucursalListar");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@Buscar", SqlDbType.VarChar, 100).Value = pBuscar;
            BEWebService oBE = new BEWebService();
            List<BEWebService> lista = new List<BEWebService>();
            try
            {

                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {

                    oBE = new BEWebService();
                    oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
                    oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
                    oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
                    oBE.StockActual = rd.GetDecimal(rd.GetOrdinal("StockActual"));
                    oBE.IDTipoImpuesto = rd.GetString(rd.GetOrdinal("IDTipoImpuesto"));
                    oBE.IDTipoPrecio = rd.GetString(rd.GetOrdinal("IDTipoPrecio"));

                     

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
