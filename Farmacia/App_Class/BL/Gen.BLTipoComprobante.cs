using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLTipoComprobante : BLBase
    { 
        public IList TipoComprobanteListar(String pInd, String pAccion)
        {
            SqlCommand cmd = ConexionCmd("gen.TipoComprobanteListar");
            cmd.Parameters.Add("@IndDoc", SqlDbType.VarChar, 10).Value = pInd;
			cmd.Parameters.Add("@Accion", SqlDbType.VarChar, 10).Value = pAccion;
			BETipoComprobante oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BETipoComprobante();
                    oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("IDTipoComprobante"));
                    oBE.Sigla = rd.GetString(rd.GetOrdinal("Sigla"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.CodigoSunat = rd.GetString(rd.GetOrdinal("CodigoSunat"));
                    oBE.IDTipoComprobanteContabilidad = rd.GetInt32(rd.GetOrdinal("IDTipoComprobanteContabilidad"));
                    oBE.IndDoc = rd.GetString(rd.GetOrdinal("IndDoc"));
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

        public BETipoComprobante TipoComprobanteSeleccionar(Int32 pIDTipoComprobante)
        {
            SqlCommand cmd = ConexionCmd("gen.TipoComprobanteSeleccionar");
            BETipoComprobante oBE = new BETipoComprobante();
            cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int).Value = pIDTipoComprobante;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("IDTipoComprobante"));
                    oBE.Sigla = rd.GetString(rd.GetOrdinal("Sigla"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.CodigoSunat = rd.GetString(rd.GetOrdinal("CodigoSunat"));
                    oBE.IDTipoComprobanteContabilidad = rd.GetInt32(rd.GetOrdinal("IDTipoComprobanteContabilidad"));
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

        public BERetornoTran TipoComprobanteGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.TipoComprobanteGuardar");
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

        public BERetornoTran TipoComprobanteActualizar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.TipoComprobanteActualizar");
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
            BETipoComprobante oBE = (BETipoComprobante)pEntidad;
            cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int).Value = oBE.IDTipoComprobante;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar,500).Value = oBE.Nombre;
            cmd.Parameters.Add("@Sigla", SqlDbType.VarChar).Value = oBE.Sigla; 
            cmd.Parameters.Add("@CodigoSunat", SqlDbType.VarChar).Value = oBE.CodigoSunat;
            cmd.Parameters.Add("@IndDoc", SqlDbType.VarChar).Value = oBE.IndDoc;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
            cmd.Parameters.Add("@IDTipoComprobanteContabilidad", SqlDbType.Int).Value = oBE.IDTipoComprobanteContabilidad; 
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return cmd;
             
        }
         
        public BERetornoTran TipoComprobanteEliminar(Int32 pIDTipoComprobante, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.TipoComprobanteEliminar");
            cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int).Value = pIDTipoComprobante; 
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;

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
         

    }
}
