using System; 

namespace Farmacia.App_Class.BE
{
    public class BEFacturaBoletaDetalle : BEBase
    {
        private Int32 _IDFacturaBoletaDetalle;
        public Int32 IDFacturaBoletaDetalle
        {
            get { return _IDFacturaBoletaDetalle; }
            set { _IDFacturaBoletaDetalle = value; }
        }

        private Int32 _IDFacturaBoleta;
        public Int32 IDFacturaBoleta
        {
            get { return _IDFacturaBoleta; }
            set { _IDFacturaBoleta = value; }
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

        private String _Producto;
        public String Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
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
        
        private String _UnidadMedidaSunat;
        public String UnidadMedidaSunat
        {
            get { return _UnidadMedidaSunat; }
            set { _UnidadMedidaSunat = value; }
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

        /**agregado 26022018*/
        private Int32 _Item;
        public Int32 Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }

        private String _IDUnidadMedida;
        public String IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }

        private Decimal _ImporteDescuento;
        public Decimal ImporteDescuento
        {
            get { return _ImporteDescuento; }
            set { _ImporteDescuento = value; }
        }

        private Decimal _TotalSinImpuesto;
        public Decimal TotalSinImpuesto
        {
            get { return _TotalSinImpuesto; }
            set { _TotalSinImpuesto = value; }
        }

        private Decimal _ImporteTotalConImpuesto;
        public Decimal ImporteTotalConImpuesto
        {
            get { return _ImporteTotalConImpuesto; }
            set { _ImporteTotalConImpuesto = value; }
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

        private String _TipoImpuesto;
        public String TipoImpuesto
        {
            get { return _TipoImpuesto; }
            set { _TipoImpuesto = value; }
        }

        private Decimal _PrecioUnitario;
        public Decimal PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }
        private Decimal _Igv;
        public Decimal Igv
        {
            get { return _Igv; }
            set { _Igv = value; }
        }

        private Decimal _PrecioReferencial;
        public Decimal PrecioReferencial
        {
            get { return _PrecioReferencial; }
            set { _PrecioReferencial = value; }
        }

        private Decimal _Descuento;
        public Decimal Descuento
        {
            get { return _Descuento; }
            set { _Descuento = value; }
        }

        private String _CodigoUnidadMedida;
        public String CodigoUnidadMedida
        {
            get { return _CodigoUnidadMedida; }
            set { _CodigoUnidadMedida = value; }
        }
  
        private String _TipoMoneda;
        public String TipoMoneda
        {
            get { return _TipoMoneda; }
            set { _TipoMoneda = value; }
        }

        
                    

    }
}
 
           