using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Compras;
using Farmacia.App_Class.BE.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.General
{
    public class BLCuentaPorPagar : BLBase
    {

        #region No Transaccional
        public IList CuentasPorPagarListar(Int32 pIDSucursal, String FechaAbo, String FechaVen)
        {
            SqlCommand cmd = ConexionCmd("gen.CuentasPorPagarListar");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@FechaAbono", SqlDbType.VarChar, 10).Value = FechaAbo;
            cmd.Parameters.Add("@FechaVencimiento", SqlDbType.VarChar, 10).Value = FechaVen;
            BECuentaPorPagar oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BECuentaPorPagar();

                    oBE.IDCuentaPagar = rd.GetInt32(rd.GetOrdinal("IDCuentaPagar"));
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
                    oBE.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacen"));
                    oBE.IDConcepto = rd.GetInt32(rd.GetOrdinal("IDConcepto"));
                    oBE.IDBanco = rd.GetInt32(rd.GetOrdinal("IDBanco"));
                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));
                    oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.SerieDocumento = rd.GetString(rd.GetOrdinal("SerieDocumento"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.FechaAbono = rd.GetDateTime(rd.GetOrdinal("FechaAbono"));
                    oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento"));
                    oBE.CuentaBancaria = rd.GetString(rd.GetOrdinal("CuentaBancaria"));
                    oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
                    oBE.Estado = rd.GetInt32(rd.GetOrdinal("Estado"));
                    oBE.FormaPago = rd.GetInt32(rd.GetOrdinal("FormaPago"));
                    oBE.NombreAlmacen = rd.GetString(rd.GetOrdinal("NombreAlmacen"));
                    oBE.NombreProveedor = rd.GetString(rd.GetOrdinal("NombreProveedor"));
                    oBE.NombreSucursal = rd.GetString(rd.GetOrdinal("NombreSucursal"));
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


      

        public BECuentaPorPagar CuentasPorPagarSeleccionar(Int32 pIDCuentaxPagar)
        {
            SqlCommand cmd = ConexionCmd("gen.CuentasPorPagarSeleccionar");
            BECuentaPorPagar oBE = new BECuentaPorPagar();
            cmd.Parameters.Add("@IDCuentaPagar", SqlDbType.Int).Value = pIDCuentaxPagar;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDCuentaPagar = rd.GetInt32(rd.GetOrdinal("IDCuentaPagar")); 
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
                    oBE.IDConcepto = rd.GetInt32(rd.GetOrdinal("IDConcepto"));
                    oBE.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacen")); 
                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));             
                    oBE.FormaPago = rd.GetInt32(rd.GetOrdinal("FormaPago")); 
                    oBE.FechaAbono = rd.GetDateTime(rd.GetOrdinal("FechaAbono"));
                    oBE.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("FechaVencimiento")); 
                    oBE.Moneda = rd.GetString(rd.GetOrdinal("Moneda")); 
                    oBE.SerieDocumento = rd.GetString(rd.GetOrdinal("SerieDocumento"));
                    oBE.NumeroDocumento = rd.GetString(rd.GetOrdinal("NumeroDocumento"));
                    oBE.CuentaBancaria = rd.GetString(rd.GetOrdinal("CuentaBancaria"));
                    oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
                     
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




        //farmacia
        public IList PagoListar(Int32 pIDSucursal,Int32 pIDProveedor, String pFechaInicio, String pFechaFin)
        {
            SqlCommand cmd = ConexionCmd("gen.PagoListarxSucursal");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
            cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = pIDProveedor; 
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
              
            BEPago oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEPago();
                    oBE.IDPago = rd.GetInt32(rd.GetOrdinal("IDPago"));
                    oBE.IDCompra = rd.GetInt32(rd.GetOrdinal("IDCompra"));
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
                    oBE.IDConceptoPago = rd.GetInt32(rd.GetOrdinal("IDConceptoPago"));
                    oBE.IDMedioPago = rd.GetInt32(rd.GetOrdinal("IDMedioPago"));
                    oBE.IDBanco = rd.GetInt32(rd.GetOrdinal("IDBanco"));
                    oBE.AnioPago = rd.GetInt32(rd.GetOrdinal("AnioPago"));
                    oBE.NumeroPago = rd.GetInt32(rd.GetOrdinal("NumeroPago"));
                    oBE.CuentaCorriente = rd.GetString(rd.GetOrdinal("CuentaCorriente"));
                    oBE.NumeroOperacion = rd.GetString(rd.GetOrdinal("NumeroOperacion"));
                    oBE.IDMonedaPago = rd.GetString(rd.GetOrdinal("IDMonedaPago"));
                    oBE.FechaPago = rd.GetDateTime(rd.GetOrdinal("FechaPago"));
                    oBE.ImportePagado = rd.GetDecimal(rd.GetOrdinal("ImportePagado"));
                    oBE.Glosa = rd.GetString(rd.GetOrdinal("Glosa"));
                    oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
                    //oBE.Concepto = rd.GetString(rd.GetOrdinal("Concepto"));
                    oBE.Proveedor = rd.GetString(rd.GetOrdinal("Proveedor"));
                    oBE.Simbolo = rd.GetString(rd.GetOrdinal("Simbolo"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.NumeroPagoFormato = rd.GetString(rd.GetOrdinal("NumeroPagoFormato"));
                    oBE.ProveedorNumeroDocumento = rd.GetString(rd.GetOrdinal("ProveedorNumeroDocumento"));

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

        public IList ReportePagoListar(Int32 pIDProveedor, Int32 pIDSucursal, String pFechaInicio, String pFechaFin)
        {
            SqlCommand cmd = ConexionCmd("gen.ReportePagoProveedores");
            cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = pIDProveedor;
            //cmd.Parameters.Add("@IDConcepto", SqlDbType.Int).Value = pIDConcepto;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin; 
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
           

            BEPago oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEPago();
                    oBE.IDPago = rd.GetInt32(rd.GetOrdinal("IDPago"));
                    oBE.IDCompra = rd.GetInt32(rd.GetOrdinal("IDCompra"));
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
                    oBE.IDConceptoPago = rd.GetInt32(rd.GetOrdinal("IDConceptoPago"));
                    oBE.IDMedioPago = rd.GetInt32(rd.GetOrdinal("IDMedioPago"));
                    oBE.IDBanco = rd.GetInt32(rd.GetOrdinal("IDBanco"));
                    oBE.AnioPago = rd.GetInt32(rd.GetOrdinal("AnioPago"));
                    oBE.NumeroPago = rd.GetInt32(rd.GetOrdinal("NumeroPago"));
                    oBE.CuentaCorriente = rd.GetString(rd.GetOrdinal("CuentaCorriente"));
                    oBE.NumeroOperacion = rd.GetString(rd.GetOrdinal("NumeroOperacion"));
                    oBE.IDMonedaPago = rd.GetString(rd.GetOrdinal("IDMonedaPago"));
                    oBE.FechaPago = rd.GetDateTime(rd.GetOrdinal("FechaPago"));
                    oBE.ImportePagado = rd.GetDecimal(rd.GetOrdinal("ImportePagado"));
                    oBE.Glosa = rd.GetString(rd.GetOrdinal("Glosa"));
                    oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.Concepto = rd.GetString(rd.GetOrdinal("Concepto"));
                    oBE.Proveedor = rd.GetString(rd.GetOrdinal("Proveedor"));
                    oBE.Simbolo = rd.GetString(rd.GetOrdinal("Simbolo"));
                    oBE.SerieNumero = rd.GetString(rd.GetOrdinal("SerieNumero"));
                    oBE.NumeroPagoFormato = rd.GetString(rd.GetOrdinal("NumeroPagoFormato"));
                    oBE.ProveedorNumeroDocumento = rd.GetString(rd.GetOrdinal("ProveedorNumeroDocumento"));

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

        public BEPago PagoSeleccionar(Int32 pIDPago)
        {
            SqlCommand cmd = ConexionCmd("gen.PagoSeleccionar");
            BEPago oBE = new BEPago();
            cmd.Parameters.Add("@IDPago", SqlDbType.Int).Value = pIDPago;
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    oBE.IDPago = rd.GetInt32(rd.GetOrdinal("IDPago"));
                    oBE.IDCompra = rd.GetInt32(rd.GetOrdinal("IDCompra"));
                    oBE.IDProveedor = rd.GetInt32(rd.GetOrdinal("IDProveedor"));
                    oBE.IDConceptoPago = rd.GetInt32(rd.GetOrdinal("IDConceptoPago"));
                    oBE.IDMedioPago = rd.GetInt32(rd.GetOrdinal("IDMedioPago"));
                    oBE.IDBanco = rd.GetInt32(rd.GetOrdinal("IDBanco"));
                    oBE.AnioPago = rd.GetInt32(rd.GetOrdinal("AnioPago"));
                    oBE.NumeroPago = rd.GetInt32(rd.GetOrdinal("NumeroPago"));
                    oBE.CuentaCorriente = rd.GetString(rd.GetOrdinal("CuentaCorriente"));
                    oBE.NumeroOperacion = rd.GetString(rd.GetOrdinal("NumeroOperacion"));
                    oBE.IDMonedaPago = rd.GetString(rd.GetOrdinal("IDMonedaPago"));
                    oBE.FechaPago = rd.GetDateTime(rd.GetOrdinal("FechaPago"));
                    oBE.ImportePagado = rd.GetDecimal(rd.GetOrdinal("ImportePagado"));
                    oBE.Glosa = rd.GetString(rd.GetOrdinal("Glosa"));
                    oBE.IDEstado = rd.GetInt32(rd.GetOrdinal("IDEstado"));
                    oBE.NumeroPagoFormato = rd.GetString(rd.GetOrdinal("NumeroPagoFormato"));

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


        #region Transaccional

        public BERetornoTran CuentaPorPagarGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            BECuentaPorPagar oBE = (BECuentaPorPagar)pEntidad;
            SqlCommand cmd = ConexionCmd("gen.CuentasPorPagarGuardar");
            cmd.Parameters.Add("@IDCuentaPagar", SqlDbType.Int).Value = oBE.IDCuentaPagar;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = oBE.IDSucursal;
            cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = oBE.IDProveedor;
            cmd.Parameters.Add("@IDMoneda", SqlDbType.Int).Value = oBE.IDMoneda;
            cmd.Parameters.Add("@IDConcepto", SqlDbType.Int).Value = oBE.IDConcepto;

            cmd.Parameters.Add("@IDFormaPago", SqlDbType.Int).Value = oBE.IDFormaPago;
            cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = oBE.IDAlmacen;
            cmd.Parameters.Add("@IDBanco", SqlDbType.Int).Value = oBE.IDBanco;
            cmd.Parameters.Add("@IDEstado", SqlDbType.Int).Value = oBE.IDEstado;

            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
            cmd.Parameters.Add("@FechaAbono", SqlDbType.DateTime).Value = oBE.FechaAbono;
            cmd.Parameters.Add("@FechaVencimiento", SqlDbType.DateTime).Value = oBE.FechaVencimiento;
            cmd.Parameters.Add("@SerieNumero", SqlDbType.VarChar, 4).Value = oBE.SerieNumero;
            cmd.Parameters.Add("@NumeroDocumento", SqlDbType.VarChar, 7).Value = oBE.NumeroDocumento;
            cmd.Parameters.Add("@CuentaCorriente", SqlDbType.VarChar, 200).Value = oBE.CuentaCorriente;
            cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 200).Value = oBE.Observacion;
            cmd.Parameters.Add("@Importe", SqlDbType.Decimal).Value = oBE.Importe; 

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


        #endregion

        public BERetornoTran PagoGuardar(BEBase pEntidad)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            BEPago oBE = (BEPago)pEntidad;
            SqlCommand cmd = ConexionCmd("gen.PagoGuardar");
            cmd.Parameters.Add("@IDPago", SqlDbType.Int).Value = oBE.IDPago;
            cmd.Parameters.Add("@IDCompra", SqlDbType.Int).Value = oBE.IDCompra;
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = oBE.IDSucursal;
            cmd.Parameters.Add("@IDProveedor", SqlDbType.Int).Value = oBE.IDProveedor;
            cmd.Parameters.Add("@IDConceptoPago", SqlDbType.Int).Value = oBE.IDConceptoPago ==0 ? (Object)DBNull.Value : oBE.IDConceptoPago;
            cmd.Parameters.Add("@IDMedioPago", SqlDbType.Int).Value = oBE.IDMedioPago;

            if (oBE.IDBanco == 0) { cmd.Parameters.Add("@IDBanco", SqlDbType.Int).Value = DBNull.Value; }
            else { cmd.Parameters.Add("@IDBanco", SqlDbType.Int).Value = oBE.IDBanco; }

            cmd.Parameters.Add("@CuentaCorriente", SqlDbType.VarChar, 100).Value = oBE.CuentaCorriente;
            cmd.Parameters.Add("@NumeroOperacion", SqlDbType.VarChar, 100).Value = oBE.NumeroOperacion;
            cmd.Parameters.Add("@IDMonedaPago", SqlDbType.VarChar, 3).Value = oBE.IDMonedaPago;
            cmd.Parameters.Add("@FechaPago", SqlDbType.DateTime).Value = oBE.FechaPago;
            cmd.Parameters.Add("@ImportePagado", SqlDbType.Decimal).Value = oBE.ImportePagado;
            cmd.Parameters.Add("@Glosa", SqlDbType.VarChar, 1000).Value = oBE.Glosa;
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