using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.Inventario
{
	public class BLInventarioFisico : BLBase
	{
		public BERetornoTran InventarioFisicoGuardar(BEInventarioFisico oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			SqlCommand cmd = new SqlCommand("inv.InventarioFisicoGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{
				cmd.Parameters.Add("@IDInventarioFisico", SqlDbType.Int).Value = oBE.IDInventarioFisico;
				cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int, 10).Value = oBE.IDAlmacen;
				cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 2000).Value = oBE.Observacion;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@IDInventarioFisicoFinal", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDInventarioFisicoFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);
				}
				cmd.Parameters.Clear();
				sqlTran.Commit();
			}
			catch (Exception ex)
			{
				sqlTran.Rollback();
				if (BERetorno.Retorno == "-1")
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

		public BERetornoTran InventarioFisicoActualizar(BEInventarioFisico oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			SqlCommand cmd = new SqlCommand("inv.InventarioFisicoActualizar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{
				cmd.Parameters.Add("@IDInventarioFisico", SqlDbType.Int).Value = oBE.IDInventarioFisico;
				cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int, 10).Value = oBE.IDAlmacen;
				cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 2000).Value = oBE.Observacion;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);
				}
				cmd.Parameters.Clear();
				sqlTran.Commit();
			}
			catch (Exception ex)
			{
				sqlTran.Rollback();
				if (BERetorno.Retorno == "-1")
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

		public BERetornoTran InventarioFisicoProcesar(BEInventarioFisico oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			SqlCommand cmd = new SqlCommand("inv.InventarioFisicoProcesar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{
				cmd.Parameters.Add("@IDInventarioFisico", SqlDbType.Int).Value = oBE.IDInventarioFisico;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = oBE.IDSucursal;
				cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);
				}
				cmd.Parameters.Clear();
				sqlTran.Commit();
			}
			catch (Exception ex)
			{
				sqlTran.Rollback();
				if (BERetorno.Retorno == "-1")
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

		public IList InventarioFisicoListar(Int32 pIDAlmacen, String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("inv.InventarioFisicoListar");
			cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = pIDAlmacen;
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;

			BEInventarioFisico oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEInventarioFisico();
					oBE.IDInventarioFisico = rd.GetInt32(rd.GetOrdinal("IDInventarioFisico"));
					oBE.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacen"));
					oBE.NumeroInventarioFormato = rd.GetString(rd.GetOrdinal("NumeroInventarioFormato"));
					oBE.NumeroInventario = rd.GetInt32(rd.GetOrdinal("NumeroInventario"));
					oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
					oBE.FechaInventario = rd.GetDateTime(rd.GetOrdinal("FechaInventario"));
					oBE.Procesado = rd.GetBoolean(rd.GetOrdinal("Procesado"));
					oBE.Almacen = rd.GetString(rd.GetOrdinal("Almacen"));

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

		public BEInventarioFisico InventarioFisicoSeleccionar(Int32 pIDInventarioFisico)
		{
			SqlCommand cmd = ConexionCmd("inv.InventarioFisicoSeleccionar");
			BEInventarioFisico oBE = new BEInventarioFisico();
			cmd.Parameters.Add("@IDInventarioFisico", SqlDbType.Int, 10).Value = pIDInventarioFisico;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDInventarioFisico = rd.GetInt32(rd.GetOrdinal("IDInventarioFisico"));
					oBE.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacen"));
					oBE.NumeroInventarioFormato = rd.GetString(rd.GetOrdinal("NumeroInventarioFormato"));
					oBE.NumeroInventario = rd.GetInt32(rd.GetOrdinal("NumeroInventario"));
					oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
					oBE.FechaInventario = rd.GetDateTime(rd.GetOrdinal("FechaInventario"));
					oBE.Procesado = rd.GetBoolean(rd.GetOrdinal("Procesado"));
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


	}
}
