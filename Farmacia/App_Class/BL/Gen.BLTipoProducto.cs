using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLTipoProducto : BLBase
	{ 
		public IList TipoProductoListar(String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("gen.TipoProductoListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
			BETipoProducto oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BETipoProducto();
					oBE.IDTipoProducto = rd.GetInt32(rd.GetOrdinal("IDTipoProducto"));
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
		 
	}
}