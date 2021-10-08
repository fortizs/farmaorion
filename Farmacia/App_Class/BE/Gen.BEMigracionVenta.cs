using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEMigracionVenta : BEBase
    {
        private String _Sucursal;  
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private String _NumeroDocumento;
        public String NumeroDocumento
        {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = value; }
        }

        private String _Cliente;
        public String Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        private String _TipoComprobante;
        public String TipoComprobante
        {
            get { return _TipoComprobante; }
            set { _TipoComprobante = value; }
        }

        private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }

        private DateTime _FechaVenta;
        public DateTime FechaVenta
        {
            get { return _FechaVenta; }
            set { _FechaVenta = value; }
        }

        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

        private Decimal _TotalVenta;
        public Decimal TotalVenta
        {
            get { return _TotalVenta; }
            set { _TotalVenta = value; }
        }

        private String _Migrado;
        public String Migrado
        {
            get { return _Migrado; }
            set { _Migrado = value; }
        }

        private Int32 _IDVenta;
        public Int32 IDVenta
        {
            get { return _IDVenta; }
            set { _IDVenta = value; }
        }
          
    }
}