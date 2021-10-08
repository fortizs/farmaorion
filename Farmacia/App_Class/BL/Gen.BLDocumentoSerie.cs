using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLDocumentoSerie : BLBase
    { 
        public String DocumentoSerieListar(String pIDTipoComprobante, Int32 pIDSucursal)
        {
            SqlCommand cmd = ConexionCmd("gen.DocumentoSerieListar");
            cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Char,2).Value = pIDTipoComprobante;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            String pValor = "";
            ArrayList lista = new ArrayList();
            try
            { 
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                { 
                    pValor = rd.GetString(rd.GetOrdinal("Serie"));  
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
            return pValor;
        }
         
        public IList DocumentoSeriexSucursalListar(Int32 pIDSucursal)
        {
            SqlCommand cmd = ConexionCmd("gen.DocumentoSeriexSucursalListar");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            BEDocumentoSerie oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEDocumentoSerie();
                    oBE.IDDocumentoSerie = rd.GetInt32(rd.GetOrdinal("IDDocumentoSerie"));
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("IDTipoComprobante"));
                    oBE.Serie = rd.GetString(rd.GetOrdinal("Serie"));
                    oBE.Numero = rd.GetInt32(rd.GetOrdinal("Numero"));
                    oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
                    oBE.DocumentoReferencia = rd.GetString(rd.GetOrdinal("DocumentoReferencia"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.TipoComprobante = rd.GetString(rd.GetOrdinal("TipoComprobante"));
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

        public BEDocumentoSerie DocumentoSerieSeleccionar(int pCodigo)
        {
            SqlCommand cmd = ConexionCmd("gen.DocumentoSerieSeleccionar");
            BEDocumentoSerie  oBE = new BEDocumentoSerie();
            cmd.Parameters.Add("@IDDocumentoSerie", SqlDbType.Int).Value = pCodigo;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDDocumentoSerie = rd.GetInt32(rd.GetOrdinal("IDDocumentoSerie"));
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.IDTipoComprobante = rd.GetInt32(rd.GetOrdinal("IDTipoComprobante"));
                    oBE.Serie = rd.GetString(rd.GetOrdinal("Serie"));
                    oBE.Numero = rd.GetInt32(rd.GetOrdinal("Numero"));
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

        public String DocumentoSerieNotaSeleccionar(String pIDTipoComprobante, String pDocumentoReferencia, Int32 pIDSucursal)
        {
            SqlCommand cmd = ConexionCmd("gen.DocumentoSerieNotaSeleccionar");
            cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.VarChar).Value = pIDTipoComprobante;
            cmd.Parameters.Add("@DocumentoReferencia", SqlDbType.VarChar).Value = pDocumentoReferencia;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            String pValor = "";
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    pValor = rd.GetString(rd.GetOrdinal("Serie"));
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
            return pValor;
        }
           
        public BERetornoTran DocumentoSerieInsertar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.DocumentoSerieInsertar");
            BEDocumentoSerie oBE = (BEDocumentoSerie)pEntidad;
            cmd.Parameters.Add("@IDDocumentoSerie", SqlDbType.Int).Value = oBE.IDDocumentoSerie;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = oBE.IDSucursal;
            cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int).Value = oBE.IDTipoComprobante;
            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = oBE.Serie;
            cmd.Parameters.Add("@Numero", SqlDbType.Int).Value = oBE.Numero;
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

        public BERetornoTran DocumentoSerieActualizar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.DocumentoSerieActualizar");
            BEDocumentoSerie oBE = (BEDocumentoSerie)pEntidad;
            cmd.Parameters.Add("@IDDocumentoSerie", SqlDbType.Int).Value = oBE.IDDocumentoSerie;
            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = oBE.Serie;
            cmd.Parameters.Add("@Numero", SqlDbType.Int).Value = oBE.Numero;
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

		public BERetornoTran DocumentoSerieEliminar(Int32 pIDDocumentoSerie)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.DocumentoSerieEliminar"); 
			cmd.Parameters.Add("@IDDocumentoSerie", SqlDbType.Int).Value = pIDDocumentoSerie; 
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
