using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLProveedorCategoria : BLBase
    { 
        public IList ProveedorCategoriaListar(Int32 pIDProveedor)
        {
            SqlCommand cmd = ConexionCmd("gen.ProveedorCategoriaListar");
            cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = pIDProveedor;
            BEProveedorCategoria oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEProveedorCategoria();
                    oBE.IDProveedorCategoria = rd.GetInt32(rd.GetOrdinal("IDProveedorCategoria"));
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
                    oBE.IDCategoria = rd.GetInt32(rd.GetOrdinal("IDCategoria"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
                    oBE.Categoria = rd.GetString(rd.GetOrdinal("Categoria")); 
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
 
        public BERetornoTran ProveedorCategoriaGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.ProveedorCategoriaGuardar");
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
      
        public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand cmd, String pTipoTransaccion)
        {
            BEProveedorCategoria oBE = (BEProveedorCategoria)pEntidad;
            cmd.Parameters.Add("@IDProveedorCategoria", SqlDbType.Int).Value = oBE.IDProveedorCategoria;
            cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = oBE.IDProveedor;
            cmd.Parameters.Add("@IDCategoria", SqlDbType.Int).Value = oBE.IDCategoria; 
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return cmd;
        }

    }
}
