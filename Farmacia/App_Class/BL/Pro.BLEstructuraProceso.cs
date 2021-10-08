using Farmacia.App_Class.BE.Proceso;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Proceso
{
	public class BLEstructuraProceso : BLBase
	{
		public IList EstructuraProcesoListar(String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("pro.EstructuraProcesoListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar).Value = pFiltro; 
			BEEstructuraProceso oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEEstructuraProceso();
					oBE.IDEstructuraProceso = rd.GetInt32(rd.GetOrdinal("IDEstructuraProceso")); 
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Procedimiento = rd.GetString(rd.GetOrdinal("Procedimiento"));
					oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
					oBE.Extension = rd.GetString(rd.GetOrdinal("Extension"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.Cargar = rd.GetBoolean(rd.GetOrdinal("Cargar"));
					oBE.NombreHoja = rd.GetString(rd.GetOrdinal("NombreHoja"));

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
		 
		public BEEstructuraProceso EstructuraProcesoSeleccionar(Int32 pIDEstructuraProceso)
		{
			SqlCommand cmd = ConexionCmd("pro.EstructuraProcesoSeleccionar");
			BEEstructuraProceso oBE = new BEEstructuraProceso();
			cmd.Parameters.Add("@IDEstructuraProceso", SqlDbType.Int).Value = pIDEstructuraProceso; 
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDEstructuraProceso = rd.GetInt32(rd.GetOrdinal("IDEstructuraProceso"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Procedimiento = rd.GetString(rd.GetOrdinal("Procedimiento"));
					oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
					oBE.Extension = rd.GetString(rd.GetOrdinal("Extension"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.Cargar = rd.GetBoolean(rd.GetOrdinal("Cargar"));
					oBE.NombreHoja = rd.GetString(rd.GetOrdinal("NombreHoja"));
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