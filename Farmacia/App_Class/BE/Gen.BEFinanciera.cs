using System;

namespace Farmacia.App_Class.BE.General
{
    public class BEFinanciera : BEBase
    {
        private String _IDFinanciera;
        public String IDFinanciera
        {
            get { return _IDFinanciera; }
            set { _IDFinanciera = value; }
        }
        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

    }
}
