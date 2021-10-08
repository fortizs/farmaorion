using Farmacia.App_Class.BE.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE
{
    public class BEKitDetalle : BEBase
    {
        private Int32 _IDKitDetalle;
        public Int32 IDKitDetalle
        {
            get { return _IDKitDetalle; }
            set { _IDKitDetalle = value; }
        }        

        private BEProducto _Producto;
        public BEProducto Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }


        private Int32 _IDKit;
        public Int32 IDKit
        {
            get { return _IDKit; }
            set { _IDKit = value; }
        }

        private Int32 _IDProducto;
        public Int32 IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }

        private String _NombreProducto;
        public String NombreProducto
        {
            get { return _NombreProducto; }
            set { _NombreProducto = value; }
        }

        private Int32 _IDUnidadMedida;
        public Int32 IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }

        private String _UnidadMedida;
        public String UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private Decimal _CantidadReg;
        public Decimal CantidadReg
        {
            get { return _CantidadReg; }
            set { _CantidadReg = value; }
        }

        private Decimal _CantidadArmado;
        public Decimal CantidadArmado
        {
            get { return _CantidadArmado; }
            set { _CantidadArmado = value; }
        }

        private Decimal _CantidadDisponible;
        public Decimal CantidadDisponible
        {
            get { return _CantidadDisponible; }
            set { _CantidadDisponible = value; }
        }

        
        private Decimal _CantidadLoteDisponible;

        public Decimal CantidadLoteDisponible
        {
            get { return _CantidadLoteDisponible; }
            set { _CantidadLoteDisponible = value; }
        }

        private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        
    }
}