using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Inventario
{
	public class BLAlmacen : BLBase
	{
		#region No Transaccional

		public IList AlmacenListar(Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("inv.AlmacenListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			BEAlmacen oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEAlmacen();
					oBE.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacen"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
					oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
					oBE.Telefono = rd.GetString(rd.GetOrdinal("Telefono"));
					oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));
					oBE.NumeroNotaIngreso = rd.GetInt32(rd.GetOrdinal("NumeroNotaIngreso"));
					oBE.NumeroNotaSalida = rd.GetInt32(rd.GetOrdinal("NumeroNotaSalida"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.Ubigeo = rd.GetString(rd.GetOrdinal("Ubigeo"));

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

		public BEAlmacen AlmacenSeleccionar(Int32 pIDAlmacen)
		{
			SqlCommand cmd = ConexionCmd("inv.AlmacenSeleccionar");
			BEAlmacen oBE = new BEAlmacen();
			cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = pIDAlmacen;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacen"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
					oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
					oBE.Telefono = rd.GetString(rd.GetOrdinal("Telefono"));
					oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));
					oBE.NumeroNotaIngreso = rd.GetInt32(rd.GetOrdinal("NumeroNotaIngreso"));
					oBE.NumeroNotaSalida = rd.GetInt32(rd.GetOrdinal("NumeroNotaSalida"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.Ubigeo = rd.GetString(rd.GetOrdinal("Ubigeo"));
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
		 
		public BERetornoTran AlmacenGuardar(BEAlmacen BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("inv.AlmacenGuardar");
			cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = BEParam.IDAlmacen;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
            cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 5).Value = BEParam.Codigo;
			cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = BEParam.Nombre;
			cmd.Parameters.Add("@IDUbigeo", SqlDbType.VarChar, 20).Value = BEParam.IDUbigeo;
			cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 200).Value = BEParam.Direccion;
			cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = BEParam.Telefono;
			cmd.Parameters.Add("@Celular", SqlDbType.VarChar, 20).Value = BEParam.Celular;
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

		public BERetornoTran AlmacenEliminar(Int32 pIDAlmacen)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("inv.AlmacenEliminar");
			cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = pIDAlmacen;
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
