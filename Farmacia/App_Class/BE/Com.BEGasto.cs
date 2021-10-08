using System;

namespace Farmacia.App_Class.BE.Compras
{
    public class BEGasto : BEBase
    {
        private Int32 _IDGasto;
        public Int32 IDGasto
        {
            get { return _IDGasto; }
            set { _IDGasto = value; }
        }

        private String _NumeroCompra;
        public String NumeroCompra
        {
            get { return _NumeroCompra; }
            set { _NumeroCompra = value; }
        }

        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private String _RucProveedor;
        public String RucProveedor
        {
            get { return _RucProveedor; }
            set { _RucProveedor = value; }
        }

        private String _Proveedor;
        public String Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }

        private String _IDTipoComprobanteCS;
        public String IDTipoComprobanteCS
        {
            get { return _IDTipoComprobanteCS; }
            set { _IDTipoComprobanteCS = value; }
        }

        private String _TipoDocumento;
        public String TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        private String _Moneda;
        public String Moneda
        {
            get { return _Moneda; }
            set { _Moneda = value; }
        }

        private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }

        private String _SerieDocumento;
        public String SerieDocumento
        {
            get { return _SerieDocumento; }
            set { _SerieDocumento = value; }
        }

        private String _NumeroDocumento;
        public String NumeroDocumento
        {
            get { return _NumeroDocumento; }
            set { _NumeroDocumento = value; }
        }

        private DateTime _FechaCompra;
        public DateTime FechaCompra
        {
            get { return _FechaCompra; }
            set { _FechaCompra = value; }
        }

        private DateTime _FechaVencimiento;
        public DateTime FechaVencimiento
        {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = value; }
        }

        private DateTime _FechaDetraccion;
        public DateTime FechaDetraccion
        {
            get { return _FechaDetraccion; }
            set { _FechaDetraccion = value; }
        }

        private String _IDMoneda;
        public String IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }

        private Decimal _SubTotal;
        public Decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }

        private Decimal _TotalIGV;
        public Decimal TotalIGV
        {
            get { return _TotalIGV; }
            set { _TotalIGV = value; }
        }

        private Decimal _TotalCompra;
        public Decimal TotalCompra
        {
            get { return _TotalCompra; }
            set { _TotalCompra = value; }
        }

        private String _Cuenta;
        public String Cuenta
        {
            get { return _Cuenta; }
            set { _Cuenta = value; }
        }

        private String _CuentaCaja;
        public String CuentaCaja
        {
            get { return _CuentaCaja; }
            set { _CuentaCaja = value; }
        }

        private String _Glosa;
        public String Glosa
        {
            get { return _Glosa; }
            set { _Glosa = value; }
        }

        private Int32 _IDProveedor;
        public Int32 IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private Int32 _ProveedorIDTipoDocumento;
        public Int32 ProveedorIDTipoDocumento
        {
            get { return _ProveedorIDTipoDocumento; }
            set { _ProveedorIDTipoDocumento = value; }
        }
         
        private String _ProveedorNumeroDocumento;
        public String ProveedorNumeroDocumento
        {
            get { return _ProveedorNumeroDocumento; }
            set { _ProveedorNumeroDocumento = value; }
        }

        private String _ProveedorRazonSocial;
        public String ProveedorRazonSocial
        {
            get { return _ProveedorRazonSocial; }
            set { _ProveedorRazonSocial = value; }
        }

        private Int32 _IDTipoDocumento;
        public Int32 IDTipoDocumento
        {
            get { return _IDTipoDocumento; }
            set { _IDTipoDocumento = value; }
        }

        private Int32 _IDFormaPago;
        public Int32 IDFormaPago
        {
            get { return _IDFormaPago; }
            set { _IDFormaPago = value; }
        }

        private String _TipoCompra;
        public String TipoCompra
        {
            get { return _TipoCompra; }
            set { _TipoCompra = value; }
        }

        private String _Serie;
        public String Serie
        {
            get { return _Serie; }
            set { _Serie = value; }
        }

        private DateTime _FechaRegistro;
        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }

        private Int32 _IDEstado;
        public Int32 IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }

        private String _EstadoNombre;
        public String EstadoNombre
        {
            get { return _EstadoNombre; }
            set { _EstadoNombre = value; }
        }

        

    }
}
