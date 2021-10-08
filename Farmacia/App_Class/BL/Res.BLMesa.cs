using Farmacia.App_Class.BE.Restaurante;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Restaurante
{
	public class BLMesa : BLBase
	{
		public IList MesaxUsuarioListar(Int32 pIDUsuario)
		{
			SqlCommand cmd = ConexionCmd("res.MesaxUsuarioListar");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			BEMesa oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEMesa();
					oBE.IDMesa = rd.GetInt32(rd.GetOrdinal("IDMesa"));
					oBE.Numero = rd.GetInt32(rd.GetOrdinal("Numero"));
					oBE.IDPedido = rd.GetInt32(rd.GetOrdinal("IDPedido"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
					oBE.Estado = rd.GetString(rd.GetOrdinal("Estado"));
					oBE.EstadoCodigo = rd.GetString(rd.GetOrdinal("EstadoCodigo"));
					oBE.EstadoColor = rd.GetString(rd.GetOrdinal("EstadoColor"));
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


		public BEMesa MesaSeleccionar(Int32 pIDMesa)
		{
			SqlCommand cmd = ConexionCmd("res.MesaSeleccionar");
			BEMesa oBE = new BEMesa();
			cmd.Parameters.Add("@IDMesa", SqlDbType.Int).Value = pIDMesa;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDMesa = rd.GetInt32(rd.GetOrdinal("IDMesa")); 
					oBE.Numero = rd.GetInt32(rd.GetOrdinal("Numero"));  
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