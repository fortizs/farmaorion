using System;

namespace Farmacia.App_Class.BE.Inventario
{
    public class BEMovimiento : BEBase
    {

        private Int32 _IDMovimiento;
        public Int32 IDMovimiento
        {
            get { return _IDMovimiento; }
            set { _IDMovimiento = value; }
        }

        private Int32 _IDAlmacenOrigen;
        public Int32 IDAlmacenOrigen
        {
            get { return _IDAlmacenOrigen; }
            set { _IDAlmacenOrigen = value; }
        }


        private String _Codigo;
        public String Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
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

        private Int32 _IDAlmacenDestino;
        public Int32 IDAlmacenDestino
        {
            get { return _IDAlmacenDestino; }
            set { _IDAlmacenDestino = value; }
        }

        private Int32 _IDEntidad;
        public Int32 IDEntidad
        {
            get { return _IDEntidad; }
            set { _IDEntidad = value; }
        }
         
        private String _Entidad;
        public String Entidad
        {
            get { return _Entidad; }
            set { _Entidad = value; }
        }

        private Int32 _IDTransaccion;
        public Int32 IDTransaccion
        {
            get { return _IDTransaccion; }
            set { _IDTransaccion = value; }
        }

        private BETransaccion _Transaccion;
        public BETransaccion Transaccion
        {
            get { return _Transaccion; }
            set { _Transaccion = value; }
        }

        private BEAlmacen _Almacen;
        public BEAlmacen Almacen {
            get { return _Almacen; }
            set { _Almacen = value; }
        }

        private String _Observacion;
        public String Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }

		private String _Sucursal;
		public String Sucursal
		{
			get { return _Sucursal; }
			set { _Sucursal = value; }
		}
		  
		private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private Int32 _IDEmpresa;
        public Int32 IDEmpresa
        {
            get { return _IDEmpresa; }
            set { _IDEmpresa = value; }
        }


        private String _Token;
        public String Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        private DateTime _Fecha;
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

		private Int32 _IDTipoComprobante;
		public Int32 IDTipoComprobante
		{
			get { return _IDTipoComprobante; }
			set { _IDTipoComprobante = value; }
		}

		private Int32 _IDTipoComprobanteReferencia;
		public Int32 IDTipoComprobanteReferencia
		{
			get { return _IDTipoComprobanteReferencia; }
			set { _IDTipoComprobanteReferencia = value; }
		}

		private Int32 _IDProveedor;
		public Int32 IDProveedor
		{
			get { return _IDProveedor; }
			set { _IDProveedor = value; }
		}

		private String _NumeroReferencia;
		public String NumeroReferencia
		{
			get { return _NumeroReferencia; }
			set { _NumeroReferencia = value; }
		}

        private String _TipoMovimiento;
        public String TipoMovimiento
        {
            get { return _TipoMovimiento; }
            set { _TipoMovimiento = value; }
        }


        private Int32 _NumeroMovimiento;
        public Int32 NumeroMovimiento
        {
            get { return _NumeroMovimiento; }
            set { _NumeroMovimiento = value; }
        }

        public decimal SubTotal { get { return _SubTotal; } set { _SubTotal = value; } }
        public decimal TotalIGV { get { return _TotalIGV; } set { _TotalIGV = value; } }
        public decimal Total { get { return _Total; } set { _Total = value; } }

        private Decimal _SubTotal;
        private Decimal _TotalIGV;
        private Decimal _Total;

    }
}
