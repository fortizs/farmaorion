using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEConcepto : BEBase
    {

        private Int32 _IDConcepto;
        public Int32 IDConcepto
        {
            get { return _IDConcepto; }
            set { _IDConcepto = value; }
        }

        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

        private String _TipoConcepto;
        public String TipoConcepto
        {
            get { return _TipoConcepto; }
            set { _TipoConcepto = value; }
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

        private String _CuentaContable;
        public String CuentaContable
        {
            get { return _CuentaContable; }
            set { _CuentaContable = value; }
        }

        private String _CuentaPagoDiferido;
        public String CuentaPagoDiferido
        {
            get { return _CuentaPagoDiferido; }
            set { _CuentaPagoDiferido = value; }
        }

        private String _IDAspecto;
        public String IDAspecto
        {
            get { return _IDAspecto; }
            set { _IDAspecto = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

    }
}
