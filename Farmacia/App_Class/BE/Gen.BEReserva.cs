using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEReserva : BEBase
    {
        private Int32 _IDReserva; 
        public Int32 IDReserva
        {
            get { return _IDReserva; }
            set { _IDReserva = value; }
        }

        private Int32 _IDVenta;
        public Int32 IDVenta
        {
            get { return _IDVenta; }
            set { _IDVenta = value; }
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
         
        private String _Usuario;
        public String Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        private Int32 _IDKardex;
        public Int32 IDKardex
        {
            get { return _IDKardex; }
            set { _IDKardex = value; }
        } 

		private Int32 _IDCliente;
        public Int32 IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }

        private Int32 _IDTipoDocumento;
        public Int32 IDTipoDocumento
        {
            get { return _IDTipoDocumento; }
            set { _IDTipoDocumento = value; }
        }
         
        private String _NumeroDocumento;
        public String NumeroDocumento
        {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = value; }
        }

        private String _Cliente;
        public String Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }


		private String _TipoDocumento;
		public String TipoDocumento
		{
			get { return _TipoDocumento; }
			set { _TipoDocumento = value; }
		}
		

		private Int32 _IDFormaPago;
        public Int32 IDFormaPago
        {
            get { return _IDFormaPago; }
            set { _IDFormaPago = value; }
        }
         
        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

		private String _Simbolo;
		public String Simbolo
		{
			get { return _Simbolo; }
			set { _Simbolo = value; }
		}

		
		private Decimal _TipoCambio;
        public Decimal TipoCambio
        {
            get { return _TipoCambio; }
            set { _TipoCambio = value; }
        }
         
        private Int32 _IDTipoComprobante;
        public Int32 IDTipoComprobante
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

        private String _TipoComprobanteCodigoSunat;
        public String TipoComprobanteCodigoSunat
        {
            get { return _TipoComprobanteCodigoSunat; }
            set { _TipoComprobanteCodigoSunat = value; }
        }
        

        private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }

        private DateTime _FechaVenta;
        public DateTime FechaVenta
        {
            get { return _FechaVenta; }
            set { _FechaVenta = value; }
        }

        private DateTime _FechaAnticipo;
        public DateTime FechaAnticipo
        {
            get { return _FechaAnticipo; }
            set { _FechaAnticipo = value; }
        }


        private Decimal _TotalVenta;
        public Decimal TotalVenta
        {
            get { return _TotalVenta; }
            set { _TotalVenta = value; }
        }

        private Decimal _DeudaPendiente;
        public Decimal DeudaPendiente
        {
            get { return _DeudaPendiente; }
            set { _DeudaPendiente = value; }
        }
        

        private Decimal _TotalIGV;
        public Decimal TotalIGV
        {
            get { return _TotalIGV; }
            set { _TotalIGV = value; }
        }

        private String _Migrado;
        public String Migrado
        {
            get { return _Migrado; }
            set { _Migrado = value; }
        }

		private String _Proceso;
		public String Proceso
		{
			get { return _Proceso; }
			set { _Proceso = value; }
		}
		

		private Int32 _IDReservaRelacionado;
		public Int32 IDReservaRelacionado
        {
			get { return _IDReservaRelacionado; }
			set { _IDReservaRelacionado = value; }
		}

		

		private String _RucEmisor;
        public String RucEmisor
        {
            get { return _RucEmisor; }
            set { _RucEmisor = value; }
        }

        private String _RazonSocialEmisor;
        public String RazonSocialEmisor
        {
            get { return _RazonSocialEmisor; }
            set { _RazonSocialEmisor = value; }
        }

        private String _UbigeoEmisor;
        public String UbigeoEmisor
        {
            get { return _UbigeoEmisor; }
            set { _UbigeoEmisor = value; }
        }

        private String _DireccionEmisor;
        public String DireccionEmisor
        {
            get { return _DireccionEmisor; }
            set { _DireccionEmisor = value; }
        }

        private String _Moneda;
        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        private String _MonedaAnticipo;
        public String MonedaAnticipo
        {
            get { return _MonedaAnticipo; }
            set { _MonedaAnticipo = value; }
        }


        private Decimal _SubtTotal;
        public Decimal SubtTotal
        {
            get { return _SubtTotal; }
            set { _SubtTotal = value; }
        }
         
        private String _Cajero;
        public String Cajero
        {
            get { return _Cajero; }
            set { _Cajero = value; }
        }

        private String _MontoLetras;
        public String MontoLetras
        {
            get { return _MontoLetras; }
            set { _MontoLetras = value; }
        }


        private Decimal _CalculoIGV;
        public Decimal CalculoIGV
        {
            get { return _CalculoIGV; }
            set { _CalculoIGV = value; }
        }

        private Decimal _CalculoISC;
        public Decimal CalculoISC
        {
            get { return _CalculoISC; }
            set { _CalculoISC = value; }
        }

        private Decimal _CalculoDetraccion;
        public Decimal CalculoDetraccion
        {
            get { return _CalculoDetraccion; }
            set { _CalculoDetraccion = value; }
        }

        private Decimal _MontoDetraccion;
        public Decimal MontoDetraccion
        {
            get { return _MontoDetraccion; }
            set { _MontoDetraccion = value; }
        }


        private Decimal _TotalOperacionGravada;
        public Decimal TotalOperacionGravada
        {
            get { return _TotalOperacionGravada; }
            set { _TotalOperacionGravada = value; }
        }

        private Decimal _TotalOperacionExonerada;
        public Decimal TotalOperacionExonerada
        {
            get { return _TotalOperacionExonerada; }
            set { _TotalOperacionExonerada = value; }
        }

        private Decimal _TotalOperacionInafecta;
        public Decimal TotalOperacionInafecta
        {
            get { return _TotalOperacionInafecta; }
            set { _TotalOperacionInafecta = value; }
        }

        private Decimal _TotalOperacionGratuita;
        public Decimal TotalOperacionGratuita
        {
            get { return _TotalOperacionGratuita; }
            set { _TotalOperacionGratuita = value; }
        }

        private Decimal _TotalISC;
        public Decimal TotalISC
        {
            get { return _TotalISC; }
            set { _TotalISC = value; }
        }

        private Decimal _TotalDescuentos;
        public Decimal TotalDescuentos
        {
            get { return _TotalDescuentos; }
            set { _TotalDescuentos = value; }
        }

        private Decimal _TotalOtrosTributos;
        public Decimal TotalOtrosTributos
        {
            get { return _TotalOtrosTributos; }
            set { _TotalOtrosTributos = value; }
        }

        



        private String _DocAnticipo;
        public String DocAnticipo
        {
            get { return _DocAnticipo; }
            set { _DocAnticipo = value; }
        }

        private Int32 _IDTipoDocAnticipo;
        public Int32 IDTipoDocAnticipo
        {
            get { return _IDTipoDocAnticipo; }
            set { _IDTipoDocAnticipo = value; }
        }

        private Decimal _MontoAnticipo;
        public Decimal MontoAnticipo
        {
            get { return _MontoAnticipo; }
            set { _MontoAnticipo = value; }
        }

        private String _CodigoQR;
        public String CodigoQR
        {
            get { return _CodigoQR; }
            set { _CodigoQR = value; }
        }

        /******/

        private String _NombreSucursal;
        public String NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }

        private String _NumeroDocumentoCliente;
        public String NumeroDocumentoCliente
        {
            get { return _NumeroDocumentoCliente; }
            set { _NumeroDocumentoCliente = value; }
        }

        private String _NombreCliente;
        public String NombreCliente
        {
            get { return _NombreCliente; }
            set { _NombreCliente = value; }
        }

        private String _NombreFormaPago;
        public String NombreFormaPago
        {
            get { return _NombreFormaPago; }
            set { _NombreFormaPago = value; }
        }

		//adicione nuevos parametros atte custy

		private Int32 _IDUsuarioReg;
		public Int32 IDUsuarioReg
		{
			get { return _IDUsuarioReg; }
			set { _IDUsuarioReg = value; }
		}

		private String _RazonSocial;
		public String RazonSocial
		{
			get { return _RazonSocial; }
			set { _RazonSocial = value; }
		}

		private String _RazonSocial1;
		public String RazonSocial1
		{
			get { return _RazonSocial1; }
			set { _RazonSocial1 = value; }
		}

		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
		 
		private Decimal _TotalVentasproducto;
		public Decimal TotalVentasproducto
		{
			get { return _TotalVentasproducto; }
			set { _TotalVentasproducto = value; }
		}
		//actualizadoporcusty el ripo de dato el 30/04/2018
		private Decimal _Cantidaddeventasproducto;
		public Decimal Cantidaddeventasproducto
		{
			get { return _Cantidaddeventasproducto; }
			set { _Cantidaddeventasproducto = value; }
		}
		private String _Nombreproducto;
		public String Nombreproducto
		{
			get { return _Nombreproducto; }
			set { _Nombreproducto = value; }
		}
		private Decimal _StockActual;
		public Decimal StockActual
		{
			get { return _StockActual; }
			set { _StockActual = value; }
		}

        private Decimal _MontoPagadoTarjeta;
        public Decimal MontoPagadoTarjeta
        {
            get { return _MontoPagadoTarjeta; }
            set { _MontoPagadoTarjeta = value; }
        }

        private String _NumeroOperacion;
        public String NumeroOperacion
        {
            get { return _NumeroOperacion; }
            set { _NumeroOperacion = value; }
        }
		//custy
		private String _Producto;
		public String Producto
		{
			get { return _Producto; }
			set { _Producto = value; }
		}
		private Decimal _Preciocosto;
		public Decimal Preciocosto
		{
			get { return _Preciocosto; }
			set { _Preciocosto = value; }
		}
		private Decimal _Precioventa;
		public Decimal Precioventa
		{
			get { return _Precioventa; }
			set { _Precioventa = value; }
		}
		private Decimal _Preciounitario;
		public Decimal Preciounitario
		{
			get { return _Preciounitario; }
			set { _Preciounitario = value; }
		}

		private Decimal _Cantidad;
		public Decimal Cantidad
		{
			get { return _Cantidad; }
			set { _Cantidad = value; }
		}
		private Decimal _Total;
		public Decimal Total
		{
			get { return _Total; }
			set { _Total = value; }
		}
		private Decimal _Totalventa;
		public Decimal Totalventa
		{
			get { return _Totalventa; }
			set { _Totalventa = value; }
		}

		private String _montonotacredito;
		public String montonotacredito
		{
			get { return _montonotacredito; }
			set { _montonotacredito = value; }
		}

		private DateTime _FechaReg;
		public DateTime FechaReg
		{
			get { return _FechaReg; }
			set { _FechaReg = value; }
		}
		 
		private Decimal _TotalVentasporusuario;
		public Decimal TotalVentasporusuario
		{
			get { return _TotalVentasporusuario; }
			set { _TotalVentasporusuario = value; }
		}
		private Decimal _Costo;
		public Decimal Costo
		{
			get { return _Costo; }
			set { _Costo = value; }
		}
		private Decimal _Ganancia;
		public Decimal Ganancia
		{
			get { return _Ganancia; }	
			set { _Ganancia = value; }
		}

		private String _DireccionReceptor;
		public String DireccionReceptor
		{
			get { return _DireccionReceptor; }
			set { _DireccionReceptor = value; }
		}
		//-----------------------------------
		//30/04/2018
		private Decimal _Totalventa1;
		public Decimal Totalventa1
		{
			get { return _Totalventa1; }
			set { _Totalventa1 = value; }
		}

		private Decimal _Totalventa2;
		public Decimal Totalventa2
		{
			get { return _Totalventa2; }
			set { _Totalventa2 = value; }
		}

        //-----------------------------
        //13052018

        

        private String _DireccionCliente;
        public String DireccionCliente
        {
            get { return _DireccionCliente; }
            set { _DireccionCliente = value; }
        }


        private String _Nombrecomercialemisor;
		public String Nombrecomercialemisor
		{
			get { return _Nombrecomercialemisor; }
			set { _Nombrecomercialemisor = value; }
		}

        private Int32 _IDContrato;
        public Int32 IDContrato
        {
            get { return _IDContrato; }
            set { _IDContrato = value; }
        }

        private Int32 _IDEstado;
        public Int32 IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }

        private String _EstadoSunat;
        public String EstadoSunat
		{
            get { return _EstadoSunat; }
            set { _EstadoSunat = value; }
        }

		private String _EstadoCobranza;
		public String EstadoCobranza
		{
			get { return _EstadoCobranza; }
			set { _EstadoCobranza = value; }
		}
		 
        private Boolean _Anulado;
        public Boolean Anulado
        {
            get { return _Anulado; }
            set { _Anulado = value; }
        }

        private String _MotivoAnulacion;
        public String MotivoAnulacion
        {
            get { return _MotivoAnulacion; }
            set { _MotivoAnulacion = value; }
        }

		private String _Filtro;
		public String Filtro
		{
			get { return _Filtro; }
			set { _Filtro = value; }
		}

		private Int32 _IDEstadoCobranza;
		public Int32 IDEstadoCobranza
		{
			get { return _IDEstadoCobranza; }
			set { _IDEstadoCobranza = value; }
		}

		private String _FechaInicio;
		public String FechaInicio
		{
			get { return _FechaInicio; }
			set { _FechaInicio = value; }
		}

		private String _FechaFin;
		public String FechaFin
		{
			get { return _FechaFin; }
			set { _FechaFin = value; }
		}

		private Int32 _IDEstadoAlmacen;
		public Int32 IDEstadoAlmacen
		{
			get { return _IDEstadoAlmacen; }
			set { _IDEstadoAlmacen = value; }
		}

		
	}
}
