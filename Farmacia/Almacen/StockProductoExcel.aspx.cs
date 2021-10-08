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
    public partial class StockProductoExcel : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            String v_TipoReporte = Request.QueryString["pTipoReporte"];

            String v_Filtro = "";
            String v_TipoConsulta = "";
            String v_FechaInicio = "";
            String v_IDProducto = "0";
            String v_FechaFin = "";
            Int32 v_IDSucursal = 0;

            if (v_TipoReporte == "STOCK")
            {

                v_IDSucursal = Int32.Parse(Request.QueryString["pIDSucursal"]);
                v_Filtro = Request.QueryString["pFiltro"];
                v_TipoConsulta = Request.QueryString["pTipoConsulta"];                
            }

            String nameReport;
            String nombre;

            String style = @"<style> .textmode { } </style>";
            Int32 v_IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
            HttpResponse response = Response;

            switch (v_TipoReporte)
            {
                case "STOCK":
                    nameReport = "STOCK_PRODUCTO_POR_ALMACEN" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    nombre = nameReport + ".xls";
                    response.Clear();
                    response.Buffer = true;
                    response.ContentType = "application/vnd.ms-excel";
                    response.AddHeader("Content-Disposition", "attachment;filename=" + nombre);
                    response.Charset = "UTF-8";
                    response.ContentEncoding = Encoding.GetEncoding("Windows-1252");
                    response.Write(StockProductoxSucursal(v_IDSucursal, v_Filtro, v_TipoConsulta));
                    response.End();
                    return;

            }

        }

        private String StockProductoxSucursal(Int32 pIDSucursal, String pFiltro, String pTipoConsulta)
        {

            StringBuilder sb = new StringBuilder();
            Int32 Reg;

            BLProducto oBL = new BLProducto();
            IList Lista = oBL.StockProductoxSucursalV2Listar(pIDSucursal, pFiltro, pTipoConsulta);

            String Sucursal = "-TODOS-";            

            if (pIDSucursal > 0)
            {
                BESucursal oBESucursal = new BLSucursal().SucursalSeleccionar(pIDSucursal);
                Sucursal = oBESucursal.Nombre.ToUpper();
            }

            sb.Append("<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<tr><td colspan='7'></td></tr>");
            sb.Append("<tr><td align='Left' colspan='23'><h2>STOCK DE PRODUCTOS POR SUCURSAL</h2></td></tr><br/><br/><br/>");            
            sb.Append("<tr><td align='Left'><b>SUCURSAL:</b></td><td align='Left' colspan='22'>" + Sucursal + "</td></tr>");            
            sb.Append("</table><br/>");

            sb.Append("<table class='tabla_kardex' width='100%' border='1'>");
            sb.Append("<tr style='background-color:#FFBF00;font-weight:bold;'>");

            sb.Append("<td align='center' rowspan='1' width='5%'>Sucursal</td>");
            sb.Append("<td rowspan='1' width='10%'>Código Producto</td>");
            sb.Append("<td rowspan='1' width='10%'>Nombre Producto</td>");
            sb.Append("<td rowspan='1' width='10%'>Unidad Medida</td>");
            sb.Append("<td rowspan='1' width='10%'>Stock Minimo</td>");
            sb.Append("<td rowspan='1' width='10%'>Stock Actual</td>");            
            sb.Append("</tr>");
            Reg = 0;
            foreach (BEProducto oBEc in Lista)
            {
                Reg = Reg + 1;
                sb.Append("<tr><td align='center'>" + oBEc.Sucursal + "</td>");  // correlativo 
                sb.Append("<td>" + oBEc.Codigo + "</td>");
                sb.Append("<td>" + oBEc.Producto + "</td>");
                sb.Append("<td>" + oBEc.UnidadMedida + "</td>");
                sb.Append("<td>" + oBEc.StockMinimo.ToString("N") + "</td>");
                sb.Append("<td>" + oBEc.StockActual.ToString("N") + "</td>");                
                sb.Append("</tr>");
            }
            sb.Append("</table><br/><br/>");
            return sb.ToString();
        }        

    }
}
