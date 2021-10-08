using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLUbigeo : BLBase
	{

		public IList Listar()
		{
			SqlCommand cmd = ConexionCmd("gen.UbigeoListar1");
			BEUbigeo oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEUbigeo();
					oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
					oBE.Distrito = rd.GetString(rd.GetOrdinal("Distrito"));
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


		public IList UbigeoListarBuscar(String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("gen.UbigeoListarBuscar");
			cmd.Parameters.Add("@Buscar", SqlDbType.VarChar, 200).Value = pFiltro;

			BEUbigeo oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEUbigeo();
					oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
					oBE.Distrito = rd.GetString(rd.GetOrdinal("Distrito"));
					oBE.IDProvincia = rd.GetString(rd.GetOrdinal("IDProvincia"));
					oBE.Provincia = rd.GetString(rd.GetOrdinal("Provincia"));
					oBE.IDDepartamento = rd.GetString(rd.GetOrdinal("IDDepartamento"));
					oBE.Departamento = rd.GetString(rd.GetOrdinal("Departamento"));
					oBE.NombreCompleto = rd.GetString(rd.GetOrdinal("NombreCompleto"));
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
		//public IList UbigeoFiltroListar(String pFiltro)
		//{
		//	SqlCommand cmd = ConexionCmd("gen.UbigeoFiltroListar");
		//	cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
		//	BEUbigeo oBE;
		//	ArrayList lista = new ArrayList();
		//	try
		//	{
		//		cmd.Connection.Open();
		//		SqlDataReader rd = cmd.ExecuteReader();
		//		while (rd.Read())
		//		{
		//			oBE = new BEUbigeo();
		//			oBE.IDUbigeo = rd.GetInt32(rd.GetOrdinal("IDUbigeo"));
		//			oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
		//			oBE.NombreCorto = rd.GetString(rd.GetOrdinal("NombreCorto"));
		//			oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
		//			oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
		//			lista.Add(oBE);
		//			oBE = null;


		//		}
		//		rd.Close();
		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
		//	finally
		//	{
		//		if ((cmd.Connection.State == ConnectionState.Open))
		//		{
		//			cmd.Connection.Close();
		//		}
		//	}
		//	return lista;
		//}

		//public BEBase Seleccionar(int pCodigo)
		//{
		//	SqlCommand cmd = ConexionCmd("gen.UbigeoSeleccionar");
		//	BEUbigeo oBE = new BEUbigeo();
		//	cmd.Parameters.Add("@IDUbigeo", SqlDbType.Int).Value = pCodigo;
		//	try
		//	{
		//		cmd.Connection.Open();
		//		SqlDataReader rd = cmd.ExecuteReader();
		//		if (rd.Read())
		//		{
		//			oBE.IDUbigeo = rd.GetInt32(rd.GetOrdinal("IDUbigeo"));
		//			oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
		//			oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
		//			oBE.NombreCorto = rd.GetString(rd.GetOrdinal("NombreCorto"));
		//			oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));


		//		}
		//		rd.Close();
		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
		//	finally
		//	{
		//		if ((cmd.Connection.State == ConnectionState.Open))
		//		{
		//			cmd.Connection.Close();
		//		}
		//	}
		//	return oBE;
		//}

		//public BERetornoTran Insertar(BEBase pEntidad)
		//{
		//	BERetornoTran BERetorno = new BERetornoTran();
		//	SqlCommand cmd = ConexionCmd("gen.UbigeoGuardar");
		//	cmd = LlenarEstructura(pEntidad, cmd, "I");
		//	try
		//	{
		//		cmd.Connection.Open();
		//		cmd.ExecuteNonQuery();
		//		BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
		//		BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
		//	}
		//	catch (Exception ex)
		//	{
		//		BERetorno.ErrorMensaje = ex.ToString();
		//	}
		//	finally
		//	{
		//		if (cmd.Connection.State == ConnectionState.Open)
		//		{
		//			cmd.Connection.Close();
		//		}
		//	}
		//	return BERetorno;
		//}

		//public BERetornoTran Actualizar(BEBase pEntidad)
		//{
		//	BERetornoTran BERetorno = new BERetornoTran();
		//	SqlCommand cmd = ConexionCmd("gen.UbigeoActualizar");
		//	cmd = LlenarEstructura(pEntidad, cmd, "A");
		//	try
		//	{
		//		cmd.Connection.Open();
		//		cmd.ExecuteNonQuery();
		//		BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
		//		BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
		//	}
		//	catch (Exception ex)
		//	{
		//		BERetorno.ErrorMensaje = ex.ToString();
		//	}
		//	finally
		//	{
		//		if (cmd.Connection.State == ConnectionState.Open)
		//		{
		//			cmd.Connection.Close();
		//		}
		//	}
		//	return BERetorno;
		//}

		//public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand cmd, String pTipoTransaccion)
		//{
		//	BEUbigeo oBE = (BEUbigeo)pEntidad;
		//	cmd.Parameters.Add("@IDUbigeo", SqlDbType.Int).Value = oBE.IDUbigeo;
		//	cmd.Parameters.Add("@Codigo", SqlDbType.VarChar, 200).Value = oBE.Codigo;
		//	cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = oBE.Nombre;
		//	cmd.Parameters.Add("@NombreCorto", SqlDbType.VarChar, 200).Value = oBE.NombreCorto;
		//	cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;

		//	cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
		//	cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
		//	cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
		//	return cmd;
		//}

	}
}
