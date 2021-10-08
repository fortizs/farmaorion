using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BETipoComprobante : BEBase
    {

        private Int32 _IDTipoComprobante;
        public Int32 IDTipoComprobante
        {
            get { return _IDTipoComprobante; }
            set { _IDTipoComprobante = value; }
        }

        private String _Sigla;
        public String Sigla
        {
            get { return _Sigla; }
            set { _Sigla = value; }
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

        private Int32 _IDTipoComprobanteContabilidad;
        public Int32 IDTipoComprobanteContabilidad
        {
            get { return _IDTipoComprobanteContabilidad; }
            set { _IDTipoComprobanteContabilidad = value; }
        }

        private String _IndDoc;
        public String IndDoc
        {
            get { return _IndDoc; }
            set { _IndDoc = value; }
        }

    }
}



