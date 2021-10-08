using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Vehicular;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.Vehicular
{
	public class BLMarcaModeloVersion : BLBase
	{
		public IList MarcaOrigenListar()
		{
			SqlCommand cmd = ConexionCmd("veh.MarcaOrigenListar");
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					BEMarcaModeloVersion oBE = new BEMarcaModeloVersion();
					oBE.Origen = rd.GetString(rd.GetOrdinal("Origen"));
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

		public IList MarcaListar(String pOrigen, Int32 pIDMarca)
		{
			SqlCommand cmd = ConexionCmd("veh.MarcaListar");
			cmd.Parameters.Add("@Origen", SqlDbType.VarChar, 25).Value = pOrigen;
			cmd.Parameters.Add("@IDMarca", SqlDbType.Int).Value = pIDMarca;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					BEMarcaModeloVersion oBE = new BEMarcaModeloVersion();
					oBE.IDMarca = rd.GetInt32(rd.GetOrdinal("IDMarca"));
					oBE.Marca = rd.GetString(rd.GetOrdinal("Marca"));
					oBE.Origen = rd.GetString(rd.GetOrdinal("Origen"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
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

		public IList ModeloListar(String pOrigen, Int32 pIDMarca, Int32 pIDModelo)
		{
			SqlCommand cmd = ConexionCmd("veh.ModeloListar");
			cmd.Parameters.Add("@Origen", SqlDbType.VarChar, 25).Value = pOrigen;
			cmd.Parameters.Add("@IDMarca", SqlDbType.Int).Value = pIDMarca;
			cmd.Parameters.Add("@IDModelo", SqlDbType.Int).Value = pIDModelo;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					BEMarcaModeloVersion oBE = new BEMarcaModeloVersion();
					oBE.IDModelo = rd.GetInt32(rd.GetOrdinal("IDModelo"));
					oBE.Modelo = rd.GetString(rd.GetOrdinal("Modelo"));
					oBE.IDMarca = rd.GetInt32(rd.GetOrdinal("IDMarca"));
					oBE.Marca = rd.GetString(rd.GetOrdinal("Marca"));
					oBE.Origen = rd.GetString(rd.GetOrdinal("Origen"));
					oBE.IDTipoVehiculo = rd.GetInt32(rd.GetOrdinal("IDTipoVehiculo"));
					oBE.TipoVehiculo = rd.GetString(rd.GetOrdinal("TipoVehiculo"));
					oBE.NumeroAsientos = rd.GetInt32(rd.GetOrdinal("NumeroAsientos"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
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

		public IList ModeloVersionListar(String pOrigen, Int32 pIDMarca, Int32 pIDModelo, Int32 pIDModeloVersion)
		{
			SqlCommand cmd = ConexionCmd("veh.ModeloVersionListar");
			cmd.Parameters.Add("@Origen", SqlDbType.VarChar, 25).Value = pOrigen;
			cmd.Parameters.Add("@IDMarca", SqlDbType.Int).Value = pIDMarca;
			cmd.Parameters.Add("@IDModelo", SqlDbType.Int).Value = pIDModelo;
			cmd.Parameters.Add("@IDModeloVersion", SqlDbType.Int).Value = pIDModeloVersion;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					BEMarcaModeloVersion oBE = new BEMarcaModeloVersion();
					oBE.IDModeloVersion = rd.GetInt32(rd.GetOrdinal("IDModeloVersion"));
					oBE.ModeloVersion = rd.GetString(rd.GetOrdinal("ModeloVersion"));
					oBE.IDModelo = rd.GetInt32(rd.GetOrdinal("IDModelo"));
					oBE.Modelo = rd.GetString(rd.GetOrdinal("Modelo"));
					oBE.IDMarca = rd.GetInt32(rd.GetOrdinal("IDMarca"));
					oBE.Marca = rd.GetString(rd.GetOrdinal("Marca"));
					oBE.Origen = rd.GetString(rd.GetOrdinal("Origen"));
					oBE.IDTipoVehiculo = rd.GetInt32(rd.GetOrdinal("IDTipoVehiculo"));
					oBE.TipoVehiculo = rd.GetString(rd.GetOrdinal("TipoVehiculo"));
					oBE.NumeroAsientos = rd.GetInt32(rd.GetOrdinal("NumeroAsientos"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
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

		public BERetornoTran MarcaGuardar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("veh.MarcaGuardar");
			cmd = LlenarEstructura(pEntidad, cmd, "MA");
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

		public BERetornoTran ModeloGuardar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("veh.ModeloGuardar");
			cmd = LlenarEstructura(pEntidad, cmd, "MO");
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

		public BERetornoTran ModeloVersionGuardar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("veh.ModeloVersionGuardar");
			cmd = LlenarEstructura(pEntidad, cmd, "MV");
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

		public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand pcmd, string pTipoTransaccion)
		{
			BEMarcaModeloVersion oBE = (BEMarcaModeloVersion)pEntidad;
			if (pTipoTransaccion == "MA")
			{
				pcmd.Parameters.Add("@IDMarca", SqlDbType.Int).Value = oBE.IDMarca;
				pcmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = oBE.Marca;
				pcmd.Parameters.Add("@Origen", SqlDbType.VarChar, 25).Value = oBE.Origen;
			}
			if (pTipoTransaccion == "MO")
			{
				pcmd.Parameters.Add("@IDModelo", SqlDbType.Int).Value = oBE.IDModelo;
				pcmd.Parameters.Add("@IDMarca", SqlDbType.Int).Value = oBE.IDMarca;
				pcmd.Parameters.Add("@IDTipoVehiculo", SqlDbType.Int).Value = oBE.IDTipoVehiculo;
				pcmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = oBE.Modelo;
				pcmd.Parameters.Add("@NumeroAsientos", SqlDbType.Int).Value = oBE.NumeroAsientos;
			}
			if (pTipoTransaccion == "MV")
			{
				pcmd.Parameters.Add("@IDModeloVersion", SqlDbType.Int).Value = oBE.IDModeloVersion;
				pcmd.Parameters.Add("@IDModelo", SqlDbType.Int).Value = oBE.IDModelo;
				pcmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = oBE.ModeloVersion;
			}
			pcmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
			pcmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
			pcmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			pcmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			return pcmd;
		}
		 
	}
}