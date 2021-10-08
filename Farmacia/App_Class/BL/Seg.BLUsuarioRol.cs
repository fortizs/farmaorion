using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Seguridad;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLUsuarioRol: BLBase 
    {
		public IList Listar(Int32 pIDEmpresa, Int32 pIDPerfil, Int32 pIDUsuario)
		{
			BEUsuarioRol oBE = new BEUsuarioRol();
			oBE.IDUsuario = pIDUsuario;
			oBE.IDEmpresa = pIDEmpresa;
			oBE.IDPerfil = pIDPerfil;
			return new BLUsuarioRol().Listar(oBE);
		}

		public IList ListarPerfilRolAsignados(Int32 pIDEmpresa, Int32 pIDUsuario)
		{
			BEUsuarioRol oBE = new BEUsuarioRol();
			oBE.IDUsuario = pIDUsuario;
			oBE.IDEmpresa = pIDEmpresa;
			return new BLUsuarioRol().ListarPerfilRolAsignados(oBE);
		}

		public IList ListarPerfilRolDisponibles(Int32 pIDUsuario, String pIDRoles)
		{
			BEUsuarioRol oBE = new BEUsuarioRol();
			oBE.IDUsuario = pIDUsuario;
			oBE.IDRoles = pIDRoles;
			return new BLUsuarioRol().ListarPerfilRolDisponibles(oBE);
		}

		public IList Listar(BEBase pEntidad)
        {
            SqlCommand cmd = ConexionCmd("seg.UsuarioRolListar");
            BEUsuarioRol oBE = (BEUsuarioRol)pEntidad;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("@IDPerfil", SqlDbType.Int).Value = oBE.IDPerfil;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEUsuarioRol();
                    oBE.IDRol = rd.GetInt32(rd.GetOrdinal("IDRol"));
                    oBE.IDUsuario = rd.GetInt32(rd.GetOrdinal("IDUsuario"));
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

        public IList ListarPerfilRolAsignados(BEBase pEntidad)
        {
            SqlCommand cmd = ConexionCmd("seg.UsuarioPerfilRolListarAsignados");
            BEUsuarioRol oBE = (BEUsuarioRol)pEntidad;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEUsuarioRol();
                    oBE.IDRol = rd.GetInt32(rd.GetOrdinal("IDRol"));
                    oBE.Perfil = rd.GetString(rd.GetOrdinal("Perfil"));
                    oBE.Rol = rd.GetString(rd.GetOrdinal("Rol"));
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

        public IList ListarPerfilRolDisponibles(BEBase pEntidad)
        {
            SqlCommand cmd = ConexionCmd("seg.UsuarioPerfilRolListarDisponibles");
            BEUsuarioRol oBE = (BEUsuarioRol)pEntidad; 
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;  
            cmd.Parameters.Add("@IDRoles", SqlDbType.VarChar, 100).Value = oBE.IDRoles;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEUsuarioRol();
                    oBE.IDRol = rd.GetInt32(rd.GetOrdinal("IDRol"));
                    oBE.Perfil = rd.GetString(rd.GetOrdinal("Perfil"));
                    oBE.Rol = rd.GetString(rd.GetOrdinal("Rol"));
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
 
    }
}
    