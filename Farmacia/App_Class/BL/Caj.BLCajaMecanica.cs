using Farmacia.App_Class.BE.Caja;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Caja
{
	public class BLCajaMecanica : BLBase
	{
		#region Transaccional

		public BERetornoTran CajaMecanicaGuardar(BECajaMecanica BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CajaMecanicaGuardar");
			cmd.Parameters.Add("@IDCajaMecanica", SqlDbType.Int).Value = BEParam.IDCajaMecanica;
			cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 3).Value = BEParam.Codigo;
			cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = BEParam.Nombre;
			cmd.Parameters.Add("@Responsable", SqlDbType.VarChar, 200).Value = BEParam.Responsable;
			cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = BEParam.Estado;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;

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
		 
		public BERetornoTran CajaMecanicaEliminar(Int32 pIDCajaMecanica)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.CajaMecanicaEliminar");
			cmd.Parameters.Add("@IDCajaMecanica", SqlDbType.Int).Value = pIDCajaMecanica;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;

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

		#endregion

		#region NoTransaccional

		public IList CajaMecanicaListar(String pFiltro, Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("gen.CajaMecanicaListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			BECajaMecanica oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECajaMecanica();
					oBE.IDCajaMecanica = rd.GetInt32(rd.GetOrdinal("IDCajaMecanica"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Responsable = rd.GetString(rd.GetOrdinal("Responsable"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
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

		public BECajaMecanica CajaMecanicaSeleccionar(Int32 pIDCajaMecanica)
		{
			SqlCommand cmd = ConexionCmd("gen.CajaMecanicaSeleccionar");
			BECajaMecanica oBE = new BECajaMecanica();
			cmd.Parameters.Add("@IDCajaMecanica", SqlDbType.Int).Value = pIDCajaMecanica;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDCajaMecanica = rd.GetInt32(rd.GetOrdinal("IDCajaMecanica"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Responsable = rd.GetString(rd.GetOrdinal("Responsable"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
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
		 
		#endregion

	}
}