using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BECombo : BEBase
    {
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
