using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE
{
    public class BENotaAjusteDetalleLote : BEBase
    {
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