using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.General
{
    public class BECuentaPorPagar : BEBase
    {

        private Int32 _IDCuentaPagar;
        public Int32 IDCuentaPagar
        {
            get { return _IDCuentaPagar; }
            set { _IDCuentaPagar = value; }
        }
        private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }
        private Int32 _IDProveedor;
        public Int32 IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private Decimal _Importe;
        public Decimal Importe
        {
            get { return _Importe; }
            set { _Importe = value; }
        }
         
            private Int32 _IDEstado;
        public Int32 IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }


        private Int32 _IDFormaPago;
        public Int32 IDFormaPago
        {
            get { return _IDFormaPago; }
            set { _IDFormaPago = value; }
        }
        private Int32 _IDMoneda;
        public Int32 IDMoneda
        {
            get { return _IDMoneda; }
            set { _IDMoneda = value; }
        }
        private Int32 _IDAlmacen;
        public Int32 IDAlmacen
        {
            get { return _IDAlmacen; }
            set { _IDAlmacen = value; }
        }
        private Int32 _IDConcepto;
        public Int32 IDConcepto
        {
            get { return _IDConcepto; }
            set { _IDConcepto = value; }
        }
        private Int32 _IDBanco;
        public Int32 IDBanco
        {
            get { return _IDBanco; }
            set { _IDBanco = value; }
        }
        private Int32 _IDEmpresa;
        public Int32 IDEmpresa
        {
            get { return _IDEmpresa; }
            set { _IDEmpresa = value; }
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

        private String _CuentaCorriente;
        public String CuentaCorriente
        {
            get { return _CuentaCorriente; }
            set { _CuentaCorriente = value; }
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
        private DateTime _FechaAbono;
        public DateTime FechaAbono
        {
            get { return _FechaAbono; }
            set { _FechaAbono = value; }
        }
        private DateTime _FechaVencimiento;
        public DateTime FechaVencimiento
        {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = value; }
        }
        private String _CuentaBancaria;
        public String CuentaBancaria
        {
            get { return _CuentaBancaria; }
            set { _CuentaBancaria = value; }
        }
        private String _Observacion;
        public String Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }
        private Int32 _Estado;
        public Int32 Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        private Int32 _FormaPago;
        public Int32 FormaPago
        {
            get { return _FormaPago; }
            set { _FormaPago = value; }
        }
        private String _NombreAlmacen;
        public String NombreAlmacen
        {
            get { return _NombreAlmacen; }
            set { _NombreAlmacen = value; }
        }
        private String _NombreProveedor;
        public String NombreProveedor
        {
            get { return _NombreProveedor; }
            set { _NombreProveedor = value; }
        }
        private String _NombreSucursal;
        public String NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }

    }
}