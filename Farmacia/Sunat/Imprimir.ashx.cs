using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.Facturacion;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace Farmacia.Sunat
{
	public class Imprimir : IHttpHandler
	{
		ReportDocument reporte = new ReportDocument();
		public void ProcessRequest(HttpContext context)
		{
			Int32 Codigo = Int32.Parse(context.Request.QueryString["ID"]);
			String Tipo = context.Request.QueryString["Tipo"];

			if (Codigo != 0)
			{
				if (Tipo.Equals("1"))
				{
					reporte.Load(context.Server.MapPath("~/Sunat/Impresion/FacturaBoleta.rpt"));
				}
				else {
					if (Tipo.Equals("2"))
					{
						reporte.Load(context.Server.MapPath("~/Sunat/Impresion/NotaCreditoDebito.rpt"));
					}
					else {
						reporte.Load(context.Server.MapPath("~/Sunat/Impresion/Ejemplo.rpt"));
					}

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


				String NombrePdf = "";
				if (Tipo.Equals("1"))
				{
					BEFacturaBoleta oBEFB = new BLFacturaBoleta().FacturaBoletaSeleccionar(Codigo);
					reporte.SetParameterValue("@IDFacturaBoleta", Codigo);
					NombrePdf = oBEFB.SerieNumero;
				}
				else {
					if (Tipo.Equals("2"))
					{
						BECreditoDebito oBECD = new BLCreditoDebito().CreditoDebitoSeleccionar(Codigo);
						reporte.SetParameterValue("@Codigo", Codigo);
						NombrePdf = oBECD.SerieNumero;
					}
				}

				context.Response.Buffer = true;
				byte[] byteArray = null;
				using (System.IO.Stream oStream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
				{
					byteArray = new byte[oStream.Length];
					oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
					context.Response.ClearContent();
					context.Response.ContentType = "application/pdf; name=" + NombrePdf + ".pdf";
					context.Response.AddHeader("Content-Disposition", "inline; filename=" + NombrePdf + ".pdf");
					context.Response.AddHeader("content-length", byteArray.Length.ToString());
					context.Response.BinaryWrite(byteArray);

				}

				context.Response.End();
				context.Response.Close();

				reporte.Close();
				reporte.Dispose();



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

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

	}
}