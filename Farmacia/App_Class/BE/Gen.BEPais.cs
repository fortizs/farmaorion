using System;

namespace Farmacia.App_Class.BE.General
{
    public class BEPais: BEBase
    {
        private String _IDPais;
        private String _Nombre;

        public String IDPais
        {
            get { return _IDPais; }
            set { _IDPais = value; }
        }        
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
    }
}