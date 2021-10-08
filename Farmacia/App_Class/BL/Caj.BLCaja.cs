
using Farmacia.App_Class.BE.Caja;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Muebleria.App_Class.BL.Caja
{
	public class BLCaja : BLBase
	{
		#region Transaccional
		 
		public BERetornoTran CajaApertura(BECaja BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("caj.CajaApertura");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@IDCajaMecanica", SqlDbType.Int).Value = BEParam.IDCajaMecanica;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
			cmd.Parameters.Add("@MontoApertura", SqlDbType.Money).Value = BEParam.MontoApertura;
			cmd.Parameters.Add("@FechaApertura", SqlDbType.DateTime).Value = BEParam.FechaApertura;
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

		public BERetornoTran CajaCierre(BECaja BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("caj.CajaCierre");
			cmd.Parameters.Add("@IDCaja", SqlDbType.Int).Value = BEParam.IDCaja;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario; 
			cmd.Parameters.Add("@Fecha", SqlDbType.VarChar, 10).Value = BEParam.Fecha;
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

		public BERetornoTran CajaEliminar(Int32 pIDCaja, Int32 pIDSucursal)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("caj.CajaEliminar");
			cmd.Parameters.Add("@IDCaja", SqlDbType.Int).Value = pIDCaja;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
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

		#region NoTransaccional

		public IList CajaResumenListar(Int32 pIDSucursal, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("caj.CajaResumenListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
			BECaja oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECaja();
					oBE.IDCaja = rd.GetInt32(rd.GetOrdinal("IDCaja"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Fecha = rd.GetDateTime(rd.GetOrdinal("Fecha"));
					oBE.FechaApertura = rd.GetDateTime(rd.GetOrdinal("FechaApertura"));
					oBE.FechaCierre = rd.GetDateTime(rd.GetOrdinal("FechaCierre"));
					oBE.FechaReAperturaCaja = rd.GetDateTime(rd.GetOrdinal("FechaReAperturaCaja"));
					oBE.UsuarioApertura = rd.GetString(rd.GetOrdinal("UsuarioApertura"));
					oBE.UsuarioCierre = rd.GetString(rd.GetOrdinal("UsuarioCierre"));
					oBE.UsuarioReaApertura = rd.GetString(rd.GetOrdinal("UsuarioReaApertura"));
					oBE.MontoApertura = rd.GetDecimal(rd.GetOrdinal("MontoApertura"));
					oBE.TotalIngreso = rd.GetDecimal(rd.GetOrdinal("TotalIngreso"));
					oBE.TotalEgreso = rd.GetDecimal(rd.GetOrdinal("TotalEgreso"));
					oBE.SaldoFinal = rd.GetDecimal(rd.GetOrdinal("SaldoFinal"));
					oBE.NombreEstado = rd.GetString(rd.GetOrdinal("NombreEstado"));
					oBE.CajaMecanica = rd.GetString(rd.GetOrdinal("CajaMecanica"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));

					oBE.Contado = rd.GetDecimal(rd.GetOrdinal("Contado"));
					oBE.Calculado = rd.GetDecimal(rd.GetOrdinal("Calculado"));
					oBE.Diferencia = rd.GetDecimal(rd.GetOrdinal("Diferencia"));
					oBE.Retiro = rd.GetDecimal(rd.GetOrdinal("Retiro"));
					oBE.Transferencia = rd.GetDecimal(rd.GetOrdinal("Transferencia"));
					 


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

		public IList CajaMecanicaListar(String pFiltro, Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("gen.CajaMecanicaListar"); 
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = pFiltro;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			BECaja oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECaja();
					oBE.IDCajaMecanica = rd.GetInt32(rd.GetOrdinal("IDCajaMecanica"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Responsable = rd.GetString(rd.GetOrdinal("Responsable"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
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

		public BECaja CajaSeleccionar(Int32 pIDCaja)
		{
			SqlCommand cmd = ConexionCmd("caj.CajaSeleccionar");
			BECaja oBE = new BECaja();
			cmd.Parameters.Add("@IDCaja", SqlDbType.Int).Value = pIDCaja;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDCaja = rd.GetInt32(rd.GetOrdinal("IDCaja"));
					oBE.CajaMecanica = rd.GetString(rd.GetOrdinal("CajaMecanica"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.Fecha = rd.GetDateTime(rd.GetOrdinal("Fecha"));
					oBE.FechaApertura = rd.GetDateTime(rd.GetOrdinal("FechaApertura"));
					oBE.FechaCierre = rd.GetDateTime(rd.GetOrdinal("FechaCierre"));
					oBE.TotalIngreso = rd.GetDecimal(rd.GetOrdinal("TotalIngreso"));
					oBE.TotalEgreso = rd.GetDecimal(rd.GetOrdinal("TotalEgreso"));
					oBE.SaldoFinal = rd.GetDecimal(rd.GetOrdinal("SaldoFinal"));
					oBE.MontoApertura = rd.GetDecimal(rd.GetOrdinal("MontoApertura"));
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

		public IList CajaResumenAbiertoListar(Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("caj.CajaResumenAbiertoListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal; 
			BECaja oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECaja();
					oBE.IDCaja = rd.GetInt32(rd.GetOrdinal("IDCaja"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Fecha = rd.GetDateTime(rd.GetOrdinal("Fecha"));
					oBE.FechaApertura = rd.GetDateTime(rd.GetOrdinal("FechaApertura"));
					oBE.FechaCierre = rd.GetDateTime(rd.GetOrdinal("FechaCierre"));
					oBE.FechaReAperturaCaja = rd.GetDateTime(rd.GetOrdinal("FechaReAperturaCaja"));
					oBE.UsuarioApertura = rd.GetString(rd.GetOrdinal("UsuarioApertura"));
					oBE.UsuarioCierre = rd.GetString(rd.GetOrdinal("UsuarioCierre"));
					oBE.UsuarioReaApertura = rd.GetString(rd.GetOrdinal("UsuarioReaApertura"));
					oBE.MontoApertura = rd.GetDecimal(rd.GetOrdinal("MontoApertura"));
					oBE.TotalIngreso = rd.GetDecimal(rd.GetOrdinal("TotalIngreso"));
					oBE.TotalEgreso = rd.GetDecimal(rd.GetOrdinal("TotalEgreso"));
					oBE.SaldoFinal = rd.GetDecimal(rd.GetOrdinal("SaldoFinal"));
					oBE.NombreEstado = rd.GetString(rd.GetOrdinal("NombreEstado"));
					oBE.CajaMecanica = rd.GetString(rd.GetOrdinal("CajaMecanica"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
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
		 
		//LISTAR CAJA POR SUCURSAL
		public IList ListarCajaxSucursal(Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("gen.ListarCajaxSucursal");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			BECaja oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECaja();
					oBE.IDCajaMecanica = rd.GetInt32(rd.GetOrdinal("IDCajaMecanica"));
					oBE.NombreCaja = rd.GetString(rd.GetOrdinal("NombreCaja"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
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
		 
		public IList CorteCajaPreListar(Int32 pIDCaja)
		{
			SqlCommand cmd = ConexionCmd("caj.CorteCajaPreListar"); 
			cmd.Parameters.Add("@IDCaja", SqlDbType.Int).Value = pIDCaja;
			BECaja oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECaja();
					oBE.IDCorteCaja = rd.GetInt64(rd.GetOrdinal("IDCorteCaja"));
					oBE.IDCaja = rd.GetInt32(rd.GetOrdinal("IDCaja"));
					oBE.IDMedioPago = rd.GetInt32(rd.GetOrdinal("IDMedioPago"));
					oBE.MedioPago = rd.GetString(rd.GetOrdinal("MedioPago"));
					oBE.Contado = rd.GetDecimal(rd.GetOrdinal("Contado"));
					oBE.Calculado = rd.GetDecimal(rd.GetOrdinal("Calculado"));
					oBE.Diferencia = rd.GetDecimal(rd.GetOrdinal("Diferencia"));
					oBE.Retiro = rd.GetDecimal(rd.GetOrdinal("Retiro"));
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

		public IList CorteCajaListar(Int32 pIDCaja)
		{
			SqlCommand cmd = ConexionCmd("caj.CorteCajaListar");
			cmd.Parameters.Add("@IDCaja", SqlDbType.Int).Value = pIDCaja;
			BECaja oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BECaja();
					oBE.IDCorteCaja = rd.GetInt32(rd.GetOrdinal("IDCorteCaja"));
					oBE.IDCaja = rd.GetInt32(rd.GetOrdinal("IDCaja"));
					oBE.IDMedioPago = rd.GetInt32(rd.GetOrdinal("IDMedioPago"));
					oBE.MedioPago = rd.GetString(rd.GetOrdinal("MedioPago"));
					oBE.Contado = rd.GetDecimal(rd.GetOrdinal("Contado"));
					oBE.Calculado = rd.GetDecimal(rd.GetOrdinal("Calculado"));
					oBE.Diferencia = rd.GetDecimal(rd.GetOrdinal("Diferencia"));
					oBE.Retiro = rd.GetDecimal(rd.GetOrdinal("Retiro"));
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

		public BERetornoTran CorteCajaGuardar(BECaja BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("caj.CorteCajaGuardar");
			cmd.Parameters.Add("@IDCorteCaja", SqlDbType.Int).Value = BEParam.IDCorteCaja;
			cmd.Parameters.Add("@IDCaja", SqlDbType.Int).Value = BEParam.IDCaja;
			cmd.Parameters.Add("@IDMedioPago", SqlDbType.Int).Value = BEParam.IDMedioPago;
			cmd.Parameters.Add("@Contado", SqlDbType.Decimal).Value = BEParam.Contado;
			cmd.Parameters.Add("@Calculado", SqlDbType.Decimal).Value = BEParam.Calculado;
			cmd.Parameters.Add("@Diferencia", SqlDbType.Decimal).Value = BEParam.Diferencia;
			cmd.Parameters.Add("@Retiro", SqlDbType.Decimal).Value = BEParam.Retiro;
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

		public BECaja CajaAbiertaSeleccionar(Int32 pIDUsuario, Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("caj.CajaAbiertaSeleccionar");
			BECaja oBE = new BECaja();
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDCaja = rd.GetInt32(rd.GetOrdinal("IDCaja"));
					oBE.CajaMecanica = rd.GetString(rd.GetOrdinal("CajaMecanica"));
					oBE.Cajero = rd.GetString(rd.GetOrdinal("Cajero")); 
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