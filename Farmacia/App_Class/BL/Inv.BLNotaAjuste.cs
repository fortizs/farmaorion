using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BL.Inventario
{
    public class BLNotaAjuste : BLBase
    {

        #region No Transaccional

        public IList Listar()
        {
            SqlCommand cmd = ConexionCmd("inv.NotaAjusteListar");
            BEMovimiento oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    BEAlmacen oBEAlmacen = new BEAlmacen();
                    oBE = new BEMovimiento();

                    oBEAlmacen.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacenOrigen"));
                    oBEAlmacen.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacenOrigen"));
                    oBEAlmacen.Nombre = rd.GetString(rd.GetOrdinal("Almacen"));
                    oBE.Almacen = oBEAlmacen;
                    //oBE. = "";
                    //oBE. ID Cargo = rd.GetInt32(rd.GetOrdinal("IDCargo"));
                    //oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    //oBE.
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

        //public IList NotaAjusteListar(Int32 IDSucursal, Int32 IDAlmacen, String FechaInicio, String FechaFin)
        public IList NotaAjusteListar(Int32 IDSucursal, Int32 IDAlmacen, String FechaInicio, String FechaFin)
        {
            SqlCommand cmd = ConexionCmd("inv.NotaAjusteListar");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = IDSucursal;
            cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int).Value = IDAlmacen;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = FechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = FechaFin;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    BEMovimiento oBE = new BEMovimiento();
                    BEAlmacen oBEAlmacen = new BEAlmacen();
                    BETransaccion oBETransaccion = new BETransaccion();

                    oBE.IDMovimiento = rd.GetInt32(rd.GetOrdinal("IDMovimiento"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.IDAlmacenOrigen = rd.GetInt32(rd.GetOrdinal("IDAlmacenOrigen"));
                    oBE.IDAlmacenDestino = rd.GetInt32(rd.GetOrdinal("IDAlmacenDestino"));
                    oBEAlmacen.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacenOrigen"));
                    oBEAlmacen.Nombre = rd.GetString(rd.GetOrdinal("Almacen"));
                    oBE.IDEntidad = rd.GetInt32(rd.GetOrdinal("IDEntidad"));
                    oBE.Entidad = rd.GetString(rd.GetOrdinal("Entidad"));
                    oBE.IDTransaccion = rd.GetInt32(rd.GetOrdinal("IDTransaccion"));
                    oBETransaccion.IDTransaccion = rd.GetInt32(rd.GetOrdinal("IDTransaccion"));
                    oBETransaccion.Nombre = rd.GetString(rd.GetOrdinal("Transaccion"));
                    oBETransaccion.TipoMovimiento = rd.GetString(rd.GetOrdinal("TipoMovimiento"));                    
                    oBE.Fecha = rd.GetDateTime(rd.GetOrdinal("FechaMovimiento"));
                    oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));                    
                    oBE.Almacen = oBEAlmacen;
                    oBE.Transaccion = oBETransaccion;
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

        public BEMovimiento NotaAjusteSeleccionar(Int32 IDMovimiento)
        {
            SqlCommand cmd = ConexionCmd("inv.NotaAjusteSeleccionar");
            cmd.Parameters.Add("@IDMovimiento", SqlDbType.Int).Value = IDMovimiento;            
            BEMovimiento oBE = new BEMovimiento();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                BEAlmacen oBEAlmacen = new BEAlmacen();
                BETransaccion oBETransaccion = new BETransaccion();
                if (rd.Read())
                {                                       
                    oBE.IDMovimiento = rd.GetInt32(rd.GetOrdinal("IDMovimiento"));
                    oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
                    oBE.IDAlmacenOrigen = rd.GetInt32(rd.GetOrdinal("IDAlmacenOrigen"));
                    oBE.IDAlmacenDestino = rd.GetInt32(rd.GetOrdinal("IDAlmacenDestino"));
                    oBEAlmacen.IDAlmacen = rd.GetInt32(rd.GetOrdinal("IDAlmacenOrigen"));
                    oBEAlmacen.Nombre = rd.GetString(rd.GetOrdinal("Almacen"));
                    oBE.IDEntidad = rd.GetInt32(rd.GetOrdinal("IDEntidad"));
                    oBE.Entidad = rd.GetString(rd.GetOrdinal("Entidad"));
                    oBETransaccion.IDTransaccion = rd.GetInt32(rd.GetOrdinal("IDTransaccion"));                    
                    oBETransaccion.Nombre = rd.GetString(rd.GetOrdinal("Transaccion"));
                    oBETransaccion.TipoMovimiento = rd.GetString(rd.GetOrdinal("TipoMovimiento"));
                    oBE.Fecha = rd.GetDateTime(rd.GetOrdinal("FechaMovimiento"));
                    oBE.Observacion = rd.GetString(rd.GetOrdinal("Observacion"));
                    oBE.IDEmpresa = rd.GetInt32(rd.GetOrdinal("IDEmpresa"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal")); 
					oBE.Almacen = oBEAlmacen;
                    oBE.Transaccion = oBETransaccion;
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

        public IList NotaAjusteDetalleListar(Int32 IDMovimiento) {

            SqlCommand cmd = ConexionCmd("inv.NotaAjusteDetalleListar");
            cmd.Parameters.Add("@IDMovimiento", SqlDbType.Int).Value = IDMovimiento;
            
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    BEMovimientoDetalle oBE = new BEMovimientoDetalle();
                    
                    oBE.IDMovimiento = rd.GetInt32(rd.GetOrdinal("IDMovimiento"));
                    oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
                    oBE.ProductoCodigo = rd.GetString(rd.GetOrdinal("CodigoProducto"));
                    oBE.NombreProducto = rd.GetString(rd.GetOrdinal("Producto"));
                    oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
                    oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
                    oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
                    oBE.PrecioUnitario = rd.GetDecimal(rd.GetOrdinal("ValorUnidad"));
                    oBE.SubTotal = rd.GetDecimal(rd.GetOrdinal("ValorUnidad"));                    
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


        //public IList KardexBuscar(Int32 pIDSucursal, String pIDProducto, String pFechaInicio, String pFechaFin)
        //{
        //	SqlCommand cmd = ConexionCmd("gen.KardexBuscar");
        //	cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pIDSucursal;
        //	cmd.Parameters.Add("@IDProducto", SqlDbType.VarChar, 10).Value = pIDProducto;
        //	cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pFechaInicio;
        //	cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pFechaFin;
        //	BEKardexDetalle oBE;
        //	ArrayList lista = new ArrayList();
        //	try
        //	{
        //		cmd.Connection.Open();
        //		SqlDataReader rd = cmd.ExecuteReader();
        //		while (rd.Read())
        //		{
        //			oBE = new BEKardexDetalle();
        //			oBE.IDKardexAlmacenDetalle = rd.GetInt32(rd.GetOrdinal("IDKardexAlmacenDetalle"));
        //			oBE.FechaMovimiento = rd.GetDateTime(rd.GetOrdinal("FechaMovimiento"));
        //			oBE.DocumentoReferencia = rd.GetString(rd.GetOrdinal("DocumentoReferencia"));
        //			oBE.EntradaSalida = rd.GetString(rd.GetOrdinal("EntradaSalida"));
        //			oBE.TipoDocumento = rd.GetString(rd.GetOrdinal("TipoDocumento"));
        //			oBE.ClienteProveedor = rd.GetString(rd.GetOrdinal("ClienteProveedor"));
        //			oBE.EntradaCantidad = rd.GetDecimal(rd.GetOrdinal("EntradaCantidad"));
        //			oBE.EntradaPrecioCosto = rd.GetDecimal(rd.GetOrdinal("EntradaPrecioCosto"));
        //			oBE.EntradaPrecioCostoTotal = rd.GetDecimal(rd.GetOrdinal("EntradaPrecioCostoTotal"));
        //			oBE.SalidaCantidad = rd.GetDecimal(rd.GetOrdinal("SalidaCantidad"));
        //			oBE.SalidaPrecioUnitario = rd.GetDecimal(rd.GetOrdinal("SalidaPrecioUnitario"));
        //			oBE.SalidaPrecioUnitarioTotal = rd.GetDecimal(rd.GetOrdinal("SalidaPrecioUnitarioTotal"));
        //			oBE.Saldo = rd.GetDecimal(rd.GetOrdinal("Saldo"));
        //			oBE.SaldoPrecioUnitario = rd.GetDecimal(rd.GetOrdinal("SaldoPrecioUnitario"));
        //			oBE.SaldoPrecioUnitarioTotal = rd.GetDecimal(rd.GetOrdinal("SaldoPrecioUnitarioTotal"));
        //			oBE.SucursalOrigen = rd.GetString(rd.GetOrdinal("SucursalOrigen"));
        //			oBE.IDSucursalDestino = rd.GetInt32(rd.GetOrdinal("IDSucursalDestino"));
        //			oBE.SucursalDestino = rd.GetString(rd.GetOrdinal("SucursalDestino"));
        //			oBE.TipoMovimiento = rd.GetString(rd.GetOrdinal("TipoMovimiento"));
        //			oBE.NombreProducto = rd.GetString(rd.GetOrdinal("NombreProducto"));
        //			lista.Add(oBE);
        //			oBE = null;



        //		}
        //		rd.Close();
        //	}
        //	catch (Exception ex)
        //	{
        //		throw ex;
        //	}
        //	finally
        //	{
        //		if ((cmd.Connection.State == ConnectionState.Open))
        //		{
        //			cmd.Connection.Close();
        //		}
        //	}
        //	return lista;
        //}


        //public BERetornoTran KardexAlmacenVenta(String pTipo, Int32 pTipoDocumento, Int32 pIDVenta, Int32 pIDSucursal, Int32 pIDUsuario, Int32 pIDEmpresa)
        //{
        //    BERetornoTran BERetorno = new BERetornoTran();
        //    SqlConnection objcn = new SqlConnection(CadenaConexion());
        //    objcn.Open();
        //    SqlTransaction sqlTran = objcn.BeginTransaction();

        //    //VENTA------------------------------------------------------------------------------------------------
        //    SqlCommand cmd = new SqlCommand("gen.KardexAlmacenGuardar", objcn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandTimeout = DuracionComando();
        //    cmd.Transaction = sqlTran;
        //    BERetorno.Retorno = "-1";
        //    try
        //    {
        //        cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar, 1).Value = pTipo;
        //        cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int, 10).Value = pIDEmpresa;
        //        cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int, 10).Value = pTipoDocumento;
        //        cmd.Parameters.Add("@IDAlmacen", SqlDbType.Int, 10).Value = 0;
        //        cmd.Parameters.Add("@IDSucursalOrigen", SqlDbType.Int, 10).Value = pIDSucursal;
        //        cmd.Parameters.Add("@IDSucursalDestino", SqlDbType.Int, 10).Value = pIDSucursal;
        //        cmd.Parameters.Add("@EntradaSalida", SqlDbType.VarChar).Value = "S";
        //        cmd.Parameters.Add("@TipoMovimiento", SqlDbType.VarChar).Value = "VTA";
        //        cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DateTime.Now;
        //        cmd.Parameters.Add("@IDVenta", SqlDbType.Int, 10).Value = pIDVenta;
        //        cmd.Parameters.Add("@IDSucursal", SqlDbType.Int, 10).Value = pIDSucursal;
        //        cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario;
        //        cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
        //        cmd.Parameters.Add("@IDKardexFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add("@IDAlmacenFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
        //        cmd.ExecuteNonQuery();
        //        BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
        //        BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
        //        if (BERetorno.Retorno != "1")
        //        {
        //            throw new Exception(BERetorno.ErrorMensaje);
        //        }
        //        cmd.Parameters.Clear();
        //        sqlTran.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        sqlTran.Rollback();
        //        if (BERetorno.Retorno == "-1")
        //            BERetorno.ErrorMensaje = ex.ToString();
        //    }
        //    finally
        //    {
        //        if (cmd.Connection.State == ConnectionState.Open)
        //        {
        //            cmd.Connection.Close();
        //        }
        //    }
        //    return BERetorno;
        //}

        public BERetornoTran MovimientoGuardar(BEMovimiento BEParam)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            SqlConnection objcn = new SqlConnection(CadenaConexion());
            objcn.Open();
            SqlTransaction sqlTran = objcn.BeginTransaction();

            SqlCommand cmd = new SqlCommand("inv.MovimientoGuardar", objcn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = DuracionComando();
            cmd.Transaction = sqlTran;
            BERetorno.Retorno = "-1";
            try
            {
                cmd.Parameters.Add("@IDMovimiento", SqlDbType.Int).Value = BEParam.IDMovimiento;
                cmd.Parameters.Add("@IDAlmacenOrigen", SqlDbType.Int).Value = BEParam.IDAlmacenOrigen;
                cmd.Parameters.Add("@IDAlmacenDestino", SqlDbType.Int).Value = BEParam.IDAlmacenDestino;
                cmd.Parameters.Add("@IDEntidad", SqlDbType.Int).Value = BEParam.IDEntidad;
                cmd.Parameters.Add("@Entidad", SqlDbType.VarChar, 50).Value = BEParam.Entidad;
                cmd.Parameters.Add("@IDTransaccion", SqlDbType.Int).Value = BEParam.IDTransaccion;
                cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 5000).Value = BEParam.Observacion;
                cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
                cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = BEParam.IDEmpresa;
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = BEParam.Fecha;
                cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
                cmd.Parameters.Add("@IDMovimientoFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
                BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDMovimientoFinal"].Value);
                BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
                if (BERetorno.Retorno != "1")
                {
                    throw new Exception(BERetorno.ErrorMensaje);
                }
                cmd.Parameters.Clear();
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


        public BERetornoTran MovimientoTransaccionGuardar(BEMovimiento oBE, ArrayList pMovimientoDetalle)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            BEGuiaRemision BEVentaRetorno = new BEGuiaRemision();
            int vSucursal = oBE.IDSucursal;
            SqlConnection objcn = new SqlConnection(CadenaConexion());
            objcn.Open();
            SqlTransaction sqlTran = objcn.BeginTransaction();

            //VENTA------------------------------------------------------------------------------------------------

            SqlCommand cmd = new SqlCommand("inv.MovimientoGuardar", objcn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = DuracionComando();
            cmd.Transaction = sqlTran;
            BERetorno.Retorno = "-1";
            try
            {

                cmd.Parameters.Add("@IDMovimiento", SqlDbType.Int, 10).Value = oBE.IDMovimiento;
                cmd.Parameters.Add("@IDAlmacenOrigen", SqlDbType.Int, 10).Value = oBE.IDAlmacenOrigen;
                cmd.Parameters.Add("@IDAlmacenDestino", SqlDbType.Int, 10).Value = oBE.IDAlmacenDestino;
                cmd.Parameters.Add("@IDEntidad", SqlDbType.Int, 10).Value = oBE.IDEntidad;
                cmd.Parameters.Add("@Entidad", SqlDbType.VarChar, 50).Value = oBE.Entidad;

                cmd.Parameters.Add("@IDTransaccion", SqlDbType.Int, 10).Value = oBE.IDTransaccion;
                cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 5000).Value = oBE.Observacion;
                cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = oBE.IDSucursal;
                cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = oBE.Fecha;
                cmd.Parameters.Add("@IDTipoComprobante", SqlDbType.Int, 10).Value = oBE.IDTipoComprobante;
                cmd.Parameters.Add("@IDTipoComprobanteReferencia", SqlDbType.Int, 10).Value = oBE.IDTipoComprobanteReferencia;
                cmd.Parameters.Add("@NumeroReferencia", SqlDbType.VarChar, 50).Value = oBE.NumeroReferencia;
                cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
                cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add("@IDMovimientoFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
                BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDMovimientoFinal"].Value);
                BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
                if (BERetorno.Retorno != "1")
                {
                    throw new Exception(BERetorno.ErrorMensaje);

                }
                else
                {
                    BERetorno.Retorno2 = cmd.Parameters["@IDMovimientoFinal"].Value.ToString();
                }

                //VENTA DETALLE-----------------------------------------------------------------------------               
                cmd.Parameters.Clear();
                BERetorno.Retorno = "-1";
                cmd.CommandText = "inv.MovimientoDetalleGuardar";

                BEMovimientoDetalle oBEMovDetalle = new BEMovimientoDetalle();
                for (Int32 i = 0; i < pMovimientoDetalle.Count; i++)
                {
                    oBEMovDetalle = (BEMovimientoDetalle)pMovimientoDetalle[i];
                    cmd.Parameters.Add("@IDMovimiento", SqlDbType.Int, 50).Value = Int32.Parse(BERetorno.Retorno2);
                    cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEMovDetalle.IDProducto;
                    cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int, 50).Value = oBEMovDetalle.IDUnidadMedida;
                    cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = oBEMovDetalle.Cantidad;
                    cmd.Parameters.Add("@FechaMovimiento", SqlDbType.DateTime).Value = oBE.Fecha;
                    cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEMovDetalle.IDUsuario;
                    cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
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

        #region Transaccional

        public BERetornoTran NotaAjusteGuardar(BEMovimiento oBE, ArrayList pMovimientoDetalle)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            BEGuiaRemision BEVentaRetorno = new BEGuiaRemision();
            int vSucursal = oBE.IDSucursal;
            SqlConnection objcn = new SqlConnection(CadenaConexion());
            objcn.Open();
            SqlTransaction sqlTran = objcn.BeginTransaction();
            Int32 IDMovimiento = 0;

            //VENTA------------------------------------------------------------------------------------------------

            SqlCommand cmd = new SqlCommand("inv.NotaAjusteGuardar", objcn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = DuracionComando();
            cmd.Transaction = sqlTran;
            BERetorno.Retorno = "-1";
            try
            {
                cmd.Parameters.Add("@IDMovimiento", SqlDbType.Int, 10).Value = oBE.IDMovimiento;
                cmd.Parameters.Add("@IDAlmacenOrigen", SqlDbType.Int, 10).Value = oBE.IDAlmacenOrigen;
                cmd.Parameters.Add("@IDAlmacenDestino", SqlDbType.Int, 10).Value = oBE.IDAlmacenDestino;
                cmd.Parameters.Add("@IDEntidad", SqlDbType.Int, 10).Value = oBE.IDEntidad;
                cmd.Parameters.Add("@Entidad", SqlDbType.VarChar, 50).Value = oBE.Entidad;
                cmd.Parameters.Add("@IDTransaccion", SqlDbType.Int, 10).Value = oBE.IDTransaccion;
                cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 5000).Value = oBE.Observacion;
                cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = oBE.Fecha;
                cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
                cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add("@IDMovimientoFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
                BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDMovimientoFinal"].Value);
                BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
                if (BERetorno.Retorno != "1")
                {
                    throw new Exception(BERetorno.ErrorMensaje);
                }

                //NOTA AJUSTE DETALLE-----------------------------------------------------------------------------               
                cmd.Parameters.Clear();
                BERetorno.Retorno = "-1";
                cmd.CommandText = "inv.NotaAjusteDetalleGuardar";

                BEMovimientoDetalle oBEMovDetalle = new BEMovimientoDetalle();
                for (Int32 i = 0; i < pMovimientoDetalle.Count; i++)
                {
                    oBEMovDetalle = (BEMovimientoDetalle)pMovimientoDetalle[i];
                    cmd.Parameters.Add("@IDMovimiento", SqlDbType.Int, 50).Value = Int32.Parse(BERetorno.Retorno2);
                    cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEMovDetalle.IDProducto;
                    cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int, 50).Value = oBEMovDetalle.IDUnidadMedida;
                    cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = oBEMovDetalle.Cantidad;
                    cmd.Parameters.Add("@varPrecioUnitario", SqlDbType.Decimal).Value = oBEMovDetalle.PrecioUnitario;
                    cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = oBE.Token;
                    cmd.Parameters.Add("@FechaMovimiento", SqlDbType.DateTime).Value = oBE.Fecha;
                    cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEMovDetalle.IDUsuario;
                    cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
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

        public BERetornoTran NotaAjusteActualizar(BEMovimiento oBE, ArrayList pMovimientoDetalle)
        {
            BERetornoTran BERetorno = new BERetornoTran();
            BEGuiaRemision BEVentaRetorno = new BEGuiaRemision();
            int vSucursal = oBE.IDSucursal;
            SqlConnection objcn = new SqlConnection(CadenaConexion());
            objcn.Open();
            SqlTransaction sqlTran = objcn.BeginTransaction();
            Int32 IDMovimiento = 0;

            //VENTA------------------------------------------------------------------------------------------------

            SqlCommand cmd = new SqlCommand("inv.NotaAjusteActualizar", objcn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = DuracionComando();
            cmd.Transaction = sqlTran;
            BERetorno.Retorno = "-1";
            try
            {
                cmd.Parameters.Add("@IDMovimiento", SqlDbType.Int, 10).Value = oBE.IDMovimiento;
                cmd.Parameters.Add("@IDAlmacenOrigen", SqlDbType.Int, 10).Value = oBE.IDAlmacenOrigen;
                cmd.Parameters.Add("@IDAlmacenDestino", SqlDbType.Int, 10).Value = oBE.IDAlmacenDestino;
                cmd.Parameters.Add("@IDEntidad", SqlDbType.Int, 10).Value = oBE.IDEntidad;
                cmd.Parameters.Add("@Entidad", SqlDbType.VarChar, 50).Value = oBE.Entidad;
                cmd.Parameters.Add("@IDTransaccion", SqlDbType.Int, 10).Value = oBE.IDTransaccion;
                cmd.Parameters.Add("@Observacion", SqlDbType.VarChar, 5000).Value = oBE.Observacion;
                cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = oBE.IDEmpresa;
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = oBE.Fecha;
                cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
                cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;                
                cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);                
                BERetorno.ErrorMensaje = Convert.ToString(cmd.Parameters["@ErrorMensaje"].Value);
                if (BERetorno.Retorno != "1")
                {
                    throw new Exception(BERetorno.ErrorMensaje);
                }               


                //NOTA AJUSTE DETALLE-----------------------------------------------------------------------------               
                cmd.Parameters.Clear();
                BERetorno.Retorno = "-1";
                cmd.CommandText = "inv.NotaAjusteDetalleGuardar";

                BEMovimientoDetalle oBEMovDetalle = new BEMovimientoDetalle();
                for (Int32 i = 0; i < pMovimientoDetalle.Count; i++)
                {
                    oBEMovDetalle = (BEMovimientoDetalle)pMovimientoDetalle[i];
                    cmd.Parameters.Add("@IDMovimiento", SqlDbType.Int, 50).Value = oBE.IDMovimiento;
                    cmd.Parameters.Add("@IDProducto", SqlDbType.Int, 50).Value = oBEMovDetalle.IDProducto;
                    cmd.Parameters.Add("@IDUnidadMedida", SqlDbType.Int, 50).Value = oBEMovDetalle.IDUnidadMedida;
                    cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = oBEMovDetalle.Cantidad;
                    cmd.Parameters.Add("@varPrecioUnitario", SqlDbType.Decimal).Value = oBEMovDetalle.PrecioUnitario;
                    cmd.Parameters.Add("@Token", SqlDbType.VarChar, 100).Value = oBE.Token;
                    cmd.Parameters.Add("@FechaMovimiento", SqlDbType.DateTime).Value = oBE.Fecha;
                    cmd.Parameters.Add("@IDUsuario", SqlDbType.Int, 50).Value = oBEMovDetalle.IDUsuario;
                    cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
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


        #endregion

    }
}