using System;

namespace Farmacia.App_Class.BE.Compras
{
    public class BERequisicionDetalle : BEBase
    {
        private Int32 _IDRequisicionDetalle;
        public Int32 IDRequisicionDetalle
        {
            get { return _IDRequisicionDetalle; }
            set { _IDRequisicionDetalle = value; }
        }

        private Int32 _IDRequisicion;
        public Int32 IDRequisicion
        {
            get { return _IDRequisicion; }
            set { _IDRequisicion = value; }
        }

        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }

        private Int32 _IDCentroCosto;
        public Int32 IDCentroCosto
        {
            get { return _IDCentroCosto; }
            set { _IDCentroCosto = value; }
        }

        private Int32 _Cantidad;
        public Int32 Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private String _Glosa;
        public String Glosa
        {
            get { return _Glosa; }
            set { _Glosa = value; }
        }

        private String _Producto;
        public String Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }

        private String _CentroCosto;
        public String CentroCosto
        {
            get { return _CentroCosto; }
            set { _CentroCosto = value; }
        }

        private String _Codigo;
        public String Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        private String _UnidadMedida;
        public String UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private Int32 _IDUnidadMedida;
        public Int32 IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }
        


    }
}
