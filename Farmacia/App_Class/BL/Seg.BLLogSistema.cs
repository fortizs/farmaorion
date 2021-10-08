
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Seguridad;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLLogSistema : BLBase 
    { 
        public IList Listar(Int32 IDModulo, String TipoFiltro, String Buscar, DateTime FechaDesde, DateTime FechaHasta)
        {
            SqlCommand cmd = ConexionCmd("seg.LogSistemaBuscar");
            BELogSistema oBE;
            cmd.Parameters.Add("@IDModulo", SqlDbType.Int).Value = IDModulo;
            cmd.Parameters.Add("@TipoFiltro", SqlDbType.VarChar, 50).Value =  TipoFiltro;
            cmd.Parameters.Add("@Buscar", SqlDbType.VarChar, 250).Value = Buscar;
            cmd.Parameters.Add("@FechaDesde", SqlDbType.DateTime).Value = FechaDesde;
            cmd.Parameters.Add("@FechaHasta", SqlDbType.DateTime).Value = FechaHasta;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BELogSistema();
                    oBE.IDLogSistema = rd.GetInt32(rd.GetOrdinal("IDLogSistema"));
                    oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));
                    oBE.Modulo = rd.GetString(rd.GetOrdinal("Modulo"));
                    oBE.Compilado = rd.GetString(rd.GetOrdinal("Compilado"));
                    oBE.Host = rd.GetString(rd.GetOrdinal("Host"));
                    oBE.IDUsuario = rd.GetInt32(rd.GetOrdinal("IDUsuario"));
                    oBE.Usuario = rd.GetString(rd.GetOrdinal("Usuario"));
                    oBE.Opcion = rd.GetString(rd.GetOrdinal("Opcion"));
                    oBE.Evento = rd.GetString(rd.GetOrdinal("Evento"));
                    oBE.MensajeError = rd.GetString(rd.GetOrdinal("Error"));
                    oBE.Fecha = rd.GetDateTime(rd.GetOrdinal("Fecha"));                    
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
		   
        public BELogSistema Seleccionar(int pCodigo)
        {
            SqlCommand cmd = ConexionCmd("seg.LogSistemaSeleccionar");
            BELogSistema oBE = new BELogSistema();
            cmd.Parameters.Add("@IDLogSistema", SqlDbType.Int).Value = pCodigo;                        
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {                   
                    oBE.IDLogSistema = rd.GetInt32(rd.GetOrdinal("IDLogSistema"));
                    oBE.IDModulo = rd.GetInt32(rd.GetOrdinal("IDModulo"));
                    oBE.Modulo = rd.GetString(rd.GetOrdinal("Modulo"));
                    oBE.Compilado = rd.GetString(rd.GetOrdinal("Compilado"));
                    oBE.Host = rd.GetString(rd.GetOrdinal("Host"));
                    oBE.IDUsuario = rd.GetInt32(rd.GetOrdinal("IDUsuario"));
                    oBE.Usuario = rd.GetString(rd.GetOrdinal("Usuario"));
                    oBE.Opcion = rd.GetString(rd.GetOrdinal("Opcion"));
                    oBE.Evento = rd.GetString(rd.GetOrdinal("Evento"));
                    oBE.MensajeError = rd.GetString(rd.GetOrdinal("Error"));
                    oBE.Fecha = rd.GetDateTime(rd.GetOrdinal("Fecha"));
                    oBE.Detalle = rd.GetString(rd.GetOrdinal("Detalle"));                    
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
			SqlCommand cmd = ConexionCmd("seg.LogSistemaInsertar");
			cmd = LlenarEstructura(pEntidad, cmd, "I");
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
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
			BELogSistema oBE = (BELogSistema)pEntidad;
			pcmd.Parameters.Add("@IDModulo", SqlDbType.Int).Value = oBE.IDModulo;
			pcmd.Parameters.Add("@Compilado", SqlDbType.VarChar, 10).Value = oBE.Compilado;
			pcmd.Parameters.Add("@Host", SqlDbType.VarChar, 50).Value = oBE.Host;
			pcmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
			pcmd.Parameters.Add("@Opcion", SqlDbType.VarChar, 250).Value = oBE.Opcion;
			pcmd.Parameters.Add("@Evento", SqlDbType.VarChar, 250).Value = oBE.Evento;
			pcmd.Parameters.Add("@Error", SqlDbType.VarChar, 8000).Value = oBE.MensajeError;
			pcmd.Parameters.Add("@Detalle", SqlDbType.VarChar, 8000).Value = oBE.Detalle;
			pcmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			return pcmd;
		}

		#endregion

	}
}
