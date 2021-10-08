using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLCarroceria : BLBase
	{
		public IList CarroceriaListar(Int32 pIDEmpresa)
		{
			SqlCommand cmd = ConexionCmd("gen.CarroceriaListar");
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
			BECarroceria oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECarroceria();
					oBE.IDCarroceria = rd.GetInt32(rd.GetOrdinal("IDCarroceria"));
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

		public BECarroceria CarroceriaSeleccionar(Int32 pCodigo)
		{
			SqlCommand cmd = ConexionCmd("gen.CarroceriaSeleccionar");
			BECarroceria oBE = new BECarroceria();
			cmd.Parameters.Add("@IDCarroceria", SqlDbType.Int).Value = pCodigo;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDCarroceria = rd.GetInt32(rd.GetOrdinal("IDCarroceria"));
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

		public BERetornoTran CarroceriaGuardar(BECarroceria oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CarroceriaGuardar");
			cmd.Parameters.Add("@IDCarroceria", SqlDbType.Int).Value = oBE.IDCarroceria;
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