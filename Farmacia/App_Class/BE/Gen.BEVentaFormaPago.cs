using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEVentaFormaPago : BEBase
    {
        private Int32 _ID;
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private Int32 _IDVentaFormaPago;
        public Int32 IDVentaFormaPago
        {
            get { return _IDVentaFormaPago; }
            set { _IDVentaFormaPago = value; }
        }

        private Int32 _IDVenta;
        public Int32 IDVenta
        {
            get { return _IDVenta; }
            set { _IDVenta = value; }
        }
         
        private Int32 _IDFormaPago;
        public Int32 IDFormaPago
        {
            get { return _IDFormaPago; }
            set { _IDFormaPago = value; }
        }

        private String _FormaPago;
        public String FormaPago
        {
            get { return _FormaPago; }
            set { _FormaPago = value; }
        }

        private Int32 _IDTarjetaCredito;
        public Int32 IDTarjetaCredito
        {
            get { return _IDTarjetaCredito; }
            set { _IDTarjetaCredito = value; }
        }

        private String _TarjetaCredito;
        public String TarjetaCredito
        {
            get { return _TarjetaCredito; }
            set { _TarjetaCredito = value; }
        }

        private Decimal _MontoPagado;
        public Decimal MontoPagado
        {
            get { return _MontoPagado; }
            set { _MontoPagado = value; }
        }
        
        private Decimal _ImportePago;
        public Decimal ImportePago
        {
            get { return _ImportePago; }
            set { _ImportePago = value; }
        }

        private String _NumeroOperacion;
        public String NumeroOperacion
        {
            get { return _NumeroOperacion; }
            set { _NumeroOperacion = value; }
        }

		private Decimal _Efectivo;
		public Decimal Efectivo
		{
			get { return _Efectivo; }
			set { _Efectivo = value; }
		}

		private Decimal _Tarjeta;
		public Decimal Tarjeta
		{
			get { return _Tarjeta; }
			set { _Tarjeta = value; }
		}

		private Decimal _Transferencia;
		public Decimal Transferencia
		{
			get { return _Transferencia; }
			set { _Transferencia = value; }
		}

		private Decimal _Credito;
		public Decimal Credito
		{
			get { return _Credito; }
			set { _Credito = value; }
		}

		private String _Referencia;
		public String Referencia
		{
			get { return _Referencia; }
			set { _Referencia = value; }
		}

		private Int32 _DiaCredito;
		public Int32 DiaCredito
		{
			get { return _DiaCredito; }
			set { _DiaCredito = value; }
		}

		private DateTime _FechaVencimiento;
		public DateTime FechaVencimiento
		{
			get { return _FechaVencimiento; }
			set { _FechaVencimiento = value; }
		}

        private String _FechaPago;
        public String FechaPago
        {
            get { return _FechaPago; }
            set { _FechaPago = value; }
        }


    }
}
