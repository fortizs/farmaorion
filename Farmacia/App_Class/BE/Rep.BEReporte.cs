using System;

namespace Farmacia.App_Class.BE.Reportes 
{
    public class BEReporte : BEBase
    { 
        private Int32 _IDVenta;
        public Int32 IDVenta
        {
            get { return _IDVenta; }
            set { _IDVenta = value; }
        }
        private Int32 _IDCliente;
        public Int32 IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }
        private Int32 _IDColaborador;
        public Int32 IDColaborador
        {
            get { return _IDColaborador; }
            set { _IDColaborador = value; }
        }

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

        private Int32 _IDFormaPago;
        public Int32 IDFormaPago
        {
            get { return _IDFormaPago; }
            set { _IDFormaPago = value; }
        }

        private Int32 _IDTipoComprobante2;
        public Int32 IDTipoComprobante2
        {
            get { return _IDTipoComprobante2; }
            set { _IDTipoComprobante2 = value; }
        }
        private Int32 _Item;
        public Int32 Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private Int32 _IDVentaDetalle;
        public Int32 IDVentaDetalle
        {
            get { return _IDVentaDetalle; }
            set { _IDVentaDetalle = value; }
        }

        private Int32 _ItemDetalle;
        public Int32 ItemDetalle
        {
            get { return _ItemDetalle; }
            set { _ItemDetalle = value; }
        }
        private Decimal _IGVDetalle;
        public Decimal IGVDetalle
        {
            get { return _IGVDetalle; }
            set { _IGVDetalle = value; }
        }
        private Decimal _ImporteTotal;
        public Decimal ImporteTotal
        {
            get { return _ImporteTotal; }
            set { _ImporteTotal = value; }
        }
        private Decimal _PorcentajeDescuento;
        public Decimal PorcentajeDescuento
        {
            get { return _PorcentajeDescuento; }
            set { _PorcentajeDescuento = value; }
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

        private Decimal _TotalDescuentos;
        public Decimal TotalDescuentos
        {
            get { return _TotalDescuentos; }
            set { _TotalDescuentos = value; }
        }
        private Decimal _TotalIGV;
        public Decimal TotalIGV
        {
            get { return _TotalIGV; }
            set { _TotalIGV = value; }
        }
        private String _NombreFormaPago;
        public String NombreFormaPago
        {
            get { return _NombreFormaPago; }
            set { _NombreFormaPago = value; }
        }

        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private String _Categoria;
        public String Categoria
        {
            get { return _Categoria; }
            set { _Categoria = value; }
        }

        
        private String _Venta;
        public String Venta
        {
            get { return _Venta; }
            set { _Venta = value; }
        }
        private Int32 _IDMovimientoCaja;
        public Int32 IDMovimientoCaja
        {
            get { return _IDMovimientoCaja; }
            set { _IDMovimientoCaja = value; }
        }

        private String _NombreTipoMovimiento;
        public String NombreTipoMovimiento
        {
            get { return _NombreTipoMovimiento; }
            set { _NombreTipoMovimiento = value; }
        }

        private DateTime _FechaMovimiento;
        public DateTime FechaMovimiento
        {
            get { return _FechaMovimiento; }
            set { _FechaMovimiento = value; }
        }

        private String _SiglaTipoComprobante;
        public String SiglaTipoComprobante
        {
            get { return _SiglaTipoComprobante; }
            set { _SiglaTipoComprobante = value; }
        }

        private Decimal _Monto;
        public Decimal Monto
        {
            get { return _Monto; }
            set { _Monto = value; }
        }
        private String _UsuarioCreacion;
        public String UsuarioCreacion
        {
            get { return _UsuarioCreacion; }
            set { _UsuarioCreacion = value; }
        }

        private String _Periodo;
        public String Periodo
        {
            get { return _Periodo; }
            set { _Periodo = value; }
        }

        private Decimal _CalculoIGV;
        public Decimal CalculoIGV
        {
            get { return _CalculoIGV; }
            set { _CalculoIGV = value; }
        }
        private Decimal _VentasTotal;
        public Decimal VentasTotal
        {
            get { return _VentasTotal; }
            set { _VentasTotal = value; }
        }

        

        private Decimal _PrecioVentaReporte;
        public Decimal PrecioVentaReporte
        {
            get { return _PrecioVentaReporte; }
            set { _PrecioVentaReporte = value; }
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
        private Decimal _TotalOperacionGravada;
        public Decimal TotalOperacionGravada
        {
            get { return _TotalOperacionGravada; }
            set { _TotalOperacionGravada = value; }
        }
        private Decimal _TotalIgv;
        public Decimal TotalIgv
        {
            get { return _TotalIgv; }
            set { _TotalIgv = value; }
        }

        private String _NombreCliente;
        public String NombreCliente
        {
            get { return _NombreCliente; }
            set { _NombreCliente = value; }
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

        private String _FormaPago;
        public String FormaPago
        {
            get { return _FormaPago; }
            set { _FormaPago = value; }
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

        private DateTime _FechaVenta;
        public DateTime FechaVenta
        {
            get { return _FechaVenta; }
            set { _FechaVenta = value; }
        }

        private String _Moneda;
        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        private String _Simbolo;
        public String Simbolo
        {
            get { return _Simbolo; }
            set { _Simbolo = value; }
        }

        private Decimal _TotalVenta;
        public Decimal TotalVenta
        {
            get { return _TotalVenta; }
            set { _TotalVenta = value; }
        }

		private Decimal _Utilidad;
		public Decimal Utilidad
		{
			get { return _Utilidad; }
			set { _Utilidad = value; }
		}

		

		private String _Cajero;
        public String Cajero
        {
            get { return _Cajero; }
            set { _Cajero = value; }
        }

        private DateTime _FechaReg;
        public DateTime FechaReg
        {
            get { return _FechaReg; }
            set { _FechaReg = value; }
        }

        private String _Producto;
        public String Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }

        private Int32 _CantidadVendida;
        public Int32 CantidadVendida
        {
            get { return _CantidadVendida; }
            set { _CantidadVendida = value; }
        }
        private Int32 _IdProductoPrecio;
        public Int32 IdProductoPrecio
        {
            get { return _IdProductoPrecio; }
            set { _IdProductoPrecio = value; }
        }

        

        private Decimal _TotalVendido;
        public Decimal TotalVendido
        {
            get { return _TotalVendido; }
            set { _TotalVendido = value; }
        }

        private String _Colaborador;
        public String Colaborador
        {
            get { return _Colaborador; }
            set { _Colaborador = value; }
        }


        private Int32 _IDContrato;
        public Int32 IDContrato
        {
            get { return _IDContrato; }
            set { _IDContrato = value; }
        }
         
        private String _NumeroPedidoFormateado;
        public String NumeroPedidoFormateado
        {
            get { return _NumeroPedidoFormateado; }
            set { _NumeroPedidoFormateado = value; }
        }

        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }
          
        private DateTime _FechaEntrega;
        public DateTime FechaEntrega
        {
            get { return _FechaEntrega; }
            set { _FechaEntrega = value; }
        }

        private DateTime _FechaPedido;
        public DateTime FechaPedido
        {
            get { return _FechaPedido; }
            set { _FechaPedido = value; }
        }

        private Decimal _MontoPedido;
        public Decimal MontoPedido
        {
            get { return _MontoPedido; }
            set { _MontoPedido = value; }
        }

        private String _CaracteristicaProducto;
        public String CaracteristicaProducto
        {
            get { return _CaracteristicaProducto; }
            set { _CaracteristicaProducto = value; }
        }

        private String _Observacion;
        public String Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }


        private Int32 _Codigo;
        public Int32 Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
         
        private String _IDTipoComprobante;
        public String IDTipoComprobante
        {
            get { return _IDTipoComprobante; }
            set { _IDTipoComprobante = value; }
        }

        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }
         
        private String _FechaEmision;
        public String FechaEmision
        {
            get { return _FechaEmision; }
            set { _FechaEmision = value; }
        }

        private String _NumeroDocumentoAdquiriente;
        public String NumeroDocumentoAdquiriente
        {
            get { return _NumeroDocumentoAdquiriente; }
            set { _NumeroDocumentoAdquiriente = value; }
        }

        private String _RazonSocialAdquiriente;
        public String RazonSocialAdquiriente
        {
            get { return _RazonSocialAdquiriente; }
            set { _RazonSocialAdquiriente = value; }
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

		private Int32 _IDCaja;
		public Int32 IDCaja
		{
			get { return _IDCaja; }
			set { _IDCaja = value; }
		}

		private String _Fecha;
		public String Fecha
		{
			get { return _Fecha; }
			set { _Fecha = value; }
		}

		private Decimal _Importe = 0;
		public Decimal Importe
		{
			get { return _Importe; }
			set { _Importe = value; }
		}


		private Decimal _SaldoInicial = 0;
		public Decimal SaldoInicial
		{
			get { return _SaldoInicial; }
			set { _SaldoInicial = value; }
		}

		private Decimal _TotalIngresos = 0;
		public Decimal TotalIngresos
		{
			get { return _TotalIngresos; }
			set { _TotalIngresos = value; }
		}

		private Decimal _TotalEgresos = 0;
		public Decimal TotalEgresos
		{
			get { return _TotalEgresos; }
			set { _TotalEgresos = value; }
		}

		private Decimal _SaldoFinal = 0;
		public Decimal SaldoFinal
		{
			get { return _SaldoFinal; }
			set { _SaldoFinal = value; }
		}
		private String _UsuarioOpen;
		public String UsuarioOpen
		{
			get { return _UsuarioOpen; }
			set { _UsuarioOpen = value; }
		}

		private String _UsuarioClose;
		public String UsuarioClose
		{
			get { return _UsuarioClose; }
			set { _UsuarioClose = value; }
		}
		 
		private String _OpenClose;
		public String OpenClose
		{
			get { return _OpenClose; }
			set { _OpenClose = value; }
		}
		 

		private String _RazonSocial;
		public String RazonSocial
		{
			get { return _RazonSocial; }
			set { _RazonSocial = value; }
		}
		 

		private String _FechaSeparacion;
		public String FechaSeparacion
		{
			get { return _FechaSeparacion; }
			set { _FechaSeparacion = value; }
		}

		private String _TipoOperacion;
		public String TipoOperacion
		{
			get { return _TipoOperacion; }
			set { _TipoOperacion = value; }
		}
		 

		private Decimal _TotalEfectivo = 0;
		public Decimal TotalEfectivo
		{
			get { return _TotalEfectivo; }
			set { _TotalEfectivo = value; }
		}

		private Decimal _TotalCredito = 0;
		public Decimal TotalCredito
		{
			get { return _TotalCredito; }
			set { _TotalCredito = value; }
		}

		private Decimal _TotalTransferencia = 0;
		public Decimal TotalTransferencia
		{
			get { return _TotalTransferencia; }
			set { _TotalTransferencia = value; }
		}

		private Decimal _TotalCheque = 0;
		public Decimal TotalCheque
		{
			get { return _TotalCheque; }
			set { _TotalCheque = value; }
		}

		private Decimal _TotalVisa = 0;
		public Decimal TotalVisa
		{
			get { return _TotalVisa; }
			set { _TotalVisa = value; }
		}

		private Decimal _TotalMastercard = 0;
		public Decimal TotalMastercard
		{
			get { return _TotalMastercard; }
			set { _TotalMastercard = value; }
		}

		private Decimal _TotalAExpress = 0;
		public Decimal TotalAExpress
		{
			get { return _TotalAExpress; }
			set { _TotalAExpress = value; }
		}

		private Decimal _TotalCompra = 0;
		public Decimal TotalCompra
		{
			get { return _TotalCompra; }
			set { _TotalCompra = value; }
		}

		private String _Anulado;
		public String Anulado
		{
			get { return _Anulado; }
			set { _Anulado = value; }
		}

		private String _DescripcionOperacion;
		public String DescripcionOperacion
		{
			get { return _DescripcionOperacion; }
			set { _DescripcionOperacion = value; }
		}

		private String _CuentaContable;
		public String CuentaContable
		{
			get { return _CuentaContable; }
			set { _CuentaContable = value; }
		}

		private String _DescripcionCuenta;
		public String DescripcionCuenta
		{
			get { return _DescripcionCuenta; }
			set { _DescripcionCuenta = value; }
		}

		private Decimal _Deudor = 0;
		public Decimal Deudor
		{
			get { return _Deudor; }
			set { _Deudor = value; }
		}

		private Decimal _Acreedor = 0;
		public Decimal Acreedor
		{
			get { return _Acreedor; }
			set { _Acreedor = value; }
		}

        private String _NombreProducto;
        public String NombreProducto
        {
            get { return _NombreProducto; }
            set { _NombreProducto = value; }
        }
        private Decimal _Cantidad;
        public Decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }
        private Decimal _PrecioUnitario;
        public Decimal PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }
        private Decimal _SubTotal;
        public Decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }

        private Decimal _ValorUnitario;
        public Decimal ValorUnitario
        {
            get { return _ValorUnitario; }
            set { _ValorUnitario = value; }
        }
        

        private String _Migrado;
        public String Migrado
        {
            get { return _Migrado; }
            set { _Migrado = value; }
        }

        private String _EstadoVenta;
        public String EstadoVenta
        {
            get { return _EstadoVenta; }
            set { _EstadoVenta = value; }
        }
        private String _EstadoCobranza;
        public String EstadoCobranza
        {
            get { return _EstadoCobranza; }
            set { _EstadoCobranza = value; }
        }
		private String _Pedido;
		public String Pedido
		{
			get { return _Pedido; }
			set { _Pedido = value; }
		}
		
		private String _Cobrado;
        public String Cobrado
        {
            get { return _Cobrado; }
            set { _Cobrado = value; }
        }
        private Decimal _TipoCambio;
        public Decimal TipoCambio
        {
            get { return _TipoCambio; }
            set { _TipoCambio = value; }
        }
        private String _MotivoAnulacion;
        public String MotivoAnulacion
        {
            get { return _MotivoAnulacion; }
            set { _MotivoAnulacion = value; }
        }
        private DateTime _FechaAnulado;
        public DateTime FechaAnulado
        {
            get { return _FechaAnulado; }
            set { _FechaAnulado = value; }
        }

        private Int32 _CodigoProducto;
        public Int32 CodigoProducto
        {
            get { return _CodigoProducto; }
            set { _CodigoProducto = value; }
        }
        private String _CodigoBarraProducto;
        public String CodigoBarraProducto
        {
            get { return _CodigoBarraProducto; }
            set { _CodigoBarraProducto = value; }
        }

        private String _PrincipioActivo;
        public String PrincipioActivo
        {
            get { return _PrincipioActivo; }
            set { _PrincipioActivo = value; }
        }
        private String _NombreCategoria;
        public String NombreCategoria
        {
            get { return _NombreCategoria; }
            set { _NombreCategoria = value; }
        }
        private String _NombreUnidadMedida;
        public String NombreUnidadMedida
        {
            get { return _NombreUnidadMedida; }
            set { _NombreUnidadMedida = value; }
        }
        private Decimal _PrecioVenta;
        public Decimal PrecioVenta
        {
            get { return _PrecioVenta; }
            set { _PrecioVenta = value; }
        }

        private DateTime _FechaApertura;
        public DateTime FechaApertura
        {
            get { return _FechaApertura; }
            set { _FechaApertura = value; }
        }
        private DateTime _FechaCierre;
        public DateTime FechaCierre
        {
            get { return _FechaCierre; }
            set { _FechaCierre = value; }
        }
        private DateTime _FechaReAperturaCaja;
        public DateTime FechaReAperturaCaja
        {
            get { return _FechaReAperturaCaja; }
            set { _FechaReAperturaCaja = value; }
        }
        private String _UsuarioApertura;
        public String UsuarioApertura
        {
            get { return _UsuarioApertura; }
            set { _UsuarioApertura = value; }
        }
        private String _UsuarioCierre;
        public String UsuarioCierre
        {
            get { return _UsuarioCierre; }
            set { _UsuarioCierre = value; }
        }
        private String _UsuarioReaApertura;
        public String UsuarioReaApertura
        {
            get { return _UsuarioReaApertura; }
            set { _UsuarioReaApertura = value; }
        }
        private Decimal _MontoApertura;
        public Decimal MontoApertura
        {
            get { return _MontoApertura; }
            set { _MontoApertura = value; }
        }

        private Decimal _TotalIngreso;
        public Decimal TotalIngreso
        {
            get { return _TotalIngreso; }
            set { _TotalIngreso = value; }
        }
        private Decimal _TotalEgreso;
        public Decimal TotalEgreso
        {
            get { return _TotalEgreso; }
            set { _TotalEgreso = value; }
        }

        private String _NombreEstado;
        public String NombreEstado
        {
            get { return _NombreEstado; }
            set { _NombreEstado = value; }
        }
        private String _CajaMecanica;
        public String CajaMecanica
        {
            get { return _CajaMecanica; }
            set { _CajaMecanica = value; }
        }

        private Int32 _IDEstado;
        public Int32 IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }

        private DateTime _FechaCaja;
        public DateTime FechaCaja
        {
            get { return _FechaCaja; }
            set { _FechaCaja = value; }
        }

		private String _Operacion;
		public String Operacion
		{
			get { return _Operacion; }
			set { _Operacion = value; }
		}

		private Decimal _Contado;
		public Decimal Contado
		{
			get { return _Contado; }
			set { _Contado = value; }
		}

		private Decimal _Calculado;
		public Decimal Calculado
		{
			get { return _Calculado; }
			set { _Calculado = value; }
		}

		private Decimal _Diferencia;
		public Decimal Diferencia
		{
			get { return _Diferencia; }
			set { _Diferencia = value; }
		}

		private Decimal _Retiro;
		public Decimal Retiro
		{
			get { return _Retiro; }
			set { _Retiro = value; }
		}

		private Decimal _Precio1;
		public Decimal Precio1
		{
			get { return _Precio1; }
			set { _Precio1 = value; }
		}


		private Decimal _Precio2;
		public Decimal Precio2
		{
			get { return _Precio2; }
			set { _Precio2 = value; }
		}


		private Decimal _Precio3;
		public Decimal Precio3
		{
			get { return _Precio3; }
			set { _Precio3 = value; }
		}
		 
		private String _CodEstab;
		public String CodEstab
		{
			get { return _CodEstab; }
			set { _CodEstab = value; }
		}

		private String _CodProd;
		public String CodProd
		{
			get { return _CodProd; }
			set { _CodProd = value; }
		}
		 

	}
}
