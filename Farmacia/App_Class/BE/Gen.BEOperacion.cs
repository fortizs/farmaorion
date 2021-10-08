using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEOperacion : BEBase
    {

        private Int32 _IDCaja;
        public Int32 IDCaja
        {
            get { return _IDCaja; }
            set { _IDCaja = value; }
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

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }


        private Int32 _IDOperacion;
        public Int32 IDOperacion
        {
            get { return _IDOperacion; }
            set { _IDOperacion = value; }
        }

        private String _TipoOperacion;
        public String TipoOperacion
        {
            get { return _TipoOperacion; }
            set { _TipoOperacion = value; }
        }
         

    }
}
