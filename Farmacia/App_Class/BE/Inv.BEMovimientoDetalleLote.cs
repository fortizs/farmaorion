using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE
{
    public class BEMovimientoDetalleLote : BEBase
    {

        private Int32 _IDMovimiento;
        public Int32 IDMovimiento
        {
            get { return _IDMovimiento; }
            set { _IDMovimiento = value; }
        }

        private Int32 _IDMovimientoDetalle;
        public Int32 IDMovimientoDetalle
        {
            get { return _IDMovimientoDetalle; }
            set { _IDMovimientoDetalle = value; }
        }

        private Int32 _IDMovimientoDetalleLote;
        public Int32 IDMovimientoDetalleLote
        {
            get { return _IDMovimientoDetalleLote; }
            set { _IDMovimientoDetalleLote = value; }
        }

        private Int32 _IDMovimientoDetalleLoteTemp;

        public Int32 IDMovimientoDetalleLoteTemp
        {
            get { return _IDMovimientoDetalleLoteTemp; }
            set { _IDMovimientoDetalleLoteTemp = value; }
        }

        private Int32 _IDLote;
        public Int32 IDLote
        {
            get { return _IDLote; }
            set { _IDLote = value; }
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

        private Decimal _Cantidad;
        public Decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private String _Token;
        public String Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
    }
}