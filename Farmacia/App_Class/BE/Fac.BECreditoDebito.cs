using System; 

namespace Farmacia.App_Class.BE
{
    public class BECreditoDebito : BEBase
    {

        private Int32 _IDCreditoDebito;
        public Int32 IDCreditoDebito
        {
            get { return _IDCreditoDebito; }
            set { _IDCreditoDebito = value; }
        }

        private Int32 _IDEmisor;
        public Int32 IDEmisor
        {
            get { return _IDEmisor; }
            set { _IDEmisor = value; }
        }

        private String _CorreoEmisor;
        public String CorreoEmisor
        {
            get { return _CorreoEmisor; }
            set { _CorreoEmisor = value; }
        }

        private String _NumeroDocumentoEmisor;
        public String NumeroDocumentoEmisor
        {
            get { return _NumeroDocumentoEmisor; }
            set { _NumeroDocumentoEmisor = value; }
        }

        private Int32 _TipoDocumentoEmisor;
        public Int32 TipoDocumentoEmisor
        {
            get { return _TipoDocumentoEmisor; }
            set { _TipoDocumentoEmisor = value; }
        }

        private String _RazonSocialEmisor;
        public String RazonSocialEmisor
        {
            get { return _RazonSocialEmisor; }
            set { _RazonSocialEmisor = value; }
        }

        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

		private String _TipoComprobante;
		public String TipoComprobante
		{
			get { return _TipoComprobante; }
			set { _TipoComprobante = value; }
		}
		

		private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }

        private String _FechaEmision;
        public String FechaEmision
        {
            get { return _FechaEmision; }
            set { _FechaEmision = value; }
        }

        private String _CorreoAdquiriente;
        public String CorreoAdquiriente
        {
            get { return _CorreoAdquiriente; }
            set { _CorreoAdquiriente = value; }
        }

        private String _NumeroDocumentoAdquiriente;
        public String NumeroDocumentoAdquiriente
        {
            get { return _NumeroDocumentoAdquiriente; }
            set { _NumeroDocumentoAdquiriente = value; }
        }

        private String _TipoDocumentoAdquiriente;
        public String TipoDocumentoAdquiriente
        {
            get { return _TipoDocumentoAdquiriente; }
            set { _TipoDocumentoAdquiriente = value; }
        }

        private String _RazonSocialAdquiriente;
        public String RazonSocialAdquiriente
        {
            get { return _RazonSocialAdquiriente; }
            set { _RazonSocialAdquiriente = value; }
        }

        private String _DireccionAdquiriente;
        public String DireccionAdquiriente
        {
            get { return _DireccionAdquiriente; }
            set { _DireccionAdquiriente = value; }
        }

        private String _TipoMoneda;
        public String TipoMoneda
        {
            get { return _TipoMoneda; }
            set { _TipoMoneda = value; }
        }

        private Decimal _TotalVenta_NetoGravada;
        public Decimal TotalVenta_NetoGravada
        {
            get { return _TotalVenta_NetoGravada; }
            set { _TotalVenta_NetoGravada = value; }
        }

        private Decimal _TotalVenta_NetoInafecta;
        public Decimal TotalVenta_NetoInafecta
        {
            get { return _TotalVenta_NetoInafecta; }
            set { _TotalVenta_NetoInafecta = value; }
        }

        private Decimal _TotalVenta_NetoExonerada;
        public Decimal TotalVenta_NetoExonerada
        {
            get { return _TotalVenta_NetoExonerada; }
            set { _TotalVenta_NetoExonerada = value; }
        }

        private Decimal _TotalVenta_NetoGratuita;
        public Decimal TotalVenta_NetoGratuita
        {
            get { return _TotalVenta_NetoGratuita; }
            set { _TotalVenta_NetoGratuita = value; }
        }

        private Decimal _TotalIgvItems;
        public Decimal TotalIgvItems
        {
            get { return _TotalIgvItems; }
            set { _TotalIgvItems = value; }
        }

        private Decimal _TotalVenta;
        public Decimal TotalVenta
        {
            get { return _TotalVenta; }
            set { _TotalVenta = value; }
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

        private String _MontoTotalLetra;
        public String MontoTotalLetra
        {
            get { return _MontoTotalLetra; }
            set { _MontoTotalLetra = value; }
        }

        private String _TramaXML_SinFirmar;
        public String TramaXML_SinFirmar
        {
            get { return _TramaXML_SinFirmar; }
            set { _TramaXML_SinFirmar = value; }
        }

        private String _TramaXML_Firmado;
        public String TramaXML_Firmado
        {
            get { return _TramaXML_Firmado; }
            set { _TramaXML_Firmado = value; }
        }

        private String _TramaZIP_CDR;
        public String TramaZIP_CDR
        {
            get { return _TramaZIP_CDR; }
            set { _TramaZIP_CDR = value; }
        }

        private String _ResumenFirma;
        public String ResumenFirma
        {
            get { return _ResumenFirma; }
            set { _ResumenFirma = value; }
        }

        private String _ValorFirmaDigital;
        public String ValorFirmaDigital
        {
            get { return _ValorFirmaDigital; }
            set { _ValorFirmaDigital = value; }
        }

        private String _CodigoQR;
        public String CodigoQR
        {
            get { return _CodigoQR; }
            set { _CodigoQR = value; }
        }
         
        private String _RutaDocumento;
        public String RutaDocumento
        {
            get { return _RutaDocumento; }
            set { _RutaDocumento = value; }
        }

        private String _NombreDocumento;
        public String NombreDocumento
        {
            get { return _NombreDocumento; }
            set { _NombreDocumento = value; }
        }

        private String _RutaArchivoZip;
        public String RutaArchivoZip
        {
            get { return _RutaArchivoZip; }
            set { _RutaArchivoZip = value; }
        }

        private String _NombreArchivoZip;
        public String NombreArchivoZip
        {
            get { return _NombreArchivoZip; }
            set { _NombreArchivoZip = value; }
        }

        private String _RutaArchivo;
        public String RutaArchivo
        {
            get { return _RutaArchivo; }
            set { _RutaArchivo = value; }
        }

        private String _NombreArchivo;
        public String NombreArchivo
        {
            get { return _NombreArchivo; }
            set { _NombreArchivo = value; }
        }

        private String _Accion;
        public String Accion
        {
            get { return _Accion; }
            set { _Accion = value; }
        }

        private String _NombrePARA;
        public String NombrePARA
        {
            get { return _NombrePARA; }
            set { _NombrePARA = value; }
        }

        private String _CorreoPARA;
        public String CorreoPARA
        {
            get { return _CorreoPARA; }
            set { _CorreoPARA = value; }
        }

        private String _NombreCC;
        public String NombreCC
        {
            get { return _NombreCC; }
            set { _NombreCC = value; }
        }

        private String _CorreoCC;
        public String CorreoCC
        {
            get { return _CorreoCC; }
            set { _CorreoCC = value; }
        }

        private Int32 _IDFacturaBoleta;
        public Int32 IDFacturaBoleta
        {
            get { return _IDFacturaBoleta; }
            set { _IDFacturaBoleta = value; }
        }

        private Int32 _IDTipoDocumento;
        public Int32 IDTipoDocumento
        {
            get { return _IDTipoDocumento; }
            set { _IDTipoDocumento = value; }
        }

        private Decimal _TotalIscItems;
        public Decimal TotalIscItems
        {
            get { return _TotalIscItems; }
            set { _TotalIscItems = value; }
        }

        private Decimal _TotalOtrosTributos;
        public Decimal TotalOtrosTributos
        {
            get { return _TotalOtrosTributos; }
            set { _TotalOtrosTributos = value; }
        }

        private String _SerieNumeroAfectado;
        public String SerieNumeroAfectado
        {
            get { return _SerieNumeroAfectado; }
            set { _SerieNumeroAfectado = value; }
        }

        private String _TipoDocumentoAfectado;
        public String TipoDocumentoAfectado
        {
            get { return _TipoDocumentoAfectado; }
            set { _TipoDocumentoAfectado = value; }
        }

        private String _IDTipoMotivo;
        public String IDTipoMotivo
        {
            get { return _IDTipoMotivo; }
            set { _IDTipoMotivo = value; }
        }

        private String _MotivoDocumento;
        public String MotivoDocumento
        {
            get { return _MotivoDocumento; }
            set { _MotivoDocumento = value; }
        }
          
        private String _NombreComercialEmisor;
        public String NombreComercialEmisor
        {
            get { return _NombreComercialEmisor; }
            set { _NombreComercialEmisor = value; }
        }

        private String _DireccionEmisor;
        public String DireccionEmisor
        {
            get { return _DireccionEmisor; }
            set { _DireccionEmisor = value; }
        }

        private String _UrbanizacionEmisor;
        public String UrbanizacionEmisor
        {
            get { return _UrbanizacionEmisor; }
            set { _UrbanizacionEmisor = value; }
        }

        private String _ProvinciaEmisor;
        public String ProvinciaEmisor
        {
            get { return _ProvinciaEmisor; }
            set { _ProvinciaEmisor = value; }
        }

        private String _DepartamentoEmisor;
        public String DepartamentoEmisor
        {
            get { return _DepartamentoEmisor; }
            set { _DepartamentoEmisor = value; }
        }

        private String _DistritoEmisor;
        public String DistritoEmisor
        {
            get { return _DistritoEmisor; }
            set { _DistritoEmisor = value; }
        }

        private String _PaisEmisor;
        public String PaisEmisor
        {
            get { return _PaisEmisor; }
            set { _PaisEmisor = value; }
        }

        private String _UbigeoEmisor;
        public String UbigeoEmisor
        {
            get { return _UbigeoEmisor; }
            set { _UbigeoEmisor = value; }
        }
         
        private String _TipoDocumentoReferencia;
        public String TipoDocumentoReferencia
        {
            get { return _TipoDocumentoReferencia; }
            set { _TipoDocumentoReferencia = value; }
        }

        private String _NumeroDocumentoReferencia;
        public String NumeroDocumentoReferencia
        {
            get { return _NumeroDocumentoReferencia; }
            set { _NumeroDocumentoReferencia = value; }
        }
         
        private String _RutaCodigoQR;
        public String RutaCodigoQR
        {
            get { return _RutaCodigoQR; }
            set { _RutaCodigoQR = value; }
        }

        private String _Certificado;
        public String Certificado
        {
            get { return _Certificado; }
            set { _Certificado = value; }
        }

        private String _ClaveCertificado;
        public String ClaveCertificado
        {
            get { return _ClaveCertificado; }
            set { _ClaveCertificado = value; }
        } 

        private String _IDTipoComprobante;
        public String IDTipoComprobante
        {
            get { return _IDTipoComprobante; }
            set { _IDTipoComprobante = value; }
        }
          
        private String _MonedaSimbolo;
        public String MonedaSimbolo
        {
            get { return _MonedaSimbolo; }
            set { _MonedaSimbolo = value; }
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
         

        private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

		private String _HoraEmision;
		public String HoraEmision
		{
			get { return _HoraEmision; }
			set { _HoraEmision = value; }
		}

		private String _FechaVencimiento;
		public String FechaVencimiento
		{
			get { return _FechaVencimiento; }
			set { _FechaVencimiento = value; }
		}

		private String _CodigoLeyenda;
		public String CodigoLeyenda
		{
			get { return _CodigoLeyenda; }
			set { _CodigoLeyenda = value; }
		}

		private String _SerieC;
		public String SerieC
		{
			get { return _SerieC; }
			set { _SerieC = value; }
		}

		private Int32 _NroItems;
		public Int32 NroItems
		{
			get { return _NroItems; }
			set { _NroItems = value; }
		}

		private Int32 _NumeroC;
		public Int32 NumeroC
		{
			get { return _NumeroC; }
			set { _NumeroC = value; }
		}

		private Int32 _IDVenta;
		public Int32 IDVenta
		{
			get { return _IDVenta; }
			set { _IDVenta = value; }
		}
		  
	}
}
