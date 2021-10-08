using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLUnidadMedida : BLBase
    {

        public IList UnidadMedidaListar(String pFiltro, Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.UnidadMedidaListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar,200).Value = pFiltro;
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            BEUnidadMedida oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEUnidadMedida();
                    oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.NombreCorto = rd.GetString(rd.GetOrdinal("NombreCorto"));
					oBE.CodigoSunat = rd.GetString(rd.GetOrdinal("CodigoSunat"));
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
		 
        public BEUnidadMedida UnidadMedidaSeleccionar(Int32 pCodigo)
        {
            SqlCommand cmd = ConexionCmd("gen.UnidadMedidaSeleccionar");
            BEUnidadMedida oBE = new BEUnidadMedida();
            cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int).Value = pCodigo;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.NombreCorto = rd.GetString(rd.GetOrdinal("NombreCorto"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.CodigoSunat = rd.GetString(rd.GetOrdinal("CodigoSunat")); 
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

        public BERetornoTran UnidadMedidaGuardar(BEUnidadMedida oBE)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.UnidadMedidaGuardar");
			cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int).Value = oBE.IDUnidadMedida;
			cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 200).Value = oBE.Codigo;
			cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = oBE.Nombre;
			cmd.Parameters.Add("@NombreCorto", SqlDbType.VarChar, 200).Value = oBE.NombreCorto;
			cmd.Parameters.Add("@CodigoSunat", SqlDbType.VarChar, 20).Value = oBE.CodigoSunat;
			cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
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
		 
		public BERetornoTran UnidadMedidaEliminar(Int32 pIDUnidadMedida)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.UnidadMedidaEliminar");
			cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int).Value = pIDUnidadMedida; 
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
