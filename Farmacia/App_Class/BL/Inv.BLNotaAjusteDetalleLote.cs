using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL
{
    public class BLNotaAjusteDetalleLote : BLBase
    {
        #region Transaccional
        public BERetornoTran NotaAjusteDetalleLoteTemporalGuardar(BENotaAjusteDetalleLote BEParam)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("inv.NotaAjusteDetalleLoteTemporalGuardar");
            cmd.Parameters.Add("@IDLote", SqlDbType.Int).Value = BEParam.IDLote;
            cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
            cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = BEParam.Cantidad;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = BEParam.Estado;
            cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = BEParam.Token;
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

//        LotexNotaAjusteTokenListar

        #endregion

    }
}