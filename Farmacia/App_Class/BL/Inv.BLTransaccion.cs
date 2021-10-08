using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Inventario
{
	public class BLTransaccion : BLBase
	{
		#region No Transaccional

		public IList TransaccionListar(String pTipoMovimiento)
		{
			SqlCommand cmd = ConexionCmd("inv.TransaccionListar");
			cmd.Parameters.Add("@TipoMovimiento", SqlDbType.Char, 1).Value = pTipoMovimiento;
			BETransaccion oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BETransaccion();
					oBE.IDTransaccion = rd.GetInt32(rd.GetOrdinal("IDTransaccion"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.TipoMovimiento = rd.GetString(rd.GetOrdinal("TipoMovimiento"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.TipoMovimientoNombre = rd.GetString(rd.GetOrdinal("TipoMovimientoNombre"));
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

        public IList TransaccionNotaIngresoListar(String pTipoMovimiento)
        {
            SqlCommand cmd = ConexionCmd("inv.TransaccionNotaIngresoListar");
            cmd.Parameters.Add("@TipoMovimiento", SqlDbType.Char, 1).Value = pTipoMovimiento;
            BETransaccion oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BETransaccion();
                    oBE.IDTransaccion = rd.GetInt32(rd.GetOrdinal("IDTransaccion"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.TipoMovimiento = rd.GetString(rd.GetOrdinal("TipoMovimiento"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
                    oBE.TipoMovimientoNombre = rd.GetString(rd.GetOrdinal("TipoMovimientoNombre"));
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

        public IList TransaccionNotaSalidaListar(String pTipoMovimiento)
        {
            SqlCommand cmd = ConexionCmd("inv.TransaccionNotaSalidaListar");
            cmd.Parameters.Add("@TipoMovimiento", SqlDbType.Char, 1).Value = pTipoMovimiento;
            BETransaccion oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BETransaccion();
                    oBE.IDTransaccion = rd.GetInt32(rd.GetOrdinal("IDTransaccion"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.TipoMovimiento = rd.GetString(rd.GetOrdinal("TipoMovimiento"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
                    oBE.TipoMovimientoNombre = rd.GetString(rd.GetOrdinal("TipoMovimientoNombre"));
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

        public BETransaccion TransaccionSeleccionar(Int32 pIDTransaccion)
		{
			SqlCommand cmd = ConexionCmd("inv.TransaccionSeleccionar");
			BETransaccion oBE = new BETransaccion();
			cmd.Parameters.Add("@IDTransaccion", SqlDbType.Int).Value = pIDTransaccion;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDTransaccion = rd.GetInt32(rd.GetOrdinal("IDTransaccion"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.TipoMovimiento = rd.GetString(rd.GetOrdinal("TipoMovimiento"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.TipoMovimientoNombre = rd.GetString(rd.GetOrdinal("TipoMovimientoNombre"));
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

		#region Transaccional

		public BERetornoTran TransaccionGuardar(BETransaccion BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("inv.TransaccionGuardar");
			cmd.Parameters.Add("@IDTransaccion", SqlDbType.Int).Value = BEParam.IDTransaccion;
			cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 4).Value = BEParam.Codigo;
			cmd.Parameters.Add("@TipoMovimiento", SqlDbType.Char, 1).Value = BEParam.TipoMovimiento;
			cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 300).Value = BEParam.Nombre;
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

		public BERetornoTran TransaccionEliminar(Int32 pIDTransaccion)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("inv.TransaccionEliminar");
			cmd.Parameters.Add("@IDTransaccion", SqlDbType.Int).Value = pIDTransaccion;
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
		  
		#endregion



	}
}
