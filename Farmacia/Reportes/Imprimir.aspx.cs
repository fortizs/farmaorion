using System;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Data.SqlClient;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;

namespace Farmacia.Reportes
{
    public partial class Imprimir : System.Web.UI.Page
    {
        ReportDocument reporte = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //    try
                //    {
                Int32 IDSucursal = 0;
                Int32 IDProveedor = 0;
                Int32 IDMedioPago = 0;
                Int32 IDCliente = 0;  
                Int32 IDColaborador = 0;
                Int32 IDCategoria = 0;
                Int32 IDCaja = 0;
                Int32 IDVenta = 0;
                Int32 IDCajaResumen = 0;
                String FechaInicio = "";
                String FiltroProducto = "";
                String Filtro = "";
                String FechaFin = "";
                String FiltroVenta = "";
                String FiltroCantidad = "0";
                String TipoMovimiento = ""; 
                String Tipo = Request.QueryString["Tipo"];

                if (Tipo.Equals("1"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    IDProveedor = Int32.Parse(Request.QueryString["IDProveedor"]);
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                }
                if (Tipo.Equals("2"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    IDMedioPago = Int32.Parse(Request.QueryString["IDMedioPago"]);
                    IDCliente = Int32.Parse(Request.QueryString["IDCliente"]);
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                }

                if (Tipo.Equals("3"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    IDColaborador = Int32.Parse(Request.QueryString["IDColaborador"]); 
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                    FiltroVenta = Request.QueryString["Filtro"];
                }

                if (Tipo.Equals("4"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    IDCaja = Int32.Parse(Request.QueryString["IDCaja"]);
                    IDColaborador = Int32.Parse(Request.QueryString["IDColaborador"]);
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"]; 
                }

                if (Tipo.Equals("5"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    IDCategoria = Int32.Parse(Request.QueryString["IDCategoria"]);
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                    FiltroCantidad = Request.QueryString["CantidadFiltro"];
                }

                if (Tipo.Equals("6"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    IDProveedor = Int32.Parse(Request.QueryString["IDProveedor"]);
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"]; 
                }

                if (Tipo.Equals("7"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    TipoMovimiento = Request.QueryString["TipoMovimiento"];
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                }

                if (Tipo.Equals("8"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    TipoMovimiento = Request.QueryString["TipoMovimiento"];
                    IDCaja = Int32.Parse(Request.QueryString["IDCaja"]); 
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                }
                if (Tipo.Equals("9"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    IDCliente = Int32.Parse(Request.QueryString["IDCliente"]);
                    IDColaborador = Int32.Parse(Request.QueryString["IDColaborador"]);
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                }

                if (Tipo.Equals("10"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]); 
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                }
                if (Tipo.Equals("11"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                    IDCliente = Int32.Parse(Request.QueryString["IDCliente"]);
                }

                if (Tipo.Equals("12"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    IDCategoria = Int32.Parse(Request.QueryString["IDCategoria"]);
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                    FiltroProducto = Request.QueryString["FiltroProducto"];
                }

                if (Tipo.Equals("13"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]); 
                    Filtro = Request.QueryString["Filtro"];
                }

                if (Tipo.Equals("14"))
                {
                    IDSucursal = Int32.Parse(Request.QueryString["IDSucursal"]);
                    IDCliente = Int32.Parse(Request.QueryString["IDCliente"]);
                    FechaInicio = Request.QueryString["FechaInicio"];
                    FechaFin = Request.QueryString["FechaFin"];
                }

                if (Tipo != null)
                {
                    if (Tipo.Equals("1"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteCompras.rpt"));
                    }
                    if (Tipo.Equals("2"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteCobranza.rpt"));
                    }
                    if (Tipo.Equals("3"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteVentas.rpt"));
                    }
                    if (Tipo.Equals("4"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteResumenCaja.rpt"));
                    }
                    if (Tipo.Equals("5"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteProductosMasVendidos.rpt"));
                    }
                    if (Tipo.Equals("6"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReportePagosProveedor.rpt"));
                    }
                    if (Tipo.Equals("7"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteMovimientoAlmacen.rpt"));
                    }
                    if (Tipo.Equals("8"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteMovimientoCaja.rpt"));
                    }
                    if (Tipo.Equals("9"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteDetalleVentaProducto.rpt"));
                    }
                    if (Tipo.Equals("10"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteVendedoresTop.rpt"));
                    }
                    if (Tipo.Equals("11"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteClientesTop.rpt"));
                    }
                    if (Tipo.Equals("12"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteProductosDigemid.rpt"));
                    }
                    if (Tipo.Equals("13"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/ReporteStockProductos.rpt"));
                    }

                    if (Tipo.Equals("14"))
                    {
                        reporte.Load(Server.MapPath("~/Reportes/ImprimirReportes/DetalleVentas.rpt"));
                    }
                    String connectionString = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;
                    SqlConnectionStringBuilder SConn = new SqlConnectionStringBuilder(connectionString);
                    ConnectionInfo CnInfo = new ConnectionInfo();
                    CnInfo.ServerName = SConn.DataSource;
                    CnInfo.DatabaseName = SConn.InitialCatalog;
                    CnInfo.UserID = SConn.UserID;
                    CnInfo.Password = SConn.Password;
                    SetConnection(reporte, CnInfo);
                    reporte.Refresh();

                    //reporte de compras
                    if (Tipo.Equals("1"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@IDProveedor", IDProveedor);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                    }
                    //reporte cobranza
                    if (Tipo.Equals("2"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@IDMedioPago", IDMedioPago);
                        reporte.SetParameterValue("@IDCliente", IDCliente);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                    }
                    if (Tipo.Equals("3"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@IDColaborador", IDColaborador); 
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                        reporte.SetParameterValue("@Filtro", FiltroVenta);
                    }
                    if (Tipo.Equals("4"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@IDCaja", IDCaja);
                        reporte.SetParameterValue("@IDColaborador", IDColaborador);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                    }

                    if (Tipo.Equals("5"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@IDCategoria", IDCategoria);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                        reporte.SetParameterValue("@CantidadFiltro", FiltroCantidad);
                    }

                    if (Tipo.Equals("6"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@IDProveedor", IDProveedor);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin); 
                    }
                    if (Tipo.Equals("7"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@TipoMovimiento", TipoMovimiento);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                    }
                    if (Tipo.Equals("8"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@TipoMovimiento", TipoMovimiento);
                        reporte.SetParameterValue("@IDCaja", IDCaja);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                    }

                    if (Tipo.Equals("9"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@IDCliente", IDCliente);
                        reporte.SetParameterValue("@IDColaborador", IDColaborador);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                    }
                    if (Tipo.Equals("10"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal); 
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                    }
                    if (Tipo.Equals("11"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                        reporte.SetParameterValue("@IDCliente", IDCliente);
                    }
                    if (Tipo.Equals("12"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@IDCategoria", IDCategoria);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                        reporte.SetParameterValue("@FiltroProducto", FiltroProducto);
                    }

                    if (Tipo.Equals("13"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@Filtro", Filtro);
                       
                    }

                    if (Tipo.Equals("14"))
                    {
                        reporte.SetParameterValue("@IDSucursal", IDSucursal);
                        reporte.SetParameterValue("@IDCliente", IDCliente);
                        reporte.SetParameterValue("@FechaInicio", FechaInicio);
                        reporte.SetParameterValue("@FechaFin", FechaFin);
                       
                    }

                    Response.Buffer = true; 
                    //En el Server
                    //using (var mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                    //{
                    //	Response.ClearContent();
                    //	// Response.Buffer = true;
                    //	Response.ContentType = "application/pdf";
                    //	Response.AddHeader("content-length", mStream.Length.ToString());
                    //	Response.BinaryWrite(mStream.ToArray());
                    //}
                    // 
                    //En Desarrollo
                    byte[] byteArray = null;
                    using (System.IO.Stream oStream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                    {
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        Response.ClearContent();
                        // Response.Buffer = true;
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-length", byteArray.Length.ToString());
                        Response.BinaryWrite(byteArray);
                    }
                     
                    // Response.Flush();
                    Response.End();
                    Response.Close();
                    /*
						if (!Response.IsClientConnected)
						{

						}
					*/ 
                }
                //}
                //catch (Exception ex)
                //{
                //    lblMensaje.Text = ex.Message;
                //}
                //finally
                //{

                //}
            }
        }

        private void SetConnection(ReportDocument rd, ConnectionInfo ci)
        {
            foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in rd.Database.Tables)
            {
                TableLogOnInfo logon = tbl.LogOnInfo;
                logon.ConnectionInfo = ci;
                tbl.ApplyLogOnInfo(logon);
                tbl.Location = tbl.Location;
            }
            if (!rd.IsSubreport)
            {
                foreach (ReportDocument sd in rd.Subreports)
                {
                    SetConnection(sd, ci);
                }
            }
        }


        protected void Page_UnLoad(object sender, EventArgs e)
        {
            if (reporte != null && reporte.IsLoaded)
            {
                reporte.Close();
                reporte.Dispose();
                reporte = null;
            }
        }



    }
}