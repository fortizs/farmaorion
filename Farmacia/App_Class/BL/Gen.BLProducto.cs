using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Reportes;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Farmacia.App_Class.BL.General
{
	public class BLProducto : BLBase
    {  
        public IList ListarPorSucursal(int IdSucursal)
        {
            SqlCommand cmd = ConexionCmd("gen.ProductoListarPorSucursal");
            cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = IdSucursal;
            BEProducto oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEProducto();
                    oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
                    oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
                    oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
                    oBE.Stock = rd.GetDecimal(rd.GetOrdinal("Stock"));
                    oBE.PrecioCosto = rd.GetDecimal(rd.GetOrdinal("PrecioCosto"));
                    oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
                    oBE.ControlaStock = rd.GetBoolean(rd.GetOrdinal("ControlaStock"));
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
         
		public IList ProductoFiltroListar(String pFiltro, int IdSucursal)
		{
			SqlCommand cmd = ConexionCmd("gen.ProductoFiltroListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
			cmd.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = IdSucursal;
			BEProducto oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProducto();
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.CodigoBarra = rd.GetString(rd.GetOrdinal("CodigoBarra"));
					oBE.CodigoAlterna = rd.GetString(rd.GetOrdinal("CodigoAlterna"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
					oBE.PrincipioActivo = rd.GetString(rd.GetOrdinal("PrincipioActivo"));
					oBE.Localizacion = rd.GetString(rd.GetOrdinal("Localizacion"));
					oBE.StockMinimo = rd.GetDecimal(rd.GetOrdinal("StockMinimo"));
					oBE.Stock = rd.GetDecimal(rd.GetOrdinal("Stock"));
					oBE.PrecioCosto = rd.GetDecimal(rd.GetOrdinal("PrecioCosto"));
					oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.IDLinea = rd.GetInt32(rd.GetOrdinal("IDLinea"));
					oBE.IDMarca = rd.GetInt32(rd.GetOrdinal("IDMarca"));
					oBE.IDCategoria = rd.GetInt32(rd.GetOrdinal("IDCategoria"));
					oBE.UnidadMedidaCompra = rd.GetString(rd.GetOrdinal("UnidadMedidaCompra"));
					oBE.UnidadMedidaVenta = rd.GetString(rd.GetOrdinal("UnidadMedidaVenta"));
					oBE.Linea = rd.GetString(rd.GetOrdinal("Linea"));
					oBE.Marca = rd.GetString(rd.GetOrdinal("Marca"));
					oBE.Categoria = rd.GetString(rd.GetOrdinal("Categoria"));
					oBE.TipoProducto = rd.GetString(rd.GetOrdinal("TipoProducto"));
					oBE.AlertaStock = rd.GetBoolean(rd.GetOrdinal("AlertaStock"));
					oBE.IDUnidadMedidaVenta = rd.GetInt32(rd.GetOrdinal("IDUnidadMedidaVenta"));
					oBE.IDTipoAfectacionIgv = rd.GetInt32(rd.GetOrdinal("IDTipoAfectacionIgv")); 
					oBE.ControlaLote = rd.GetBoolean(rd.GetOrdinal("ControlaLote"));
					oBE.VentaConReceta = rd.GetBoolean(rd.GetOrdinal("VentaConReceta"));
					oBE.StockCompra = rd.GetDecimal(rd.GetOrdinal("Stock")); 
					oBE.ControlStock = rd.GetString(rd.GetOrdinal("ControlStock"));

					//oBE.Chasis = rd.GetString(rd.GetOrdinal("Chasis"));
					//oBE.NumeroLote = rd.GetString(rd.GetOrdinal("NumeroLote"));
					//oBE.Motor = rd.GetString(rd.GetOrdinal("Motor"));
					//oBE.IDTipoCombustible = rd.GetInt32(rd.GetOrdinal("IDTipoCombustible"));
					//oBE.IDColor = rd.GetInt32(rd.GetOrdinal("IDColor"));
					//oBE.NumeroDUA = rd.GetString(rd.GetOrdinal("NumeroDUA"));
					//oBE.FechaDUA = rd.GetDateTime(rd.GetOrdinal("FechaDUA"));
					//oBE.AnioModelo = rd.GetInt32(rd.GetOrdinal("AnioModelo"));
					//oBE.IDModeloVersion = rd.GetInt32(rd.GetOrdinal("IDModeloVersion"));

					//oBE.TipoCombustible = rd.GetString(rd.GetOrdinal("TipoCombustible"));
					//oBE.Color = rd.GetString(rd.GetOrdinal("Color"));
					//oBE.Modelo = rd.GetString(rd.GetOrdinal("Modelo"));
					//oBE.MarcaVehiculo = rd.GetString(rd.GetOrdinal("MarcaVehiculo"));
					//oBE.ModeloVersion = rd.GetString(rd.GetOrdinal("ModeloVersion"));
					 

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
          
        public IList StockProductoxSucursalListar(Int32 pIDProducto)
        {
            SqlCommand cmd = ConexionCmd("gen.StockProductoxSucursalListar");
            cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = pIDProducto;
            BEProducto oBE;
            ArrayList lista = new ArrayList();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEProducto();
                    oBE.IdStock = rd.GetInt32(rd.GetOrdinal("IdStock")); 
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal")); 
                    oBE.StockActual = rd.GetDecimal(rd.GetOrdinal("StockActual"));
                    oBE.StockMinimo = rd.GetDecimal(rd.GetOrdinal("StockMinimo"));
                      
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
		 
		public IList StockProductoxSucursalV2Listar(Int32 pIDSucursal, String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("gen.StockProductoxSucursalV2Listar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar,200).Value = pFiltro;
			BEProducto oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProducto();
					oBE.IdStock = rd.GetInt32(rd.GetOrdinal("IdStock"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IdProducto"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IdSucursal"));
					oBE.StockActual = rd.GetDecimal(rd.GetOrdinal("StockActual"));
					oBE.StockMinimo = rd.GetDecimal(rd.GetOrdinal("StockMinimo"));
					oBE.StockInicial = rd.GetDecimal(rd.GetOrdinal("StockInicial"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.PrecioCosto = rd.GetDecimal(rd.GetOrdinal("PrecioCosto"));
					oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
					oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
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

		public IList AlertaProductoxSucursalListar(Int32 pIDSucursal, String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("inv.AlertaProductoxSucursalListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
			BEProducto oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProducto();
					oBE.IdStock = rd.GetInt32(rd.GetOrdinal("IdStock"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IdProducto"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IdSucursal"));
					oBE.StockActual = rd.GetDecimal(rd.GetOrdinal("StockActual"));
					oBE.StockMinimo = rd.GetDecimal(rd.GetOrdinal("StockMinimo"));
					oBE.StockInicial = rd.GetDecimal(rd.GetOrdinal("StockInicial"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
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

		public Int32 CantidadAlertaProductoxSucursal(Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("inv.AlertaProductoxSucursalxCantidadListar");
			BEProducto oBE = new BEProducto();
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			Int32 CantidadFilas = 0;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					CantidadFilas = rd.GetInt32(rd.GetOrdinal("Cantidad"));
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
			return CantidadFilas;
		}

		public BEProducto ProductoSeleccionar(Int32 pIDProducto, Int32 pIDSucursal)
        {
            SqlCommand cmd = ConexionCmd("gen.ProductoSeleccionar");
            BEProducto oBE = new BEProducto();
            cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = pIDProducto;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDLinea = rd.GetInt32(rd.GetOrdinal("IDLinea"));
					oBE.IDMarca = rd.GetInt32(rd.GetOrdinal("IDMarca"));
					oBE.IDCategoria = rd.GetInt32(rd.GetOrdinal("IDCategoria"));
					oBE.IDTipoProducto = rd.GetInt32(rd.GetOrdinal("IDTipoProducto"));
					oBE.IDUnidadMedidaCompra = rd.GetInt32(rd.GetOrdinal("IDUnidadMedidaCompra"));
					oBE.IDUnidadMedidaVenta = rd.GetInt32(rd.GetOrdinal("IDUnidadMedidaVenta"));
					oBE.CodigoBarra = rd.GetString(rd.GetOrdinal("CodigoBarra"));
					oBE.CodigoAlterna = rd.GetString(rd.GetOrdinal("CodigoAlterna"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
					oBE.PrincipioActivo = rd.GetString(rd.GetOrdinal("PrincipioActivo"));
					oBE.NombreCorto = rd.GetString(rd.GetOrdinal("NombreCorto"));
					oBE.Localizacion = rd.GetString(rd.GetOrdinal("Localizacion"));
					oBE.Factor = rd.GetInt32(rd.GetOrdinal("Factor"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.ControlStock = rd.GetString(rd.GetOrdinal("ControlStock"));
					oBE.ControlaLote = rd.GetBoolean(rd.GetOrdinal("ControlaLote"));
					oBE.VentaConReceta = rd.GetBoolean(rd.GetOrdinal("VentaConReceta"));
					oBE.Peso = rd.GetDecimal(rd.GetOrdinal("Peso"));
					oBE.IDTipoAfectacionIgv = rd.GetInt32(rd.GetOrdinal("IDTipoAfectacionIgv"));
					oBE.IDTipoPrecio = rd.GetInt32(rd.GetOrdinal("IDTipoPrecio"));
					oBE.StockMinimo = rd.GetDecimal(rd.GetOrdinal("StockMinimo"));
					oBE.Stock = rd.GetDecimal(rd.GetOrdinal("Stock"));
					oBE.PrecioCosto = rd.GetDecimal(rd.GetOrdinal("PrecioCosto"));
					oBE.PrecioCostoTotalSinIgv = rd.GetDecimal(rd.GetOrdinal("PrecioCostoTotalSinIgv"));
					oBE.PrecioCostoUnidadSinIgv = rd.GetDecimal(rd.GetOrdinal("PrecioCostoUnidadSinIgv"));
					oBE.PrecioCostoUnidadConIgv = rd.GetDecimal(rd.GetOrdinal("PrecioCostoUnidadConIgv"));
					oBE.MargenUtilidad = rd.GetDecimal(rd.GetOrdinal("MargenUtilidad"));
					oBE.PrecioVentaSinIgv = rd.GetDecimal(rd.GetOrdinal("PrecioVentaSinIgv"));
					oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
					oBE.MayoreoUnidad = rd.GetInt32(rd.GetOrdinal("MayoreoUnidad"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Linea = rd.GetString(rd.GetOrdinal("Linea"));
					oBE.Marca = rd.GetString(rd.GetOrdinal("Marca"));
					oBE.Categoria = rd.GetString(rd.GetOrdinal("Categoria"));
					oBE.TipoProducto = rd.GetString(rd.GetOrdinal("TipoProducto"));
					oBE.UnidadMedidaCompra = rd.GetString(rd.GetOrdinal("UnidadMedidaCompra"));
					oBE.UnidadMedidaVenta = rd.GetString(rd.GetOrdinal("UnidadMedidaVenta"));

					//oBE.Chasis = rd.GetString(rd.GetOrdinal("Chasis"));
					//oBE.NumeroLote = rd.GetString(rd.GetOrdinal("NumeroLote"));
					//oBE.Motor = rd.GetString(rd.GetOrdinal("Motor"));
					//oBE.IDTipoCombustible = rd.GetInt32(rd.GetOrdinal("IDTipoCombustible"));
					//oBE.IDColor = rd.GetInt32(rd.GetOrdinal("IDColor"));
					//oBE.NumeroDUA = rd.GetString(rd.GetOrdinal("NumeroDUA"));
					//oBE.FechaDUA = rd.GetDateTime(rd.GetOrdinal("FechaDUA"));
					//oBE.AnioModelo = rd.GetInt32(rd.GetOrdinal("AnioModelo"));

					//oBE.IDMarcaVehiculo = rd.GetInt32(rd.GetOrdinal("IDMarcaVehiculo"));
					//oBE.IDModelo = rd.GetInt32(rd.GetOrdinal("IDModelo"));
					//oBE.IDModeloVersion = rd.GetInt32(rd.GetOrdinal("IDModeloVersion"));
					//oBE.IDCarroceria = rd.GetInt32(rd.GetOrdinal("IDCarroceria"));

					

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
          
        public BERetornoTran ProductoGuardar(BEProducto BEParam)
        {
            BERetornoTran BERetorno = new BERetornoTran(); 
			SqlCommand cmd = ConexionCmd("gen.ProductoGuardar");
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
			cmd.Parameters.Add("@IDLinea", SqlDbType.Int).Value = BEParam.IDLinea == 0 ? (Object) DBNull.Value : BEParam.IDLinea;
			cmd.Parameters.Add("@IDMarca", SqlDbType.Int).Value = BEParam.IDMarca == 0 ? (Object)DBNull.Value : BEParam.IDMarca;
			cmd.Parameters.Add("@IDCategoria", SqlDbType.Int).Value = BEParam.IDCategoria == 0 ? (Object)DBNull.Value : BEParam.IDCategoria;
			cmd.Parameters.Add("@IDTipoProducto", SqlDbType.Int).Value = BEParam.IDTipoProducto == 0 ? (Object)DBNull.Value : BEParam.IDTipoProducto;
			cmd.Parameters.Add("@IDUnidadMedidaCompra", SqlDbType.Int).Value = BEParam.IDUnidadMedidaCompra;
			cmd.Parameters.Add("@IDUnidadMedidaVenta", SqlDbType.Int).Value = BEParam.IDUnidadMedidaVenta;
			cmd.Parameters.Add("@CodigoBarra", SqlDbType.VarChar, 12).Value = BEParam.CodigoBarra;
			cmd.Parameters.Add("@CodigoAlterna", SqlDbType.VarChar, 12).Value = BEParam.CodigoAlterna;
			cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 250).Value = BEParam.Nombre; 
			cmd.Parameters.Add("@NombreCorto", SqlDbType.VarChar, 30).Value = BEParam.NombreCorto;
			cmd.Parameters.Add("@Localizacion", SqlDbType.VarChar, 100).Value = BEParam.Localizacion;
			cmd.Parameters.Add("@Factor", SqlDbType.Int).Value = BEParam.Factor;
			cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = BEParam.Estado;
			cmd.Parameters.Add("@ControlStock", SqlDbType.VarChar, 3).Value = BEParam.ControlStock; 
			cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = BEParam.Peso; 
			cmd.Parameters.Add("@StockMinimo", SqlDbType.Int).Value = BEParam.StockMinimo;
			cmd.Parameters.Add("@PrecioCosto", SqlDbType.Decimal).Value = BEParam.PrecioCosto;
			cmd.Parameters.Add("@PrecioCostoTotalSinIgv", SqlDbType.Decimal).Value = BEParam.PrecioCostoTotalSinIgv;
			cmd.Parameters.Add("@PrecioCostoUnidadSinIgv", SqlDbType.Decimal).Value = BEParam.PrecioCostoUnidadSinIgv;
			cmd.Parameters.Add("@PrecioCostoUnidadConIgv", SqlDbType.Decimal).Value = BEParam.PrecioCostoUnidadConIgv; 
			cmd.Parameters.Add("@MargenUtilidad", SqlDbType.Decimal).Value = BEParam.MargenUtilidad;
			cmd.Parameters.Add("@PrecioVenta", SqlDbType.Decimal).Value = BEParam.PrecioVenta;
			cmd.Parameters.Add("@MayoreoUnidad", SqlDbType.Int).Value = BEParam.MayoreoUnidad;

			//cmd.Parameters.Add("@Chasis", SqlDbType.VarChar, 50).Value = BEParam.Chasis;
			//cmd.Parameters.Add("@NumeroLote", SqlDbType.VarChar, 50).Value = BEParam.NumeroLote; 
			//cmd.Parameters.Add("@Motor", SqlDbType.VarChar, 50).Value = BEParam.Motor;
			//cmd.Parameters.Add("@IDTipoCombustible", SqlDbType.Int).Value = BEParam.IDTipoCombustible;
			//cmd.Parameters.Add("@IDColor", SqlDbType.Int).Value = BEParam.IDColor;
			//cmd.Parameters.Add("@NumeroDUA", SqlDbType.VarChar, 100).Value = BEParam.NumeroDUA;
			//cmd.Parameters.Add("@FechaDUA", SqlDbType.DateTime).Value = BEParam.FechaDUA;
			//cmd.Parameters.Add("@AnioModelo", SqlDbType.Int).Value = BEParam.AnioModelo;
			//cmd.Parameters.Add("@IDModeloVersion", SqlDbType.Int).Value = BEParam.IDModeloVersion;
			//cmd.Parameters.Add("@IDCarroceria", SqlDbType.Int).Value = BEParam.IDCarroceria;

			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
			cmd.Parameters.Add("@IDProductoFinal", SqlDbType.Int).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDProductoFinal"].Value);
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
		 
		public BERetornoTran ProductoDatosAdicionalGuardar(BEProducto BEParam)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ProductoDatosAdicionalGuardar");
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = BEParam.IDProducto;
			cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 8000).Value = BEParam.Descripcion;
			cmd.Parameters.Add("@PrincipioActivo", SqlDbType.VarChar, 8000).Value = BEParam.PrincipioActivo;
			cmd.Parameters.Add("@ControlaLote", SqlDbType.Bit).Value = BEParam.ControlaLote;
			cmd.Parameters.Add("@VentaConReceta", SqlDbType.Bit).Value = BEParam.VentaConReceta;
			cmd.Parameters.Add("@IDTipoAfectacionIgv", SqlDbType.Int).Value = BEParam.IDTipoAfectacionIgv;
			cmd.Parameters.Add("@IDTipoPrecio", SqlDbType.Int).Value = BEParam.IDTipoPrecio;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = BEParam.IDSucursal;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = BEParam.IDUsuario;
			cmd.Parameters.Add("@ErrorMensaje", SqlDbType.VarChar, 5000).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("ReturnValue", SqlDbType.VarChar).Direction = ParameterDirection.ReturnValue;
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				BERetorno.Retorno = Convert.ToString(cmd.Parameters["ReturnValue"].Value);
				BERetorno.Retorno2 = Convert.ToString(cmd.Parameters["@IDProductoFinal"].Value);
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

		public IList ReporteProductosMasVendidosListar(Int32 pIDSucursal, Int32 pIDCategoria, String pFechaInicio, String pFechaFin)
		{
			SqlCommand cmd = ConexionCmd("gen.ReporteProductosMasVendidosListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@IDCategoria", SqlDbType.Int).Value = pIDCategoria;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin; 
			 
			BEVentaDetalle oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEVentaDetalle();
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.NombreProducto = rd.GetString(rd.GetOrdinal("NombreProducto"));
					oBE.NombreCategoria = rd.GetString(rd.GetOrdinal("NombreCategoria"));
					oBE.NombreUnidadMedida = rd.GetString(rd.GetOrdinal("NombreUnidadMedida"));
					oBE.Cantidad = rd.GetDecimal(rd.GetOrdinal("Cantidad")); 
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
		 
		public IList StockProductoxSucursalV2Listar(Int32 pIDSucursal, String pFiltro, String TipoConsulta)
		{
			SqlCommand cmd = ConexionCmd("gen.StockProductoxSucursalV2Listar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
			cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar, 1).Value = TipoConsulta;
			BEProducto oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProducto();
					oBE.IdStock = rd.GetInt32(rd.GetOrdinal("IdStock"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IdProducto"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IdSucursal"));
					oBE.StockActual = rd.GetDecimal(rd.GetOrdinal("StockActual"));
					oBE.StockMinimo = rd.GetDecimal(rd.GetOrdinal("StockMinimo"));
					oBE.StockInicial = rd.GetDecimal(rd.GetOrdinal("StockInicial"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("Codigo"));
					oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.PrecioCosto = rd.GetDecimal(rd.GetOrdinal("PrecioCosto"));
					oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
					oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
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

		//digemid
		public IList ReporteProductoDigemid(Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("gen.ReporteDigemisaListar");
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;  
			BEReporte oBE;
			ArrayList lista = new ArrayList();
			try
			{

				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEReporte(); 
					oBE.CodEstab = rd.GetString(rd.GetOrdinal("CodEstab"));
					oBE.CodProd = rd.GetString(rd.GetOrdinal("CodProd")); 
					oBE.Precio1 = rd.GetDecimal(rd.GetOrdinal("Precio1"));
					oBE.Precio2 = rd.GetDecimal(rd.GetOrdinal("Precio2"));
					oBE.Precio3 = rd.GetDecimal(rd.GetOrdinal("Precio3"));
					  
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
		 
		public IList ProductoPrecioListar(Int32 pIDProducto)
		{
			SqlCommand cmd = ConexionCmd("gen.ProductoPrecioListar"); 
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = pIDProducto;
			BEProducto oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProducto();
					oBE.IdProductoPrecio = rd.GetInt32(rd.GetOrdinal("IdProductoPrecio"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal")); 
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
		 
		public BERetornoTran ProductoEliminar(Int32 pIDProducto)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ProductoEliminar");
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = pIDProducto;  
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

		public IList AlertaProductoxVencerListar(Int32 pIDSucursal, String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("inv.ProductosVencidosListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 200).Value = pFiltro;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			BEProducto oBE;
			BELote oBLote;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProducto();
					oBLote = new BELote();
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.Codigo = rd.GetString(rd.GetOrdinal("CodigoBarra"));
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.Producto = rd.GetString(rd.GetOrdinal("Producto"));
					oBLote.IDLote = rd.GetInt32(rd.GetOrdinal("IDLote"));
					oBLote.Lote = rd.GetString(rd.GetOrdinal("Lote"));
					oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
					oBLote.CantidadLote = rd.GetDecimal(rd.GetOrdinal("Cantidad"));
					oBLote.FechaFabricacion = rd.GetDateTime(rd.GetOrdinal("Fabricacion"));
					oBLote.FechaVencimiento = rd.GetDateTime(rd.GetOrdinal("Vencimiento"));
					oBE.Lote = oBLote;
					if (oBLote.FechaVencimiento > DateTime.Today)
					{
						oBE.Color = "Orange";
					}
					else
					{
						oBE.Color = "Red";
					}

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

		public Int32 AlertaCantidadProductosVencidos(Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("inv.AlertaCantidadProductosVencidos");
			BEProducto oBE = new BEProducto();
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			Int32 CantidadFilas = 0;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					CantidadFilas = rd.GetInt32(rd.GetOrdinal("Cantidad"));
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
			return CantidadFilas;
		}
		 
		public IList ProductoTempListar(String pFiltro)
		{
			SqlCommand cmd = ConexionCmd("gen.ProductoTempListar");
			cmd.Parameters.Add("@Filtro", SqlDbType.VarChar).Value = pFiltro;
			BEProducto oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProducto();
					oBE.IDProductoTemp = rd.GetInt32(rd.GetOrdinal("IDProductoTemp"));
					oBE.Cod_Prod = rd.GetString(rd.GetOrdinal("Cod_Prod"));
					oBE.NombreCompleto = rd.GetString(rd.GetOrdinal("NombreCompleto"));
					oBE.Nom_Titular = rd.GetString(rd.GetOrdinal("Nom_Titular"));
					oBE.Presentac = rd.GetString(rd.GetOrdinal("Presentac")); 
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
		 
		public BERetornoTran ProductoTempGuardar(BEProducto oBE)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ProductoTempGuardar");
			cmd.Parameters.Add("@IDProductoTemp", SqlDbType.Int).Value = oBE.IDProductoTemp;
			cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = oBE.Codigo;
			cmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar).Value = oBE.NombreCompleto;
			cmd.Parameters.Add("@Laboratorio", SqlDbType.VarChar).Value = oBE.Laboratorio;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = oBE.IDUsuario;
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
		 
		public BEProducto EventoProductoValidar(Int32 pIDProducto, String pFechaEvento, Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("gen.EventoProductoValidar");
			BEProducto oBE = new BEProducto();
			cmd.Parameters.Add("@IDProducto", SqlDbType.Int).Value = pIDProducto;
			cmd.Parameters.Add("@FechaEvento", SqlDbType.DateTime).Value = pFechaEvento;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				if (rd.Read())
				{
					oBE.Reservado = rd.GetInt32(rd.GetOrdinal("Reservado")); 
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
		 
		public IList ProductoxCategoriaSucursalListar(Int32 pIDCategoria, Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("gen.ProductoxCategoriaSucursalListar");
			cmd.Parameters.Add("@IDCategoria", SqlDbType.Int).Value = pIDCategoria;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;

			BEProducto oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEProducto();
					oBE.IDProducto = rd.GetInt32(rd.GetOrdinal("IDProducto"));
					oBE.IDUnidadMedida = rd.GetInt32(rd.GetOrdinal("IDUnidadMedida"));
					oBE.NombreCorto = rd.GetString(rd.GetOrdinal("NombreCorto"));
					oBE.Nombre = rd.GetString(rd.GetOrdinal("Nombre"));
					oBE.Descripcion = rd.GetString(rd.GetOrdinal("Descripcion"));
					oBE.StockMinimo = rd.GetDecimal(rd.GetOrdinal("StockMinimo"));
					oBE.Stock = rd.GetDecimal(rd.GetOrdinal("Stock"));
					oBE.PrecioCosto = rd.GetDecimal(rd.GetOrdinal("PrecioCosto"));
					oBE.PrecioVentaSinIgv = rd.GetDecimal(rd.GetOrdinal("PrecioVentaSinIgv"));
					oBE.PrecioVenta = rd.GetDecimal(rd.GetOrdinal("PrecioVenta"));
					oBE.Estado = rd.GetBoolean(rd.GetOrdinal("Estado"));
					oBE.UnidadMedida = rd.GetString(rd.GetOrdinal("UnidadMedida"));
					oBE.ControlStock = rd.GetString(rd.GetOrdinal("ControlStock"));
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
		 
		public BERetornoTran ProductoPrecioActualizar(Int32 pIdProductoPrecio, Decimal pPrecioVenta, Int32 pIDUsuario)
		{
			BERetornoTran BERetorno = new BERetornoTran();
			SqlCommand cmd = ConexionCmd("gen.ProductoPrecioActualizar");
			cmd.Parameters.Add("@IdProductoPrecio", SqlDbType.Int).Value = pIdProductoPrecio;
			cmd.Parameters.Add("@PrecioVenta", SqlDbType.Decimal).Value = pPrecioVenta;
			cmd.Parameters.Add("@IDUsuario", SqlDbType.Int).Value = pIDUsuario; 
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