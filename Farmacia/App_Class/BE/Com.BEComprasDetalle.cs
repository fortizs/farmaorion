using System; 

namespace Farmacia.App_Class.BE.Compras
{
    public class BEComprasDetalle : BEBase
    { 
        private Int32 _IDComprasDetalle;
        public Int32 IDComprasDetalle
        {
            get { return _IDComprasDetalle; }
            set { _IDComprasDetalle = value; }
        }

        private Int32 _IDCompras;
        public Int32 IDCompras
        {
            get { return _IDCompras; }
            set { _IDCompras = value; }
        }

        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }

        private String _Nombre;
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private Decimal _CantidadVenta;
        public Decimal CantidadVenta
        {
            get { return _CantidadVenta; }
            set { _CantidadVenta = value; }
        }

        

        private String _DetalleProducto;
		public String DetalleProducto
		{
			get { return _DetalleProducto; }
			set { _DetalleProducto = value; }
		}
		
		private String _ProductoDetalle;
        public String ProductoDetalle
        {
            get { return _ProductoDetalle; }
            set { _ProductoDetalle = value; }
        }


        private Decimal _Stock;
        public Decimal Stock
        {
            get { return _Stock; }
            set { _Stock = value; }
        }

        private Int32 _Item;
        public Int32 Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        private Int32 _IDUnidadMedida;
        public Int32 IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }

        private Int32 _Cantidad;
        public Int32 Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private Decimal _SaldoCantidad;
        public Decimal SaldoCantidad
        {
            get { return _SaldoCantidad; }
            set { _SaldoCantidad = value; }
        }

        private Decimal _PrecioUnitario;
        public Decimal PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }

        private Decimal _SubTotal;
        public Decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
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

        private Boolean _ControlaLote;
        public Boolean ControlaLote
        {
            get { return _ControlaLote; }
            set { _ControlaLote = value; }
        }
        private Int32 _Factor;
        public Int32 Factor
        {
            get { return _Factor; }
            set { _Factor = value; }
        }
        private String _UnidadMedidaVenta;
        public String UnidadMedidaVenta
        {
            get { return _UnidadMedidaVenta; }
            set { _UnidadMedidaVenta = value; }
        }

    }
}
