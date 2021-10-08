using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEDocumentoSerie : BEBase
    {

        private Int32 _IDDocumentoSerie;
        public Int32 IDDocumentoSerie
        {
            get { return _IDDocumentoSerie; }
            set { _IDDocumentoSerie = value; }
        }

        private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private Int32 _IDTipoComprobante;
        public Int32 IDTipoComprobante
        {
            get { return _IDTipoComprobante; }
            set { _IDTipoComprobante = value; }
        }

        private String _Serie;
        public String Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        private Int32 _Numero;
        public Int32 Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        private Boolean _Estado;
        public Boolean Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        private String _DocumentoReferencia;
        public String DocumentoReferencia
        {
            get { return _DocumentoReferencia; }
            set { _DocumentoReferencia = value; }
        }

        private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }

        private String _TipoComprobante;
        public String TipoComprobante
        {
            get { return _TipoComprobante; }
            set { _TipoComprobante = value; }
        }

        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

    }
}
