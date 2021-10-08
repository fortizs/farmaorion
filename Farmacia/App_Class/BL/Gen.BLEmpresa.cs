
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLEmpresa : BLBase
    {
        #region No Transaccional

        public IList EmpresaListar()
        {
            SqlCommand cmd = ConexionCmd("gen.EmpresaListar");
            BEEmpresa oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEEmpresa();
                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));
                    oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
                    oBE.IDTema = rd.GetInt32(rd.GetOrdinal("IDTema"));
                    oBE.Ruc = rd.GetString(rd.GetOrdinal("Ruc"));
                    oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
                    oBE.NombreComercial = rd.GetString(rd.GetOrdinal("NombreComercial"));
                    oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
                    oBE.Urbanizacion = rd.GetString(rd.GetOrdinal("Urbanizacion"));
                    oBE.Pais = rd.GetString(rd.GetOrdinal("Pais"));
                    oBE.Departamento = rd.GetString(rd.GetOrdinal("Departamento"));
                    oBE.Provincia = rd.GetString(rd.GetOrdinal("Provincia"));
                    oBE.Distrito = rd.GetString(rd.GetOrdinal("Distrito"));
                    oBE.EsPrincipal = rd.GetBoolean(rd.GetOrdinal("EsPrincipal"));
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

        public BEEmpresa EmpresaSeleccionar(Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.EmpresaSeleccionar");
            BEEmpresa oBE = new BEEmpresa();
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {

                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));
                    oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
                    oBE.TipoDocumentoSunat = rd.GetInt32(rd.GetOrdinal("TipoDocumentoSunat"));
                    oBE.IDTema = rd.GetInt32(rd.GetOrdinal("IDTema"));
                    oBE.IDEmailSMTP = rd.GetInt32(rd.GetOrdinal("IDEmailSMTP"));
                    oBE.IDSunat = rd.GetInt32(rd.GetOrdinal("IDSunat"));
                    oBE.Ruc = rd.GetString(rd.GetOrdinal("Ruc"));
                    oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
                    oBE.NombreComercial = rd.GetString(rd.GetOrdinal("NombreComercial"));
                    oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
                    oBE.Ubigeo = rd.GetString(rd.GetOrdinal("Ubigeo"));
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
                    oBE.Urbanizacion = rd.GetString(rd.GetOrdinal("Urbanizacion"));
                    oBE.Pais = rd.GetString(rd.GetOrdinal("Pais"));
                    oBE.Departamento = rd.GetString(rd.GetOrdinal("Departamento"));
                    oBE.Provincia = rd.GetString(rd.GetOrdinal("Provincia"));
                    oBE.Distrito = rd.GetString(rd.GetOrdinal("Distrito"));
                    oBE.UsuarioSol = rd.GetString(rd.GetOrdinal("UsuarioSol"));
                    oBE.ClaveSol = rd.GetString(rd.GetOrdinal("ClaveSol"));
                    oBE.Correo = rd.GetString(rd.GetOrdinal("Correo"));
                    oBE.ClaveCorreo = rd.GetString(rd.GetOrdinal("ClaveCorreo"));
                    oBE.CuentaDetraccion = rd.GetString(rd.GetOrdinal("CuentaDetraccion"));
                    oBE.CuentaBancaria = rd.GetString(rd.GetOrdinal("CuentaBancaria"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
                    oBE.EsPrincipal = rd.GetBoolean(rd.GetOrdinal("EsPrincipal"));
                    oBE.Host = rd.GetString(rd.GetOrdinal("Host"));
                    oBE.Puerto = rd.GetInt32(rd.GetOrdinal("Puerto"));
                    oBE.HabilitarSSL = rd.GetBoolean(rd.GetOrdinal("HabilitarSSL"));
                    oBE.DefaultCredencial = rd.GetBoolean(rd.GetOrdinal("DefaultCredencial"));
                    oBE.Certificado = rd.GetString(rd.GetOrdinal("Certificado"));
                    oBE.ClaveCertificado = rd.GetString(rd.GetOrdinal("ClaveCertificado"));
                    oBE.EndPointUrl = rd.GetString(rd.GetOrdinal("EndPointUrl"));

					oBE.SalidaAlmacen = rd.GetString(rd.GetOrdinal("SalidaAlmacen"));
					oBE.IngresoAlmacen = rd.GetString(rd.GetOrdinal("IngresoAlmacen")); 
					oBE.ImpresionVenta = rd.GetString(rd.GetOrdinal("ImpresionVenta"));

					oBE.CodigoEstablecimiento = rd.GetString(rd.GetOrdinal("CodigoEstablecimiento"));
					oBE.TerminoCondicion = rd.GetString(rd.GetOrdinal("TerminoCondicion"));
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

        public IList EmpresaBuscarListar(String pFiltro)
        {
            SqlCommand cmd = ConexionCmd("gen.EmpresaBuscarListar");
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
            BEEmpresa oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEEmpresa();
                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));
                    oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
                    oBE.IDTema = rd.GetInt32(rd.GetOrdinal("IDTema"));
                    oBE.Ruc = rd.GetString(rd.GetOrdinal("Ruc"));
                    oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
                    oBE.NombreComercial = rd.GetString(rd.GetOrdinal("NombreComercial"));
                    oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
                    oBE.Ubigeo = rd.GetString(rd.GetOrdinal("Ubigeo")); 
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
                    oBE.Urbanizacion = rd.GetString(rd.GetOrdinal("Urbanizacion"));
                    oBE.Pais = rd.GetString(rd.GetOrdinal("Pais"));
                    oBE.Departamento = rd.GetString(rd.GetOrdinal("Departamento"));
                    oBE.Provincia = rd.GetString(rd.GetOrdinal("Provincia"));
                    oBE.Distrito = rd.GetString(rd.GetOrdinal("Distrito"));
                    oBE.EsPrincipal = rd.GetBoolean(rd.GetOrdinal("EsPrincipal"));
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

        public IList EmpresaArchivoListar(Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.ArchivoEmpresaListar");
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            BEEmpresa oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEEmpresa();
                    oBE.IDArchivoEmpresa = rd.GetInt32(rd.GetOrdinal("IDArchivoEmpresa"));
                    oBE.TipoArchivo = rd.GetString(rd.GetOrdinal("TipoArchivo"));
                    oBE.RutaArchivo = rd.GetString(rd.GetOrdinal("RutaArchivo"));
                    oBE.NombreArchivo = rd.GetString(rd.GetOrdinal("NombreArchivo"));
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

        #endregion

        #region Transaccional

        public BERetornoTran EmpresaGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            BEEmpresa oBE = (BEEmpresa)pEntidad;
            SqlCommand cmd = ConexionCmd("gen.EmpresaGuardar");
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
            cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = oBE.IDTipoDocumento;
            cmd.Parameters.Add("@IDTema", SqlDbType.Int).Value = oBE.IDTema;
            cmd.Parameters.Add("@IDEmailSMTP", SqlDbType.Int).Value = oBE.IDEmailSMTP;
            cmd.Parameters.Add("@IDSunat", SqlDbType.Int).Value = oBE.IDSunat;

            cmd.Parameters.Add("@Ruc", SqlDbType.VarChar, 15).Value = oBE.Ruc;
            cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 200).Value = oBE.RazonSocial;
            cmd.Parameters.Add("@NombreComercial", SqlDbType.VarChar, 100).Value = oBE.NombreComercial;
            cmd.Parameters.Add("@IDUbigeo", SqlDbType.VarChar, 15).Value = oBE.IDUbigeo;

            cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 300).Value = oBE.Direccion;
            cmd.Parameters.Add("@Urbanizacion", SqlDbType.VarChar, 300).Value = oBE.Urbanizacion;
            cmd.Parameters.Add("@ClaveCertificado", SqlDbType.VarChar, 100).Value = oBE.ClaveCertificado;
            cmd.Parameters.Add("@UsuarioSol", SqlDbType.VarChar, 100).Value = oBE.UsuarioSol; 
            cmd.Parameters.Add("@ClaveSol", SqlDbType.VarChar, 100).Value = oBE.ClaveSol;
            cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = oBE.Correo;
            cmd.Parameters.Add("@ClaveCorreo", SqlDbType.VarChar, 100).Value = oBE.ClaveCorreo;
			cmd.Parameters.Add("@CodigoEstablecimiento", SqlDbType.VarChar, 100).Value = oBE.CodigoEstablecimiento;

			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@IDEmpresaFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
                BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDEmpresaFinal"].Value);
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

		public BERetornoTran EmpresaTerminoActualizar(BEEmpresa oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran(); 
			SqlCommand cmd = ConexionCmd("gen.EmpresaTerminoActualizar");
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa; 
			cmd.Parameters.Add("@TerminoCondicion", SqlDbType.VarChar).Value = oBE.TerminoCondicion; 
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
		 
		public BERetornoTran EmpresaConfigGuardar(BEEmpresa oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran(); 
			SqlCommand cmd = ConexionCmd("gen.EmpresaConfigGuardar");
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa; 
			cmd.Parameters.Add("@SalidaAlmacen", SqlDbType.VarChar, 2).Value = oBE.SalidaAlmacen;
			cmd.Parameters.Add("@IngresoAlmacen", SqlDbType.VarChar, 2).Value = oBE.IngresoAlmacen;
			cmd.Parameters.Add("@ImpresionVenta", SqlDbType.VarChar, 10).Value = oBE.ImpresionVenta;
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

		public BERetornoTran DataTablasEliminar(Int32 pIDEmpresa)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.DataTablasEliminar");
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa; 
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

        public BERetornoTran EmpresaArchivoGuardar(BEEmpresa oBE) {

            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.ArchivoEmpresaInsertar");
            cmd.Parameters.Add("@IDArchivoEmpresa", SqlDbType.Int).Value = oBE.IDArchivoEmpresa;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
            cmd.Parameters.Add("@TipoArchivo", SqlDbType.VarChar, 10).Value = oBE.TipoArchivo;
            cmd.Parameters.Add("@RutaArchivo", SqlDbType.VarChar, 100).Value = oBE.RutaArchivo;
            cmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar, 100).Value = oBE.NombreArchivo;
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
		  
		public BERetornoTran ArchivoEmpresaEliminar(Int32 pIDArchivoEmpresa)
		{

			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ArchivoEmpresaEliminar");
			cmd.Parameters.Add("@IDArchivoEmpresa", SqlDbType.Int).Value = pIDArchivoEmpresa;
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
