using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Farmacia
{
	/// <summary>
	/// Descripción breve de wsDashBoard
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
	[System.Web.Script.Services.ScriptService]
	public class WSDashBoard : System.Web.Services.WebService
	{ 
		[WebMethod]
		public List<BEIBVentas> ClientesTop10(string[] Filtros)
		{
			List<BEIBVentas> Lista = null;
			//List<BEIBVentas> ListaClientesTop = null;
			//List<BEIBVentas> ListaVentaServicios = null;
			BEIBVentas oBE = new BEIBVentas();
			try
			{
				oBE.IDEmpresa = Int32.Parse(Filtros[0]);
				oBE.IDSucursal = Int32.Parse(Filtros[1]);
				oBE.FechaInicio = Filtros[2].ToString();
				oBE.FechaFin = Filtros[3].ToString();
				oBE.IDCliente = Int32.Parse(Filtros[4]);
				Lista = new BLIBVentas().ClientesTop10(oBE);
				//ListaVentaServicios = new BLIBVentas().ServiciosMasVendidos(oBE);
				//Lista.Add(ListaClientesTop);
			}
			catch (Exception ex)
			{
				Lista = null;
			}

			return Lista;
		}

		[WebMethod]
		public List<BEIBVentas> VentaProductos(string[] Filtros)
		{
			List<BEIBVentas> Lista = null;
			BEIBVentas oBE = new BEIBVentas();
			try
			{
			 
				oBE.IDSucursal = Int32.Parse(Filtros[0]);
                oBE.IDCategoria = Int32.Parse(Filtros[1]);
                oBE.FechaInicio = Filtros[2].ToString();
				oBE.FechaFin = Filtros[3].ToString(); 
				Lista = new BLIBVentas().ProductosMasVendidos(oBE);
			}
			catch (Exception ex)
			{
				Lista = null;
			}
			return Lista;
		}

		[WebMethod]
		public List<BEIBVentas> VentaxTipoServicioListar(string[] Filtros)
		{
			List<BEIBVentas> Lista = null;
			BEIBVentas oBE = new BEIBVentas();
			try
			{
				oBE.IDEmpresa = Int32.Parse(Filtros[0]);
				oBE.IDSucursal = Int32.Parse(Filtros[1]);
				oBE.FechaInicio = Filtros[2].ToString();
				oBE.FechaFin = Filtros[3].ToString();
				oBE.IDCliente = Int32.Parse(Filtros[4]);
				Lista = new BLIBVentas().IBVentaxTipoServicioListar(oBE);
			}
			catch (Exception ex)
			{
				Lista = null;
			}
			return Lista;
		}

		[WebMethod]
		public List<BEIBVentas> VentaxSucursal(string[] Filtros)
		{
			List<BEIBVentas> Lista = null;
			BEIBVentas oBE = new BEIBVentas();
			try
			{
				oBE.IDEmpresa = Int32.Parse(Filtros[0]);
				oBE.IDSucursal = Int32.Parse(Filtros[1]);
				oBE.FechaInicio = Filtros[2].ToString();
				oBE.FechaFin = Filtros[3].ToString();
				oBE.IDCliente = Int32.Parse(Filtros[4]);
				Lista = new BLIBVentas().IBVentaxSucursal(oBE);
			}
			catch (Exception ex)
			{
				Lista = null;
			}
			return Lista;
		}

        [WebMethod]
        public List<BEIBVentas> graficoVendedoresTop(string[] Filtros)
        {
            List<BEIBVentas> Lista = null;
            BEIBVentas oBE = new BEIBVentas();
            try
            {
                oBE.IDEmpresa = Int32.Parse(Filtros[0]);
                oBE.IDSucursal = Int32.Parse(Filtros[1]);
                oBE.FechaInicio = Filtros[2].ToString();
                oBE.FechaFin = Filtros[3].ToString();                
                Lista = new BLIBVentas().VendedoresTopPorSucursal(oBE);
            }
            catch (Exception ex)
            {
                Lista = null;
            }
            return Lista;
        }

        [WebMethod]
		public List<BEIBVentas> IBTotalVentayCompraListar(string[] Filtros)
		{
			List<BEIBVentas> Lista = null;
			BEIBVentas oBE = new BEIBVentas();
			try
			{
				oBE.IDEmpresa = Int32.Parse(Filtros[0]);
				oBE.IDSucursal = Int32.Parse(Filtros[1]);
				oBE.FechaInicio = Filtros[2].ToString();
				oBE.FechaFin = Filtros[3].ToString();
				oBE.IDCliente = Int32.Parse(Filtros[4]);
				Lista = new BLIBVentas().IBTotalVentayCompraListar(oBE);
			}
			catch (Exception ex)
			{
				Lista = null;
			}
			return Lista;
		}

		[WebMethod]
		public List<BEIBVentas> ClientesTop100(BEIBVentas pBE)
		{
			BLIBVentas oBL = new BLIBVentas();
			return oBL.ClientesTop10(pBE);
		}

        [WebMethod]
        public List<BEIBVentas> IBReporteVentaMensualPorAnio(string[] Filtros)
        {            
            
            List<BEIBVentas> Lista = null;
            BEIBVentas oBE = new BEIBVentas();
            try
            {
                oBE.IDSucursal = Int32.Parse(Filtros[1]);
                //oBE.Anio = DateTime.Now.Year;
                oBE.Anio = Int32.Parse(Filtros[6]);
                Lista = new BLIBVentas().IBReporteVentaMensualPorAnio(oBE);
            }
            catch (Exception ex)
            {
                Lista = null;
            }
            return Lista;
        }

        [WebMethod]
        public List<BEIBVentas> IBReporteVentaDiariaPorMes(string[] Filtros)
        {

            List<BEIBVentas> Lista = null;
            BEIBVentas oBE = new BEIBVentas();
            try
            {
                oBE.IDSucursal = Int32.Parse(Filtros[1]);                
                oBE.Anio = Int32.Parse(Filtros[7]);
                oBE.IDMes = Int32.Parse(Filtros[8]);
                Lista = new BLIBVentas().IBReporteVentaDiariaPorMes(oBE);
            }
            catch (Exception ex)
            {
                Lista = null;
            }
            return Lista;
        }

        [WebMethod]
        public List<BEIBVentas> IBMetaAnualPorSucursal(string[] Filtros)
        {

            List<BEIBVentas> Lista = null;
            BEIBVentas oBE = new BEIBVentas();
            try
            {
                oBE.IDTipoMeta = Int32.Parse(Filtros[9]);                
                oBE.Anio = DateTime.Now.Year;
                Lista = new BLIBVentas().IBMetaAnualPorSucursal(oBE);
            }
            catch (Exception ex)
            {
                Lista = null;
            }
            return Lista;
        }

        [WebMethod]
        public List<BEIBVentas> IBMetaAnualPorEmpresa(string[] Filtros)
        {

            List<BEIBVentas> Lista = null;
            BEIBVentas oBE = new BEIBVentas();
            try
            {
                oBE.IDTipoMeta = 3; //Meta Anual por Empresa 
                oBE.IDEmpresa = Int32.Parse(Filtros[0]);
                oBE.Anio = DateTime.Now.Year;
                Lista = new BLIBVentas().IBMetaAnualPorEmpresa(oBE);
            }
            catch (Exception ex)
            {
                Lista = null;
            }
            return Lista;
        }

    }
}
