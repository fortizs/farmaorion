using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BE.General;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLRolMenu: BLBase 
    {   
        public IList Listar(Int32 pIDRol, Int32 pIDModulo, Int32 pIDUsuario)
        {
            SqlCommand cmd = ConexionCmd("seg.RolMenuListar");
           
            cmd.Parameters.Add("@IDRol", SqlDbType.Int).Value = pIDRol;
            cmd.Parameters.Add("@IDModulo", SqlDbType.Int).Value = pIDModulo;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
            ArrayList lista = new ArrayList();
			BERolMenu oBE;
			try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BERolMenu();
                    oBE.IDRol = rd.GetInt32(rd.GetOrdinal("IDRol"));
                    oBE.IDMenu = rd.GetInt32(rd.GetOrdinal("IDMenu"));
                    oBE.IDMenuPadre = rd.GetInt32(rd.GetOrdinal("IDMenuPadre"));                    
                    oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));                    
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.ConfigOperacion = rd.GetBoolean(rd.GetOrdinal("ConfigOperacion"));                     
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

		#region Transaccional
		public BERetornoTran Insertar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.RolMenuGrabar");
			cmd = LlenarEstructura(pEntidad, cmd, "I");
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
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
			BERolMenu oBE = (BERolMenu)pEntidad;
			pcmd.Parameters.Add("@IDRol", SqlDbType.Int).Value = oBE.IDRol;
			pcmd.Parameters.Add("@IDMenu", SqlDbType.Int).Value = oBE.IDMenu;
			pcmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
			pcmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
			pcmd.Parameters.Add("@Operaciones", SqlDbType.VarChar, 500).Value = oBE.Operaciones;
			return pcmd;
		}
		#endregion
	}
}
    