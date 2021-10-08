using System;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Data.SqlClient;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;

namespace Farmacia.Ventas
{
	public partial class Imprimir : System.Web.UI.Page
	{
		ReportDocument reporte = new ReportDocument();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				Int32 Codigo = Int32.Parse(Request.QueryString["ID"]);
				  
				if (Codigo != 0)
				{
					BEVenta oBE = new BLVenta().VentaSeleccionar(Codigo);
					String NombrePdf = oBE.SerieNumero;
					 
					switch (oBE.ImpresionVenta)
					{
						case "TK":
							reporte.Load(Server.MapPath("~/Ventas/Impresion/TicketFarmaOrion.rpt"));
							break;
						case "A4":
							reporte.Load(Server.MapPath("~/Ventas/Impresion/A4.rpt"));
							break;
						case "A4Moto":
							reporte.Load(Server.MapPath("~/Ventas/Impresion/A4Moto.rpt"));
							break;
						default:
							break;
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
					reporte.SetParameterValue("@IDVenta", Codigo); 
					Response.Buffer = true; 
					//En Desarrollo
					byte[] byteArray = null;
					using (System.IO.Stream oStream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
					{
						byteArray = new byte[oStream.Length];
						oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
						Response.ClearContent();
						// Response.Buffer = true;
					    Response.ContentType = "application/pdf; name=" + NombrePdf + ".pdf";
					    Response.AddHeader("Content-Disposition", "inline; filename=" + NombrePdf + ".pdf");
						//Response.ContentType = "application/pdf";
						//Response.AddHeader("content-length", byteArray.Length.ToString());
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