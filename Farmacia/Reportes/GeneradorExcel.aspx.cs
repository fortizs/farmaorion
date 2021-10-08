using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.Caja;
using Farmacia.App_Class.BE.Compras;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BE.Inventario;
using Farmacia.App_Class.BE.Reportes;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.Caja;
using Farmacia.App_Class.BL.Compras;
using Farmacia.App_Class.BL.General;
using Farmacia.App_Class.BL.Inventario;
using Muebleria.App_Class.BL.Caja;
using System;
using System.Collections;
using System.Text;
using System.Web;

namespace Farmacia.Reportes
{
	public partial class GeneradorExcel : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Int32 v_IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
			String v_TipoReporte = Request.QueryString["pTipoReporte"];
			Int32 v_IDSucursal = Int32.Parse(Request.QueryString["pIDSucursal"]);
			String v_FechaInicio = Request.QueryString["pFechaInicio"];
			String v_FechaFin = Request.QueryString["pFechaFin"];



			String v_TipoCaja = "";
			Int32 v_IDProveedor = 0;
			Int32 v_IDCliente = 0;
			Int32 v_IDCategoria = 0;
			Int32 v_IDMarca = 0;

			Int32 v_IDCaja = 0;
			String v_Periodo = Request.QueryString["pPeriodo"];
			Int32 v_IDMedioPago = 0;
			Decimal v_pCantidad = 0;
			String v_TipoMovimiento = "";
			String v_TipoMovimientoCaja = "";
			String v_FiltroProducto = "";

			Int32 v_IDColaborador = 0;
			Int32 v_IDEstadoVenta = 0;
			Int32 v_IDEstadoCobranza = 0;
			String v_Filtro = "";
            String v_TipoConsulta = "";
			Int32 v_IDRespPedido = 0;

			switch (v_TipoReporte)
			{
				case "VENTA_DETALLADA":
					v_IDColaborador = Int32.Parse(Request.QueryString["pIDColaborador"]);
					v_IDEstadoVenta = Int32.Parse(Request.QueryString["pIDEstadoVenta"]);
					v_IDEstadoCobranza = Int32.Parse(Request.QueryString["pIDEstadoCobranza"]);
					v_Filtro = Request.QueryString["pFiltro"];
					break;
				case "VENTA_RESUMIDA":
					v_IDColaborador = Int32.Parse(Request.QueryString["pIDColaborador"]);
					v_IDRespPedido = Int32.Parse(Request.QueryString["pIDRespPedido"]);
					v_IDEstadoVenta = Int32.Parse(Request.QueryString["pIDEstadoVenta"]);
					v_IDEstadoCobranza = Int32.Parse(Request.QueryString["pIDEstadoCobranza"]);
					v_Filtro = Request.QueryString["pFiltro"];
					break;
				case "MOV_CAJA":
					v_IDCaja = Int32.Parse(Request.QueryString["pIDCaja"]);
					v_TipoMovimientoCaja = Request.QueryString["pIDTipoMovimientoCaja"];
					break;
				case "PROD_MAS_VENDIDOS":
					v_IDCategoria = Int32.Parse(Request.QueryString["pIDCategoria"]);
					break;
				case "PRODUCTOS":
					v_IDCategoria = Int32.Parse(Request.QueryString["pIDCategoria"]);
					v_IDMarca = Int32.Parse(Request.QueryString["pIDMarca"]);
					v_FiltroProducto = Request.QueryString["pIDFiltroProducto"];
                    v_TipoConsulta = Request.QueryString["pTipoConsulta"];
                    break;
				case "COMPRAS":
					v_IDProveedor = Int32.Parse(Request.QueryString["pIDProveedor"]);
					break;
				case "COMPRAS_DET":
					v_IDProveedor = Int32.Parse(Request.QueryString["pIDProveedor"]);
					break;
				case "COBRANZA":
					v_IDCliente = Int32.Parse(Request.QueryString["pIDCliente"]);
					break;






			}


			if (v_TipoReporte == "DI")
			{
				v_TipoCaja = Request.QueryString["pTipoCaja"];
				v_FechaInicio = Request.QueryString["pFechaInicio"];
				v_FechaFin = Request.QueryString["pFechaFin"];
			}
			if (v_TipoReporte == "LIBRO_VENTAS")
			{
				v_Periodo = Request.QueryString["pPeriodo"];
			}
			 

			if (v_TipoReporte == "MOVIMIENTOALM")
			{

				v_TipoMovimiento = Request.QueryString["pTipoMovimiento"];

			}

			String NombreEmpresa = new BLEmpresa().EmpresaSeleccionar(v_IDEmpresa).RazonSocial;


			String nameReport;
			String nombre;

			String style = @"<style> .textmode { } </style>";

			HttpResponse response = Response;

			switch (v_TipoReporte)
			{
				case "VENTA_RESUMIDA":
					nameReport = "REPORTE_VENTAS_RESUMIDAS_" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteVentasListar(NombreEmpresa, v_IDSucursal, v_IDColaborador, v_IDRespPedido, v_FechaInicio, v_FechaFin, v_IDEstadoVenta, v_IDEstadoCobranza, v_Filtro));
					response.Flush();
					response.End();
					return;
				case "VENTA_DETALLADA":
					nameReport = "REPORTE_DETALLE_VENTAS_" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteVentasDetalleListar(NombreEmpresa, v_IDSucursal, v_IDColaborador, v_IDRespPedido, v_FechaInicio, v_FechaFin, v_IDEstadoVenta, v_IDEstadoCobranza, v_Filtro));
					response.Flush();
					response.End();
					return;
				case "MOV_CAJA":
					nameReport = "REPORTE_MOVIMIENTO_CAJA" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteMovimientoCaja(NombreEmpresa, v_IDSucursal, v_TipoMovimientoCaja, v_IDCaja, v_FechaInicio, v_FechaFin));
					response.Flush();
					response.End();
					return;
				case "RES_CAJA":
					nameReport = "REPORTE_RESUMEN_CAJA" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteResumenCaja(NombreEmpresa, v_IDSucursal, v_IDCaja, v_IDColaborador, v_FechaInicio, v_FechaFin));
					response.Flush();
					response.End();
					return;
				case "VEND_TOP":
					nameReport = "REPORTE_TOP_VENDEDORES" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteVendedoresTopPorSucursal(NombreEmpresa, v_IDSucursal, v_FechaInicio, v_FechaFin));
					response.Flush();
					response.End();
					return;
				case "TOP_CLIENTE":
					nameReport = "REPORTE_TOP_CLIENTES" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteClientesTop10Ventas(NombreEmpresa, v_IDSucursal, v_FechaInicio, v_FechaFin));
					response.Flush();
					response.End();
					return;
				case "DIGEMID":
					nameReport = "Hoja1";
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReportePrecioProductoDigemid(v_IDSucursal));
					response.Flush();
					response.End();
					return;
				case "PROD_MAS_VENDIDOS":
					nameReport = "REPORTE_PRODUCTO_MAS_VENDIDO" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteProductoMasVendidos(NombreEmpresa, v_IDSucursal, v_IDCategoria, v_FechaInicio, v_FechaFin, v_pCantidad));
					response.Flush();
					response.End();
					return;
				case "PRODUCTOS":
					nameReport = "REPORTE_STOCK_PRODUCTOS" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteStockProductos(NombreEmpresa, v_IDCategoria, v_IDMarca, v_IDSucursal, v_FiltroProducto, v_TipoConsulta));
					response.Flush();
					response.End();
					return;
				case "COMPRAS":
					nameReport = "REPORTE_COMPRAS_XPROVEEDOR_RESUMIDO_" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ComprasxProveedorResumido(NombreEmpresa, v_IDSucursal, v_IDProveedor, v_FechaInicio, v_FechaFin));
					response.Flush();
					response.End();
					return;
				case "COMPRAS_DET":
					nameReport = "REPORTE_COMPRAS_DETALLADO_" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ComprasxProveedorDetallado(NombreEmpresa, v_IDSucursal, v_IDProveedor, v_FechaInicio, v_FechaFin));
					response.Flush();
					response.End();
					return;
				case "COBRANZA":
					nameReport = "REPORTE_COBRANZA" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteCobranza(NombreEmpresa, v_IDSucursal, v_IDMedioPago, v_IDCliente, v_FechaInicio, v_FechaFin));
					response.Flush();
					response.End();
					return;
				case "PAGO_PROV":
					nameReport = "REPORTE_PAGO_PROVEEDOR" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(PagoProveedor(NombreEmpresa,v_IDSucursal, v_IDProveedor, v_FechaInicio, v_FechaFin));
					response.Flush();
					response.End();
					return;
				case "LIBRO_VENTAS":
					nameReport = "REPORTE_REGISTRO_VENTAS_" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ListarRegistroVentas(NombreEmpresa, v_IDSucursal, v_Periodo));
					response.Flush();
					response.End();
					return; 
				case "LIBRO_COMPRAS":
					nameReport = "REPORTE_REGISTRO_COMPRAS_" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ListarRegistroCompras(NombreEmpresa,v_IDSucursal, v_Periodo));
					response.Flush();
					response.End();
					return;








				case "DI":
					nameReport = "REPORTE_DETALLE_INGESOS" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteDetalleIngresos(v_IDSucursal, v_FechaInicio, v_FechaFin));
					response.End();
					return; 
				case "MOVIMIENTOALM":
					nameReport = "REPORTE_MOVIMIENTOS_ALMACEN" + DateTime.Now.ToString("yyyyMMddHHmmss");
					nombre = nameReport + ".xls";
					response.Clear();
					response.Buffer = true;
					response.ContentType = "application/vnd.ms-excel";
					response.Write(style);
					response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
					response.Charset = "UTF-8";
					response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
					response.Write(ReporteMovimientoListar(v_IDSucursal, v_TipoMovimiento, v_FechaInicio, v_FechaFin));
					response.Flush();
					response.End();
					return;
			}
		}

		private String ReporteDetalleIngresos(Int32 pIDSucursal, String pFechaInicio, String pFechaFin)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			Int32 RegDet;
			IList ListaVenta;
			IList ListaVentaDet;
			Decimal nTotalVenta, nTotalEfectivo, nTotalCheque, nTotalTransferencia, nTotalCredito, nTotalVisa, nTotalMasterCard, nTotalAExpress;
			BLReporte oBL = new BLReporte();
			BLVentaDetalle oBLVenta = new BLVentaDetalle();

			nTotalVenta = 0;
			nTotalEfectivo = 0;
			nTotalCredito = 0;
			nTotalTransferencia = 0;
			nTotalCheque = 0;
			nTotalVisa = 0;
			nTotalMasterCard = 0;
			nTotalAExpress = 0;

			ListaVenta = oBL.ReporteDetalleIngresos(pIDSucursal, pFechaInicio, pFechaFin);
			//TotalVendido = oBL.TotalVentasResumidasPorSucursal(pIDSucursal, pIDFormaPago, pIDColaborador, pFechaInicio, pFechaFin, pFiltro);
			String Sucursal = "-TODOS-";

			if (pIDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(pIDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='18'></td></tr>");
			sb.Append("<tr><td align='center' colspan='18'><b>REPORTE DE INGRESOS</b></td></tr>");
			sb.Append("<tr><td colspan='18'></td></tr>");
			sb.Append("<tr><td colspan='18'><b>SUCURSAL:</b> " + Sucursal + "</td></tr>");
			sb.Append("<tr><td><b>FECHA INICIO:</b> " + pFechaInicio + " </td><td></td><td><b>FECHA FIN:</b> " + pFechaFin + " </td><td colspan='16'></td></tr>");
			sb.Append("</table><br/><br/>");
			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#2196F3;'>" +
						"<td>Item</td>" +
						"<td>Sucursal</td>" +
						"<td>Comprobante</td>" +
						"<td>Documento</td>" +
						"<td>Fecha de Venta</td>" +
						"<td>Fecha Separacion</td>" +
						"<td>Ruc/Dni</td>" +
						"<td>Cliente</td>" +
						"<td>Tipo Operacion</td>" +
						"<td>Total Ingreso</td>" +
						"<td>Anulado</td>" +
						"<td>Efectivo</td>" +
						"<td>Credito</td>" +
						"<td>Transf</td>" +
						"<td>Cheque</td>" +
						"<td>Visa</td>" +
						"<td>MasterCard</td>" +
						"<td>A.Express</td></tr>");
			Reg = 0;
			foreach (BEReporte oBEVenta in ListaVenta)
			{

				Reg = Reg + 1;
				ListaVentaDet = oBLVenta.VentaDetalleListar(oBEVenta.Codigo);

				sb.Append("<tr><td align='center'>" + Reg + "</td>");
				sb.Append("<td>" + oBEVenta.Sucursal + "</td>");
				sb.Append("<td>" + oBEVenta.TipoComprobante + "</td>");
				sb.Append("<td>" + oBEVenta.SerieNumero + "</td>");
				sb.Append("<td>" + oBEVenta.FechaEmision + "</td>");
				sb.Append("<td>" + oBEVenta.FechaSeparacion + "</td>");
				sb.Append("<td>" + oBEVenta.NumeroDocumento + "</td>");
				sb.Append("<td>" + oBEVenta.RazonSocial + "</td>");
				sb.Append("<td>" + oBEVenta.TipoOperacion + "</td>");
				sb.Append("<td align='right'>" + oBEVenta.TotalVenta.ToString("N") + "</td>");
				sb.Append("<td align='center'>" + oBEVenta.Anulado + "</td>");
				sb.Append("<td align='right'>" + oBEVenta.TotalEfectivo.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEVenta.TotalCredito.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEVenta.TotalTransferencia.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEVenta.TotalCheque.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEVenta.TotalVisa.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEVenta.TotalMastercard.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEVenta.TotalAExpress.ToString("N") + "</td>");
				sb.Append("</tr>");

				nTotalVenta = nTotalVenta + oBEVenta.TotalVenta;
				nTotalEfectivo = nTotalEfectivo + oBEVenta.TotalEfectivo;
				nTotalCredito = nTotalCredito + oBEVenta.TotalCredito;
				nTotalTransferencia = nTotalTransferencia + oBEVenta.TotalTransferencia;
				nTotalCheque = nTotalCheque + oBEVenta.TotalCheque;
				nTotalVisa = nTotalVisa + oBEVenta.TotalVisa;
				nTotalMasterCard = nTotalMasterCard + oBEVenta.TotalMastercard;
				nTotalAExpress = nTotalAExpress + oBEVenta.TotalAExpress;

				RegDet = 0;
				sb.Append("<tr><td align='center' colspan='18'>");
				sb.Append("<table class='tabla_detalle' width='100%' border='0'>");
				sb.Append("<tr style='background-color:#f1f3f6;'>");
				sb.Append("<td></td>");
				sb.Append("<td>Item</td>");
				sb.Append("<td>Producto</td>");
				sb.Append("<td>Precio Venta</td>");
				sb.Append("<td>Cantidad</td>");
				sb.Append("<td>Total</td></tr>");
				foreach (BEVentaDetalle oBEVentaDet in ListaVentaDet)
				{
					RegDet = RegDet + 1;
					sb.Append("<tr>");
					sb.Append("<td align='center'></td><td align='center'>" + RegDet + "</td>");
					sb.Append("<td>" + oBEVentaDet.Codigo + " " + oBEVentaDet.Producto + " - " + oBEVentaDet.ProductoDetalle + "</td>");
					sb.Append("<td align='right'>" + oBEVentaDet.PrecioUnitarioConIgv.ToString("0.00") + "</td>");
					sb.Append("<td align='center'>" + Convert.ToInt32(oBEVentaDet.Cantidad) + "</td>");
					sb.Append("<td align='right'>" + oBEVentaDet.ImporteVenta.ToString("0.00") + "</td>");
					sb.Append("</tr>");
				}
				//sb.Append("<tr style='background-color:#f1f3f6;'>" +
				//            "<td colspan='6'></td>" +
				//            "</tr>");
				sb.Append("</table>");
				sb.Append("</td></tr>");
			}
			sb.Append("<tr style='background-color:#4CAF50;'>");
			sb.Append("<td colspan='9' align='right'>TOTALES</td>");
			sb.Append("<td align='right'>" + nTotalVenta.ToString("N") + "</td>");
			sb.Append("<td align='center'></td>");
			sb.Append("<td align='right'>" + nTotalEfectivo.ToString("N") + "</td>");
			sb.Append("<td align='right'>" + nTotalCredito.ToString("N") + "</td>");
			sb.Append("<td align='right'>" + nTotalTransferencia.ToString("N") + "</td>");
			sb.Append("<td align='right'>" + nTotalCheque.ToString("N") + "</td>");
			sb.Append("<td align='right'>" + nTotalVisa.ToString("N") + "</td>");
			sb.Append("<td align='right'>" + nTotalMasterCard.ToString("N") + "</td>");
			sb.Append("<td align='right'>" + nTotalAExpress.ToString("N") + "</td>");
			sb.Append("</tr>");

			sb.Append("</table>");
			return sb.ToString();
		}

		private String ReporteMovimientoListar(Int32 v_IDSucursal, String v_TipoMovimiento, String v_FechaInicio, String v_FechaFin)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BLMovimiento oBL = new BLMovimiento();
			ListaDet = oBL.ReporteMovimientoListar(v_IDSucursal, v_TipoMovimiento, v_FechaInicio, v_FechaFin);
			String Sucursal = "-TODOS-";

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}
			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='7'></td></tr>");
			sb.Append("<tr><td align='Left' colspan='23'><h2>REPORTE DE MOVIMIENTOS DE ALMACEN</h2></td></tr><br/><br/><br/>");
			sb.Append("<tr><td align='Left'><b>FECHA INICIO:</b><td align='Left' colspan='22'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>FECHA FIN:</b><td align='Left' colspan='22'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='22'>" + Sucursal + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>RUC:</b></td><td align='Left' colspan='22'>20600921763</td></tr>");
			sb.Append("<tr><td align='Left'><b>RAZON SOCIAL:</b></td><td align='Left' colspan='22'>FARMA ORION</td></tr>");
			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#FFBF00;font-weight:bold;'>");

			sb.Append("<td align='center' rowspan='1' width='5%'>N°</td>");
			sb.Append("<td rowspan='1' width='15%'># Movimiento</td>");
			sb.Append("<td rowspan='1' width='15%'>Entidad</td>");
			sb.Append("<td rowspan='1' width='15%'>Transaccion</td>");
			sb.Append("<td rowspan='1' width='10%'>Fecha Movimiento</td>");
			sb.Append("<td rowspan='1' width='10%'>Tipo Movimiento</td>");
			sb.Append("<td rowspan='1' width='10%'>AlmacenOrigen</td>");
			sb.Append("<td rowspan='1' width='10%'>AlmacenDestino</td>");
			sb.Append("<td rowspan='1' width='10%'>Producto</td>");
			sb.Append("<td rowspan='1' width='10%'>Cantidad</td>");
			sb.Append("<td rowspan='1' width='10%'>UnidadMedida</td>");
			sb.Append("<td rowspan='1' width='10%'>ValorUnidad</td>");
			sb.Append("<td rowspan='1' width='10%'>ValorTotal</td>");
			sb.Append("</tr>");
			Reg = 0;
			foreach (BEReporteMovimiento oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr><td align='center'>" + Reg + "</td>");  // correlativo 
				sb.Append("<td>" + oBEc.IDMovimiento + "</td>");
				sb.Append("<td>" + oBEc.Entidad + "</td>");
				sb.Append("<td>" + oBEc.Transaccion + "</td>");
				sb.Append("<td>" + oBEc.FechaMovimiento.ToString("dd/MM/yyyy") + "</td>");
				sb.Append("<td>" + oBEc.TipoMovimiento + "</td>");
				sb.Append("<td>" + oBEc.AlmacenOrigen + "</td>");
				sb.Append("<td>" + oBEc.AlmacenDestino + "</td>");
				sb.Append("<td>" + oBEc.Producto + "</td>");
				sb.Append("<td>" + oBEc.Cantidad.ToString("N") + "</td>");
				sb.Append("<td>" + oBEc.UnidadMedida + "</td>");
				sb.Append("<td>" + oBEc.ValorUnidad.ToString("N") + "</td>");
				sb.Append("<td>" + oBEc.ValorTotal.ToString("N") + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}

		private String ReporteVentasListar(String pNombreEmpresa, Int32 v_IDSucursal, Int32 v_IDColaborador, Int32 v_IDRespPedido, String v_FechaInicio, String v_FechaFin, Int32 v_IDEstadoVenta, Int32 v_IDEstadoCobranza, String v_Filtro)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BLReporte oBL = new BLReporte();
			ListaDet = oBL.VentasResumidasPorSucursal(v_IDSucursal, v_IDColaborador, v_IDRespPedido, v_FechaInicio, v_FechaFin, v_IDEstadoVenta, v_IDEstadoCobranza, v_Filtro);
			String Sucursal = "-TODOS-";
			String Colaborador = "-TODOS-";
			Decimal TotalVenta = 0;
			Decimal TotalUtilidad = 0;

			
			foreach (BEReporte oBEc in ListaDet)
			{
				TotalVenta += oBEc.TotalVenta;
				TotalUtilidad += oBEc.Utilidad;
			}

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			if (v_IDColaborador > 0)
			{
				BEColaborador oBECol = new BLColaborador().SeleccionarColaborador(v_IDColaborador);
				Colaborador = oBECol.Colaborador.ToUpper();
			}
			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='13'></td></tr>");
			sb.Append("<tr><td align='Left' colspan='13'><h2>REPORTE DE VENTAS RESUMIDAS</h2></td></tr><br/><br/><br/>");
			sb.Append("<tr><td colspan='13'></td></tr>");
			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left' colspan='2'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='2'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>COLABORADOR:</b></td><td align='Left' colspan='2'>" + Colaborador + "</td><td align='Left' style='background-color:#FFFE33;font-weight:bold;'><b>TOTAL VENTA:</b><td align='Left' style='background-color:#FFFE33;font-weight:bold;'>" + TotalVenta.ToString("N") + "</td><td align='Left' style='background-color:#FFFE33;font-weight:bold;'><b>Utilidad:</b><td align='Left' style='background-color:#FFFE33;font-weight:bold;'>" + TotalUtilidad.ToString("N") + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");
			sb.Append("<td align='center' rowspan='1' width='5%'>#</td>");
			sb.Append("<td rowspan='1' width='15%'>SUCURSAL</td>");
			sb.Append("<td rowspan='1' width='15%'>NRO.DOCUMENTO</td>");
			sb.Append("<td rowspan='1' width='15%'>CLIENTE</td>");
			sb.Append("<td rowspan='1' width='10%'>TIPO COMPROBANTE</td>");
			sb.Append("<td rowspan='1' width='10%'>SERIE-NÚMERO</td>");
			sb.Append("<td rowspan='1' width='10%'>FECHA VENTA</td>");
			sb.Append("<td rowspan='1' width='10%'>CAJERO</td>");
			sb.Append("<td rowspan='1' width='10%'>MONEDA</td>");
			sb.Append("<td rowspan='1' width='10%'>TOTAL VENTA</td>");
			sb.Append("<td rowspan='1' width='10%'>UTILIDAD</td>");
			sb.Append("<td rowspan='1' width='10%'>ESTADO VENTA</td>");
			sb.Append("<td rowspan='1' width='10%'>ESTADO COBRANZA</td>");
			sb.Append("<td rowspan='1' width='10%'>PEDIDO</td>");
			 
			sb.Append("</tr>");
			Reg = 0;
			foreach (BEReporte oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr><td align='center'>" + Reg + "</td>");  // correlativo 
				sb.Append("<td>" + oBEc.Sucursal + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.NumeroDocumento + "</td>");
				sb.Append("<td>" + oBEc.Cliente + "</td>");
				sb.Append("<td>" + oBEc.TipoComprobante + "</td>");
				sb.Append("<td>" + oBEc.SerieNumero + "</td>");
				sb.Append("<td>" + oBEc.FechaVenta.ToString("dd/MM/yyyy HH:mm:ss") + "</td>");
				sb.Append("<td>" + oBEc.Cajero + "</td>");
				sb.Append("<td>" + oBEc.Moneda + "</td>");
				sb.Append("<td align='right' style = 'mso-number-format:\\@;'>" + oBEc.TotalVenta + "</td>");
				sb.Append("<td align='right' style = 'mso-number-format:\\@;'>" + oBEc.Utilidad.ToString("N") + "</td>");
				sb.Append("<td>" + oBEc.EstadoVenta + "</td>");
				sb.Append("<td>" + oBEc.EstadoCobranza + "</td>");
				sb.Append("<td>" + oBEc.Pedido + "</td>");
				 
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}

		private String ReporteVentasDetalleListar(String pNombreEmpresa, Int32 v_IDSucursal, Int32 v_IDColaborador, Int32 v_IDRespPedido, String v_FechaInicio, String v_FechaFin, Int32 v_IDEstadoVenta, Int32 v_IDEstadoCobranza, String v_Filtro)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg, RegDet;
			IList ListaDet;
			IList ListaVentaDet;
			BLReporte oBL = new BLReporte();
			ListaDet = oBL.VentasResumidasPorSucursal(v_IDSucursal, v_IDColaborador, v_IDRespPedido, v_FechaInicio, v_FechaFin, v_IDEstadoVenta, v_IDEstadoCobranza, v_Filtro);
			String Sucursal = "-TODOS-";
			String Colaborador = "-TODOS-";
			Decimal TotalVenta = 0;

			foreach (BEReporte oBEc in ListaDet)
			{
				TotalVenta += oBEc.TotalVenta;
			}

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			if (v_IDColaborador > 0)
			{
				BEColaborador oBECol = new BLColaborador().SeleccionarColaborador(v_IDColaborador);
				Colaborador = oBECol.Colaborador.ToUpper();
			}
			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='13'></td></tr>");
			sb.Append("<tr><td align='Left' colspan='13'><h2>REPORTE DE VENTAS DETALLADA</h2></td></tr><br/><br/><br/>");
			sb.Append("<tr><td colspan='13'></td></tr>");
			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left' colspan='2'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='2'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>COLABORADOR:</b></td><td align='Left' colspan='2'>" + Colaborador + "</td><td align='Left' style='background-color:#FFFE33;font-weight:bold;'><b>TOTAL VENTA:</b><td align='Left' style='background-color:#FFFE33;font-weight:bold;'>" + TotalVenta.ToString("N") + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");
			sb.Append("<td align='center' rowspan='1' width='5%'>#</td>");
			sb.Append("<td rowspan='1' width='15%'>SUCURSAL</td>");
			sb.Append("<td rowspan='1' width='15%'>NRO.DOCUMENTO</td>");
			sb.Append("<td rowspan='1' width='15%'>CLIENTE</td>");
			sb.Append("<td rowspan='1' width='10%'>TIPO COMPROBANTE</td>");
			sb.Append("<td rowspan='1' width='10%'>SERIE-NÚMERO</td>");
			sb.Append("<td rowspan='1' width='10%'>FECHA VENTA</td>");
			sb.Append("<td rowspan='1' width='10%'>CAJERO</td>");
			sb.Append("<td rowspan='1' width='10%'>MONEDA</td>");
			sb.Append("<td rowspan='1' width='10%'>TOTAL VENTA</td>");
			sb.Append("<td rowspan='1' width='10%'>ESTADO VENTA</td>");
			sb.Append("<td rowspan='1' width='10%'>ESTADO COBRANZA</td>");
			sb.Append("</tr>");
			Reg = 0;
			foreach (BEReporte oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr><td align='center'>" + Reg + "</td>");  // correlativo 
				sb.Append("<td>" + oBEc.Sucursal + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.NumeroDocumento + "</td>");
				sb.Append("<td>" + oBEc.Cliente + "</td>");
				sb.Append("<td>" + oBEc.TipoComprobante + "</td>");
				sb.Append("<td>" + oBEc.SerieNumero + "</td>");
				sb.Append("<td>" + oBEc.FechaVenta.ToString("dd/MM/yyyy HH:mm:ss") + "</td>");
				sb.Append("<td>" + oBEc.Cajero + "</td>");
				sb.Append("<td>" + oBEc.Moneda + "</td>");
				sb.Append("<td align='right' style = 'mso-number-format:\\@;'>" + oBEc.TotalVenta + "</td>");
				sb.Append("<td>" + oBEc.EstadoVenta + "</td>");
				sb.Append("<td>" + oBEc.EstadoCobranza + "</td>");
				sb.Append("</tr>");


				//Detalle de Ventas
				sb.Append("<tr>");
				sb.Append("<td colspan='3'></td>");
				sb.Append("<td colspan='6'>");

				ListaVentaDet = new BLVentaDetalle().VentaDetalleListar(oBEc.IDVenta);

				sb.Append("<br/><table class='tabla_kardex' width='100%' border='1'>");
				sb.Append("<tr style='background-color:#BDFFFD;font-weight:bold;'>");
				sb.Append("<td align='center' width='5%'>#</td>");
				sb.Append("<td width='15%' colspan='2'>PRODUCTO</td>");
				sb.Append("<td width='15%'>UM</td>");
				sb.Append("<td width='15%'>CANTIDAD</td>");
				sb.Append("<td width='10%'>PRE.UNI</td>");
				sb.Append("<td width='10%'>IMP.TOTAL</td>");
				sb.Append("</tr>");
				RegDet = 0;
				foreach (BEVentaDetalle oBEDet in ListaVentaDet)
				{
					RegDet = RegDet + 1;
					sb.Append("<tr><td align='center'>" + RegDet + "</td>");  // correlativo 
					sb.Append("<td colspan='2'>" + oBEDet.Producto + "</td>");
					sb.Append("<td>" + oBEDet.UnidadMedidaVenta + "</td>");
					sb.Append("<td align='center' style = 'mso-number-format:\\@;'>" + oBEDet.Cantidad + "</td>");
					sb.Append("<td align='right' style = 'mso-number-format:\\@;'>" + oBEDet.PrecioVenta + "</td>");
					sb.Append("<td align='right' style = 'mso-number-format:\\@;'>" + oBEDet.ImporteTotal + "</td>");
					sb.Append("</tr>");
				}
				sb.Append("</table>");
				sb.Append("</td>");
				sb.Append("</tr>");
				//Fin Detalle

			}
			sb.Append("</table><br/>");
			return sb.ToString();

		}

		private String ReporteMovimientoCaja(String pNombreEmpresa, Int32 v_IDSucursal, String v_TipoMovimientoCaja, Int32 v_IDCaja, String v_FechaInicio, String v_FechaFin)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BLReporte oBL = new BLReporte();
			ListaDet = oBL.ReporteMovimientoCajaListar(v_IDSucursal, v_TipoMovimientoCaja, v_IDCaja, v_FechaInicio, v_FechaFin);

			String Sucursal = "-TODOS-";
			String Caja = "-TODOS-";

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			if (v_IDCaja > 0)
			{
				BECajaMecanica oBECaja = new BLCajaMecanica().CajaMecanicaSeleccionar(v_IDCaja);
				Caja = oBECaja.Nombre.ToUpper();
			}

			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='13'></td></tr>");
			sb.Append("<tr><td align='Left' colspan='13'><h2>REPORTE MOVIMIENTOS DE CAJA</h2></td></tr><br/><br/><br/>");
			sb.Append("<tr><td colspan='13'></td></tr>");
			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left' colspan='2'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='2'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>CAJA:</b></td><td align='Left' colspan='2'>" + Caja + "</td></tr>");
			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");
			sb.Append("<td align='center' rowspan='1' width='5%'>N°</td>");
			sb.Append("<td rowspan='1' width='15%'>SUCURSAL</td>");
			sb.Append("<td rowspan='1' width='45%'>CAJA</td>");
			sb.Append("<td rowspan='1' width='45%'>TIPO MOVIMIENTO</td>");
			sb.Append("<td rowspan='1' width='45%'>OPERACIÓN</td>");
			sb.Append("<td rowspan='1' width='45%'>FECHA MOVIMIENTO</td>");
			sb.Append("<td rowspan='1' width='15%'>TIPO COMPROBANTE</td>");
			sb.Append("<td rowspan='1' width='15%'>SERIE COMPROBANTE</td>");
			sb.Append("<td rowspan='1' width='15%'>MONTO</td>");
			sb.Append("<td rowspan='1' width='15%'>OBSERVACION</td>");
			sb.Append("<td rowspan='1' width='15%'>USUARIO CREADOR</td>");
			sb.Append("</tr>");
			Reg = 0;
			foreach (BEReporte oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr><td align='center'>" + Reg + "</td>");  // correlativo 
				sb.Append("<td width='40%'>" + oBEc.Sucursal + "</td>");
				sb.Append("<td>" + oBEc.CajaMecanica + "</td>");
				sb.Append("<td>" + oBEc.NombreTipoMovimiento + "</td>");
				sb.Append("<td>" + oBEc.Operacion + "</td>");
				sb.Append("<td>" + oBEc.FechaMovimiento.ToString("dd/MM/yyyy") + "</td>");
				sb.Append("<td>" + oBEc.TipoComprobante + "</td>");
				sb.Append("<td>" + oBEc.SerieNumero + "</td>");
				sb.Append("<td>" + oBEc.Monto.ToString("N") + "</td>");
				sb.Append("<td>" + oBEc.Observacion + "</td>");
				sb.Append("<td>" + oBEc.UsuarioCreacion + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}

		private String ReporteResumenCaja(String pNombreEmpresa, Int32 v_IDSucursal, Int32 v_IDCaja, Int32 v_IDColaborador, String v_FechaInicio, String v_FechaFin)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BLReporte oBL = new BLReporte();
			ListaDet = oBL.ReporteCajaResumenListar(v_IDSucursal, v_IDCaja, v_IDColaborador, v_FechaInicio, v_FechaFin);
			String Sucursal = "-TODOS-";
			String Caja = "-TODOS-";
			String Colaborador = "-TODOS-";

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			if (v_IDCaja > 0)
			{
				BECaja oBECaja = new BLCaja().CajaSeleccionar(v_IDCaja);
				Caja = oBECaja.CajaMecanica.ToUpper();
			}

			if (v_IDColaborador > 0)
			{
				BEColaborador oBECol = new BLColaborador().SeleccionarColaborador(v_IDColaborador);
				Colaborador = oBECol.Colaborador.ToUpper();
			}
			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='7'></td></tr>");
			sb.Append("<tr><td align='Left' colspan='23'><h2>REPORTE RESUMEN DE CAJA</h2></td></tr><br/><br/><br/>");

			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left' colspan='2'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='2'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>CAJA:</b></td><td align='Left' colspan='2'>" + Caja + "</td><td align='Left'><b>CAJERO:</b></td><td align='Left'>" + Colaborador + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");

			sb.Append("<td align='center' rowspan='1' width='5%'>N°</td>");
			sb.Append("<td rowspan='1' width='15%'>SUCURSAL</td>");
			sb.Append("<td rowspan='1' width='15%'>CAJA</td>");
			sb.Append("<td rowspan='1' width='45%'>FECHA REGISTRO</td>");
			sb.Append("<td rowspan='1' width='45%'>FECHA APERTURA</td>");
			sb.Append("<td rowspan='1' width='45%'>FECHA CIERRE</td>");
			sb.Append("<td rowspan='1' width='15%'>USUARIO APERTURA</td>");
			sb.Append("<td rowspan='1' width='15%'>CONTADO</td>");
			sb.Append("<td rowspan='1' width='15%'>CALCULADO</td>");
			sb.Append("<td rowspan='1' width='15%'>DIFERENCIA</td>");
			sb.Append("<td rowspan='1' width='15%'>RETIRO</td>");
			sb.Append("</tr>");
			Reg = 0;
			foreach (BEReporte oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr><td align='center'>" + Reg + "</td>");  // correlativo 
				sb.Append("<td>" + oBEc.Sucursal + "</td>");
				sb.Append("<td>" + oBEc.CajaMecanica + "</td>");
				sb.Append("<td width='45%'>" + oBEc.FechaCaja + "</td>");
				sb.Append("<td width='45%'>" + oBEc.FechaApertura + "</td>");
				sb.Append("<td width='45%'>" + oBEc.FechaCierre + "</td>");
				sb.Append("<td>" + oBEc.UsuarioApertura + "</td>");
				sb.Append("<td>" + oBEc.Contado.ToString("N") + "</td>");
				sb.Append("<td>" + oBEc.Calculado.ToString("N") + "</td>");
				sb.Append("<td>" + oBEc.Diferencia.ToString("N") + "</td>");
				sb.Append("<td>" + oBEc.Retiro.ToString("N") + "</td>");

				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}

		private String ReporteVendedoresTopPorSucursal(String pNombreEmpresa, Int32 v_IDSucursal, String v_FechaInicio, String v_FechaFin)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BLReporte oBL = new BLReporte();
			ListaDet = oBL.ReporteVendedoresTopPorSucursal(v_IDSucursal, v_FechaInicio, v_FechaFin);

			String Sucursal = "-TODOS-";

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			Decimal TotalVenta = 0;

			foreach (BEReporte oBEc in ListaDet)
			{
				TotalVenta += oBEc.TotalVenta;
			}

			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='3'></td></tr>");

			sb.Append("<tr><td align='Left' colspan='3'><h2>REPORTE VENDEDORES TOP</h2></td></tr><br/><br/><br/>");

			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left' style='background-color:#FFFE33;font-weight:bold;'><b>TOTAL VENTA:</b><td align='Left' style='background-color:#FFFE33;font-weight:bold;'>" + TotalVenta.ToString("N") + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");

			sb.Append("<td align='center' width='5%'>SUCURSAL</td>");
			sb.Append("<td rowspan='1' width='30%'>COLABORADOR</td>");
			sb.Append("<td rowspan='1' width='15%'>VENTA TOTAL</td>");
			sb.Append("</tr>");
			Reg = 0;
			foreach (BEReporte oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr><td align='center'>" + oBEc.Sucursal + "</td>");
				sb.Append("<td>" + oBEc.Colaborador + "</td>");
				sb.Append("<td>" + oBEc.TotalVenta.ToString("N") + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}

		private String ReporteClientesTop10Ventas(String pNombreEmpresa, Int32 v_IDSucursal, String v_FechaInicio, String v_FechaFin)
		{


			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BLReporte oBL = new BLReporte();
			ListaDet = oBL.ReporteClientesTop10Ventas(v_IDSucursal, 0, v_FechaInicio, v_FechaFin);
			String Sucursal = "-TODOS-";
			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			Decimal TotalVenta = 0;

			foreach (BEReporte oBEc in ListaDet)
			{
				TotalVenta += oBEc.VentasTotal;
			}



			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='4'></td></tr>");


			sb.Append("<tr><td align='Left' colspan='4'><h2>REPORTE CLIENTES TOP</h2></td></tr><br/><br/><br/>");

			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left' style='background-color:#FFFE33;font-weight:bold;'><b>TOTAL VENTA:</b><td align='Left' style='background-color:#FFFE33;font-weight:bold;'>" + TotalVenta.ToString("N") + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");

			sb.Append("<td align='left'>SUCURSAL</td>");
			sb.Append("<td width='15%'>NUMERO DOCUMENTO</td>");
			sb.Append("<td width='30%'>CLIENTE</td>");
			sb.Append("<td width='15%'>VENTA TOTAL</td>");
			sb.Append("</tr>");
			Reg = 0;
			foreach (BEReporte oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr><td align='left'>" + oBEc.Nombre + "</td>");  // correlativo 
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.NumeroDocumento + "</td>");
				sb.Append("<td>" + oBEc.Cliente + "</td>");
				sb.Append("<td align='right'>" + oBEc.VentasTotal.ToString("N") + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}

		private String ReportePrecioProductoDigemid(Int32 v_IDSucursal)
		{
			StringBuilder sb = new StringBuilder();
			IList ListaDet;
			BLProducto oBL = new BLProducto();
			ListaDet = oBL.ReporteProductoDigemid(v_IDSucursal);
			sb.Append("<table class='tabla_kardex' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");
			sb.Append("<td>CodEstab</td>");
			sb.Append("<td>CodProd</td>");
			sb.Append("<td>Precio 1</td>");
			sb.Append("<td>Precio 2</td>");
			sb.Append("<td>Precio 3</td>");
			sb.Append("</tr>");
			foreach (BEReporte oBEc in ListaDet)
			{
				sb.Append("<tr>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.CodEstab + "</td>");
				sb.Append("<td align='right'>" + oBEc.CodProd + "</td>");
				sb.Append("<td align='right'>" + oBEc.Precio1.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEc.Precio2.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEc.Precio3.ToString("N") + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table>");
			return sb.ToString();
		}

		private String ReporteProductoMasVendidos(String pNombreEmpresa, Int32 v_IDSucursal, Int32 v_IDCategoria, String v_FechaInicio, String v_FechaFin, Decimal v_pCantidad)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BLProducto oBL = new BLProducto();
			ListaDet = oBL.ReporteProductosMasVendidosListar(v_IDSucursal, v_IDCategoria, v_FechaInicio, v_FechaFin);

			String Sucursal = "-TODOS-";
			String Categoria = "-TODOS-";

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			if (v_IDCategoria > 0)
			{
				BECategoria oBECate = new BLCategoria().Seleccionar(v_IDCategoria);
				Categoria = oBECate.Nombre.ToUpper();
			}

			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='7'></td></tr>");

			sb.Append("<tr><td align='Left' colspan='4'><h2>REPORTE PRODUCTOS MAS VENDIDOS</h2></td></tr><br/><br/><br/>");

			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>CATEGORIA:</b><td align='Left'>" + Categoria + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");
			sb.Append("<td width='15%'>Sucursal</td>");
			sb.Append("<td width='15%'>Categoria</td>");
			sb.Append("<td width='15%'>Producto</td>");
			sb.Append("<td width='15%'>Unidad de Medida</td>");
			sb.Append("<td width='10%'>Cantidad</td>");
			sb.Append("</tr>");
			Reg = 0;
			foreach (BEVentaDetalle oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr>");
				sb.Append("<td>" + oBEc.Sucursal + "</td>");
				sb.Append("<td>" + oBEc.NombreCategoria + "</td>");
				sb.Append("<td>" + oBEc.NombreProducto + "</td>");
				sb.Append("<td>" + oBEc.NombreUnidadMedida + "</td>");
				sb.Append("<td>" + oBEc.Cantidad.ToString("N") + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}

		private String ReporteStockProductos(String pNombreEmpresa, Int32 pIDCategoria, Int32 pIDMarca, Int32 pIDSucursal, String pFiltroProducto, String pTipoConsulta)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BLReporte oBL = new BLReporte();
			ListaDet = oBL.ReporteStockProductoxSucursalListar(pIDCategoria, pIDMarca, pIDSucursal, pFiltroProducto, pTipoConsulta);

			String Sucursal = "-TODOS-";
			String Categoria = "-TODOS-";
			String Marca = "-TODOS-";

			if (pIDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(pIDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			if (pIDMarca > 0)
			{
				BEMarca oBEMarca = new BLMarca().Seleccionar(pIDMarca);
				Marca = oBEMarca.Nombre.ToUpper();
			}

			if (pIDCategoria > 0)
			{
				BECategoria oBECat = new BLCategoria().Seleccionar(pIDCategoria);
				Categoria = oBECat.Nombre.ToUpper();
			}

			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='7'></td></tr>");

			sb.Append("<tr><td align='Left' colspan='4'><h2>REPORTE DE PRODUCTOS</h2></td></tr><br/><br/><br/>");

			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left'>" + pNombreEmpresa + "</td><td align='Left'><b>CATEGORIA:</b><td align='Left'>" + Categoria + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left'>" + Sucursal + "</td><td align='Left'><b>LABORATORIO:</b><td align='Left'>" + Marca + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");

			sb.Append("<td>Sucursal</td>");
			sb.Append("<td>Categoria</td>");
			sb.Append("<td>Laboratorio</td>");
			sb.Append("<td>Código Alterna</td>");
			sb.Append("<td>Nombre</td>");
			sb.Append("<td>Localización</td>");
			sb.Append("<td>UnidadMedidaCompra</td>");
			sb.Append("<td>UnidadMedidaVenta</td>");
			sb.Append("<td>Factor</td>");
			sb.Append("<td>StockMinimo</td>");
			sb.Append("<td>StockActual</td>");
			sb.Append("<td>PrecioCostoUnidadConIgv</td>");
			sb.Append("<td>Valorizado</td>");
			sb.Append("<td>PrecioVenta</td>");
			sb.Append("<td>MargenUtilidad</td>");
			sb.Append("<td>ControlaLote</td>");
			sb.Append("<td>VentaConReceta</td>");
			sb.Append("</tr>");
			Reg = 0;
			foreach (BEProducto oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr>");
				sb.Append("<td>" + oBEc.Sucursal + "</td>");
				sb.Append("<td>" + oBEc.Categoria + "</td>");
				sb.Append("<td>" + oBEc.Laboratorio + "</td>");
				sb.Append("<td>" + oBEc.CodigoAlterna + "</td>");
				sb.Append("<td>" + oBEc.Nombre + "</td>");
				sb.Append("<td>" + oBEc.Localizacion + "</td>");
				sb.Append("<td>" + oBEc.UnidadMedidaCompra + "</td>");
				sb.Append("<td>" + oBEc.UnidadMedidaVenta + "</td>");
				sb.Append("<td align='center'>" + oBEc.Factor + "</td>");
				sb.Append("<td align='center'>" + oBEc.StockMinimo.ToString("N") + "</td>");
				sb.Append("<td align='center'>" + oBEc.StockActual.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEc.PrecioCostoUnidadConIgv.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEc.Valorizado.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEc.PrecioVenta.ToString("N") + "</td>");
				sb.Append("<td align='center'>" + oBEc.MargenUtilidad.ToString("N") + "</td>");
				sb.Append("<td align='center'>" + oBEc.ControlaLote + "</td>");
				sb.Append("<td align='center'>" + oBEc.VentaConReceta + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}

		private String ComprasxProveedorResumido(String pNombreEmpresa, Int32 v_IDSucursal, Int32 v_IDProveedor, String v_FechaInicio, String v_FechaFin)
		{

			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BECompras oBEComp = new BECompras();
			BLCompras oBL = new BLCompras();
			ListaDet = oBL.ReporteComprasxProveedorListar(v_IDSucursal, v_IDProveedor, v_FechaInicio, v_FechaFin);

			Decimal TotalCompra = 0;

			foreach (BECompras oBEc in ListaDet)
			{
				TotalCompra += oBEc.TotalCompra;
			}

			String Sucursal = "-TODOS-";
			String Proveedor = "-TODOS-";

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			if (v_IDProveedor > 0)
			{
				BEProveedor oBEProve = new BLProveedor().ProveedorSeleccionar(v_IDProveedor);
				Proveedor = oBEProve.RazonSocial.ToUpper();
			}

			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='7'></td></tr>");

			sb.Append("<tr><td align='Left' colspan='4'><h2>REPORTE DE COMPRAS</h2></td></tr><br/><br/><br/>");
			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left' colspan='2'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='2'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>PROVEEDOR:</b></td><td align='Left' colspan='2'>" + Proveedor + "</td><td align='Left' style='background-color:#FFFE33;font-weight:bold;'><b>TOTAL COMPRA:</b><td align='Left' style='background-color:#FFFE33;font-weight:bold;'>" + TotalCompra.ToString("N") + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");

			sb.Append("<td>Sucursal</td>");
			sb.Append("<td>NumeroCompra</td>");
			sb.Append("<td>NumeroDocumento</td>");
			sb.Append("<td>Proveedor</td>");
			sb.Append("<td>FechaCompra</td>");
			sb.Append("<td>TipoComprobante</td>");
			sb.Append("<td>Serie-Numero</td>");
			sb.Append("<td>TotalCompra</td>");
			sb.Append("<td>FormaPago</td>");
			sb.Append("<td>EstadoCompra</td>");
			sb.Append("<td>EstadoPago</td>");
			sb.Append("</tr>");
			Reg = 0;
			foreach (BECompras oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr>");              // correlativo 
				sb.Append("<td>" + oBEc.Sucursal + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.NumeroCompra + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.NumeroDocumento + "</td>");   // FechaVencimiento
				sb.Append("<td>" + oBEc.RazonSocial + "</td>");              // FechaEmision
				sb.Append("<td>" + oBEc.FechaCompra.ToShortDateString() + "</td>");              // FechaEmision
				sb.Append("<td>" + oBEc.TipoComprobante + "</td>");              // FechaEmision
				sb.Append("<td>" + oBEc.SerieNumero + "</td>");              // FechaEmision 
				sb.Append("<td align='right'>" + oBEc.TotalCompra.ToString("N") + "</td>");              // FechaEmision 
				sb.Append("<td>" + oBEc.FormaPago + "</td>");
				sb.Append("<td>" + oBEc.EstadoCompra + "</td>");
				sb.Append("<td>" + oBEc.EstadoPago + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}

		private String ComprasxProveedorDetallado(String pNombreEmpresa, Int32 v_IDSucursal, Int32 v_IDProveedor, String v_FechaInicio, String v_FechaFin)
		{

			StringBuilder sb = new StringBuilder();
			Int32 Reg, RegDet;
			IList ListaDet;
			IList ListaCompraDet;

			BECompras oBEComp = new BECompras();
			BLCompras oBL = new BLCompras();
			ListaDet = oBL.ReporteComprasxProveedorListar(v_IDSucursal, v_IDProveedor, v_FechaInicio, v_FechaFin);

			Decimal TotalCompra = 0;

			foreach (BECompras oBEc in ListaDet)
			{
				TotalCompra += oBEc.TotalCompra;
			}

			String Sucursal = "-TODOS-";
			String Proveedor = "-TODOS-";

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			if (v_IDProveedor > 0)
			{
				BEProveedor oBEProve = new BLProveedor().ProveedorSeleccionar(v_IDProveedor);
				Proveedor = oBEProve.RazonSocial.ToUpper();
			}

			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='7'></td></tr>");

			sb.Append("<tr><td align='Left' colspan='4'><h2>REPORTE DE COMPRAS DETALLADO</h2></td></tr><br/><br/><br/>");
			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left' colspan='2'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='2'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>PROVEEDOR:</b></td><td align='Left' colspan='2'>" + Proveedor + "</td><td align='Left' style='background-color:#FFFE33;font-weight:bold;'><b>TOTAL COMPRA:</b><td align='Left' style='background-color:#FFFE33;font-weight:bold;'>" + TotalCompra.ToString("N") + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");

			sb.Append("<td>Sucursal</td>");
			sb.Append("<td>NumeroCompra</td>");
			sb.Append("<td>NumeroDocumento</td>");
			sb.Append("<td>Proveedor</td>");
			sb.Append("<td>FechaCompra</td>");
			sb.Append("<td>TipoComprobante</td>");
			sb.Append("<td>Serie-Numero</td>");
			sb.Append("<td>TotalCompra</td>");
			sb.Append("<td>FormaPago</td>");
			sb.Append("<td>EstadoCompra</td>");
			sb.Append("<td>EstadoPago</td>");
			sb.Append("</tr>");
			Reg = 0;
			foreach (BECompras oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr>");              // correlativo 
				sb.Append("<td>" + oBEc.Sucursal + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.NumeroCompra + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.NumeroDocumento + "</td>");   // FechaVencimiento
				sb.Append("<td>" + oBEc.RazonSocial + "</td>");              // FechaEmision
				sb.Append("<td>" + oBEc.FechaCompra.ToShortDateString() + "</td>");              // FechaEmision
				sb.Append("<td>" + oBEc.TipoComprobante + "</td>");              // FechaEmision
				sb.Append("<td>" + oBEc.SerieNumero + "</td>");              // FechaEmision 
				sb.Append("<td align='right'>" + oBEc.TotalCompra.ToString("N") + "</td>");              // FechaEmision 
				sb.Append("<td>" + oBEc.FormaPago + "</td>");
				sb.Append("<td>" + oBEc.EstadoCompra + "</td>");
				sb.Append("<td>" + oBEc.EstadoPago + "</td>");
				sb.Append("</tr>");



				//Detalle de Compras
				sb.Append("<tr>");
				sb.Append("<td colspan='2'></td>");
				sb.Append("<td colspan='5'>");

				ListaCompraDet = new BLCompras().ComprasDetalleListar(oBEc.IDCompras);

				sb.Append("<br/><table class='tabla_kardex' width='100%' border='1'>");
				sb.Append("<tr style='background-color:#BDFFFD;font-weight:bold;'>");
				sb.Append("<td align='center' width='5%'>#</td>");
				sb.Append("<td width='15%' colspan='2'>PRODUCTO</td>");
				sb.Append("<td width='15%'>UM</td>");
				sb.Append("<td width='15%'>CANTIDAD</td>");
				sb.Append("<td width='10%'>PRE.UNI</td>");
				sb.Append("<td width='10%'>IMP.TOTAL</td>");
				sb.Append("</tr>");
				RegDet = 0;
				foreach (BEComprasDetalle oBEDet in ListaCompraDet)
				{
					RegDet = RegDet + 1;
					sb.Append("<tr><td align='center'>" + RegDet + "</td>");  // correlativo 
					sb.Append("<td colspan='2'>" + oBEDet.Producto + "</td>");
					sb.Append("<td>" + oBEDet.UnidadMedidaVenta + "</td>");
					sb.Append("<td align='center' style = 'mso-number-format:\\@;'>" + oBEDet.Cantidad.ToString("N") + "</td>");
					sb.Append("<td align='right' style = 'mso-number-format:\\@;'>" + oBEDet.PrecioUnitario.ToString("N") + "</td>");
					sb.Append("<td align='right' style = 'mso-number-format:\\@;'>" + oBEDet.SubTotal.ToString("N") + "</td>");
					sb.Append("</tr>");
				}
				sb.Append("</table>");
				sb.Append("</td>");
				sb.Append("</tr>");
				//Fin Detalle


			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}

		private String ReporteCobranza(String pNombreEmpresa, Int32 v_IDSucursal, Int32 v_IDMedioPago, Int32 v_IDCliente, String v_FechaInicio, String v_FechaFin)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BLCobranza oBL = new BLCobranza();
			ListaDet = oBL.ReporteCobranzaListar(v_IDSucursal, v_IDCliente, v_IDMedioPago, v_FechaInicio, v_FechaFin);
			String Sucursal = "-TODOS-";
			String Cliente = "-TODOS-";
			String MedioPago = "-TODOS-";

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			if (v_IDCliente > 0)
			{
				BECliente oBECliente = new BLCliente().ClienteSeleccionar(v_IDCliente);
				Cliente = oBECliente.RazonSocial.ToUpper();
			}

			if (v_IDMedioPago > 0)
			{
				BEMedioPago oBEMedioPago = new BLMedioPago().MedioPagoSeleccionar(v_IDMedioPago);
				MedioPago = oBEMedioPago.Nombre.ToUpper();
			}

			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='7'></td></tr>");

			sb.Append("<tr><td align='Left' colspan='4'><h2>REPORTE DE COBRANZA</h2></td></tr><br/><br/><br/>");
			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left' colspan='2'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='2'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>CLIENTE:</b></td><td align='Left' colspan='2'>" + Cliente + "</td><td align='Left'><b>MEDIO DE PAGO:</b><td align='Left'>" + MedioPago + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>");
			sb.Append("<td>Numero Cobranza</td>");
			sb.Append("<td>Medio Pago</td>"); 
			sb.Append("<td>Fecha de Cobranza</td>"); 
			sb.Append("<td>Observacion</td>");
			sb.Append("<td>Monto Cobrado</td>");

			sb.Append("</tr>");
			Reg = 0;
			foreach (BECobranza oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr>");  // correlativo 
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.NumeroCobranzaFormato + "</td>");
				sb.Append("<td>" + oBEc.MedioPago + "</td>"); 
				sb.Append("<td>" + oBEc.FechaCobro.ToShortDateString() + "</td>"); 
				sb.Append("<td>" + oBEc.Observacion + "</td>");
				sb.Append("<td>" + oBEc.MontoCobrado.ToString("N") + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}
		 
		private String PagoProveedor(String pNombreEmpresa, Int32 v_IDSucursal, Int32 v_IDProveedor, String v_FechaInicio, String v_FechaFin)
		{
			StringBuilder sb = new StringBuilder();
			Int32 Reg;
			IList ListaDet;
			BLCuentaPorPagar oBL = new BLCuentaPorPagar();
			ListaDet = oBL.ReportePagoListar(v_IDProveedor, v_IDSucursal, v_FechaInicio, v_FechaFin);

			String Sucursal = "-TODOS-";
			String Proveedor = "-TODOS-";

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			if (v_IDProveedor > 0)
			{
				BEProveedor oBEProve = new BLProveedor().ProveedorSeleccionar(v_IDProveedor);
				Proveedor = oBEProve.RazonSocial.ToUpper();
			}

			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='7'></td></tr>");

			sb.Append("<tr><td align='Left' colspan='4'><h2>REPORTE DE PAGO A PROVEEDORES</h2></td></tr><br/><br/><br/>");
			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left' colspan='2'>" + pNombreEmpresa + "</td><td align='Left'><b>FECHA INICIO:</b><td align='Left'>" + v_FechaInicio + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='2'>" + Sucursal + "</td><td align='Left'><b>FECHA FIN:</b><td align='Left'>" + v_FechaFin + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>PROVEEDOR:</b></td><td align='Left' colspan='2'>" + Proveedor + "</td></tr>");
			 
			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;font-weight:bold;'>"); 
			sb.Append("<td>NumeroPagoFormato</td>");
			sb.Append("<td>RUC</td>");
			sb.Append("<td>Proveedor</td>"); 
			sb.Append("<td>Fecha de Pago</td>");
			sb.Append("<td>Serie-Numero</td>");
			sb.Append("<td>Concepto</td>"); 
			sb.Append("<td>ImportePagado</td>");

			sb.Append("</tr>");
			Reg = 0;
			foreach (BEPago oBEc in ListaDet)
			{
				Reg = Reg + 1;
				sb.Append("<tr>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.NumeroPagoFormato + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEc.ProveedorNumeroDocumento + "</td>");
				sb.Append("<td>" + oBEc.Proveedor + "</td>"); 
				sb.Append("<td>" + oBEc.FechaPago.ToShortDateString() + "</td>");
				sb.Append("<td>" + oBEc.SerieNumero + "</td>");
				sb.Append("<td>" + oBEc.Concepto + "</td>"); 
				sb.Append("<td>" + oBEc.ImportePagado.ToString("N") + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();

		}
		 
		private String ListarRegistroVentas(String pNombreEmpresa, Int32 v_IDSucursal, String pPeriodo)
		{
			StringBuilder sb = new StringBuilder();
			IList ListaDet;
			BLReporte oBL = new BLReporte();
			ListaDet = oBL.ReporteRegistroLibroVentas(v_IDSucursal, pPeriodo);

			String Sucursal = "-TODOS-"; 

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}


			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='30'></td></tr>");

			sb.Append("<tr><td align='Left' colspan='4'><h2>REGISTRO DE VENTAS E INGRESOS</h2></td></tr><br/><br/><br/>");
			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left' colspan='2'>" + pNombreEmpresa + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='2'>" + Sucursal + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>PERIODO:</b></td><td align='Left' colspan='2'>" + pPeriodo + "</td></tr>");

			sb.Append("</table><br/>");

			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;'>");
			sb.Append("<td colspan = '2' width='10%' align='Center'>NRO. CORELATIVO DE REGISTRO</td>");
			sb.Append("<td rowspan = '2' width='20%' align='Center'>FECHA VENTA</td>");
			sb.Append("<td colspan='3' width='20%' align='Center'>INFORMACION DEL CLIENTE</td>"); 
			sb.Append("<td colspan='3' width='20%' align='Center'>PRODUCTO</td>"); 
			sb.Append("<td colspan='2' width='10%' align='Center'>BASE IMPONIBLE OPERACION GRAVADA</td>");
			sb.Append("<td rowspan = '2' align='Center'>IGV</td>");
			sb.Append("<td rowspan= '2' align='Center'>IMPORTE TOTAL</td>");
			sb.Append("<td colspan='8' align='Center'>INFORMACION FACTURACION</td>"); 
			sb.Append("</tr>");

			sb.Append("<tr style='background-color:#f7f7f7;'>");
			sb.Append("<td>NRO</td>");
			sb.Append("<td>PERIODO</td>"); 
			sb.Append("<td>SUCURSAL</td>");
			sb.Append("<td>NOMBRE CLIENTE</td>"); 
			sb.Append("<td>SERIE NUMERO</td>"); 
			sb.Append("<td>PRODUCTO</td>"); 
			sb.Append("<td>CALCULO IGV</td>");
			sb.Append("<td>CALCULO ISC</td>");
			sb.Append("<td>CALCULO DETRACCION</td>");
			sb.Append("<td>TOTAL OP. GRAVADA</td>"); 
			sb.Append("<td>TIPO DE CAMBIO</td>");
			sb.Append("<td>MIGRADO</td>");
			sb.Append("<td>ANULADO</td>");
			sb.Append("<td>COBRADO</td>");
			sb.Append("<td>MOTIVO ANULACION</td>");
			sb.Append("<td>FECHA ANULADO</td>");
			sb.Append("<td>ESTADO VENTA</td>");
			sb.Append("<td>ESTADO COBRANZA</td>");
			sb.Append("</tr>");

			foreach (BEReporte oBEcomp in ListaDet)
			{
				sb.Append("<td>" + oBEcomp.Venta + "</td>");
				sb.Append("<td>" + oBEcomp.Periodo + "</td>");
				sb.Append("<td>" + oBEcomp.FechaVenta.ToString("dd/MM/yyyy") + "</td>");

				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.Sucursal + "</td>");
				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.NombreCliente + "</td>");
				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.SerieNumero + "</td>");
				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.Producto + "</td>");
				sb.Append("<td align='Center'>" + oBEcomp.CalculoIGV.ToString("0.00") + "</td>");
				sb.Append("<td align='Center'>" + oBEcomp.CalculoISC.ToString("0.00") + "</td>");
				sb.Append("<td align='Center'>" + oBEcomp.CalculoDetraccion.ToString("0.00") + "</td>");
				sb.Append("<td align='Center'>" + oBEcomp.TotalOperacionGravada.ToString("0.00") + "</td>"); 
				sb.Append("<td align='Center'>" + oBEcomp.TotalIgv.ToString("0.00") + "</td>");
				sb.Append("<td align='Center'>" + oBEcomp.TotalVenta.ToString("0.00") + "</td>"); 
				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.TipoCambio + "</td>");
				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.Migrado + "</td>");
				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.Anulado + "</td>");
				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.Cobrado + "</td>");
				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.MotivoAnulacion + "</td>");
				sb.Append("<td align='Center'>" + oBEcomp.FechaAnulado.ToString("dd/MM/yyyy") + "</td>");
				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.EstadoVenta + "</td>");
				sb.Append("<td align='Center' style = 'mso-number-format:\\@;'>" + oBEcomp.EstadoCobranza + "</td>");

				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();
		}

		private String ListarRegistroCompras(String pNombreEmpresa, Int32 v_IDSucursal, String pPeriodo)
		{
			StringBuilder sb = new StringBuilder();
			IList ListaDet;
			BLCompras oBL = new BLCompras();
			ListaDet = oBL.ReporteRegistroCompras(v_IDSucursal, pPeriodo);

			String Sucursal = "-TODOS-";

			if (v_IDSucursal > 0)
			{
				BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(v_IDSucursal);
				Sucursal = oBESucursal.Nombre.ToUpper();
			}

			sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
			sb.Append("<tr><td colspan='23'></td></tr>");

			sb.Append("<tr><td align='Left' colspan='4'><h2>REGISTRO DE COMPRAS</h2></td></tr><br/><br/><br/>");
			sb.Append("<tr><td align='Left'><b>EMPRESA:</b></td><td align='Left' colspan='2'>" + pNombreEmpresa + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='2'>" + Sucursal + "</td></tr>");
			sb.Append("<tr><td align='Left'><b>PERIODO:</b></td><td align='Left' colspan='2'>" + pPeriodo + "</td></tr>");

			sb.Append("</table><br/>");
			  
			sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
			sb.Append("<tr style='background-color:#f7f7f7;'>");
			sb.Append("<td rowspan = '2' width='10%'>PERIODO</td>");
			sb.Append("<td rowspan = '2' width='10%'>FECHA EMISIÓN</td>");
			sb.Append("<td colspan='2' width='10%'>FECHA DE VENCIMIENTO O FECHA DE PAGO</td>");
			sb.Append("<td colspan='3' width='10%'>COMPROBANTE DE PAGO</td>");
			sb.Append("<td colspan='3' width='10%'>INFORMACION DEL PROVEEDOR</td>");
			sb.Append("<td colspan='2' width='10%'>ADQUISICIONES GRAVADAS</td>");
			sb.Append("<td rowspan = '2' width='5%'>VALOR ADQUISICION NO GRAVADA</td>");
			sb.Append("<td rowspan = '2'>IMPORTE TOTAL</td>");
			sb.Append("<td colspan='4' width='10%'>REFERENCIA DEL COMPROBANTE DE PAGO O DOCUMENTO ORIGINAL QUE SE MODIFICA</td>");
			sb.Append("<td colspan='2' width='10%'>DOCUMENTO DE RETENCION</td>");
			//sb.Append("<td colspan='2' width='10%'>CUENTA CONTABLE</td>");
			sb.Append("<td rowspan = '2' width='10%'>PAGADO</td>");
			sb.Append("</tr>");
			sb.Append("<tr style='background-color:#f7f7f7;'>");
			sb.Append("<td>FECHA REGISTRO</td>");
			sb.Append("<td>FECHA VENCIMIENTO</td>");
			sb.Append("<td>TIPO COMPROBANTE</td>");
			sb.Append("<td>SERIE</td>");
			sb.Append("<td>NUMERO</td>");
			sb.Append("<td>TIPO</td>");
			sb.Append("<td>DOC. INDENTIDAD NUMERO</td>");
			sb.Append("<td>APELLIDOS Y NOMBRES O RAZON SOCIAL</td>");
			sb.Append("<td>BASE IMPONIBLE</td>");
			sb.Append("<td>IGV</td>");
			sb.Append("<td width='2%'>FECHA</td>");
			sb.Append("<td width='2%'>TIPO</td>");
			sb.Append("<td width='2%'>SERIE</td>");
			sb.Append("<td width='2%'>NUMERO</td>");
			sb.Append("<td>FECHA</td>");
			sb.Append("<td>NRO. COMPROBANTE</td>");
			//sb.Append("<td>CUENTA GASTO</td>");
			//sb.Append("<td>CUENTA PAGO</td>");
			sb.Append("</tr>");

			foreach (BECompras oBEcomp in ListaDet)
			{
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.Periodo + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.FechaRegistro.ToShortDateString() + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.FechaRegistro.ToShortDateString() + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.FechaVencimiento.ToShortDateString() + "</td>");
				//sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.CPTipoComprobante + "</td>");
				//sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.CPSerieComprobante + "</td>");
				//sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.CPNumeroComprobante + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.Sigla + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.CPSerieDocumento + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.CPNumeroDocumento + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.PROVTipoDocumento + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.PROVNumeroDocumento + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.PROVRazonSocial + "</td>");
				sb.Append("<td align='right'>" + oBEcomp.BaseImponible.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEcomp.Igv.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEcomp.ImporteNoGravado.ToString("N") + "</td>");
				sb.Append("<td align='right'>" + oBEcomp.ImporteTotal.ToString("N") + "</td>");
				if (oBEcomp.FechaEmisionReferencia.ToShortDateString() == "1/01/1900")
				{ sb.Append("<td></td>"); }
				else { sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.FechaEmisionReferencia.ToShortDateString() + "</td>"); }
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.TipoDocumentoReferencia + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.SerieReferencia + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.NumeroComprobanteReferencia + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.FechaEmisionRetencion + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.NumeroComprobanteRetencion + "</td>");
				//sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.CuentaGasto + "</td>");
				//sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.CuentaPago + "</td>");
				sb.Append("<td style = 'mso-number-format:\\@;'>" + oBEcomp.EstadoPago + "</td>");
				sb.Append("</tr>");
			}
			sb.Append("</table><br/><br/>");
			return sb.ToString();
		}

	}
}
