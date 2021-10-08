using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEMoneda : BEBase
    {
        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }
        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        private String _NombreCorto;
        public String NombreCorto
        {
            get { return _NombreCorto; }
            set { _NombreCorto = value; }
        } 
    }
}
