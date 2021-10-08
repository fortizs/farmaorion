using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL
{
    public class BLKitDetalle : BLBase
    {

        #region No Transaccional

        public IList KitDetalleListar(Int32 pIDKit, Int32 pIDSucursal)
        {
            SqlCommand cmd = ConexionCmd("gen.KitDetalleListar");
            cmd.Parameters.Add("@IDKit", SqlDbType.Int, 10).Value = pIDKit;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pIDSucursal;
            BEKitDetalle oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                BEProducto oBEProducto = new BEProducto();                
                while (rd.Read())
                {
                    oBE = new BEKitDetalle();                    
                    oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
                    oBEProducto.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
                    oBEProducto.PrecioCosto = rd.GetDecimal(rd.GetOrdinal("PrecioCosto"));                    
                    oBE.NombreProducto = rd.GetString(rd.GetOrdinal("Producto"));
                    oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
                    oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
                    oBE.CantidadReg = rd.GetDecimal(rd.GetOrdinal("CantidadReg"));
                    oBE.CantidadArmado = rd.GetDecimal(rd.GetOrdinal("CantidadArmado"));
                    oBE.CantidadDisponible = rd.GetDecimal(rd.GetOrdinal("CantidadDisponible"));
                    oBE.CantidadLoteDisponible = rd.GetDecimal(rd.GetOrdinal("CantidadLoteDisponible"));
                    oBEProducto.ControlaLote = rd.GetBoolean(rd.GetOrdinal("ControlaLote"));
                    if (oBEProducto.ControlaLote) {
                        oBE.CantidadDisponible = oBE.CantidadLoteDisponible;
                    }
                    oBE.Producto = oBEProducto;
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

        #endregion


    }
}