
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE;

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLProveedor : BLBase
    {   
		public IList ProveedorFiltroListar(Int32 pIDEmpresa, String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("gen.ProveedorFiltroListar");
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
			BEProveedor oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProveedor();
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
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
                    oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));
                    oBE.NroCategoria = rd.GetString(rd.GetOrdinal("NroCategoria"));
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

        public IList ProveedorxRequisicionListar(Int32 pIDRequisicion, String pIDProveedores)
        {
            SqlCommand cmd = ConexionCmd("gen.ProveedorxRequisicionListar");
            cmd.Parameters.Add("@IDRequisicion", SqlDbType.Int).Value = pIDRequisicion;
            cmd.Parameters.Add("@IDProveedores", SqlDbType.VarChar,200).Value = pIDProveedores;
             
            BEProveedor oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                { 
                    oBE = new BEProveedor();
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor")); 
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial")); 
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
          
        public BEProveedor ProveedorSeleccionar(int pCodigo)
		{
			SqlCommand cmd = ConexionCmd("gen.ProveedorSeleccionar");
			BEProveedor oBE = new BEProveedor();
			cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = pCodigo;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
					oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
					oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
					oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
					oBE.NombreComercial = rd.GetString(rd.GetOrdinal("NombreComercial"));
					oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
                    oBE.Ubigeo = rd.GetString(rd.GetOrdinal("Ubigeo"));
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
					oBE.Urbanizacion = rd.GetString(rd.GetOrdinal("Urbanizacion"));
					oBE.Correo = rd.GetString(rd.GetOrdinal("Correo"));
                    oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));

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

        public BEProveedor ProveedorxNumeroDocumentoSeleccionar(String pNumeroDocumento, Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.ProveedorxNumeroDocumentoSeleccionar");
            BEProveedor oBE = new BEProveedor();
            cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar,20).Value = pNumeroDocumento;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
                    oBE.IDTipoDocumento = rd.GetInt32(rd.GetOrdinal("IDTipoDocumento"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.RazonSocial = rd.GetString(rd.GetOrdinal("RazonSocial"));
                    oBE.NombreComercial = rd.GetString(rd.GetOrdinal("NombreComercial"));
                    oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
                    oBE.Urbanizacion = rd.GetString(rd.GetOrdinal("Urbanizacion"));
                    oBE.Correo = rd.GetString(rd.GetOrdinal("Correo"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));

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
         
        public BERetornoTran ProveedorGuardar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ProveedorGuardar");
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

		public BERetornoTran ProveedorActualizar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ProveedorActualizar");
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

        public BERetornoTran ProveedorRapidoGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            BEProveedor oBE = (BEProveedor)pEntidad;
            SqlCommand cmd = ConexionCmd("gen.ProveedorRapidoGuardar");
            cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 200).Value = oBE.NumeroDocumento;
            cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = oBE.IDTipoDocumento;
            cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 200).Value = oBE.RazonSocial;
            cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 200).Value = oBE.Direccion;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa; 
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

        public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand cmd, String pTipoTransaccion)
		{
			BEProveedor oBE = (BEProveedor)pEntidad;
            if (pTipoTransaccion.Equals("I")) {
                cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
            }

			cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = oBE.IDProveedor; 
            cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = oBE.IDTipoDocumento;
			cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 200).Value = oBE.NumeroDocumento;
			cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 200).Value = oBE.RazonSocial;
			cmd.Parameters.Add("@NombreComercial", SqlDbType.VarChar,200).Value = oBE.NombreComercial;
			cmd.Parameters.Add("@IDUbigeo", SqlDbType.VarChar, 10).Value = oBE.IDUbigeo;
			cmd.Parameters.Add("@Direccion", SqlDbType.VarChar,200).Value = oBE.Direccion;
			cmd.Parameters.Add("@Urbanizacion", SqlDbType.VarChar,200).Value = oBE.Urbanizacion;
			cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = oBE.Correo;
            cmd.Parameters.Add("@Celular", SqlDbType.VarChar, 100).Value = oBE.Celular;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			return cmd;
		}

		public BERetornoTran ProveedorEliminar(Int32 pIDProveedor)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ProveedorEliminar");
			cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = pIDProveedor;
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