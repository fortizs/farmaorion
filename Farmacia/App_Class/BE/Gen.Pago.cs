using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.General
{
    public class BEPago : BEBase
    {
        private Int32 _IDPago;
        public Int32 IDPago
        {
            get { return _IDPago; }
            set { _IDPago = value; }
        }

        private Int32 _IDCompra;
        public Int32 IDCompra
        {
            get { return _IDCompra; }
            set { _IDCompra = value; }
        }


        private Int32 _IDEmpresa;
        public Int32 IDEmpresa
        {
            get { return _IDEmpresa; }
            set { _IDEmpresa = value; }
        }

        private Int32 _IDProveedor;
        public Int32 IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private Int32 _IDConceptoPago;
        public Int32 IDConceptoPago
        {
            get { return _IDConceptoPago; }
            set { _IDConceptoPago = value; }
        }

        private Int32 _IDMedioPago;
        public Int32 IDMedioPago
        {
            get { return _IDMedioPago; }
            set { _IDMedioPago = value; }
        }

        private Int32 _IDBanco;
        public Int32 IDBanco
        {
            get { return _IDBanco; }
            set { _IDBanco = value; }
        }

        private Int32 _AnioPago;
        public Int32 AnioPago
        {
            get { return _AnioPago; }
            set { _AnioPago = value; }
        }

        private Int32 _NumeroPago;
        public Int32 NumeroPago
        {
            get { return _NumeroPago; }
            set { _NumeroPago = value; }
        }

        private String _CuentaCorriente;
        public String CuentaCorriente
        {
            get { return _CuentaCorriente; }
            set { _CuentaCorriente = value; }
        }

        private String _NumeroOperacion;
        public String NumeroOperacion
        {
            get { return _NumeroOperacion; }
            set { _NumeroOperacion = value; }
        }
        private String _Sucursal;
        public String Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }
        

        private String _IDMonedaPago;
        public String IDMonedaPago
        {
            get { return _IDMonedaPago; }
            set { _IDMonedaPago = value; }
        }

        private DateTime _FechaPago;
        public DateTime FechaPago
        {
            get { return _FechaPago; }
            set { _FechaPago = value; }
        }

        private Decimal _ImportePagado;
        public Decimal ImportePagado
        {
            get { return _ImportePagado; }
            set { _ImportePagado = value; }
        }

        private Decimal _Saldo;
        public Decimal Saldo
        {
            get { return _Saldo; }
            set { _Saldo = value; }
        }



        private String _Glosa;
        public String Glosa
        {
            get { return _Glosa; }
            set { _Glosa = value; }
        }

        private Int32 _IDEstado;
        public Int32 IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }

        private String _Concepto;
        public String Concepto
        {
            get { return _Concepto; }
            set { _Concepto = value; }
        }

        private String _Proveedor;
        public String Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }

        private String _Simbolo;
        public String Simbolo
        {
            get { return _Simbolo; }
            set { _Simbolo = value; }
        }

        private String _SerieNumero;
        public String SerieNumero
        {
            get { return _SerieNumero; }
            set { _SerieNumero = value; }
        }

        private String _NumeroPagoFormato;
        public String NumeroPagoFormato
        {
            get { return _NumeroPagoFormato; }
            set { _NumeroPagoFormato = value; }
        }

        private String _ProveedorNumeroDocumentoo;
        public String ProveedorNumeroDocumento
        {
            get { return _ProveedorNumeroDocumentoo; }
            set { _ProveedorNumeroDocumentoo = value; }
        }
    }
}