using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BE.General;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLPerfilMenu: BLBase 
    {   
        public IList Listar(BEBase pEntidad)
        {
            SqlCommand cmd = ConexionCmd("seg.PerfilMenuListar");
            BEPerfilMenu oBE = (BEPerfilMenu)pEntidad;
            cmd.Parameters.Add("@IDPerfil", SqlDbType.Int).Value = oBE.IDPerfil;
            cmd.Parameters.Add("@IDModulo", SqlDbType.Int).Value = oBE.IDModulo;
            cmd.Parameters.Add("@IDPais", SqlDbType.Char,2).Value = oBE.IDPais;
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEPerfilMenu();
                    oBE.IDPerfil = rd.GetInt32(rd.GetOrdinal("IDPerfil"));
                    oBE.IDMenu = rd.GetInt32(rd.GetOrdinal("IDMenu"));
                    oBE.IDMenuPadre = rd.GetInt32(rd.GetOrdinal("IDMenuPadre"));                    
                    oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));
                    oBE.IDPais = rd.GetString(rd.GetOrdinal("IDPais"));
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

		#region Transaccional

		public BERetornoTran Insertar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.PerfilMenuGrabar");
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
			BEPerfilMenu oBE = (BEPerfilMenu)pEntidad;
			pcmd.Parameters.Add("@IDPerfil", SqlDbType.Int).Value = oBE.IDPerfil;
			pcmd.Parameters.Add("@IDMenu", SqlDbType.Int).Value = oBE.IDMenu;
			pcmd.Parameters.Add("@IDPais", SqlDbType.Char, 2).Value = oBE.IDPais;
			pcmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
			pcmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
			return pcmd;
		}

		#endregion

	}
}
    