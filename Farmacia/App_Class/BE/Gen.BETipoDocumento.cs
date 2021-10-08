using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BETipoDocumento : BEBase
    {
        private Int32 _IDTipoDocumento;
        private String _Nombre;
  
        public Int32 IDTipoDocumento
        {
            get { return _IDTipoDocumento; }
            set { _IDTipoDocumento = value; }
        }
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private String _Codigo;
        public String Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
    }
}



