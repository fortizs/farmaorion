using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BECuentaBancaria : BEBase
    {

        private Int32 _IDCuentaBancaria;
        public Int32 IDCuentaBancaria
        {
            get { return _IDCuentaBancaria; }
            set { _IDCuentaBancaria = value; }
        }

        private Int32 _IDBanco;
        public Int32 IDBanco
        {
            get { return _IDBanco; }
            set { _IDBanco = value; }
        }

        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

        private String _NumeroCuentaBancaria;
        public String NumeroCuentaBancaria
        {
            get { return _NumeroCuentaBancaria; }
            set { _NumeroCuentaBancaria = value; }
        }

        private String _Descripcion;
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        private String _Moneda;
        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        private String _TipoCuenta;
        public String TipoCuenta
        {
            get { return _TipoCuenta; }
            set { _TipoCuenta = value; }
        }

        private Int32 _IDTipoCuenta;
        public Int32 IDTipoCuenta
        {
            get { return _IDTipoCuenta; }
            set { _IDTipoCuenta = value; }
        }

        private Int32 _Anio;
        public Int32 Anio
        {
            get { return _Anio; }
            set { _Anio = value; }
        }

        private String _Mes;
        public String Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }

        private Decimal _SaldoFinalMes;
        public Decimal SaldoFinalMes
        {
            get { return _SaldoFinalMes; }
            set { _SaldoFinalMes = value; }
        }
          
    }
}
