
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Seguridad;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLEvento : BLBase 
    { 
        public IList ListarxUsuario(Int32 pIDUsuario, Int32 pIDTipoEvento, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            SqlCommand cmd = ConexionCmd("seg.EventoListarxUsuario");
            BEEvento oBE = new BEEvento();
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
            cmd.Parameters.Add("@IDTipoEvento", SqlDbType.Int).Value = pIDTipoEvento;
            cmd.Parameters.Add("@FechaDesde", SqlDbType.DateTime).Value = pFechaDesde;
            cmd.Parameters.Add("@FechaHasta", SqlDbType.DateTime).Value = pFechaHasta;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEEvento();
                    oBE.IDEvento = rd.GetInt32(rd.GetOrdinal("IDEvento"));
                    oBE.IDTipoEvento = rd.GetInt32(rd.GetOrdinal("IDTipoEvento"));
                    oBE.TipoEvento = rd.GetString(rd.GetOrdinal("TipoEvento"));
                    oBE.FechaRegistro = rd.GetDateTime(rd.GetOrdinal("FechaRegistro"));
                    oBE.Detalle = rd.GetString(rd.GetOrdinal("Detalle"));
                    oBE.Host = rd.GetString(rd.GetOrdinal("Host"));
                    oBE.HostDetalles = rd.GetString(rd.GetOrdinal("HostDetalles"));
                    oBE.Navegador = rd.GetString(rd.GetOrdinal("Navegador"));
                    oBE.NavegadorDetalles = rd.GetString(rd.GetOrdinal("NavegadorDetalles"));
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
        
        public IList ListaTipoEvento()
        {
            SqlCommand cmd = ConexionCmd("seg.TipoEventoListar");
            BEEvento oBE = new BEEvento();
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEEvento();
                    oBE.IDTipoEvento = rd.GetInt32(rd.GetOrdinal("IDTipoEvento"));
                    oBE.TipoEvento = rd.GetString(rd.GetOrdinal("Nombre"));
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
        
        public BEEvento SeleccionarUltimo(Int32 pIDUsuario, Int32 pIDTipoEvento)
        {            
            SqlCommand cmd = ConexionCmd("seg.EventoSeleccionarUltimo");
            BEEvento oBE = new BEEvento();
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
            cmd.Parameters.Add("@IDTipoEvento", SqlDbType.Int).Value = pIDTipoEvento;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    
                    oBE.IDEvento = rd.GetInt32(rd.GetOrdinal("IDEvento"));
                    oBE.Detalle = rd.GetString(rd.GetOrdinal("Detalle"));  
                    oBE.FechaRegistro = rd.GetDateTime(rd.GetOrdinal("FechaRegistro"));                
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

		public bool PrimerLogin(Int32 pIDUsuario)
		{
			BEEvento oBE = new BLEvento().SeleccionarUltimo(pIDUsuario, 4);
			if (oBE.IDEvento == 0)
			{
				//Si no se encontró ningún inicio de sesión
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool ClaveCaducada(Int32 pIDUsuario, Int32 pNroDiasCaduca)
		{
			BEEvento oBE = new BLEvento().SeleccionarUltimo(pIDUsuario, 4);
			DateTime hoy = DateTime.Today;
			int NroDias = ((TimeSpan)(hoy - oBE.FechaRegistro)).Days;
			if (NroDias > pNroDiasCaduca)
			{
				//Si el número de días desde el último cambio de contraseña es mayor a los días en que la contraseña caduca
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool ClaveModificadaHoy(Int32 pIDUsuario)
		{
			BEEvento oBE = new BLEvento().SeleccionarUltimo(pIDUsuario, 4);
			if (oBE.IDEvento != 0)
			{
				if (oBE.FechaRegistro.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		#region Transaccional

		public BERetornoTran Insertar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.EventoInsertar");
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

		public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand pcmd, String pTipoTransaccion)
		{
			BEEvento oBE = (BEEvento)pEntidad;
			if (pTipoTransaccion == "I")  // Insertar
			{
				pcmd.Parameters.Add("@IDTipoEvento", SqlDbType.Int).Value = oBE.IDTipoEvento;
				pcmd.Parameters.Add("@Detalle", SqlDbType.VarChar, 500).Value = oBE.Detalle;
				pcmd.Parameters.Add("@Host", SqlDbType.VarChar, 50).Value = oBE.Host;
				pcmd.Parameters.Add("@HostDetalles", SqlDbType.VarChar, 500).Value = oBE.HostDetalles;
				pcmd.Parameters.Add("@Navegador", SqlDbType.VarChar, 250).Value = oBE.Navegador;
				pcmd.Parameters.Add("@NavegadorDetalles", SqlDbType.VarChar, 1000).Value = oBE.NavegadorDetalles;
			}
			pcmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
			pcmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			pcmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			return pcmd;
		}

		#endregion
	}
}
