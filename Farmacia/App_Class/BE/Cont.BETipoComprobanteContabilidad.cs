using System;

namespace Farmacia.App_Class.BE.Contabilidad
{
    public class BETipoComprobanteContabilidad : BEBase
    {

        private Int32 _IDTipoComprobanteContabilidad;
        public Int32 IDTipoComprobanteContabilidad
        {
            get { return _IDTipoComprobanteContabilidad; }
            set { _IDTipoComprobanteContabilidad = value; }
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

        private String _CodigoSunat;
        public String CodigoSunat
        {
            get { return _CodigoSunat; }
            set { _CodigoSunat = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

    }
}
