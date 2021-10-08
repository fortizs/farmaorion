using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEColor : BEBase
    {
        private Int32 _IDColor; 
        public Int32 IDColor
        {
            get { return _IDColor; }
            set { _IDColor = value; }
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