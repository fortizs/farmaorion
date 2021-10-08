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
	public class BLInventarioFisicoDetalle :BLBase
	{
		public IList InventarioFisicoDetalleListar(Int32 pIDInventarioFisico)
		{
			SqlCommand cmd = ConexionCmd("inv.InventarioFisicoDetalleListar");
			cmd.Parameters.Add("@IDInventarioFisico", SqlDbType.Int).Value = pIDInventarioFisico;
			BEInventarioFisicoDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEInventarioFisicoDetalle();
					oBE.IDInventarioFisicoDetalle = rd.GetInt32(rd.GetOrdinal("IDInventarioFisicoDetalle"));
					oBE.IDInventarioFisico = rd.GetInt32(rd.GetOrdinal("IDInventarioFisico"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.IDLote = rd.GetInt32(rd.GetOrdinal("IDLote"));
					oBE.NombreLote = rd.GetString(rd.GetOrdinal("NombreLote"));
					oBE.StockLote = rd.GetDecimal(rd.GetOrdinal("StockLote"));
					oBE.Categoria = rd.GetString(rd.GetOrdinal("Categoria"));
					oBE.StockActual = rd.GetDecimal(rd.GetOrdinal("StockActual"));
					oBE.IngresoConteo = rd.GetDecimal(rd.GetOrdinal("IngresoConteo"));
					oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));

					if (oBE.IDLote > 0) {
						oBE.StockActual = oBE.StockLote;
						oBE.Nombre = oBE.Nombre + " / " + oBE.NombreLote;
					}

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

		public BERetornoTran InventarioFisicoDetalleActualizar(BEInventarioFisicoDetalle oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			SqlCommand cmd = new SqlCommand("inv.InventarioFisicoDetalleActualizar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";
			try
			{
				cmd.Parameters.Add("@IDInventarioFisicoDetalle", SqlDbType.Int).Value = oBE.IDInventarioFisicoDetalle;
				cmd.Parameters.Add("@IngresoConteo", SqlDbType.Decimal).Value = oBE.IngresoConteo;
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

	}
}
