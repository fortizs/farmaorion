
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Facturacion.BE.General;
using Facturacion.Sunat.Comun.Dto.Intercambio;
using Facturacion.Sunat.Comun.Dto.Modelos;
using Facturacion.Sunat.Controllers;
using Facturacion.Sunat.Servicio;
using Farmacia.App_Class;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.Facturacion;
using Farmacia.App_Class.BL.General;
using Ionic.Zip; 
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
	public partial class FacturaElectronica : PageBase
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
            BLFacturaBoleta oBL  = new BLFacturaBoleta();
            gvLista.DataSource = oBL.FacturaBoletaListar(Int32.Parse(ddlIDSucursal.SelectedValue), "01", txtFiltro.Text.Trim(), Int32.Parse(ddlIDEstadoSunat.SelectedValue));
            gvLista.DataBind();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.SelectedIndex = -1;
            Listar();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Int32 pCodigo = 0;
              
            switch (e.CommandName)
            { 
                case "FirmarEnviar":
                    pCodigo = Int32.Parse(e.CommandArgument.ToString());
                    FirmarDocumentoFactura(pCodigo); 
                    EnviarDocumentoSunat(pCodigo);
                    GeneraCodigoBarra(pCodigo);
				    GenerarDocumentoElectronico_PDF(pCodigo);
					GenerarArchivo_ZIP(pCodigo);
					break;
                case "DescargaXML":
                    pCodigo = Int32.Parse(e.CommandArgument.ToString()); 
                    GenerarDocumentoElectronico_PDF(pCodigo);
                    GenerarArchivo_ZIP(pCodigo);
                    DescargarCDR(pCodigo);
                    break;
				case "EnviarCorreo": 
					hdfIDFacturaBoleta.Value = e.CommandArgument.ToString();
					EnvioMail();
					break;
				case "Anular":
                    pCodigo = Int32.Parse(e.CommandArgument.ToString());
                    BERetornoTran oBERetorno = new BERetornoTran();

                    oBERetorno = new BLFacturaBoleta().FacturaBoletaAnular(pCodigo, IDUsuario());

                    if (oBERetorno.Retorno == "1")
                    {
                       msgbox(TipoMsgBox.confirmation, "Facturacion", "El documento se ha anulado con éxito"); 
                    }
                    Listar();
                    break;
            }

        }

		#endregion

		#region Envio Mail
		private void EnvioMail()
		{
			Int32 pCodigo = Int32.Parse(hdfIDFacturaBoleta.Value);

			BEEmailGenerico pBEEmailGenerico = new BEEmailGenerico();
			ArrayList pDataEmailPara = new ArrayList();
			ArrayList pDataEmailCC = new ArrayList();
			ArrayList pDataAdjunto = new ArrayList();

			BEFacturaBoleta oBE = new BLFacturaBoleta().FacturaBoletaSeleccionar(pCodigo);


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

		#region Generar_Firmar_Enviar

		private void FirmarDocumentoFactura(Int32 pCodigo)
        {
            try
            {
                BEFacturaBoleta oBE = new BLFacturaBoleta().FacturaBoletaSeleccionar(pCodigo);
                Int32 IDVenta = oBE.IDVenta;
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

                    TipoOperacion = oBE.TipoOperacion,
                    IdDocumento = oBE.SerieNumero,
                    FechaEmision = oBE.FechaEmision,
					HoraEmision = oBE.HoraEmision,
					FechaVencimiento = oBE.FechaVencimiento,

					Moneda = oBE.TipoMoneda,
                    TipoDocumento = oBE.TipoDocumento,
                    MontoEnLetras = oBE.MontoTotalLetra,
                    CalculoIgv = 0.18m,
                    CalculoIsc = 0.10m,
                    CalculoDetraccion = oBE.CalculoDetraccion,
                    MontoDetraccion = oBE.MontoDetraccion,
                    MonedaAnticipo = oBE.TipoMoneda,
                    MontoAnticipo = oBE.MontoAnticipo,
                    DocAnticipo = oBE.DocAnticipo,
                    TipoDocAnticipo = oBE.TipoDocAnticipo,
                    TotalValorVenta = oBE.TotalVenta_NetoGravada, //Jave
                    TotalIgv = oBE.TotalIgvItems,
                    TotalVenta = oBE.TotalVenta,

                    Gravadas = oBE.TotalVenta_NetoGravada,
                    Exoneradas = oBE.TotalVenta_NetoExonerada,
                    Gratuitas = oBE.TotalVenta_NetoGratuita,
                    DescuentoGlobal = oBE.DescuentoGlobal,
                    Inafectas = oBE.TotalVenta_NetoInafecta,

					CodigoLeyenda = oBE.CodigoLeyenda,
					CantidadItemsDocumentoDetalle = oBE.NroItems.ToString(),
					TotalImpuestos = oBE.TotalIgvItems,
					Items = new List<DetalleDocumento>(),
                    ItemsFormaPago = new List<DetalleFormaPago>()

                };

                IList lista = new BLFacturaBoleta().FacturaBoletaDetalleListar(pCodigo);

                foreach (BEFacturaBoletaDetalle oBEDetalle in lista)
                {
                    documento.Items.Add(new DetalleDocumento
                    {
                        Id = oBEDetalle.NumeroOrdenItem,
                        CodigoItem = oBEDetalle.CodigoProducto,
                        Descripcion = oBEDetalle.DescripcionProducto,
                        PrecioUnitario = oBEDetalle.ImporteUniSinImpuesto, //PrevioValorUnitario
                        PrecioReferencial = oBEDetalle.ImporteUniConImpuesto, //PrecioVentaUnitario con Impuesto
                        TipoPrecio = oBEDetalle.TipoPrecio,
                        Cantidad = oBEDetalle.Cantidad,
                        UnidadMedida = oBEDetalle.IDUnidadMedida,
                        // ImporteDescuento
                        Impuesto = oBEDetalle.ImporteIgv,
                        TipoImpuesto = oBEDetalle.IDTipoImpuesto, // Afectacion IGV
                        ImpuestoSelectivo = 0, //Importe ISC
                        OtroImpuesto = 0,
                        Descuento = 0,
                        TotalVenta = oBEDetalle.ImporteTotalSinImpuesto,
						PorcentajeImpuesto = 18,
                        Carroceria = "",
                        Marca = "",
                        Modelo = "",
                        SerieChasis = "",
                        Motor = "",
                        Color = "",
                        AnioModelo = "0"

                    });
                }              

                IList listaFormaPago = new BLVentaFormaPago().VentaFormaPagoSunatListar(IDVenta);
                Int32 contador = 1;

                foreach (BEVentaFormaPago item in listaFormaPago)
                {

                    documento.ItemsFormaPago.Add(new DetalleFormaPago
                    {
                        //Id = item.ID,
                        Id = "FormaPago",
                        Item = contador,
                        FormaPago = item.FormaPago,
                        ImporteFormaPago = item.ImportePago,
                        FechaPago = item.FechaPago
                    });
                    contador += 1;
                }

                Console.WriteLine("Generando XML....");
                var controller = new GenerarFacturaController();
                var response = controller.Post(documento);
                BERetornoTran oBERetorno = new BERetornoTran();

                if (response.Exito)
                {
                    oBERetorno = new BLFacturaBoleta().FacturaBoletaTramaActualizar(pCodigo, response.TramaXmlSinFirma, "", "", "FB_TSF", IDUsuario());

                    Console.WriteLine("Firmando XML....");
                    var firmado = new FirmarController();
                    var firmadoRequest = new FirmadoRequest
                    {
                        TramaXmlSinFirma = response.TramaXmlSinFirma,
                        CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(ConfigurationManager.AppSettings["RutaArchivos"].ToString() + oBE.Certificado)),
                        PasswordCertificado = oBE.ClaveCertificado,
                        UnSoloNodoExtension = true 
					};

                    var response_firmado = firmado.Post(firmadoRequest);

                    if (response_firmado.Exito)
                    {
                        oBERetorno = new BLFacturaBoleta().FacturaBoletaTramaActualizar(pCodigo, response_firmado.TramaXmlFirmado, response_firmado.ResumenFirma, response_firmado.ValorFirma, "FB_TF", IDUsuario());
				    }
                    else {
                        oBERetorno = new BLFacturacion().ActualizarErrorXML(pCodigo, response_firmado.MensajeError, "FB_ERROR", IDUsuario());
                        msgbox(TipoMsgBox.warning, "Facturacion", response_firmado.MensajeError);
                    }
                }
                else {
                    RegistrarLogSistema("FirmarDocumentoFactura()", response.MensajeError + " "+ response.Pila, true);
                    oBERetorno = new BLFacturacion().ActualizarErrorXML(pCodigo, response.MensajeError, "FB_ERROR", IDUsuario());
                    msgbox(TipoMsgBox.warning, "Facturacion", response.MensajeError);
                } 
                Listar();
                upLista.Update();
            }
            catch (Exception ex)
            {
                RegistrarLogSistema("FirmarDocumentoFactura()", ex.ToString(), true);
            }

        }

        private void EnviarDocumentoSunat(Int32 pCodigo)
        { 
            BEFacturaBoleta oBE = new BLFacturaBoleta().FacturaBoletaSeleccionar(pCodigo);
            BEEmpresa oBEEmpresa  = new BLEmpresa().EmpresaSeleccionar(Int32.Parse(Session["IDEmpresa"].ToString()));
            BERetornoTran oBERetorno = new BERetornoTran();

            try
            {

                Console.WriteLine("Enviando a SUNAT....");

                var sendBill = new EnviarDocumentoRequest
                {
                    Ruc = oBE.NumeroDocumentoEmisor,
                    UsuarioSol = oBEEmpresa.UsuarioSol,
                    ClaveSol = oBEEmpresa.ClaveSol,
                    EndPointUrl = oBEEmpresa.EndPointUrl,  
                    IdDocumento = oBE.SerieNumero,
                    TipoDocumento = oBE.TipoDocumento,
                    TramaXmlFirmado = oBE.TramaXML_Firmado
                };

                var enviar_sunat = new EnviarDocumentoController();
                var response_sunat = enviar_sunat.Post(sendBill);

                if (response_sunat.Exito)
                {
                    if (response_sunat.CodigoRespuesta == "0")
                    {
                        oBERetorno = new BLFacturacion().ActualizarDocumentoEnviadoSunat(pCodigo, response_sunat.TramaZipCdr, response_sunat.MensajeRespuesta, "FB_ACEP", IDUsuario());
                        msgbox(TipoMsgBox.confirmation, "Facturacion", "El proceso se realizó con éxito");
                    }
                    else {
                        oBERetorno = new BLFacturacion().ActualizarErrorXML(pCodigo, response_sunat.MensajeError, "FB_ERROR", IDUsuario());
                        msgbox(TipoMsgBox.error, "Facturacion: Codigo de Error", response_sunat.MensajeRespuesta);
                    } 
                }
                else { 
                    oBERetorno = new BLFacturacion().ActualizarErrorXML(pCodigo, response_sunat.MensajeError, "FB_ERROR", IDUsuario());
                    msgbox(TipoMsgBox.warning, "Facturacion", response_sunat.MensajeError);
                }
                Listar();
                upLista.Update();
            }
            catch (Exception ex)
            {
                RegistrarLogSistema("EnviarDocumentoSunat()", ex.ToString(), true);
            }

        }
		 
        private void ConsultarCDR3(Int32 pCodigo) {

            var sendBill = new EnviarDocumentoRequest
            {
                Ruc = "20504075355",
                UsuarioSol = "IMPLAST7",
                ClaveSol = "IMPLAST7",
                EndPointUrl = "https://e-factura.sunat.gob.pe/ol-it-wsconscpegem/billConsultService",
                IdDocumento = "01",
                TipoDocumento = "01" 
            };

            var sendBillX = new DatosDocumento
            {
                RucEmisor = "20504075355",
                TipoComprobante = "01",
                Serie = "F001",
                Numero = 9 
            };

            var enviar_sunat = new ConsultarTicketController();
           //var response_sunat = enviar_sunat.Post(sendBill, sendBillX);

            BERetornoTran oBERetorno = new BERetornoTran(); 
            //oBERetorno = new BLFacturacion().ActualizarDocumentoEnviadoSunat(pCodigo, response_sunat.ConstanciaDeRecepcion, "", "XXX", IDUsuario());


        }

		private void ConsultarCDR(Int32 pCodigo)
		{
			BEFacturaBoleta oBE = new BLFacturaBoleta().FacturaBoletaSeleccionar(pCodigo);
			BEEmpresa oBEEmpresa = new BLEmpresa().EmpresaSeleccionar(Int32.Parse(Session["IDEmpresa"].ToString()));
			BERetornoTran oBERetorno = new BERetornoTran();

			try
			{

				var requestConstanciaRequest = new ConsultaConstanciaRequest
				{
					Ruc = oBEEmpresa.Ruc,
					UsuarioSol = oBEEmpresa.UsuarioSol,
					ClaveSol = oBEEmpresa.ClaveSol,
					EndPointUrl = "https://e-factura.sunat.gob.pe/ol-it-wsconscpegem/billConsultService",
					TipoDocumento = oBE.TipoDocumento,
					Serie = oBE.SerieC,
					Numero = oBE.NumeroC
				};

				var ControllerConsultarCDR = new ConsultarConstanciaController();
				var ResponseConsultaCDR = ControllerConsultarCDR.Post(requestConstanciaRequest);

				if (ResponseConsultaCDR.Exito)
				{
					BEFacturacion oBEFact = new BEFacturacion();
					oBEFact.IDComprobante = pCodigo;
					oBEFact.TramaZipCdr = ResponseConsultaCDR.TramaZipCdr;
					oBEFact.MensajeRespuesta = ResponseConsultaCDR.CodigoRespuesta == "0" ? ResponseConsultaCDR.MensajeRespuesta : ResponseConsultaCDR.MensajeError;
					oBEFact.Tipo = "FB";
					oBEFact.IDUsuario = IDUsuario();
					oBERetorno = new BLFacturacion().ComprobanteCDRActualizar(oBEFact);

					if (oBERetorno.Retorno == "1")
					{
						msgbox(TipoMsgBox.confirmation, "Facturacion", Constantes.MENSAJE_EXITO);
					}
					else {
						msgbox(TipoMsgBox.warning, "Facturacion", oBERetorno.ErrorMensaje);
					}
				}
				else {
					msgbox(TipoMsgBox.warning, "Facturacion", ResponseConsultaCDR.MensajeError + " Codigo Error Sunat= " + ResponseConsultaCDR.CodigoRespuesta);
				}

				Listar();
				upLista.Update();
			}
			catch (Exception ex)
			{
				RegistrarLogSistema("ConsultarCDR()", ex.ToString(), true);
			}

		}

		#endregion

		#region GeneraDocumento

		private void GenerarDocumentoElectronico_PDF(Int32 pIDCodigo)
        {
            Int32 pRetorno = -1;

            try
            {
                BEFacturaBoleta oBE = new BLFacturaBoleta().FacturaBoletaSeleccionar(pIDCodigo);

                String pArchivoNombre = oBE.NumeroDocumentoEmisor + "-" + oBE.TipoDocumento + "-" + oBE.SerieNumero + ".pdf";
                String pArchivoRuta = oBE.NumeroDocumentoEmisor + "\\" + oBE.NumeroDocumentoAdquiriente + "\\" + oBE.TipoDocumento + "\\" + oBE.SerieNumero + "\\";

                String archivoRutaNombre = @Path.Combine(pArchivoRuta, pArchivoNombre);
                String archivoRutaServidor = RutaArchivos + "" + pArchivoRuta;

                if (!System.IO.Directory.Exists(archivoRutaServidor))
                    Directory.CreateDirectory(archivoRutaServidor);

                ReportDocument reporte = new ReportDocument();
                reporte.Load(Server.MapPath("~/Sunat/Impresion/FacturaBoleta.rpt"));
                SetConexion(reporte, ConexionInfo());
                reporte.Refresh();
                reporte.SetParameterValue("@IDFacturaBoleta", pIDCodigo);
                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, @Path.Combine(archivoRutaServidor, pArchivoNombre));
                reporte.Close();
                reporte.Dispose();
                reporte = null;

                //Guarda el nombre del archivo
                BLFacturaBoleta oBL = new BLFacturaBoleta();
                BEFacturaBoleta oBEx = new BEFacturaBoleta();
                BERetornoTran oBERetorno = new BERetornoTran();

                oBEx.IDFacturaBoleta = pIDCodigo;
                oBEx.RutaArchivo = pArchivoRuta;
                oBEx.NombreArchivo = pArchivoNombre;
                oBEx.Accion = "PDF";
                oBEx.IDUsuario = IDUsuario();
                oBERetorno = oBL.FacturaBoletaArchivoActualizar(oBEx);
            }
            catch (Exception ex)
            {
                pRetorno = LogSistema("GenerarDocumentoElectronico_PDF()", "", ex.Message);
            }

        }

        private void GenerarArchivo_ZIP(Int32 pIDCodigo)
        {

            BLFacturaBoleta oBL = new BLFacturaBoleta();
            BEFacturaBoleta oBE = oBL.FacturaBoletaSeleccionar(pIDCodigo);

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
            BEFacturaBoleta oBEx = new BEFacturaBoleta();
            BERetornoTran oBERetorno = new BERetornoTran();

            oBEx.IDFacturaBoleta = pIDCodigo;
            oBEx.RutaArchivo = pArchivoRuta;
            oBEx.NombreArchivo = NombreArchivoZIP;
            oBEx.Accion = "ZIP";
            oBEx.IDUsuario = IDUsuario();
            oBERetorno = new BLFacturaBoleta().FacturaBoletaArchivoActualizar(oBEx);

        }

        private void GeneraCodigoBarra(Int32 pCodigo)
        {
            try
            {
                BEFacturaBoleta oBE = new BLFacturaBoleta().FacturaBoletaSeleccionar(pCodigo);
                /*GenerarCodigo*/
                var barcodeWriter = new BarcodeWriter();

                // set the barcode format
                barcodeWriter.Options.Width = 750;
                barcodeWriter.Options.Height = 80;
                barcodeWriter.Options.Margin = 1;
                barcodeWriter.Format = BarcodeFormat.PDF_417;

                String CodigoQR = oBE.IDFacturaBoleta.ToString() + oBE.SerieNumero + DateTime.Now.ToString("yyyyMMdd") + ".jpg";
                String RutaNombreArchivo = oBE.NumeroDocumentoEmisor + "\\" + oBE.NumeroDocumentoAdquiriente + "\\" + oBE.TipoDocumento + "\\" + oBE.SerieNumero + "\\";
                String RutaNombre = CodigoQR;
                String RutaArchivosx = RutaNombreArchivo + RutaNombre;
                String RutaArchivosy = RutaArchivos + RutaNombreArchivo;
                String RutaArchivosz = RutaArchivos + RutaArchivosx;
                //                String RutaArchivosPortalx = RutaArchivosPortal + oBE.NumeroDocumentoEmisor +"\\" + oBE.NumeroDocumentoAdquiriente +"\\" + oBE.TipoDocumentoAdquiriente + "\\" + oBE.SerieNumero + "\\" + RutaNombre;
                BERetornoTran oBERetorno = new BERetornoTran();
                oBERetorno = new BLFacturacion().ActualizarCodigoQR(pCodigo, RutaArchivosx, "FB_QR", IDUsuario());
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