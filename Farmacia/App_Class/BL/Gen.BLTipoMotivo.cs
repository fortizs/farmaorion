using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.General
{
	public class BLTipoMotivo : BLBase
	{
		public IList TipoMotivoListar(Int32 pIDTipoComprobante)
		{
			SqlCommand cmd = ConexionCmd("gen.TipoMotivoListar");
			cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.VarChar, 100).Value = pIDTipoComprobante;
			BETipoMotivo oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BETipoMotivo();
					oBE.IDTipoMotivo = rd.GetInt32(rd.GetOrdinal("IDTipoMotivo"));
					oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("IDTipoComprobante"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.CodigoSunat = rd.GetString(rd.GetOrdinal("CodigoSunat"));
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