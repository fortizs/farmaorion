using System;
namespace Farmacia.App_Class.BE
{
    public class BEFacturacion : BEBase
    {
        private Int32 _Codigo;
        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        private Int32 _Index;
        public Int32 Index
        {
            get { return _Index; }
            set { _Index = value; }
        }
        

        private String _IDEstadoDocumento;
        public String IDEstadoDocumento
        {
            get { return _IDEstadoDocumento; }
            set { _IDEstadoDocumento = value; }
        }
        private String _IDEstadoSunat;
        public String IDEstadoSunat
        {
            get { return _IDEstadoSunat; }
            set { _IDEstadoSunat = value; }
        }


        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }

        private String _TipoMoneda;
        public String TipoMoneda
        {
            get { return _TipoMoneda; }
            set { _TipoMoneda = value; }
        }

        private Decimal _TotalVenta;
        public Decimal TotalVenta
        {
            get { return _TotalVenta; }
            set { _TotalVenta = value; }
        }

        private String _FechaEmision;
        public String FechaEmision
        {
            get { return _FechaEmision; }
            set { _FechaEmision = value; }
        }

        private String _EstadoDocumento;
        public String EstadoDocumento
        {
            get { return _EstadoDocumento; }
            set { _EstadoDocumento = value; }
        }

        private String _EstadoSunat;
        public String EstadoSunat
        {
            get { return _EstadoSunat; }
            set { _EstadoSunat = value; }
        }

        private DateTime _FechaEnvioSunat;
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

        private String _Cliente;
        public String Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        private String _NumeroDocumentoCliente;
        public String NumeroDocumentoCliente
        {
            get { return _NumeroDocumentoCliente; }
            set { _NumeroDocumentoCliente = value; }
        }

        private String _TipoComprobante;
        public String TipoComprobante
        {
            get { return _TipoComprobante; }
            set { _TipoComprobante = value; }
        }

        /************************************************/

        private Int32 _IDComunicacionBaja;
        public Int32 IDComunicacionBaja
        {
            get { return _IDComunicacionBaja; }
            set { _IDComunicacionBaja = value; }
        }

        private String _IDResumen;
        public String IDResumen
        {
            get { return _IDResumen; }
            set { _IDResumen = value; }
        }

        private DateTime _FechaEmisionComprobante;
        public DateTime FechaEmisionComprobante
        {
            get { return _FechaEmisionComprobante; }
            set { _FechaEmisionComprobante = value; }
        }

        private DateTime _FechaGeneracionResumen;
        public DateTime FechaGeneracionResumen
        {
            get { return _FechaGeneracionResumen; }
            set { _FechaGeneracionResumen = value; }
        }

        private String _TicketSunat;
        public String TicketSunat
        {
            get { return _TicketSunat; }
            set { _TicketSunat = value; }
        }

        private String _SerieDocumentoBaja;
        public String SerieDocumentoBaja
        {
            get { return _SerieDocumentoBaja; }
            set { _SerieDocumentoBaja = value; }
        }

        private String _NumeroDocumentoBaja;
        public String NumeroDocumentoBaja
        {
            get { return _NumeroDocumentoBaja; }
            set { _NumeroDocumentoBaja = value; }
        }

        private String _MotivoBaja;
        public String MotivoBaja
        {
            get { return _MotivoBaja; }
            set { _MotivoBaja = value; }
        }

        private Int32 _IDComunicacionBajaDetalle;
        public Int32 IDComunicacionBajaDetalle
        {
            get { return _IDComunicacionBajaDetalle; }
            set { _IDComunicacionBajaDetalle = value; }
        }

        private Int32 _NumeroItem;
        public Int32 NumeroItem
        {
            get { return _NumeroItem; }
            set { _NumeroItem = value; }
        }

        private Int32 _IDComunicacionBajaFinal;
        public Int32 IDComunicacionBajaFinal
        {
            get { return _IDComunicacionBajaFinal; }
            set { _IDComunicacionBajaFinal = value; }
        }

        private Int32 _IDFacturaBoleta;
        public Int32 IDFacturaBoleta
        {
            get { return _IDFacturaBoleta; }
            set { _IDFacturaBoleta = value; }
        }

		private Int32 _IDComprobante;
		public Int32 IDComprobante
		{
			get { return _IDComprobante; }
			set { _IDComprobante = value; }
		}

		private String _TramaZipCdr;
		public String TramaZipCdr
		{
			get { return _TramaZipCdr; }
			set { _TramaZipCdr = value; }
		}

		private String _MensajeRespuesta;
		public String MensajeRespuesta
		{
			get { return _MensajeRespuesta; }
			set { _MensajeRespuesta = value; }
		}

		private String _Tipo;
		public String Tipo
		{
			get { return _Tipo; }
			set { _Tipo = value; }
		}
		  
    }
}
