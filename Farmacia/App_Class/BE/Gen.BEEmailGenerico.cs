using Farmacia.App_Class.BE;
using System;

namespace Facturacion.BE.General
{
    public class BEEmailGenerico : BEBase
    {
        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private String _Email;
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private String _Archivo;
        public String Archivo
        {
            get { return _Archivo; }
            set { _Archivo = value; }
        }
    }
}