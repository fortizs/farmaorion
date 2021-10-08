using System; 

namespace Farmacia.App_Class.BE
{
    public class BEComunicacionBaja : BEBase
    {
        private Int32 _IDComunicacionBaja;
        public Int32 IDComunicacionBaja
        {
            get { return _IDComunicacionBaja; }
            set { _IDComunicacionBaja = value; }
        }

        private String _RazonSocialEmisor;
        public String RazonSocialEmisor
        {
            get { return _RazonSocialEmisor; }
            set { _RazonSocialEmisor = value; }
        }

        private Int32 _TipoDocumentoEmisor;
        public Int32 TipoDocumentoEmisor
        {
            get { return _TipoDocumentoEmisor; }
            set { _TipoDocumentoEmisor = value; }
        }

        private String _RucEmisor;
        public String RucEmisor
        {
            get { return _RucEmisor; }
            set { _RucEmisor = value; }
        }

        private String _FechaEmisionComprobante;
        public String  FechaEmisionComprobante
        {
            get { return _FechaEmisionComprobante; }
            set { _FechaEmisionComprobante = value; }
        }

        private String  _FechaGeneracionResumen;
        public String  FechaGeneracionResumen
        {
            get { return _FechaGeneracionResumen; }
            set { _FechaGeneracionResumen = value; }
        }

        private String _CorreoEmisor;
        public String CorreoEmisor
        {
            get { return _CorreoEmisor; }
            set { _CorreoEmisor = value; }
        }

        private String _TicketSunat;
        public String TicketSunat
        {
            get { return _TicketSunat; }
            set { _TicketSunat = value; }
        }
          
        private String _TramaXML_Firmado;
        public String TramaXML_Firmado
        {
            get { return _TramaXML_Firmado; }
            set { _TramaXML_Firmado = value; }
        }

        private String _TramaXML_SinFirmar;
        public String TramaXML_SinFirmar
        {
            get { return _TramaXML_SinFirmar; }
            set { _TramaXML_SinFirmar = value; }
        }

        private String _IDResumen;
        public String IDResumen
        {
            get { return _IDResumen; }
            set { _IDResumen = value; }
        }

        private String _IDTipoComprobante;
        public String IDTipoComprobante
        {
            get { return _IDTipoComprobante; }
            set { _IDTipoComprobante = value; }
        }

        private String _TipoComprobante;
        public String TipoComprobante
        {
            get { return _TipoComprobante; }
            set { _TipoComprobante = value; }
        }

        private String _IDEstadoDocumento;
        public String IDEstadoDocumento
        {
            get { return _IDEstadoDocumento; }
            set { _IDEstadoDocumento = value; }
        }

        private String _EstadoDocumento;
        public String EstadoDocumento
        {
            get { return _EstadoDocumento; }
            set { _EstadoDocumento = value; }
        }

        private String _IDEstadoSunat;
        public String IDEstadoSunat
        {
            get { return _IDEstadoSunat; }
            set { _IDEstadoSunat = value; }
        }

        private String _EstadoSunat;
        public String EstadoSunat
        {
            get { return _EstadoSunat; }
            set { _EstadoSunat = value; }
        }

        private DateTime  _FechaEnvioSunat;
        public DateTime FechaEnvioSunat
        {
            get { return _FechaEnvioSunat; }
            set { _FechaEnvioSunat = value; }
        }

        private String _MensajeSunat;
        public String MensajeSunat
        {
            get { return _MensajeSunat; }
            set { _MensajeSunat = value; }
        }
         
        private Int32 _Correlativo;
        public Int32 Correlativo
        {
            get { return _Correlativo; }
            set { _Correlativo = value; }
        }
          
        private String _TramaZIP_CDR;
        public String TramaZIP_CDR
        {
            get { return _TramaZIP_CDR; }
            set { _TramaZIP_CDR = value; }
        }

        private String _CodigoRespuestaSunat;
        public String CodigoRespuestaSunat
        {
            get { return _CodigoRespuestaSunat; }
            set { _CodigoRespuestaSunat = value; }
        }
  
    }
}

