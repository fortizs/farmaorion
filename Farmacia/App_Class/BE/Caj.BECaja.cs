using System; 

namespace Farmacia.App_Class.BE.Caja
{
    public class BECaja : BEBase
    {

		private Int32 _IDCaja;
		public Int32 IDCaja
		{
			get { return _IDCaja; }
			set { _IDCaja = value; }
		}

		private Int32 _IDMedioPago;
		public Int32 IDMedioPago
		{
			get { return _IDMedioPago; }
			set { _IDMedioPago = value; }
		}
		

		private Int32 _IDSucursal;
		public Int32 IDSucursal
		{
			get { return _IDSucursal; }
			set { _IDSucursal = value; }
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
        private String _NombreCaja;
        public String NombreCaja
        {
            get { return _NombreCaja; }
            set { _NombreCaja = value; }
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
		private Decimal _SaldoFinal;
		public Decimal SaldoFinal
		{
			get { return _SaldoFinal; }
			set { _SaldoFinal = value; }
		}
		private String _NombreEstado;
		public String NombreEstado
		{
			get { return _NombreEstado; }
			set { _NombreEstado = value; }
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


		private DateTime _Fecha;
		public DateTime Fecha
		{
			get { return _Fecha; }
			set { _Fecha = value; }
		}

		private Decimal _Importe;
		public Decimal Importe
		{
			get { return _Importe; }
			set { _Importe = value; }
		}
		 
		private String _Codigo;
		public String Codigo
		{
			get { return _Codigo; }
			set { _Codigo = value; }
		}
		private String _Nombre;
		public String Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
		private String _Responsable;
		public String Responsable
		{
			get { return _Responsable; }
			set { _Responsable = value; }
		}
		private Boolean _Estado;
		public Boolean Estado
		{
			get { return _Estado; }
			set { _Estado = value; }
		}
		 
		private String _Sucursal;
		public String Sucursal
		{
			get { return _Sucursal; }
			set { _Sucursal = value; }
		}

		private Int32 _IDCajaMecanica;
		public Int32 IDCajaMecanica
		{
			get { return _IDCajaMecanica; }
			set { _IDCajaMecanica = value; }
		}

		private String _CajaMecanica;
		public String CajaMecanica
		{
			get { return _CajaMecanica; }
			set { _CajaMecanica = value; }
		}

		private String _Cajero;
		public String Cajero
		{
			get { return _Cajero; }
			set { _Cajero = value; }
		}

		

		private Int32 _IDEstado;
		public Int32 IDEstado
		{
			get { return _IDEstado; }
			set { _IDEstado = value; }
		}


		private Int64 _IDCorteCaja;
		public Int64 IDCorteCaja
		{
			get { return _IDCorteCaja; }
			set { _IDCorteCaja = value; }
		}
	 
		private String _MedioPago;
		public String MedioPago
		{
			get { return _MedioPago; }
			set { _MedioPago = value; }
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

		private Decimal _Transferencia;
		public Decimal Transferencia
		{
			get { return _Transferencia; }
			set { _Transferencia = value; }
		}

		

	}
}