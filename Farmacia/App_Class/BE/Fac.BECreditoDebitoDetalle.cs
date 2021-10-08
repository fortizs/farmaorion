using System; 

namespace Farmacia.App_Class.BE
{
    public class BECreditoDebitoDetalle : BEBase
    {
        private Int32 _IDCreditoDebitoDetalle;
        public Int32 IDCreditoDebitoDetalle
        {
            get { return _IDCreditoDebitoDetalle; }
            set { _IDCreditoDebitoDetalle = value; }
        }

        private Int32 _IDCreditoDebito;
        public Int32 IDCreditoDebito
        {
            get { return _IDCreditoDebito; }
            set { _IDCreditoDebito = value; }
        }

        private Int32 _NumeroOrdenItem;
        public Int32 NumeroOrdenItem
        {
            get { return _NumeroOrdenItem; }
            set { _NumeroOrdenItem = value; }
        }

        private String _CodigoProducto;
        public String CodigoProducto
        {
            get { return _CodigoProducto; }
            set { _CodigoProducto = value; }
        }

        private String _DescripcionProducto;
        public String DescripcionProducto
        {
            get { return _DescripcionProducto; }
            set { _DescripcionProducto = value; }
        }

        private Decimal _Cantidad;
        public Decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private String _UnidadMedida;
        public String UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private String _TipoPrecio;
        public String TipoPrecio
        {
            get { return _TipoPrecio; }
            set { _TipoPrecio = value; }
        }

        private Decimal _ImporteIgv;
        public Decimal ImporteIgv
        {
            get { return _ImporteIgv; }
            set { _ImporteIgv = value; }
        }

        private Decimal _ImporteUniConImpuesto;
        public Decimal ImporteUniConImpuesto
        {
            get { return _ImporteUniConImpuesto; }
            set { _ImporteUniConImpuesto = value; }
        }

        private Decimal _ImporteTotalSinImpuesto;
        public Decimal ImporteTotalSinImpuesto
        {
            get { return _ImporteTotalSinImpuesto; }
            set { _ImporteTotalSinImpuesto = value; }
        }

        private Decimal _ImporteReferencial;
        public Decimal ImporteReferencial
        {
            get { return _ImporteReferencial; }
            set { _ImporteReferencial = value; }
        }

        private Decimal _ImporteUniSinImpuesto;
        public Decimal ImporteUniSinImpuesto
        {
            get { return _ImporteUniSinImpuesto; }
            set { _ImporteUniSinImpuesto = value; }
        }

        private String _CodigoRazonExoneracion;
        public String CodigoRazonExoneracion
        {
            get { return _CodigoRazonExoneracion; }
            set { _CodigoRazonExoneracion = value; }
        }

        private Decimal _ImporteIsc;
        public Decimal ImporteIsc
        {
            get { return _ImporteIsc; }
            set { _ImporteIsc = value; }
        }

        private Decimal _ImporteDescuento;
        public Decimal ImporteDescuento
        {
            get { return _ImporteDescuento; }
            set { _ImporteDescuento = value; }
        }


        private String _CodigoAfectacionIgv;
        public String CodigoAfectacionIgv
        {
            get { return _CodigoAfectacionIgv; }
            set { _CodigoAfectacionIgv = value; }
        }


        private String _CodigoSistemaIsc;
        public String CodigoSistemaIsc
        {
            get { return _CodigoSistemaIsc; }
            set { _CodigoSistemaIsc = value; }
        }

        private String _CodigoImporteReferencial;
        public String CodigoImporteReferencial
        {
            get { return _CodigoImporteReferencial; }
            set { _CodigoImporteReferencial = value; }
        }

        private String _IDUnidadMedida;
        public String IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }

        private String _CodigoUnidadMedida;
        public String CodigoUnidadMedida
        {
            get { return _CodigoUnidadMedida; }
            set { _CodigoUnidadMedida = value; }
        }

        private String _Producto;
        public String Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }

        private Decimal _ImporteTotalConImpuesto;
        public Decimal ImporteTotalConImpuesto
        {
            get { return _ImporteTotalConImpuesto; }
            set { _ImporteTotalConImpuesto = value; }
        }

        private String _IDTipoImpuesto;
        public String IDTipoImpuesto
        {
            get { return _IDTipoImpuesto; }
            set { _IDTipoImpuesto = value; }
        }

        private String _TipoImpuesto;
        public String TipoImpuesto
        {
            get { return _TipoImpuesto; }
            set { _TipoImpuesto = value; }
        }
        



    }
}
