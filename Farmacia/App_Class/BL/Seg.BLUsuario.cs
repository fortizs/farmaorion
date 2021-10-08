using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Seguridad;
using Farmacia.App_Class.BE.General;

namespace Farmacia.App_Class.BL.Seguridad
{
    public class BLUsuario : BLBase 
    { 
        public IList UsuarioListar(String pFiltro, Int32 pIDSucursal)
        {
            SqlCommand cmd = ConexionCmd("seg.UsuarioListar");
            BEUsuario oBE;
            cmd.Parameters.Add("@Buscar", SqlDbType.VarChar, 50).Value = pFiltro;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read()) 
                {
                    oBE = new BEUsuario();
                    oBE.IDUsuario = rd.GetInt32(rd.GetOrdinal("IDUsuario"));
                    oBE.IDColaborador = rd.GetInt32(rd.GetOrdinal("IDColaborador")); 
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.NombreCompleto = rd.GetString(rd.GetOrdinal("NombreCompleto"));
                    oBE.Usuario = rd.GetString(rd.GetOrdinal("Usuario"));   
                    oBE.EstadoUsuario = rd.GetBoolean(rd.GetOrdinal("EstadoUsuario"));
                    oBE.EstadoColaborador = rd.GetBoolean(rd.GetOrdinal("EstadoColaborador"));
                    oBE.UltimoBloqueo = rd.GetString(rd.GetOrdinal("UltimoBloqueo"));
                    oBE.UltimoLogin = rd.GetString(rd.GetOrdinal("UltimoLogin"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal")); 
					oBE.UsuarioModificacion = rd.GetString(rd.GetOrdinal("UsuarioModificacion"));
					oBE.FechaModificacion = rd.GetDateTime(rd.GetOrdinal("FechaModificacion"));

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

		public BEUsuario Seleccionar(Int32 pIDUsuario, Int32 pIDColaborador)
		{
			SqlCommand cmd = ConexionCmd("seg.UsuarioColaboradorSeleccionar");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			cmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = pIDColaborador;
			BEUsuario oBE = new BEUsuario(); 
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.IDUsuario = rd.GetInt32(rd.GetOrdinal("IDUsuario"));
					oBE.Usuario = rd.GetString(rd.GetOrdinal("Usuario"));
					oBE.Nombres = rd.GetString(rd.GetOrdinal("Nombres"));
					oBE.ApellidoPaterno = rd.GetString(rd.GetOrdinal("ApellidoPaterno"));
					oBE.ApellidoMaterno = rd.GetString(rd.GetOrdinal("ApellidoMaterno"));
					oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
					oBE.NombreCompleto = rd.GetString(rd.GetOrdinal("NombreCompleto"));
					oBE.Email = rd.GetString(rd.GetOrdinal("Email"));
					oBE.EstadoUsuario = rd.GetBoolean(rd.GetOrdinal("EstadoUsuario"));
					oBE.IDColaborador = rd.GetInt32(rd.GetOrdinal("IDColaborador"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDCargo = rd.GetInt32(rd.GetOrdinal("IDCargo"));
					oBE.EstadoColaborador = rd.GetBoolean(rd.GetOrdinal("EstadoColaborador"));
					oBE.Bloqueado = rd.GetBoolean(rd.GetOrdinal("Bloqueado"));
					oBE.CambiarClave = rd.GetBoolean(rd.GetOrdinal("CambiarClave"));
					oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));
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
		 
        public BEUsuario ValidarUsuario(String pUsuario, String pClave, String pRuc)
        {
            SqlCommand cmd = ConexionCmd("seg.UsuarioValidar");
            BEUsuario oBE = new BEUsuario();
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = pUsuario;
            cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 100).Value = oBE.GetMD5(pClave);
            cmd.Parameters.Add("@Ruc", SqlDbType.VarChar, 20).Value = pRuc;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDUsuario = rd.GetInt32(rd.GetOrdinal("IDUsuario"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.NombreCompleto = rd.GetString(rd.GetOrdinal("NombreCompleto"));
                    oBE.Usuario = rd.GetString(rd.GetOrdinal("Usuario"));
                    oBE.IDColaborador = rd.GetInt32(rd.GetOrdinal("IDColaborador"));
                    oBE.Acceso = rd.GetBoolean(rd.GetOrdinal("Acceso"));
                    oBE.Bloqueado = rd.GetBoolean(rd.GetOrdinal("Bloqueado"));
                    oBE.CambiarClave = rd.GetBoolean(rd.GetOrdinal("CambiarClave"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.Empresa = rd.GetString(rd.GetOrdinal("Empresa"));
                    oBE.ColorInterfaz = rd.GetString(rd.GetOrdinal("Color"));
                    oBE.EsPrincipal = rd.GetBoolean(rd.GetOrdinal("EsPrincipal"));
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                oBE.Mensaje = ex.ToString();
                //throw ex;
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

        public String ValidarNombreUsuario(String pUsuario)
        {
            SqlCommand cmd = ConexionCmd("seg.UsuarioValidarNombre");
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = pUsuario;
            cmd.Parameters["@Usuario"].Direction = ParameterDirection.InputOutput; 
            String Resultado = "/Error/";
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                Resultado = cmd.Parameters["@Usuario"].Value.ToString();
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
            return Resultado;
        }

        public Boolean UsuarioEsAdmin(Int32 pIDUsuario, Int32 pIDRolAdmin)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            Boolean EsAdmin = false;
            SqlCommand cmd = ConexionCmd("seg.UsuarioEsAdmin");            
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
            cmd.Parameters.Add("@IDRol", SqlDbType.Int).Value = pIDRolAdmin;
            cmd.Parameters.Add("@EsAdmin", SqlDbType.Bit).Direction = ParameterDirection.Output;            
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                EsAdmin = Convert.ToBoolean(cmd.Parameters["ReturnValue"].Value);                
            }
            catch (Exception ex)
            {
                //BERetorno.ErrorMensaje = ex.ToString();
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return EsAdmin;
        }        

        #region Transaccional

        public BERetornoTran Insertar(BEBase pEntidad)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.UsuarioColaboradorGrabar");
			cmd = LlenarEstructura(pEntidad, cmd, "I");
			cmd.Connection.Open();
			SqlTransaction sqlTran = cmd.Connection.BeginTransaction();
			cmd.Transaction = sqlTran;
			try
			{
				cmd.ExecuteNonQuery();
				BEUsuario oBEUsuario = (BEUsuario)pEntidad;
				oBEUsuario.IDColaborador = Int32.Parse(cmd.Parameters["@IDColaborador"].Value.ToString());
				if (oBEUsuario.IDUsuario != -1)
				{
					if (oBEUsuario.IDUsuario == 0)
						oBEUsuario.IDUsuario = Int32.Parse(cmd.Parameters["@IDUsuario"].Value.ToString());
					//Eliminar roles del perfil anterior del usuario-------------------------------------------
					cmd.Parameters.Clear();
					cmd.CommandText = "seg.UsuarioRolEliminar";
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBEUsuario.IDUsuario;
					if (oBEUsuario.IDPerfil == -1)
					{
						cmd.Parameters.Add("@IDPerfil", SqlDbType.Int).Value = DBNull.Value;
					}
					else
					{
						cmd.Parameters.Add("@IDPerfil", SqlDbType.Int).Value = oBEUsuario.IDPerfil;
					}
					cmd.Parameters.Add("@IDUsuarioAuditoria", SqlDbType.Int).Value = oBEUsuario.IDUsuarioAuditoria;
					cmd.ExecuteNonQuery();
					//Grabar roles del usuario--------------------------------------------
					IList ListaRol = oBEUsuario.ListaRol;
					if (ListaRol != null)
					{
						cmd.CommandText = "seg.UsuarioRolGrabar";
						foreach (var oItem in ListaRol)
						{
							BEUsuarioRol oBEUsuarioRol = (BEUsuarioRol)oItem;
							cmd.Parameters.Clear();
							cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBEUsuario.IDUsuario;
							cmd.Parameters.Add("@IDRol", SqlDbType.Int).Value = oBEUsuarioRol.IDRol;
							cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = true;
							cmd.Parameters.Add("@IDUsuarioAuditoria", SqlDbType.Int).Value = oBEUsuario.IDUsuarioAuditoria;
							cmd.ExecuteNonQuery();
						}
					}
				}
				sqlTran.Commit();
				BERetorno.Retorno = "1";
			}
			catch (Exception ex)
			{
				BERetorno.Retorno = "-1";
				BERetorno.ErrorMensaje = ex.ToString();
				sqlTran.Rollback();
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

		public BERetornoTran Registrar(BEUsuario pBEUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("seg.UsuarioRegistrar");
			cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 20).Value = pBEUsuario.NumeroDocumento;
			cmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, 200).Value = pBEUsuario.NombreCompleto;
			cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = pBEUsuario.Usuario;
			cmd.Parameters.Add("@Email", SqlDbType.VarChar, 250).Value = pBEUsuario.Email;
			cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 50).Value = pBEUsuario.Telefono;
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

		public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand pcmd, String pTipoTransaccion)
		{
			BEUsuario oBE = (BEUsuario)pEntidad;
			pcmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
			pcmd.Parameters["@IDUsuario"].Direction = ParameterDirection.InputOutput;
			pcmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = oBE.IDSucursal;
			pcmd.Parameters.Add("@IDCargo", SqlDbType.Int).Value = oBE.IDCargo;
			pcmd.Parameters.Add("@Nombres", SqlDbType.VarChar, 200).Value = oBE.Nombres;
			pcmd.Parameters.Add("@ApellidoPaterno", SqlDbType.VarChar, 200).Value = oBE.ApellidoPaterno;
			pcmd.Parameters.Add("@ApellidoMaterno", SqlDbType.VarChar, 200).Value = oBE.ApellidoMaterno;
			pcmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 20).Value = oBE.NumeroDocumento;
			pcmd.Parameters.Add("@Email", SqlDbType.VarChar, 250).Value = oBE.Email;
			pcmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = oBE.Usuario;
			pcmd.Parameters.Add("@Clave", SqlDbType.VarChar, 100).Value = "";// oBE.GetMD5(oBE.Clave); 
			pcmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = oBE.IDColaborador;
			pcmd.Parameters["@IDColaborador"].Direction = ParameterDirection.InputOutput;
			pcmd.Parameters.Add("@EstadoUsuario", SqlDbType.Bit).Value = oBE.EstadoUsuario;

			pcmd.Parameters.Add("@Bloqueado", SqlDbType.Bit).Value = oBE.Bloqueado; 
			pcmd.Parameters.Add("@CambiarClave", SqlDbType.Bit).Value = oBE.CambiarClave;
			 
			pcmd.Parameters.Add("@IDUsuarioAuditoria", SqlDbType.Int).Value = oBE.IDUsuarioAuditoria;
			pcmd.Parameters.Add("@EstadoColaborador", SqlDbType.Bit).Value = oBE.EstadoColaborador;

			return pcmd;
		}

		public BERetornoTran RegistrarRecuperarClave(String pUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEUsuario oBE = new BEUsuario();
			SqlCommand cmd = ConexionCmd("seg.UsuarioRegistrarRecuperarClave");
			cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = pUsuario;
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

		public BERetornoTran ValidarTokenOperacion(String pTokenOperacion, String pTipoOperacion)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEUsuario oBE = new BEUsuario();
			SqlCommand cmd = ConexionCmd("seg.UsuarioValidarTokenOperacion");
			cmd.Parameters.Add("@TokenOperacion", SqlDbType.VarChar, 50).Value = pTokenOperacion;
			cmd.Parameters.Add("@TipoOperacion", SqlDbType.Char, 2).Value = pTipoOperacion;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDEmpresa"].Value);
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

		public BERetornoTran CambiarClave(Int32 pIDUsuario, String pClave, Int32 pIDUsuarioAuditoria)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEUsuario oBE = new BEUsuario();
			SqlCommand cmd = ConexionCmd("seg.UsuarioCambiarClave");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 100).Value = oBE.GetMD5(pClave);
			cmd.Parameters.Add("@IDUsuarioAuditoria", SqlDbType.Int).Value = pIDUsuarioAuditoria;
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

		public BERetornoTran CambiarClave(Int32 pIDUsuario, String pClaveActual, String pClaveNueva, Int32 CantidadClaveSinRepetir, Int32 pIDUsuarioAuditoria, Boolean pRequiereClaveActual = true)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEUsuario oBE = new BEUsuario();
			SqlCommand cmd = ConexionCmd("seg.UsuarioCambiarClaveU");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			cmd.Parameters.Add("@ClaveActual", SqlDbType.VarChar, 100).Value = oBE.GetMD5(pClaveActual);
			cmd.Parameters.Add("@ClaveNueva", SqlDbType.VarChar, 100).Value = oBE.GetMD5(pClaveNueva);
			cmd.Parameters.Add("@CantidadClave", SqlDbType.Int).Value = CantidadClaveSinRepetir;
			cmd.Parameters.Add("@RequiereClaveActual", SqlDbType.Int).Value = pRequiereClaveActual;
			cmd.Parameters.Add("@IDUsuarioAuditoria", SqlDbType.Int).Value = pIDUsuarioAuditoria;
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

		public BERetornoTran Desbloquear(Int32 pIDUsuario, Int32 pIDUsuarioAuditoria, Boolean pReiniciarClave)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEUsuario oBE = new BEUsuario();
			SqlCommand cmd = ConexionCmd("seg.UsuarioDesbloquear");
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
			cmd.Parameters.Add("@IDUsuarioAuditoria", SqlDbType.Int).Value = pIDUsuarioAuditoria;
			cmd.Parameters.Add("@ReiniciarClave", SqlDbType.Bit).Value = pReiniciarClave;
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

        public BERetornoTran UsuarioEliminar(BEUsuario pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            BEUsuario oBE = new BEUsuario();
            SqlCommand cmd = ConexionCmd("seg.UsuarioEliminar");
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pEntidad.IDUsuario;
            cmd.Parameters.Add("@IDColaborador", SqlDbType.Int).Value = pEntidad.IDColaborador;
            cmd.Parameters.Add("@IDUsuarioModificacion", SqlDbType.Int).Value = pEntidad.IDUsuarioAuditoria;            
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
