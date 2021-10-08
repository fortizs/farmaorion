using System;

namespace Farmacia.App_Class.BE.General
{
    public class BEGuiaRemisionDetalle : BEBase
    {

        private Int32 _IDGuiaRemisionDetalle;
        public Int32 IDGuiaRemisionDetalle
        {
            get { return _IDGuiaRemisionDetalle; }
            set { _IDGuiaRemisionDetalle = value; }
        }

        private Int32 _IDGuiaRemision;
        public Int32 IDGuiaRemision
        {
            get { return _IDGuiaRemision; }
            set { _IDGuiaRemision = value; }
        }

        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
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

        private Decimal _Cantidad;
        public Decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
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

        private String _Descripcion;
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
         
    }
}
