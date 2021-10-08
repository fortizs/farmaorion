using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Laboratorio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.Laboratorio
{
	public class BLResultadoPredefinido : BLBase
	{
		public IList ResultadoPredefinidoListar(Int32 pIDGenerico, String pGenerico)
		{
			SqlCommand cmd = ConexionCmd("gen.ResultadoPredefinidoListar");
			cmd.Parameters.Add("@IDGenerico", SqlDbType.Int).Value = pIDGenerico;
			cmd.Parameters.Add("@Generico", SqlDbType.VarChar).Value = pGenerico;
			BEResultadoPredefinido oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEResultadoPredefinido();
					oBE.IDResultadoPredefinido = rd.GetInt32(rd.GetOrdinal("IDResultadoPredefinido"));
					oBE.IDGenerico = rd.GetInt32(rd.GetOrdinal("IDGenerico"));
					oBE.Generico = rd.GetString(rd.GetOrdinal("Generico"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Posicion = rd.GetInt32(rd.GetOrdinal("Posicion"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
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

		public BEResultadoPredefinido ResultadoPredefinidoSeleccionar(Int32 pIDResultadoPredefinido)
		{
			SqlCommand cmd = ConexionCmd("gen.ResultadoPredefinidoSeleccionar");
			BEResultadoPredefinido oBE = new BEResultadoPredefinido();
			cmd.Parameters.Add("@IDResultadoPredefinido", SqlDbType.Int).Value = pIDResultadoPredefinido;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDResultadoPredefinido = rd.GetInt32(rd.GetOrdinal("IDResultadoPredefinido"));
					oBE.IDGenerico = rd.GetInt32(rd.GetOrdinal("IDGenerico"));
					oBE.Generico = rd.GetString(rd.GetOrdinal("Generico"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Posicion = rd.GetInt32(rd.GetOrdinal("Posicion"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
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

		public BERetornoTran ResultadoPredefinidoGuardar(BEResultadoPredefinido BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ResultadoPredefinidoGuardar");
			cmd.Parameters.Add("@IDResultadoPredefinido", SqlDbType.Int).Value = BEParam.IDResultadoPredefinido;
			cmd.Parameters.Add("@IDGenerico", SqlDbType.Int).Value = BEParam.IDGenerico;
			cmd.Parameters.Add("@Generico", SqlDbType.VarChar, 100).Value = BEParam.Generico;
			cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = BEParam.Nombre;
			cmd.Parameters.Add("@Posicion", SqlDbType.Int).Value = BEParam.Posicion;
			cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = BEParam.Estado;
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

		public BERetornoTran ResultadoPredefinidoEliminar(BEResultadoPredefinido BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ResultadoPredefinidoEliminar");
			cmd.Parameters.Add("@IDResultadoPredefinido", SqlDbType.Int).Value = BEParam.IDResultadoPredefinido;
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

	}
}
