
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Facturacion.BE.General;
using Facturacion.Sunat.Comun.Dto.Intercambio;
using Facturacion.Sunat.Comun.Dto.Modelos;
using Facturacion.Sunat.Controllers;
using Ionic.Zip;
using Farmacia.App_Class;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZXing;

namespace Farmacia.Sunat
{
	public partial class NotaDebito : PageBase
    {
        String RutaArchivos = ConfigurationManager.AppSettings["RutaArchivos"].ToString();

        #region Load
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
                CargarDDL(ddlIDSucursal, new BLSucursal().SucursalxEmpresaListar(IDEmpresa()), "IDSucursal", "Sucursal", false);
				CargarDDL(ddlIDEstadoSunat, new BLEstado().EstadoListar("SUNAT"), "Codigo", "Nombre", true, Constantes.TODOS);
				ddlIDSucursal.SelectedValue = IDSucursal().ToString();
                Listar();
            }
        }
        #endregion

        #region Listar 
        private void Listar()
        {
            BLCreditoDebito oBL = new BLCreditoDebito();
            gvLista.DataSource = oBL.CreditoDebitoListar(Int32.Parse(ddlIDSucursal.SelectedValue), "08", txtFiltro.Text, Int32.Parse(ddlIDEstadoSunat.SelectedValue));
            gvLista.DataBind();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            Listar();
        }

        protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            Int32 pCodigo = 0;

            switch (e.CommandName)
            { 
                case "Enviar":
                    pCodigo = Int32.Parse(e.CommandArgument.ToString());
                    FirmarDocumentoCreditoDebito(pCodigo, "08");
                    EnviarDocumentoCreditoDebitoSunat(pCodigo); 
                    GeneraCodigoBarra(pCodigo);
					GenerarDocumentoElectronico_PDF(pCodigo);
					GenerarArchivo_ZIP(pCodigo);
					break;
                case "DescargaXML":
                    pCodigo = Int32.Parse(e.CommandArgument.ToString());
                    GenerarDocumentoElectronico_PDF(pCodigo);
                    GenerarArchivo_ZIP(pCodigo);
                    GeneraCodigoBarra(pCodigo);
                    DescargarCDR(pCodigo);
                    break;
                case "EnviarCorreo":
					hdfIDCreditoDebito.Value = e.CommandArgument.ToString();
					EnvioMail();
                    break;
                case "Anular":
                    pCodigo = Int32.Parse(e.CommandArgument.ToString());
                    BERetornoTran oBERetorno = new BERetornoTran();

                    oBERetorno = new BLCreditoDebito().CreditoDebitoAnular(pCodigo, IDUsuario());

                    if (oBERetorno.Retorno == "1")
                    {
                        msgbox(TipoMsgBox.confirmation, "Facturacion", "El documento se ha anulado con éxito");
                    }
                    Listar();
                    break;
            }
             

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }
		#endregion

		#region Envio Mail
		private void EnvioMail()
		{
			Int32 pCodigo = Int32.Parse(hdfIDCreditoDebito.Value);

			BEEmailGenerico pBEEmailGenerico = new BEEmailGenerico();
			ArrayList pDataEmailPara = new ArrayList();
			ArrayList pDataEmailCC = new ArrayList();
			ArrayList pDataAdjunto = new ArrayList();

			BECreditoDebito oBE = new BLCreditoDebito().CreditoDebitoSeleccionar(pCodigo);

			pBEEmailGenerico = new BEEmailGenerico();
			pBEEmailGenerico.Nombre = oBE.NombrePARA;
			pBEEmailGenerico.Email = oBE.CorreoPARA;
			pDataEmailPara.Add(pBEEmailGenerico);

			pBEEmailGenerico = new BEEmailGenerico();
			pBEEmailGenerico.Nombre = oBE.NombreCC;
			pBEEmailGenerico.Email = oBE.CorreoCC;
			pDataEmailCC.Add(pBEEmailGenerico);

			//Envio de Correo con Plantilla XML----------------------------------------------------
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(Server.MapPath("~/Plantilla/CorreoDocumento.xml"));
			XmlNodeList XMLCorreo = xDoc.GetElementsByTagName("correo");
			String Asunto = (((XmlElement)XMLCorreo[0]).GetElementsByTagName("asunto"))[0].InnerText;
			String Mensaje = (((XmlElement)XMLCorreo[0]).GetElementsByTagName("cuerpo"))[0].InnerText;

			Asunto = String.Format(Asunto, oBE.SerieNumero, oBE.RazonSocialAdquiriente);
			Mensaje = String.Format(Mensaje, oBE.NumeroDocumentoAdquiriente, oBE.RazonSocialAdquiriente, oBE.TipoComprobante, oBE.SerieNumero);

			pBEEmailGenerico = new BEEmailGenerico();
			pBEEmailGenerico.Nombre = oBE.NombreDocumento;
			pBEEmailGenerico.Archivo = oBE.RutaDocumento + "" + oBE.NombreDocumento;
			pDataAdjunto.Add(pBEEmailGenerico);

			pBEEmailGenerico = new BEEmailGenerico();
			pBEEmailGenerico.Nombre = oBE.NombreArchivoZip;
			pBEEmailGenerico.Archivo = oBE.RutaArchivoZip + "" + oBE.NombreArchivoZip;
			pDataAdjunto.Add(pBEEmailGenerico);

			EnviarMail(this.Page, pCodigo.ToString(), "", Asunto, Mensaje, pDataEmailPara, pDataEmailCC, pDataAdjunto);
		}

		public void EnviarMail(Page pPage, String pIDGenerico, String pCarpeta, String pAsunto, String pMensaje, ArrayList pListaEmailPara, ArrayList pListaEmailCC, ArrayList pListaArchivo)
		{
			Session["IDGenerico"] = pIDGenerico;
			Session["TipoComprobante"] = "FB";
			Session["Carpeta"] = (pCarpeta == "" ? "Adjunto/" : pCarpeta);
			Session["Asunto"] = pAsunto;
			Session["Mensaje"] = pMensaje;
			Session["ListaEmailPara"] = pListaEmailPara;
			Session["ListaEmailCC"] = pListaEmailCC;
			Session["ListaArchivo"] = pListaArchivo;

			registrarScript("EnviarMailAbrir();");
		}
		#endregion
		 
		#region Firmar

		private void FirmarDocumentoCreditoDebito(Int32 pCodigo, String pTipoComprobante)
        { 
            BECreditoDebito  oBE = new BLCreditoDebito().CreditoDebitoSeleccionar(pCodigo);
            
            var documento = new DocumentoElectronico
            {
                Emisor = new Contribuyente
                {
                    NroDocumento = oBE.NumeroDocumentoEmisor,
                    TipoDocumento = oBE.TipoDocumentoEmisor.ToString(),
                    Direccion = oBE.DireccionEmisor,
                    Urbanizacion = oBE.UrbanizacionEmisor,
                    Departamento = oBE.DepartamentoEmisor,
                    Provincia = oBE.ProvinciaEmisor,
                    Distrito = oBE.DistritoEmisor,
                    NombreComercial = oBE.NombreComercialEmisor,
                    NombreLegal = oBE.RazonSocialEmisor,
                    Ubigeo = oBE.UbigeoEmisor
                },
                Receptor = new Contribuyente
                {
                    NroDocumento = oBE.NumeroDocumentoAdquiriente,
                    TipoDocumento = oBE.TipoDocumentoAdquiriente,
                    NombreLegal = oBE.RazonSocialAdquiriente
                },
                IdDocumento = oBE.SerieNumero,
                FechaEmision = oBE.FechaEmision, //DateTime.Today.AddDays(-5).ToString("yyyy-MM-dd"),
                Moneda = oBE.TipoMoneda,
                TipoDocumento = oBE.TipoDocumento,
                MontoEnLetras = oBE.MontoTotalLetra,
                CalculoIgv = 0.18m,
                CalculoIsc = 0.10m,
                CalculoDetraccion = 0.04m,
                TotalIgv = oBE.TotalIgvItems,
                TotalVenta = oBE.TotalVenta,
                Gravadas = oBE.TotalVenta_NetoGravada,
                Exoneradas = oBE.TotalVenta_NetoExonerada,
                Gratuitas = oBE.TotalVenta_NetoGratuita,
                // DescuentoGlobal = oBE.DescuentoGlobal,
                Inafectas = oBE.TotalVenta_NetoInafecta,
                Items = new List<DetalleDocumento>(),
                Discrepancias = new List<Discrepancia>()


            };

            IList lista = new BLCreditoDebito().CreditoDebitoDetalleListar(pCodigo);

            foreach (BECreditoDebitoDetalle oBEDetalle in lista)
            {

                documento.Items.Add(new DetalleDocumento
                {
                    Id = oBEDetalle.NumeroOrdenItem,
                    Cantidad = oBEDetalle.Cantidad,
                    PrecioUnitario = oBEDetalle.ImporteUniSinImpuesto,
                    PrecioReferencial = oBEDetalle.ImporteReferencial,
                    TipoPrecio = oBEDetalle.TipoPrecio,
                    CodigoItem = oBEDetalle.CodigoProducto,
                    Descripcion = oBEDetalle.DescripcionProducto,
                    UnidadMedida = oBEDetalle.IDUnidadMedida,
                    Impuesto = oBEDetalle.ImporteIgv,
                    TipoImpuesto = oBEDetalle.IDTipoImpuesto, // Gravada
                    TotalVenta = oBEDetalle.ImporteTotalSinImpuesto,
                    Suma = oBE.TotalVenta

                });

				
            }

            documento.Discrepancias.Add(new Discrepancia
            {
                NroReferencia = oBE.SerieNumeroAfectado,
                Descripcion = oBE.MotivoDocumento,
                Tipo = oBE.IDTipoMotivo 
                

            });

            documento.Relacionados.Add(new DocumentoRelacionado
            {
                NroDocumento = oBE.SerieNumeroAfectado,
                TipoDocumento = oBE.TipoDocumentoAfectado

            });

            if (pTipoComprobante == "08")
            {
                BERetornoTran oBERetorno = new BERetornoTran();
                var controller_debito = new GenerarNotaDebitoController();

                var response_debito = controller_debito.Post(documento);

                if (response_debito.Exito)
                {
                    oBERetorno = new BLFacturacion().ActualizarFacturaTramaFirmado(pCodigo, response_debito.TramaXmlSinFirma, "", "", "ND_TSF", IDUsuario());

                    var firmado_debito = new FirmarController();
                    var firmadoRequest_debito = new FirmadoRequest
                    {
                        TramaXmlSinFirma = response_debito.TramaXmlSinFirma,
                        CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(ConfigurationManager.AppSettings["RutaArchivos"].ToString() + oBE.Certificado)),
                        PasswordCertificado = oBE.ClaveCertificado,
                        UnSoloNodoExtension = false
                    };

                    var response_firmado = firmado_debito.Post(firmadoRequest_debito);

                    if (response_firmado.Exito)
                    {
                        oBERetorno = new BLFacturacion().ActualizarFacturaTramaFirmado(pCodigo, response_firmado.TramaXmlFirmado, response_firmado.ResumenFirma, response_firmado.ValorFirma, "ND_TF", IDUsuario());

                        if (oBERetorno.Retorno == "1")
                        {
                            msgbox(TipoMsgBox.confirmation, "Facturacion", "El documento se ha Firmado con éxito"); 
                        }
                    }
                    else {
                        oBERetorno = new BLFacturacion().ActualizarErrorXML(pCodigo, response_firmado.MensajeError, "CD_EXMLF", IDUsuario());
                        msgbox(TipoMsgBox.warning, "Facturacion", response_firmado.MensajeError);
                    } 
                }
                else {
                    oBERetorno = new BLFacturacion().ActualizarErrorXML(pCodigo, response_debito.MensajeError, "CD_EXML", IDUsuario());
                    msgbox(TipoMsgBox.warning, "Facturacion", response_debito.MensajeError);

                } 
            }

            Listar();
            upLista.Update();
        }
         
        private void EnviarDocumentoCreditoDebitoSunat(Int32 pCodigo)
        { 
            BECreditoDebito  oBE = new BLCreditoDebito().CreditoDebitoSeleccionar(pCodigo);
            BERetornoTran oBERetorno = new BERetornoTran();
			BEEmpresa oBEEmisor = new BLEmpresa().EmpresaSeleccionar(Int32.Parse(Session["IDEmpresa"].ToString()));           
            Console.WriteLine("Enviando a SUNAT....");

            var sendBill = new EnviarDocumentoRequest
            { 
                Ruc = oBE.NumeroDocumentoEmisor,
                UsuarioSol = oBEEmisor.UsuarioSol,
                ClaveSol = oBEEmisor.ClaveSol,
                EndPointUrl = oBEEmisor.EndPointUrl,
                IdDocumento = oBE.SerieNumero,
                TipoDocumento = oBE.TipoDocumento,
                TramaXmlFirmado = oBE.TramaXML_Firmado
            };

            var enviar_sunat = new EnviarDocumentoController();

            var response_sunat = enviar_sunat.Post(sendBill);

            if (response_sunat.Exito)
            {
                oBERetorno = new BLFacturacion().ActualizarDocumentoEnviadoSunat(pCodigo, response_sunat.TramaZipCdr, response_sunat.MensajeRespuesta, "NC_ACEP", IDUsuario());

                if (oBERetorno.Retorno == "1")
                {
                    msgbox(TipoMsgBox.confirmation, "Facturacion", "El documento se ha enviado a sunat con éxito"); 
                } 
            }
            else { 
                oBERetorno = new BLFacturacion().ActualizarErrorXML(pCodigo, response_sunat.MensajeError, "CD_ERROR", IDUsuario());
                msgbox(TipoMsgBox.warning, "Facturacion", response_sunat.MensajeError);
            }
            Listar();
            upLista.Update();


        }

        #endregion

        #region GeneraDocumento

        private void GenerarDocumentoElectronico_PDF(Int32 pIDCodigo)
        {
            Int32 pRetorno = -1;

            try
            {
                BECreditoDebito oBE = new BLCreditoDebito().CreditoDebitoSeleccionar(pIDCodigo);

                String pArchivoNombre = oBE.NumeroDocumentoEmisor + "-" + oBE.TipoDocumento + "-" + oBE.SerieNumero + ".pdf";
                String pArchivoRuta = oBE.NumeroDocumentoEmisor + "\\" + oBE.NumeroDocumentoAdquiriente + "\\" + oBE.TipoDocumento + "\\" + oBE.SerieNumero + "\\";

                String archivoRutaNombre = @Path.Combine(pArchivoRuta, pArchivoNombre);
                String archivoRutaServidor = RutaArchivos + "" + pArchivoRuta;

                if (!System.IO.Directory.Exists(archivoRutaServidor))
                    Directory.CreateDirectory(archivoRutaServidor);

                ReportDocument reporte = new ReportDocument();
                reporte.Load(Server.MapPath("~/Sunat/Impresion/NotaCreditoDebito.rpt"));
                SetConexion(reporte, ConexionInfo());
                reporte.Refresh();
                reporte.SetParameterValue("@Codigo", pIDCodigo);
                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, @Path.Combine(archivoRutaServidor, pArchivoNombre));
                reporte.Close();
                reporte.Dispose();
                reporte = null;

                //Guarda el nombre del archivo
                BLCreditoDebito oBL = new BLCreditoDebito();
                BECreditoDebito oBEx = new BECreditoDebito();
                BERetornoTran oBERetorno = new BERetornoTran();


                oBEx.IDCreditoDebito = pIDCodigo;
                oBEx.RutaArchivo = pArchivoRuta;
                oBEx.NombreArchivo = pArchivoNombre;
                oBEx.Accion = "PDF";
                oBEx.IDUsuario = IDUsuario();
                oBERetorno = oBL.CreditoDebitoArchivoActualizar(oBEx);
            }
            catch (Exception ex)
            {
                pRetorno = LogSistema("GenerarDocumentoElectronico_PDF()", "", ex.Message);
            }

        }

        private void GenerarArchivo_ZIP(Int32 pIDCodigo)
        {

            BLCreditoDebito oBL = new BLCreditoDebito();
            BECreditoDebito oBE = oBL.CreditoDebitoSeleccionar(pIDCodigo);

            //GENERAR EL XML Y ELCDR ENTRO DEL MISMO ZIP
            var memZIP_CDR = new MemoryStream(Convert.FromBase64String(oBE.TramaZIP_CDR));
            var memXML_Firmado = new MemoryStream(Convert.FromBase64String(oBE.TramaXML_Firmado));

            String NombreArchivo = oBE.NumeroDocumentoEmisor + "-" + oBE.TipoDocumento + "-" + oBE.SerieNumero;
            String NombreArchivoRespuesta = "R-" + oBE.NumeroDocumentoEmisor + "-" + oBE.TipoDocumento + "-" + oBE.SerieNumero;
            String NombreArchivoZIP = NombreArchivo + ".zip";
            String NombreArchivoZIP_Respuesta = NombreArchivoRespuesta + ".zip";
            String NombreArchivoXML = NombreArchivo + ".xml";

            String pArchivoRuta = oBE.NumeroDocumentoEmisor + "\\" + oBE.NumeroDocumentoAdquiriente + "\\" + oBE.TipoDocumento + "\\" + oBE.SerieNumero + "\\";
            String archivoRutaServidor = RutaArchivos + "" + pArchivoRuta;

            if (!System.IO.Directory.Exists(archivoRutaServidor))
                Directory.CreateDirectory(archivoRutaServidor);

            using (var fileZip = new ZipFile(NombreArchivoZIP))
            {
                fileZip.AddEntry(NombreArchivoXML, memXML_Firmado);
                fileZip.AddEntry(NombreArchivoZIP_Respuesta, memZIP_CDR);
                fileZip.Save(archivoRutaServidor + NombreArchivoZIP);
            }

            // Liberamos memoria RAM.
            memZIP_CDR.Close();
            memXML_Firmado.Close();

            //Guarda el nombre del archivo 
            BECreditoDebito oBEx = new BECreditoDebito();
            BERetornoTran oBERetorno = new BERetornoTran();

            oBEx.IDCreditoDebito = pIDCodigo;
            oBEx.RutaArchivo = pArchivoRuta;
            oBEx.NombreArchivo = NombreArchivoZIP;
            oBEx.Accion = "ZIP";
            oBEx.IDUsuario = IDUsuario();
            oBERetorno = new BLCreditoDebito().CreditoDebitoArchivoActualizar(oBEx);

        }

        private void GeneraCodigoBarra(Int32 pCodigo)
        {
            try
            {
                BECreditoDebito oBE = new BLCreditoDebito().CreditoDebitoSeleccionar(pCodigo);
                /*GenerarCodigo*/
                var barcodeWriter = new BarcodeWriter();

                // set the barcode format
                barcodeWriter.Options.Width = 750;
                barcodeWriter.Options.Height = 80;
                barcodeWriter.Options.Margin = 1;
                barcodeWriter.Format = BarcodeFormat.PDF_417;

                String CodigoQR = oBE.IDCreditoDebito.ToString() + oBE.SerieNumero + DateTime.Now.ToString("yyyyMMdd") + ".jpg";
                String RutaNombreArchivo = oBE.NumeroDocumentoEmisor + "\\" + oBE.NumeroDocumentoAdquiriente + "\\" + oBE.TipoDocumento + "\\" + oBE.SerieNumero + "\\";
                String RutaNombre = CodigoQR;
                String RutaArchivosx = RutaNombreArchivo + RutaNombre;
                String RutaArchivosy = RutaArchivos + RutaNombreArchivo;
                String RutaArchivosz = RutaArchivos + RutaArchivosx;
                //                String RutaArchivosPortalx = RutaArchivosPortal + oBE.NumeroDocumentoEmisor +"\\" + oBE.NumeroDocumentoAdquiriente +"\\" + oBE.TipoDocumentoAdquiriente + "\\" + oBE.SerieNumero + "\\" + RutaNombre;
                BERetornoTran oBERetorno = new BERetornoTran();
                oBERetorno = new BLFacturacion().ActualizarCodigoQR(pCodigo, RutaArchivosx, "CD_QR", IDUsuario());
                if (!Directory.Exists(RutaArchivosy))
                {
                    Console.WriteLine("That path NOT exists already.");
                    System.IO.Directory.CreateDirectory(RutaArchivosy);
                }
                if (oBERetorno.Retorno == "1")
                {
                    // write text and generate a 2-D barcode as a bitmap
                    barcodeWriter
                        .Write(oBE.CodigoQR)
                        .Save(@RutaArchivosz);
                }
            }
            catch (Exception ex)
            {
                RegistrarLogSistema("GeneraCodigoBarra()", ex.ToString(), true);
            }

        }

        private void DescargarCDR(Int32 pCodigo)
        {
            registrarScript("Descargar('" + pCodigo + "');");
        }
		 
        #endregion

        #region Crystal Report SetConnection
        private CrystalDecisions.Shared.ConnectionInfo ConexionInfo()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["BD"].ConnectionString;
            System.Data.SqlClient.SqlConnectionStringBuilder SConn = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
            CrystalDecisions.Shared.ConnectionInfo CnInfo = new CrystalDecisions.Shared.ConnectionInfo();
            CnInfo.ServerName = SConn.DataSource;
            CnInfo.DatabaseName = SConn.InitialCatalog;
            CnInfo.UserID = SConn.UserID;
            CnInfo.Password = SConn.Password;
            return CnInfo;
        }

        private void SetConexion(CrystalDecisions.CrystalReports.Engine.ReportDocument rd, CrystalDecisions.Shared.ConnectionInfo ci)
        {
            foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in rd.Database.Tables)
            {
                CrystalDecisions.Shared.TableLogOnInfo logon = tbl.LogOnInfo;
                logon.ConnectionInfo = ci;
                tbl.ApplyLogOnInfo(logon);
                tbl.Location = tbl.Location;
            }
            if (!rd.IsSubreport)
            {
                foreach (CrystalDecisions.CrystalReports.Engine.ReportDocument sd in rd.Subreports)
                {
                    SetConexion(sd, ci);
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
        #endregion 

    }
}