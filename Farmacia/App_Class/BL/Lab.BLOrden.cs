using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Laboratorio;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.Laboratorio
{
	public class BLOrden : BLBase
	{
		public IList OrdenListar(Int32 pIDSucursal, Int32 pIDCliente, String pFechaInicio, String pFechaFin, String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("gen.OrdenListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pIDCliente;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin;
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar).Value = pFiltro;
			BEOrden oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEOrden();
					oBE.IDOrden = rd.GetInt32(rd.GetOrdinal("IDOrden"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
					oBE.IDClienteConvenio = rd.GetInt32(rd.GetOrdinal("IDClienteConvenio"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.Anio = rd.GetInt32(rd.GetOrdinal("Anio"));
					oBE.NumeroOrden = rd.GetInt32(rd.GetOrdinal("NumeroOrden"));
					oBE.FechaOrden = rd.GetDateTime(rd.GetOrdinal("FechaOrden"));
					oBE.TotalOperacionGravada = rd.GetDecimal(rd.GetOrdinal("TotalOperacionGravada"));
					oBE.TotalIgv = rd.GetDecimal(rd.GetOrdinal("TotalIgv"));
					oBE.TotalOrden = rd.GetDecimal(rd.GetOrdinal("TotalOrden"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
					oBE.ClienteNumeroDocumento = rd.GetString(rd.GetOrdinal("ClienteNumeroDocumento"));
					oBE.ClienteRazonSocial = rd.GetString(rd.GetOrdinal("ClienteRazonSocial"));
					oBE.ClienteSexoNombre = rd.GetString(rd.GetOrdinal("ClienteSexoNombre"));
					oBE.ClienteCelular1 = rd.GetString(rd.GetOrdinal("ClienteCelular1"));
					oBE.ClienteCelular2 = rd.GetString(rd.GetOrdinal("ClienteCelular2"));
					oBE.ClienteCorreo = rd.GetString(rd.GetOrdinal("ClienteCorreo"));
					oBE.ClienteCONumeroDocumento = rd.GetString(rd.GetOrdinal("ClienteCONumeroDocumento"));
					oBE.ClienteCORazonSocial = rd.GetString(rd.GetOrdinal("ClienteCORazonSocial"));
					oBE.ClienteCOSexoNombre = rd.GetString(rd.GetOrdinal("ClienteCOSexoNombre"));
					oBE.ClienteCOCelular1 = rd.GetString(rd.GetOrdinal("ClienteCOCelular1"));
					oBE.ClienteCOCelular2 = rd.GetString(rd.GetOrdinal("ClienteCOCelular2"));
					oBE.ClienteCOCorreo = rd.GetString(rd.GetOrdinal("ClienteCOCorreo"));
					oBE.EstadoNombre = rd.GetString(rd.GetOrdinal("EstadoNombre"));
					oBE.OrdenFormato = rd.GetString(rd.GetOrdinal("OrdenFormato"));
					oBE.NumeroMuestra = rd.GetInt32(rd.GetOrdinal("NumeroMuestra"));
					oBE.ClienteEdad = rd.GetInt32(rd.GetOrdinal("ClienteEdad"));



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

		public BEOrden OrdenSeleccionar(Int32 pIDOrden)
		{
			SqlCommand cmd = ConexionCmd("gen.OrdenSeleccionar");
			BEOrden oBE = new BEOrden();
			cmd.Parameters.Add("@IDOrden", SqlDbType.Int).Value = pIDOrden;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{

					oBE.IDOrden = rd.GetInt32(rd.GetOrdinal("IDOrden"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
					oBE.IDClienteConvenio = rd.GetInt32(rd.GetOrdinal("IDClienteConvenio"));
					oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
					oBE.Anio = rd.GetInt32(rd.GetOrdinal("Anio"));
					oBE.NumeroOrden = rd.GetInt32(rd.GetOrdinal("NumeroOrden"));
					oBE.FechaOrden = rd.GetDateTime(rd.GetOrdinal("FechaOrden"));
					oBE.TotalOperacionGravada = rd.GetDecimal(rd.GetOrdinal("TotalOperacionGravada"));
					oBE.TotalIgv = rd.GetDecimal(rd.GetOrdinal("TotalIgv"));
					oBE.TotalOrden = rd.GetDecimal(rd.GetOrdinal("TotalOrden"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
					oBE.ClienteNumeroDocumento = rd.GetString(rd.GetOrdinal("ClienteNumeroDocumento"));
					oBE.ClienteRazonSocial = rd.GetString(rd.GetOrdinal("ClienteRazonSocial"));
					oBE.ClienteSexoNombre = rd.GetString(rd.GetOrdinal("ClienteSexoNombre"));
					oBE.ClienteCelular1 = rd.GetString(rd.GetOrdinal("ClienteCelular1"));
					oBE.ClienteCelular2 = rd.GetString(rd.GetOrdinal("ClienteCelular2"));
					oBE.ClienteCorreo = rd.GetString(rd.GetOrdinal("ClienteCorreo"));
					oBE.ClienteCONumeroDocumento = rd.GetString(rd.GetOrdinal("ClienteCONumeroDocumento"));
					oBE.ClienteCORazonSocial = rd.GetString(rd.GetOrdinal("ClienteCORazonSocial"));
					oBE.ClienteCOSexoNombre = rd.GetString(rd.GetOrdinal("ClienteCOSexoNombre"));
					oBE.ClienteCOCelular1 = rd.GetString(rd.GetOrdinal("ClienteCOCelular1"));
					oBE.ClienteCOCelular2 = rd.GetString(rd.GetOrdinal("ClienteCOCelular2"));
					oBE.ClienteCOCorreo = rd.GetString(rd.GetOrdinal("ClienteCOCorreo"));
					oBE.EstadoNombre = rd.GetString(rd.GetOrdinal("EstadoNombre"));
					oBE.OrdenFormato = rd.GetString(rd.GetOrdinal("OrdenFormato"));
					oBE.ClienteEdad = rd.GetInt32(rd.GetOrdinal("ClienteEdad"));
					oBE.Diagnostico = rd.GetString(rd.GetOrdinal("Diagnostico"));
					oBE.Recomendacion = rd.GetString(rd.GetOrdinal("Recomendacion"));

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

		public IList OrdenDetalleListar(Int32 pIDOrden)
		{
			SqlCommand cmd = ConexionCmd("gen.OrdenDetalleListar");
			cmd.Parameters.Add("@IDOrden", SqlDbType.Int).Value = pIDOrden;
			BEOrden oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEOrden();
					oBE.IDOrdenDetalle = rd.GetInt32(rd.GetOrdinal("IDOrdenDetalle"));
					oBE.IDOrden = rd.GetInt32(rd.GetOrdinal("IDOrden"));
					oBE.Examen = rd.GetString(rd.GetOrdinal("Examen"));
					oBE.Resultado = rd.GetString(rd.GetOrdinal("Resultado"));
					oBE.Tipo = rd.GetString(rd.GetOrdinal("Tipo"));
					oBE.Orden = rd.GetInt32(rd.GetOrdinal("Orden"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					oBE.ResultadoPredefinido = rd.GetString(rd.GetOrdinal("ResultadoPredefinido"));
					oBE.IDGenerico = rd.GetInt32(rd.GetOrdinal("IDGenerico"));
					oBE.Generico = rd.GetString(rd.GetOrdinal("Generico"));
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

		public BERetornoTran OrdenDetalleResultadoActualizar(BEOrden oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.OrdenDetalleResultadoActualizar");
			cmd.Parameters.Add("@IDOrdenDetalle", SqlDbType.Int).Value = oBE.IDOrdenDetalle;
			cmd.Parameters.Add("@Resultado", SqlDbType.VarChar, 1000).Value = oBE.Resultado;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
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

		public BERetornoTran OrdenCompletar(Int32 pIDOrden, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.OrdenCompletar");
			cmd.Parameters.Add("@IDOrden", SqlDbType.Int).Value = pIDOrden;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
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

		public BERetornoTran OrdenActualizar(Int32 pIDOrden, String pDiagnostico, String pRecomendacion, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.OrdenActualizar");
			cmd.Parameters.Add("@IDOrden", SqlDbType.Int).Value = pIDOrden;
			cmd.Parameters.Add("@Diagnostico", SqlDbType.VarChar).Value = pDiagnostico;
			cmd.Parameters.Add("@Recomendacion", SqlDbType.VarChar).Value = pRecomendacion;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
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

		public BERetornoTran OrdenEliminar(Int32 pIDOrden, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.OrdenEliminar");
			cmd.Parameters.Add("@IDOrden", SqlDbType.Int).Value = pIDOrden;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
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


	}
}
