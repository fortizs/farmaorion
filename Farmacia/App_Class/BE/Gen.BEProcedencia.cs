using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEProcedencia : BEBase
    {
        private Int32 _IDProcedencia; 
        public Int32 IDProcedencia
        {
            get { return _IDProcedencia; }
            set { _IDProcedencia = value; }
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