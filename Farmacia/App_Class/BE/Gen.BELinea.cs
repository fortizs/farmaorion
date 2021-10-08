using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BELinea : BEBase
    {
        private Int32 _IDLinea; 
        public Int32 IDLinea
        {
            get { return _IDLinea; }
            set { _IDLinea = value; }
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