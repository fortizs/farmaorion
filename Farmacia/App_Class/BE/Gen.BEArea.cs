using System;

namespace Farmacia.App_Class.BE.General
{
    public class BEArea : BEBase
    { 
        private Int32 _IDArea;
        public Int32 IDArea
        {
            get { return _IDArea; }
            set { _IDArea = value; }
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

    }
}