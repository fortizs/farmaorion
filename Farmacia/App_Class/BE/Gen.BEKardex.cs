using System; 

namespace Farmacia.App_Class.BE.General
{
    public class BEKardex : BEBase
    {

    


        private Int32 _IDVenta; 
        public Int32 IDVenta
        {
            get { return _IDVenta; }
            set { _IDVenta = value; }
        }

        //private Int32 _IDSucursal;
        //public Int32 IDSucursal
        //{
        //    get { return _IDSucursal; }
        //    set { _IDSucursal = value; }
        //}


        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }


        private String _Usuario;
        public String Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        private Int32 _IDKardex;
        public Int32 IDKardex
        {
            get { return _IDKardex; }
            set { _IDKardex = value; }
        } 

		private Int32 _IDCliente;
        public Int32 IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
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

        private Int32 _IDFormaPago;
        public Int32 IDFormaPago
        {
            get { return _IDFormaPago; }
            set { _IDFormaPago = value; }
        }
         
        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

        private Int32 _IDTipoDocumento;
        public Int32 IDTipoDocumento
        {
            get { return _IDTipoDocumento; }
            set { _IDTipoDocumento = value; }
        }

        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
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

        private Decimal _TotalVenta;
        public Decimal TotalVenta
        {
            get { return _TotalVenta; }
            set { _TotalVenta = value; }
        }

        private Decimal _TotalIGV;
        public Decimal TotalIGV
        {
            get { return _TotalIGV; }
            set { _TotalIGV = value; }
        }

        private String _Migrado;
        public String Migrado
        {
            get { return _Migrado; }
            set { _Migrado = value; }
        }
         
            
        private String _RucEmisor;
        public String RucEmisor
        {
            get { return _RucEmisor; }
            set { _RucEmisor = value; }
        }

        private String _RazonSocialEmisor;
        public String RazonSocialEmisor
        {
            get { return _RazonSocialEmisor; }
            set { _RazonSocialEmisor = value; }
        }

        private String _UbigeoEmisor;
        public String UbigeoEmisor
        {
            get { return _UbigeoEmisor; }
            set { _UbigeoEmisor = value; }
        }

        private String _DireccionEmisor;
        public String DireccionEmisor
        {
            get { return _DireccionEmisor; }
            set { _DireccionEmisor = value; }
        }

        private String _Moneda;
        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        private Decimal _SubtTotal;
        public Decimal SubtTotal
        {
            get { return _SubtTotal; }
            set { _SubtTotal = value; }
        }
         
        private String _Cajero;
        public String Cajero
        {
            get { return _Cajero; }
            set { _Cajero = value; }
        }

        private String _MontoLetras;
        public String MontoLetras
        {
            get { return _MontoLetras; }
            set { _MontoLetras = value; }
        }


        private Decimal _CalculoIGV;
        public Decimal CalculoIGV
        {
            get { return _CalculoIGV; }
            set { _CalculoIGV = value; }
        }

        private Decimal _CalculoISC;
        public Decimal CalculoISC
        {
            get { return _CalculoISC; }
            set { _CalculoISC = value; }
        }

        private Decimal _CalculoDetraccion;
        public Decimal CalculoDetraccion
        {
            get { return _CalculoDetraccion; }
            set { _CalculoDetraccion = value; }
        }

        private Decimal _TotalOperacionGravada;
        public Decimal TotalOperacionGravada
        {
            get { return _TotalOperacionGravada; }
            set { _TotalOperacionGravada = value; }
        }

        private Decimal _TotalOperacionExonerada;
        public Decimal TotalOperacionExonerada
        {
            get { return _TotalOperacionExonerada; }
            set { _TotalOperacionExonerada = value; }
        }

        private Decimal _TotalOperacionInafecta;
        public Decimal TotalOperacionInafecta
        {
            get { return _TotalOperacionInafecta; }
            set { _TotalOperacionInafecta = value; }
        }

        private Decimal _TotalOperacionGratuita;
        public Decimal TotalOperacionGratuita
        {
            get { return _TotalOperacionGratuita; }
            set { _TotalOperacionGratuita = value; }
        }

        private Decimal _TotalISC;
        public Decimal TotalISC
        {
            get { return _TotalISC; }
            set { _TotalISC = value; }
        }

        private Decimal _TotalDescuentos;
        public Decimal TotalDescuentos
        {
            get { return _TotalDescuentos; }
            set { _TotalDescuentos = value; }
        }

        private Decimal _TotalOtrosTributos;
        public Decimal TotalOtrosTributos
        {
            get { return _TotalOtrosTributos; }
            set { _TotalOtrosTributos = value; }
        }

         

    }
}
