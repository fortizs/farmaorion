using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEBanco : BEBase
    { 
        private Int32 _IDBanco;
        public Int32 IDBanco
        {
            get { return _IDBanco; }
            set { _IDBanco = value; }
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

        private String _IDFinanciera;
        public String IDFinanciera
        {
            get { return _IDFinanciera; }
            set { _IDFinanciera = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

    }
}
