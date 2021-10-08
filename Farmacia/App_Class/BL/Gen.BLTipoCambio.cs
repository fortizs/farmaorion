using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLTipoCambio : BLBase
    { 
        public IList TipoCambioListar(String pAnio, String pMes, Int32 pIDEmpresa)
        {
            SqlCommand cmd = ConexionCmd("gen.TipoCambioListar");
            cmd.Parameters.Add("@Anio", SqlDbType.VarChar,10).Value = pAnio;
            cmd.Parameters.Add("@Mes", SqlDbType.VarChar, 10).Value = pMes;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pIDEmpresa;
            BETipoCambio oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BETipoCambio();
                    oBE.IDTipoCambio = rd.GetInt32(rd.GetOrdinal("IDTipoCambio"));
                    oBE.IDMoneda = rd.GetString(rd.GetOrdinal("IDMoneda"));
                    oBE.FechaPublicacion = rd.GetDateTime(rd.GetOrdinal("FechaPublicacion"));
                    oBE.PrecioCompra = rd.GetDecimal(rd.GetOrdinal("PrecioCompra"));
                    oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta")); 
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

        public BETipoCambio TipoCambioSeleccionar(DateTime pFechaPublicacion)
        {
            SqlCommand cmd = ConexionCmd("gen.TipoCambioSeleccionar");
            BETipoCambio oBE = new BETipoCambio();
            cmd.Parameters.Add("@FechaPublicacion", SqlDbType.DateTime).Value = pFechaPublicacion;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.PrecioCompra = rd.GetDecimal(rd.GetOrdinal("PrecioCompra"));
                    oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
                     
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
         
        public BERetornoTran TipoCambioSincronizarGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.TipoCambioSincronizarGuardar");
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

        public BERetornoTran TipoCambioSincronizarEliminar(String pAnio, String pMes, Int32 pIDUsuario)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.TipoCambioSincronizarEliminar");
            cmd.Parameters.Add("@Anio", SqlDbType.VarChar, 10).Value = pAnio;
            cmd.Parameters.Add("@Mes", SqlDbType.VarChar, 10).Value = pMes;
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
          
        public SqlCommand LlenarEstructura(BEBase pEntidad, SqlCommand cmd, String pTipoTransaccion)
        {
            BETipoCambio oBE = (BETipoCambio)pEntidad;
            cmd.Parameters.Add("@Anio", SqlDbType.VarChar, 10).Value = oBE.Anio;
            cmd.Parameters.Add("@Mes", SqlDbType.VarChar, 10).Value = oBE.Mes;
            cmd.Parameters.Add("@NumeroDia", SqlDbType.Int).Value = oBE.NumeroDia;
            cmd.Parameters.Add("@PrecioCompra", SqlDbType.Decimal).Value = oBE.PrecioCompra;
            cmd.Parameters.Add("@PrecioVenta", SqlDbType.Decimal).Value = oBE.PrecioVenta; 
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return cmd;
        }
		 
		public BERetornoTran TipoCambioGuardar(BETipoCambio oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.TipoCambioGuardar"); 
			cmd.Parameters.Add("@IDTipoCambio", SqlDbType.Int).Value = oBE.IDTipoCambio;
			cmd.Parameters.Add("@FechaPublicacion", SqlDbType.DateTime).Value = oBE.FechaPublicacion;
			cmd.Parameters.Add("@IDMoneda", SqlDbType.VarChar).Value = oBE.IDMoneda;
			cmd.Parameters.Add("@PrecioCompra", SqlDbType.Decimal).Value = oBE.PrecioCompra;
			cmd.Parameters.Add("@PrecioVenta", SqlDbType.Decimal).Value = oBE.PrecioVenta;
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
		 
	}
}
