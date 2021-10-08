using Facturacion.Sunat.Comun.Dto.Modelos;
using Facturacion.Sunat.Controllers;
using Muebleria.App_Class.BE;
using Muebleria.App_Class.BE.General;
using Muebleria.App_Class.BL;
using Muebleria.App_Class.BL.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Muebleria.Sunat
{
	public partial class ResumenBoleta : PageBase
    {
        String RutaArchivos = ConfigurationManager.AppSettings["RutaArchivos"].ToString(); 

        #region Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ConfigPage();
                txtFechaInicio.Text = DateTime.Today.ToShortDateString();
                txtFechaFin.Text = DateTime.Today.ToShortDateString();
                Listar();
            }
        }
        #endregion

        #region Listar 
        private void Listar()
        {
            BLFacturacion oBLMigracion = new BLFacturacion();
          //  string cIDTipoDocumento = "03"; //03: Boleta
            gvLista.DataSource = oBLMigracion.ResumenDiarioSunatListar(Int32.Parse(ddlSucursal.SelectedValue), txtFechaInicio.Text, txtFechaFin.Text);
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
        #endregion

        #region CrearResumen

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            txtFechaResumenM.Focus();
            gvLista.SelectedIndex = -1;
            registrarScript("funModalAbrir();");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            gvFacturaLista.DataSource = null;
            gvFacturaLista.DataBind();
            registrarScript("funModalCerrar();");
        }

        private void LimpiarFormulario()
        {
            txtFechaResumenM.Text = DateTime.Now.ToShortDateString();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String pFechaResumen = txtFechaResumenM.Text;
                //Int32 pSucursal = Convert.ToInt32(ddlSucursalM.SelectedValue);
                String pTipoDocumento= ddlDocumentos.SelectedValue;
                //wqee


                BERetornoTran oBERetorno = new BERetornoTran();
                oBERetorno = new BLFacturacion().CrearResumenDiario(Int32.Parse(Session["IDEmpresa"].ToString()), pTipoDocumento, pFechaResumen, IDUsuario());

                if (oBERetorno.Retorno == "1")
                {
                    msgbox(TipoMsgBox.confirmation, "Facturacion", "El resumen diario se registro correctamente.");

                    Listar();

                    upLista.Update();

                    registrarScript("funModalCerrar();");

                }
                else if (oBERetorno.Retorno == "-1")
                {
                    msgbox(TipoMsgBox.error, "Facturacion", oBERetorno.ErrorMensaje);
                    RegistrarLogSistema("btnCrear_Click()", oBERetorno.ErrorMensaje, true);
                }
                else
                { msgbox(TipoMsgBox.error, "Facturacion", oBERetorno.ErrorMensaje);  }
            }
            catch (Exception ex)
            {
                RegistrarLogSistema("btnCrear_Click()", ex.ToString(), true);
            }
        }

        protected void btnResumenBuscar_Click(object sender, EventArgs e)
        {
            ResumenListar();
        }


        private void ResumenListar()
        {
            BLFacturacion oBLMigracion = new BLFacturacion();
            gvFacturaLista.DataSource = oBLMigracion.VentasCrearResumenListar(Int32.Parse(Session["IDEmpresa"].ToString()), ddlDocumentos.SelectedValue, txtFechaResumenM.Text);
            gvFacturaLista.DataBind();
            upResumen.Update();
        }

        protected void gvFacturaLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFacturaLista.PageIndex = e.NewPageIndex;
            gvFacturaLista.SelectedIndex = -1;
            ResumenListar();
        }

        #endregion

        #region Firmar

        protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String[] cmdArgumentos = e.CommandArgument.ToString().Split(new Char[] { ',' });
            Int32 pCodigo = Int32.Parse(cmdArgumentos[0].ToString());
            String pTipoComprobante = "10";

            if (e.CommandName == "Firmar")
            {
                FirmarDocumentoResumen(pCodigo);
            }

            if (e.CommandName == "Enviar")
            {
                EnviarDocumentoSunat(pCodigo);
            }

            if (e.CommandName == "GeneraXML")
            {
                GenerarXML(pCodigo);
            }

            if (e.CommandName == "DescargaXML")
            {
                DescargarXML(pCodigo, pTipoComprobante);
            }

            if (e.CommandName == "VerDocumentos")
            {
                registrarScript("ModalRegistroDetalleDocumento();");
                DetalleDocumentoListar(pCodigo);
            }
        }

        #endregion

        #region Funciones

        private void FirmarDocumentoResumen(Int32 pCodigo)
        {
            BLFacturacion oBL = new BLFacturacion();
            BEResumenComprobante oBE = oBL.ResumenInfoXML(pCodigo);
            BEEmpresa oBEEmisor = new BLEmpresa().EmpresaSeleccionar(Int32.Parse(Session["IDEmpresa"].ToString()));

            var resumen = new ResumenDiario
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
                IdDocumento = oBE.IDResumen,
                FechaEmision = oBE.FechaGeneracionResumen,
                FechaReferencia = oBE.FechaEmisionComprobante,
                Resumenes = new List<GrupoResumen>()

            };

            IList lista = new BLFacturacion().ResumenInfoDetalleXML(pCodigo);

            foreach (BEResumenComprobanteDetalle oBEDetalle in lista)
            {

                resumen.Resumenes.Add(new GrupoResumen
                {
                    Id = oBEDetalle.NumeroItem,
                    TipoDocumento = oBEDetalle.TipoDocumento,
                    CorrelativoFin = Int32.Parse(oBEDetalle.NumeroCorrelativoFin),
                    CorrelativoInicio = Int32.Parse(oBEDetalle.NumeroCorrelativoInicio),
                    Gravadas = oBEDetalle.TotalVentaGravadaIgv,
                    Moneda = oBEDetalle.TipoMoneda,
                    Serie = oBEDetalle.SerieNumero,
                    TotalIgv = oBEDetalle.TotalIGV,
                    TotalVenta = oBEDetalle.TotalVenta,
                    Exoneradas = oBEDetalle.TotalVentaExoneradaIgv,
                    Gratuitas = oBEDetalle.TotalVentaGratuita,
                    Inafectas = oBEDetalle.TotalVentaInafectaIgv,
                    NumeroDocReferencia = oBEDetalle.NumeroDocReferencia,
                    TipoDocReferencia = oBEDetalle.TipoDocReferencia,
                    EstadoItem = oBEDetalle.EstadoItem,
                    NumeroDocAdquiriente = oBEDetalle.NumeroDocAdquiriente,
                    TipoDocAdquiriente = oBEDetalle.TipoDocAdquiriente

                });

            }

            Console.WriteLine("Generando XML....");

            var controller = new GenerarResumenDiarioController();

            var response = controller.Post(resumen);

            if (!response.Exito)
            {
                throw new ApplicationException(response.MensajeError);
            }

            BERetornoTran oBERetorno = new BERetornoTran();
            oBERetorno = new BLFacturacion().ActualizarFacturaTramaFirmado(pCodigo, response.TramaXmlSinFirma,"", "", "RB_TSF", IDUsuario());


            Console.WriteLine("Firmando XML....");

            var firmado = new FirmarController();
            var firmadoRequest = new FirmadoRequest
            {
                TramaXmlSinFirma = response.TramaXmlSinFirma,
                CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(ConfigurationManager.AppSettings["RutaArchivos"].ToString() + oBEEmisor.Certificado)),
                PasswordCertificado = oBEEmisor.ClaveCertificado,
                UnSoloNodoExtension = true
            };

            var response_firmado = firmado.Post(firmadoRequest);

            if (!response_firmado.Exito)
            {
                throw new ApplicationException(response_firmado.MensajeError);
            }

            oBERetorno = new BLFacturacion().ActualizarFacturaTramaFirmado(pCodigo, response_firmado.TramaXmlFirmado,"", "", "RB_TF", IDUsuario());

            if (oBERetorno.Retorno == "1")
            {
                msgbox(TipoMsgBox.confirmation, "Facturacion", "El documento se ha Firmado con éxito");
                Listar();
                upLista.Update();
            }



        }

        private void EnviarDocumentoSunat(Int32 pCodigo)
        {

            BLFacturacion oBL = new BLFacturacion();
            BEResumenComprobante oBE = oBL.ResumenInfoXML(pCodigo);
            BEEmpresa oBEEmisor = new BLEmpresa().EmpresaSeleccionar(Int32.Parse(Session["IDEmpresa"].ToString()));

            Console.WriteLine("Enviando a SUNAT....");

            var sendBill = new EnviarDocumentoRequest
            {
                Ruc = oBE.NumeroDocumentoEmisor,
                UsuarioSol = oBEEmisor.UsuarioSol,
                ClaveSol = oBEEmisor.ClaveSol,
                EndPointUrl = ConfigurationManager.AppSettings["EndPointUrl"].ToString(),
                IdDocumento = oBE.IDResumen,
                TipoDocumento = oBE.IDTipoComprobante,
                TramaXmlFirmado = oBE.TramaXML_Firmado

            };


            var enviar_sunat = new EnviarResumenController();

            var response_sunat = enviar_sunat.Post_fisico(sendBill);

            if (!response_sunat.Exito)
            {
                msgbox(TipoMsgBox.error, "Resumen Boleta", response_sunat.MensajeError);
                throw new ApplicationException(response_sunat.MensajeError);
            }

            Console.WriteLine("Respuesta de SUNAT:");
            Console.WriteLine(response_sunat.NroTicket);
            BERetornoTran oBERetorno = new BERetornoTran();
            oBERetorno = new BLFacturacion().ActualizarFacturaTramaFirmado(pCodigo, response_sunat.NroTicket,"", "", "RB_TICK", IDUsuario());

            if (oBERetorno.Retorno == "1")
            {
                msgbox(TipoMsgBox.confirmation, "Facturacion", "El documento se ha enviado con éxito");
                Listar();
                upLista.Update();
            }

        }

        private void GenerarXML(Int32 pCodigo)
        {
            BLFacturacion oBL = new BLFacturacion();
            BEResumenComprobante oBE = oBL.ResumenInfoXML(pCodigo);

            var sendBillX = new EnviarDocumentoRequest
            {
                Ruc = oBE.RucEmisor,
                IdDocumento = oBE.IDResumen,
                TipoDocumento = oBE.IDTipoComprobante,
                TramaXmlFirmado = oBE.TramaXML_Firmado,
                TramaXmlCdr = oBE.TramaXML_SinFirmar
            };

            String RutaNombreArchivo = oBE.RucEmisor + "\\" + oBE.IDResumen + "\\";
            String NombreDocumento = oBE.RucEmisor + '-' + oBE.IDResumen + ".zip";
            String RutaArchivosx = RutaNombreArchivo;
            String RutaArchivosy = RutaArchivos + RutaNombreArchivo;
            BERetornoTran oBERetorno = new BERetornoTran();
            oBERetorno = new BLFacturacion().ActualizarUrlDocumento(pCodigo, RutaArchivosx, NombreDocumento, "10", IDUsuario());
            if (!Directory.Exists(RutaArchivosy))
            {
                Console.WriteLine("That path NOT exists already.");
                System.IO.Directory.CreateDirectory(RutaArchivosy);
            }
            Console.WriteLine("Generando XML....");


            var enviar_sunatX = new EnviarDocumentoController();

            var response = enviar_sunatX.generar_XML_CB(sendBillX, RutaArchivosy);

            if (!response.Exito)
            {
                throw new ApplicationException(response.MensajeError);
            }
            else
            {
                msgbox(TipoMsgBox.confirmation, "EJ-FACT", "Se Genero XML <br />... La Operación se ha registrado con éxito");
            }
        }

        private void DescargarXML(Int32 pCodigo, String pTipoComprobante)
        {
            Descargar(pCodigo, pTipoComprobante);
        }
        
        private void Descargar(Int32 pCodigo, String Tipo)
        {
            registrarScript("Descargar(" + pCodigo + ",'" + Tipo + "');");
        }

        #endregion

        #region DetalleDocumento

        private void DetalleDocumentoListar(Int32 pID)
        {
            BLFacturacion oBLMigracion = new BLFacturacion();
            gvDetalleLista.DataSource = oBLMigracion.ResumenInfoDetalleXML(pID);
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

    }
}