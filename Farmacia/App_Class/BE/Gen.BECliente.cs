using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BECliente : BEBase
    {
        private Int32 _IDCliente;
        public int IDCliente
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


        private String _NumeroDocumento = String.Empty;
        public String NumeroDocumento
        {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = value; }
        }


		private String _RazonSocial = String.Empty;
		public String RazonSocial
		{
			get { return _RazonSocial; }
			set { _RazonSocial = value; }
		}

		private String _NombreComercial = String.Empty;
		public String NombreComercial
		{
			get { return _NombreComercial; }
			set { _NombreComercial = value; }
		}


		private String _IDUbigeo ;
		public String IDUbigeo
		{
			get { return _IDUbigeo; }
			set { _IDUbigeo = value; }
		}


		private String _Direccion = String.Empty;
		public String Direccion
		{
			get { return _Direccion; }
			set { _Direccion = value; }
		}

        private String _Celular = String.Empty;
        public String Celular
        {
            get { return _Celular; }
            set { _Celular = value; }
        }

		private String _Telefono = String.Empty;
		public String Telefono
		{
			get { return _Telefono; }
			set { _Telefono = value; }
		}

		private DateTime _FechaNacimiento;
		public DateTime FechaNacimiento
		{
			get { return _FechaNacimiento; }
			set { _FechaNacimiento = value; }
		}

		private Int32 _Edad;
		public Int32 Edad
		{
			get { return _Edad; }
			set { _Edad = value; }
		}
		

		private String _Urbanizacion = String.Empty;
		public String Urbanizacion
		{
			get { return _Urbanizacion; }
			set { _Urbanizacion = value; }
		}

		private String _Sexo = String.Empty;
		public String Sexo
		{
			get { return _Sexo; }
			set { _Sexo = value; }
		}
		 
		private String _Correo = String.Empty;
		public String Correo
		{
			get { return _Correo; }
			set { _Correo = value; }
		}



		//el nombre de iddocumentoidentificacion
		private String _TipoDocumento = String.Empty;
		public String TipoDocumento
		{
			get { return _TipoDocumento; }
			set { _TipoDocumento = value; }
		}
		//el nombre de idubigeo
		private String _Distrito = String.Empty;
		public String Distrito
		{
			get { return _Distrito; }
			set { _Distrito = value; }
		}

        private String _Ubigeo = String.Empty;
        public String Ubigeo
        {
            get { return _Ubigeo; }
            set { _Ubigeo = value; }
        }

        private Decimal _ImporteTotal;
        public Decimal ImporteTotal
        {
            get { return _ImporteTotal; }
            set { _ImporteTotal = value; }
        }

        private Decimal _SaldoTotal;
        public Decimal SaldoTotal
        {
            get { return _SaldoTotal; }
            set { _SaldoTotal = value; }
        }


		private Decimal _LimiteCredito;
		public Decimal LimiteCredito
		{
			get { return _LimiteCredito; }
			set { _LimiteCredito = value; }
		}

		private Decimal _CreditoDisponible;
		public Decimal CreditoDisponible
		{
			get { return _CreditoDisponible; }
			set { _CreditoDisponible = value; }
		}


		private Int32 _DiasCredito;
		public Int32 DiasCredito
		{
			get { return _DiasCredito; }
			set { _DiasCredito = value; }
		}

		private DateTime _FechaVencimiento;
		public DateTime FechaVencimiento
		{
			get { return _FechaVencimiento; }
			set { _FechaVencimiento = value; }
		}


		

	}

}