using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
    public class BLVentaFormaPago : BLBase
    {
        public IList VentaFormaPagoListar(Int32 pIDVenta)
        {
            SqlCommand cmd = ConexionCmd("gen.VentaFormaPagoListar");
            cmd.Parameters.Add("@IDVenta", SqlDbType.Int).Value = pIDVenta;
            BEVentaFormaPago oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEVentaFormaPago();
                    oBE.IDVentaFormaPago = rd.GetInt32(rd.GetOrdinal("IDVentaFormaPago"));
                    oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
                    oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
                    oBE.IDTarjetaCredito = rd.GetInt32(rd.GetOrdinal("IDTarjetaCredito"));
                    oBE.MontoPagado = rd.GetDecimal(rd.GetOrdinal("MontoPagado"));
                    oBE.NumeroOperacion = rd.GetString(rd.GetOrdinal("NumeroOperacion"));
                    oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));
                    oBE.TarjetaCredito = rd.GetString(rd.GetOrdinal("TarjetaCredito"));

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

        public IList VentaFormaPagoSunatListar(Int32 pIDVenta)
        {
            SqlCommand cmd = ConexionCmd("gen.VentaFormaPagoSunatListar");
            cmd.Parameters.Add("@IDVenta", SqlDbType.Int).Value = pIDVenta;
            BEVentaFormaPago oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEVentaFormaPago();
                    oBE.ID = rd.GetInt32(rd.GetOrdinal("IDVentaFormaPago"));
                    oBE.IDVentaFormaPago = rd.GetInt32(rd.GetOrdinal("IDVentaFormaPago"));
                    oBE.IDVenta = rd.GetInt32(rd.GetOrdinal("IDVenta"));
                    oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago"));                    
                    oBE.ImportePago = rd.GetDecimal(rd.GetOrdinal("ImportePago"));
                    oBE.FechaPago = rd.GetString(rd.GetOrdinal("FechaPago"));                                    
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

        public BERetornoTran VentaFormaPagoGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlCommand cmd = ConexionCmd("gen.VentaFormaPagoGuardar");
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
            BEVentaFormaPago oBE = (BEVentaFormaPago)pEntidad;
            cmd.Parameters.Add("@IDVenta", SqlDbType.Int).Value = oBE.IDVenta;
            cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = oBE.IDFormaPago;
            cmd.Parameters.Add("@IDTarjetaCredito", SqlDbType.Int).Value = oBE.IDTarjetaCredito;
            cmd.Parameters.Add("@MontoPagado", SqlDbType.Decimal).Value = oBE.MontoPagado;
            cmd.Parameters.Add("@NumeroOperacion", SqlDbType.VarChar, 15).Value = oBE.NumeroOperacion; 
            cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
            cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
            return cmd;
        }

    }
}
