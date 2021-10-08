using Farmacia.App_Class.BE.Vehicular;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacia.App_Class.BL.Vehicular
{
	public class BLTipoVehiculo : BLBase
	{
		public IList Listar()
		{
			SqlCommand cmd = ConexionCmd("veh.TipoVehiculoListar");
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					BETipoVehiculo oBE = new BETipoVehiculo();
					oBE.IDTipoVehiculo = rd.GetInt32(rd.GetOrdinal("IDTipoVehiculo"));
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

		public IList ListarxModelo(Int32 pIDModelo)
		{
			SqlCommand cmd = ConexionCmd("veh.TipoVehiculoListarxModelo");
			cmd.Parameters.Add("@IDModelo", SqlDbType.Int).Value = pIDModelo;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					BETipoVehiculo oBE = new BETipoVehiculo();
					oBE.IDTipoVehiculo = rd.GetInt32(rd.GetOrdinal("IDTipoVehiculo"));
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
	}
}