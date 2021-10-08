using System;

namespace Farmacia.App_Class.BE.General
{
    public class BESolicitante : BEBase
    {
        private Int32 _IDSolicitante;
        public Int32 IDSolicitante
        {
            get { return _IDSolicitante; }
            set { _IDSolicitante = value; }
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