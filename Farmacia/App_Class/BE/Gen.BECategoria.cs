using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BECategoria : BEBase
    {
        private Int32 _IDCategoria; 
        public Int32 IDCategoria
        {
            get { return _IDCategoria; }
            set { _IDCategoria = value; }
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