using Farmacia.App_Class.BE.Facturacion;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Facturacion
{
	public class BLTipoPrecio : BLBase
	{
		public IList TipoPrecioListar()
		{
			SqlCommand cmd = ConexionCmd("fac.TipoPrecioListar");

			BETipoPrecio oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BETipoPrecio();
					oBE.IDTipoPrecio = rd.GetInt32(rd.GetOrdinal("IDTipoPrecio"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.CodigoSunat = rd.GetString(rd.GetOrdinal("CodigoSunat"));
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