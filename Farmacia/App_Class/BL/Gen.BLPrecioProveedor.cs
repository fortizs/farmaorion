using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLPrecioProveedor : BLBase
	{
		public IList PrecioProveedorListar(Int32 pIDProducto)
		{
			SqlCommand cmd = ConexionCmd("gen.PrecioProveedorListar");
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = pIDProducto; 
			BEPrecioProveedor oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEPrecioProveedor();
					oBE.IDPrecioProveedor = rd.GetInt32(rd.GetOrdinal("IDPrecioProveedor"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
					oBE.FechaUltimoPrecio = rd.GetDateTime(rd.GetOrdinal("FechaUltimoPrecio"));
					oBE.UltimoPrecioCompra = rd.GetDecimal(rd.GetOrdinal("UltimoPrecioCompra"));
					oBE.Proveedor = rd.GetString(rd.GetOrdinal("Proveedor"));
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
		 
		public BERetornoTran PrecioProveedorGuardar(BEPrecioProveedor BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PrecioProveedorGuardar");
			cmd.Parameters.Add("@IDPrecioProveedor", SqlDbType.Int).Value = BEParam.IDPrecioProveedor;
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
			cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = BEParam.IDProveedor;
			cmd.Parameters.Add("@FechaUltimoPrecio", SqlDbType.DateTime).Value = BEParam.FechaUltimoPrecio;
			cmd.Parameters.Add("@UltimoPrecioCompra", SqlDbType.Decimal).Value = BEParam.UltimoPrecioCompra;
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

		public BERetornoTran PrecioProveedorEliminar(Int32 pIDPrecioProveedor)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.PrecioProveedorEliminar");
			cmd.Parameters.Add("@IDPrecioProveedor", SqlDbType.Int).Value = pIDPrecioProveedor; 
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
