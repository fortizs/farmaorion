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
	public class BLParametroItem : BLBase
	{
		public IList ParametroItemListar(Int32 pIDParametro)
		{
			SqlCommand cmd = ConexionCmd("gen.ParametroItemListar");
			cmd.Parameters.Add("@IDParametro", SqlDbType.Int).Value = pIDParametro;
			BEParametroItem oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEParametroItem();
					oBE.IDParametroItem = rd.GetInt32(rd.GetOrdinal("IDParametroItem"));
					oBE.IDParametro = rd.GetInt32(rd.GetOrdinal("IDParametro"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Tipo = rd.GetString(rd.GetOrdinal("Tipo"));
					oBE.TipoNombre = rd.GetString(rd.GetOrdinal("TipoNombre"));
					oBE.Unidad = rd.GetString(rd.GetOrdinal("Unidad"));
					oBE.Posicion = rd.GetInt32(rd.GetOrdinal("Posicion"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.ValorReferencial = rd.GetString(rd.GetOrdinal("ValorReferencial"));
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

		public BEParametroItem ParametroItemSeleccionar(Int32 pIDParametroItem)
		{
			SqlCommand cmd = ConexionCmd("gen.ParametroItemSeleccionar");
			BEParametroItem oBE = new BEParametroItem();
			cmd.Parameters.Add("@IDParametroItem", SqlDbType.Int).Value = pIDParametroItem;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDParametroItem = rd.GetInt32(rd.GetOrdinal("IDParametroItem"));
					oBE.IDParametro = rd.GetInt32(rd.GetOrdinal("IDParametro"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Tipo = rd.GetString(rd.GetOrdinal("Tipo"));
					oBE.TipoNombre = rd.GetString(rd.GetOrdinal("TipoNombre"));
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

		public BERetornoTran ParametroItemGuardar(BEParametroItem BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ParametroItemGuardar");
			cmd.Parameters.Add("@IDParametroItem", SqlDbType.Int).Value = BEParam.IDParametroItem;
			cmd.Parameters.Add("@IDParametro", SqlDbType.Int).Value = BEParam.IDParametro;
			cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = BEParam.Nombre;
			cmd.Parameters.Add("@Tipo", SqlDbType.Char, 1).Value = BEParam.Tipo;
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

		public BERetornoTran ParametroItemEliminar(BEParametroItem BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ParametroItemEliminar");
			cmd.Parameters.Add("@IDParametroItem", SqlDbType.Int).Value = BEParam.IDParametroItem;
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