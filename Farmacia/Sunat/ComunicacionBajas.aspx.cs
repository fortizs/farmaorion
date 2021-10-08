using Ionic.Zip;
using Farmacia.App_Class;
using Farmacia.App_Class.BE;
using Farmacia.App_Class.BE.General;
using Farmacia.App_Class.BL;
using Farmacia.App_Class.BL.Facturacion;
using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Farmacia.App_Class.BL.General;
using Facturacion.Sunat.Comun.Dto.Modelos;
using System.Collections.Generic;
using Facturacion.Sunat.Comun.Dto.Intercambio;
using Facturacion.Sunat.Controllers;

namespace Farmacia.Sunat
{
	public partial class ComunicacionBajas : PageBase
    {
        String RutaArchivos = ConfigurationManager.AppSettings["RutaArchivos"].ToString();
        #region Load
        protected void Page_Load(object sender, EventArgs e)
        {
			ValidarEstadoSesion();
			if (!Page.IsPostBack)
            {
                ConfigPage();
				CargarDDL(ddlIDEstadoSunat, new BLEstado().EstadoListar("SUNAT"), "Codigo", "Nombre", true, Constantes.TODOS);
				 
                txtFechaInicio.Text = DateTime.Today.ToShortDateString();
                Listar();
            }
        }
        #endregion

        #region Listar 
        private void Listar()
        {
            BLComunicacionBaja oBL = new BLComunicacionBaja();
            gvLista.DataSource = oBL.ComunicacionBajaListar(Int32.Parse(Session["IDEmpresa"].ToString()), Int32.Parse(ddlIDEstadoSunat.SelectedValue), txtFiltro.Text.Trim());
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
            switch (e.CommandName)
            {
                case "Enviar":
                    hdfIDComunicacionBaja.Value = e.CommandArgument.ToString(); 
                    GenerarXMLComunicacionBajaEnviarSunat(Int32.Parse(hdfIDComunicacionBaja.Value));
                    break;
                case "DescargaXML":
                    hdfIDComunicacionBaja.Value = e.CommandArgument.ToString(); 
                    GenerarArchivo_ZIP(Int32.Parse(hdfIDComunicacionBaja.Value)); 
                    break;
                case "VerDocumentos":
                    hdfIDComunicacionBaja.Value = e.CommandArgument.ToString(); 
                    registrarScript("ModalRegistroDetalleDocumento();");
                    DetalleDocumentoListar(Int32.Parse(hdfIDComunicacionBaja.Value));
                    break;
            }


        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Listar();
        }
        #endregion

        #region Funciones
  
        private void GenerarArchivo_ZIP(Int32 pIDCodigo)
        {  
            BEComunicacionBaja oBE = new BLComunicacionBaja().ComunicacionBajaSeleccionar(pIDCodigo);
             
            //GENERAR EL XML Y ELCDR ENTRO DEL MISMO ZIP
            var memZIP_CDR = new MemoryStream(Convert.FromBase64String(oBE.TramaZIP_CDR));
            var memXML_Firmado = new MemoryStream(Convert.FromBase64String(oBE.TramaXML_Firmado));

            String NombreArchivo = oBE.IDResumen;
            String NombreArchivoRespuesta = "R-" + oBE.IDResumen;
            String NombreArchivoZIP = NombreArchivo + ".zip";
            String NombreArchivoZIP_Respuesta = NombreArchivoRespuesta + ".zip";
            String NombreArchivoXML = NombreArchivo + ".xml";

            String pArchivoRuta = oBE.RucEmisor + "//COMUNICACION_BAJA//";
            String archivoRutaServidor = RutaArchivos + "" + pArchivoRuta;

            if (!System.IO.Directory.Exists(archivoRutaServidor))
                Directory.CreateDirectory(archivoRutaServidor);

            using (var fileZip = new ZipFile(NombreArchivoZIP))
            {
                fileZip.AddEntry(NombreArchivoXML, memXML_Firmado);
                if (oBE.TramaZIP_CDR.Length > 0) {
                    fileZip.AddEntry(NombreArchivoZIP_Respuesta, memZIP_CDR);
                } 
                fileZip.Save(archivoRutaServidor + NombreArchivoZIP);
            }

            // Liberamos memoria RAM.
            memZIP_CDR.Close();
            memXML_Firmado.Close();
             
            registrarScript("Descargar('" + pIDCodigo + "','" + pArchivoRuta + "','" + NombreArchivoZIP + "');");
        }

		private void GenerarXMLComunicacionBajaEnviarSunat(Int32 pCodigo)
		{
			BEComunicacionBaja oBE = new BLComunicacionBaja().ComunicacionBajaSeleccionar(pCodigo);
			IList lista = new BLComunicacionBajaDetalle().ComunicacionBajaDetalleListar(pCodigo);

			BEEmpresa oBEEmisor = new BLEmpresa().EmpresaSeleccionar(Int32.Parse(Session["IDEmpresa"].ToString()));

			var documentoBaja = new ComunicacionBaja
			{
				Emisor = new Contribuyente
				{
					NroDocumento = oBE.RucEmisor,
					TipoDocumento = oBE.TipoDocumentoEmisor.ToString(),
					NombreLegal = oBE.RazonSocialEmisor
				},
				IdDocumento = oBE.IDResumen,
				FechaEmision = oBE.FechaGeneracionResumen,
				FechaReferencia = oBE.FechaEmisionComprobante,
				Bajas = new List<DocumentoBaja>()
			};

			foreach (BEComunicacionBajaDetalle oBEDetalle in lista)
			{
				// En las comunicaciones de Baja ya no se pueden colocar boletas, ya que la anulacion de las mismas
				// la realiza el resumen diario.
				documentoBaja.Bajas.Add(new DocumentoBaja
				{
					Id = oBEDetalle.NumeroItem,
					Correlativo = oBEDetalle.NumeroDocumentoBaja,
					TipoDocumento = oBEDetalle.TipoDocumento,
					Serie = oBEDetalle.SerieDocumentoBaja,
					MotivoBaja = oBEDetalle.MotivoBaja
				});
			}

			Console.WriteLine("Generando XML....");
			BERetornoTran oBERetorno = new BERetornoTran();
			BEComunicacionBaja oBECom = new BEComunicacionBaja();

            var controller = new GenerarComunicacionBajaController();
            var documentoResponse = controller.Post(documentoBaja);

            //var documentoResponse = RestHelper<ComunicacionBaja, DocumentoResponse>.Execute("GenerarComunicacionBaja", documentoBaja);
			if (!documentoResponse.Exito)
			{
				oBECom.IDComunicacionBaja = pCodigo;
				oBECom.TramaXML_SinFirmar = documentoResponse.TramaXmlSinFirma;
				oBECom.TramaXML_Firmado = "";
				oBECom.TramaZIP_CDR = "";
				oBECom.TicketSunat = "";
				oBECom.CodigoRespuestaSunat = "";
				oBECom.MensajeSunat = documentoResponse.MensajeError;
				oBECom.IDEstadoDocumento = "8";
				oBECom.IDEstadoSunat = "1";
				oBECom.IDUsuario = IDUsuario();
				oBERetorno = new BLComunicacionBaja().ComunicacionBajaActualizar(oBECom);
				msgbox(TipoMsgBox.warning, "Facturacion", "xml =" + documentoResponse.MensajeError);
			}

			Console.WriteLine("Firmando XML....");
            var firmado = new FirmarController();
            // Firmado del Documento.
            var firmadoRequest = new FirmadoRequest
			{
				TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
				CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(ConfigurationManager.AppSettings["RutaArchivos"].ToString() + oBEEmisor.Certificado)),
				PasswordCertificado = oBEEmisor.ClaveCertificado,
				UnSoloNodoExtension = true
			};

            var responseFirma = firmado.Post(firmadoRequest);
           // var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("Firmar", firmado);

			if (!responseFirma.Exito)
			{
				oBECom.IDComunicacionBaja = pCodigo;
				oBECom.TramaXML_SinFirmar = documentoResponse.TramaXmlSinFirma;
				oBECom.TramaXML_Firmado = responseFirma.TramaXmlFirmado;
				oBECom.TramaZIP_CDR = "";
				oBECom.TicketSunat = "";
				oBECom.CodigoRespuestaSunat = "";
				oBECom.MensajeSunat = responseFirma.MensajeError;
				oBECom.IDEstadoDocumento = "6";
				oBECom.IDEstadoSunat = "1";
				oBECom.IDUsuario = IDUsuario();
				oBERetorno = new BLComunicacionBaja().ComunicacionBajaActualizar(oBECom);
				msgbox(TipoMsgBox.warning, "Facturacion", "Firmado =" + responseFirma.MensajeError);

			}

			Console.WriteLine("Enviando a SUNAT....");
			var sendBill = new EnviarDocumentoRequest
			{
				Ruc = oBE.RucEmisor,
				UsuarioSol = oBEEmisor.UsuarioSol,
				ClaveSol = oBEEmisor.ClaveSol,
				EndPointUrl = oBEEmisor.EndPointUrl,
				IdDocumento = oBE.IDResumen,
				TramaXmlFirmado = responseFirma.TramaXmlFirmado
            };

            var enviar_sunat = new EnviarResumenController();
            var enviarResumenResponse = enviar_sunat.Post(sendBill);


           // var enviarResumenResponse = RestHelper<EnviarDocumentoRequest, EnviarResumenResponse>.Execute("EnviarResumen", sendBill);

			if (!enviarResumenResponse.Exito)
			{
				oBECom.IDComunicacionBaja = pCodigo;
				oBECom.TramaXML_SinFirmar = documentoResponse.TramaXmlSinFirma;
				oBECom.TramaXML_Firmado = responseFirma.TramaXmlFirmado;
				oBECom.TramaZIP_CDR = "";
				oBECom.TicketSunat = "";
				oBECom.CodigoRespuestaSunat = "";
				oBECom.MensajeSunat = enviarResumenResponse.MensajeError;
				oBECom.IDEstadoDocumento = "10";
				oBECom.IDEstadoSunat = "11";
				oBECom.IDUsuario = IDUsuario();
				oBERetorno = new BLComunicacionBaja().ComunicacionBajaActualizar(oBECom);
				msgbox(TipoMsgBox.warning, "Facturacion", "Envio =" + enviarResumenResponse.MensajeError);

			}
			else
			{

				var consultarTicketRequest = new ConsultaTicketRequest
				{
					Ruc = oBEEmisor.Ruc,
					NroTicket = enviarResumenResponse.NroTicket,
					UsuarioSol = oBEEmisor.UsuarioSol,
					ClaveSol = oBEEmisor.ClaveSol,
					EndPointUrl = oBEEmisor.EndPointUrl
				};

                var enviar_sunatX = new ConsultarTicketController();
                var responseTicket = enviar_sunatX.Post(consultarTicketRequest);

              //  var responseTicket = RestHelper<ConsultaTicketRequest, EnviarDocumentoResponse>.Execute("ConsultarTicket", consultarTicketRequest);

				if (!responseTicket.Exito)
				{
					oBECom.IDComunicacionBaja = pCodigo;
					oBECom.TramaXML_SinFirmar = documentoResponse.TramaXmlSinFirma;
					oBECom.TramaXML_Firmado = responseFirma.TramaXmlFirmado;
					oBECom.TramaZIP_CDR = responseTicket.TramaZipCdr;
					oBECom.TicketSunat = enviarResumenResponse.NroTicket;
					oBECom.CodigoRespuestaSunat = responseTicket.CodigoRespuesta;
					oBECom.MensajeSunat = responseTicket.MensajeError;
					oBECom.IDEstadoDocumento = "10";
					oBECom.IDEstadoSunat = "11";
					oBECom.IDUsuario = IDUsuario();
					oBERetorno = new BLComunicacionBaja().ComunicacionBajaActualizar(oBECom);
					msgbox(TipoMsgBox.warning, "Facturacion", "Ticket =" + responseTicket.MensajeError);
				}
				else
				{

					oBECom.IDComunicacionBaja = pCodigo;
					oBECom.TramaXML_SinFirmar = documentoResponse.TramaXmlSinFirma;
					oBECom.TramaXML_Firmado = responseFirma.TramaXmlFirmado;
					oBECom.TramaZIP_CDR = responseTicket.TramaZipCdr;
					oBECom.TicketSunat = enviarResumenResponse.NroTicket;
					oBECom.CodigoRespuestaSunat = responseTicket.CodigoRespuesta;
					oBECom.MensajeSunat = responseTicket.MensajeRespuesta;
					oBECom.IDEstadoDocumento = "5";
					oBECom.IDEstadoSunat = "2";
					oBECom.IDUsuario = IDUsuario();
					oBERetorno = new BLComunicacionBaja().ComunicacionBajaActualizar(oBECom);

					if (oBERetorno.Retorno == "1")
					{
						msgbox(TipoMsgBox.confirmation, "Facturacion", "El documento se ha enviado con éxito");
					}
				}
			}

			Listar();
			upLista.Update();
		}

		private void GenerarXML(Int32 pCodigo)
        {
            //BLFacturacion oBL = new BLFacturacion();
            //BEComunicacionBaja oBE = oBL.ComunicacionBajaInfoXML(pCodigo);

            //var sendBillX = new EnviarDocumentoRequest
            //{
            //    Ruc = oBE.RucEmisor,
            //    IdDocumento = oBE.IDResumen,
            //    TipoDocumento = oBE.IDTipoComprobante,
            //    TramaXmlFirmado = oBE.TramaXML_Firmado,
            //    TramaXmlCdr = oBE.TramaXML_SinFirmar

            //};
            //String RutaNombreArchivo = oBE.RucEmisor + "\\" + oBE.IDResumen + "\\";
            //String NombreDocumento = oBE.RucEmisor + '-' + oBE.IDResumen + ".zip";
            //String RutaArchivosx = RutaNombreArchivo;
            //String RutaArchivosy = RutaArchivos + RutaNombreArchivo;
            //BERetornoTran oBERetorno = new BERetornoTran();
            //oBERetorno = new BLFacturacion().ActualizarUrlDocumento(pCodigo, RutaArchivosx, NombreDocumento, "09", IDUsuario());
            //if (!Directory.Exists(RutaArchivosy))
            //{
            //    Console.WriteLine("That path NOT exists already.");
            //    System.IO.Directory.CreateDirectory(RutaArchivosy);
            //}
            //Console.WriteLine("Generando XML....");

            //var enviar_sunatX = new EnviarDocumentoController();

            //var response = enviar_sunatX.generar_XML_CB(sendBillX, RutaArchivosy);

            //if (!response.Exito)
            //{
            //    throw new ApplicationException(response.MensajeError);
            //}
            //else
            //{
            //    msgbox(TipoMsgBox.confirmation, "EJ-FACT", "Se Genero XML <br />... La Operación se ha registrado con éxito");
            //}
            //Descargar(response.MensajeRespuesta);             
        }

        private void DescargarXML(Int32 pCodigo, String pTipoComprobante)
        {
            Descargar(pCodigo, pTipoComprobante);
        }
        #endregion

        #region DetalleDocumento

        private void DetalleDocumentoListar(Int32 pID)
        {
            BLFacturacion oBLMigracion = new BLFacturacion();
            gvDetalleLista.DataSource = oBLMigracion.ComunicacionBajaDetalleListar(pID);
            gvDetalleLista.DataBind();
            upDetalleDocumentos.Update();
        }
        protected void gvDetalleLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalleLista.PageIndex = e.NewPageIndex;
            gvDetalleLista.SelectedIndex = -1;
            DetalleDocumentoListar(1);
        }

        protected void btnDetalleCancelar_Click(object sender, EventArgs e)
        {
            registrarScript("ModalCerrarDetalleDocumento();");
        }

        #endregion

        #region CrearResumen

        protected void btnCrearResumen_Click(object sender, EventArgs e)
        {
            registrarScript("ModalRegistroResumen();");
            FacturaListar();
        }

        protected void btnCerrarResumen_Click(object sender, EventArgs e)
        {
            registrarScript("ModalCerrarResumen();");
        }

        protected void btnFacturaBuscar_Click(object sender, EventArgs e)
        {
            FacturaListar();
        }

        protected void btnGenerarResumen_Click(object sender, EventArgs e)
        {
            ArrayList ListaIndex = new ArrayList();
            foreach (GridViewRow row in gvFacturaLista.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (CheckBox)row.Cells[0].FindControl("chkRDSel");
                    if (chkRow.Checked)
                    {
                        BEFacturacion pBE = new BEFacturacion();
                        pBE.Index = Int32.Parse(((Label)row.Cells[0].FindControl("lblIndex")).Text);
                        pBE.Codigo = Int32.Parse(((Label)row.Cells[0].FindControl("lblIDFacturaBoleta")).Text);
                        pBE.MotivoBaja = ((TextBox)row.Cells[6].FindControl("txtMotivoBaja")).Text;

                        ListaIndex.Add(pBE);
                    }
                }
            }
            if (ListaIndex.Count > 0)
            {
                Session["aListaBEDocumento"] = ListaIndex;

                /********************************************************/

                //VALIDACIONES
                StringBuilder pValidaciones = new StringBuilder();
                //if (txtFechaGeneracion.Text.Trim().Length == 0) pValidaciones.Append("<div>Ingrese la fecha de Generación.</div>");

                ArrayList aListaBEDocumento = new ArrayList();
                aListaBEDocumento = (ArrayList)Session["aListaBEDocumento"];

                if (aListaBEDocumento.Count == 0) pValidaciones.Append("<div>Seleccione al menos un Documento a dar de Baja!.</div>");

                if (pValidaciones.Length > 0)
                {
                    msgbox(TipoMsgBox.warning, pValidaciones.ToString());
                    return;
                }
                //FIN DE VALIDACIONES--------------------------------------

                BLFacturacion oBL = new BLFacturacion();
                BEFacturacion oBE = new BEFacturacion();

                ArrayList ListaBEDocumento = new ArrayList();

                oBE.TipoDocumento = ddlDocumentos.SelectedValue;
                oBE.FechaEmision = txtFechaInicio.Text;
                oBE.IDUsuario = IDUsuario();
                oBE.IDEmpresa = Int32.Parse(Session["IDEmpresa"].ToString());
                SetListaBEDocumento(ref ListaBEDocumento);

                BERetornoTran oBERetorno = new BERetornoTran();
                oBERetorno = oBL.CrearResumenBajaMasivo(oBE, ListaBEDocumento);

                if (oBERetorno.Retorno == "1")
                {
                    msgbox(TipoMsgBox.confirmation, "Facturación", "La generación de Baja se ha registrado con éxito");
                    Session["aListaBEDocumento"] = null;
                    registrarScript("ModalCerrarResumen();");
                    Listar();
                    upLista.Update();

                }
                else if (oBERetorno.Retorno == "-2")
                {
                    msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
                }
                else if (oBERetorno.Retorno == "-1")
                {
                    msgbox(TipoMsgBox.warning, oBERetorno.ErrorMensaje);
                    RegistrarLogSistema("lnkGuardarCompra_Click()", oBERetorno.ErrorMensaje, true);
                }


                /**********************************************************/

            }
            else
            {
                msgbox(TipoMsgBox.warning, "Seleccione al menos un Documento");
            }
        }

        private void SetListaBEDocumento(ref ArrayList ListaBEDocumento)
        {

            ArrayList aListaBEDocumento = new ArrayList();
            aListaBEDocumento = (ArrayList)Session["aListaBEDocumento"];
            if (aListaBEDocumento != null)
            {
                foreach (BEFacturacion oBE in aListaBEDocumento)
                {
                    oBE.Codigo = oBE.Codigo;
                    oBE.MotivoBaja = oBE.MotivoBaja;
                    oBE.IDUsuario = IDUsuario();
                    ListaBEDocumento.Add(oBE);
                }
            }
        }

        private void FacturaListar()
        {
            BLFacturacion oBLMigracion = new BLFacturacion();
            gvFacturaLista.DataSource = oBLMigracion.VentasComunicacionBajaListar(Int32.Parse(Session["IDEmpresa"].ToString()), ddlDocumentos.SelectedValue, txtFechaInicio.Text);
            gvFacturaLista.DataBind();
            upResumen.Update();
        }

        protected void gvFacturaLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFacturaLista.PageIndex = e.NewPageIndex;
            gvFacturaLista.SelectedIndex = -1;
            FacturaListar();
        }

        private void Descargar(Int32 pCodigo, String Tipo)
        {
            registrarScript("Descargar(" + pCodigo + ",'" + Tipo + "');");
        }

        #endregion

    }
}