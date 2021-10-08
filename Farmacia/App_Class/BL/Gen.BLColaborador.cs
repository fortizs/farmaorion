 
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLColaborador : BLBase
    { 
        public IList ColaboradorListar()
        {
            SqlCommand cmd = ConexionCmd("gen.ColaboradorListar"); 
            BEColaborador oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEColaborador();
                    oBE.IDColaborador = rd.GetInt32(rd.GetOrdinal("IDColaborador"));
                    oBE.Colaborador = rd.GetString(rd.GetOrdinal("Colaborador"));
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
		   
        public BEColaborador SeleccionarColaborador(Int32 pIDColaborador)
        {
            SqlCommand cmd = ConexionCmd("gen.ColaboradorSeleccionar");
            BEColaborador oBE = new BEColaborador();
            cmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = pIDColaborador;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
					oBE.IDColaborador = rd.GetInt32(rd.GetOrdinal("IDColaborador"));
					oBE.Colaborador = rd.GetString(rd.GetOrdinal("Colaborador"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDCargo = rd.GetInt32(rd.GetOrdinal("IDCargo"));
					oBE.Sexo = rd.GetString(rd.GetOrdinal("Sexo"));
					oBE.Dni = rd.GetString(rd.GetOrdinal("Dni"));
					oBE.Telefono = rd.GetString(rd.GetOrdinal("Telefono"));
					oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));
					oBE.Email = rd.GetString(rd.GetOrdinal("Email"));
					oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
					oBE.Ubigeo = rd.GetString(rd.GetOrdinal("Ubigeo"));
					oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.RutaImagenFoto = rd.GetString(rd.GetOrdinal("RutaImagenFoto"));
					oBE.NombreImagenFoto = rd.GetString(rd.GetOrdinal("NombreImagenFoto"));
					oBE.RutaNombreImagenFotoCompleto = rd.GetString(rd.GetOrdinal("RutaNombreImagenFotoCompleto"));

					
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
		 
		public ArrayList ColaboradorListarSeleccionar(Int32 pIDColaborador)
		{
			BEColaborador oBE = new BEColaborador();
			SqlCommand cmd = ConexionCmd("gen.ColaboradorSeleccionar");
			cmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = pIDColaborador;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEColaborador();
					oBE.IDColaborador = rd.GetInt32(rd.GetOrdinal("IDColaborador"));
					oBE.Colaborador = rd.GetString(rd.GetOrdinal("Colaborador"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDCargo = rd.GetInt32(rd.GetOrdinal("IDCargo"));
					oBE.Sexo = rd.GetString(rd.GetOrdinal("Sexo"));
					oBE.Dni = rd.GetString(rd.GetOrdinal("Dni"));
					oBE.Telefono = rd.GetString(rd.GetOrdinal("Telefono"));
					oBE.Celular = rd.GetString(rd.GetOrdinal("Celular"));
					oBE.Email = rd.GetString(rd.GetOrdinal("Email"));
					oBE.IDUbigeo = rd.GetString(rd.GetOrdinal("IDUbigeo"));
					oBE.Ubigeo = rd.GetString(rd.GetOrdinal("Ubigeo"));
					oBE.Direccion = rd.GetString(rd.GetOrdinal("Direccion"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.RutaImagenFoto = rd.GetString(rd.GetOrdinal("RutaImagenFoto"));
					oBE.NombreImagenFoto = rd.GetString(rd.GetOrdinal("NombreImagenFoto"));
					oBE.RutaNombreImagenFotoCompleto = rd.GetString(rd.GetOrdinal("RutaNombreImagenFotoCompleto"));
					
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
		 
		public BERetornoTran ColaboradorImagenEliminar(Int32 pIDColaborador)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.ColaboradorImagenEliminar");
			cmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = pIDColaborador;
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

		public BERetornoTran UsuarioColaboradorActualizar(BEColaborador BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.UsuarioColaboradorActualizar");
			cmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = BEParam.IDColaborador;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
			cmd.Parameters.Add("@IDEstadoCivil", SqlDbType.Int).Value = BEParam.IDEstadoCivil;
			cmd.Parameters.Add("@FechaNacimiento", SqlDbType.DateTime).Value = BEParam.FechaNacimiento;
			cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = BEParam.Telefono;
			cmd.Parameters.Add("@Celular", SqlDbType.VarChar, 20).Value = BEParam.Celular;
			cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = BEParam.Email;
			cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 15).Value = BEParam.Clave;
			cmd.Parameters.Add("@IDUbigeo", SqlDbType.VarChar, 15).Value = BEParam.IDUbigeo;
			cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 300).Value = BEParam.Direccion;
            cmd.Parameters.Add("@Sexo", SqlDbType.VarChar, 1).Value = BEParam.Sexo;
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

		public BERetornoTran ColaboradorImagenActualizar(BEColaborador BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.ColaboradorImagenActualizar");
			cmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = BEParam.IDColaborador;
			cmd.Parameters.Add("@RutaImagenFoto", SqlDbType.VarChar, 500).Value = BEParam.RutaImagenFoto;
			cmd.Parameters.Add("@NombreImagenFoto", SqlDbType.VarChar, 300).Value = BEParam.NombreImagenFoto;
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
	}
}
