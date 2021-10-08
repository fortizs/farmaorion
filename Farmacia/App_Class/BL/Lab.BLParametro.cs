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
	public class BLParametro : BLBase
	{
		public IList ParametroListar(Int32 pIDProducto)
		{
			SqlCommand cmd = ConexionCmd("gen.ParametroListar");
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = pIDProducto;
			BEParametro oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEParametro();
					oBE.IDParametro = rd.GetInt32(rd.GetOrdinal("IDParametro"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.TipoResultado = rd.GetString(rd.GetOrdinal("TipoResultado"));
					oBE.TipoResultadoNombre = rd.GetString(rd.GetOrdinal("TipoResultadoNombre"));
					oBE.Unidad = rd.GetString(rd.GetOrdinal("Unidad"));
					oBE.ValorReferencial = rd.GetString(rd.GetOrdinal("ValorReferencial"));
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

		public BEParametro ParametroSeleccionar(Int32 pIDParametro)
		{
			SqlCommand cmd = ConexionCmd("gen.ParametroSeleccionar");
			BEParametro oBE = new BEParametro();
			cmd.Parameters.Add("@IDParametro", SqlDbType.Int).Value = pIDParametro;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDParametro = rd.GetInt32(rd.GetOrdinal("IDParametro"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.TipoResultado = rd.GetString(rd.GetOrdinal("TipoResultado"));
					oBE.TipoResultadoNombre = rd.GetString(rd.GetOrdinal("TipoResultadoNombre"));
					oBE.Unidad = rd.GetString(rd.GetOrdinal("Unidad"));
					oBE.Posicion = rd.GetInt32(rd.GetOrdinal("Posicion"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.ValorReferencial = rd.GetString(rd.GetOrdinal("ValorReferencial"));
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

		public BERetornoTran ParametroGuardar(BEParametro BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ParametroGuardar");
			cmd.Parameters.Add("@IDParametro", SqlDbType.Int).Value = BEParam.IDParametro;
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
			cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = BEParam.Nombre;
			cmd.Parameters.Add("@TipoResultado", SqlDbType.Char, 1).Value = BEParam.TipoResultado;
			cmd.Parameters.Add("@Unidad", SqlDbType.VarChar, 50).Value = BEParam.Unidad;
			cmd.Parameters.Add("@Posicion", SqlDbType.Int).Value = BEParam.Posicion;
			cmd.Parameters.Add("@ValorReferencial", SqlDbType.VarChar).Value = BEParam.ValorReferencial;
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

		public BERetornoTran ParametroEliminar(BEParametro BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ParametroEliminar");
			cmd.Parameters.Add("@IDParametro", SqlDbType.Int).Value = BEParam.IDParametro;
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