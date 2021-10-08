using System; 

namespace Farmacia.App_Class.BE
{
    public class BEFacturaBoleta : BEBase
    {
        private Int32 _IDFacturaBoleta;
        public Int32 IDFacturaBoleta
        {
            get { return _IDFacturaBoleta; }
            set { _IDFacturaBoleta = value; }
        }

        private Int32 _Codigo;
        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
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


        private String _NombreSucursal;
        public String NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal  = value; }
        }

        private String _EstadoDocumento;
        public String EstadoDocumento
        {
            get { return _EstadoDocumento; }
            set { _EstadoDocumento = value; }
        }

        private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }

        private String _SerieDocumento;
        public String SerieDocumento
        {
            get { return _SerieDocumento; }
            set { _SerieDocumento = value; }
        }

        private String _NumeroDocumento;
        public String NumeroDocumento
        {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = value; }
        }

        private String _FechaEmision;
        public String FechaEmision
        {
            get { return _FechaEmision; }
            set { _FechaEmision = value; }
        }

        private String _FechaVencimiento;
        public String FechaVencimiento
        {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = value; }
        }

        private String _TipoMoneda;
        public String TipoMoneda
        {
            get { return _TipoMoneda; }
            set { _TipoMoneda = value; }
        }
         
        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        private Decimal _TotalIgvItems;
        public Decimal TotalIgvItems
        {
            get { return _TotalIgvItems; }
            set { _TotalIgvItems = value; }
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

        private Decimal _DescuentoGlobal;
        public Decimal DescuentoGlobal
        {
            get { return _DescuentoGlobal; }
            set { _DescuentoGlobal = value; }
        }

        private Decimal _TotalVenta;
        public Decimal TotalVenta
        {
            get { return _TotalVenta; }
            set { _TotalVenta = value; }
        }

        private String _TextoLeyenda_1;
        public String TextoLeyenda_1
        {
            get { return _TextoLeyenda_1; }
            set { _TextoLeyenda_1 = value; }
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

        private String _TramaZIP_CDR;
        public String TramaZIP_CDR
        {
            get { return _TramaZIP_CDR; }
            set { _TramaZIP_CDR = value; }
        }

        private Int32 _TotalIsc;
        public Int32 TotalIsc
        {
            get { return _TotalIsc; }
            set { _TotalIsc = value; }
        }

        private Int32 _TotalOtrosTributos;
        public Int32 TotalOtrosTributos
        {
            get { return _TotalOtrosTributos; }
            set { _TotalOtrosTributos = value; }
        }

        /***************/


        private Int32 _IDFacturaBoletaDetalle;
        public Int32 IDFacturaBoletaDetalle
        {
            get { return _IDFacturaBoletaDetalle; }
            set { _IDFacturaBoletaDetalle = value; }
        }

        private Int32 _Item;
        public Int32 Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private String _DescripcionProducto;
        public String DescripcionProducto
        {
            get { return _DescripcionProducto; }
            set { _DescripcionProducto = value; }
        }

        private Int32 _Cantidad;
        public Int32 Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private String _UnidadMedida;
        public String UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private Decimal _PrecioUnitario;
        public Decimal PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }

        private Decimal _PrecioReferencial;
        public Decimal PrecioReferencial
        {
            get { return _PrecioReferencial; }
            set { _PrecioReferencial = value; }
        }

        private String _TipoPrecio;
        public String TipoPrecio
        {
            get { return _TipoPrecio; }
            set { _TipoPrecio = value; }
        }

        private Decimal _Descuento;
        public Decimal Descuento
        {
            get { return _Descuento; }
            set { _Descuento = value; }
        }

        private Decimal _Igv;
        public Decimal Igv
        {
            get { return _Igv; }
            set { _Igv = value; }
        }

        private String _TipoImpuesto;
        public String TipoImpuesto
        {
            get { return _TipoImpuesto; }
            set { _TipoImpuesto = value; }
        }

        private Decimal _TotalSinImpuesto;
        public Decimal TotalSinImpuesto
        {
            get { return _TotalSinImpuesto; }
            set { _TotalSinImpuesto = value; }
        }

        private String _CorreoEmisor;
        public String CorreoEmisor
        {
            get { return _CorreoEmisor; }
            set { _CorreoEmisor = value; }
        }

        private String _CorreoAdquiriente;
        public String CorreoAdquiriente
        {
            get { return _CorreoAdquiriente; }
            set { _CorreoAdquiriente = value; }
        }

        private String _CodigoQR;
        public String CodigoQR
        {
            get { return _CodigoQR; }
            set { _CodigoQR = value; }
        }

        private String _DireccionAdquiriente;
        public String DireccionAdquiriente
        {
            get { return _DireccionAdquiriente; }
            set { _DireccionAdquiriente = value; }
        }

        private String _Resumen;
        public String Resumen
        {
            get { return _Resumen; }
            set { _Resumen = value; }
        }

        private String _UrlLogo;
        public String UrlLogo
        {
            get { return _UrlLogo; }
            set { _UrlLogo = value; }
        }

        private String _CuentaBancaria;
        public String CuentaBancaria
        {
            get { return _CuentaBancaria; }
            set { _CuentaBancaria = value; }
        }

        private String _CuentaDetraccion;
        public String CuentaDetraccion
        {
            get { return _CuentaDetraccion; }
            set { _CuentaDetraccion = value; }
        }

        private String _MonedaNombre;
        public String MonedaNombre
        {
            get { return _MonedaNombre; }
            set { _MonedaNombre = value; }
        }

        private String _MonedaNombreCorto;
        public String MonedaNombreCorto
        {
            get { return _MonedaNombreCorto; }
            set { _MonedaNombreCorto = value; }
        }

        private Decimal _MontoDetraccion;
        public Decimal MontoDetraccion
        {
            get { return _MontoDetraccion; }
            set { _MontoDetraccion = value; }
        }

        private Decimal _CalculoDetraccion;
        public Decimal CalculoDetraccion
        {
            get { return _CalculoDetraccion; }
            set { _CalculoDetraccion = value; }
        }

        private String _FechaAnticipo;
        public String FechaAnticipo
        {
            get { return _FechaAnticipo; }
            set { _FechaAnticipo = value; }
        }


        private String _DocAnticipo;
        public String DocAnticipo
        {
            get { return _DocAnticipo; }
            set { _DocAnticipo = value; }
        }

        private String _TipoDocAnticipo;
        public String TipoDocAnticipo
        {
            get { return _TipoDocAnticipo; }
            set { _TipoDocAnticipo = value; }
        }

        private Decimal _MontoAnticipo;
        public Decimal MontoAnticipo
        {
            get { return _MontoAnticipo; }
            set { _MontoAnticipo = value; }
        }

        private String _TipoOperacion;
        public String TipoOperacion
        {
            get { return _TipoOperacion; }
            set { _TipoOperacion = value; }
        }

        private String _FormaPago;
        public String FormaPago
        {
            get { return _FormaPago; }
            set { _FormaPago = value; }
        }

        private Decimal _CalculoIgv;
        public Decimal CalculoIgv
        {
            get { return _CalculoIgv; }
            set { _CalculoIgv = value; }
        }

        private Decimal _CalculoIsc;
        public Decimal CalculoIsc
        {
            get { return _CalculoIsc; }
            set { _CalculoIsc = value; }
        }

        private Decimal _PorcentajeDetraccion;
        public Decimal PorcentajeDetraccion
        {
            get { return _PorcentajeDetraccion; }
            set { _PorcentajeDetraccion = value; }
        }

        private String _Anticipo;
        public String Anticipo
        {
            get { return _Anticipo; }
            set { _Anticipo = value; }
        }

        private String _Moneda;
        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }

        private String _CodigoProducto;
        public String CodigoProducto
        {
            get { return _CodigoProducto; }
            set { _CodigoProducto = value; }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private String _IDTipoImpuesto;
        public String IDTipoImpuesto
        {
            get { return _IDTipoImpuesto; }
            set { _IDTipoImpuesto = value; }
        }
         
        private Decimal _SubTotal;
        public Decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }

        private String _CodigoCuenta;
        public String CodigoCuenta
        {
            get { return _CodigoCuenta; }
            set { _CodigoCuenta = value; }
        }

        private String _CodigoCuentaCaja;
        public String CodigoCuentaCaja
        {
            get { return _CodigoCuentaCaja; }
            set { _CodigoCuentaCaja = value; }
        }

        private String _Glosa;
        public String Glosa
        {
            get { return _Glosa; }
            set { _Glosa= value; }
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

        private Int32 _IDEmisor;
        public Int32 IDEmisor
        {
            get { return _IDEmisor; }
            set { _IDEmisor = value; }
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
         
        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
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
          
        private String _NumeroDocumentoCliente;
        public String NumeroDocumentoCliente
        {
            get { return _NumeroDocumentoCliente; }
            set { _NumeroDocumentoCliente = value; }
        }

        private String _Cliente;
        public String Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }
          
        private Int32 _NumeroOrdenItem;
        public Int32 NumeroOrdenItem
        {
            get { return _NumeroOrdenItem; }
            set { _NumeroOrdenItem = value; }
        }
         
        private String _IDUnidadMedida;
        public String IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }
         

        private Decimal _ImporteTotalSinImpuesto;
        public Decimal ImporteTotalSinImpuesto
        {
            get { return _ImporteTotalSinImpuesto; }
            set { _ImporteTotalSinImpuesto = value; }
        }

        private Decimal _ImporteUniSinImpuesto;
        public Decimal ImporteUniSinImpuesto
        {
            get { return _ImporteUniSinImpuesto; }
            set { _ImporteUniSinImpuesto = value; }
        }

        private Decimal _ImporteUniConImpuesto;
        public Decimal ImporteUniConImpuesto
        {
            get { return _ImporteUniConImpuesto; }
            set { _ImporteUniConImpuesto = value; }
        }

        private Decimal _ImporteIgv;
        public Decimal ImporteIgv
        {
            get { return _ImporteIgv; }
            set { _ImporteIgv = value; }
        }

        private Decimal _ImporteDescuento;
        public Decimal ImporteDescuento
        {
            get { return _ImporteDescuento; }
            set { _ImporteDescuento = value; }
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


        private String _MonedaSimbolo;
        public String MonedaSimbolo
        {
            get { return _MonedaSimbolo; }
            set { _MonedaSimbolo = value; }
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


        private String _Serie;
        public String Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        private String _Numero;
        public String Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        private String _OtrosIngresos;
        public String OtrosIngresos
        {
            get { return _OtrosIngresos; }
            set { _OtrosIngresos = value; }
        }

        private String _FechaEmisionAfectado;
        public String FechaEmisionAfectado
        {
            get { return _FechaEmisionAfectado; }
            set { _FechaEmisionAfectado = value; }
        }

        private String _TipoDocumentoAfectado;
        public String TipoDocumentoAfectado
        {
            get { return _TipoDocumentoAfectado; }
            set { _TipoDocumentoAfectado = value; }
        }

        private String _SerieAfectado;
        public String SerieAfectado
        {
            get { return _SerieAfectado; }
            set { _SerieAfectado = value; }
        }

        private String _NumeroAfectado;
        public String NumeroAfectado
        {
            get { return _NumeroAfectado; }
            set { _NumeroAfectado = value; }
        }

		private String _HoraEmision;
		public String HoraEmision
		{
			get { return _HoraEmision; }
			set { _HoraEmision = value; }
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

		private Int32 _NumeroC;
		public Int32 NumeroC
		{
			get { return _NumeroC; }
			set { _NumeroC = value; }
		}

		private Int32 _NroItems;
		public Int32 NroItems
		{
			get { return _NroItems; }
			set { _NroItems = value; }
		}

		private Int32 _IDVenta;
		public Int32 IDVenta
		{
			get { return _IDVenta; }
			set { _IDVenta = value; }
		}

		private Decimal _TotalDescuentoItems;
		public Decimal TotalDescuentoItems
		{
			get { return _TotalDescuentoItems; }
			set { _TotalDescuentoItems = value; }
		}
		

	}
}
