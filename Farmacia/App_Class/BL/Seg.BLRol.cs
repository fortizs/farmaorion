using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BE.General;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLRol : BLBase 
    {  
        public IList Listar(BERol pEntidad)
        {
            SqlCommand cmd = ConexionCmd("seg.RolListar");
            BERol oBE = (BERol)pEntidad;
            cmd.Parameters.Add("@Buscar", SqlDbType.VarChar, 50).Value = oBE.Buscar;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
            cmd.Parameters.Add("@IDPerfil", SqlDbType.Int).Value = oBE.IDPerfil;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BERol();
                    oBE.IDRol = rd.GetInt32(rd.GetOrdinal("IDRol"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre")); 
                    oBE.Empresa = rd.GetString(rd.GetOrdinal("Empresa"));
                    oBE.Perfil = rd.GetString(rd.GetOrdinal("Perfil"));                    
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
         
        public BERol Seleccionar(int pCodigo)
        {
            SqlCommand cmd = ConexionCmd("seg.RolSeleccionar");
            BERol oBE = new BERol();
            cmd.Parameters.Add("@IDRol", SqlDbType.Int).Value = pCodigo;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDRol = rd.GetInt32(rd.GetOrdinal("IDRol")); 
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
			SqlCommand cmd = ConexionCmd("seg.RolInsertar");
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
			SqlCommand cmd = ConexionCmd("seg.RolActualizar");
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

		public BERetornoTran Eliminar(Int32 idRol)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.RolEliminar");
			cmd.Parameters.Add("@IDRol", SqlDbType.Int).Value = idRol;
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

		public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand pcmd, String pTipoTransaccion)
		{
			BERol oBE = (BERol)pEntidad;
			if (pTipoTransaccion == "A" || pTipoTransaccion == "E")  // Actualizar - Eliminar
			{
				pcmd.Parameters.Add("@IDRol", SqlDbType.Int).Value = oBE.IDRol;
			}
			if (pTipoTransaccion == "I")  // Actualizar - Insertar
			{
				pcmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
				pcmd.Parameters.Add("@IDPerfil", SqlDbType.Int).Value = oBE.IDPerfil;
				pcmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = oBE.Nombre;
				pcmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
			}
			if (pTipoTransaccion == "A")  // Actualizar - Insertar
			{
				pcmd.Parameters.Add("@IDPerfil", SqlDbType.Int).Value = oBE.IDPerfil;
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
