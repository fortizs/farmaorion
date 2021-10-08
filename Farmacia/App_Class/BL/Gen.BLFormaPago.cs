
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLFormaPago : BLBase
    {
        #region NoTransaccional

        public IList FormaPagoListar()
        {
            SqlCommand cmd = ConexionCmd("gen.FormaPagoListar");
            BEFormaPago oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEFormaPago();
                    oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.NumeroDia = rd.GetInt32(rd.GetOrdinal("NumeroDia"));
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

        public BEFormaPago FormaPagoSeleccionar(Int32 pIDFormaPago)
        {
            SqlCommand cmd = ConexionCmd("gen.FormaPagoSeleccionar");
            BEFormaPago oBE = new BEFormaPago();
            cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = pIDFormaPago;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.NumeroDia = rd.GetInt32(rd.GetOrdinal("NumeroDia"));
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

        #endregion

        #region Transaccional

        public BERetornoTran FormaPagoGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.FormaPagoGuardar");
            cmd = LlenarEstructura(pEntidad, cmd, "I");
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

        public BERetornoTran FormaPagoActualizar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.FormaPagoActualizar");
            cmd = LlenarEstructura(pEntidad, cmd, "A");
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

        public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand cmd, String pTipoTransaccion)
        {
            BEFormaPago oBE = (BEFormaPago)pEntidad;
            cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = oBE.IDFormaPago;
            cmd.Parameters.Add("@Codigo", SqlDbType.Char, 2).Value = oBE.Codigo;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = oBE.Nombre;
            cmd.Parameters.Add("@NumeroDia", SqlDbType.Int).Value = oBE.NumeroDia;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return cmd;
        }
         
        #endregion

    }
}
