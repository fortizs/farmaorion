using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLEstado : BLBase
	{
		public IList EstadoListar(String pGrupo)
		{
			SqlCommand cmd = ConexionCmd("gen.EstadoListar");
			cmd.Parameters.Add("@Grupo", SqlDbType.VarChar, 5).Value = pGrupo;

			BEEstado oBE;
			ArrayList lista = new ArrayList();
			try
			{ 
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEEstado();
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
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
