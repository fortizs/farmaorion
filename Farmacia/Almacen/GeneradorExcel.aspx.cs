using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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


namespace Farmacia.Almacen
{
    public partial class GeneradorExcel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            String v_TipoReporte = Request.QueryString["pTipoReporte"];

            String v_FechaInicio = "";
            String v_IDProducto = "0";
            String v_FechaFin = "";
            Int32 v_IDSucursal = 0;

            if (v_TipoReporte == "KARDEX")
            {

                v_IDSucursal = Int32.Parse(Request.QueryString["pIDSucursal"]);
                v_IDProducto = Request.QueryString["pIDProducto"];
                v_FechaInicio = Request.QueryString["pFechaInicio"];
                v_FechaFin = Request.QueryString["pFechaFin"];
            }

            if (v_TipoReporte == "KARDEXALM")
            {

                v_IDSucursal = Int32.Parse(Request.QueryString["pIDSucursal"]);
                v_FechaInicio = Request.QueryString["pFechaInicio"];
                v_FechaFin = Request.QueryString["pFechaFin"];
            }



            String nameReport;
            String nombre;

            String style = @"<style> .textmode { } </style>";
            Int32 v_IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
            HttpResponse response = Response;

            switch (v_TipoReporte)
            {
                case "KARDEX":
                    nameReport = "KARDEX_POR_PRODUCTO" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    nombre = nameReport + ".xls";
                    response.Clear();
                    response.Buffer = true;
                    response.ContentType = "application/vnd.ms-excel";
                    response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
                    response.Charset = "UTF-8";
                    response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
                    response.Write(KardexProducto(v_IDSucursal, v_IDProducto, v_FechaInicio, v_FechaFin));
                    response.End();
                    return;

                case "KARDEXALM":
                    nameReport = "KARDEX_POR_SUCURSAL" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    nombre = nameReport + ".xls";
                    response.Clear();
                    response.Buffer = true;
                    response.ContentType = "application/vnd.ms-excel";
                    response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
                    response.Charset = "UTF-8";
                    response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
                    response.Write(KardexAlmacen(v_IDSucursal, v_FechaInicio, v_FechaFin));
                    response.End();
                    return;

            }

        }

        private String KardexProducto(Int32 pIDSucursal, String pIDProducto, String pFechaInicio, String pFechaFin)
        {
            StringBuilder sb = new StringBuilder();
            Int32 Reg;

            BLKardex oBLKardex = new BLKardex();
            IList ListaKardex = oBLKardex.KardexBuscar(pIDSucursal, pIDProducto, pFechaInicio, pFechaFin);

            String Sucursal = "-TODOS-";
            String Producto = "-TODOS-";

            if (pIDSucursal > 0)
            {
                BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(pIDSucursal);
                Sucursal = oBESucursal.Nombre.ToUpper();
            }

            if (Int32.Parse(pIDProducto) > 0)
            {
                BEProducto oBEProducto = new BLProducto().ProductoSeleccionar(Int32.Parse(pIDProducto), pIDSucursal);
                Producto = oBEProducto.Nombre.ToUpper();
            }

            sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<tr><td colspan='7'></td></tr>");
            sb.Append("<tr><td align='Left' colspan='23'><h2>KARDEX POR PRODUCTO</h2></td></tr><br/><br/><br/>");
            sb.Append("<tr><td align='Left'><b>FECHA INICIO:</b><td align='Left' colspan='22'>" + pFechaInicio + "</td></tr>");
            sb.Append("<tr><td align='Left'><b>FECHA FIN:</b><td align='Left' colspan='22'>" + pFechaFin + "</td></tr>");
            sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='22'>" + Sucursal + "</td></tr>");
            sb.Append("<tr><td align='Left'><b>PRODUCTO:</b></td><td align='Left' colspan='22'>" + Producto + "</td></tr>");
            sb.Append("</table><br/>");

            sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
            sb.Append("<tr style='background-color:#FFBF00;font-weight:bold;'>");

            sb.Append("<td align='center' rowspan='1' width='5%'>Fecha Movimiento</td>");
            sb.Append("<td rowspan='1' width='10%'>Serie-Numero</td>");
            sb.Append("<td rowspan='1' width='10%'>Transacción</td>");
            sb.Append("<td rowspan='1' width='10%'>Cantidad Entrada</td>");
            sb.Append("<td rowspan='1' width='10%'>Precio Entrada</td>");
            sb.Append("<td rowspan='1' width='10%'>Sub Total Entrada</td>");
            sb.Append("<td rowspan='1' width='10%'>Cantidad Salida</td>");
            sb.Append("<td rowspan='1' width='10%'>Precio Salida</td>");
            sb.Append("<td rowspan='1' width='10%'>Sub Total Salida</td>");
            sb.Append("<td rowspan='1' width='10%'>Cantidad Saldo</td>");
            sb.Append("<td rowspan='1' width='10%'>Precio Saldo</td>");
            sb.Append("<td rowspan='1' width='10%'>Sub Total Saldo</td>");
            sb.Append("</tr>");
            Reg = 0;
            foreach (BEMovimientoDetalle oBEc in ListaKardex)
            {
                Reg = Reg + 1;
                sb.Append("<tr><td align='center'>" + oBEc.FechaMovimiento.ToString("dd/MM/yyyy") + "</td>");  // correlativo 
                sb.Append("<td>" + oBEc.DocumentoReferencia + "</td>");
                sb.Append("<td>" + oBEc.Transaccion + "</td>");
                sb.Append("<td>" + oBEc.EntradaCantidad.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.EntradaValorUnidad.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.EntradaValorUnidad.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.SalidaCantidad.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.SalidaValorUnidad.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.SalidaValorTotal.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.SaldoCantidad.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.SaldoValorUnidad.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.SaldoValorTotal.ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table><br/><br/>");
            return sb.ToString();
        }

        private String KardexAlmacen(Int32 pIDSucursal, String pFechaInicio, String pFechaFin)
        {
            StringBuilder sb = new StringBuilder();
            Int32 Reg;

            BLKardex oBLKardex = new BLKardex();
            IList ListaKardex = oBLKardex.KardexAlmacenListar(pIDSucursal, 0, pFechaInicio, pFechaFin);

            String Sucursal = "-TODOS-";

            if (pIDSucursal > 0)
            {
                BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(pIDSucursal);
                Sucursal = oBESucursal.Nombre.ToUpper();
            }


            sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<tr><td colspan='7'></td></tr>");
            sb.Append("<tr><td align='Left' colspan='23'><h2>KARDEX POR SUCURSAL</h2></td></tr><br/><br/><br/>");
            sb.Append("<tr><td align='Left'><b>FECHA INICIO:</b><td align='Left' colspan='22'>" + pFechaInicio + "</td></tr>");
            sb.Append("<tr><td align='Left'><b>FECHA FIN:</b><td align='Left' colspan='22'>" + pFechaFin + "</td></tr>");
            sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='22'>" + Sucursal + "</td></tr>");
            sb.Append("</table><br/>");

            sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
            sb.Append("<tr style='background-color:#FFBF00;font-weight:bold;'>");

            sb.Append("<td align='center' rowspan='1' width='5%'>Código Producto</td>");
            sb.Append("<td rowspan='1' width='10%'>Nombre Producto</td>");
            sb.Append("<td rowspan='1' width='10%'>Cantidad Entrada</td>");
            sb.Append("<td rowspan='1' width='10%'>Cantidad Salida</td>");
            sb.Append("<td rowspan='1' width='10%'>Cantidad Saldo</td>");
            sb.Append("</tr>");
            Reg = 0;
            foreach (BEKardexDetalle oBEc in ListaKardex)
            {
                Reg = Reg + 1;
                sb.Append("<tr><td align='center'>" + oBEc.CodigoProducto + "</td>");  // correlativo 
                sb.Append("<td>" + oBEc.NombreProducto + "</td>");
                sb.Append("<td>" + oBEc.EntradaCantidad.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.SalidaCantidad.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.Saldo.ToString("N") + "</td>");                
                sb.Append("</tr>");
            }
            sb.Append("</table><br/><br/>");
            return sb.ToString();
        }

    }
}
