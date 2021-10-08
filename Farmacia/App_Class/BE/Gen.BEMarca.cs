using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEMarca : BEBase
    {
        private Int32 _IDMarca; 
        public Int32 IDMarca
        {
            get { return _IDMarca; }
            set { _IDMarca = value; }
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