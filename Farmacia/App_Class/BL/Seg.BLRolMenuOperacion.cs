
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Seguridad;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLRolMenuOperacion : BLBase
    { 
        public IList Listar(int pIDRol, int pIDMenu, int pIDUsuario)
        {
            SqlCommand cmd = ConexionCmd("seg.RolMenuOperacionListar");
            cmd.Parameters.Add("@IDRol", SqlDbType.Int).Value = pIDRol;            
            cmd.Parameters.Add("@IDMenu", SqlDbType.Int).Value = pIDMenu;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    BERolMenuOperacion oBE = new BERolMenuOperacion();                    
                    oBE.IDMenu = rd.GetInt32(rd.GetOrdinal("IDMenu"));
                    oBE.IDOperacion = rd.GetInt32(rd.GetOrdinal("IDOperacion"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.Acceso = rd.GetBoolean(rd.GetOrdinal("Estado"));
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


        public IList ListarOperacionPermiso(int pIDUsuario, int pIDMenu)
        {
            SqlCommand cmd = ConexionCmd("seg.RolMenuOperacionPermisoListar");
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
            cmd.Parameters.Add("@IDMenu", SqlDbType.Int).Value = pIDMenu;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    BERolMenuOperacion oBE = new BERolMenuOperacion();
                    oBE.IDUsuario = rd.GetInt32(rd.GetOrdinal("IDUsuario"));
                    oBE.IDMenu = rd.GetInt32(rd.GetOrdinal("IDMenu"));
                    oBE.IDRol = rd.GetInt32(rd.GetOrdinal("IDRol"));
                    oBE.IDOperacion = rd.GetInt32(rd.GetOrdinal("IDOperacion"));
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
        

        public BEBase Acceso(BEBase pEntidad)
        {
            SqlCommand cmd = ConexionCmd("seg.RolMenuOperacionSeleccionar");
            BERolMenuOperacion oBE = (BERolMenuOperacion)pEntidad;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("@IDMenu", SqlDbType.Int).Value = oBE.IDMenu;
            cmd.Parameters.Add("@IDOperacion", SqlDbType.Int).Value = oBE.IDOperacion;
            cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = oBE.IDProducto;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.Acceso = rd.GetBoolean(rd.GetOrdinal("Acceso"));
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
      
    }
}
