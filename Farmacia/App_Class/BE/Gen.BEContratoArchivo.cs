using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.General
{
    public class BEContratoArchivo : BEBase
    {

        private Int32 _IDContratoArchivo;
        public Int32 IDContratoArchivo
        {
            get { return _IDContratoArchivo; }
            set { _IDContratoArchivo = value; }
        }

        private Int32 _IDContrato;
        public Int32 IDContrato
        {
            get { return _IDContrato; }
            set { _IDContrato = value; }
        }

        private String _RutaArchivo;
        public String RutaArchivo
        {
            get { return _RutaArchivo; }
            set { _RutaArchivo = value; }
        }

        private String _NombreArchivo;
        public String NombreArchivo
        {
            get { return _NombreArchivo; }
            set { _NombreArchivo = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }


    }
}
