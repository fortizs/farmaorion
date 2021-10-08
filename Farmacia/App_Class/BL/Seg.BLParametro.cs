
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Seguridad;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Seguridad
{
	public class BLParametro : BLBase
	{ 
		public IList ParametroListar(String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("seg.ParametroListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar).Value = pFiltro;
			ArrayList lista = new ArrayList();
			BEParametro oBE;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEParametro();
					oBE.IDParametro = rd.GetString(rd.GetOrdinal("IDParametro"));
					oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
					oBE.ValorDefecto = rd.GetString(rd.GetOrdinal("ValorDefecto")); 
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
		 
		public BEParametro ParametroSeleccionar(String pIDParametro)
		{
			SqlCommand cmd = ConexionCmd("seg.ParametroSeleccionar");
			BEParametro oBE = new BEParametro();
			cmd.Parameters.Add("@IDParametro", SqlDbType.VarChar).Value = pIDParametro; 
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDParametro = rd.GetString(rd.GetOrdinal("IDParametro"));
					oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
					oBE.ValorDefecto = rd.GetString(rd.GetOrdinal("ValorDefecto"));
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
		 
		public BERetornoTran ParametroGuardar(BEParametro oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.ParametroGuardar");
			cmd.Parameters.Add("@IDParametro", SqlDbType.VarChar).Value = oBE.IDParametro;
			cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = oBE.Descripcion;
			cmd.Parameters.Add("@ValorDefecto", SqlDbType.VarChar, 200).Value = oBE.ValorDefecto; 
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			   
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
		  
	}
}
