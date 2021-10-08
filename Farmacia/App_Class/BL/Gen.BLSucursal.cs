using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLSucursal : BLBase
    {
        public IList SucursalxEmpresaListar(Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.SucursalxEmpresaListar");
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            BESucursal oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BESucursal();
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.Telefono = rd.GetString(rd.GetOrdinal("Telefono"));
                    oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));
                    oBE.Email = rd.GetString(rd.GetOrdinal("Email"));
                    oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
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
         
        public BESucursal SucursalSeleccionar(Int32 pCodigo)
        {
            SqlCommand cmd = ConexionCmd("gen.SucursalSeleccionar");
            BESucursal oBE = new BESucursal();
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pCodigo;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.Telefono = rd.GetString(rd.GetOrdinal("Telefono"));
                    oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));
                    oBE.Email = rd.GetString(rd.GetOrdinal("Email"));
                    oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
                    oBE.Ubigeo = rd.GetString(rd.GetOrdinal("Ubigeo"));
                    oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
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

        public BERetornoTran SucursalGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.SucursalGuardar");
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

        public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand cmd, String pTipoTransaccion)
        {
            BESucursal oBE = (BESucursal)pEntidad;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = oBE.IDSucursal;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa; 
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 200).Value = oBE.Nombre;
            cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 200).Value = oBE.Telefono;
            cmd.Parameters.Add("@Celular", SqlDbType.VarChar, 200).Value = oBE.Celular;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = oBE.Email;
            cmd.Parameters.Add("@IDUbigeo", SqlDbType.VarChar, 200).Value = oBE.IDUbigeo;
            cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 500).Value = oBE.Direccion;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = oBE.Estado; 
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return cmd;
             
        }

		public BERetornoTran SucursalEliminar(Int32 pIDSucursal)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.SucursalEliminar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
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