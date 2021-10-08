using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL
{
	public class BLKit : BLBase
	{
        #region Transaccional

        public BERetornoTran GuardarKits(BEKit BEParam, ArrayList pKitDetalle)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			BEKit BEKitRetorno = new BEKit();
			int vSucursal = BEParam.IDSucursal;
			SqlConnection objcn = new SqlConnection(CadenaConexion());
			objcn.Open();
			SqlTransaction sqlTran = objcn.BeginTransaction();

			//VENTA------------------------------------------------------------------------------------------------

			SqlCommand cmd = new SqlCommand("gen.KitGuardar", objcn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = DuracionComando();
			cmd.Transaction = sqlTran;
			BERetorno.Retorno = "-1";

			try
			{
				cmd.Parameters.Add("@IDKit", SqlDbType.Int).Value = BEParam.IDKit;
				cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
				cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
				cmd.Parameters.Add("@NombreProducto", SqlDbType.VarChar, 200).Value = BEParam.NombreProducto;
				cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int).Value = BEParam.IDUnidadMedida;
				cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = BEParam.Cantidad;
                cmd.Parameters.Add("@Glosa", SqlDbType.VarChar, 100).Value = BEParam.Glosa;
                cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = BEParam.Estado;
				cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
				cmd.Parameters.Add("@IDKitFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
				cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDKitFinal"].Value);
				BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
				if (BERetorno.Retorno != "1")
				{
					throw new Exception(BERetorno.ErrorMensaje);
				}
				else
				{
					BEKitRetorno.IDKit = Int32.Parse(cmd.Parameters["@IDKitFinal"].Value.ToString());
				}

				//KIT DETALLE-----------------------------------------------------------------------------               
				cmd.Parameters.Clear();
				BERetorno.Retorno = "-1";
				cmd.CommandText = "gen.KitDetalleGuardar";

				BEKitDetalle oBEKitDetalle = new BEKitDetalle();
				for (Int32 i = 0; i < pKitDetalle.Count; i++)
				{
					oBEKitDetalle = (BEKitDetalle)pKitDetalle[i];
					cmd.Parameters.Add("@IDKitDetalle", SqlDbType.Int).Value = 0;
					cmd.Parameters.Add("@IDKit", SqlDbType.Int).Value = BEKitRetorno.IDKit;
					cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = oBEKitDetalle.IDProducto;
					cmd.Parameters.Add("@NombreProducto", SqlDbType.VarChar, 200).Value = oBEKitDetalle.NombreProducto;
					cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int).Value = oBEKitDetalle.IDUnidadMedida;
					cmd.Parameters.Add("@CantidadReg", SqlDbType.Decimal).Value = oBEKitDetalle.CantidadReg;
					cmd.Parameters.Add("@CantidadArmado", SqlDbType.Decimal).Value = oBEKitDetalle.CantidadArmado;
					cmd.Parameters.Add("@CantidadDisponible", SqlDbType.Decimal).Value = oBEKitDetalle.CantidadDisponible;
					cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
					cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
					cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
					cmd.ExecuteNonQuery();
					BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
					BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
					if (BERetorno.Retorno == "-1")
					{
						throw new Exception(BERetorno.ErrorMensaje);
					}
					cmd.Parameters.Clear();
				}
				sqlTran.Commit();
			}
			catch (Exception ex)
			{
				sqlTran.Rollback();
				if (BERetorno.Retorno == "-1")
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

        public BERetornoTran KitEliminar(Int32 pIDKit)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.KitEliminar");
            cmd.Parameters.Add("@IDKit", SqlDbType.Int).Value = pIDKit;
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

        #region No Transaccional

        public String KitNumeroSeleccionar()
		{
			SqlCommand cmd = ConexionCmd("gen.KitNumeroSeleccionar");
			String pNumeroKitFormateado = "";
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					pNumeroKitFormateado = rd.GetString(rd.GetOrdinal("NumeroKitFormateado"));
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
			return pNumeroKitFormateado;
		}

		public IList KitListar(Int32 pIDSucursal, String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("gen.KitListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					BEKit oBE = new BEKit();

					oBE.IDKit = rd.GetInt32(rd.GetOrdinal("IDKit"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.NombreProducto = rd.GetString(rd.GetOrdinal("NombreProducto"));
					oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
					oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
					oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.IDUsuarioCreacion = rd.GetInt32(rd.GetOrdinal("IDUsuarioCreacion"));
					oBE.FechaCreacion = rd.GetDateTime(rd.GetOrdinal("FechaCreacion"));
					//oBE.IDUsuarioModificacion = rd.GetInt32(rd.GetOrdinal("IDUsuarioModificacion"));
					//oBE.FechaModificacion = rd.GetDateTime(rd.GetOrdinal("FechaModificacion"));
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

        public BEKit KitSeleccionar(Int32 pIDKit, Int32 pIDSucursal)
        {
            BEKit oBE = new BEKit();
            SqlCommand cmd = ConexionCmd("gen.KitSeleccionar");
            cmd.Parameters.Add("@IDKit", SqlDbType.Int).Value = pIDKit;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;

            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDKit = rd.GetInt32(rd.GetOrdinal("IDKit"));
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
                    oBE.NombreProducto = rd.GetString(rd.GetOrdinal("NombreProducto"));
                    oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
                    oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
                    oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));                   
                    oBE.Glosa = rd.GetString(rd.GetOrdinal("Glosa"));
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