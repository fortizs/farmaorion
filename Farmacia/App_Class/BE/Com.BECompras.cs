using System;

namespace Farmacia.App_Class.BE.Compras
{
    public class BECompras : BEBase
    { 
        private Int32 _IDCompras;
        public Int32 IDCompras
        {
            get { return _IDCompras; }
            set { _IDCompras = value; }
        }

        private String _NumeroCompra;
        public String NumeroCompra
        {
            get { return _NumeroCompra; }
            set { _NumeroCompra = value; }
        }

        private String _Colaborador;
        public String Colaborador
        {
            get { return _Colaborador; }
            set { _Colaborador = value; }
        } 
        private String _RazonSocial;
        public String RazonSocial
        {
            get { return _RazonSocial; }
            set { _RazonSocial = value; }
        }

        private String _Sigla;
        public String Sigla
        {
            get { return _Sigla; }
            set { _Sigla = value; }
        }

        private DateTime _FechaEmisionReferencia;
        public DateTime FechaEmisionReferencia
        {
            get { return _FechaEmisionReferencia; }
            set { _FechaEmisionReferencia = value; }
        }

        private String _SerieReferencia;
        public String SerieReferencia
        {
            get { return _SerieReferencia; }
            set { _SerieReferencia = value; }
        }

        private String _NumeroComprobanteReferencia;
        public String NumeroComprobanteReferencia
        {
            get { return _NumeroComprobanteReferencia; }
            set { _NumeroComprobanteReferencia = value; }
        }

        private String _NumeroCompraFormato;
        public String NumeroCompraFormato
        {
            get { return _NumeroCompraFormato; }
            set { _NumeroCompraFormato = value; }
        }
        private Decimal _Saldo;
        public Decimal Saldo
        {
            get { return _Saldo; }
            set { _Saldo = value; }
        }
        private Int32 _IDTipoComprobante;
        public Int32 IDTipoComprobante
        {
            get { return _IDTipoComprobante; }
            set { _IDTipoComprobante = value; }
        }

        private String _EstadoPago;
        public String EstadoPago
        {
            get { return _EstadoPago; }
            set { _EstadoPago = value; }
        }

        private Int32 _IDEstadoCompra;
        public Int32 IDEstadoCompra
        {
            get { return _IDEstadoCompra; }
            set { _IDEstadoCompra = value; }
        }



        private Int32 _IDEstadoPago;
        public Int32 IDEstadoPago
        {
            get { return _IDEstadoPago; }
            set { _IDEstadoPago = value; }
        }
        private String _NumeroComprobante;
        public String NumeroComprobante
        {
            get { return _NumeroComprobante; }
            set { _NumeroComprobante = value; }
        }

        private String _MonedaSimbolo;
        public String MonedaSimbolo
        {
            get { return _MonedaSimbolo; }
            set { _MonedaSimbolo = value; }
        }

        private Int32 _NumeroCompraCuentas;
        public Int32 NumeroCompraCuentas
        {
            get { return _NumeroCompraCuentas; }
            set { _NumeroCompraCuentas = value; }
        }
        private String _TipoComprobanteSigla;
        public String TipoComprobanteSigla
        {
            get { return _TipoComprobanteSigla; }
            set { _TipoComprobanteSigla = value; }
        }

        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private String _RucProveedor;
        public String RucProveedor
        {
            get { return _RucProveedor; }
            set { _RucProveedor = value; }
        }

        private String _Proveedor;
        public String Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }

        private String _IDTipoComprobanteCS;
        public String IDTipoComprobanteCS
        {
            get { return _IDTipoComprobanteCS; }
            set { _IDTipoComprobanteCS = value; }
        }

        private String _TipoComprobante;
        public String TipoComprobante
        {
            get { return _TipoComprobante; }
            set { _TipoComprobante = value; }
        }

        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        private String _Moneda;
        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
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

        private DateTime _FechaCompra;
        public DateTime FechaCompra
        {
            get { return _FechaCompra; }
            set { _FechaCompra = value; }
        }

        private DateTime _FechaVencimiento;
        public DateTime FechaVencimiento
        {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = value; }
        }
         
        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

        private Decimal _SubTotal;
        public Decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }

        private Decimal _TotalIGV;
        public Decimal TotalIGV
        {
            get { return _TotalIGV; }
            set { _TotalIGV = value; }
        }

        private Decimal _TotalCompra;
        public Decimal TotalCompra
        {
            get { return _TotalCompra; }
            set { _TotalCompra = value; }
        }

        private String _Cuenta;
        public String Cuenta
        {
            get { return _Cuenta; }
            set { _Cuenta = value; }
        }

        private String _INDPagada;
        public String INDPagada
        {
            get { return _INDPagada; }
            set { _INDPagada = value; }
        }
         
        private String _CuentaCaja;
        public String CuentaCaja
        {
            get { return _CuentaCaja; }
            set { _CuentaCaja = value; }
        }

        private String _Glosa;
        public String Glosa
        {
            get { return _Glosa; }
            set { _Glosa = value; }
        }

        private Int32 _IDProveedor;
        public Int32 IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private Int32 _ProveedorIDTipoDocumento;
        public Int32 ProveedorIDTipoDocumento
        {
            get { return _ProveedorIDTipoDocumento; }
            set { _ProveedorIDTipoDocumento = value; }
        }

        
        private String _ProveedorNumeroDocumento;
        public String ProveedorNumeroDocumento
        {
            get { return _ProveedorNumeroDocumento; }
            set { _ProveedorNumeroDocumento = value; }
        }

        private String _ProveedorRazonSocial;
        public String ProveedorRazonSocial
        {
            get { return _ProveedorRazonSocial; }
            set { _ProveedorRazonSocial = value; }
        }
         
        private Int32 _IDTipoDocumento;
        public Int32 IDTipoDocumento
        {
            get { return _IDTipoDocumento; }
            set { _IDTipoDocumento = value; }
        }

        private Int32 _IDFormaPago;
        public Int32 IDFormaPago
        {
            get { return _IDFormaPago; }
            set { _IDFormaPago = value; }
        }

        private Int32 _IDAlmacen;
        public Int32 IDAlmacen
        {
            get { return _IDAlmacen; }
            set { _IDAlmacen = value; }
        }
         
        private String _TipoCompra;
        public String TipoCompra
        {
            get { return _TipoCompra; }
            set { _TipoCompra = value; }
        }

        private String _Serie;
        public String Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        private DateTime _FechaRegistro;
        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

		private DateTime _FechaDetraccion;
		public DateTime FechaDetraccion
		{
			get { return _FechaDetraccion; }
			set { _FechaDetraccion = value; }
		}
		

		private String _FormaPago;
        public String FormaPago
        {
            get { return _FormaPago; }
            set { _FormaPago = value; }
        }

		private String _EstadoNombre;
		public String EstadoNombre
		{
			get { return _EstadoNombre; }
			set { _EstadoNombre = value; }
		}
		

		private Decimal _DeudaPendiente;
        public Decimal DeudaPendiente
        {
            get { return _DeudaPendiente; }
            set { _DeudaPendiente = value; }
        }

        private Int32 _IDComprasPago;
        public Int32 IDComprasPago
        {
            get { return _IDComprasPago; }
            set { _IDComprasPago = value; }
        }

        private Int32 _IDMedioPago;
        public Int32 IDMedioPago
        {
            get { return _IDMedioPago; }
            set { _IDMedioPago = value; }
        }

        private Int32 _IDBanco;
        public Int32 IDBanco
        {
            get { return _IDBanco; }
            set { _IDBanco = value; }
        }

        private Decimal _MontoPagado;
        public Decimal MontoPagado
        {
            get { return _MontoPagado; }
            set { _MontoPagado = value; }
        }

        private String _CuentaBancaria;
        public String CuentaBancaria
        {
            get { return _CuentaBancaria; }
            set { _CuentaBancaria = value; }
        }

        private String _MedioPago;
        public String MedioPago
        {
            get { return _MedioPago; }
            set { _MedioPago = value; }
        }

        private String _Banco;
        public String Banco
        {
            get { return _Banco; }
            set { _Banco = value; }
        }


        private DateTime _FechaPago;
        public DateTime FechaPago
        {
            get { return _FechaPago; }
            set { _FechaPago = value; }
        }


        /*REGISTRO COMPRAS*/



        private String _Periodo;
        public String Periodo
        {
            get { return _Periodo; }
            set { _Periodo = value; }
        }

        private DateTime _FechaEmision;
        public DateTime FechaEmision
        {
            get { return _FechaEmision; }
            set { _FechaEmision = value; }
        }

        private String _CPTipoDocumento;
        public String CPTipoDocumento
        {
            get { return _CPTipoDocumento; }
            set { _CPTipoDocumento = value; }
        }

        private String _CPSerieDocumento;
        public String CPSerieDocumento
        {
            get { return _CPSerieDocumento; }
            set { _CPSerieDocumento = value; }
        }

        private String _CPNumeroDocumento;
        public String CPNumeroDocumento
        {
            get { return _CPNumeroDocumento; }
            set { _CPNumeroDocumento = value; }
        }

        private Int32 _PROVTipoDocumento;
        public Int32 PROVTipoDocumento
        {
            get { return _PROVTipoDocumento; }
            set { _PROVTipoDocumento = value; }
        }

        private String _PROVNumeroDocumento;
        public String PROVNumeroDocumento
        {
            get { return _PROVNumeroDocumento; }
            set { _PROVNumeroDocumento = value; }
        }

        private String _PROVRazonSocial;
        public String PROVRazonSocial
        {
            get { return _PROVRazonSocial; }
            set { _PROVRazonSocial = value; }
        }

        private Decimal _BaseImponible;
        public Decimal BaseImponible
        {
            get { return _BaseImponible; }
            set { _BaseImponible = value; }
        }

        private Decimal _Igv;
        public Decimal Igv
        {
            get { return _Igv; }
            set { _Igv = value; }
        }

        private Decimal _ImporteNoGravado;
        public Decimal ImporteNoGravado
        {
            get { return _ImporteNoGravado; }
            set { _ImporteNoGravado = value; }
        }

        private Decimal _ImporteTotal;
        public Decimal ImporteTotal
        {
            get { return _ImporteTotal; }
            set { _ImporteTotal = value; }
        }

        private String _FechaEmisionDocumentoModifica;
        public String FechaEmisionDocumentoModifica
        {
            get { return _FechaEmisionDocumentoModifica; }
            set { _FechaEmisionDocumentoModifica = value; }
        }

        private String _TipoDocumentoModifica;
        public String TipoDocumentoModifica
        {
            get { return _TipoDocumentoModifica; }
            set { _TipoDocumentoModifica = value; }
        }

        private String _SerieModifica;
        public String SerieModifica
        {
            get { return _SerieModifica; }
            set { _SerieModifica = value; }
        }

        private String _NumeroModifica;
        public String NumeroModifica
        {
            get { return _NumeroModifica; }
            set { _NumeroModifica = value; }
        }

        private String _FechaEmisionRetencion;
        public String FechaEmisionRetencion
        {
            get { return _FechaEmisionRetencion; }
            set { _FechaEmisionRetencion = value; }
        }

        private String _NumeroComprobanteRetencion;
        public String NumeroComprobanteRetencion
        {
            get { return _NumeroComprobanteRetencion; }
            set { _NumeroComprobanteRetencion = value; }
        }

        private String _TipoDocumentoReferencia;
        public String TipoDocumentoReferencia
        {
            get { return _TipoDocumentoReferencia; }
            set { _TipoDocumentoReferencia = value; }
        }

        private String _SerieNumeroDocumentoReferencia;
        public String SerieNumeroDocumentoReferencia
        {
            get { return _SerieNumeroDocumentoReferencia; }
            set { _SerieNumeroDocumentoReferencia = value; }
        }

        private DateTime _FechaEmisionDocumentoReferencia;
        public DateTime FechaEmisionDocumentoReferencia
        {
            get { return _FechaEmisionDocumentoReferencia; }
            set { _FechaEmisionDocumentoReferencia = value; }
        }

        private Int32 _IDEstado;
        public Int32 IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }

        private Int32 _IDEstadoAlmacen;
        public Int32 IDEstadoAlmacen
        {
            get { return _IDEstadoAlmacen; }
            set { _IDEstadoAlmacen = value; }
        }
         
        private String _EstadoCompra;
        public String EstadoCompra
        {
            get { return _EstadoCompra; }
            set { _EstadoCompra = value; }
        }

        private String _EstadoAlmacen;
        public String EstadoAlmacen
        {
            get { return _EstadoAlmacen; }
            set { _EstadoAlmacen = value; }
        }
        private Int32 _TipoComprobanteCompra;
        public Int32 TipoComprobanteCompra
        {
            get { return _TipoComprobanteCompra; }
            set { _TipoComprobanteCompra = value; }
        }
        

    }
}
