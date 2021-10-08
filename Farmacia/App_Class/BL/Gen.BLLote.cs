using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLLote : BLBase
    {
		#region Transaccional
		public BERetornoTran LoteGuardar(BELote BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.LoteGuardar");
			cmd.Parameters.Add("@IDLote", SqlDbType.Int).Value = BEParam.IDLote;
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@Lote", SqlDbType.VarChar, 100).Value = BEParam.Lote;
			cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = BEParam.CantidadLote;
			cmd.Parameters.Add("@FechaVencimiento", SqlDbType.DateTime).Value = BEParam.FechaVencimiento;
			cmd.Parameters.Add("@FechaFabricacion", SqlDbType.DateTime).Value = BEParam.FechaFabricacion;
			cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = BEParam.Estado;
			cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = BEParam.Token;
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

		public BERetornoTran LoteAjusteGuardar(BELote BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.LoteAjusteGuardar");
			cmd.Parameters.Add("@IDLote", SqlDbType.Int).Value = BEParam.IDLote;
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@Lote", SqlDbType.VarChar, 100).Value = BEParam.Lote;
			cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = BEParam.CantidadLote;
			cmd.Parameters.Add("@FechaVencimiento", SqlDbType.DateTime).Value = BEParam.FechaVencimiento;
			cmd.Parameters.Add("@FechaFabricacion", SqlDbType.DateTime).Value = BEParam.FechaFabricacion;
			cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = BEParam.Estado;
			cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = BEParam.Token;
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

		public BERetornoTran LoteEliminar(Int32 pIDLote)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.LoteEliminar");
			cmd.Parameters.Add("@IDLote", SqlDbType.Int).Value = pIDLote;
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

		#region No Transaccional

		public IList LoteListar(Int32 pIDProducto, Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("gen.LoteListar");
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = pIDProducto;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal; 
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					BELote oBE = new BELote();
					oBE.IDLote = rd.GetInt32(rd.GetOrdinal("IDLote"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Lote = rd.GetString(rd.GetOrdinal("Lote"));
					oBE.CantidadLote = rd.GetDecimal(rd.GetOrdinal("CantidadLote"));
					oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
					oBE.FechaFabricacion = rd.GetDateTime(rd.GetOrdinal("FechaFabricacion"));
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


		public IList LotexTokenListar(Int32 pIDProducto, String pToken)
		{
			SqlCommand cmd = ConexionCmd("gen.LotexTokenListar");
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = pIDProducto;
			cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = pToken;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					BELote oBE = new BELote();
					oBE.IDLote = rd.GetInt32(rd.GetOrdinal("IDLote"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Lote = rd.GetString(rd.GetOrdinal("Lote"));
					oBE.CantidadLote = rd.GetDecimal(rd.GetOrdinal("CantidadLote"));
					oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
					oBE.FechaFabricacion = rd.GetDateTime(rd.GetOrdinal("FechaFabricacion"));
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

		public IList LotexNotaAjusteTokenListar(BELote BEParam)
		{
			SqlCommand cmd = ConexionCmd("gen.LotexNotaAjusteTokenListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
			cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = BEParam.Token;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					BELote oBE = new BELote();
					oBE.IDLote = rd.GetInt32(rd.GetOrdinal("IDLote"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Lote = rd.GetString(rd.GetOrdinal("Lote"));
					oBE.Stock = rd.GetDecimal(rd.GetOrdinal("Stock"));
					oBE.CantidadLote = rd.GetDecimal(rd.GetOrdinal("CantidadLote"));
					oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
					oBE.FechaFabricacion = rd.GetDateTime(rd.GetOrdinal("FechaFabricacion"));
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
		

		public BELote LoteSeleccionar(Int32 pIDLote)
		{
			BELote oBE = new BELote();
			SqlCommand cmd = ConexionCmd("gen.LoteSeleccionar");
			cmd.Parameters.Add("@IDLote", SqlDbType.Int).Value = pIDLote;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDLote = rd.GetInt32(rd.GetOrdinal("IDLote"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Lote = rd.GetString(rd.GetOrdinal("Lote"));
					oBE.CantidadLote = rd.GetDecimal(rd.GetOrdinal("CantidadLote"));
					oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
					oBE.FechaFabricacion = rd.GetDateTime(rd.GetOrdinal("FechaFabricacion"));
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