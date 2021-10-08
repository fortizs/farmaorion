
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLMoneda : BLBase
    { 
        public IList MonedaListar()
        {
            SqlCommand cmd = ConexionCmd("gen.MonedaListar"); 
            BEMoneda oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEMoneda();
                    oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.NombreCorto = rd.GetString(rd.GetOrdinal("NombreCorto"));
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

        public BEMoneda MonedaSeleccionar(String pCodigo)
        {
            SqlCommand cmd = ConexionCmd("gen.MonedaSeleccionar");
            BEMoneda oBE = new BEMoneda();
            cmd.Parameters.Add("@IDMoneda", SqlDbType.VarChar).Value = pCodigo;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.NombreCorto = rd.GetString(rd.GetOrdinal("NombreCorto"));
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

        public BERetornoTran MonedaGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.MonedaGuardar");
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

        public BERetornoTran MonedaActualizar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.MonedaActualizar");
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
            BEMoneda oBE = (BEMoneda)pEntidad;
            cmd.Parameters.Add("@IDMoneda", SqlDbType.VarChar,3).Value = oBE.IDMoneda; 
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = oBE.Nombre;
            cmd.Parameters.Add("@NombreCorto", SqlDbType.VarChar, 10).Value = oBE.NombreCorto;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado; 

            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return cmd;
        }

    }
}
