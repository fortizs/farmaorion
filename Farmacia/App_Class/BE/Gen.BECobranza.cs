using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.General
{
    public class BECobranza : BEBase
    {

        private Int32 _IDCobranza;
        public Int32 IDCobranza
        {
            get { return _IDCobranza; }
            set { _IDCobranza = value; }
        }

        private Int32 _IDVenta;
        public Int32 IDVenta
        {
            get { return _IDVenta; }
            set { _IDVenta = value; }
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
          
        private String _MedioPago;
        public String MedioPago
        {
            get { return _MedioPago; }
            set { _MedioPago = value; }
        }
        private String _Cliente;
        public String Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }
        private String _Banco;
        public String Banco
        {
            get { return _Banco; }
            set { _Banco = value; }
        }

        private String _CuentaBancaria;
        public String CuentaBancaria
        {
            get { return _CuentaBancaria; }
            set { _CuentaBancaria = value; }
        }

        private Decimal _MontoCobrado;
        public Decimal MontoCobrado
        {
            get { return _MontoCobrado; }
            set { _MontoCobrado = value; }
        }

        private DateTime _FechaCobro;
        public DateTime FechaCobro
        {
            get { return _FechaCobro; }
            set { _FechaCobro = value; }
        }

		private String _Observacion;
		public String Observacion
		{
			get { return _Observacion; }
			set { _Observacion = value; }
		}
		private String _NumeroCobranzaFormato;
		public String NumeroCobranzaFormato
		{
			get { return _NumeroCobranzaFormato; }
			set { _NumeroCobranzaFormato = value; }
		}
		private String _UsuarioCreacion;
		public String UsuarioCreacion
		{
			get { return _UsuarioCreacion; }
			set { _UsuarioCreacion = value; }
		}
		private DateTime _FechaCreacion;
		public DateTime FechaCreacion
		{
			get { return _FechaCreacion; }
			set { _FechaCreacion = value; }
		}

	}
}


