using System; 

namespace Farmacia.App_Class.BE
{
    public class BEComunicacionBajaDetalle : BEBase
    {
        private Int32 _IDComunicacionBajaDetalle;
        public Int32 IDComunicacionBajaDetalle
        {
            get { return _IDComunicacionBajaDetalle; }
            set { _IDComunicacionBajaDetalle = value; }
        }

        private Int32 _IDComunicacionBaja;
        public Int32 IDComunicacionBaja
        {
            get { return _IDComunicacionBaja; }
            set { _IDComunicacionBaja = value; }
        }

        private Int32 _NumeroItem;
        public Int32 NumeroItem
        {
            get { return _NumeroItem; }
            set { _NumeroItem = value; }
        }
         
        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        private String _SerieDocumentoBaja;
        public String SerieDocumentoBaja
        {
            get { return _SerieDocumentoBaja; }
            set { _SerieDocumentoBaja = value; }
        }

        private String _NumeroDocumentoBaja;
        public String NumeroDocumentoBaja
        {
            get { return _NumeroDocumentoBaja; }
            set { _NumeroDocumentoBaja = value; }
        }

        private String _MotivoBaja;
        public String MotivoBaja
        {
            get { return _MotivoBaja; }
            set { _MotivoBaja = value; }
        }
         
        private String _IDResumen;
        public String IDResumen
        {
            get { return _IDResumen; }
            set { _IDResumen = value; }
        }
          
        private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }
          
    }
}

