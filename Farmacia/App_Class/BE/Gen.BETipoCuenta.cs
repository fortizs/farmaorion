using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.General
{
    public class BETipoCuenta : BEBase
    {

        private Int32 _IDTipoCuenta;
        public Int32 IDTipoCuenta
        {
            get { return _IDTipoCuenta; }
            set { _IDTipoCuenta = value; }
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

    }
}
