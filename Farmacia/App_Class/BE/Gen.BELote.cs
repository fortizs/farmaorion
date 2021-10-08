using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Farmacia.App_Class.BE.General
{
    public class BELote : BEBase
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
        private Int32 _IDSucursal;
        public Int32 IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }
        private String _Lote;
        public String Lote
        {
            get { return _Lote; }
            set { _Lote = value; }
        }
        private Decimal _CantidadLote;
        public Decimal CantidadLote
        {
            get { return _CantidadLote; }
            set { _CantidadLote = value; }
        }

        private Decimal _Stock;
        public Decimal Stock
        {
            get { return _Stock; }
            set { _Stock = value; }
        }

        private DateTime _FechaVencimiento;
        public DateTime FechaVencimiento
        {
            get { return _FechaVencimiento; }
            set { _FechaVencimiento = value; }
        }
        private DateTime _FechaFabricacion;
        public DateTime FechaFabricacion
        {
            get { return _FechaFabricacion; }
            set { _FechaFabricacion = value; }
        }

		private String _Token;
		public String Token
		{
			get { return _Token; }
			set { _Token = value; }
		} 
	}
}