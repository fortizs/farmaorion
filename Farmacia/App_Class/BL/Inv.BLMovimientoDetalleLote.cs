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
    public class BLMovimientoDetalleLote : BLBase
    {
        #region Transaccional
        public BERetornoTran MovimientoDetalleLoteTemporalGuardar(BEMovimientoDetalleLote BEParam)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("inv.MovimientoDetalleLoteTemporalGuardar");
            cmd.Parameters.Add("@IDLote", SqlDbType.Int).Value = BEParam.IDLote;
            cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
            cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = BEParam.Cantidad;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = BEParam.Estado;
            cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = BEParam.Token;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.VarChar, 100).Value = BEParam.IDUsuario;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
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

        public BERetornoTran MovimientoDetalleLoteTemporalEliminar(string Token)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("inv.MovimientoDetalleLoteTempEliminar");
            cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = Token;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
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

        #endregion

        #region No Transaccional

        public IList MovimientoDetalleLoteListar(BEMovimientoDetalleLote BEParam)
        {
            SqlCommand cmd = ConexionCmd("inv.MovimientoDetalleLoteListar");
            cmd.Parameters.Add("@IDMovimiento", SqlDbType.Int).Value = BEParam.IDMovimiento;
            cmd.Parameters.Add("@IDmovimientoDetalle", SqlDbType.Int).Value = BEParam.IDMovimientoDetalle;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
            cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
            cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = BEParam.Token;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    BELote oBE = new BELote();
                    oBE.IDLote = rd.GetInt32(rd.GetOrdinal("IDLote"));
                    oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.Lote = rd.GetString(rd.GetOrdinal("Lote"));
                    oBE.Stock = rd.GetDecimal(rd.GetOrdinal("Stock"));
                    oBE.CantidadLote = rd.GetDecimal(rd.GetOrdinal("CantidadLote"));
                    oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
                    oBE.FechaFabricacion = rd.GetDateTime(rd.GetOrdinal("FechaFabricacion"));
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