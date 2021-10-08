using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLMarca : BLBase
    { 
        public IList Listar(Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.MarcaListar");
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            BEMarca oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEMarca();
                    oBE.IDMarca = rd.GetInt32(rd.GetOrdinal("IDMarca"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre")); 
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

        public IList MarcaFiltroListar(String pFiltro, Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.MarcaFiltroListar");
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            BEMarca oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEMarca();
                    oBE.IDMarca = rd.GetInt32(rd.GetOrdinal("IDMarca"));
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

        public BEMarca Seleccionar(int pCodigo)
        {
            SqlCommand cmd = ConexionCmd("gen.MarcaSeleccionar");
            BEMarca oBE = new BEMarca();
            cmd.Parameters.Add("@IDMarca", SqlDbType.Int).Value = pCodigo;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDMarca = rd.GetInt32(rd.GetOrdinal("IDMarca"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
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

        public BERetornoTran Insertar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.MarcaGuardar");
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

        public BERetornoTran Actualizar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.MarcaActualizar");
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
            BEMarca oBE = (BEMarca)pEntidad; 
            cmd.Parameters.Add("@IDMarca", SqlDbType.Int).Value = oBE.IDMarca;
            cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 200).Value = oBE.Codigo;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = oBE.Nombre;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;

            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return cmd;
        }

		public BERetornoTran MarcaEliminar(Int32 pIDMarca)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.MarcaEliminar");
			cmd.Parameters.Add("@IDMarca", SqlDbType.Int).Value = pIDMarca;
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
