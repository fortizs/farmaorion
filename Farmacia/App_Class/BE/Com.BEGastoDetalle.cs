using System;

namespace Farmacia.App_Class.BE.Compras
{
    public class BEGastoDetalle : BEBase
    {
        private Int32 _IDGastoDetalle;
        public Int32 IDGastoDetalle
        {
            get { return _IDGastoDetalle; }
            set { _IDGastoDetalle = value; }
        }

        private Int32 _IDGasto;
        public Int32 IDGasto
        {
            get { return _IDGasto; }
            set { _IDGasto = value; }
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

		private String _DetalleProducto;
		public String DetalleProducto
		{
			get { return _DetalleProducto; }
			set { _DetalleProducto = value; }
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
        
         private Decimal _Igv;
        public Decimal Igv
        {
            get { return _Igv; }
            set { _Igv = value; }
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

        private Decimal _Total;
        public Decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
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

        private Boolean _AplicaIgv;
        public Boolean AplicaIgv
        {
            get { return _AplicaIgv; }
            set { _AplicaIgv = value; }
        }

        

    }
}
