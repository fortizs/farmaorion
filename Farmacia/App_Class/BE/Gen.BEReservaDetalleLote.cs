using System;

namespace Farmacia.App_Class.BE.General
{
	public class BEReservaDetalleLote : BEBase
	{
		
			private Int32 _IDReservaDetalleLote;
		public Int32 IDReservaDetalleLote
        {
			get { return _IDReservaDetalleLote; }
			set { _IDReservaDetalleLote = value; }
		}

		private Int32 _IDReservaDetalle;
		public Int32 IDReservaDetalle
        {
			get { return _IDReservaDetalle; }
			set { _IDReservaDetalle = value; }
		}

		private Int32 _IDReserva;
		public Int32 IDReserva
		{
			get { return _IDReserva; }
			set { _IDReserva = value; }
		}

		private Int32 _IDProducto;
		public Int32 IDProducto
		{
			get { return _IDProducto; }
			set { _IDProducto = value; }
		}

		private Int32 _IDLote;
		public Int32 IDLote
		{
			get { return _IDLote; }
			set { _IDLote = value; }
		}

		private Decimal _CantidadLote;
		public Decimal CantidadLote
		{
			get { return _CantidadLote; }
			set { _CantidadLote = value; }
		}


		private Int32 _IDReservaDetalleLoteTemp;
		public Int32 IDReservaDetalleLoteTemp
		{
			get { return _IDReservaDetalleLoteTemp; }
			set { _IDReservaDetalleLoteTemp = value; }
		}
		private Int32 _IDReservaDetalleTemp;
		public Int32 IDReservaDetalleTemp
		{
			get { return _IDReservaDetalleTemp; }
			set { _IDReservaDetalleTemp = value; }
		}
		 
		private Int32 _IDSucursal;
		public Int32 IDSucursal
		{
			get { return _IDSucursal; }
			set { _IDSucursal = value; }
		}
		private Decimal _Cantidad;
		public Decimal Cantidad
		{
			get { return _Cantidad; }
			set { _Cantidad = value; }
		}
		private Int32 _IDUsuarioCreacion;
		public Int32 IDUsuarioCreacion
		{
			get { return _IDUsuarioCreacion; }
			set { _IDUsuarioCreacion = value; }
		}
		private DateTime _FechaCreacion;
		public DateTime FechaCreacion
		{
			get { return _FechaCreacion; }
			set { _FechaCreacion = value; }
		}
		private String _Lote;
		public String Lote
		{
			get { return _Lote; }
			set { _Lote = value; }
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
		private Decimal _StockActualLote;
		public Decimal StockActualLote
		{
			get { return _StockActualLote; }
			set { _StockActualLote = value; }
		}

		
	}
}