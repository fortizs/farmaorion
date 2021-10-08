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
	public class BLIBVentas : BLBase
	{

		public List<BEIBVentas> IBTotalVentayCompraListar(BEIBVentas pBE)
		{
			SqlCommand cmd = ConexionCmd("gen.IBTotalVentayCompraListarFarmacia");
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pBE.IDEmpresa;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pBE.IDCliente;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pBE.FechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pBE.FechaFin;

			BEIBVentas oBE;
			List<BEIBVentas> lista = new List<BEIBVentas>();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEIBVentas();
					oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
					oBE.TotalCompra = rd.GetDecimal(rd.GetOrdinal("TotalCompra"));
					oBE.Utilidad = rd.GetDecimal(rd.GetOrdinal("Utilidad"));
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

		public List<BEIBVentas> ClientesTop10(BEIBVentas pBE)
		{
			SqlCommand cmd = ConexionCmd("gen.IBClientesTop10");
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pBE.IDEmpresa;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pBE.FechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pBE.FechaFin;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pBE.IDCliente;
			BEIBVentas oBE;
			List<BEIBVentas> lista = new List<BEIBVentas>();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEIBVentas();
					oBE.Cliente = rd.GetString(rd.GetOrdinal("Cliente"));
					oBE.CantidadServicio = rd.GetInt32(rd.GetOrdinal("CantidadServicio"));
					oBE.MontoServicio = rd.GetDecimal(rd.GetOrdinal("MontoServicio"));
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

		public List<BEIBVentas> ServiciosMasVendidos(BEIBVentas pBE)
		{
			SqlCommand cmd = ConexionCmd("gen.IBServiciosMasVendidos");
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pBE.IDEmpresa;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pBE.FechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pBE.FechaFin;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pBE.IDCliente;
			BEIBVentas oBE;
			List<BEIBVentas> lista = new List<BEIBVentas>();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEIBVentas();
					oBE.Servicio = rd.GetString(rd.GetOrdinal("Servicio"));
					oBE.CantidadServicio = rd.GetInt32(rd.GetOrdinal("CantidadServicio"));
					oBE.MontoServicio = rd.GetDecimal(rd.GetOrdinal("MontoServicio"));
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

        public List<BEIBVentas> ProductosMasVendidos(BEIBVentas pBE)
        {
            SqlCommand cmd = ConexionCmd("gen.IBProductosMasVendidosListar");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
            cmd.Parameters.Add("@IDCategoria", SqlDbType.Int).Value = pBE.IDCategoria;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar).Value = pBE.FechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pBE.FechaFin;
            BEIBVentas oBE;
            List<BEIBVentas> lista = new List<BEIBVentas>();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEIBVentas();
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
		 
        public List<BEIBVentas> IBVentaxTipoServicioListar(BEIBVentas pBE)
		{
			SqlCommand cmd = ConexionCmd("gen.IBVentaxTipoServicioListar");
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pBE.IDEmpresa;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pBE.IDCliente;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pBE.FechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pBE.FechaFin;

			BEIBVentas oBE;
			List<BEIBVentas> lista = new List<BEIBVentas>();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEIBVentas();
					oBE.TipoServicio = rd.GetString(rd.GetOrdinal("TipoServicio"));
					oBE.CantidadServicio = rd.GetInt32(rd.GetOrdinal("Cantidad"));
					oBE.MontoServicio = rd.GetDecimal(rd.GetOrdinal("Total"));
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

		public List<BEIBVentas> IBVentaxSucursal(BEIBVentas pBE)
		{
			SqlCommand cmd = ConexionCmd("gen.IBVentaxSucursalListar");
			cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pBE.IDEmpresa;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
			cmd.Parameters.Add("@IDCliente", SqlDbType.Int).Value = pBE.IDCliente;
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pBE.FechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pBE.FechaFin;

			BEIBVentas oBE;
			List<BEIBVentas> lista = new List<BEIBVentas>();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEIBVentas();
					oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
					oBE.MontoServicio = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
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

        public List<BEIBVentas> VendedoresTopPorSucursal(BEIBVentas pBE)
        {
            SqlCommand cmd = ConexionCmd("gen.ReporteVendedoresTopPorSucursal");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
            cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar, 10).Value = pBE.FechaInicio;
            cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar, 10).Value = pBE.FechaFin;

            BEIBVentas oBE;
            List<BEIBVentas> lista = new List<BEIBVentas>();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEIBVentas();
                    oBE.IDColaborador = rd.GetInt32(rd.GetOrdinal("IDColaborador"));
                    oBE.Colaborador = rd.GetString(rd.GetOrdinal("Colaborador"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.Foto = rd.GetString(rd.GetOrdinal("Foto"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));

                    if (oBE.Foto == "") {
                        oBE.Foto = "https://www.amcharts.com/wp-content/uploads/2019/04/monica.jpg";
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

        public List<BEIBVentas> IBReporteVentaMensualPorAnio(BEIBVentas pBE)
        {
            SqlCommand cmd = ConexionCmd("gen.IBReporteVentaMensualPorAnio");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
            cmd.Parameters.Add("@Anio", SqlDbType.Int).Value = pBE.Anio;            
            BEIBVentas oBE;
            List<BEIBVentas> lista = new List<BEIBVentas>();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEIBVentas();
                    oBE.IDMes = rd.GetInt32(rd.GetOrdinal("Mes"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("Total"));                    
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

        public List<BEIBVentas> IBReporteVentaDiariaPorMes(BEIBVentas pBE)
        {
            SqlCommand cmd = ConexionCmd("gen.IBReporteVentaDiariaPorMes");
            cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pBE.IDSucursal;
            cmd.Parameters.Add("@Anio", SqlDbType.Int).Value = pBE.Anio;
            cmd.Parameters.Add("@Mes", SqlDbType.Int).Value = pBE.IDMes;
            BEIBVentas oBE;
            List<BEIBVentas> lista = new List<BEIBVentas>();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEIBVentas();
                    oBE.IDDia = rd.GetInt32(rd.GetOrdinal("Fecha"));                    
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("Total"));
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

        public List<BEIBVentas> IBMetaAnualPorSucursal(BEIBVentas pBE)
        {
            SqlCommand cmd = ConexionCmd("gen.IBMetaAnualPorSucursal");
            cmd.Parameters.Add("@IDTipoMeta", SqlDbType.Int).Value = pBE.IDTipoMeta;            
            cmd.Parameters.Add("@Anio", SqlDbType.Int).Value = pBE.Anio;
            BEIBVentas oBE;
            List<BEIBVentas> lista = new List<BEIBVentas>();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEIBVentas();
                    oBE.IDSucursal = rd.GetInt32(rd.GetOrdinal("IDSucursal"));
                    oBE.Sucursal = rd.GetString(rd.GetOrdinal("Sucursal"));
                    oBE.Anio = rd.GetInt32(rd.GetOrdinal("Anio"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.MontoMeta = rd.GetDecimal(rd.GetOrdinal("MontoMeta"));
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

        public List<BEIBVentas> IBMetaAnualPorEmpresa(BEIBVentas pBE)
        {
            SqlCommand cmd = ConexionCmd("gen.IBMetaAnualPorEmpresa");
            cmd.Parameters.Add("@IDTipoMeta", SqlDbType.Int).Value = pBE.IDTipoMeta;
            cmd.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = pBE.IDEmpresa;
            cmd.Parameters.Add("@Anio", SqlDbType.Int).Value = pBE.Anio;
            BEIBVentas oBE;
            List<BEIBVentas> lista = new List<BEIBVentas>();
            try
            {
                cmd.Connection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    oBE = new BEIBVentas();                    
                    oBE.Anio = rd.GetInt32(rd.GetOrdinal("Anio"));
                    oBE.TotalVenta = rd.GetDecimal(rd.GetOrdinal("TotalVenta"));
                    oBE.MontoMeta = rd.GetDecimal(rd.GetOrdinal("MontoMeta"));
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
		 
		public IList IBFormaPagoxSucursalListar(String pFechaInicio, String pFechaFin, Int32 pIDSucursal)
		{
			SqlCommand cmd = ConexionCmd("gen.IBFormaPagoxSucursalListar");
			cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar).Value = pFechaInicio;
			cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin;
			cmd.Parameters.Add("@IDSucursal", SqlDbType.Int).Value = pIDSucursal;
			BEIBVentas oBE;
			ArrayList lista = new ArrayList();
			try
			{
				cmd.Connection.Open();
				SqlDataReader rd = cmd.ExecuteReader();
				while (rd.Read())
				{
					oBE = new BEIBVentas();
					oBE.IDFormaPago = rd.GetInt32(rd.GetOrdinal("IDFormaPago"));
					oBE.FormaPago = rd.GetString(rd.GetOrdinal("FormaPago")); 
					oBE.Total = rd.GetDecimal(rd.GetOrdinal("Total")); 

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


	}
}
