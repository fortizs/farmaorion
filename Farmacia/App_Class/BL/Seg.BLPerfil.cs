using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLPerfil : BLBase 
    { 
        public IList Listar(String pFiltro)
        {
            SqlCommand cmd = ConexionCmd("seg.PerfilListar");
            BEPerfil oBE;
            cmd.Parameters.Add("@Buscar", SqlDbType.VarChar, 50).Value = pFiltro;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEPerfil();
                    oBE.IDPerfil = rd.GetInt32(rd.GetOrdinal("IDPerfil"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));                    
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.UsuarioModificacion = rd.GetString(rd.GetOrdinal("UsuarioModificacion"));
					oBE.FechaModificacion = rd.GetDateTime(rd.GetOrdinal("FechaModificacion"));
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

        public IList Listar(int pCodigo)
        {
            SqlCommand cmd = ConexionCmd("seg.PerfilListarxUsuario");            
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pCodigo;
            ArrayList lista = new ArrayList();
            BEPerfil oBE;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEPerfil();
                    oBE.IDPerfil = rd.GetInt32(rd.GetOrdinal("IDPerfil"));
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

        public IList ListarxEmpresa(Int32 pIDEmpresa, Int32 pIDUsuario)
        {
            SqlCommand cmd = ConexionCmd("seg.PerfilListarxEmpresa");
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
            ArrayList lista = new ArrayList();
            BEPerfil oBE;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEPerfil();
                    oBE.IDPerfil = rd.GetInt32(rd.GetOrdinal("IDPerfil"));
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
       
        public BEPerfil Seleccionar(int pCodigo)
        {
            SqlCommand cmd = ConexionCmd("seg.PerfilSeleccionar");
            BEPerfil oBE = new BEPerfil();
            cmd.Parameters.Add("@IDPerfil", SqlDbType.Int).Value = pCodigo;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDPerfil = rd.GetInt32(rd.GetOrdinal("IDPerfil"));
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

		#region Transaccional

		public BERetornoTran Insertar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.PerfilInsertar");
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
			SqlCommand cmd = ConexionCmd("seg.PerfilActualizar");
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

		public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand pcmd, String pTipoTransaccion)
		{
			BEPerfil oBE = (BEPerfil)pEntidad;
			if (pTipoTransaccion == "I")  // Insertar
			{
			}
			if (pTipoTransaccion == "A" || pTipoTransaccion == "E")  // Actualizar - Eliminar
			{
				pcmd.Parameters.Add("@IDPerfil", SqlDbType.Int).Value = oBE.IDPerfil;
			}
			if (pTipoTransaccion == "A" || pTipoTransaccion == "I")  // Actualizar - Insertar
			{
				pcmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = oBE.Nombre;
				pcmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
			}
			pcmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
			pcmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			pcmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			return pcmd;
		}

		#endregion

	}
}
