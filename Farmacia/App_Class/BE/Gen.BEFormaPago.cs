using System;

namespace Farmacia.App_Class.BE.General
{
    public class BEFormaPago : BEBase
    {
        private Int32 _IDFormaPago;
        public Int32 IDFormaPago
        {
            get { return _IDFormaPago; }
            set { _IDFormaPago = value; }
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

        private Int32 _NumeroDia;
        public Int32 NumeroDia
        {
            get { return _NumeroDia; }
            set { _NumeroDia = value; }
        }

    }
}
