using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App_Class.BE.General
{
    public class BEContratoProducto : BEBase
    { 
        private Int32 _IDContratoProducto;
        public Int32 IDContratoProducto
        {
            get { return _IDContratoProducto; }
            set { _IDContratoProducto = value; }
        }

        private Int32 _IDContrato;
        public Int32 IDContrato
        {
            get { return _IDContrato; }
            set { _IDContrato = value; }
        }

        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }

        private Int32 _IDUnidadMedida;
        public Int32 IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }

        private Int32 _Item;
        public Int32 Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private String _Descripcion;
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private Decimal _Cantidad;
        public Decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private Decimal _PrecioUnitario;
        public Decimal PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }

        private Decimal _PrecioUnitarioConIgv;
        public Decimal PrecioUnitarioConIgv
        {
            get { return _PrecioUnitarioConIgv; }
            set { _PrecioUnitarioConIgv = value; }
        }

        private Decimal _Igv;
        public Decimal Igv
        {
            get { return _Igv; }
            set { _Igv = value; }
        }

        private Decimal _Descuento;
        public Decimal Descuento
        {
            get { return _Descuento; }
            set { _Descuento = value; }
        }

        private Decimal _ImporteVenta;
        public Decimal ImporteVenta
        {
            get { return _ImporteVenta; }
            set { _ImporteVenta = value; }
        }

        private String _IDTipoPrecio;
        public String IDTipoPrecio
        {
            get { return _IDTipoPrecio; }
            set { _IDTipoPrecio = value; }
        }

        private String _IDTipoImpuesto;
        public String IDTipoImpuesto
        {
            get { return _IDTipoImpuesto; }
            set { _IDTipoImpuesto = value; }
        }
         
        private String _CodigoProducto;
        public String CodigoProducto
        {
            get { return _CodigoProducto; }
            set { _CodigoProducto = value; }
        }

        private String _Producto;
        public String Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }

        private String _UnidadMedida;
        public String UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private String _TipoImpuesto;
        public String TipoImpuesto
        {
            get { return _TipoImpuesto; }
            set { _TipoImpuesto = value; }
        }

        private String _DetalleProducto;
        public String DetalleProducto
        {
            get { return _DetalleProducto; }
            set { _DetalleProducto = value; }
        }

        private Decimal _ValorUnitario;
        public Decimal ValorUnitario
        {
            get { return _ValorUnitario; }
            set { _ValorUnitario = value; }
        }

        private Decimal _ValorUnitarioConIgv;
        public Decimal ValorUnitarioConIgv
        {
            get { return _ValorUnitarioConIgv; }
            set { _ValorUnitarioConIgv = value; }
        }

        private Decimal _SubTotal;
        public Decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }

        private Decimal _Stock;
        public Decimal Stock
        {
            get { return _Stock; }
            set { _Stock = value; }
        }

     

    }
}
