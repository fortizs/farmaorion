using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BE;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLModulo: BLBase 
    { 
        public IList ModuloListar()
        {
            SqlCommand cmd = ConexionCmd("seg.ModuloListar");
            BEModulo oBE;            
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEModulo();
                    oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));
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

        public IList ModuloListarxUsuario(Int32 pIDUsuario)
        {
            SqlCommand cmd = ConexionCmd("seg.ModuloListarxUsuario"); 
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario; 
            ArrayList lista = new ArrayList();
			BEModulo oBE;
			try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEModulo();
                    oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.Imagen = rd.GetString(rd.GetOrdinal("Imagen"));
                    oBE.Icono = rd.GetString(rd.GetOrdinal("Icono"));
                    oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
                    oBE.VisibleSinPermiso = rd.GetBoolean(rd.GetOrdinal("VisibleSinPermiso"));
                    oBE.Url = rd.GetString(rd.GetOrdinal("Url"));
                    oBE.Acceso = rd.GetBoolean(rd.GetOrdinal("Acceso"));
                    oBE.Orden = rd.GetInt32(rd.GetOrdinal("Orden"));
                    oBE.Espacio = rd.GetInt32(rd.GetOrdinal("Espacio"));
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

        public IList ModuloListarxPermiso(Int32 pIDUsuario)
        {
            SqlCommand cmd = ConexionCmd("seg.ModuloListarxPermiso");
            BEModulo oBE;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEModulo();
                    oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));
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
		 
        public BEModulo ModuloSeleccionar(Int32 pIDModulo, Int32 pIDIdioma)
        {
            SqlCommand cmd = ConexionCmd("seg.ModuloSeleccionar");
            BEModulo oBE = new BEModulo();
            cmd.Parameters.Add("@IDModulo", SqlDbType.Int).Value = pIDModulo;
            cmd.Parameters.Add("@IDIdioma", SqlDbType.Int).Value = pIDIdioma;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.Imagen = rd.GetString(rd.GetOrdinal("Imagen"));
                    oBE.Icono = rd.GetString(rd.GetOrdinal("Icono"));
                    oBE.VisibleSinPermiso = rd.GetBoolean(rd.GetOrdinal("VisibleSinPermiso"));
                    oBE.Url = rd.GetString(rd.GetOrdinal("Url"));
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
