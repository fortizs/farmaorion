using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLTipoCombustible : BLBase
	{
		public IList TipoCombustibleListar(Int32 pIDEmpresa)
		{
			SqlCommand cmd = ConexionCmd("gen.TipoCombustibleListar");
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
			BETipoCombustible oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BETipoCombustible();
					oBE.IDTipoCombustible = rd.GetInt32(rd.GetOrdinal("IDTipoCombustible"));
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

		public BETipoCombustible TipoCombustibleSeleccionar(Int32 pCodigo)
		{
			SqlCommand cmd = ConexionCmd("gen.TipoCombustibleSeleccionar");
			BETipoCombustible oBE = new BETipoCombustible();
			cmd.Parameters.Add("@IDTipoCombustible", SqlDbType.Int).Value = pCodigo;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDTipoCombustible = rd.GetInt32(rd.GetOrdinal("IDTipoCombustible"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
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

		public BERetornoTran TipoCombustibleGuardar(BETipoCombustible oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.TipoCombustibleGuardar");
			cmd.Parameters.Add("@IDTipoCombustible", SqlDbType.Int).Value = oBE.IDTipoCombustible;
			cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = oBE.Nombre;
			cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
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
