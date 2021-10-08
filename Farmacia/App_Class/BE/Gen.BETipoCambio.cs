using System;

namespace Farmacia.App_Class.BE.General
{
    public class BETipoCambio : BEBase
    {

        private Int32 _IDTipoCambio;
        public Int32 IDTipoCambio
        {
            get { return _IDTipoCambio; }
            set { _IDTipoCambio = value; }
        }

        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

        private DateTime _FechaPublicacion;
        public DateTime FechaPublicacion
        {
            get { return _FechaPublicacion; }
            set { _FechaPublicacion = value; }
        }

        private Decimal _PrecioCompra;
        public Decimal PrecioCompra
        {
            get { return _PrecioCompra; }
            set { _PrecioCompra = value; }
        }

        private Decimal _PrecioVenta;
        public Decimal PrecioVenta
        {
            get { return _PrecioVenta; }
            set { _PrecioVenta = value; }
        }

        private Int32 _NumeroDia;
        public Int32 NumeroDia
        {
            get { return _NumeroDia; }
            set { _NumeroDia = value; }
        }

        private String _Anio;
        public String Anio
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

    }
}