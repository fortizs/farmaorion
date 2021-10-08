using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Seguridad;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLMenu: BLBase 
    {
		public IList Listar(Int32 pIDUsuario, Int32 pIDIdioma)
		{
			BLMenu oDLModulo = new BLMenu();
			BEMenu oBEModulo = new BEMenu();
			oBEModulo.IDUsuario = pIDUsuario;
			oBEModulo.IDIdioma = pIDIdioma;
			return oDLModulo.Listar(oBEModulo);
		}
	 

		public IList Listar(BEBase pEntidad)
        {
            SqlCommand cmd = ConexionCmd("seg.MenuListarxUsuario");
            BEMenu oBE = (BEMenu)pEntidad;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;           
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEMenu();
                    oBE.IDMenu = rd.GetInt32(rd.GetOrdinal("IDMenu"));
                    oBE.IDMenuPadre = rd.GetInt32(rd.GetOrdinal("IDMenuPadre"));                    
                    oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));                    
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));                    
                    oBE.Url = rd.GetString(rd.GetOrdinal("Url"));
                    oBE.Orden = rd.GetInt32(rd.GetOrdinal("Orden"));
                    oBE.Visible = rd.GetBoolean(rd.GetOrdinal("Visible"));
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

		public IList MenuListarxModuloUsuario(Int32 pIDUsuario, Int32 pIDModulo)
		{
			SqlCommand cmd = ConexionCmd("seg.MenuListarxModuloUsuario");
			BEMenu oBE;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			cmd.Parameters.Add("@IDModulo", SqlDbType.Int).Value = pIDModulo;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEMenu();
					oBE.IDMenu = rd.GetInt32(rd.GetOrdinal("IDMenu"));
					oBE.IDMenuPadre = rd.GetInt32(rd.GetOrdinal("IDMenuPadre"));
					oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Url = rd.GetString(rd.GetOrdinal("Url"));
					oBE.Orden = rd.GetInt32(rd.GetOrdinal("Orden"));
					oBE.Visible = rd.GetBoolean(rd.GetOrdinal("Visible"));
					oBE.Icono = rd.GetString(rd.GetOrdinal("Icono"));

					
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

		public BEMenu ValidarMenu(BEMenu pEntidad)
        {
            SqlCommand cmd = ConexionCmd("seg.MenuValidarxUsuario");
            BEMenu oBE = (BEMenu)pEntidad;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("@Url", SqlDbType.VarChar,250).Value = oBE.Url; 
            
            try
            {
                cmd.Connection.Open(); 
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDMenu = rd.GetInt32(rd.GetOrdinal("IDMenu"));
                    oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.Modulo = rd.GetString(rd.GetOrdinal("Modulo"));
                    oBE.ModuloIcono = rd.GetString(rd.GetOrdinal("ModuloIcono"));
                    oBE.RutaNavegacion = rd.GetString(rd.GetOrdinal("RutaNavegacion"));                    
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
    
