using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLProductoCompatible : BLBase
	{
		public IList ProductoCompatibleListar(Int32 pIDProducto, Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("gen.ProductoCompatibleListar");
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = pIDProducto;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			BEProductoCompatible oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProductoCompatible();
					oBE.IDProductoCompatible = rd.GetInt32(rd.GetOrdinal("IDProductoCompatible"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDProductoComp = rd.GetInt32(rd.GetOrdinal("IDProductoComp"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.CodigoBarra = rd.GetString(rd.GetOrdinal("CodigoBarra"));
					oBE.CodigoAlterna = rd.GetString(rd.GetOrdinal("CodigoAlterna"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
					oBE.PrincipioActivo = rd.GetString(rd.GetOrdinal("PrincipioActivo"));
					oBE.Localizacion = rd.GetString(rd.GetOrdinal("Localizacion"));
					oBE.StockMinimo = rd.GetDecimal(rd.GetOrdinal("StockMinimo"));
					oBE.Stock = rd.GetDecimal(rd.GetOrdinal("Stock"));
					oBE.PrecioCosto = rd.GetDecimal(rd.GetOrdinal("PrecioCosto"));
					oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.IDLinea = rd.GetInt32(rd.GetOrdinal("IDLinea"));
					oBE.IDMarca = rd.GetInt32(rd.GetOrdinal("IDMarca"));
					oBE.IDCategoria = rd.GetInt32(rd.GetOrdinal("IDCategoria"));
					oBE.UnidadMedidaCompra = rd.GetString(rd.GetOrdinal("UnidadMedidaCompra"));
					oBE.UnidadMedidaVenta = rd.GetString(rd.GetOrdinal("UnidadMedidaVenta"));
					oBE.Linea = rd.GetString(rd.GetOrdinal("Linea"));
					oBE.Marca = rd.GetString(rd.GetOrdinal("Marca"));
					oBE.Categoria = rd.GetString(rd.GetOrdinal("Categoria"));
					oBE.TipoProducto = rd.GetString(rd.GetOrdinal("TipoProducto"));
					oBE.AlertaStock = rd.GetBoolean(rd.GetOrdinal("AlertaStock"));
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

		public BERetornoTran ProductoCompatibleGuardar(BEProductoCompatible BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ProductoCompatibleGuardar");
			cmd.Parameters.Add("@IDProductoCompatible", SqlDbType.Int).Value = BEParam.IDProductoCompatible;
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
			cmd.Parameters.Add("@IDProductoComp", SqlDbType.Int).Value = BEParam.IDProductoComp;
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

		public BERetornoTran ProductoCompatibleEliminar(Int32 pIDProductoCompatible)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ProductoCompatibleEliminar");
			cmd.Parameters.Add("@IDProductoCompatible", SqlDbType.Int).Value = pIDProductoCompatible;
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