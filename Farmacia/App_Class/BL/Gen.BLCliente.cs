
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE;

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLCliente : BLBase
    {
        #region No Transaccional
        public IList ClienteListar(String pFiltro, Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.ClienteListar");
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            BECliente oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BECliente();
                    oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
                    oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
                    oBE.NombreComercial = rd.GetString(rd.GetOrdinal("NombreComercial"));
                    oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
                    oBE.Urbanizacion = rd.GetString(rd.GetOrdinal("Urbanizacion"));
                    oBE.Correo = rd.GetString(rd.GetOrdinal("Correo"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.Ubigeo = rd.GetString(rd.GetOrdinal("Ubigeo"));
					oBE.LimiteCredito = rd.GetDecimal(rd.GetOrdinal("LimiteCredito"));
					oBE.DiasCredito = rd.GetInt32(rd.GetOrdinal("DiasCredito"));

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

        public BECliente ClienteSeleccionar(int pCodigo)
        {
            SqlCommand cmd = ConexionCmd("gen.ClienteSeleccionar");
            BECliente oBE = new BECliente();
            cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pCodigo;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
                    oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
                    oBE.NombreComercial = rd.GetString(rd.GetOrdinal("NombreComercial"));
                    oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
                    oBE.Ubigeo = rd.GetString(rd.GetOrdinal("Ubigeo"));
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
                    oBE.Urbanizacion = rd.GetString(rd.GetOrdinal("Urbanizacion"));
                    oBE.Correo = rd.GetString(rd.GetOrdinal("Correo"));

					oBE.LimiteCredito = rd.GetDecimal(rd.GetOrdinal("LimiteCredito"));
					oBE.DiasCredito = rd.GetInt32(rd.GetOrdinal("DiasCredito"));
					oBE.CreditoDisponible = rd.GetDecimal(rd.GetOrdinal("CreditoDisponible"));

					oBE.FechaNacimiento = rd.GetDateTime(rd.GetOrdinal("FechaNacimiento"));
					oBE.Edad = rd.GetInt32(rd.GetOrdinal("Edad"));
					oBE.Telefono = rd.GetString(rd.GetOrdinal("Telefono"));
					oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));
					oBE.Sexo = rd.GetString(rd.GetOrdinal("Sexo"));

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

        public BECliente ClienteXNumeroDocumentoSeleccionar(String pNumeroDocumento, Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.ClienteXNumeroDocumentoSeleccionar");
            BECliente oBE = new BECliente();
            cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 20).Value = pNumeroDocumento;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
                    oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
                    oBE.NombreComercial = rd.GetString(rd.GetOrdinal("NombreComercial"));
                    oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
                    oBE.Urbanizacion = rd.GetString(rd.GetOrdinal("Urbanizacion"));
                    oBE.Correo = rd.GetString(rd.GetOrdinal("Correo"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
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

        public IList ClientePendienteCobrarListar(String pFiltro, Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.ClientePendienteCobrarListar");
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            BECliente oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BECliente();
                    oBE.IDCliente = rd.GetInt32(rd.GetOrdinal("IDCliente"));
                    oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
                    oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));
                    oBE.ImporteTotal = rd.GetDecimal(rd.GetOrdinal("ImporteTotal"));
                    oBE.SaldoTotal = rd.GetDecimal(rd.GetOrdinal("SaldoTotal"));
                    

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


		public BECliente FechaVencimientoxNumeroDiaCreditoCliente(Int32 pNumeroDiaCredito)
		{
			SqlCommand cmd = ConexionCmd("gen.FechaVencimientoxNumeroDiaCreditoCliente");
			BECliente oBE = new BECliente(); 
			cmd.Parameters.Add("@NumeroDiaCredito", SqlDbType.Int).Value = pNumeroDiaCredito;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));  
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
		public BERetornoTran ClienteGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.ClienteGuardar");
            cmd = LlenarEstructura(pEntidad, cmd, "I");
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

        public BERetornoTran ClienteActualizar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.ClienteActualizar");
            cmd = LlenarEstructura(pEntidad, cmd, "A");
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

        public BERetornoTran ClienteRapidoGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.ClienteRapidoGuardar");
            cmd = LlenarEstructura(pEntidad, cmd, "IR");
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

        public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand cmd, String pTipoTransaccion)
        {
            BECliente oBE = (BECliente)pEntidad;
            if (pTipoTransaccion.Equals("I") || pTipoTransaccion.Equals("A"))
            {
                cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = oBE.IDCliente;
                cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = oBE.IDTipoDocumento;
                cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 200).Value = oBE.NumeroDocumento;
                cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 200).Value = oBE.RazonSocial;
                cmd.Parameters.Add("@NombreComercial", SqlDbType.VarChar, 200).Value = oBE.NombreComercial;
                cmd.Parameters.Add("@IDUbigeo", SqlDbType.VarChar, 10).Value = oBE.IDUbigeo.Trim() == "0" ? (Object)DBNull.Value : oBE.IDUbigeo;
				cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 200).Value = oBE.Direccion;
                cmd.Parameters.Add("@Urbanizacion", SqlDbType.VarChar, 200).Value = oBE.Urbanizacion;
				
			    cmd.Parameters.Add("@Sexo", SqlDbType.Char, 1).Value = oBE.Sexo == "0" ? (Object)DBNull.Value : oBE.Sexo;
				cmd.Parameters.Add("@FechaNacimiento", SqlDbType.DateTime).Value = oBE.FechaNacimiento.ToShortDateString() == "01/01/1900" ? (Object)DBNull.Value : oBE.FechaNacimiento;
				cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 100).Value = oBE.Telefono;
				cmd.Parameters.Add("@Celular", SqlDbType.VarChar, 100).Value = oBE.Celular;
				 
				cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = oBE.Correo;
				cmd.Parameters.Add("@LimiteCredito", SqlDbType.Decimal).Value = oBE.LimiteCredito;
				cmd.Parameters.Add("@DiasCredito", SqlDbType.Int).Value = oBE.DiasCredito;
				cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
                cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
            }
            if (pTipoTransaccion.Equals("IR"))
            {
                cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 200).Value = oBE.NumeroDocumento;
                cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = oBE.IDTipoDocumento;
                cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 200).Value = oBE.RazonSocial;
                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 200).Value = oBE.Direccion;
                cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
            }

            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return cmd;
        }

		public BERetornoTran ClienteEliminar(Int32 pIDCliente)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ClienteEliminar");
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pIDCliente;
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