using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEReservaFormaPago : BEBase
    {
        private Int32 _IDReservaFormaPago;
        public Int32 IDReservaFormaPago
        {
            get { return _IDReservaFormaPago; }
            set { _IDReservaFormaPago = value; }
        }

        private Int32 _IDReserva;
        public Int32 IDReserva
        {
            get { return _IDReserva; }
            set { _IDReserva = value; }
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

		private String _Referencia;
		public String Referencia
		{
			get { return _Referencia; }
			set { _Referencia = value; }
		}
		 
        
    }
}
