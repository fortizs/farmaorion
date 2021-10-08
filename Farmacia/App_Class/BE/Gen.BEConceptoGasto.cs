using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.General
{
    public class BEConceptoGasto : BEBase
    {

        private Int32 _IDConceptoGasto;
        public Int32 IDConceptoGasto
        {
            get { return _IDConceptoGasto; }
            set { _IDConceptoGasto = value; }
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

        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }



    }
}
