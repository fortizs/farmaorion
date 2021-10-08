using Farmacia.App_Class.BE;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.Facturacion;
using System;
using System.Configuration;

namespace Farmacia.Sunat
{
	public partial class Descargar :  PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String pArchivoRuta = "";
            String archivoRutaServidor = "";
            String NombreArchivoZip = "";
            String pRutaArchivo = "";
            String pNombreArchivo = "";

           String RutaArchivos = ConfigurationManager.AppSettings["RutaArchivos"].ToString();  
            String pCodigo = Request.QueryString["pCodigo"].ToString();
            String pTipo = Request.QueryString["pTipo"].ToString();

            if (pTipo.Equals("CB")) {
                  pRutaArchivo = Request.QueryString["pRutaArchivo"].ToString();
                  pNombreArchivo = Request.QueryString["pNombreArchivo"].ToString();
            }

            archivoRutaServidor = RutaArchivos + pRutaArchivo;
            NombreArchivoZip = pNombreArchivo;

            BEFacturaBoleta oBE;
            BECreditoDebito oBEx;

           

            if (pTipo.Equals("FB"))
            { 
                  oBE = new BLFacturaBoleta().FacturaBoletaSeleccionar(Int32.Parse(pCodigo));
                pArchivoRuta = oBE.NumeroDocumentoEmisor + "\\" + oBE.NumeroDocumentoAdquiriente + "\\" + oBE.TipoDocumento + "\\" + oBE.SerieNumero + "\\";
                archivoRutaServidor = RutaArchivos + "" + pArchivoRuta;
                NombreArchivoZip = oBE.NombreArchivoZip;
            }
            else {
                if (pTipo.Equals("CD")) { 
                    oBEx = new BLCreditoDebito().CreditoDebitoSeleccionar(Int32.Parse(pCodigo));
                    pArchivoRuta = oBEx.NumeroDocumentoEmisor + "\\" + oBEx.NumeroDocumentoAdquiriente + "\\" + oBEx.TipoDocumento + "\\" + oBEx.SerieNumero + "\\";
                    archivoRutaServidor = RutaArchivos + "" + pArchivoRuta;
                    NombreArchivoZip = oBEx.NombreArchivoZip;
                }
            }
             
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + NombreArchivoZip);
            Response.WriteFile(archivoRutaServidor + NombreArchivoZip);
            Response.End(); 
        }
    }
}