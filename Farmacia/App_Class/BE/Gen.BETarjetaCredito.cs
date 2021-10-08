using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BETarjetaCredito : BEBase
    { 
        private Int32 _IDTarjetaCredito;
        public Int32 IDTarjetaCredito
        {
            get { return _IDTarjetaCredito; }
            set { _IDTarjetaCredito = value; }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        } 
    }
}
